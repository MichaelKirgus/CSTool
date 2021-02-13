'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Windows.Forms
Imports Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine

Public Class ItemPropDlg

    Public PropertyObj As WqlResultObject = Nothing

    Private Delegate Sub AddListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem, ByVal EnsureVisible As Boolean)
    Private Delegate Sub BeginUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub EndUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub ClearListViewDelegate(ByVal ListViewCtl As ListView)

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

    Private Sub ItemPropDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If PropertyLoader.IsBusy = False Then
            SMSPropGUIState(False)
            SMSPropLoadGUIState(True)
            PropertyLoader.RunWorkerAsync()
        End If
    End Sub

    Public Sub SMSPropGUIState(ByVal State As Boolean)
        SCCMPropSplash.Visible = Not State
        SCCMPropSplashLabel.Visible = Not State

    End Sub

    Public Sub SMSPropLoadGUIState(ByVal State As Boolean)
        If State Then
            SCCMPropSplash.Image = My.Resources.adminstatus
            SCCMPropSplashLabel.Text = "Loading..."
        Else
            SCCMPropSplash.Image = My.Resources.NoStat
            SCCMPropSplashLabel.Text = "No data"
        End If
    End Sub

    Public Function SetOptimalListViewStyle() As Boolean
        Try
            ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub PropertyLoader_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles PropertyLoader.DoWork
        Try
            BeginUpdateListView(ListView1)
            ClearListView(ListView1)

            Dim ind As Integer = 0
            For index = 0 To PropertyObj.Count
                Try
                    Dim oo As New ListViewItem
                    oo.Text = PropertyObj.PropertyNames(index)
                    oo.SubItems.Add(PropertyObj.PropertyNames(index))

                    AddListViewItem(ListView1, oo)
                Catch ex As Exception
                End Try
            Next

            EndUpdateListView(ListView1)
            SetOptimalListViewStyle()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PropertyLoader_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles PropertyLoader.RunWorkerCompleted
        SMSPropGUIState(True)
        SMSPropLoadGUIState(False)
    End Sub

    Private Sub ExportInDateiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExportInDateiToolStripMenuItem1.Click
        SaveTextfileDlg.ShowDialog()
    End Sub
End Class
