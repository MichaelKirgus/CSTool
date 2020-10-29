'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports CSToolEnvironmentManager
Imports CSToolPingHelper

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public EnvManager As New EnvironmentManager
    Public CurrentIPHostname As String = ""

    Private Delegate Sub AddListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem, ByVal EnsureVisible As Boolean)
    Private Delegate Sub RemoveListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem)
    Private Delegate Sub BeginUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub EndUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub ClearListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub UpdateListViewItemByIndexDelgeate(ByVal ListViewCtl As ListView, ListViewItemIndex As Integer, ListViewSubItemIndex As Integer, ListViewSubItemText As String)
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

    Private Sub UpdateListViewItemByIndex(ByVal ListViewCtl As ListView, ListViewItemIndex As Integer, ListViewSubItemIndex As Integer, ListViewSubItemText As String)
        If ListViewCtl.InvokeRequired Then
            Dim UpdateListViewSubItem As New UpdateListViewItemByIndexDelgeate(AddressOf UpdateListViewItemByIndex)
            ListViewCtl.Invoke(UpdateListViewSubItem, New Object() {ListViewCtl, ListViewItemIndex, ListViewSubItemIndex, ListViewSubItemText})
        Else
            ListViewCtl.Items(ListViewItemIndex).SubItems(ListViewSubItemIndex).Text = ListViewSubItemText
        End If
    End Sub

    Public Sub RaiseAction(ByVal IPOrHostname As String, Optional ByVal IsRefresh As Boolean = False)
        CurrentIPHostname = IPOrHostname

    End Sub
    Public Sub RefreshGUI()
        LoadHostsToView()
        LoadCommandsToView()
    End Sub

    Public Sub LoadHostsToView()
        ListView1.BeginUpdate()
        ListView1.Items.Clear()

        Try
            If Not _Settings.HostCollection.Count = 0 Then
                For index = 0 To _Settings.HostCollection.Count - 1
                    Dim bb As New ListViewItem
                    bb.Text = _Settings.HostCollection(index).Hostname
                    bb.SubItems.Add(_Settings.HostCollection(index).Description)

                    If _Settings.HostCollection(index).CheckHost Then
                        bb.SubItems.Add("Pending...")
                    Else
                        bb.SubItems.Add("No check")
                    End If

                    bb.SubItems.Add(_Settings.HostCollection(index).SSHLoginUsername)
                    bb.SubItems.Add("None")

                    ListView1.Items.Add(bb)
                Next
            End If
        Catch ex As Exception
        End Try

        ListView1.EndUpdate()
    End Sub

    Public Sub LoadCommandsToView()
        Try
            ToolStripComboBox1.Items.Clear()

            If Not _Settings.CommandCollection.Count = 0 Then
                ToolStripComboBox1.Enabled = True
                ToolStripButton1.Enabled = True

                For index = 0 To _Settings.CommandCollection.Count - 1
                    ToolStripComboBox1.Items.Add(_Settings.CommandCollection(index).Name)
                Next

                ToolStripComboBox1.SelectedIndex = 0
            Else
                ToolStripButton1.Enabled = False
                ToolStripComboBox1.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadHostsToView()
        LoadCommandsToView()

        If _Settings.PingAllHostsAtStart Then
            If Not PingHosts.IsBusy Then
                ToolStripButton3.Enabled = False
                PingHosts.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        If Not PingHosts.IsBusy Then
            ToolStripButton3.Enabled = False
            PingHosts.RunWorkerAsync()
        End If
    End Sub

    Private Sub PingHosts_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles PingHosts.DoWork
        Try
            If Not ListView1.Items.Count = 0 Then
                Dim PingHandler As New PingHelper

                For index = 0 To ListView1.Items.Count - 1
                    If _Settings.HostCollection(index).CheckHost Then
                        Dim pingres As Boolean
                        pingres = PingHandler.Ping(_Settings.HostCollection(index).Hostname, _Settings.HostCollection(index).PingTimeout)

                        If pingres Then
                            UpdateListViewItemByIndex(ListView1, index, 2, "OK (" & PingHandler.ResponseTime & " ms)")
                        Else
                            UpdateListViewItemByIndex(ListView1, index, 2, "Timeout")
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PingHosts_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles PingHosts.RunWorkerCompleted
        ToolStripButton3.Enabled = True
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            If Not IsNothing(ListView1.SelectedItems) Then
                If Not ListView1.SelectedItems.Count = 0 Then
                    Dim hostcnt As Integer
                    hostcnt = ListView1.SelectedItems.Count

                    Dim msgboxres As MsgBoxResult
                    msgboxres = MsgBox("Do you want to execute the command on " & hostcnt & " hosts?", MsgBoxStyle.YesNo)

                    If msgboxres = MsgBoxResult.Yes Then
                        FlowLayoutPanel1.SuspendLayout()

                        For Each selitm As ListViewItem In ListView1.SelectedItems
                            Dim CommandCtl As New ExecuteCtl
                            Dim ExCommandItm As SSHExecuteItem
                            ExCommandItm = _Settings.CommandCollection(ToolStripComboBox1.SelectedIndex)

                            CommandCtl._Commands.Add(ExCommandItm)
                            CommandCtl._HostInfoObj = _Settings.HostCollection(selitm.Index)
                            CommandCtl._ParentListViewCtl = ListView1
                            CommandCtl._ParentListViewItemIndex = selitm.Index

                            Dim SSHManagerObj As New SSHManager
                            SSHManagerObj.Init(_Settings.HostCollection(selitm.Index))

                            CommandCtl.SSHConnectionManager = SSHManagerObj

                            FlowLayoutPanel1.Controls.Add(CommandCtl)

                            CommandCtl.Width = FlowLayoutPanel1.Width - 8
                        Next

                        FlowLayoutPanel1.ResumeLayout()
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
