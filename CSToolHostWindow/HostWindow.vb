'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Windows.Forms
Imports CSToolPluginLib

Public Class HostWindow
    Public PluginHandler As ICSToolInterface
    Public Property InstanceGUID As String
    Public Property SettingsKey As String = "Default"
    Public Property AppInit As Boolean = False
    Public Property PluginLoaded As Boolean = False
    Public Property _UserProfilePath As String = ""
    Public Property _PluginSettingsFile As String = ""
    Public Property _IsNonPersistent As Boolean = False
    Private Sub HostWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim ctl As UserControl = Nothing

            If AppInit Then
                'User created new instance of plugin
                PluginHandler = Me.Tag
            Else
                'Load plugin from layout file and restore settings (if saved)
                If IO.File.Exists(_PluginSettingsFile) Then
                    PluginHandler.LoadPluginSettings(_PluginSettingsFile)
                End If
                Me.Tag = PluginHandler
            End If

            ctl = PluginHandler.UserControl

            Me.Text = PluginHandler.WindowTitle

            Me.Controls.Add(ctl)
            ctl.Dock = DockStyle.Fill

            Dim kk As New Drawing.Size(ctl.Width, ctl.Height)
            Me.Size = kk

            Me.Icon = PluginHandler.UserControlIcon
        Catch ex As Exception
        End Try
    End Sub

    Private Sub EinstellungenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EinstellungenToolStripMenuItem.Click
        Try
            Dim hh As New PluginSettingsForm
            hh.SettingsObj = PluginHandler.UserSettingsClass
            hh.EnvironmentObj = PluginHandler.EnvironmentRuntimeVariables
            hh._Parent = Me
            hh.Show()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        HostFormPluginMenu.Show(MousePosition)
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        PluginHandler.RefreshGUI()
    End Sub

    Private Sub HostWindow_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not _IsNonPersistent Then
            If AllowCloseToolStripMenuItem.Checked Then
                PluginHandler.UnloadPlugin()

                If SaveSettingsToolStripMenuItem.Checked Then
                    PluginHandler.SavePluginSettings(_PluginSettingsFile)
                End If
            End If
        End If
    End Sub

    Private Sub SaveSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveSettingsToolStripMenuItem.Click
        PluginHandler.SavePluginSettings(_PluginSettingsFile)
    End Sub
End Class