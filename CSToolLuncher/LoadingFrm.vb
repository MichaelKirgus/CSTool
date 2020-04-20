'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolApplicationSettingsLib
Imports CSToolApplicationSettingsManager
Imports CSToolLauncherLib
Imports CSToolSyncLib

Public Class LoadingFrm
    Public IsElevated As Boolean = False
    Public ApplicationSettingsFile As String = "AppSettings.xml"
    Public AppSettingsHandler As New ApplicationSettingsManager
    Public AppSettingsObj As New ApplicationSettings
    Public LauncherHelperInstance As New LauncherLib
    Private Delegate Sub SetLabelTextDelegate(ByVal LabelCtl As Label, ByVal TextStr As String)

    Private Sub SetLabelText(ByVal LabelCtl As Label, ByVal TextStr As String)
        If LabelCtl.InvokeRequired Then
            Dim ClearListViewObj As New SetLabelTextDelegate(AddressOf SetLabelText)
            LabelCtl.Invoke(ClearListViewObj, New Object() {LabelCtl, TextStr})
        Else
            LabelCtl.Text = TextStr
        End If
    End Sub

    Private Sub StartLoadingFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AppVersionLbl.Text = My.Application.Info.Version.ToString
        LoadingState.RunWorkerAsync()
    End Sub

    Public Function CheckForCommandLineArgsAppBase(ByVal CmdArgs As String()) As Boolean
        Try
            Dim arglist As List(Of String)
            arglist = CmdArgs.ToList

            For ind = 0 To arglist.Count - 1
                If arglist(ind).ToLower = "/elevated" Then
                    IsElevated = True
                End If

                If arglist(ind).ToLower = "/appsettings" Then
                    ApplicationSettingsFile = arglist(ind + 1)
                End If

                If arglist(ind).ToLower = "/noshortcut" Then
                    AppSettingsObj.LauncherCreateMainApplicationShortcutOnDesktop = False
                End If

                If arglist(ind).ToLower = "/shortcut" Then
                    AppSettingsObj.LauncherCreateMainApplicationShortcutOnDesktop = True
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ConvertCmdArgsToString(ByVal CmdArgs As String()) As String
        Try
            Dim newstr As String = ""
            Dim arglist As List(Of String)
            arglist = CmdArgs.ToList

            If arglist.Count > 1 Then
                For ind = 1 To arglist.Count - 1
                    If Not arglist(ind) = " " Or Not arglist(ind) = "" Then
                        newstr += arglist(ind) & " "
                    End If
                Next
            End If

            newstr = newstr.Replace("/elevated", "")
            If newstr.EndsWith(" ") Then
                newstr = newstr.Substring(0, newstr.Length - 1)
            End If

            Return newstr
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Public Function LoadAppSettings() As Boolean
        Try
            AppSettingsObj = AppSettingsHandler.LoadSettings(ApplicationSettingsFile)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function RunAppElevated() As Boolean
        Try
            Dim elevatedproccess As New Process
            elevatedproccess.StartInfo.FileName = Application.ExecutablePath
            elevatedproccess.StartInfo.Arguments = "/elevated " & ConvertCmdArgsToString(Environment.GetCommandLineArgs)
            elevatedproccess.StartInfo.Verb = "runas"
            elevatedproccess.StartInfo.UseShellExecute = True
            If elevatedproccess.Start() Then
                elevatedproccess.WaitForExit()
                Threading.Thread.Sleep(500)
            Else
                Return False
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub UntilFinishShowStatus(ByVal SyncHandlerInstance As SyncLib)
        Do While SyncHandlerInstance.IsAsyncTaskRunning
            SetLabelText(LoadingStateLbl, SyncHandlerInstance.CurrentTask & ": " & SyncHandlerInstance.CurrentFile)
            Threading.Thread.Sleep(10)
        Loop
    End Sub

    Public Function SyncNeeded(Optional ByVal OnlyCheck As Boolean = False) As Boolean
        Try
            If Not OnlyCheck Then
                If AppSettingsObj.LauncherSyncNeedsElevation And IsElevated = False Then
                    Select Case HandleUserInteraction()
                        Case 1
                            StartMainAppFromSourceNonElevated()
                        Case 2
                            StartMainAppFromShareNonElevated()
                    End Select
                End If
            End If

            SetLabelText(LoadingStateLbl, "Initialize syncing...")
            Dim SyncHandler As New SyncLib
            SyncHandler.LogHandler.LogSettings = AppSettingsObj.LauncherLogSettings
            SyncHandler.LogHandler.InitLogSystem()
            SetLabelText(LoadingStateLbl, "Update main application files...")
            SyncHandler.StartSyncAsync(Application.StartupPath, Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath), False, OnlyCheck)
            UntilFinishShowStatus(SyncHandler)
            If SyncHandler.FilesOrDirsChanged Then
                SyncHandler.LogHandler.CloseStreams()
                SyncNeeded(False)
                Return False
            End If
            SyncHandler.StartSyncAsync(Application.StartupPath & "\locales", Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\locales", True, OnlyCheck)
            UntilFinishShowStatus(SyncHandler)
            If SyncHandler.FilesOrDirsChanged Then
                SyncHandler.LogHandler.CloseStreams()
                SyncNeeded(False)
                Return False
            End If
            SyncHandler.StartSyncAsync(Application.StartupPath & "\swiftshader", Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\swiftshader", True, OnlyCheck)
            UntilFinishShowStatus(SyncHandler)
            If SyncHandler.FilesOrDirsChanged Then
                SyncHandler.LogHandler.CloseStreams()
                SyncNeeded(False)
                Return False
            End If
            SyncHandler.StartSyncAsync(Application.StartupPath & "\Firefox64", Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\Firefox64", True, OnlyCheck)
            UntilFinishShowStatus(SyncHandler)
            If SyncHandler.FilesOrDirsChanged Then
                SyncHandler.LogHandler.CloseStreams()
                SyncNeeded(False)
                Return False
            End If
            SetLabelText(LoadingStateLbl, "Update credential plugins...")
            SyncHandler.StartSyncAsync(Application.StartupPath & "\" & AppSettingsObj.CredentialPluginDir, Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\" & AppSettingsObj.CredentialPluginDir, True, OnlyCheck)
            UntilFinishShowStatus(SyncHandler)
            If SyncHandler.FilesOrDirsChanged Then
                SyncHandler.LogHandler.CloseStreams()
                SyncNeeded(False)
                Return False
            End If
            SetLabelText(LoadingStateLbl, "Update environment plugins...")
            SyncHandler.StartSyncAsync(Application.StartupPath & "\" & AppSettingsObj.EnvironmentPluginDir, Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\" & AppSettingsObj.EnvironmentPluginDir, True, OnlyCheck)
            UntilFinishShowStatus(SyncHandler)
            If SyncHandler.FilesOrDirsChanged Then
                SyncHandler.LogHandler.CloseStreams()
                SyncNeeded(False)
                Return False
            End If
            SetLabelText(LoadingStateLbl, "Update GUI plugins...")
            SyncHandler.StartSyncAsync(Application.StartupPath & "\" & AppSettingsObj.GUIPluginDir, Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\" & AppSettingsObj.GUIPluginDir, True, OnlyCheck)
            UntilFinishShowStatus(SyncHandler)
            If SyncHandler.FilesOrDirsChanged Then
                SyncHandler.LogHandler.CloseStreams()
                SyncNeeded(False)
                Return False
            End If
            If Not AppSettingsObj.LauncherIncludeFolderCollection.Count = 0 Then
                SetLabelText(LoadingStateLbl, "Update additional files...")
                For index = 0 To AppSettingsObj.LauncherIncludeFolderCollection.Count - 1
                    SyncHandler.StartSyncAsync(Application.StartupPath & "\" & AppSettingsObj.LauncherIncludeFolderCollection(index).FolderName, Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\" & AppSettingsObj.LauncherIncludeFolderCollection(index).FolderName, AppSettingsObj.LauncherIncludeFolderCollection(index).Recursive, OnlyCheck)
                    UntilFinishShowStatus(SyncHandler)
                Next
                If SyncHandler.FilesOrDirsChanged Then
                    SyncHandler.LogHandler.CloseStreams()
                    SyncNeeded(False)
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function HandleUserInteraction() As Integer
        Try
            Dim userdlg As New Form1
            Dim targetdir As String
            targetdir = Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath)
            If Not IO.Directory.Exists(targetdir) Then
                userdlg.Button1.Enabled = False
            End If

            userdlg.ShowDialog()

            If userdlg.DialogResult = MsgBoxResult.Ignore Then
                Return 1
            End If
            If userdlg.DialogResult = MsgBoxResult.No Then
                Return 2
            End If

            If userdlg.DialogResult = MsgBoxResult.Retry Then
                SetLabelText(LoadingStateLbl, "Start launcher elevated...")
                If RunAppElevated() = False Then
                    Return -1
                Else
                    Return 3
                End If
            End If

            Return -1
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Public Function StartMainAppFromShareNonElevated() As Boolean
        Try
            Dim mainappargs As String
            Dim mainapp As New Process
            mainapp.StartInfo.FileName = Application.StartupPath & "\CSTool.exe"
            mainappargs = ConvertCmdArgsToString(Environment.GetCommandLineArgs) & " /fromlauncher"
            mainapp.StartInfo.Arguments = mainappargs
            mainapp.StartInfo.WorkingDirectory = Application.StartupPath
            mainapp.Start()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function StartMainAppFromSourceNonElevated() As Boolean
        Try
            Dim mainappargs As String = ""
            Dim mainapp As New Process
            mainapp.StartInfo.FileName = Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\CSTool.exe"
            mainapp.StartInfo.WorkingDirectory = Application.StartupPath

            Select Case AppSettingsObj.LauncherMainAppStartMode
                Case MainAppCommandlLineArgumentsMode.OnlyRunMainAppWithoutLocalWorkingDir
                    mainappargs = ConvertCmdArgsToString(Environment.GetCommandLineArgs) & " /fromlauncher " & AppSettingsObj.LauncherAdditionalMainAppArguments
                Case MainAppCommandlLineArgumentsMode.OnlyRunMainAppWithLocalWorkingDir
                    mainappargs = ConvertCmdArgsToString(Environment.GetCommandLineArgs) & " /fromlauncher " & AppSettingsObj.LauncherAdditionalMainAppArguments
                    mainapp.StartInfo.WorkingDirectory = Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath)
                Case MainAppCommandlLineArgumentsMode.RunMainAppWithLocalWorkingDirAndLocalPluginFolders
                    mainappargs = ConvertCmdArgsToString(Environment.GetCommandLineArgs) & " /fromlauncher" & " " & GenerateCommandLineMainAppLocalFolders() & " " & AppSettingsObj.LauncherAdditionalMainAppArguments
                    mainapp.StartInfo.WorkingDirectory = Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath)
                Case MainAppCommandlLineArgumentsMode.RunMainAppWithLocalWorkingDirAndSetAppSettingsFolders
                    mainappargs = ConvertCmdArgsToString(Environment.GetCommandLineArgs) & " /fromlauncher" & " " & GenerateCommandLineMainAppSettings() & " " & AppSettingsObj.LauncherAdditionalMainAppArguments
                    mainapp.StartInfo.WorkingDirectory = Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath)
                Case MainAppCommandlLineArgumentsMode.RunMainAppWithoutLocalWorkingDirAndSetAppSettingsFolders
                    mainappargs = ConvertCmdArgsToString(Environment.GetCommandLineArgs) & " /fromlauncher" & " " & GenerateCommandLineMainAppSettings() & " " & AppSettingsObj.LauncherAdditionalMainAppArguments
            End Select

            mainapp.StartInfo.Arguments = mainappargs
            mainapp.Start()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GenerateCommandLineMainAppSettings() As String
        Dim result As String
        result = "/appsettingspath " & Application.StartupPath & "\AppSettings.xml" &
            " /usertemplatesdir " & Application.StartupPath & "\" & AppSettingsObj.UserTemplatesDir &
            " /userinitialtemplatedir " & Application.StartupPath & "\" & AppSettingsObj.UserInitialTemplateDir

        Return result
    End Function

    Public Function GenerateCommandLineMainAppLocalFolders() As String
        Dim result As String
        result = "/environmentplugindir " & Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\" & AppSettingsObj.EnvironmentPluginDir &
            " /credentialplugindir " & Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\" & AppSettingsObj.CredentialPluginDir &
            " /guiplugindir " & Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\" & AppSettingsObj.GUIPluginDir

        Return result
    End Function

    Private Sub LoadingState_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadingState.DoWork
        Try
            SetLabelText(LoadingStateLbl, "Loading settings...")
            CheckForCommandLineArgsAppBase(Environment.GetCommandLineArgs)
            SetLabelText(LoadingStateLbl, "Reading application settings...")
            If Not IO.File.Exists(ApplicationSettingsFile) Then
                'Create new settings initial file if no file exists...
                AppSettingsHandler.SaveSettings(New ApplicationSettings, ApplicationSettingsFile)
            End If
            LoadAppSettings()
            If AppSettingsObj.EnableLauncherSync Then
                SetLabelText(LoadingStateLbl, "Check files...")
                Dim targetdir As String
                targetdir = Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath)

                If IO.Directory.Exists(targetdir) Then
                    If SyncNeeded(True) Then
                        StartMainAppFromSourceNonElevated()
                        Exit Try
                    End If
                Else
                    If AppSettingsObj.LauncherSyncNeedsElevation Then
                        If IsElevated = False Then
                            SyncNeeded()
                        Else
                            Try
                                IO.Directory.CreateDirectory(targetdir)
                            Catch ex As Exception
                            End Try

                            If SyncNeeded() = False Then
                                'Start main app if target directory was first created.
                                StartMainAppFromSourceNonElevated()
                            End If
                        End If
                            Else
                        IO.Directory.CreateDirectory(targetdir)
                        SyncNeeded()
                    End If
                End If
                If Not IsElevated Then
                    If AppSettingsObj.LauncherCreateMainApplicationShortcutOnDesktop Then
                        SetLabelText(LoadingStateLbl, "Create shortcut...")
                        CheckAndCreateDesktopShortcut()
                    End If

                    'Start main app if target directory was synced.
                    StartMainAppFromSourceNonElevated()
                End If
            Else
                If AppSettingsObj.LauncherCreateMainApplicationShortcutOnDesktop Then
                    SetLabelText(LoadingStateLbl, "Create shortcut...")
                    CheckAndCreateDesktopShortcut()
                End If
                StartMainAppFromSourceNonElevated()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Function CheckAndCreateDesktopShortcut() As Boolean
        If Not LauncherHelperInstance.ShortcutExists(My.Computer.FileSystem.SpecialDirectories.Desktop & "\CSTool") Then
            If Not LauncherHelperInstance.CreateShortCut(Application.StartupPath & "\CSToolLauncher.exe", My.Computer.FileSystem.SpecialDirectories.Desktop, "CSTool", Application.StartupPath) Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

    Private Sub LoadingState_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadingState.RunWorkerCompleted
        Application.ExitThread()
    End Sub
End Class