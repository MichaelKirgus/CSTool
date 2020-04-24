Public Class LauncherLib
    Public Function CreateShortCut(ByVal TargetName As String, ByVal ShortCutPath As String, ByVal ShortCutName As String, ByVal WorkingDir As String) As Boolean
        Dim oShell As Object
        Dim oLink As Object

        Try
            oShell = CreateObject("WScript.Shell")
            oLink = oShell.CreateShortcut(ShortCutPath & "\" & ShortCutName & ".lnk")

            oLink.TargetPath = TargetName
            oLink.WorkingDirectory = WorkingDir
            oLink.WindowStyle = 1
            oLink.Save()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ShortcutExists(ByVal ShortCutPath As String) As Boolean
        Try
            If IO.File.Exists(ShortCutPath & ".ink") Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function IsCurrentUserAdmin() As Boolean
        If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub ShowElevatedAppWarningMsg()
        MsgBox("It is not allowed to run this application as elevated user. Please execute this application as normal user.", MsgBoxStyle.Exclamation)
    End Sub
End Class
