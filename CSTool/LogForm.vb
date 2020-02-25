'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Runtime.InteropServices
Imports CSToolLogLib
Imports CSToolLogLib.LogSettings

Public Class LogForm
    Public _LogLibInstance As LogLib
    Public InitLinePos As Int64 = 0
    Public CurrentLoadedLinePos As Int64 = 0
    Public MaxVisibleLines As Int64 = 100
    Public RefreshInterval As Integer = 1000
    Public ShowInfoItems As Boolean = True
    Public ShowWarningItems As Boolean = True
    Public ShowErrorItems As Boolean = True

    Private Delegate Sub AddListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem, ByVal EnsureVisible As Boolean)
    Private Delegate Sub RemoveListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem)
    Private Delegate Sub BeginUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub EndUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub ClearListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Function GetItemFromIndexDelegate(ByVal ListViewCtl As ListView, ByVal Index As Integer) As ListViewItem

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

    Private Sub LogForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitLinePos = _LogLibInstance.LogCollection.LongCount
        ToolStripComboBox1.SelectedIndex = 0
        InitLogWaitWorker.RunWorkerAsync()
        SetUXThemeForAllListViews()
    End Sub

    Private Sub InitLogWaitWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles InitLogWaitWorker.DoWork
        Threading.Thread.Sleep(100)
    End Sub

    Private Sub InitLogWaitWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles InitLogWaitWorker.RunWorkerCompleted
        LogRefreshWorker.RunWorkerAsync()
    End Sub

    Public Sub AddLogItemToGUI(ByVal LogObj As LogEntry)
        Try
            Dim newitm As New ListViewItem
            newitm.Text = LogObj.DateAndTime.ToString("dd.MM.yyyy H:mm:ss:fff")
            newitm.SubItems.Add(LogObj.Type.ToString)
            newitm.SubItems.Add(LogObj.Level.ToString)
            newitm.SubItems.Add(LogObj.TargetHandler)
            newitm.SubItems.Add(LogObj.Message)
            newitm.Tag = LogObj
            Dim canceladd As Boolean = False
            Select Case LogObj.Type
                Case LogEntryTypeEnum.Info
                    If ShowInfoItems = False Then
                        canceladd = True
                        Exit Select
                    End If
                    newitm.ImageIndex = 0
                    newitm.StateImageIndex = 0
                Case LogEntryTypeEnum.Warning
                    If ShowWarningItems = False Then
                        canceladd = True
                        Exit Select
                    End If
                    newitm.ImageIndex = 1
                    newitm.StateImageIndex = 1
                Case Else
                    If ShowErrorItems = False Then
                        canceladd = True
                        Exit Select
                    End If
                    newitm.ImageIndex = 2
                    newitm.StateImageIndex = 2
            End Select

            If canceladd = False Then
                AddListViewItem(ListView1, newitm, True)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LogRefreshWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LogRefreshWorker.DoWork
        Try
            If Not IsNothing(_LogLibInstance.LogCollection) Then
                If Not _LogLibInstance.LogCollection.Count = 0 Then
                    Dim ToLoadLines As Integer = _LogLibInstance.LogCollection.Count - InitLinePos - CurrentLoadedLinePos
                    If Not ToLoadLines <= 0 Then
                        For index = 0 To ToLoadLines - 1
                            If MaxVisibleLines < ListView1.Items.Count Then
                                RemoveListViewItem(ListView1, GetItemFromIndex(ListView1, 0))
                            End If
                            If Not _LogLibInstance.LogCollection.Count <= (ToLoadLines + index) Then
                                If InitLinePos = 0 Then
                                    AddLogItemToGUI(_LogLibInstance.LogCollection(index + InitLinePos + CurrentLoadedLinePos))
                                Else
                                    AddLogItemToGUI(_LogLibInstance.LogCollection(index + InitLinePos))
                                End If
                            Else
                                If Not InitLinePos = 0 Then
                                    AddLogItemToGUI(_LogLibInstance.LogCollection(index + InitLinePos - 1))
                                Else
                                    AddLogItemToGUI(_LogLibInstance.LogCollection(index + InitLinePos))
                                End If
                            End If
                        Next
                        CurrentLoadedLinePos += ToLoadLines
                    End If
                End If
            End If

            e.Result = True
        Catch ex As Exception
            e.Result = False
        End Try
    End Sub

    Private Sub LogRefreshWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LogRefreshWorker.RunWorkerCompleted
        ToolStripButton1.Enabled = True
        If Not LogWaitWorker.IsBusy Then
            LogWaitWorker.RunWorkerAsync(RefreshInterval)
        End If
    End Sub

    Private Sub LogWaitWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LogWaitWorker.DoWork
        Threading.Thread.Sleep(e.Argument)
    End Sub

    Private Sub LogWaitWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LogWaitWorker.RunWorkerCompleted
        If ToolStripButton2.Checked Then
            If LogRefreshWorker.IsBusy = False Then
                ToolStripButton1.Enabled = False
                LogRefreshWorker.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        MaxVisibleLines = ToolStripComboBox1.SelectedItem
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If Not LogRefreshWorker.IsBusy Then
            ToolStripButton1.Enabled = False
            LogRefreshWorker.RunWorkerAsync()
        End If
    End Sub

    Private Sub ToolStripButton3_CheckedChanged(sender As Object, e As EventArgs) Handles ToolStripButton3.CheckedChanged
        If ToolStripButton3.Checked = False Then
            ListView1.Items.Clear()
            CurrentLoadedLinePos = 0
            InitLinePos = 0
            If Not LogRefreshWorker.IsBusy Then
                ToolStripButton1.Enabled = False
                LogRefreshWorker.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Try
            Dim searchcoll As ListViewItem()
            searchcoll = ListView1.Items.Find(ToolStripTextBox1.Text, True)

            If Not searchcoll.Count = 0 Then
                For index = 0 To searchcoll.Count - 1
                    searchcoll(index).Selected = True
                Next
            Else
                MsgBox("No results.")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        If ToolStripButton5.Checked Then
            Me.TopMost = True
        Else
            Me.TopMost = False
        End If
    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        If ToolStripButton6.Checked Then
            ShowInfoItems = True
        Else
            ShowInfoItems = False
        End If
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        If ToolStripButton7.Checked Then
            ShowWarningItems = True
        Else
            ShowWarningItems = False
        End If
    End Sub

    Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs) Handles ToolStripButton8.Click
        If ToolStripButton8.Checked Then
            ShowErrorItems = True
        Else
            ShowErrorItems = False
        End If
    End Sub

    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs) Handles ToolStripButton9.Click
        CurrentLoadedLinePos = 0
        ListView1.Items.Clear()
    End Sub

    Private Sub ListView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDoubleClick
        Try
            Dim currobj As LogEntry
            currobj = ListView1.SelectedItems(0).Tag

            If Not IsNothing(currobj.ErrorObj) Then
                Dim errorfrm As New LogFormError
                errorfrm.PropertyGrid1.SelectedObject = currobj.ErrorObj
                errorfrm.TextBox1.Text = currobj.Message
                errorfrm.Show()
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class