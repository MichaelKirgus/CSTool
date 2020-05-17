'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.CodeDom.Compiler
Imports System.Runtime.InteropServices
Imports CSWorkspaceTemplateManager

Public Class WorkspaceTemplateForm
    Private Delegate Sub AddListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem, ByVal EnsureVisible As Boolean)
    Private Delegate Sub RemoveListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem)
    Private Delegate Sub BeginUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub EndUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub ClearListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Function GetItemFromIndexDelegate(ByVal ListViewCtl As ListView, ByVal Index As Integer) As ListViewItem

    Public WorkspaceTemplateHandler As New WorkspaceTemplateManager
    Public _parent As MainForm
    Public ModeSwitch As LoadMode = LoadMode.FromUserProfile
    Public WorkspaceProfileIndex As Integer = 0

    Public Enum LoadMode As Integer
        FromUserProfile = 0
        FromGlobalWorkspaceNonPersistent = 1
        AddFromGlobalWorkspace = 2
        AddNewEmptyWorkspaceToProfile = 3
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
            Dim lvi As New ListViewItem(Text)
            ListViewCtl.Items.Add(ListViewItemCtl)
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
        SetUXThemeForAllListViews()
        BeginUpdateListView(ListView1)

        LoadProfileTemplates.RunWorkerAsync()
        LoadGlobalTemplates.RunWorkerAsync()
        LoadWait.RunWorkerAsync()
    End Sub

    Private Sub LoadProfileTemplates_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadProfileTemplates.DoWork
        Try
            If Not _parent.UserSettings.UserTemplates.Count = 0 Then
                For index = 0 To _parent.UserSettings.UserTemplates.Count - 1
                    Dim tempitm As New ListViewItem
                    tempitm.Text = _parent.UserSettings.UserTemplates(index).TemplateName
                    tempitm.SubItems.Add(_parent.UserSettings.UserTemplates(index).TemplateDescription)
                    tempitm.SubItems.Add(index)
                    tempitm.Tag = 0
                    tempitm.ImageIndex = 0
                    tempitm.Group = ListView1.Groups(0)

                    AddListViewItem(ListView1, tempitm)
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
                    globitm.Group = ListView1.Groups(2)
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
                    PictureBox1.Image = ImageList1.Images(0)
                    WorkspaceNameLbl.Text = curritem.Text
                    TextBox1.Text = curritem.SubItems(1).Text
                    WorkspaceProfileIndex = curritem.SubItems(2).Text
                    LoadImage2.Visible = False
                    If curritem.Tag = 0 Then
                        ComboBox1.Enabled = False
                        ModeSwitch = LoadMode.FromUserProfile
                    Else
                        ComboBox1.Enabled = True
                        ComboBox1.SelectedIndex = 1
                        ModeSwitch = LoadMode.AddNewEmptyWorkspaceToProfile
                        PictureBox1.Image = ImageList1.Images(1)
                    End If
                End If
                If curritem.Group.Header = ListView1.Groups(1).Header Then
                    Dim templateobj As WorkspaceTemplateClass
                    templateobj = curritem.Tag

                    ComboBox1.Enabled = True
                    ComboBox1.SelectedIndex = 0
                    ModeSwitch = LoadMode.FromGlobalWorkspaceNonPersistent

                    WorkspaceNameLbl.Text = curritem.Text
                    TextBox1.Text = templateobj.TemplateDescription

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
End Class