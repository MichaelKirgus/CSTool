'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine

Public Class ManualWQLQueryDlg

    Public CurrentSMSConnection As WqlConnectionManager = Nothing

    Private Delegate Sub AddListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem, ByVal EnsureVisible As Boolean)
    Private Delegate Sub AddListViewColumnDelegate(ByVal ListViewCtl As ListView, ListViewItemColumnText As String)
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

    Private Sub AddListViewColumn(ByVal ListViewCtl As ListView, ListViewItemColumnText As String)
        If ListViewCtl.InvokeRequired Then
            Dim delAddListViewItem As New AddListViewColumnDelegate(AddressOf AddListViewColumn)
            ListViewCtl.Invoke(delAddListViewItem, New Object() {ListViewCtl, ListViewItemColumnText})
        Else
            ListViewCtl.Columns.Add(ListViewItemColumnText)
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

    Public Sub SMSPropGUIState(ByVal State As Boolean)
        SCCMPropSplash.Visible = Not State
        SCCMPropSplashLabel.Visible = Not State

        ToolStripComboBox1.Enabled = State
        ToolStripButton1.Enabled = State
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

    Private Sub ManualWQLQueryDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Function GetCustomQuery(ByVal Query As String) As WqlQueryResultsObject
        Try
            Dim CustomQueryResult As WqlQueryResultsObject
            Dim CustomQuery = Query
            CustomQueryResult = CurrentSMSConnection.QueryProcessor.ExecuteQuery(CustomQuery)

            Return CustomQueryResult
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub PropertyLoader_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles PropertyLoader.DoWork
        Try
            If (Not CurrentSMSConnection Is Nothing) And (Not e.Argument = "") Then
                BeginUpdateListView(ListView1)
                ClearListView(ListView1)

                Dim ff As WqlQueryResultsObject
                ff = GetCustomQuery(e.Argument)

                'Spalten hinzufügen
                For Each item As WqlResultObject In ff
                    For Each item2 As String In ff.PropertyNames
                        AddListViewColumn(ListView1, item2)
                    Next
                    Exit For
                Next

                'Einträge hinzufügen
                Dim currind As Integer = 0

                For Each item As WqlResultObject In ff
                    Dim uu As New ListViewItem

                    For Each item2 As String In ff.PropertyNames
                        Try
                            If currind = 0 Then
                                uu.Text = item.PropertyList(item.PropertyNames(currind))
                            Else
                                uu.SubItems.Add(item.PropertyList(item.PropertyNames(currind)))
                            End If

                            AddListViewItem(ListView1, uu)
                        Catch ex As Exception
                        End Try
                        currind += 1
                    Next

                    currind = 0
                Next
            Else
                MsgBox("No connection to server or the query was empty.", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If Not PropertyLoader.IsBusy Then
            SMSPropGUIState(False)
            SMSPropLoadGUIState(True)
            PropertyLoader.RunWorkerAsync(ToolStripComboBox1.Text)
        End If
    End Sub

    Private Sub PropertyLoader_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles PropertyLoader.RunWorkerCompleted
        EndUpdateListView(ListView1)
        SetOptimalListViewStyle()
        SMSPropGUIState(True)
        SMSPropLoadGUIState(False)
    End Sub

    Private Sub ExportInDateiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExportInDateiToolStripMenuItem1.Click
        SaveTextfileDlg.ShowDialog()
        If Not SaveTextfileDlg.FileName = "" Then
            'Form1.SaveListviewContentToFile(SaveTextfileDlg.FileName, ListView1)
        End If
    End Sub

    Private Sub ManualWQLQueryDlg_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        Dim jj As Size
        jj.Width = Me.Size.Width - ToolStripButton1.Width - ToolStripLabel1.Width - 20
        jj.Height = ToolStripComboBox1.Height
        ToolStripComboBox1.Size = jj
    End Sub

    Private Sub ToolStripComboBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ToolStripComboBox1.KeyDown
        If e.KeyValue = Keys.Enter Then
            ToolStripButton1.PerformClick()
        End If
    End Sub
End Class