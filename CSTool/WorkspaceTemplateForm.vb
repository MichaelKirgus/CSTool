﻿'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.CodeDom.Compiler
Imports System.Runtime.InteropServices
Imports CSToolUserSettingsLib
Imports CSWorkspaceTemplateManager

Public Class WorkspaceTemplateForm
    Private Delegate Sub AddListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem, ByVal EnsureVisible As Boolean)
    Private Delegate Sub InsertListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem, ByVal InsertIndex As Integer, ByVal EnsureVisible As Boolean)
    Private Delegate Sub RemoveListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem)
    Private Delegate Sub BeginUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub EndUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub ClearListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Function GetItemFromIndexDelegate(ByVal ListViewCtl As ListView, ByVal Index As Integer) As ListViewItem

    Public WorkspaceTemplateHandler As New WorkspaceTemplateManager
    Public _parent As MainForm
    Public ModeSwitch As LoadMode = LoadMode.FromUserProfile
    Public ParentProfilePath As String = ""
    Public WorkspaceSettingsFile As String = ""
    Public WorkspaceProfileIndex As Integer = 0
    Public WasValueChanged As Boolean = False

    Public Enum LoadMode As Integer
        FromUserProfile = 0
        FromGlobalWorkspaceNonPersistent = 1
        AddFromGlobalWorkspace = 2
        AddNewEmptyWorkspaceToProfile = 3
        AddInitialWorkspaceToProfile = 4
    End Enum


    <DllImport("uxtheme", CharSet:=CharSet.Unicode)>
    Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal textSubAppName As String, ByVal textSubIdList As String) As Integer
    End Function
    Public Sub SetUXThemeForAllListViews()
        Try
            SetWindowTheme(ListView1.Handle, "explorer", Nothing)
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetItemFromIndex(ByVal ListViewCtl As ListView, ByVal Index As Integer) As ListViewItem
        If ListViewCtl.InvokeRequired Then
            Dim GetItemFromIndexObj As New GetItemFromIndexDelegate(AddressOf GetItemFromIndex)
            ListViewCtl.Invoke(GetItemFromIndexObj, New Object() {ListViewCtl, Index})
            Return New ListViewItem
        Else
            Return ListViewCtl.Items(Index)
        End If
    End Function

    Private Sub ClearListView(ByVal ListViewCtl As ListView)
        If ListViewCtl.InvokeRequired Then
            Dim ClearListViewObj As New BeginUpdateListViewDelegate(AddressOf ClearListView)
            ListViewCtl.Invoke(ClearListViewObj, New Object() {ListViewCtl})
        Else
            ListViewCtl.Items.Clear()
        End If
    End Sub

    Private Sub BeginUpdateListView(ByVal ListViewCtl As ListView)
        Try
            If ListViewCtl.InvokeRequired Then
                Dim BeginUpdateListViewObj As New BeginUpdateListViewDelegate(AddressOf BeginUpdateListView)
                ListViewCtl.Invoke(BeginUpdateListViewObj, New Object() {ListViewCtl})
            Else
                ListViewCtl.BeginUpdate()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub EndUpdateListView(ByVal ListViewCtl As ListView)
        Try
            If ListViewCtl.InvokeRequired Then
                Dim EndUpdateListViewObj As New EndUpdateListViewDelegate(AddressOf EndUpdateListView)
                ListViewCtl.Invoke(EndUpdateListViewObj, New Object() {ListViewCtl})
            Else
                ListViewCtl.EndUpdate()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub AddListViewItem(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem, Optional ByVal EnsureVisible As Boolean = False)
        If ListViewCtl.InvokeRequired Then
            Dim delAddListViewItem As New AddListViewItemDelegate(AddressOf AddListViewItem)
            ListViewCtl.Invoke(delAddListViewItem, New Object() {ListViewCtl, ListViewItemCtl, EnsureVisible})
        Else
            ListViewCtl.Items.Add(ListViewItemCtl)
            If EnsureVisible Then
                ListViewCtl.EnsureVisible(ListViewCtl.Items.Count - 1)
            End If
        End If
    End Sub

    Private Sub InsertListViewItem(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem, ByVal InsertIndex As Integer, Optional ByVal EnsureVisible As Boolean = False)
        If ListViewCtl.InvokeRequired Then
            Dim delInsertistViewItem As New InsertListViewItemDelegate(AddressOf InsertListViewItem)
            ListViewCtl.Invoke(delInsertistViewItem, New Object() {ListViewCtl, ListViewItemCtl, InsertIndex, EnsureVisible})
        Else
            ListViewCtl.Items.Insert(InsertIndex, ListViewItemCtl)

            If EnsureVisible Then
                ListViewCtl.EnsureVisible(ListViewCtl.Items.Count - 1)
            End If
        End If
    End Sub

    Private Sub RemoveListViewItem(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem)
        If ListViewCtl.InvokeRequired Then
            Dim delRemoveListViewItem As New RemoveListViewItemDelegate(AddressOf RemoveListViewItem)
            ListViewCtl.Invoke(delRemoveListViewItem, New Object() {ListViewCtl, ListViewItemCtl})
        Else
            ListViewCtl.Items.Remove(ListViewItemCtl)
        End If
    End Sub

    Private Sub WorkspaceTemplateForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckBox1.Checked = _parent.UserSettings.ShowWorkplaceTemplateForm

        Dim dirinf As New IO.DirectoryInfo(_parent.CurrentUserProfilePath)
        ParentProfilePath = dirinf.Parent.FullName

        SetUXThemeForAllListViews()
        BeginUpdateListView(ListView1)

        LoadProfileTemplates.RunWorkerAsync()
        LoadGlobalTemplates.RunWorkerAsync()
        LoadWait.RunWorkerAsync()
    End Sub

    Private Sub LoadProfileTemplates_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadProfileTemplates.DoWork
        Try
            If Not _parent.Workspaces.Count = 0 Then
                For index = 0 To _parent.Workspaces.Count - 1
                    Dim tempitm As New ListViewItem
                    tempitm.Text = _parent.Workspaces(index).TemplateName
                    tempitm.SubItems.Add(_parent.Workspaces(index).TemplateDescription)
                    tempitm.SubItems.Add(index)
                    tempitm.Tag = 0
                    tempitm.ImageIndex = 0
                    tempitm.Group = ListView1.Groups(0)

                    If _parent.Workspaces(index).SettingName = "Default" Then
                        InsertListViewItem(ListView1, tempitm, 0)
                    Else
                        AddListViewItem(ListView1, tempitm)
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadWait_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadWait.DoWork
        Do While LoadProfileTemplates.IsBusy And LoadGlobalTemplates.IsBusy
            Threading.Thread.Sleep(20)
            If e.Cancel Then
                Exit Do
            End If
        Loop
    End Sub

    Private Sub LoadWait_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadWait.RunWorkerCompleted
        EndUpdateListView(ListView1)
        LoadImage1.Visible = False

        ListView1.Items(0).Selected = True
        CheckBox1.Enabled = True
        CheckBox2.Enabled = True
    End Sub

    Private Sub LoadGlobalTemplates_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadGlobalTemplates.DoWork
        Try
            Dim globitems As List(Of WorkspaceTemplateClass)
            globitems = WorkspaceTemplateHandler.GetTemplates(_parent.ApplicationSettings.UserWorkspaceTemplatesDir)

            If Not globitems.Count = 0 Then
                For index = 0 To globitems.Count - 1
                    Dim globitm As New ListViewItem
                    globitm.Text = globitems(index).TemplateName
                    globitm.Tag = globitems(index)
                    globitm.ImageIndex = 2
                    globitm.Group = ListView1.Groups(1)

                    AddListViewItem(ListView1, globitm)
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            If Not ListView1.SelectedItems.Count = 0 Then
                Button1.Enabled = False

                Dim curritem As ListViewItem
                curritem = ListView1.SelectedItems(0)

                If curritem.Group.Header = ListView1.Groups(0).Header Then
                    PictureBox1.Image = ImageList1.Images(1)
                    WorkspaceNameLbl.Text = curritem.Text
                    TextBox1.Text = curritem.SubItems(1).Text

                    If _parent.UserSettings.LastSettingName = curritem.Text Then
                        CheckBox2.Checked = True
                    Else
                        CheckBox2.Checked = False
                    End If

                    WorkspaceProfileIndex = curritem.SubItems(2).Text
                    LoadImage2.Visible = False
                    Select Case curritem.Tag
                        Case 0
                            ComboBox1.Enabled = False
                            ModeSwitch = LoadMode.FromUserProfile
                        Case 1
                            ComboBox1.Enabled = True
                            ComboBox1.SelectedIndex = 1
                            ModeSwitch = LoadMode.AddNewEmptyWorkspaceToProfile
                            PictureBox1.Image = ImageList1.Images(0)
                        Case 2
                            ComboBox1.Enabled = True
                            ComboBox1.SelectedIndex = 1
                            ModeSwitch = LoadMode.AddInitialWorkspaceToProfile
                            PictureBox1.Image = ImageList1.Images(3)
                    End Select
                End If
                If curritem.Group.Header = ListView1.Groups(1).Header Then
                    Dim templateobj As WorkspaceTemplateClass
                    templateobj = curritem.Tag

                    ComboBox1.Enabled = True
                    ComboBox1.SelectedIndex = 0
                    ModeSwitch = LoadMode.FromGlobalWorkspaceNonPersistent
                    WorkspaceSettingsFile = templateobj.TemplateUserSettingsPath

                    WorkspaceNameLbl.Text = curritem.Text
                    TextBox1.Text = templateobj.TemplateDescription

                    If _parent.UserSettings.LastSettingName = curritem.Text Then
                        CheckBox2.Checked = True
                    Else
                        CheckBox2.Checked = False
                    End If

                    If Not LoadThumbnail.IsBusy Then
                        LoadImage2.Visible = True
                        LoadThumbnail.RunWorkerAsync(templateobj)
                    End If
                End If

                Button1.Enabled = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadThumbnail_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadThumbnail.DoWork
        Try
            Dim tempclass As WorkspaceTemplateClass
            tempclass = e.Argument

            Dim thumb As Image
            thumb = Image.FromFile(tempclass.TemplateImagePath)

            PictureBox1.Image = thumb
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadThumbnail_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadThumbnail.RunWorkerCompleted
        LoadImage2.Visible = False
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        Button1.PerformClick()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim newdirguid As String
        newdirguid = Guid.NewGuid.ToString

        Select Case ModeSwitch
            Case LoadMode.FromUserProfile
                If WorkspaceProfileIndex = 0 Then
                    _parent.OpenNewWindow(True, "Default", _parent.HostnameOrIPCtl.Text)
                Else
                    Dim settingid As String
                    settingid = _parent.WorkspaceSettingsHandler.GetWorkspaceFromTemplateName(_parent.Workspaces, ListView1.SelectedItems(0).Text).SettingName
                    _parent.OpenNewWindow(True, settingid, _parent.HostnameOrIPCtl.Text)
                End If
            Case LoadMode.AddNewEmptyWorkspaceToProfile
                Dim newclass As New UserSettings
                newclass.TemplateName = "New Workspace (empty)"
                newclass.TemplateDescription = "Added " & DateAndTime.Now.ToString
                newclass.LastSettingName = newdirguid
                newclass.SettingName = newdirguid

                If CheckBox2.Checked Then
                    newclass.Autostart = True
                End If

                My.Computer.FileSystem.CreateDirectory(ParentProfilePath & "\" & newdirguid)
                _parent.UserSettingManager.SaveSettings(newclass, ParentProfilePath & "\" & newdirguid & "\" & _parent.UserSettingManager.UserSettingsFile)

                _parent.SpawnNewProcessInstance(newclass.SettingName, False, _parent.HostnameOrIPCtl.Text)
                WasValueChanged = True
            Case LoadMode.AddInitialWorkspaceToProfile
                Dim newclass As UserSettings
                newclass = _parent.UserSettingManager.LoadSettings(_parent.ApplicationSettings.UserInitialTemplateDir & "\Default\" & _parent.UserSettingManager.UserSettingsFile)

                newclass.TemplateName = "New Workspace (from default)"
                newclass.TemplateDescription = "Cloned " & DateAndTime.Now.ToString
                newclass.LastSettingName = newdirguid
                newclass.SettingName = newdirguid

                If CheckBox2.Checked Then
                    newclass.Autostart = True
                End If

                My.Computer.FileSystem.CreateDirectory(ParentProfilePath & "\" & newdirguid)
                _parent.UserSettingManager.SaveSettings(newclass, ParentProfilePath & "\" & newdirguid & "\" & _parent.UserSettingManager.UserSettingsFile)

                My.Computer.FileSystem.CopyDirectory(_parent.ApplicationSettings.UserInitialTemplateDir & "\Default", ParentProfilePath & "\" & newdirguid, True)

                newclass.Autostart = CheckBox2.Checked
                _parent.UserSettingManager.SaveSettings(newclass, ParentProfilePath & "\" & newdirguid & "\" & _parent.UserSettingManager.UserSettingsFile)

                _parent.SpawnNewProcessInstance(newclass.SettingName, False, _parent.HostnameOrIPCtl.Text)
                WasValueChanged = True
            Case LoadMode.AddFromGlobalWorkspace
                Dim newclass As UserSettings
                newclass = _parent.UserSettingManager.LoadSettings(WorkspaceSettingsFile)

                newclass.TemplateDescription += " (Added " & DateAndTime.Now.ToString & ")"
                newclass.SettingName = newdirguid
                newclass.LastSettingName = newdirguid

                Dim dirinfobj As New IO.FileInfo(WorkspaceSettingsFile)

                My.Computer.FileSystem.CopyDirectory(dirinfobj.DirectoryName, ParentProfilePath & "\" & newdirguid)

                If CheckBox2.Checked Then
                    newclass.Autostart = True
                End If

                _parent.UserSettingManager.SaveSettings(newclass, ParentProfilePath & "\" & newdirguid & "\" & _parent.UserSettingManager.UserSettingsFile)

                _parent.SpawnNewProcessInstance(newdirguid, False, _parent.HostnameOrIPCtl.Text)
                WasValueChanged = True
            Case LoadMode.FromGlobalWorkspaceNonPersistent
                Dim newclass As UserSettings
                newclass = _parent.UserSettingManager.LoadSettings(WorkspaceSettingsFile)
                Dim newclassguid As String
                newclassguid = Guid.NewGuid.ToString
                newclass.SettingName = newclassguid
                newclass.TemplateName += " (non-persistent)"

                Dim dirinfobj As New IO.FileInfo(WorkspaceSettingsFile)

                My.Computer.FileSystem.CopyDirectory(dirinfobj.DirectoryName, ParentProfilePath & "\" & newclassguid)

                _parent.OpenNewWindow(False, newclassguid, _parent.HostnameOrIPCtl.Text)
                My.Computer.FileSystem.DeleteDirectory(ParentProfilePath & "\" & newclassguid, FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Select
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Enabled Then
            If ComboBox1.SelectedIndex = 0 Then
                ModeSwitch = LoadMode.FromGlobalWorkspaceNonPersistent
                CheckBox2.Checked = False
                CheckBox2.Enabled = False
            Else
                ModeSwitch = LoadMode.AddFromGlobalWorkspace
                CheckBox2.Enabled = True
            End If
        End If
    End Sub

    Private Sub WorkspaceTemplateForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        _parent.UserSettings.ShowWorkplaceTemplateForm = CheckBox1.Checked

        If WasValueChanged Then
            _parent.Workspaces = _parent.WorkspaceSettingsHandler.GetAllWorkspaces(ParentProfilePath)
        End If
    End Sub
End Class