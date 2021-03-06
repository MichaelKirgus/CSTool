﻿'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolApplicationSettingsLib
Imports CSToolApplicationSettingsManager
Imports CSToolLauncherLib
Imports CSToolLogLib
Imports CSToolSignatureHelper
Imports CSToolSyncLib

Public Class LoadingFrm
    Public IsElevated As Boolean = False
    Public ApplicationSettingsFile As String = "AppSettings.xml"
    Public AppSettingsHandler As New ApplicationSettingsManager
    Public AppSettingsObj As New ApplicationSettings
    Public LauncherHelperInstance As New LauncherLib
    Private Delegate Sub SetLabelTextDelegate(ByVal LabelCtl As Label, ByVal TextStr As String)
    Private Delegate Sub SetFormOpacityDelegate(ByVal FormCtl As Form, ByVal OpacityValue As Integer)

    Private Sub SetLabelText(ByVal LabelCtl As Label, ByVal TextStr As String)
        If LabelCtl.InvokeRequired Then
            Dim ClearListViewObj As New SetLabelTextDelegate(AddressOf SetLabelText)
            LabelCtl.Invoke(ClearListViewObj, New Object() {LabelCtl, TextStr})
        Else
            LabelCtl.Text = TextStr
        End If
    End Sub

    Private Sub SetFormOpacity(ByVal FormCtl As Form, ByVal OpacityValue As Integer)
        If FormCtl.InvokeRequired Then
            Dim FormOpacityObj As New SetFormOpacityDelegate(AddressOf SetFormOpacity)
            FormCtl.Invoke(FormOpacityObj, New Object() {FormCtl, OpacityValue})
        Else
            FormCtl.Opacity = OpacityValue
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

                If arglist(ind).ToLower = "/skipenvironmentcheck" Then
                    AppSettingsObj.LauncherCheckIfLauncherRunningFromValidWorkingDirectory = False
                    AppSettingsObj.LauncherCheckIfUserCanSaveSettings = False
                End If

                If arglist(ind).ToLower = "/skiprunningmainappcheck" Then
                    AppSettingsObj.LauncherCheckIfMainApplicationIsRunning = False
                End If

                If arglist(ind).ToLower = "/lockfile" Then
                    AppSettingsObj.LauncherLockfile = arglist(ind + 1)
                End If

                If arglist(ind).ToLower = "/ignorelockfile" Then
                    AppSettingsObj.LauncherLockfile = ""
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
            elevatedproccess.StartInfo.WorkingDirectory = Application.StartupPath
            elevatedproccess.StartInfo.Arguments = "/elevated " & ConvertCmdArgsToString(Environment.GetCommandLineArgs)
            elevatedproccess.StartInfo.Verb = "runas"
            elevatedproccess.StartInfo.UseShellExecute = True
            If elevatedproccess.Start() Then
                SetLabelText(LoadingStateLbl, "Waiting...")
                SetFormOpacity(Me, 0)
                elevatedproccess.WaitForExit()
                SetFormOpacity(Me, 100)
                Threading.Thread.Sleep(500)
            Else
                Return False
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeleteNonPersistentFilesFromShare() As Boolean
        Try
            If Not AppSettingsObj.NonPersistentFilesCollection.Count = 0 Then
                For index = 0 To AppSettingsObj.NonPersistentFilesCollection.Count - 1
                    If IO.File.Exists(Application.StartupPath & "\" & AppSettingsObj.NonPersistentFilesCollection(index).FileName) Then
                        IO.File.Delete(Application.StartupPath & "\" & AppSettingsObj.NonPersistentFilesCollection(index).FileName)
                    End If
                Next
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CheckIfUserCanWriteAtProfilePath() As Boolean
        Try
            If Not IO.File.Exists(AppSettingsObj.UserProfileDir & "\Test.dat") Then
                IO.File.WriteAllText(AppSettingsObj.UserProfileDir & "\Test.dat", "Test")
                IO.File.Delete(AppSettingsObj.UserProfileDir & "\Test.dat")
            Else
                Return True
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub UntilFinishShowStatus(ByRef SyncHandlerInstance As SyncLib)
        Do While SyncHandlerInstance.SyncTaskHost.IsBusy
            If Not SyncHandlerInstance.CurrentFile = "" Then
                SetLabelText(LoadingStateLbl, SyncHandlerInstance.CurrentTask & ": " & SyncHandlerInstance.CurrentFile)
            Else
                SetLabelText(LoadingStateLbl, SyncHandlerInstance.CurrentTask & ": " & SyncHandlerInstance.CurrentFolder)
            End If

            Threading.Thread.Sleep(10)
        Loop
    End Sub


    Public Function SyncNeeded(Optional ByVal OnlyCheck As Boolean = False, Optional SyncLibInstance As SyncLib = Nothing) As Boolean
        Dim SyncHandler As SyncLib = Nothing

        If IsNothing(SyncLibInstance) Then
            SyncHandler = New SyncLib
            SyncHandler.LogHandler = New LogLib
            SyncHandler.LogHandler.LogSettings = AppSettingsObj.LauncherLogSettings
            SyncHandler.LogHandler.InitLogSystem()
        Else
            SyncHandler = SyncLibInstance
        End If

        Try
            If Not OnlyCheck Then
                If AppSettingsObj.LauncherSyncNeedsElevation And IsElevated = False Then
                    Select Case HandleUserInteraction()
                        Case 1
                            If StartMainAppFromSourceNonElevated() = False Then
                                If LauncherHelperInstance.IsCurrentUserAdmin Then
                                    LauncherHelperInstance.ShowElevatedAppWarningMsg()
                                    Exit Try
                                End If
                            End If
                        Case 2
                            If StartMainAppFromShareNonElevated() = False Then
                                LauncherHelperInstance.ShowElevatedAppWarningMsg()
                                Exit Try
                            End If
                        Case -1
                            Exit Try
                    End Select
                End If
            Else
                SetLabelText(LoadingStateLbl, "Delete temp. files...")
                DeleteNonPersistentFilesFromShare()
            End If

            SetLabelText(LoadingStateLbl, "Initialize syncing...")
            SyncHandler.LogHandler.WriteLogEntry("Update main application files (Core application files)...", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
            SetLabelText(LoadingStateLbl, "Update main application files...")
            SyncHandler.StartSyncAsync(Application.StartupPath, Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath), False, OnlyCheck)
            UntilFinishShowStatus(SyncHandler)
            If SyncHandler.FilesOrDirsChanged Then
                SyncHandler.LogHandler.WriteLogEntry("Update main application files (Core application files) => Files or directories changed.", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
                SyncNeeded(False, SyncHandler)
                Return False
            End If
            SyncHandler.LogHandler.WriteLogEntry("Update main application files (Chromium locales directory)...", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
            SyncHandler.StartSyncAsync(Application.StartupPath & "\locales", Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\locales", True, OnlyCheck)
            UntilFinishShowStatus(SyncHandler)
            If SyncHandler.FilesOrDirsChanged Then
                SyncHandler.LogHandler.WriteLogEntry("Update main application files (Chromium locales directory) => Files or directories changed.", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
                SyncNeeded(False, SyncHandler)
                Return False
            End If
            SyncHandler.LogHandler.WriteLogEntry("Update main application files (Chromium swiftshader directory)...", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
            SyncHandler.StartSyncAsync(Application.StartupPath & "\swiftshader", Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\swiftshader", True, OnlyCheck)
            UntilFinishShowStatus(SyncHandler)
            If SyncHandler.FilesOrDirsChanged Then
                SyncHandler.LogHandler.WriteLogEntry("Update main application files (Chromium swiftshader directory) => Files or directories changed.", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
                SyncNeeded(False, SyncHandler)
                Return False
            End If
            SyncHandler.LogHandler.WriteLogEntry("Update main application files (Gecko core directory)...", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
            SyncHandler.StartSyncAsync(Application.StartupPath & "\Firefox64", Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\Firefox64", True, OnlyCheck)
            UntilFinishShowStatus(SyncHandler)
            If SyncHandler.FilesOrDirsChanged Then
                SyncHandler.LogHandler.WriteLogEntry("Update main application files (Gecko core directory) => Files or directories changed.", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
                SyncNeeded(False, SyncHandler)
                Return False
            End If
            SyncHandler.LogHandler.WriteLogEntry("Update main application files (License documentation)...", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
            SyncHandler.StartSyncAsync(Application.StartupPath & "\Licenses", Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\Licenses", True, OnlyCheck)
            UntilFinishShowStatus(SyncHandler)
            If SyncHandler.FilesOrDirsChanged Then
                SyncHandler.LogHandler.WriteLogEntry("Update main application files (License documentation) => Files or directories changed.", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
                SyncNeeded(False, SyncHandler)
                Return False
            End If
            SyncHandler.LogHandler.WriteLogEntry("Update credential plugins...", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
            SetLabelText(LoadingStateLbl, "Update credential plugins...")
            SyncHandler.StartSyncAsync(Application.StartupPath & "\" & AppSettingsObj.CredentialPluginDir, Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\" & AppSettingsObj.CredentialPluginDir, True, OnlyCheck)
            UntilFinishShowStatus(SyncHandler)
            If SyncHandler.FilesOrDirsChanged Then
                SyncHandler.LogHandler.WriteLogEntry("Update credential plugins => Files or directories changed.", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
                SyncNeeded(False, SyncHandler)
                Return False
            End If
            SyncHandler.LogHandler.WriteLogEntry("Update environment plugins...", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
            SetLabelText(LoadingStateLbl, "Update environment plugins...")
            SyncHandler.StartSyncAsync(Application.StartupPath & "\" & AppSettingsObj.EnvironmentPluginDir, Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\" & AppSettingsObj.EnvironmentPluginDir, True, OnlyCheck)
            UntilFinishShowStatus(SyncHandler)
            If SyncHandler.FilesOrDirsChanged Then
                SyncHandler.LogHandler.WriteLogEntry("Update environment plugins => Files or directories changed.", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
                SyncNeeded(False, SyncHandler)
                Return False
            End If
            SyncHandler.LogHandler.WriteLogEntry("Update GUI plugins...", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
            SetLabelText(LoadingStateLbl, "Update GUI plugins...")
            SyncHandler.StartSyncAsync(Application.StartupPath & "\" & AppSettingsObj.GUIPluginDir, Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\" & AppSettingsObj.GUIPluginDir, True, OnlyCheck)
            UntilFinishShowStatus(SyncHandler)
            If SyncHandler.FilesOrDirsChanged Then
                SyncHandler.LogHandler.WriteLogEntry("Update GUI plugins => Files or directories changed.", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
                SyncNeeded(False, SyncHandler)
                Return False
            End If
            If Not AppSettingsObj.LauncherIncludeFolderCollection.Count = 0 Then
                SyncHandler.LogHandler.WriteLogEntry("Update additional files...", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
                SetLabelText(LoadingStateLbl, "Update additional files...")
                For index = 0 To AppSettingsObj.LauncherIncludeFolderCollection.Count - 1
                    SyncHandler.StartSyncAsync(Application.StartupPath & "\" & AppSettingsObj.LauncherIncludeFolderCollection(index).FolderName, Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\" & AppSettingsObj.LauncherIncludeFolderCollection(index).FolderName, AppSettingsObj.LauncherIncludeFolderCollection(index).Recursive, OnlyCheck)
                    UntilFinishShowStatus(SyncHandler)
                Next
                If SyncHandler.FilesOrDirsChanged Then
                    SyncHandler.LogHandler.WriteLogEntry("Update additional files => Files or directories changed.", Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.Info, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential)
                    SyncNeeded(False, SyncHandler)
                    Return False
                End If
            End If

            SyncHandler.LogHandler.CloseStreams()

            Return True
        Catch ex As Exception
            SyncHandler.LogHandler.WriteLogEntry("Error: " & Err.Description, Me.GetType, CSToolLogLib.LogSettings.LogEntryTypeEnum.ErrorL, CSToolLogLib.LogSettings.LogEntryLevelEnum.Essential, Err)
            SyncHandler.LogHandler.CloseStreams()
            Return False
        End Try
    End Function

    Public Function HandleUserInteraction() As Integer
        Try
            Select Case AppSettingsObj.LauncherPolicy
                Case LauncherPolicyMode.CheckForChangesAndAskUser
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
                Case LauncherPolicyMode.CheckForChangesAndForceUpdate
                    Dim userdlg As New Form1
                    Dim targetdir As String
                    targetdir = Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath)
                    If Not IO.Directory.Exists(targetdir) Then
                        userdlg.Button1.Enabled = False
                    End If
                    userdlg.Button2.Enabled = False

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
                Case LauncherPolicyMode.CheckForChangesAndForceUpdateWithoutAskingUser
                    SetLabelText(LoadingStateLbl, "Start launcher elevated...")
                    If RunAppElevated() = False Then
                        Return -1
                    Else
                        Return 3
                    End If
            End Select

            Return -1
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Public Function StartMainAppFromShareNonElevated() As Boolean
        Try
            If Not LauncherHelperInstance.IsCurrentUserAdmin And AppSettingsObj.PreventMainAppFromRunningElevated Then
                Dim mainappargs As String
                Dim mainapp As New Process
                mainapp.StartInfo.FileName = Application.StartupPath & "\CSTool.exe"
                mainappargs = ConvertCmdArgsToString(Environment.GetCommandLineArgs) & " /fromlauncher"
                mainapp.StartInfo.Arguments = mainappargs
                mainapp.StartInfo.WorkingDirectory = Application.StartupPath

                Dim result As Boolean
                result = mainapp.Start()

                Return result
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function StartMainAppFromSourceNonElevated() As Boolean
        Try
            If Not LauncherHelperInstance.IsCurrentUserAdmin And AppSettingsObj.PreventMainAppFromRunningElevated And Not IsElevated Then
                Dim SignatureHandler As New SignatureHelper
                If SignatureHandler.CheckSign(Application.ExecutablePath) Then
                    If Not SignatureHandler.CheckSign(Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\CSTool.exe") Then
                        Dim result As MsgBoxResult
                        result = MsgBox("Main application file is not digital signed or the signature is not valid! Do you want to run the application anyway?", MsgBoxStyle.YesNo)
                        If result = MsgBoxResult.No Then
                            Return False
                        End If
                    End If
                End If

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
                    Case MainAppCommandlLineArgumentsMode.RunMainAppWithoutLocalWorkingDirAndLocalPluginFolders
                        mainappargs = ConvertCmdArgsToString(Environment.GetCommandLineArgs) & " /fromlauncher" & " " & GenerateCommandLineMainAppLocalFolders() & " " & AppSettingsObj.LauncherAdditionalMainAppArguments
                End Select

                If mainappargs.EndsWith(" ") Then
                    mainappargs = mainappargs.Substring(0, mainappargs.Length - 1)
                End If

                mainapp.StartInfo.Arguments = mainappargs
                Dim execresult As Boolean
                execresult = mainapp.Start()

                Return execresult
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GenerateCommandLineMainAppSettings() As String
        Dim result As String
        result = "/appsettingspath " & My.Resources.pathsep & Application.StartupPath & "\AppSettings.xml" & My.Resources.pathsep &
            " /usertemplatesdir " & My.Resources.pathsep & Application.StartupPath & "\" & AppSettingsObj.UserTemplatesDir & My.Resources.pathsep &
            " /userinitialtemplatedir " & My.Resources.pathsep & Application.StartupPath & "\" & AppSettingsObj.UserInitialTemplateDir & My.Resources.pathsep &
            " /userprofiledir " & My.Resources.pathsep & Application.StartupPath & "\" & AppSettingsObj.UserProfileDir & My.Resources.pathsep &
            " /userworkspacetemplatesdir " & My.Resources.pathsep & Application.StartupPath & "\" & AppSettingsObj.UserWorkspaceTemplatesDir & My.Resources.pathsep

        If Not AppSettingsObj.MainAppInstanceTag = "" Then
            result += " /instancetag " & My.Resources.pathsep & AppSettingsObj.MainAppInstanceTag & My.Resources.pathsep
        End If

        Return result
    End Function

    Public Function GenerateCommandLineMainAppLocalFolders() As String
        Dim result As String
        result = "/environmentplugindir " & My.Resources.pathsep & Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\" & AppSettingsObj.EnvironmentPluginDir & My.Resources.pathsep &
            " /credentialplugindir " & My.Resources.pathsep & Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\" & AppSettingsObj.CredentialPluginDir & My.Resources.pathsep &
            " /guiplugindir " & My.Resources.pathsep & Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath) & "\" & AppSettingsObj.GUIPluginDir & My.Resources.pathsep

        If Not AppSettingsObj.MainAppInstanceTag = "" Then
            result += " /instancetag " & My.Resources.pathsep & AppSettingsObj.MainAppInstanceTag & My.Resources.pathsep
        End If

        Return result
    End Function

    Private Sub LoadingState_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadingState.DoWork
        Try
            SetLabelText(LoadingStateLbl, "Loading settings...")
            CheckForCommandLineArgsAppBase(Environment.GetCommandLineArgs)
            If AppSettingsObj.LauncherCheckIfLauncherRunningFromValidWorkingDirectory Then
                'Is launcher running with valid working directory (check if appsetings.xml and main application exists)?
                SetLabelText(LoadingStateLbl, "Check for valid global config...")
                If Not IO.File.Exists(ApplicationSettingsFile) Or Not IO.File.Exists("CSTool.exe") Then
                    LauncherHelperInstance.ShowWrongLauncherWorkingDirectoryMsg()
                    Exit Try
                End If
            End If
            SetLabelText(LoadingStateLbl, "Reading application settings...")
            If Not IO.File.Exists(ApplicationSettingsFile) Then
                'Create new settings initial file if no file exists...
                AppSettingsHandler.SaveSettings(New ApplicationSettings, ApplicationSettingsFile)
            End If
            LoadAppSettings()
            If Not AppSettingsObj.LauncherLockfile = "" Then
                SetLabelText(LoadingStateLbl, "Check for lockfile...")
                If IO.File.Exists(AppSettingsObj.LauncherLockfile) Then
                    'Show message
                    Dim warningmsg As String
                    warningmsg = IO.File.ReadAllText(AppSettingsObj.LauncherLockfile)
                    If Not warningmsg = "" Then
                        MsgBox(warningmsg, MsgBoxStyle.Exclamation)
                    Else
                        MsgBox("This application is currently not available.")
                    End If
                    Exit Try
                End If
            End If
            If AppSettingsObj.DetectAppInstanceTagByParentDirectory Then
                'Get parent directory and set instance tag
                SetLabelText(LoadingStateLbl, "Detect application instance tag...")
                Dim parentinfo As New IO.DirectoryInfo(Application.StartupPath)
                AppSettingsObj.MainAppInstanceTag = parentinfo.Name
            End If
            If AppSettingsObj.LauncherCheckIfMainApplicationIsRunning Then
                'Is main application running?
                SetLabelText(LoadingStateLbl, "Check for running CSTool...")
                If LauncherHelperInstance.IsApplicationRunning("CSTool") Then
                    LauncherHelperInstance.ShowMainAppAlreadyRunningMsg()
                    Exit Try
                End If
            End If
            If AppSettingsObj.LauncherCheckIfUserCanSaveSettings Then
                'Can user write in the profiles folder?
                SetLabelText(LoadingStateLbl, "Check file permissions...")
                If Not CheckIfUserCanWriteAtProfilePath() Then
                    LauncherHelperInstance.ShowUserCanNotWriteInUserProfilePathMsg()
                    Exit Try
                End If
            End If
            If AppSettingsObj.EnableLauncherSync Then
                SetLabelText(LoadingStateLbl, "Check files...")
                Dim targetdir As String
                targetdir = Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath)

                If IO.Directory.Exists(targetdir) Then
                    If SyncNeeded(True) Then
                        SetLabelText(LoadingStateLbl, "Execute start scripts (elevated)...")
                        ExecuteScripts(AppSettingsObj.LauncherElevatedStartScriptCollection)

                        If StartMainAppFromSourceNonElevated() = False Then
                            LauncherHelperInstance.ShowElevatedAppWarningMsg()
                        End If
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

                            SetLabelText(LoadingStateLbl, "Execute start scripts (elevated)...")
                            ExecuteScripts(AppSettingsObj.LauncherElevatedStartScriptCollection)

                            If SyncNeeded() = False Then
                                'Start main app if target directory was first created.
                                If StartMainAppFromSourceNonElevated() = False Then
                                    LauncherHelperInstance.ShowElevatedAppWarningMsg()
                                    Exit Try
                                End If
                            End If
                        End If
                    Else
                        IO.Directory.CreateDirectory(targetdir)
                        SyncNeeded()
                    End If
                End If
                If Not IsElevated Then
                    SetLabelText(LoadingStateLbl, "Execute start scripts...")
                    ExecuteScripts(AppSettingsObj.LauncherStartScriptCollection)

                    If AppSettingsObj.LauncherCreateMainApplicationShortcutOnDesktop Then
                        SetLabelText(LoadingStateLbl, "Create shortcut...")
                        CheckAndCreateDesktopShortcut()
                    End If

                    'Start main app if target directory was synced.
                    If StartMainAppFromSourceNonElevated() = False Then
                        LauncherHelperInstance.ShowElevatedAppWarningMsg()
                        Exit Try
                    End If
                End If
            Else
                If AppSettingsObj.LauncherCreateMainApplicationShortcutOnDesktop Then
                    SetLabelText(LoadingStateLbl, "Create shortcut...")
                    CheckAndCreateDesktopShortcut()
                End If
                If StartMainAppFromSourceNonElevated() = False Then
                    LauncherHelperInstance.ShowElevatedAppWarningMsg()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Function CheckAndCreateDesktopShortcut() As Boolean
        If AppSettingsObj.MainAppInstanceTag = "" Then
            If Not LauncherHelperInstance.ShortcutExists(My.Computer.FileSystem.SpecialDirectories.Desktop & "\CSTool") Then
                If Not LauncherHelperInstance.CreateShortCut(Application.StartupPath & "\CSToolLauncher.exe", My.Computer.FileSystem.SpecialDirectories.Desktop, "CSTool", Application.StartupPath) Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If
        Else
            If Not LauncherHelperInstance.ShortcutExists(My.Computer.FileSystem.SpecialDirectories.Desktop & "\CSTool (" & AppSettingsObj.MainAppInstanceTag & ")") Then
                If Not LauncherHelperInstance.CreateShortCut(Application.StartupPath & "\CSToolLauncher.exe", My.Computer.FileSystem.SpecialDirectories.Desktop, "CSTool (" & AppSettingsObj.MainAppInstanceTag & ")", Application.StartupPath) Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If
        End If
    End Function

    Public Function ExecuteScripts(ByVal ScriptCollection As List(Of LauncherStartScriptEntry)) As Boolean
        Try
            If Not ScriptCollection.Count = 0 Then
                For index = 0 To ScriptCollection.Count - 1
                    If ScriptCollection(index).ScriptPath.EndsWith(".reg") Then
                        Shell("regedit /s " & My.Resources.pathsep & Environment.ExpandEnvironmentVariables(ScriptCollection(index).ScriptPath) & My.Resources.pathsep, AppWinStyle.Hide, True)
                    Else
                        Shell(Environment.ExpandEnvironmentVariables(ScriptCollection(index).ScriptPath), AppWinStyle.Hide, True)
                    End If
                Next
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub LoadingState_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadingState.RunWorkerCompleted
        Application.ExitThread()
    End Sub
End Class