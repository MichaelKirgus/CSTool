'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports System.Windows.Forms

Public Class MessageFrm

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub MessageFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            TrackBar1.Value = TextBox1.Font.SizeInPoints
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Try
            Dim gg As New Font(TextBox1.Font.FontFamily, TrackBar1.Value)
            TextBox1.Font = gg
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            'IO.File.WriteAllText(My.Computer.FileSystem.SpecialDirectories.Temp & "\export.txt", TextBox1.Text)
            'Shell(parentFrm._settings.ExportResultToTextfileExecutablePath & " " & My.Resources.trenn & My.Computer.FileSystem.SpecialDirectories.Temp & "\export.txt" & My.Resources.trenn, AppWinStyle.NormalFocus)
        Catch ex As Exception
        End Try
    End Sub
End Class
