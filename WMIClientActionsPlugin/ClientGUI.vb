'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports CSToolEnvironmentManager
Imports CSToolLogLib.LogSettings
Imports CSToolMessageBoxEx
Imports CSToolSendKeysHelper
Imports CSToolWindowSearcherHelper
Imports CSToolWMIHelper

Public Class ClientGUI
    Public _Settings As Settings
    Public WMIHandler As New WMIHandler
    Public EnvManager As New EnvironmentManager
    Public CurrentIPHostname As String = ""
    Public CurrentVNCViewerPID As Integer = 0
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        _ParentInstance.CurrentLogInstance.WriteLogEntry("Shutdown host " & CurrentIPHostname, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
        If Not RunTaskAsync.IsBusy Then
            If Not _Settings.ShutdownWMIExecWarningMessage = "" Then
                Dim msgres As MsgBoxResult
                msgres = MsgBox(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.ShutdownWMIExecWarningMessage), MsgBoxStyle.YesNo, "Warning")
                If msgres = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If

            Dim argslist As New List(Of String)
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.ShutdownWMIExecScope))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.ShutdownWMIExecQuery))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.ShutdownWMIExecMethod))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.ShutdownWMIExecArgumentValue))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.ShutdownWMIExecArgumentData))
            argslist.Add(_Settings.UseCustomWMIConnectionOptions)
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority))
            argslist.Add(_Settings.CustomWMIConnectionOptionsAuthentication)
            argslist.Add(_Settings.CustomWMIConnectionOptionsImpersonation)

            RunTaskAsync.RunWorkerAsync(argslist)
        End If
    End Sub

    Sub RaiseAction(ByVal IPOrHostname As String, Optional ByVal IsRefresh As Boolean = False)
        CurrentIPHostname = IPOrHostname
    End Sub

    Sub RefreshGUI()
        LoadCustomItems(CustomItemsContext)
    End Sub

    Private Sub RunTaskAsync_DoWork(sender As Object, e As DoWorkEventArgs) Handles RunTaskAsync.DoWork
        e.Result = WMIHandler.ExecWMIMethod(CurrentIPHostname, e.Argument(0), e.Argument(1), e.Argument(2), e.Argument(3), e.Argument(4), e.Argument(5), e.Argument(6), e.Argument(7), e.Argument(8), e.Argument(9), e.Argument(10))
    End Sub

    Private Sub RunTaskAsync_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles RunTaskAsync.RunWorkerCompleted
        MsgBox("Successful.")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        _ParentInstance.CurrentLogInstance.WriteLogEntry("Restart host " & CurrentIPHostname, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
        If Not RunTaskAsync.IsBusy Then
            If Not _Settings.RestartWMIExecWarningMessage = "" Then
                Dim msgres As MsgBoxResult
                msgres = MsgBox(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RestartWMIExecWarningMessage), MsgBoxStyle.YesNo, "Warning")
                If msgres = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If

            Dim argslist As New List(Of String)
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RestartWMIExecScope))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RestartWMIExecQuery))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RestartWMIExecMethod))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RestartWMIExecArgumentValue))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RestartWMIExecArgumentData))
            argslist.Add(_Settings.UseCustomWMIConnectionOptions)
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority))
            argslist.Add(_Settings.CustomWMIConnectionOptionsAuthentication)
            argslist.Add(_Settings.CustomWMIConnectionOptionsImpersonation)

            RunTaskAsync.RunWorkerAsync(argslist)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        _ParentInstance.CurrentLogInstance.WriteLogEntry("Logoff user on host " & CurrentIPHostname, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
        If Not RunTaskAsync.IsBusy Then
            If Not _Settings.LogoffWMIExecWarningMessage = "" Then
                Dim msgres As MsgBoxResult
                msgres = MsgBox(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.LogoffWMIExecWarningMessage), MsgBoxStyle.YesNo, "Warning")
                If msgres = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If

            Dim argslist As New List(Of String)
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.LogoffWMIExecScope))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.LogoffWMIExecQuery))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.LogoffWMIExecMethod))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.LogoffWMIExecArgumentValue))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.LogoffWMIExecArgumentData))
            argslist.Add(_Settings.UseCustomWMIConnectionOptions)
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority))
            argslist.Add(_Settings.CustomWMIConnectionOptionsAuthentication)
            argslist.Add(_Settings.CustomWMIConnectionOptionsImpersonation)

            RunTaskAsync.RunWorkerAsync(argslist)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        _ParentInstance.CurrentLogInstance.WriteLogEntry("Set hibernation on host " & CurrentIPHostname, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
        If Not RunTaskAsync.IsBusy Then
            If Not _Settings.StandbyWMIExecWarningMessage = "" Then
                Dim msgres As MsgBoxResult
                msgres = MsgBox(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.StandbyWMIExecWarningMessage), MsgBoxStyle.YesNo, "Warning")
                If msgres = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If

            Dim argslist As New List(Of String)
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.StandbyWMIExecScope))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.StandbyWMIExecQuery))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.StandbyWMIExecMethod))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.StandbyWMIExecArgumentValue))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.StandbyWMIExecArgumentData))
            argslist.Add(_Settings.UseCustomWMIConnectionOptions)
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority))
            argslist.Add(_Settings.CustomWMIConnectionOptionsAuthentication)
            argslist.Add(_Settings.CustomWMIConnectionOptionsImpersonation)

            RunTaskAsync.RunWorkerAsync(argslist)
        End If
    End Sub

    Public Sub StartVNCViewerAndRaiseSendKeys()
        Try
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Raising VNC-Viewer start...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

            Try
                If _Settings.DetectVNCViewerSuppressMultipleInstances = True Then
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("Check, if VNC-Viewer-Instance is already active...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    Dim jj As Process
                    jj = Process.GetProcessById(CurrentVNCViewerPID)
                    If jj.HasExited = False Then
                        _ParentInstance.CurrentLogInstance.WriteLogEntry("VNC-Viewer-Instance is already active, exit.", Me.GetType, LogEntryTypeEnum.Warning, LogEntryLevelEnum.Debug)
                        Exit Sub
                    End If
                End If
            Catch ex As Exception
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Start VNC-Viewer: Error.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug)
            End Try

            Dim pid As Integer = 0
            If EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.StartVNCViewerCustomParameters) = "" Then
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Starting VNC-Viewer without custom parameters..." & CurrentVNCViewerPID, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                pid = Shell(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.StartVNCViewerShellExec), AppWinStyle.NormalFocus)
            Else
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Starting VNC-Viewer with custom parameters..." & CurrentVNCViewerPID, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                pid = Shell(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.StartVNCViewerShellExec) & " " & EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.StartVNCViewerCustomParameters), AppWinStyle.NormalFocus)
            End If

            CurrentVNCViewerPID = pid
            _ParentInstance.CurrentLogInstance.WriteLogEntry("VNC-Viewer-Instance Process PID :" & CurrentVNCViewerPID, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

            If _Settings.AutoFillUltraVNCDomainAuth = True Then
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Use UltraVNC domain authentification...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                If _Settings.DetectVNCViewerWindow = True Then
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("Starting detection of VNC-Window...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    Dim detected As Boolean = False
                    Dim failed As Boolean = False
                    Dim timeoutcnt As Integer = 0
                    Dim WindowSearcherObj As New WindowSearcher
                    Do While (detected = False) And (failed = False) And (timeoutcnt < _Settings.DetectVNCViewerWindowTimeout)
                        For Each item As String In WindowSearcherObj.GetWindowTitles
                            If item.Contains(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.DetectVNCViewerWindowCredentialsTitle)) Then
                                detected = True
                            End If
                            If item.Contains(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.DetectVNCViewerWindowFailedTitle)) Then
                                failed = True
                            End If
                        Next
                        Threading.Thread.Sleep(_Settings.DetectVNCViewerWindowDelay)
                        timeoutcnt += _Settings.DetectVNCViewerWindowDelay
                        _ParentInstance.CurrentLogInstance.WriteLogEntry("Waiting for VNC-Viewer window... (" & timeoutcnt & ")", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    Loop

                    If failed = True Then
                        _ParentInstance.CurrentLogInstance.WriteLogEntry("VNC-Viewer connection error.", Me.GetType, LogEntryTypeEnum.Warning, LogEntryLevelEnum.Debug)
                        If _Settings.DetectVNCViewerWindowKillVNCOnFail = True Then
                            Try
                                Dim vncprocess As Process
                                vncprocess = Process.GetProcessById(pid)
                                vncprocess.Kill()
                            Catch ex As Exception
                            End Try
                        End If
                    Else
                        _ParentInstance.CurrentLogInstance.WriteLogEntry("VNC-Viewer window successful detected.", Me.GetType, LogEntryTypeEnum.Warning, LogEntryLevelEnum.Debug)
                    End If

                    If detected = True Then
                        'VNC-Fenster fokusieren, damit die Eingabe automatisch simuliert werden kann:
                        If _Settings.DetectVNCViewerWindowSetForeground = True Then
                            _ParentInstance.CurrentLogInstance.WriteLogEntry("Set VNC-Viewer window to active...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                            Try
                                Dim vncprocess As Process
                                vncprocess = Process.GetProcessById(pid)
                                WindowSearcher.SetForegroundWindow(vncprocess.MainWindowHandle)
                            Catch ex As Exception
                            End Try
                        End If

                        'Jetzt autom. alle Textfelder füllen:
                        Threading.Thread.Sleep(_Settings.VNCViewerAutoFillDelayAfterWindowDetected)
                        _ParentInstance.CurrentLogInstance.WriteLogEntry("Auto-Type username and password...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Advanced)
                        Dim SendKeysHelperObj As New SendKeysHelper
                        SendKeysHelperObj.SimulateText(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.VNCViewerUsername),
                                                       EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.VNCViewerPassword),
                                                       EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.VNCViewerAutoFillCharTimeout),
                                                       EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.VNCViewerAutoFillCtlTimeout),
                                                       EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.VNCViewerAutoFillSwitchCommand),
                                                       EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.VNCViewerAutoFillSubmitCommand))

                        'Die Eingabe wurde nun vorgenommen, deshalb kann die Mausüberwachung übersprungen werden.
                    End If
                Else
                    ' Wichtig! Zunächst zurücksetzen
                    Call SendKeysHelper.GetAsyncKeyState(SendKeysHelper.VK_LBUTTON)
                    'Start mouse click detection
                    autofill.Start()
                End If
            End If
        Catch ex As Exception
            _ParentInstance.CurrentLogInstance.WriteLogEntry("VNC-Viewer: Error.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug)
        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        StartVNCViewerAndRaiseSendKeys()
    End Sub

    Private Sub autofill_Tick(sender As Object, e As EventArgs) Handles autofill.Tick
        Try
            autofill.Stop()
            If SendKeysHelper.GetAsyncKeyState(SendKeysHelper.VK_RBUTTON) Then
                Exit Sub
            End If
            If SendKeysHelper.GetAsyncKeyState(SendKeysHelper.VK_LBUTTON) Then
                Threading.Thread.Sleep(_Settings.VNCViewerAutoFillDelayAfterWindowDetected)
                Dim args As New List(Of String)

                _ParentInstance.CurrentLogInstance.WriteLogEntry("Auto-Type username and password...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Advanced)
                Dim SendKeysHelperObj As New SendKeysHelper
                SendKeysHelperObj.SimulateText(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.VNCViewerUsername),
                                                       EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.VNCViewerPassword),
                                                       EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.VNCViewerAutoFillCharTimeout),
                                                       EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.VNCViewerAutoFillCtlTimeout),
                                                       EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.VNCViewerAutoFillSwitchCommand),
                                                       EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.VNCViewerAutoFillSubmitCommand))
            Else
                autofill.Start()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Function LoadCustomItems(ByVal ContextMenuStripCtl As ContextMenuStrip) As Boolean
        Try
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Load custom actions...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            ContextMenuStripCtl.Items.Clear()

            If Not _Settings.CustomItemsCollection.Count = 0 Then
                Button5.Enabled = True
                For Each itm As CustomItem In _Settings.CustomItemsCollection
                    Try
                        Dim rr As ToolStripItem
                        rr = ContextMenuStripCtl.Items.Add(itm.Name)
                        If Not itm.IconPath = "" Then
                            Try
                                Dim tt As Image
                                tt = Image.FromFile(itm.IconPath)
                                rr.Image = tt
                            Catch ex As Exception
                            End Try
                        End If

                        rr.Tag = itm
                        AddHandler rr.Click, AddressOf ClickCustomItemEntry
                    Catch ex As Exception
                    End Try
                Next
            Else
                Button5.Enabled = False
            End If

            _ParentInstance.CurrentLogInstance.WriteLogEntry("Load custom actions successful.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

            Return True
        Catch ex As Exception
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Load custom actions failed.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug)
            Return False
        End Try
    End Function

    Public Sub ClickCustomItemEntry(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Try
                Call SendKeysHelper.GetAsyncKeyState(SendKeysHelper.VK_LBUTTON)
            Catch ex As Exception
            End Try

            If Not RaiseCustomActionAsync.IsBusy = True Then
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Run custom action", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Advanced)
                RaiseCustomActionAsync.RunWorkerAsync(sender.tag)
            Else
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Custom action is currently pending", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Essential)
                MsgBox("Custom action is currently pending, please wait!", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If Not _Settings.CustomItemsCollection.Count = 0 Then
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Custom items found, show context menu.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Advanced)
            CustomItemsContext.Show(MousePosition)
        Else
            _ParentInstance.CurrentLogInstance.WriteLogEntry("No custom items, disable context menu.", Me.GetType, LogEntryTypeEnum.Warning, LogEntryLevelEnum.Advanced)
        End If
    End Sub

    Private Sub RaiseCustomActionAsync_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles RaiseCustomActionAsync.DoWork
        Try
            Dim arg1 As CustomItem
            arg1 = e.Argument

            RaiseCustomItem(arg1)
            e.Result = arg1
        Catch ex As Exception
        End Try
    End Sub

    Public Function RaiseCustomItem(ByVal CustomItemx As CustomItem) As Boolean
        Try
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Custom item name: " & CustomItemx.Name, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Custom item command line: " & CustomItemx.Command, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            Dim isok = False

            If CustomItemx.WarningMessage = "" Then
                isok = True
            Else
                If _Settings.ShowWarningOnCustomActions = True Then
                    Dim resmsg As MsgBoxResult
                    resmsg = MsgBox(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, CustomItemx.WarningMessage), MsgBoxStyle.YesNo)
                    If resmsg = MsgBoxResult.Yes Then
                        isok = True
                    End If
                Else
                    isok = True
                End If
            End If

            If isok = True Then
                If CustomItemx.ReturnResult = True Then
                    Dim args As Array
                    args = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, CustomItemx.Command).Split(" ")
                    Dim filename As String = args(0)
                    Dim kk As New Process
                    kk.StartInfo.FileName = filename
                    Dim allargs As String = ""
                    For index = 1 To args.Length - 1
                        allargs += " " & args(index)
                    Next
                    kk.StartInfo.Arguments = allargs
                    kk.StartInfo.UseShellExecute = CustomItemx.UseShellExecute
                    kk.StartInfo.CreateNoWindow = CustomItemx.CreateNoWindow
                    kk.StartInfo.ErrorDialog = CustomItemx.ErrorDialog
                    kk.StartInfo.LoadUserProfile = CustomItemx.LoadUserProfile
                    kk.StartInfo.RedirectStandardOutput = True
                    kk.Start()
                    kk.WaitForExit(CustomItemx.CustomItemExecTimeout)
                    Dim result As String
                    result = kk.StandardOutput.ReadToEnd

                    Dim MsgBoxExObj As New MessageBoxHandler
                    MsgBoxExObj.Show(result, MsgBoxStyle.Information)
                Else
                    If Not CustomItemx.RunAsUsername = "" Then
                        Dim args As Array
                        args = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, CustomItemx.Command).Split(" ")
                        Dim filename As String = args(0)
                        Dim kk As New Process
                        kk.StartInfo.FileName = filename
                        Dim allargs As String = ""
                        For index = 1 To args.Length - 1
                            allargs += " " & args(index)
                        Next
                        kk.StartInfo.Arguments = allargs
                        kk.StartInfo.UseShellExecute = CustomItemx.UseShellExecute
                        kk.StartInfo.CreateNoWindow = CustomItemx.CreateNoWindow
                        kk.StartInfo.ErrorDialog = CustomItemx.ErrorDialog
                        kk.StartInfo.LoadUserProfile = CustomItemx.LoadUserProfile
                        kk.StartInfo.UserName = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, CustomItemx.RunAsUsername)
                        kk.StartInfo.PasswordInClearText = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, CustomItemx.RunAsPassword)
                    Else
                        Shell(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, CustomItemx.Command), AppWinStyle.NormalFocus, CustomItemx.WaitForExit, CustomItemx.CustomItemExecTimeout)
                    End If
                End If
            End If

            _ParentInstance.CurrentLogInstance.WriteLogEntry("Custom item name: " & CustomItemx.Name & " successful processed.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

            Return True
        Catch ex As Exception
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Custom item name: " & CustomItemx.Name & " processing failed.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
            Return False
        End Try
    End Function

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WMIHandler._LogInstanceHandler = _ParentInstance.CurrentLogInstance
        RefreshGUI()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        _ParentInstance.CurrentLogInstance.WriteLogEntry("Activate remote service process on host " & CurrentIPHostname, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
        If Not RunTaskAsync.IsBusy Then
            If Not _Settings.StartVNCServiceWMIExecWarningMessage = "" Then
                Dim msgres As MsgBoxResult
                msgres = MsgBox(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.StartVNCServiceWMIExecWarningMessage), MsgBoxStyle.YesNo, "Warning")
                If msgres = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If

            Dim argslist As New List(Of String)
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.StartVNCServiceWMIExecScope))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.StartVNCServiceWMIExecQuery))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.StartVNCServiceWMIExecMethod))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.StartVNCServiceWMIExecArgumentValue))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.StartVNCServiceWMIExecArgumentData))
            argslist.Add(_Settings.UseCustomWMIConnectionOptions)
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword))
            argslist.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority))
            argslist.Add(_Settings.CustomWMIConnectionOptionsAuthentication)
            argslist.Add(_Settings.CustomWMIConnectionOptionsImpersonation)

            RunTaskAsync.RunWorkerAsync(argslist)
        End If
    End Sub

    Private Sub KillVNCViewerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KillVNCViewerToolStripMenuItem.Click
        Try
            Dim proc As Process
            proc = Process.GetProcessById(CurrentVNCViewerPID)

            proc.Kill()
        Catch ex As Exception
        End Try
    End Sub
End Class
