Imports System.ComponentModel
Imports System.IO
Imports CSToolLogLib

Public Class SyncLib
    Public WithEvents SyncTaskHost As New BackgroundWorker

    Public FilesOrDirsChanged As Boolean = False
    Public LogHandler As New LogLib

    Public CurrentTask As String = "None"
    Public CurrentFile As String = "None"
    Public CurrentFolder As String = "None"
    Public IsAsyncTaskRunning As Boolean = False
    Public SyncSuccessful As Boolean = False
    Public LastError As String = ""

    Public Function StartSyncAsync(ByVal sSrcPath As String, ByVal sDestPath As String, ByVal Recursive As Boolean, ByVal Simulate As Boolean)
        Try
            Dim RuntimeArgs As New List(Of Object)
            RuntimeArgs.Add(sSrcPath)
            RuntimeArgs.Add(sDestPath)
            RuntimeArgs.Add(Recursive)
            RuntimeArgs.Add(Simulate)

            If Not SyncTaskHost.IsBusy Then
                SyncTaskHost.RunWorkerAsync(RuntimeArgs)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function WaitForSyncEnd() As Boolean
        If IsAsyncTaskRunning Then
            Do While IsAsyncTaskRunning
                Threading.Thread.Sleep(50)
            Loop
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub StartSyncWorker(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles SyncTaskHost.DoWork
        IsAsyncTaskRunning = True

        Dim args As List(Of Object)
        args = e.Argument

        e.Result = StartSync(args(0), args(1), args(2), args(3))
    End Sub

    Public Sub FinishedSyncWorker(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles SyncTaskHost.RunWorkerCompleted
        CurrentTask = "None"
        CurrentFile = "None"
        CurrentFolder = "None"

        If e.Result Then
            SyncSuccessful = True
        Else
            SyncSuccessful = False
        End If

        IsAsyncTaskRunning = False
    End Sub

    Public Function CopyFolder_Sync(ByVal sSrcPath As String, ByVal sDestPath As String, ByVal Recursive As Boolean, ByVal Simulate As Boolean) As Boolean
        Try
            CurrentFile = ""
            CurrentFolder = sDestPath
            CurrentTask = "Checking"
            If Not Directory.Exists(sDestPath) Then
                LogHandler.WriteLogEntry("Directory " & sDestPath & " does not exist.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                If Not Simulate Then
                    Try
                        LogHandler.WriteLogEntry("Create Directory " & sDestPath, Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                        CurrentTask = "Create"
                        Directory.CreateDirectory(sDestPath)
                    Catch ex As Exception
                        LogHandler.WriteLogEntry("Create Directory " & sDestPath & ": Error.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug, Err)
                        Return False
                    End Try
                Else
                    LogHandler.WriteLogEntry("Directory " & sDestPath & ": Directory was changed.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                    FilesOrDirsChanged = True
                    Return True
                End If
            Else
                LogHandler.WriteLogEntry("Directory " & sDestPath & " exists.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
            End If

            If Not sSrcPath.EndsWith("\") Then
                sSrcPath &= "\"
            End If
            If Not sDestPath.EndsWith("\") Then
                sDestPath &= "\"
            End If

            LogHandler.WriteLogEntry("Directory " & sDestPath & ": Delete old files...", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
            DeleteFilesDestination(sSrcPath, sDestPath, Simulate)
            If FilesOrDirsChanged And Simulate Then
                Return True
            End If
            LogHandler.WriteLogEntry("Directory " & sDestPath & ": Update (override) old files...", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
            UpdateFilesDestination(sSrcPath, sDestPath, Simulate)
            If FilesOrDirsChanged And Simulate Then
                Return True
            End If
            LogHandler.WriteLogEntry("Directory " & sDestPath & ": Copy new additional files...", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
            CopyNewFilesDestination(sSrcPath, sDestPath, Simulate)
            If FilesOrDirsChanged And Simulate Then
                Return True
            End If

            If Recursive Then
                LogHandler.WriteLogEntry("Directory " & sDestPath & ": Recursive mode enabled. Entering subdirs...", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                Dim sDirs() As String = Directory.GetDirectories(sSrcPath)
                Dim sDir As String
                If sDirs.Length > 0 Then
                    For i As Integer = 0 To sDirs.Length - 1
                        If sDirs(i) <> sDestPath Then
                            sDir = sDirs(i).Substring(sDirs(i).LastIndexOf("\") + 1)
                            LogHandler.WriteLogEntry("Entering directory " & sDir & " (" & sDirs(i).ToString & ")", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                            CopyFolder_Sync(sDirs(i).ToString, sDestPath & sDir, Recursive, Simulate)
                        End If
                    Next i
                Else
                    LogHandler.WriteLogEntry("Directory " & sDestPath & ": No sub-directories.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                End If
            Else
                LogHandler.WriteLogEntry("Directory " & sDestPath & ": Recursive mode disabled. Skipping subdirs...", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub DeleteFilesDestination(ByVal sSrcPath As String, ByVal sDestPath As String, ByVal Simulate As Boolean)

        Dim sFiles() As String = Directory.GetFiles(sDestPath)
        Dim sFile As String

        For i As Integer = 0 To sFiles.Length - 1
            sFile = sFiles(i).Substring(sFiles(i).LastIndexOf("\") + 1)

            Try
                If Not File.Exists(sSrcPath & sFile) Then
                    If Not Simulate Then
                        Try
                            LogHandler.WriteLogEntry("Delete file " & sSrcPath & sFile & " ...", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                            CurrentTask = "Delete"
                            CurrentFile = sFile
                            My.Computer.FileSystem.DeleteFile(sDestPath & sFile)
                        Catch ex As Exception
                            LogHandler.WriteLogEntry("Delete file " & sSrcPath & sFile & ": Error.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug, Err)
                        End Try
                    Else
                        LogHandler.WriteLogEntry("File  " & sSrcPath & sFile & ": File was deleted from source directory.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                        FilesOrDirsChanged = True
                        Exit Sub
                    End If
                Else
                    LogHandler.WriteLogEntry("Skipping file " & sSrcPath & sFile & ": File exists in source directory.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                End If
            Catch ex As Exception
                LastError = Err.Description
                LogHandler.WriteLogEntry("File " & sSrcPath & sFile & ": Error.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug, Err)
            End Try
        Next i
    End Sub

    Private Sub UpdateFilesDestination(ByVal sSrcPath As String, ByVal sDestPath As String, ByVal Simulate As Boolean)

        Dim sFiles() As String = Directory.GetFiles(sDestPath)
        Dim sFile As String = ""

        For i As Integer = 0 To sFiles.Length - 1
            Try
                sFile = sFiles(i).Substring(sFiles(i).LastIndexOf("\") + 1)

                If File.Exists(sSrcPath & sFile) Then
                    Dim DestFile As New FileInfo(sDestPath & sFile)
                    Dim SrcFile As New FileInfo(sSrcPath & sFile)

                    If DestFile.LastWriteTime <> SrcFile.LastWriteTime Then
                        If Not Simulate Then
                            Try
                                CurrentTask = "Update"
                                CurrentFile = sFile
                                LogHandler.WriteLogEntry("Delete file " & sDestPath & sFile & " ...", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                                My.Computer.FileSystem.DeleteFile(sDestPath & sFile)
                                LogHandler.WriteLogEntry("Copy file " & sSrcPath & sFile & " to " & sDestPath & sFile, Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                                My.Computer.FileSystem.CopyFile(sSrcPath & sFile, sDestPath & sFile)
                            Catch ex As Exception
                                LogHandler.WriteLogEntry("Update file " & sSrcPath & sFile & ": Error.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug, Err)
                            End Try
                        Else
                            LogHandler.WriteLogEntry("File  " & sSrcPath & sFile & ": File was changed.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                            FilesOrDirsChanged = True
                            Exit Sub
                        End If
                    End If
                End If
            Catch ex As Exception
                LastError = Err.Description
                LogHandler.WriteLogEntry("File " & sSrcPath & sFile & ": Error.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug, Err)
            End Try
        Next i
    End Sub

    Private Sub CopyNewFilesDestination(ByVal sSrcPath As String, ByVal sDestPath As String, ByVal Simulate As Boolean)

        Dim sFiles() As String = Directory.GetFiles(sSrcPath)
        Dim sFile As String = ""

        For i As Integer = 0 To sFiles.Length - 1
            Try
                sFile = sFiles(i).Substring(sFiles(i).LastIndexOf("\") + 1)

                If Not File.Exists(sDestPath & sFile) Then
                    If Not Simulate Then
                        Try
                            LogHandler.WriteLogEntry("Copy file " & sSrcPath & sFile & " to " & sDestPath & sFile, Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                            CurrentTask = "Copy"
                            CurrentFile = sFile
                            My.Computer.FileSystem.CopyFile(sSrcPath & sFile, sDestPath & sFile)
                        Catch ex As Exception
                            LogHandler.WriteLogEntry("Copy file " & sSrcPath & sFile & ": Error.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug, Err)
                        End Try
                    Else
                        LogHandler.WriteLogEntry("File  " & sSrcPath & sFile & ": File was new.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                        FilesOrDirsChanged = True
                        Exit Sub
                    End If
                Else
                    LogHandler.WriteLogEntry("File  " & sDestPath & sFile & ": Exists in source and target.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                End If
            Catch ex As Exception
                LastError = Err.Description
                LogHandler.WriteLogEntry("File " & sSrcPath & sFile & ": Error.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug, Err)
            End Try
        Next i
    End Sub

    Public Function DeleteFolder_Sync(ByVal sSrcPath As String, ByVal sDestPath As String, ByVal Recursive As Boolean, ByVal Simulate As Boolean) As Boolean

        If Not Directory.Exists(sSrcPath) Then
            LogHandler.WriteLogEntry("Directory " & sDestPath & " does not exist in source directory " & sSrcPath, Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
            If Not Simulate Then
                Try
                    LogHandler.WriteLogEntry("Delete Directory " & sDestPath, Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                    My.Computer.FileSystem.DeleteDirectory(sDestPath, FileIO.DeleteDirectoryOption.DeleteAllContents)
                Catch ex As Exception
                    LogHandler.WriteLogEntry("Delete Directory " & sDestPath & ": Error.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug, Err)
                End Try
            Else
                LogHandler.WriteLogEntry("Directory " & sDestPath & ": Directory was deleted at source directory.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                FilesOrDirsChanged = True
                Return True
            End If
        Else
            LogHandler.WriteLogEntry("Directory " & sDestPath & " exists.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
        End If

        Try
            If Recursive Then
                LogHandler.WriteLogEntry("Get Directories from " & sDestPath, Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                Dim sDirs() As String = Directory.GetDirectories(sDestPath)
                Dim sDir As String
                For i As Integer = 0 To sDirs.Length - 1
                    If sDirs(i) <> sSrcPath Then
                        sDir = sDirs(i).Substring(sDirs(i).LastIndexOf("\") + 1)
                        DeleteFolder_Sync(sSrcPath & "\" & sDir & "\", sDirs(i).ToString & "\", Recursive, Simulate)
                    End If
                Next i
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function StartSync(ByVal sSrcPath As String, ByVal sDestPath As String, ByVal Recursive As Boolean, ByVal Simulate As Boolean) As Boolean
        FilesOrDirsChanged = False

        LogHandler.WriteLogEntry("SyncLib: Copy task started.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Advanced)
        If CopyFolder_Sync(sSrcPath, sDestPath, Recursive, Simulate) Then
            If FilesOrDirsChanged And Simulate Then
                LogHandler.WriteLogEntry("File-system changes detected.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Advanced)
                Return True
            End If
            LogHandler.WriteLogEntry("SyncLib: Delete task started.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Advanced)
            If DeleteFolder_Sync(sSrcPath, sDestPath, Recursive, Simulate) Then
                If FilesOrDirsChanged And Simulate Then
                    LogHandler.WriteLogEntry("File-system changes detected.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Advanced)
                Else
                    LogHandler.WriteLogEntry("No file-system changes detected.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Advanced)
                End If

                Return True
            Else
                Return False
            End If
        Else
            LogHandler.WriteLogEntry("Error apply file-system changes.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug, Err)
            Return False
        End If
    End Function
End Class
