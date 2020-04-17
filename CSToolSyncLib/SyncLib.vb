Imports System.IO
Imports CSToolLogLib

Public Class SyncLib
    Public FilesOrDirsChanged As Boolean = False
    Public LogHandler As New LogLib

    Public Function CopyFolder_Sync(ByVal sSrcPath As String, ByVal sDestPath As String, ByVal Recursive As Boolean, ByVal Simulate As Boolean) As Boolean
        Try
            If Not Directory.Exists(sDestPath) Then
                LogHandler.WriteLogEntry("Directory " & sDestPath & " does not exist.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                If Not Simulate Then
                    Try
                        LogHandler.WriteLogEntry("Create Directory " & sDestPath, Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
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
                For i As Integer = 0 To sDirs.Length - 1
                    If sDirs(i) <> sDestPath Then
                        sDir = sDirs(i).Substring(sDirs(i).LastIndexOf("\") + 1)
                        Debug.WriteLine(sDir)
                        CopyFolder_Sync(sDirs(i).ToString, sDestPath & sDir, Recursive, Simulate)
                    End If
                Next i
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

            If Not File.Exists(sSrcPath & sFile) Then
                If Not Simulate Then
                    Try
                        LogHandler.WriteLogEntry("Delete file " & sSrcPath & sFile & " ...", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                        File.Delete(sDestPath & sFile)
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
        Next i
    End Sub

    Private Sub UpdateFilesDestination(ByVal sSrcPath As String, ByVal sDestPath As String, ByVal Simulate As Boolean)

        Dim sFiles() As String = Directory.GetFiles(sDestPath)
        Dim sFile As String

        For i As Integer = 0 To sFiles.Length - 1
            sFile = sFiles(i).Substring(sFiles(i).LastIndexOf("\") + 1)

            If File.Exists(sSrcPath & sFile) Then
                Dim DestFile As New FileInfo(sDestPath & sFile)
                Dim SrcFile As New FileInfo(sSrcPath & sFile)

                If DestFile.LastWriteTime <> SrcFile.LastWriteTime Then
                    If Not Simulate Then
                        Try
                            LogHandler.WriteLogEntry("Delete file " & sDestPath & sFile & " ...", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                            File.Delete(sDestPath & sFile)
                            LogHandler.WriteLogEntry("Copy file " & sSrcPath & sFile & " to " & sDestPath & sFile, Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                            File.Copy(sSrcPath & sFile, sDestPath & sFile)
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
        Next i
    End Sub

    Private Sub CopyNewFilesDestination(ByVal sSrcPath As String, ByVal sDestPath As String, ByVal Simulate As Boolean)

        Dim sFiles() As String = Directory.GetFiles(sSrcPath)
        Dim sFile As String

        For i As Integer = 0 To sFiles.Length - 1
            sFile = sFiles(i).Substring(sFiles(i).LastIndexOf("\") + 1)

            If Not File.Exists(sDestPath & sFile) Then
                If Not Simulate Then
                    Try
                        LogHandler.WriteLogEntry("Copy file " & sSrcPath & sFile & " to " & sDestPath & sFile, Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                        File.Copy(sSrcPath & sFile, sDestPath & sFile)
                    Catch ex As Exception
                        LogHandler.WriteLogEntry("Copy file " & sSrcPath & sFile & ": Error.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug, Err)
                    End Try
                Else
                    LogHandler.WriteLogEntry("File  " & sSrcPath & sFile & ": File was new.", Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                    FilesOrDirsChanged = True
                    Exit Sub
                End If
            End If
        Next i
    End Sub

    Public Function DeleteFolder_Sync(ByVal sSrcPath As String, ByVal sDestPath As String, ByVal Recursive As Boolean, ByVal Simulate As Boolean) As Boolean

        If Not Directory.Exists(sSrcPath) Then
            If Not Simulate Then
                Try
                    LogHandler.WriteLogEntry("Delete Directory " & sDestPath, Me.GetType, LogSettings.LogEntryTypeEnum.Info, LogSettings.LogEntryLevelEnum.Debug)
                    Directory.Delete(sDestPath, True)
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
                Dim sDirs() As String = Directory.GetDirectories(sDestPath)
                Dim sDir As String
                For i As Integer = 0 To sDirs.Length - 1
                    If sDirs(i) <> sSrcPath Then
                        sDir = sDirs(i).Substring(sDirs(i).LastIndexOf("\") + 1)
                        DeleteFolder_Sync(sSrcPath & sDir & "\", sDirs(i).ToString & "\", Recursive, Simulate)
                    End If
                Next i
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function StartSync(ByVal sSrcPath As String, ByVal sDestPath As String, ByVal Recursive As Boolean, ByVal Simulate As Boolean) As Boolean
        If CopyFolder_Sync(sSrcPath, sDestPath, Recursive, Simulate) Then
            If FilesOrDirsChanged And Simulate Then
                Return True
            End If
            If DeleteFolder_Sync(sSrcPath, sDestPath, Recursive, Simulate) Then
                Return True
            End If
        End If

        Return False
    End Function
End Class
