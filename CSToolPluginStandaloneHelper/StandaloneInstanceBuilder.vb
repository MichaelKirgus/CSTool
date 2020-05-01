Public Class StandaloneInstanceBuilder
    Public Function SpawnStandaloneInstance(ByVal PluginFilePath As String, ByVal PluginSettingFile As String, ByVal UserProfilePath As String, Optional ByVal EnvironmentPluginFolder As String = "", Optional ByVal CredentialPluginFolder As String = "", Optional IsNonPersistent As Boolean = False, Optional ByVal UserSetting As String = "Default", Optional ByVal InstanceTag As String = "", Optional InitialHostname As String = "", Optional ByVal WindowState As ProcessWindowStyle = ProcessWindowStyle.Normal) As Boolean
        Try
            Dim newhost As New Process
            newhost.StartInfo.FileName = "CSToolPluginHost.exe"

            Dim args As String = ""
            If Not EnvironmentPluginFolder = "" Then
                args += "/environmentplugindir " & My.Resources.pathsep & EnvironmentPluginFolder & My.Resources.pathsep & " "
            End If
            If Not CredentialPluginFolder = "" Then
                args += "/credentialplugindir " & My.Resources.pathsep & CredentialPluginFolder & My.Resources.pathsep & " "
            End If
            args += "/plugin " & My.Resources.pathsep & PluginFilePath & My.Resources.pathsep & " "
            args += "/pluginsettings " & My.Resources.pathsep & PluginSettingFile & My.Resources.pathsep & " "
            args += "/userprofilepath " & My.Resources.pathsep & UserProfilePath & My.Resources.pathsep & " "
            If IsNonPersistent Then
                args += "/nonpersistent "
            End If
            If Not InstanceTag = "" Then
                args += "/instancetag " & My.Resources.pathsep & InstanceTag & My.Resources.pathsep & " "
            End If
            If Not InitialHostname = "" Then
                args += "/hostname " & My.Resources.pathsep & InitialHostname & My.Resources.pathsep & " "
            End If

            If args.EndsWith(" ") Then
                args = args.Substring(0, args.Length - 1)
            End If

            newhost.StartInfo.WindowStyle = WindowState
            If newhost.Start() Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
