'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Windows.Forms
Imports CSToolCryptHelper
Imports CSToolEnvironmentManager
Imports CSToolLogLib.LogSettings
Imports CSToolWindowSearcherHelper

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public EnvManager As New EnvironmentManager
    Public CurrentIPHostname As String = ""
    Private IsWindowIntegrated As Boolean = False
    Private MainProcessKilledByApp As Boolean = False

    Declare Function SetWindowPos Lib "user32" (ByVal hwnd As IntPtr, ByVal _
    hWndInsertAfter As Int32, ByVal x As Int32, ByVal y As Int32, ByVal cx As Int32, ByVal cy As Int32, ByVal wFlags As Int32) As Int32

    Declare Auto Function SetWindowLong Lib "user32" (ByVal hWnd As IntPtr, ByVal nIndex As Int32, ByVal dwNewLong As Int32) As IntPtr
    Declare Function SetParent Lib "user32.dll" (ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As IntPtr
    Declare Function ShowWindow Lib "user32" (ByVal hWnd As IntPtr, ByVal nCmdShow As Int32) As Boolean
    Const GWL_STYLE As Int32 = -16
    Const SW_MAXIMIZE As Int32 = 3

    Declare Function GetForegroundWindow Lib "user32" () As IntPtr

    Public Declare Function mouse_event Lib "user32" Alias "mouse_event" (ByVal _dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer) As Boolean
    Private Const MOUSEEVENTF_LEFTDOWN = &H2

    Public CurrHandle As IntPtr = 0
    Public WithEvents CurrProcess As Process = Nothing

    Delegate Sub SetFormTextCallback(ByVal FormCtl As Form, ByVal text As String)

    Public Sub SetText(ByVal FormCtl As Form, ByVal text As String)
        Try
            If FormCtl.InvokeRequired Then
                Dim d As SetFormTextCallback = New SetFormTextCallback(AddressOf SetText)
                Me.Invoke(d, New Object() {FormCtl, text})
            Else
                FormCtl.Text = text
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Function LoadAppInForm() As Boolean
        Try
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Starting application " & _Settings.Filename, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            If Not _Settings.Filename = "" Then
                IsWindowIntegrated = False
                MainProcessKilledByApp = False
                LoadImage.Visible = True

                If _Settings.UseExtendedStartInfo = True Then
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("Starting application using extended start info", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    Dim gg As New Process
                    Try
                        gg.StartInfo.FileName = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.Filename)
                    Catch ex As Exception
                    End Try
                    Try
                        gg.StartInfo.Arguments = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.ProcessArguments)
                    Catch ex As Exception
                    End Try
                    Try
                        gg.StartInfo.UseShellExecute = _Settings.UseShellExecute
                    Catch ex As Exception
                    End Try
                    Try
                        gg.StartInfo.WorkingDirectory = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.ProcessWorkingDir)
                    Catch ex As Exception
                    End Try
                    Try
                        gg.StartInfo.WindowStyle = _Settings.ProcessWindowStyle
                    Catch ex As Exception
                    End Try
                    Try
                        gg.StartInfo.Domain = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.ProcessDomain)
                    Catch ex As Exception
                    End Try
                    Try
                        gg.StartInfo.UserName = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.ProcessUsername)
                    Catch ex As Exception
                    End Try
                    Try
                        gg.StartInfo.LoadUserProfile = _Settings.LoadUserProfile
                    Catch ex As Exception
                    End Try
                    Try
                        Dim tt As New CredentialHandler
                        gg.StartInfo.Password = tt.ConvertStringInSecureString(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.ProcessUsernamePassword))
                    Catch ex As Exception
                    End Try

                    CurrProcess = gg
                    CurrProcess.Start()
                Else
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("Starting application using normal start info", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    CurrProcess = Process.Start(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.Filename))
                End If
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Starting application successful.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

                If _Settings.ManualIntegration = True Then
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("Waiting for user-driven-integration...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    Button1.Visible = True
                    Return True
                End If

                If Not _Settings.BeforeIntegrationDelay = 0 Then
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("Waiting... (" & _Settings.BeforeIntegrationDelay & ")", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    BackgroundWorker1.RunWorkerAsync(_Settings.BeforeIntegrationDelay)
                    Do
                        Threading.Thread.Sleep(50)
                        Application.DoEvents()
                    Loop While BackgroundWorker1.IsBusy
                End If

                If _Settings.WaitForInputIdle Then
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("Waiting for application is ready and in idle...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    CurrProcess.WaitForInputIdle()
                End If

                IsWindowIntegrated = ApplyIntegration(CurrProcess)
                If IsWindowIntegrated Then
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("Application integrated.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                Else
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("Error: Application NOT integrated.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug)
                End If
                CurrHandle = CurrProcess.MainWindowHandle
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Application handle: " & CurrHandle.ToInt32, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

                LoadImage.Visible = False

                Return True
            Else
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Error: Application NOT integrated.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug)
                LoadImage.Visible = False
                Return False
            End If
        Catch ex As Exception
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Error: Application NOT integrated.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
            Return False
        End Try
    End Function

    Public Function ApplyIntegration(ByVal ExProcess As Process, Optional OnlyWindowResize As Boolean = False) As Boolean
        Try
            If OnlyWindowResize = False Then
                If _Settings.ActivateProcess = True Then
                    AppActivate(_Settings.WindowText)
                End If

                If _Settings.RescanForProcessHandle = True Then
                    Try
                        Dim jj As Process
                        jj = Process.GetProcessesByName(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RescanForProcessHandleProcessName))(0)
                        If Not jj.ProcessName = "" Then
                            ExProcess = jj
                        End If
                    Catch ex As Exception
                        If _Settings.ExitProcessIfIntegrationFails Then
                            MainProcessKilledByApp = True
                            ExProcess.Kill()
                        End If
                    End Try
                End If
            End If

            If _Settings.UseCurrentActiveWindowHandle = False Then
                SetParent(ExProcess.MainWindowHandle, Panel1.Handle)
                SetWindowLong(ExProcess.MainWindowHandle, GWL_STYLE, 0)
                ShowWindow(ExProcess.MainWindowHandle, SW_MAXIMIZE)
            Else
                Dim hnd As IntPtr = GetForegroundWindow
                SetParent(hnd, Panel1.Handle)

                SetWindowLong(hnd, GWL_STYLE, 0)
                ShowWindow(hnd, SW_MAXIMIZE)
            End If

            If Not IsNothing(ExProcess) Then
                If _Settings.ShowMainWindowTitleInWindowTitle Then
                    _ParentInstance.CurrentWindowTitle = ExProcess.MainWindowTitle
                    SetText(Me.ParentForm, ExProcess.MainWindowTitle)
                Else
                    _ParentInstance.CurrentWindowTitle = _Settings.InitialWindowTitle
                    SetText(Me.ParentForm, _Settings.InitialWindowTitle)
                End If
            End If

            Return True
        Catch ex As Exception
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Error: Application integration", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
            Return False
        End Try
    End Function
    Public Function ManualIntegration()
        Try
            IsWindowIntegrated = False

            If _Settings.SearchWindow = True Then
                Dim gg As New WindowSearcher
                For Each item As String In gg.GetWindowTitles()
                    If item.Contains(_Settings.WindowText) Then
                        If _Settings.WaitForInputIdle Then
                            CurrProcess.WaitForInputIdle()
                        End If
                        ApplyIntegration(CurrProcess)
                        Exit For
                    End If
                Next
                If _Settings.ExitProcessIfIntegrationFails Then
                    If Not IsNothing(CurrProcess) Then
                        CurrProcess.Kill()
                    End If
                End If
            Else
                IsWindowIntegrated = ApplyIntegration(CurrProcess)
            End If
            Panel1.BackgroundImage = Nothing
            Button1.Visible = False

            Return True
        Catch ex As Exception
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Error: Application integration", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
            Return False
        End Try
    End Function

    Public Sub HandleAppExit() Handles CurrProcess.Exited
        If Not MainProcessKilledByApp Then
            If _Settings.RestartApplication Then
                IsWindowIntegrated = False
                RefreshGUI()
            End If
        End If
    End Sub

    Public Sub UnloadPlugin()
        If Not IsNothing(CurrProcess) Then
            If CurrProcess.HasExited = False Then
                MainProcessKilledByApp = True
                CurrProcess.CloseMainWindow()
                If CurrProcess.HasExited = False Then
                    CurrProcess.Kill()
                End If
            End If
        End If
    End Sub

    Public Sub LoadPlugin()

    End Sub

    Public Sub RaiseAction(ByVal IPOrHostname As String, Optional ByVal IsRefresh As Boolean = False)
        CurrentIPHostname = IPOrHostname
    End Sub
    Public Sub RefreshGUI(Optional FiredFromWindowEvent As Boolean = True)
        Try
            If Not IsNothing(CurrProcess) Then
                If FiredFromWindowEvent Then
                    If IsWindowIntegrated = False Then
                        If CurrProcess.HasExited Or CurrProcess.Handle.ToInt32 = 0 Then
                            LoadAppInForm()
                        Else
                            ApplyIntegration(CurrProcess)
                        End If
                    Else
                        ApplyIntegration(CurrProcess)
                    End If
                    If Not CurrHandle = 0 And IsWindowIntegrated = True Then
                        SetParent(CurrHandle, Panel1.Handle)
                        SetWindowLong(CurrHandle, GWL_STYLE, 0)
                        ShowWindow(CurrHandle, SW_MAXIMIZE)
                    End If
                Else
                    IsWindowIntegrated = False
                    LoadAppInForm()
                End If
            End If
        Catch ex As Exception
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Error: Application integration", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            Dim time As Integer
            time = e.Argument
            Threading.Thread.Sleep(time)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ManualIntegration()
    End Sub

    Private Sub ClientGUI_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        If IsWindowIntegrated Then
            RefreshGUI()
        End If
    End Sub

    Public Sub ReloadWindowIntegration()
        If IsWindowIntegrated Then
            ApplyIntegration(CurrProcess, True)
        End If
    End Sub

    Private Sub ClientGUI_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        ReloadWindowIntegration()
    End Sub

    Private Sub InitWait_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles InitWait.DoWork
        Threading.Thread.Sleep(_Settings.FilenameStartDelay)
    End Sub

    Private Sub InitWait_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles InitWait.RunWorkerCompleted
        If Not IsNothing(_Settings) Then
            If IsWindowIntegrated = False Then
                LoadAppInForm()
            End If
        End If
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitWait.RunWorkerAsync()
    End Sub

    Private Sub ClientGUI_Validated(sender As Object, e As EventArgs) Handles MyBase.Validated
        RefreshGUI()
    End Sub
End Class
