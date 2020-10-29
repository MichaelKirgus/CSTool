Imports System.Windows.Forms

Public Class ExecuteCtl
    Public _ParentListViewItemIndex As Integer = 0
    Public _ParentListViewCtl As ListView
    Public _Commands As New List(Of SSHExecuteItem)
    Public _HostInfoObj As SSHHostItem
    Public _AbortIfOneCommandFails As Boolean = True

    Public SSHConnectionManager As New SSHManager

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If SSHConnectionManager.ConnectToServer() Then
            For index = 0 To _Commands.Count - 1
                If Not SSHConnectionManager.ExecuteCommand(_Commands(index), True) Then
                    If _AbortIfOneCommandFails Then
                        Exit For
                    End If
                End If
            Next

            If _HostInfoObj.CloseConnectionAfterExecute Then
                SSHConnectionManager.CloseConnection(False)
            End If

            e.Result = SSHConnectionManager.OutputConsoleResults
        End If
    End Sub

    Private Sub ExecuteCtl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = _HostInfoObj.Hostname
        Label2.Text = "Pending..."

        If Not BackgroundWorker1.IsBusy Then
            Label2.Text = "Execute..."
            BackgroundWorker1.RunWorkerAsync()
        End If
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Label2.Text = "Completed."
        ProgressBar1.Visible = False

        Dim resultlist As List(Of String)
        resultlist = e.Result

        Dim output As String = ""

        If Not IsNothing(resultlist) Then
            For index = 0 To resultlist.Count - 1
                output += resultlist(index) & vbNewLine & vbNewLine
            Next

            TextBox1.Text = output
            _ParentListViewCtl.Items(_ParentListViewItemIndex).SubItems(4).Text = resultlist(resultlist.Count - 1)
        Else
            TextBox1.Text = "No output." & vbNewLine & vbNewLine & SSHConnectionManager.LastError.Message
            _ParentListViewCtl.Items(_ParentListViewItemIndex).SubItems(4).Text = "No output."
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SSHConnectionManager.CloseConnection(True)
        Me.Parent.Controls.Remove(Me)
    End Sub
End Class
