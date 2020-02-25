'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Public Class PluginSettingsForm
    Public _Parent As Object
    Public SettingsObj As Object
    Public EnvironmentObj As List(Of KeyValuePair(Of String, String)) = Nothing

    Private Sub PluginSettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PropertyGrid1.SelectedObject = SettingsObj
        If Not IsNothing(EnvironmentObj) Then
            Dim EnvMngr As New CSToolEnvironmentManager.EnvironmentManager
            PropertyGrid2.SelectedObject = EnvMngr.GetEnvironmentObjectFromAllEnvironmentVariables(EnvironmentObj)
        Else
            TabControl1.TabPages.RemoveAt(1)
        End If

        If Not IsNothing(_Parent) Then
            PropertyGrid3.SelectedObject = _Parent
        End If
    End Sub

    Private Sub PluginSettingsForm_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        _Parent.PluginHandler.RefreshGUI()
    End Sub

    Private Sub PropertyGrid1_PropertyValueChanged(s As Object, e As Windows.Forms.PropertyValueChangedEventArgs) Handles PropertyGrid1.PropertyValueChanged
        _Parent.PluginHandler.RefreshGUI()
    End Sub
End Class