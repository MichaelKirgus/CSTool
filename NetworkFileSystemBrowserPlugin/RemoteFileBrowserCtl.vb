'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports System.Net
Imports System.Windows.Forms

Public Class RemoteFileBrowserCtl
    Public ShareURI As String = ""
    Public ShareUsername As String = ""
    Public SharePassword As String = ""
    Public ShareDomain As String = ""
    Public ShareAuthentification As String = "Basic"
    Public LoadFileInfo As Boolean = True
    Public LoadDirInfo As Boolean = True
    Public AutoSizeColumns As Boolean = True

    Public _parentCtl As ClientGUI = Nothing
    Private CurrentDirInfo As IO.DirectoryInfo = Nothing

    Dim NetworkCredential As NetworkCredential
    Dim Netcache As CredentialCache
    Private Delegate Sub AddListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem, ByVal EnsureVisible As Boolean)
    Private Delegate Sub AddListViewColumnDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ColumnHeader)
    Private Delegate Sub RemoveListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem)
    Private Delegate Sub BeginUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub EndUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub ClearListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Function GetItemFromIndexDelegate(ByVal ListViewCtl As ListView, ByVal Index As Integer) As ListViewItem
    Private Delegate Sub SetGroupOfListViewItemDelegate(ByVal ListViewCtl As ListView, ByVal GroupListItem As ListViewItem, ByVal GroupItem As ListViewGroup)
    Private Delegate Sub AddGroupToListViewDelegate(ByVal ListViewCtl As ListView, ByVal GroupItem As ListViewGroup)

    Public Shared Function GetFileType(ByVal Extention As String) As String
        Try
            Return My.Computer.Registry.GetValue("HKEY_CLASSES_ROOT\" & My.Computer.Registry.GetValue("HKEY_CLASSES_ROOT\" & Extention, "", Extention).ToString, "", Extention).ToString
        Catch ex As Exception
            Return Extention
        End Try
    End Function

    Private Sub AddGroupToListView(ByVal ListViewCtl As ListView, ByVal GroupItem As ListViewGroup)
        If ListViewCtl.InvokeRequired Then
            Dim AddGroupToListViewObj As New AddGroupToListViewDelegate(AddressOf AddGroupToListView)
            ListViewCtl.Invoke(AddGroupToListViewObj, New Object() {ListViewCtl, GroupItem})
        Else
            ListViewCtl.Groups.Add(GroupItem)
        End If
    End Sub

    Private Sub SetGroupOfListViewItem(ByVal ListViewCtl As ListView, ByVal GroupListItem As ListViewItem, ByVal GroupItem As ListViewGroup)
        If ListViewCtl.InvokeRequired Then
            Dim SetGroupOfListViewItemObj As New SetGroupOfListViewItemDelegate(AddressOf SetGroupOfListViewItem)
            ListViewCtl.Invoke(SetGroupOfListViewItemObj, New Object() {ListViewCtl, GroupListItem, GroupItem})
        Else
            ListViewCtl.Items(ListViewCtl.Items.IndexOf(GroupListItem)).Group = GroupItem
        End If
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

    Private Sub AddListViewColumn(ByVal ListViewCtl As ListView, ListViewColumnCtl As ColumnHeader)
        If ListViewCtl.InvokeRequired Then
            Dim delAddListViewColumn As New AddListViewColumnDelegate(AddressOf AddListViewColumn)
            ListViewCtl.Invoke(delAddListViewColumn, New Object() {ListViewCtl, ListViewColumnCtl})
        Else
            ListViewCtl.Columns.Add(ListViewColumnCtl)
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

    Public Function OpenNetworkConnection() As Boolean
        Try
            NetworkCredential = New NetworkCredential(_parentCtl.EnvManager.ResolveEnvironmentVariables(_parentCtl._ParentInstance.EnvironmentRuntimeVariables, ShareUsername),
                                                      _parentCtl.EnvManager.ResolveEnvironmentVariables(_parentCtl._ParentInstance.EnvironmentRuntimeVariables, SharePassword),
                                                      _parentCtl.EnvManager.ResolveEnvironmentVariables(_parentCtl._ParentInstance.EnvironmentRuntimeVariables, ShareDomain))
            Netcache = New CredentialCache()
            Netcache.Add(New Uri(_parentCtl.EnvManager.ResolveEnvironmentVariables(_parentCtl._ParentInstance.EnvironmentRuntimeVariables, ShareURI)),
                        _parentCtl.EnvManager.ResolveEnvironmentVariables(_parentCtl._ParentInstance.EnvironmentRuntimeVariables, ShareAuthentification),
                         NetworkCredential)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub LoadFolders()
        If OpenNetworkConnection() Then
            If Not LoadDirsAndFilesWorker.IsBusy Then
                LoadImage.Visible = True
                ConnectionClosed.Visible = False
                LoadDirsAndFilesWorker.RunWorkerAsync(ToolStripComboBox1.Text)
            End If
        Else
            LoadImage.Visible = False
            ConnectionClosed.Visible = True
        End If
    End Sub

    Private Sub LoadDirsAndFilesWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadDirsAndFilesWorker.DoWork
        Try
            BeginUpdateListView(ListView1)
            ClearListView(ListView1)
            If IO.Directory.Exists(e.Argument) Then
                CurrentDirInfo = New IO.DirectoryInfo(e.Argument)
                Dim dircoll As String()
                dircoll = IO.Directory.GetDirectories(e.Argument)
                If Not dircoll.Count = 0 Then
                    For index = 0 To dircoll.Count - 1
                        If LoadDirInfo Then
                            Try
                                Dim dirinf As New IO.DirectoryInfo(dircoll(index))
                                Dim itm As New ListViewItem
                                itm.Text = dirinf.Name
                                itm.SubItems.Add(dirinf.LastWriteTime)
                                itm.StateImageIndex = 0
                                itm.ImageIndex = 0
                                itm.Tag = dircoll(index)
                                AddListViewItem(ListView1, itm, False)
                            Catch ex As Exception
                                Dim itm As New ListViewItem
                                itm.Text = dircoll(index).Split("\")(dircoll(index).Split("\").Count - 1)
                                itm.StateImageIndex = 0
                                itm.ImageIndex = 0
                                itm.Tag = dircoll(index)
                                AddListViewItem(ListView1, itm, False)
                            End Try
                        Else
                            Dim itm As New ListViewItem
                            itm.Text = dircoll(index).Split("\")(dircoll(index).Split("\").Count - 1)
                            itm.StateImageIndex = 0
                            itm.ImageIndex = 0
                            itm.Tag = dircoll(index)
                            AddListViewItem(ListView1, itm, False)
                        End If
                    Next
                End If
                Dim filescoll As String()
                filescoll = IO.Directory.GetFiles(e.Argument)
                If Not filescoll.Count = 0 Then
                    For index = 0 To filescoll.Count - 1
                        If LoadFileInfo Then
                            Try
                                Dim fileinf As New IO.FileInfo(filescoll(index))
                                Dim itm As New ListViewItem
                                itm.Text = fileinf.Name
                                itm.SubItems.Add(fileinf.LastWriteTime)
                                itm.SubItems.Add(GetFileType(fileinf.Extension))
                                itm.SubItems.Add(GetRightSizeFormat(fileinf.Length))
                                itm.StateImageIndex = 1
                                itm.ImageIndex = 1
                                itm.Tag = filescoll(index)
                                AddListViewItem(ListView1, itm, False)
                            Catch ex As Exception
                                Dim itm As New ListViewItem
                                itm.Text = filescoll(index).Split("\")(filescoll(index).Split("\").Count - 1)
                                itm.StateImageIndex = 1
                                itm.ImageIndex = 1
                                itm.Tag = filescoll(index)
                                AddListViewItem(ListView1, itm, False)
                            End Try
                        Else
                            Dim itm As New ListViewItem
                            itm.Text = filescoll(index).Split("\")(filescoll(index).Split("\").Count - 1)
                            itm.StateImageIndex = 1
                            itm.ImageIndex = 1
                            itm.Tag = filescoll(index)
                            AddListViewItem(ListView1, itm, False)
                        End If
                    Next
                End If
            Else
                e.Result = False
            End If

            EndUpdateListView(ListView1)
            e.Result = True
        Catch ex As Exception
            EndUpdateListView(ListView1)
            e.Result = False
        End Try
    End Sub

    Public Function GetRightSizeFormat(ByVal size_bytes As Int64, Optional ByVal DecimalPlaces As Integer = 2) As String
        'Diese Funktion generiert aus einer Anzahl von Bytes eine lesbare Größenangabe in Bytes/KB/MB/GB/TB
        Try
            Dim newstr As String = ""
            Dim newdec As Double = 0
            Dim by As Decimal = size_bytes
            If by > 1024 Then
                Dim kb As Double = by / 1024
                If kb > 1024 Then
                    Dim mb As Double = kb / 1024
                    If mb > 1024 Then
                        Dim gb As Double = mb / 1024
                        If gb > 1024 Then
                            Dim tb As Double = gb / 1024
                            newdec = Math.Round(tb, DecimalPlaces)
                            newstr += newdec & " TB"
                        Else
                            newdec = Math.Round(gb, DecimalPlaces)
                            newstr += Math.Round(gb, DecimalPlaces).ToString & " GB"
                        End If
                    Else
                        newdec = Math.Round(mb, DecimalPlaces)
                        newstr += Math.Round(mb, DecimalPlaces).ToString & " MB"
                    End If
                Else
                    newdec = Math.Round(kb, DecimalPlaces)
                    newstr += Math.Round(kb, DecimalPlaces).ToString & " KB"
                End If
            Else
                newdec = Math.Round(by, DecimalPlaces)
                newstr += Math.Round(by, DecimalPlaces).ToString & " Bytes"
            End If

            Return newstr
        Catch ex As Exception
            Return "0 KB"
        End Try
    End Function

    Private Sub LoadDirsAndFilesWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadDirsAndFilesWorker.RunWorkerCompleted
        If e.Result = True Then
            LoadImage.Visible = False
            ConnectionClosed.Visible = False
            ToolStripButton4.Enabled = True
            ToolStripComboBox1.Enabled = True

            If Not IsNothing(CurrentDirInfo.Parent) Then
                If CurrentDirInfo.Parent.FullName = CurrentDirInfo.FullName Then
                    ToolStripButton7.Enabled = False
                Else
                    ToolStripButton7.Enabled = True
                End If
            Else
                ToolStripButton7.Enabled = False
            End If

            If AutoSizeColumns Then
                ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            End If
        Else
            LoadImage.Visible = False
            ConnectionClosed.Visible = True
            ToolStripButton4.Enabled = False
            ToolStripButton7.Enabled = False
            ToolStripComboBox1.Enabled = True
        End If
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        Try
            If Not ListView1.SelectedItems.Count = 0 Then
                If IO.File.Exists(ListView1.SelectedItems(0).Tag) Then
                    Dim fileexec As New Process
                    fileexec.StartInfo.FileName = ListView1.SelectedItems(0).Tag
                    fileexec.Start()
                Else
                    ToolStripComboBox1.Text = ListView1.SelectedItems(0).Tag
                    ShareURI = ListView1.SelectedItems(0).Tag
                    LoadFolders()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        ToolStripComboBox1.Text = CurrentDirInfo.Parent.FullName
        ShareURI = CurrentDirInfo.Parent.FullName
        LoadFolders()
    End Sub

    Private Sub ToolStrip1_SizeChanged(sender As Object, e As EventArgs) Handles ToolStrip1.SizeChanged
        Try
            Dim newsize As New Size(Me.Width - 70, ToolStripComboBox1.Size.Height)
            ToolStripComboBox1.Size = newsize
            ToolStrip1.Hide()
            ToolStrip1.Show()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripComboBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ToolStripComboBox1.KeyDown
        If e.KeyCode = 13 Then
            If Not ToolStripComboBox1.Text = "" Then
                ShareURI = ToolStripComboBox1.Text
                LoadFolders()
                e.Handled = True
                e.SuppressKeyPress = True
            End If
        End If
    End Sub
End Class
