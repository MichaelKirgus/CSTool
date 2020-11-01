'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Public Class RuntimeInfoForm
    Public _parent As MainForm

    Private Sub RuntimeInfoForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            TextBox1.Text = Application.StartupPath

            If Not IsNothing(_parent.LogManager) Then
                If Not IsNothing(_parent.LogManager.LogCollection) Then
                    TextBox2.Text = _parent.LogManager.LogCollection.Count
                End If
            End If

            For Each cmdobj As String In Environment.GetCommandLineArgs
                ListBox1.Items.Add(cmdobj)
            Next
        Catch ex As Exception
        End Try
    End Sub
End Class