'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Windows.Forms
Imports CSToolPluginLib
Imports WeifenLuo.WinFormsUI.Docking
Imports WeifenLuo.WinFormsUI.Docking.DockPanelExtender

Public Class DockingHostWindow
    Public PluginHandler As ICSToolInterface
    Public Property InstanceGUID As String
    Public Property SettingsKey As String = "Default"
    Public Property AppInit As Boolean = False
    Public Property PluginLoaded As Boolean = False
    Public Property _UserProfilePath As String = ""
    Public Property _PluginSettingsFile As String = ""
    Public Property _IsNonPersistent As Boolean = False

    Protected Overloads Overrides Function GetPersistString() As String
        Return PluginHandler.PluginGUID & "|" & InstanceGUID.ToString & "|" & PluginHandler.PluginName
    End Function

    Private Sub DockingHostWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            Me.TabText = PluginHandler.WindowTitle

            Me.Controls.Add(ctl)
            ctl.Dock = DockStyle.Fill

            Dim kk As New Drawing.Size(ctl.Width, ctl.Height)
            Me.Size = kk

            Me.Icon = PluginHandler.UserControlIcon

            If PluginHandler.SupportFloat Then
                Me.DockAreas = DockAreas.DockLeft Or DockAreas.DockRight Or DockAreas.DockTop Or DockAreas.DockBottom Or DockAreas.Document Or DockAreas.Float
            Else
                Me.DockAreas = DockAreas.DockLeft Or DockAreas.DockRight Or DockAreas.DockTop Or DockAreas.DockBottom Or DockAreas.Document
            End If

            If PluginHandler.SupportsRaisingActions Then
                ReciveActionsToolStripMenuItem.Checked = PluginHandler.RaisingActionsEnabled
            Else
                ReciveActionsToolStripMenuItem.Checked = False
                ReciveActionsToolStripMenuItem.Enabled = False
            End If

            If Not _PluginSettingsFile = "" Then
                WatchingWorker.RunWorkerAsync()
            End If
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

    Private Sub DockingHostWindow_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If PluginLoaded = False Then
            Me.Icon = PluginHandler.UserControlIcon

            PluginLoaded = True
        End If
    End Sub

    Public Sub ShowWindowTitle(Optional ByVal ForceTitle As Boolean = False)
        If PluginLoaded Or ForceTitle Then
            Dim currtitle As String
            currtitle = PluginHandler.CurrentWindowTitle
            If Not currtitle = "" Then
                Me.Text = currtitle
                Me.TabText = currtitle
            End If
        End If
    End Sub

    Private Sub DockingHostWindow_TextChanged(sender As Object, e As EventArgs) Handles MyBase.TextChanged
        ShowWindowTitle()
    End Sub

    Private Sub DockingHostWindow_ResizeEnd(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd
        Me.Invalidate(True)
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        Try
            PluginHandler.RefreshGUI()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DockingHostWindow_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Save plugin settings
        If Not _IsNonPersistent Then
            If AllowCloseToolStripMenuItem.Checked Then
                FileSystemWatcher1.EnableRaisingEvents = False
                If Not e.CloseReason = CloseReason.UserClosing Then
                    PluginHandler.UnloadPlugin()

                    If SaveSettingsToolStripMenuItem.Checked Then
                        PluginHandler.SavePluginSettings(_PluginSettingsFile)
                    End If
                Else
                    PluginHandler.UnloadPlugin()

                    Try
                        'Save settings to recycle bin
                        If Not IO.Directory.Exists(_UserProfilePath & "\" & "\BIN") Then
                            IO.Directory.CreateDirectory(_UserProfilePath & "\BIN")
                        Else
                            Dim files As String()
                            files = IO.Directory.GetFiles(_UserProfilePath & "\BIN")
                            If Not files.Count = 0 Then
                                For index = 0 To files.Count - 1
                                    Try
                                        IO.File.Delete(files(index))
                                    Catch ex As Exception
                                    End Try
                                Next
                            End If
                        End If
                        PluginHandler.SavePluginSettings(_UserProfilePath & "\BIN\" & PluginHandler.PluginGUID & ".xml")
                    Catch ex As Exception
                    End Try

                    'Delete original settings file
                    IO.File.Delete(_PluginSettingsFile)
                End If
            End If
        End If
    End Sub

    Private Sub AllowCloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllowCloseToolStripMenuItem.Click
        If AllowCloseToolStripMenuItem.Checked Then
            Me.CloseButtonVisible = True
            Me.CloseButton = True
        Else
            Me.CloseButtonVisible = False
            Me.CloseButton = False
        End If
    End Sub

    Private Sub DockingHostWindow_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Controls.Clear()
        PluginHandler = Nothing
        Me.Tag = Nothing
    End Sub

    Private Sub DockingHostWindow_DockStateChanged(sender As Object, e As EventArgs) Handles MyBase.DockStateChanged
        Try
            If Me.DockState = DockState.Float Then
                Dim ctl As UserControl
                ctl = Me.Controls(0)
                Dim kk As New Drawing.Size(ctl.Width, ctl.Height)
                Me.Size = kk
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ExportSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportSettingsToolStripMenuItem.Click
        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub ImportSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportSettingsToolStripMenuItem.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Try
            If PluginHandler.LoadPluginSettings(OpenFileDialog1.FileName) Then
                PluginHandler.RefreshGUI()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Try
            If PluginHandler.SavePluginSettings(SaveFileDialog1.FileName) Then
                MsgBox("Settings export complete.")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub WatchingWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles WatchingWorker.DoWork
        Try
            Threading.Thread.Sleep(100)

            If Not _PluginSettingsFile = "" Then
                Dim dirinf As New IO.FileInfo(_PluginSettingsFile)
                FileSystemWatcher1.Path = dirinf.DirectoryName
                FileSystemWatcher1.Filter = dirinf.Name
                FileSystemWatcher1.EnableRaisingEvents = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FileSystemWatcher1_Changed(sender As Object, e As IO.FileSystemEventArgs) Handles FileSystemWatcher1.Changed
        Try
            If Not WatchingSettingsLoadWorker.IsBusy Then
                WatchingSettingsLoadWorker.RunWorkerAsync()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub AutoReloadSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutoReloadSettingsToolStripMenuItem.Click
        If AutoReloadSettingsToolStripMenuItem.Checked Then
            FileSystemWatcher1.EnableRaisingEvents = True
        Else
            FileSystemWatcher1.EnableRaisingEvents = False
        End If
    End Sub

    Private Sub WatchingSettingsLoadWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles WatchingSettingsLoadWorker.DoWork
        Threading.Thread.Sleep(500)
    End Sub

    Private Sub WatchingSettingsLoadWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles WatchingSettingsLoadWorker.RunWorkerCompleted
        If Not IsNothing(PluginHandler) Then
            If PluginHandler.LoadPluginSettings(_PluginSettingsFile) Then
                PluginHandler.RefreshGUI()
            End If
        End If
    End Sub

    Private Sub ReciveActionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReciveActionsToolStripMenuItem.Click
        PluginHandler.RaisingActionsEnabled = ReciveActionsToolStripMenuItem.Checked
        PluginHandler.PluginSettingsChanged = True
    End Sub
End Class