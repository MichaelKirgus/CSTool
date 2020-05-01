Imports CSToolApplicationSettingsLib
Imports CSToolApplicationSettingsManager
Imports CSToolEnvironmentManager
Imports CSToolLogLib
Imports CSToolWindowManager

Public Class Form1
    Public LogManager As New LogLib
    Public WindowManagerHandler As New WindowManager
    Public ApplicationSettingManager As New ApplicationSettingsManager
    Public ApplicationSettings As New ApplicationSettings
    Public EnvironmentManager As New EnvironmentManager

    Public PluginFilePath As String = ""
    Public UserWorkspaceSetting As String = "Default"

    Public Function ProccessCommandLineArgsAppBase(ByVal CmdArgs As String()) As Boolean
        Try
            Dim arglist As List(Of String)
            arglist = CmdArgs.ToList

            For ind = 0 To arglist.Count - 1
                If arglist(ind).ToLower = "/logtofile" Then
                    LogManager.LogSettings.LogToFile = True
                    LogManager.LogSettings.LogToFilePath = (arglist(ind + 1))
                    LogManager.ReInitLogSystem()
                End If
                If arglist(ind).ToLower = "/logeventlog" Then
                    LogManager.LogSettings.LogToEventLog = True
                    LogManager.ReInitLogSystem()
                End If
                If arglist(ind).ToLower = "/environmentplugindir" Then
                    Dim EnvDirInfo As New IO.DirectoryInfo(arglist(ind + 1))
                    WindowManagerHandler.EnvironmentPluginDir = EnvDirInfo.FullName
                End If
                If arglist(ind).ToLower = "/credentialplugindir" Then
                    Dim CredDirInfo As New IO.DirectoryInfo(arglist(ind + 1))
                    WindowManagerHandler.CredentialPluginDir = CredDirInfo.FullName
                End If
                If arglist(ind).ToLower = "/plugin" Then
                    PluginFilePath = (arglist(ind + 1))
                End If
                If arglist(ind).ToLower = "/workspace" Then
                    UserWorkspaceSetting = (arglist(ind + 1))
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub HostnameOrIPCtl_KeyDown(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub SearchInNewWindowToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub SearchInNewInstanceToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ToolStripButton4_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LogManager.InitLogSystem()
        WindowManagerHandler._LogManager = LogManager
        ProccessCommandLineArgsAppBase(Environment.GetCommandLineArgs)
        Dim EnvDirInfo As New IO.DirectoryInfo(ApplicationSettings.EnvironmentPluginDir)
        Dim CredDirInfo As New IO.DirectoryInfo(ApplicationSettings.CredentialPluginDir)
        WindowManagerHandler.EnvironmentPluginDir = EnvDirInfo.FullName
        WindowManagerHandler.CredentialPluginDir = CredDirInfo.FullName
        WindowManagerHandler.PluginManager.LoadPluginFile(PluginFilePath)
        WindowManagerHandler._DockingContent = CSDockPanelHosting

    End Sub
End Class
