Public Class LauncherLib
    Public Function CreateShortCut(ByVal TargetName As String, ByVal ShortCutPath As String, ByVal ShortCutName As String) As Boolean
        Dim oShell As Object
        Dim oLink As Object

        Try
            oShell = CreateObject("WScript.Shell")
            oLink = oShell.CreateShortcut(ShortCutPath & "\" & ShortCutName & ".lnk")

            oLink.TargetPath = TargetName
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
End Class
