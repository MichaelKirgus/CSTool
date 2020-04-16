﻿'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolApplicationSettingsLib
Imports CSToolApplicationSettingsManager
Imports CSToolSyncLib

Public Class LoadingFrm
    Public IsElevated As Boolean = False
    Public ApplicationSettingsFile As String = "AppSettings.xml"
    Public AppSettingsHandler As ApplicationSettingsManager
    Public AppSettingsObj As ApplicationSettings
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

            For ind = 0 To arglist.Count - 1
                newstr += arglist(ind)
            Next

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
            elevatedproccess.StartInfo.Arguments = "/elevated"
            elevatedproccess.StartInfo.Verb = "runas"
            If elevatedproccess.Start() Then
                Return True
            Else
                Return False
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CheckFilesDate(ByVal SourcePath As String, ByVal DestinationPath As String, ByVal Recursive As Boolean) As Boolean
        Try
            Dim allfiles As String()
            SetLabelText(LoadingStateLbl, "Check files in path " & DestinationPath & " ...")
            allfiles = IO.Directory.GetFiles(SourcePath)

            If Not allfiles.Count = 0 Then
                For index = 0 To allfiles.Count - 1
                    Try
                        Dim fileinfsource As New IO.FileInfo(allfiles(index))
                        SetLabelText(LoadingStateLbl, "Check file " & DestinationPath & "\" & fileinfsource.Name & " ...")
                        If IO.File.Exists(DestinationPath & "\" & fileinfsource.Name) Then
                            Dim fileinfdest As New IO.DirectoryInfo(DestinationPath & "\" & fileinfsource.Name)
                            If fileinfdest.LastWriteTime < fileinfsource.LastWriteTime Then
                                Return True
                            End If
                        Else
                            Return True
                        End If

                    Catch ex As Exception
                        Return False
                    End Try
                Next
            End If

            If Recursive Then
                Dim alldirs As String()
                SetLabelText(LoadingStateLbl, "Check subfolders in path " & DestinationPath & " ...")
                alldirs = IO.Directory.GetDirectories(SourcePath)

                If Not alldirs.Count = 0 Then
                    For index = 0 To allfiles.Count - 1
                        Try
                            Dim dirinf As New IO.DirectoryInfo(alldirs(index))
                            If CheckFilesDate(SourcePath & "\" & dirinf.Name, DestinationPath & "\" & dirinf.Name, Recursive) Then
                                Return True
                            End If
                        Catch ex As Exception
                            Return False
                        End Try
                    Next
                End If
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SyncNeeded() As Boolean
        Try
            If AppSettingsObj.LauncherSyncNeedsElevation And IsElevated = False Then
                HandleUserInteraction()
                Application.Exit()
            End If

            SetLabelText(LoadingStateLbl, "Initialize syncing...")
            Dim SyncHandler As New SyncLib
            SetLabelText(LoadingStateLbl, "Update main application files...")
            SyncHandler.StartSync(Application.StartupPath, AppSettingsObj.LauncherSyncPath, False)
            SetLabelText(LoadingStateLbl, "Update credential plugins..")
            SyncHandler.StartSync(Application.StartupPath & "\" & AppSettingsObj.CredentialPluginDir, AppSettingsObj.LauncherSyncPath & "\" & AppSettingsObj.CredentialPluginDir, True)
            SetLabelText(LoadingStateLbl, "Update environment plugins..")
            SyncHandler.StartSync(Application.StartupPath & "\" & AppSettingsObj.EnvironmentPluginDir, AppSettingsObj.LauncherSyncPath & "\" & AppSettingsObj.EnvironmentPluginDir, True)
            SetLabelText(LoadingStateLbl, "Update GUI plugins..")
            SyncHandler.StartSync(Application.StartupPath & "\" & AppSettingsObj.GUIPluginDir, AppSettingsObj.LauncherSyncPath & "\" & AppSettingsObj.GUIPluginDir, True)
            If Not AppSettingsObj.LauncherIncludeFolderCollection.Count = 0 Then
                SetLabelText(LoadingStateLbl, "Update additional files...")
                For index = 0 To AppSettingsObj.LauncherIncludeFolderCollection.Count - 1
                    SyncHandler.StartSync(Application.StartupPath & "\" & AppSettingsObj.LauncherIncludeFolderCollection(index).FolderName, AppSettingsObj.LauncherSyncPath & "\" & AppSettingsObj.LauncherIncludeFolderCollection(index).FolderName, AppSettingsObj.LauncherIncludeFolderCollection(index).Recursive)
                Next
            End If

            StartMainAppNonElevated()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function HandleUserInteraction() As Boolean
        Try
            Dim userdlg As New Form1
            Dim targetdir As String
            targetdir = Environment.ExpandEnvironmentVariables(AppSettingsObj.LauncherSyncPath)
            If Not IO.Directory.Exists(targetdir) Then
                userdlg.Button1.Enabled = False
            End If

            Dim result As MsgBoxResult
            result = userdlg.ShowDialog()

            If result = MsgBoxResult.Abort Then
                Return True
            End If
            If result = MsgBoxResult.Ignore Then
                'Start old app local
                Dim mainappargs As String
                Dim mainapp As New Process
                mainapp.StartInfo.FileName = AppSettingsObj.LauncherSyncPath & "\CSTool.exe"
                mainappargs = ConvertCmdArgsToString(Environment.GetCommandLineArgs)
                mainapp.StartInfo.Arguments = mainappargs
                mainapp.StartInfo.WorkingDirectory = Application.StartupPath
                mainapp.Start()
                Return True
            End If
            If result = MsgBoxResult.No Then
                'Start old app remote
                Dim mainappargs As String
                Dim mainapp As New Process
                mainapp.StartInfo.FileName = Application.StartupPath & "\CSTool.exe"
                mainappargs = ConvertCmdArgsToString(Environment.GetCommandLineArgs)
                mainapp.StartInfo.Arguments = mainappargs
                mainapp.StartInfo.WorkingDirectory = Application.StartupPath
                mainapp.Start()
                Return True
            End If

            If result = MsgBoxResult.Retry Then
                RunAppElevated()
                Return True
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function StartMainAppNonElevated()
        Try
            Dim mainappargs As String
            Dim mainapp As New Process
            mainappargs = ConvertCmdArgsToString(Environment.GetCommandLineArgs)
            mainapp.StartInfo.Arguments = My.Resources.pathsep & AppSettingsObj.LauncherSyncPath & "\CSTool.exe " & mainappargs & My.Resources.pathsep
            mainapp.StartInfo.FileName = "explorer.exe"
            mainapp.StartInfo.WorkingDirectory = Application.StartupPath
            mainapp.Start()

            Return True
        Catch ex As Exception
            Return False
        End Try
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
                    Dim endresult As Boolean = False
                    endresult = CheckFilesDate(Application.StartupPath, AppSettingsObj.LauncherSyncPath, False)
                    If endresult = False Then
                        endresult = CheckFilesDate(Application.StartupPath & "\" & AppSettingsObj.CredentialPluginDir, AppSettingsObj.LauncherSyncPath & "\" & AppSettingsObj.CredentialPluginDir, True)
                        If endresult = False Then
                            endresult = CheckFilesDate(Application.StartupPath & "\" & AppSettingsObj.EnvironmentPluginDir, AppSettingsObj.LauncherSyncPath & "\" & AppSettingsObj.EnvironmentPluginDir, True)
                            If endresult = False Then
                                endresult = CheckFilesDate(Application.StartupPath & "\" & AppSettingsObj.GUIPluginDir, AppSettingsObj.LauncherSyncPath & "\" & AppSettingsObj.GUIPluginDir, True)
                                If endresult = False Then
                                    If Not AppSettingsObj.LauncherIncludeFolderCollection.Count = 0 Then
                                        For index = 0 To AppSettingsObj.LauncherIncludeFolderCollection.Count - 1
                                            endresult = CheckFilesDate(Application.StartupPath & "\" & AppSettingsObj.LauncherIncludeFolderCollection(index).FolderName, AppSettingsObj.LauncherSyncPath & "\" & AppSettingsObj.LauncherIncludeFolderCollection(index).FolderName, AppSettingsObj.LauncherIncludeFolderCollection(index).Recursive)
                                            If endresult = True Then
                                                Exit For
                                            End If
                                        Next
                                        If endresult = False Then
                                            'No sync needed, all files are ok.
                                        Else
                                            SyncNeeded()
                                        End If
                                    End If
                                Else
                                    SyncNeeded()
                                    Exit Sub
                                End If
                            Else
                                SyncNeeded()
                                Exit Sub
                            End If
                        Else
                            SyncNeeded()
                            Exit Sub
                        End If
                    Else
                        SyncNeeded()
                        Exit Sub
                    End If
                Else
                    If AppSettingsObj.LauncherSyncNeedsElevation Then
                        HandleUserInteraction()
                    Else
                        IO.Directory.CreateDirectory(targetdir)
                        SyncNeeded()
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class