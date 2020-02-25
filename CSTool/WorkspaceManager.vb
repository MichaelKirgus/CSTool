'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolUserSettingsLib
Imports CSToolUserSettingsManager

Public Class WorkspaceManager
    Public _parent As MainForm

    Private Sub WorkspaceManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddDefaultWorkspaceToGUI()
        LoadCustomWorkspacesToGUI()
        ListView1.Items(0).Selected = True
    End Sub

    Public Sub AddDefaultWorkspaceToGUI()
        Dim itm As New ListViewItem
        itm.Text = _parent.UserSettings.TemplateName
        itm.SubItems.Add(_parent.UserSettings.TemplateDescription)
        itm.Checked = True
        ListView1.Items.Add(itm)
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

                If ListView1.SelectedItems(0).Index = 0 Then
                    Dim Folderstr As String
                    Folderstr = _parent.CurrentUserProfilePath & "\Default"
                    My.Computer.FileSystem.CopyDirectory(Folderstr, _parent.CurrentUserProfilePath & "\" & itm.Text)
                Else
                    Dim Folderstr As String
                    Folderstr = _parent.CurrentUserProfilePath & "\" & _parent.UserSettings.UserTemplates(ListView1.SelectedItems(0).Index - 1).TemplateName
                    My.Computer.FileSystem.CopyDirectory(Folderstr, _parent.CurrentUserProfilePath & "\" & itm.Text)
                End If

                _parent.UserSettings.UserTemplates.Add(cloneobj)
            Else
                Dim newclass As New UserSettings
                newclass.TemplateName = "New Workspace"

                itm.Text = "New Workspace"
                itm.SubItems.Add("")
                itm.Checked = False

                _parent.UserSettings.UserTemplates.Add(newclass)
            End If

            ListView1.Items.Add(itm)
            ListView1.Items(ListView1.Items.Count - 1).Selected = True
        Catch ex As Exception
        End Try
    End Sub

    Public Function RemoveWorkspaceFromGUI(ByVal index As Integer) As Boolean
        Try
            Dim Folderstr As String
            Folderstr = _parent.CurrentUserProfilePath & "\" & _parent.UserSettings.UserTemplates(index).TemplateName
            My.Computer.FileSystem.DeleteDirectory(Folderstr, FileIO.DeleteDirectoryOption.DeleteAllContents)
            _parent.UserSettings.UserTemplates.RemoveAt(index)
            ListView1.Items.RemoveAt(ListView1.SelectedItems(0).Index)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function SaveWorkspaceItemSettings(ByVal index As Integer, ByVal TemplateName As String, ByVal TemplateDescription As String, ByVal Autostart As Boolean) As Boolean
        Try
            If Not index = 0 Then
                If Not TemplateName = _parent.UserSettings.UserTemplates(index - 1).TemplateName Then
                    Dim Folderstr As String
                    Folderstr = _parent.CurrentUserProfilePath & "\" & _parent.UserSettings.UserTemplates(index - 1).TemplateName
                    If Not IO.Directory.Exists(Folderstr) Then
                        My.Computer.FileSystem.RenameDirectory(Folderstr, TemplateName)
                    End If
                End If

                _parent.UserSettings.UserTemplates(index - 1).TemplateName = TemplateName
                _parent.UserSettings.UserTemplates(index - 1).TemplateDescription = TemplateDescription
                _parent.UserSettings.UserTemplates(index - 1).Autostart = Autostart
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub LoadCustomWorkspacesToGUI()
        Try
            ListView1.BeginUpdate()

            If Not _parent.UserSettings.UserTemplates.Count = 0 Then
                For index = 0 To _parent.UserSettings.UserTemplates.Count - 1
                    Dim itm As New ListViewItem
                    itm.Text = _parent.UserSettings.UserTemplates(index).TemplateName
                    itm.SubItems.Add(_parent.UserSettings.UserTemplates(index).TemplateDescription)
                    itm.Checked = _parent.UserSettings.UserTemplates(index).Autostart
                    ListView1.Items.Add(itm)
                Next
            End If

            ListView1.EndUpdate()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            If Not IsNothing(ListView1.SelectedItems) Then
                If ListView1.SelectedItems(0).Index = 0 Then
                    ToolStripButton4.Enabled = False
                    GroupBox1.Enabled = False
                    GroupBox2.Enabled = False
                    CheckBox1.Enabled = False
                    CheckBox1.Checked = True
                    TextBox1.Text = _parent.UserSettings.TemplateName
                    TextBox2.Text = _parent.UserSettings.TemplateDescription
                    PropertyGrid1.SelectedObject = _parent.UserSettings
                Else
                    ToolStripButton4.Enabled = True
                    GroupBox1.Enabled = True
                    GroupBox2.Enabled = True
                    CheckBox1.Enabled = True
                    CheckBox1.Checked = _parent.UserSettings.UserTemplates(ListView1.SelectedItems(0).Index - 1).Autostart
                    TextBox1.Text = _parent.UserSettings.UserTemplates(ListView1.SelectedItems(0).Index - 1).TemplateName
                    TextBox2.Text = _parent.UserSettings.UserTemplates(ListView1.SelectedItems(0).Index - 1).TemplateDescription
                    PropertyGrid1.SelectedObject = _parent.UserSettings.UserTemplates(ListView1.SelectedItems(0).Index - 1)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
            ListView1.SelectedItems(0).Text = TextBox1.Text
            SaveWorkspaceItemSettings(ListView1.SelectedItems(0).Index, ListView1.SelectedItems(0).Text, ListView1.SelectedItems(0).SubItems(1).Text, ListView1.SelectedItems(0).Checked)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Try
            ListView1.SelectedItems(0).SubItems(1).Text = TextBox2.Text
            SaveWorkspaceItemSettings(ListView1.SelectedItems(0).Index, ListView1.SelectedItems(0).Text, ListView1.SelectedItems(0).SubItems(1).Text, ListView1.SelectedItems(0).Checked)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Try
            ListView1.SelectedItems(0).Checked = CheckBox1.Checked
            SaveWorkspaceItemSettings(ListView1.SelectedItems(0).Index, ListView1.SelectedItems(0).Text, ListView1.SelectedItems(0).SubItems(1).Text, ListView1.SelectedItems(0).Checked)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        AddNewWorkspaceToGUI(False)
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        RemoveWorkspaceFromGUI(ListView1.SelectedItems(0).Index - 1)
    End Sub

    Private Sub ListView1_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles ListView1.ItemChecked
        Try
            CheckBox1.Checked = ListView1.SelectedItems(0).Checked
            SaveWorkspaceItemSettings(ListView1.SelectedItems(0).Index, ListView1.SelectedItems(0).Text, ListView1.SelectedItems(0).SubItems(1).Text, ListView1.SelectedItems(0).Checked)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        AddNewWorkspaceToGUI(True)
    End Sub
End Class