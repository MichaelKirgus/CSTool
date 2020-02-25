'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolEnvironmentManager
Imports CSToolLogLib.LogSettings
Imports MSTSCLib

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public EnvManager As New EnvironmentManager
    Public CurrentIPHostname As String = ""
    Private FirstStart As Boolean = True

    Friend WithEvents RDPCtl As AxMSTSCLib.AxMsRdpClient9NotSafeForScripting = Nothing

    Public Sub RaiseAction(ByVal IPOrHostname As String, Optional ByVal IsRefresh As Boolean = False)
        CurrentIPHostname = IPOrHostname
    End Sub
    Public Sub RefreshGUI(Optional ByVal AutoConnect As Boolean = True)
        SetGUIState()
        InitAndConnectToRDP(AutoConnect)
    End Sub

    Public Function InitAndConnectToRDP(Optional ByVal AutoConnect As Boolean = True)
        Try
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Refresh RDP instance " & _Settings.ServerName, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            LoadImage.Visible = True

            If FirstStart Then
                _ParentInstance.CurrentLogInstance.WriteLogEntry("First start, set window title...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                If Not IsNothing(_Settings) Then
                    If Not _Settings.InitialTitle = "" Then
                        _ParentInstance.CurrentWindowTitle = _Settings.InitialTitle
                        ParentForm.Text = _Settings.InitialTitle
                    End If
                End If
            End If

            If Not IsNothing(RDPCtl) Then
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Resetting RDP active-x object", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                If RDPCtl.Connected Then
                    RDPCtl.Disconnect()
                    RDPCtl.Dispose()
                    RDPBox.Controls.Remove(RDPCtl)
                    RDPCtl = Nothing
                Else
                    RDPCtl.Dispose()
                    RDPBox.Controls.Remove(RDPCtl)
                    RDPCtl = Nothing
                End If
            End If

            _ParentInstance.CurrentLogInstance.WriteLogEntry("Create new active-x object...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            RDPCtl = New AxMSTSCLib.AxMsRdpClient9NotSafeForScripting
            RDPCtl.BeginInit()
            RDPBox.Controls.Add(RDPCtl)
            RDPCtl.Dock = Windows.Forms.DockStyle.Fill
            RDPCtl.Enabled = True
            RDPCtl.Location = New System.Drawing.Point(0, 0)
            RDPCtl.Name = "rdp"
            RDPCtl.TabIndex = 0
            RDPCtl.Visible = True
            Dim serverstr As String = ""

            If AutoConnect Then
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Auto-Connect to server...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                If _Settings.AutoConnect Then
                    If Not _Settings.ServerName = "" Then
                        serverstr = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.ServerName)
                        RDPCtl.Server = serverstr
                        If Not _Settings.RDPDomain = "" Then
                            RDPCtl.Domain = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RDPDomain)
                        End If
                        If Not _Settings.RDPUsername = "" Then
                            RDPCtl.UserName = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RDPUsername)
                        End If
                        If Not _Settings.RDPPassword = "" Then
                            RDPCtl.AdvancedSettings9.ClearTextPassword = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RDPPassword)
                        End If
                        Dim ocx As IMsRdpClientNonScriptable7 = RDPCtl.GetOcx()
                        ocx.EnableCredSspSupport = _Settings.EnableCredentialSSPSupport
                        ocx.AllowCredentialSaving = _Settings.AllowCredentialSaving
                        ocx.PromptForCredentials = _Settings.PromptForCredentials
                        ocx.PromptForCredsOnClient = _Settings.PromptForCredentialsAtClient
                        RDPCtl.AdvancedSettings9.SmartSizing = _Settings.EnableSmartResizing
                        RDPCtl.AdvancedSettings9.RedirectPrinters = _Settings.EnablePrinterRedirection
                        RDPCtl.AdvancedSettings9.RedirectClipboard = _Settings.EnableBidirectionalClipboard
                        RDPCtl.AdvancedSettings9.SmartSizing = _Settings.EnableSmartResizing
                        RDPCtl.AdvancedSettings9.GrabFocusOnConnect = _Settings.GrabFocusOnConnect
                        If _Settings.EnableSoundRedirection = False Then
                            RDPCtl.AdvancedSettings9.AudioRedirectionMode = 2
                        End If
                        RDPCtl.Connect()
                        RDPCtl.EndInit()
                    Else
                        LoadImage.Visible = False
                    End If
                End If
            Else
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Auto-Connect disabled.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                If Not ToolStripComboBox1.Text = "" Then
                    serverstr = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, ToolStripComboBox1.Text)
                    RDPCtl.Server = serverstr
                    If Not ToolStripTextBox1.Text = "" Then
                        RDPCtl.Domain = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, ToolStripTextBox1.Text)
                    End If
                    If Not ToolStripTextBox2.Text = "" Then
                        RDPCtl.UserName = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, ToolStripTextBox2.Text)
                    End If
                    If Not ToolStripTextBox3.Text = "" Or Not ToolStripTextBox3.Text = "<not set>" Then
                        RDPCtl.AdvancedSettings9.ClearTextPassword = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, ToolStripTextBox3.Text)
                    Else
                        If ToolStripTextBox3.Text = "<set>" Or ToolStripTextBox3.Text.StartsWith("%") Then
                            RDPCtl.AdvancedSettings9.ClearTextPassword = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RDPPassword)
                        End If
                    End If
                    Dim ocx As IMsRdpClientNonScriptable7 = RDPCtl.GetOcx()
                    ocx.EnableCredSspSupport = _Settings.EnableCredentialSSPSupport
                    ocx.AllowCredentialSaving = _Settings.AllowCredentialSaving
                    ocx.PromptForCredentials = _Settings.PromptForCredentials
                    ocx.PromptForCredsOnClient = _Settings.PromptForCredentialsAtClient
                    RDPCtl.AdvancedSettings9.RedirectPrinters = _Settings.EnablePrinterRedirection
                    RDPCtl.AdvancedSettings9.RedirectClipboard = _Settings.EnableBidirectionalClipboard
                    RDPCtl.AdvancedSettings9.SmartSizing = _Settings.EnableSmartResizing
                    RDPCtl.AdvancedSettings9.GrabFocusOnConnect = _Settings.GrabFocusOnConnect
                    If _Settings.EnableSoundRedirection = False Then
                        RDPCtl.AdvancedSettings9.AudioRedirectionMode = 2
                    End If
                    RDPCtl.Connect()
                    RDPCtl.EndInit()
                End If
            End If
            If _Settings.ShowServerNameInWindowTitle Then
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Set window title...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                _ParentInstance.CurrentWindowTitle = serverstr
                ParentForm.Text = serverstr
            End If
        Catch ex As Exception
            _ParentInstance.CurrentLogInstance.WriteLogEntry("RDP: Error", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
            LoadImage.Visible = False
            ConnectionClosed.Visible = True
        End Try
    End Function

    Public Sub LogonFailed() Handles RDPCtl.OnLogonError
        LoadImage.Visible = False
        ConnectionClosed.Visible = True
        ToolStrip1.Visible = True
        ToolStripButton1.Enabled = True
        ToolStripButton2.Enabled = False
        ToolStripButton3.Enabled = False
        _ParentInstance.CurrentLogInstance.WriteLogEntry("RDP: Login failed.", Me.GetType, LogEntryTypeEnum.Warning, LogEntryLevelEnum.Debug)
    End Sub

    Public Sub Disconnected() Handles RDPCtl.OnDisconnected
        LoadImage.Visible = False
        ConnectionClosed.Visible = True
        ToolStrip1.Visible = True
        ToolStripButton1.Enabled = True
        ToolStripButton2.Enabled = False
        ToolStripButton3.Enabled = False
        _ParentInstance.CurrentLogInstance.WriteLogEntry("RDP: Session closed.", Me.GetType, LogEntryTypeEnum.Warning, LogEntryLevelEnum.Debug)
    End Sub

    Public Sub LogonSucessful() Handles RDPCtl.OnLoginComplete
        ConnectionClosed.Visible = False
        LoadImage.Visible = False
        If _Settings.HideToolbarAfterConnect Then
            ToolStrip1.Visible = False
        End If
        ToolStripButton1.Enabled = False
        ToolStripButton2.Enabled = True
        ToolStripButton3.Enabled = True
        _ParentInstance.CurrentLogInstance.WriteLogEntry("RDP: Login successful.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
        If _Settings.ShowServerNameInWindowTitle Then
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Set window title...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            Dim serverstr As String
            serverstr = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.ServerName)
            _ParentInstance.CurrentWindowTitle = serverstr
            ParentForm.Text = serverstr
        End If
    End Sub

    Public Sub LeaveFullScreenMode() Handles RDPCtl.OnLeaveFullScreenMode
        ToolStripButton3.Enabled = True
        DoRemotescreenUpdate()
    End Sub

    Public Sub DoDisconnect()
        If Not IsNothing(RDPCtl) Then
            If RDPCtl.Connected Then
                RDPCtl.Disconnect()
            End If
        End If
    End Sub

    Public Sub DoRemotescreenUpdate()
        If Not IsNothing(RDPCtl) Then
            If RDPCtl.Connected Then
                RDPCtl.Reconnect(RDPBox.Width, RDPBox.Height)
            End If
        End If
    End Sub

    Public Sub DoFullscreen()
        If Not IsNothing(RDPCtl) Then
            If RDPCtl.Connected Then
                RDPCtl.Reconnect(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)
                RDPCtl.FullScreen = True
            End If
        End If
    End Sub

    Private Sub RaiseAutoConnect_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles RaiseAutoConnect.DoWork
        Threading.Thread.Sleep(100)
    End Sub

    Private Sub RaiseAutoConnect_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles RaiseAutoConnect.RunWorkerCompleted
        RefreshGUI()
    End Sub

    Private Sub ClientGUI_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        SetGUIState()
    End Sub

    Public Sub SetGUIState()
        If FirstStart Then
            If Not IsNothing(_Settings) Then
                If Not _Settings.InitialTitle = "" Then
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("Plugin load, set window title...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    Me.ParentForm.Text = _Settings.InitialTitle
                    _ParentInstance.CurrentWindowTitle = _Settings.InitialTitle
                End If

                ToolStrip1.Visible = _Settings.ShowToolbar
                ToolStripComboBox1.Text = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.ServerName)
                ToolStripTextBox1.Text = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RDPDomain)
                ToolStripTextBox2.Text = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RDPUsername)

                Dim pass As String
                pass = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RDPPassword)
                If Not pass = "" Then
                    ToolStripTextBox3.Text = "<set>"
                End If

                RaiseAutoConnect.RunWorkerAsync()
                FirstStart = False
            End If
        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        ToolStripButton1.Enabled = False
        RefreshGUI(False)
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        DoDisconnect()
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        ToolStripButton3.Enabled = False
        DoFullscreen()
    End Sub

    Private Sub ClientGUI_SizeChanged(sender As Object, e As EventArgs) Handles RDPBox.SizeChanged
        If Not IsNothing(_Settings) Then
            If Not IsNothing(RDPCtl) Then
                If RDPCtl.Connected Then
                    If _Settings.ResizeRemoteDesktopToLocalSize Then
                        _ParentInstance.CurrentLogInstance.WriteLogEntry("Resize detected, request remote session update...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        DoRemotescreenUpdate()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ConnectionClosed_MouseDoubleClick(sender As Object, e As Windows.Forms.MouseEventArgs) Handles ConnectionClosed.MouseDoubleClick
        _ParentInstance.CurrentLogInstance.WriteLogEntry("Reconnect to server...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
        RefreshGUI()
    End Sub
End Class
