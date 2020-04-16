Imports System.IO

Public Class SyncLib
    Public Function CopyFolder_Sync(ByVal sSrcPath As String, ByVal sDestPath As String, ByVal Recursive As Boolean) As Boolean
        Try
            If Not Directory.Exists(sDestPath) Then
                Directory.CreateDirectory(sDestPath)
            End If

            DeleteFilesDestination(sSrcPath, sDestPath)
            UpdateFilesDestination(sSrcPath, sDestPath)
            CopyNewFilesDestination(sSrcPath, sDestPath)

            If Recursive Then
                Dim sDirs() As String = Directory.GetDirectories(sSrcPath)
                Dim sDir As String
                For i As Integer = 0 To sDirs.Length - 1
                    If sDirs(i) <> sDestPath Then
                        sDir = sDirs(i).Substring(sDirs(i).LastIndexOf("\") + 1)
                        CopyFolder_Sync(sDirs(i).ToString & "\", sDestPath & sDir & "\", Recursive)
                    End If
                Next i
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub DeleteFilesDestination(ByVal sSrcPath As String, ByVal sDestPath As String)

        Dim sFiles() As String = Directory.GetFiles(sDestPath)
        Dim sFile As String

        For i As Integer = 0 To sFiles.Length - 1
            sFile = sFiles(i).Substring(sFiles(i).LastIndexOf("\") + 1)

            If Not File.Exists(sSrcPath & sFile) Then
                File.Delete(sDestPath & sFile)
            End If
        Next i
    End Sub

    Private Sub UpdateFilesDestination(ByVal sSrcPath As String, ByVal sDestPath As String)

        Dim sFiles() As String = Directory.GetFiles(sDestPath)
        Dim sFile As String

        For i As Integer = 0 To sFiles.Length - 1
            sFile = sFiles(i).Substring(sFiles(i).LastIndexOf("\") + 1)

            If File.Exists(sSrcPath & sFile) Then
                Dim DestFile As New FileInfo(sDestPath & sFile)
                Dim SrcFile As New FileInfo(sSrcPath & sFile)

                If DestFile.LastWriteTime <> SrcFile.LastWriteTime Then
                    File.Delete(sDestPath & sFile)
                    File.Copy(sSrcPath & sFile, sDestPath & sFile)
                End If
            End If
        Next i
    End Sub

    Private Sub CopyNewFilesDestination(ByVal sSrcPath As String, ByVal sDestPath As String)

        Dim sFiles() As String = Directory.GetFiles(sSrcPath)
        Dim sFile As String

        For i As Integer = 0 To sFiles.Length - 1
            sFile = sFiles(i).Substring(sFiles(i).LastIndexOf("\") + 1)

            If Not File.Exists(sDestPath & sFile) Then
                File.Copy(sSrcPath & sFile, sDestPath & sFile)
            End If
        Next i
    End Sub

    Public Function DeleteFolder_Sync(ByVal sSrcPath As String, ByVal sDestPath As String, ByVal Recursive As Boolean) As Boolean

        If Not Directory.Exists(sSrcPath) Then
            Directory.Delete(sDestPath, True)
        End If

        Try
            If Recursive Then
                Dim sDirs() As String = Directory.GetDirectories(sDestPath)
                Dim sDir As String
                For i As Integer = 0 To sDirs.Length - 1
                    If sDirs(i) <> sSrcPath Then
                        sDir = sDirs(i).Substring(sDirs(i).LastIndexOf("\") + 1)
                        DeleteFolder_Sync(sSrcPath & sDir & "\", sDirs(i).ToString & "\", Recursive)
                    End If
                Next i
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function StartSync(ByVal sSrcPath As String, ByVal sDestPath As String, ByVal Recursive As Boolean)
        If CopyFolder_Sync(sSrcPath, sDestPath, Recursive) Then
            If DeleteFolder_Sync(sSrcPath, sDestPath, Recursive) Then
                Return True
            End If
        End If

        Return False
    End Function
End Class
