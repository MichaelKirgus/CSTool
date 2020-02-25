'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Public Class EnvironmentState
    Public _Parent As MainForm

    Private Sub EnvironmentState_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadEnvVarsToGUI(_Parent.EnvironmentManager.GetEnvironmentVarsFromPlugins(_Parent.WindowManagerHandler.PluginManager.PluginCollection), ListView1, ToolStripButton2.Checked)
    End Sub

    Public Function LoadEnvVarsToGUI(ByVal TargetEnvVars As List(Of KeyValuePair(Of String, String)), ByVal ListViewCtl As ListView, ByVal ShowPasswords As Boolean) As Boolean
        Try
            ListViewCtl.BeginUpdate()
            ListViewCtl.Items.Clear()

            For index = 0 To TargetEnvVars.Count - 1
                Dim KeyP As KeyValuePair(Of String, String)
                KeyP = TargetEnvVars(index)

                Dim oo As New ListViewItem
                oo.Text = KeyP.Key

                If KeyP.Key.Contains("Password") And ShowPasswords = False Then
                    oo.SubItems.Add("[Password]")
                Else
                    oo.SubItems.Add(KeyP.Value)
                End If

                ListViewCtl.Items.Add(oo)
            Next

            ListViewCtl.EndUpdate()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        LoadEnvVarsToGUI(_Parent.EnvironmentManager.GetEnvironmentVarsFromPlugins(_Parent.WindowManagerHandler.PluginManager.PluginCollection), ListView1, ToolStripButton2.Checked)
    End Sub

    Private Sub CopyNameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyNameToolStripMenuItem.Click
        My.Computer.Clipboard.SetText(ListView1.SelectedItems(0).Text)
    End Sub

    Private Sub CopyValueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyValueToolStripMenuItem.Click
        My.Computer.Clipboard.SetText(ListView1.SelectedItems(0).SubItems(1).Text)
    End Sub
End Class