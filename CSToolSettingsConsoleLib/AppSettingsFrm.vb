'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolApplicationSettingsLib
Imports CSToolApplicationSettingsManager
Imports CSToolUserSettingsLib
Imports CSToolUserSettingsManager

Public Class AppSettingsFrm
    Public WasChanged As Boolean = False
    Public ApplicationSettingsFile As String = "AppSettings.xml"
    Public AppSettingsHandler As New ApplicationSettingsManager
    Public UserSettingsHandler As New UserSettingsManager
    Public AppSettingsObj As ApplicationSettings

    Private Sub AppSettingsFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AppSettingsObj = AppSettingsHandler.LoadSettings(ApplicationSettingsFile)
        PropertyGrid1.SelectedObject = AppSettingsObj
        PropertyGrid2.SelectedObject = AppSettingsObj.LogSettings
        PropertyGrid3.SelectedObject = AppSettingsObj.LauncherLogSettings
        If Not AppSettingsObj.UserInitialTemplateDir = "" Then
            If IO.File.Exists(AppSettingsObj.UserInitialTemplateDir & "\" & UserSettingsHandler.UserSettingsFile) Then
                PropertyGrid5.SelectedObject = UserSettingsHandler.LoadSettings(AppSettingsObj.UserInitialTemplateDir & "\" & UserSettingsHandler.UserSettingsFile)
                Dim settingsobj As UserSettings
                settingsobj = PropertyGrid5.SelectedObject
                If Not settingsobj.CentralCustomActions = "" Then
                    ToolStripTextBox1.Text = settingsobj.CentralCustomActions
                    PropertyGrid4.SelectedObject = UserSettingsHandler.LoadCentralCustomActions(ToolStripTextBox1.Text)
                End If
            End If
        End If
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

    Public Sub HandleSaveSettings()
        AppSettingsHandler.SaveSettings(AppSettingsObj, ApplicationSettingsFile)
        If IO.File.Exists(AppSettingsObj.UserInitialTemplateDir & "\" & UserSettingsHandler.UserSettingsFile) Then
            PropertyGrid5.SelectedObject = UserSettingsHandler.LoadSettings(AppSettingsObj.UserInitialTemplateDir & "\" & UserSettingsHandler.UserSettingsFile)
        End If
    End Sub

    Private Sub PropertyGrid2_PropertyValueChanged(s As Object, e As Windows.Forms.PropertyValueChangedEventArgs) Handles PropertyGrid2.PropertyValueChanged
        WasChanged = True
    End Sub

    Private Sub PropertyGrid3_PropertyValueChanged(s As Object, e As Windows.Forms.PropertyValueChangedEventArgs) Handles PropertyGrid3.PropertyValueChanged
        WasChanged = True
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If Not ToolStripTextBox1.Text = "" Then
            If IO.File.Exists(ToolStripTextBox1.Text) Then
                PropertyGrid4.SelectedObject = UserSettingsHandler.LoadCentralCustomActions(ToolStripTextBox1.Text)
            End If
        End If
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        If Not ToolStripTextBox1.Text = "" Then
            If Not IsNothing(PropertyGrid4.SelectedObject) Then
                UserSettingsHandler.SaveCentralCustomActions(PropertyGrid4.SelectedObject, ToolStripTextBox1.Text)
            End If
        End If
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        HandleSaveSettings()
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        HandleSaveSettings()
    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        HandleSaveSettings()
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        If Not IsNothing(PropertyGrid5.SelectedObject) Then
            UserSettingsHandler.SaveSettings(PropertyGrid5.SelectedObject, AppSettingsObj.UserInitialTemplateDir & "\" & UserSettingsHandler.UserSettingsFile)
        End If
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        If Not OpenFileDialog1.FileName = "" Then
            ToolStripTextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub
End Class