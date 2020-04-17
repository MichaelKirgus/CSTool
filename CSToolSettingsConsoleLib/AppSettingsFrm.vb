'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolApplicationSettingsLib
Imports CSToolApplicationSettingsManager

Public Class AppSettingsFrm
    Public WasChanged As Boolean = False
    Public ApplicationSettingsFile As String = "AppSettings.xml"
    Public AppSettingsHandler As New ApplicationSettingsManager
    Public AppSettingsObj As ApplicationSettings

    Private Sub AppSettingsFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AppSettingsObj = AppSettingsHandler.LoadSettings(ApplicationSettingsFile)
        PropertyGrid1.SelectedObject = AppSettingsObj
        PropertyGrid2.SelectedObject = AppSettingsObj.LogSettings
        PropertyGrid3.SelectedObject = AppSettingsObj.LauncherLogSettings
    End Sub

    Private Sub PropertyGrid1_PropertyValueChanged(s As Object, e As Windows.Forms.PropertyValueChangedEventArgs) Handles PropertyGrid1.PropertyValueChanged
        WasChanged = True
    End Sub

    Private Sub AppSettingsFrm_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If WasChanged Then
            Dim result As MsgBoxResult
            result = MsgBox("Do you want to save changes? Manual main application restart is needed to reload all settings.", MsgBoxStyle.YesNo)
            If result = MsgBoxResult.Yes Then
                AppSettingsHandler.SaveSettings(AppSettingsObj, ApplicationSettingsFile)
            End If
        End If
    End Sub

    Private Sub PropertyGrid2_PropertyValueChanged(s As Object, e As Windows.Forms.PropertyValueChangedEventArgs) Handles PropertyGrid2.PropertyValueChanged
        WasChanged = True
    End Sub

    Private Sub PropertyGrid3_PropertyValueChanged(s As Object, e As Windows.Forms.PropertyValueChangedEventArgs) Handles PropertyGrid3.PropertyValueChanged
        WasChanged = True
    End Sub
End Class