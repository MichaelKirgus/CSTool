﻿'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolUserSettingsLib
Imports CSToolUserSettingsManager

Public Class WorkspaceManager
    Public _parent As MainForm
    Public UserSettingsFilename As String = "UserSettings.xml"
    Public ProfilePath As String = ""

    Private Sub WorkspaceManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ParentDir As New IO.DirectoryInfo(_parent.CurrentUserProfilePath)
        ProfilePath = ParentDir.Parent.FullName

        LoadCustomWorkspacesToGUI()
        ListView1.Items(0).Selected = True
    End Sub

    Public Sub AddNewWorkspaceToGUI(ByVal CloneFromItem As Boolean)
        Try
            Dim itm As New ListViewItem

            If CloneFromItem Then
                Dim cloneobj As New UserSettings
                If ListView1.SelectedItems(0).Index = 0 Then
                    cloneobj = _parent.UserSettings.Clone
                    cloneobj.UserTemplates.Clear()
                Else
                    cloneobj = _parent.UserSettings.UserTemplates(ListView1.SelectedItems(0).Index - 1)
                End If

                itm.Text = cloneobj.TemplateName & " (duplicate)"
                itm.SubItems.Add(cloneobj.TemplateDescription)
                itm.Checked = cloneobj.Autostart

                Dim newdirguid As String
                newdirguid = Guid.NewGuid.ToString

                If ListView1.SelectedItems(0).Index = 0 Then
                    Dim Folderstr As String
                    Folderstr = _parent.CurrentUserProfilePath
                    My.Computer.FileSystem.CopyDirectory(Folderstr, ProfilePath & "\" & newdirguid)
                Else
                    Dim Folderstr As String
                    Folderstr = ProfilePath & "\" & _parent.UserSettings.UserTemplates(ListView1.SelectedItems(0).Index - 1).TemplateName
                    My.Computer.FileSystem.CopyDirectory(Folderstr, ProfilePath & "\" & newdirguid)
                End If

                _parent.UserSettingManager.SaveSettings(cloneobj, ProfilePath & "\" & newdirguid & "\" & UserSettingsFilename)
                itm.Tag = newdirguid
            Else
                Dim newclass As New UserSettings
                newclass.TemplateName = "New Workspace"
                newclass.LastSettingName = "New Workspace"
                newclass.SettingName = "New Workspace"

                itm.Text = "New Workspace"
                itm.SubItems.Add("")
                itm.Checked = False

                Dim newdirguid As String
                newdirguid = Guid.NewGuid.ToString

                My.Computer.FileSystem.CreateDirectory(ProfilePath & "\" & newdirguid)
                _parent.UserSettingManager.SaveSettings(newclass, ProfilePath & "\" & newdirguid & "\" & UserSettingsFilename)

                itm.Tag = newdirguid
            End If

            ListView1.Items.Add(itm)
            ListView1.Items(ListView1.Items.Count - 1).Selected = True
        Catch ex As Exception
        End Try
    End Sub

    Public Function RemoveWorkspaceFromGUI(ByVal index As Integer) As Boolean
        Try
            Dim Folderstr As String
            Folderstr = ProfilePath & "\" & ListView1.Items(index).Tag
            My.Computer.FileSystem.DeleteDirectory(Folderstr, FileIO.DeleteDirectoryOption.DeleteAllContents)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SaveWorkspaceItemSettings(ByVal index As Integer, ByVal TemplateName As String, ByVal TemplateDescription As String, ByVal Autostart As Boolean) As Boolean
        Try
            If Not index = 0 Then
                Dim Folderstr As String
                Folderstr = ProfilePath & "\" & ListView1.Items(index).Tag & "\" & UserSettingsFilename

                Dim settingsobj As UserSettings
                settingsobj = PropertyGrid1.SelectedObject
                settingsobj.TemplateDescription = TemplateDescription
                settingsobj.TemplateName = TemplateName
                settingsobj.Autostart = Autostart

                _parent.UserSettingManager.SaveSettings(settingsobj, Folderstr)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub LoadCustomWorkspacesToGUI()
        Try
            ListView1.BeginUpdate()
            ListView1.Items.Clear()

            Dim dircoll As New IO.DirectoryInfo(ProfilePath)

            Dim dircollarray As IO.DirectoryInfo()
            dircollarray = dircoll.GetDirectories

            If Not dircollarray.Count = 0 Then
                For index = 0 To dircollarray.Count - 1
                    Dim settingspath As String
                    settingspath = dircollarray(index).FullName & "\" & UserSettingsFilename
                    Dim settingsobj As UserSettings
                    settingsobj = _parent.UserSettingManager.LoadSettings(settingspath)

                    Dim itm As New ListViewItem
                    itm.Text = settingsobj.TemplateName
                    itm.SubItems.Add(settingsobj.TemplateDescription)
                    itm.Checked = settingsobj.Autostart
                    itm.Tag = dircollarray(index).Name
                    ListView1.Items.Add(itm)
                Next
            End If

            ListView1.EndUpdate()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            If Not IsNothing(ListView1.SelectedItems) And Not ListView1.SelectedItems.Count = 0 Then
                If ListView1.SelectedItems(0).Index = 0 Then
                    ToolStripButton4.Enabled = False
                    ToolStripButton3.Enabled = False
                    ToolStripButton6.Enabled = False
                    GroupBox1.Enabled = False
                    GroupBox2.Enabled = False
                    CheckBox1.Enabled = False
                    CheckBox1.Checked = True
                    TextBox1.Text = _parent.UserSettings.TemplateName
                    TextBox2.Text = _parent.UserSettings.TemplateDescription
                    PropertyGrid1.SelectedObject = _parent.UserSettings
                Else
                    ToolStripButton4.Enabled = True
                    ToolStripButton3.Enabled = True
                    ToolStripButton6.Enabled = True
                    GroupBox1.Enabled = True
                    GroupBox2.Enabled = True
                    CheckBox1.Enabled = True

                    Dim Folderstr As String
                    Folderstr = ProfilePath & "\" & ListView1.SelectedItems(0).Tag
                    Dim settingsobj As UserSettings
                    settingsobj = _parent.UserSettingManager.LoadSettings(Folderstr & "\" & UserSettingsFilename)

                    CheckBox1.Checked = settingsobj.Autostart
                    TextBox1.Text = settingsobj.TemplateName
                    TextBox2.Text = settingsobj.TemplateDescription
                    PropertyGrid1.SelectedObject = settingsobj
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Try
            If Not IsNothing(ListView1.SelectedItems) And Not ListView1.SelectedItems.Count = 0 Then
                ListView1.SelectedItems(0).Checked = CheckBox1.Checked
                SaveWorkspaceItemSettings(ListView1.SelectedItems(0).Index, ListView1.SelectedItems(0).Text, ListView1.SelectedItems(0).SubItems(1).Text, ListView1.SelectedItems(0).Checked)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        AddNewWorkspaceToGUI(False)
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        RemoveWorkspaceFromGUI(ListView1.SelectedItems(0).Index)
        LoadCustomWorkspacesToGUI()
        ListView1.Items(0).Selected = True
    End Sub

    Private Sub ListView1_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles ListView1.ItemChecked
        Try
            If Not IsNothing(ListView1.SelectedItems) And Not ListView1.SelectedItems.Count = 0 Then
                CheckBox1.Checked = ListView1.SelectedItems(0).Checked
                SaveWorkspaceItemSettings(ListView1.SelectedItems(0).Index, ListView1.SelectedItems(0).Text, ListView1.SelectedItems(0).SubItems(1).Text, ListView1.SelectedItems(0).Checked)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        AddNewWorkspaceToGUI(True)
    End Sub

    Public Sub SaveGUIChanges()
        Try
            If Not IsNothing(ListView1.SelectedItems) And Not ListView1.SelectedItems.Count = 0 Then
                ListView1.SelectedItems(0).Text = TextBox1.Text
                ListView1.SelectedItems(0).SubItems(1).Text = TextBox2.Text
                SaveWorkspaceItemSettings(ListView1.SelectedItems(0).Index, ListView1.SelectedItems(0).Text, ListView1.SelectedItems(0).SubItems(1).Text, ListView1.SelectedItems(0).Checked)

                If ListView1.SelectedItems(0).Index = 0 Then
                    _parent.UserSettings = PropertyGrid1.SelectedObject
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        SaveGUIChanges()
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        If Not ListView1.SelectedItems.Count = 0 Then
            SaveGUIChanges()
            _parent.SpawnNewProcessInstance(ListView1.SelectedItems(0).Text)
        End If
    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        If Not ListView1.SelectedItems.Count = 0 Then
            SaveGUIChanges()
            _parent.OpenNewWindow(False, ListView1.SelectedItems(0).Text)
        End If
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        ExportWorkspaceFile.ShowDialog()
        Dim result As Boolean = False
        If Not ExportWorkspaceFile.FileName = "" Then
            _parent.SaveSettings()
            result = _parent.WindowManagerHandler.ExportWorkspace(ExportWorkspaceFile.FileName, ListView1.SelectedItems(0).Text)

            If result Then
                MsgBox("Export successful!")
            Else
                MsgBox("Export failed!")
            End If
        End If
    End Sub

    Private Sub ToolStripButton8_ButtonClick(sender As Object, e As EventArgs) Handles ToolStripButton8.ButtonClick
        ImportWorkspaceFile.ShowDialog()
        Dim result As Boolean = False
        If Not ImportWorkspaceFile.FileName = "" Then
            result = _parent.WindowManagerHandler.ImportWorkspace(ImportWorkspaceFile.FileName, ListView1.SelectedItems(0).Text)

            If result Then
                MsgBox("Import successful.")
            Else
                MsgBox("Import failed!")
            End If
        End If
    End Sub

    Private Sub ImportToNewWorkplaceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportToNewWorkplaceToolStripMenuItem.Click
        ImportWorkspaceFile.ShowDialog()
        Dim result As Boolean = False
        If Not ImportWorkspaceFile.FileName = "" Then
            AddNewWorkspaceToGUI(False)
            Dim newsetting As String
            newsetting = ListView1.Items(ListView1.Items.Count - 1).Text
            result = _parent.WindowManagerHandler.ImportWorkspace(ImportWorkspaceFile.FileName, newsetting)

            If result Then
                MsgBox("Import successful.")
            Else
                MsgBox("Import failed!")
            End If
        End If
    End Sub

    Private Sub WorkspaceManager_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        _parent.LoadGUIStateFromUserSettings()
    End Sub

    Private Sub PropertyGrid1_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles PropertyGrid1.PropertyValueChanged
        SaveWorkspaceItemSettings(ListView1.SelectedItems(0).Index, ListView1.SelectedItems(0).Text, ListView1.SelectedItems(0).SubItems(1).Text, ListView1.SelectedItems(0).Checked)
    End Sub
End Class