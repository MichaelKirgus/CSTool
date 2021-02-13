'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Public Class StartLoadingFrm
    Public LastStatusMessageText As String = ""
    Private Delegate Sub SetLabelTextDelegate(ByVal LabelCtl As Label, ByVal TextStr As String)

    Private Sub SetLabelText(ByVal LabelCtl As Label, ByVal TextStr As String)
        If LabelCtl.InvokeRequired Then
            Dim ClearListViewObj As New SetLabelTextDelegate(AddressOf SetLabelText)
            LabelCtl.Invoke(ClearListViewObj, New Object() {LabelCtl, TextStr})
        Else
            LabelCtl.Text = TextStr
        End If
    End Sub

    Private Sub StartLoadingFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AppVersionLbl.Text = My.Application.Info.Version.ToString
        LoadingState.RunWorkerAsync()
    End Sub

    Private Sub LoadingState_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadingState.DoWork
        Try
            While Not Me.Disposing
                If Application.OpenForms.Count = 2 Then
                    Dim currfrm As MainForm
                    currfrm = Application.OpenForms.OfType(Of MainForm).ElementAt(0)
                    If Not LastStatusMessageText = currfrm.CurrentLoadActionState Then
                        LastStatusMessageText = currfrm.CurrentLoadActionState
                        SetLabelText(LoadingStateLbl, LastStatusMessageText)
                    End If
                    If LastStatusMessageText = "Finished!" Then
                        Exit While
                    End If
                End If
                Threading.Thread.Sleep(20)
            End While
        Catch ex As Exception
        End Try
    End Sub
End Class