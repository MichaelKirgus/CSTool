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

    Public Sub ShowMainAppAlreadyRunningMsg()
        MsgBox("The main application is running! Please use the main application to create new window/application instances.", MsgBoxStyle.Exclamation)
    End Sub

    Public Sub ShowUserCanNotWriteInUserProfilePathMsg()
        MsgBox("The user can not write in the user profile path.", MsgBoxStyle.Exclamation)
    End Sub

    Public Function IsApplicationRunning(ByVal ProcessName As String) As Boolean
        Try
            Dim currinstance As Process()
            currinstance = Process.GetProcessesByName(ProcessName)

            If Not currinstance.Count = 0 Then
                Return True
            Else
                Return False
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
