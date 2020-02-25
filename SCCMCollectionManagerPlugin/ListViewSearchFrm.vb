'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Windows.Forms

Public Class ListViewSearchFrm

    Public ListViewCtl As ListView

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim result As Boolean = False
            Dim FirstFoundItemIndex As Integer = 0

            For ind = 0 To ListViewCtl.Items.Count - 1
                If ListViewCtl.Items(ind).Text.ToLower.Contains(TextBox1.Text) Then
                    ListViewCtl.Items(ind).Selected = True
                    If result = False Then
                        FirstFoundItemIndex = ind
                    End If
                    result = True
                End If
            Next

            If result = False Then
                MsgBox("Keine Ergebnisse gefunden!", MsgBoxStyle.Information)
            Else
                ListViewCtl.EnsureVisible(FirstFoundItemIndex)
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
