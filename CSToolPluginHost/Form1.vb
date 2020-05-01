Imports CSToolApplicationSettingsLib
Imports CSToolApplicationSettingsManager
Imports CSToolEnvironmentManager
Imports CSToolHostWindow
Imports CSToolLogGUILib
Imports CSToolLogLib
Imports CSToolWindowManager

Public Class Form1
    Public LogManager As New LogLib
    Public WindowManagerHandler As New WindowManager
    Public ApplicationSettingManager As New ApplicationSettingsManager
    Public ApplicationSettings As New ApplicationSettings
    Public EnvironmentManager As New EnvironmentManager

    Public PluginFilePath As String = ""
    Public PluginSettingsfile As String = ""
    Public UserWorkspaceSetting As String = "Default"
    Public InstanceTag As String = ""
    Public UserProfilePath As String = ""
    Public InitHostname As String = ""
    Public IsNonPersistent As Boolean = False

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
                If arglist(ind).ToLower = "/pluginsettings" Then
                    PluginSettingsfile = (arglist(ind + 1))
                End If
                If arglist(ind).ToLower = "/workspace" Then
                    UserWorkspaceSetting = (arglist(ind + 1))
                End If
                If arglist(ind).ToLower = "/instancetag" Then
                    InstanceTag = (arglist(ind + 1))
                End If
                If arglist(ind).ToLower = "/hostname" Then
                    InitHostname = (arglist(ind + 1))
                End If
                If arglist(ind).ToLower = "/userprofilepath" Then
                    UserProfilePath = (arglist(ind + 1))
                End If
                If arglist(ind).ToLower = "/nonpersistent" Then
                    IsNonPersistent = True
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub HostnameOrIPCtl_KeyDown(sender As Object, e As KeyEventArgs)
        WindowManagerHandler.SendRaiseActionsToPlugins(HostnameOrIPCtl.Text)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LogManager.InitLogSystem()
        WindowManagerHandler._LogManager = LogManager
        ProccessCommandLineArgsAppBase(Environment.GetCommandLineArgs)

        If Not PluginFilePath = "" And Not PluginSettingsfile = "" And Not UserProfilePath = "" Then
            WindowManagerHandler._UserSettingName = UserWorkspaceSetting
            WindowManagerHandler._UserProfilePath = UserProfilePath
            Dim EnvDirInfo As New IO.DirectoryInfo(ApplicationSettings.EnvironmentPluginDir)
            Dim CredDirInfo As New IO.DirectoryInfo(ApplicationSettings.CredentialPluginDir)
            WindowManagerHandler.EnvironmentPluginDir = EnvDirInfo.FullName
            WindowManagerHandler.CredentialPluginDir = CredDirInfo.FullName
            WindowManagerHandler.InitAllEnvironmentPlugins()
            WindowManagerHandler.InitAllCredentialPlugins()
            WindowManagerHandler.LoadEnvironmentPluginsSettings(UserWorkspaceSetting)
            WindowManagerHandler.LoadCredentialPluginsSettings(UserWorkspaceSetting)
            WindowManagerHandler._EnvironmentEntries = WindowManagerHandler.GetEnvironmentEntriesFromEnvironmentPlugins()
            WindowManagerHandler._CredentialEntries = WindowManagerHandler.GetCredentialEntriesFromCredentialPlugins(WindowManagerHandler.PluginManager.PluginCollection, True)
            WindowManagerHandler.PluginManager.LoadPluginFile(PluginFilePath)
            WindowManagerHandler._DockingContent = CSDockPanelHosting
            WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, InitHostname, WindowManagerHandler.GetPluginsByType(CSToolPluginLib.ICSToolInterface.PluginTypeEnum.GUIWindow, WindowManagerHandler.PluginManager.PluginCollection)(0).PluginName,
                                                      WindowManagerHandler.PluginManager.PluginCollection, UserWorkspaceSetting, WeifenLuo.WinFormsUI.Docking.DockState.Document, False, PluginSettingsfile)

            Dim HostWindow As DockingHostWindow
            HostWindow = WindowManagerHandler.GetAllDockingWindows(0)
            HostWindow.CloseButton = False
            HostWindow.CloseButtonVisible = False

            If Not InitHostname = "" Then
                HostnameOrIPCtl.Text = InitHostname
                WindowManagerHandler.SendRaiseActionsToPlugins(InitHostname)
            End If

            SetWindowTitle(InitHostname)
        Else
            MsgBox("No valid command line arguments passed.", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Public Sub SetWindowTitle(ByVal Clientname As String)
        Dim pluginwindowtitle As String
        pluginwindowtitle = WindowManagerHandler.GetAllDockingWindows(0).TabText

        If Clientname = "" Then
            If Not pluginwindowtitle = "" Then
                Me.Text = pluginwindowtitle
            Else
                Me.Text = "CSTool"
            End If
        Else
            Me.Text = Clientname.ToUpper
            If Not pluginwindowtitle = "" Then
                Me.Text += " - " & pluginwindowtitle
            End If
        End If
        If IsNonPersistent Then
            Me.Text += " [Non-Persistent]"
        End If
        If Not InstanceTag = "" Then
            Me.Text += " - " & InstanceTag
        End If
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        WindowManagerHandler.SendRefreshToPlugins(True)
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Dim LogGUIInstance As New LogForm
        LogGUIInstance._LogLibInstance = LogManager
        LogGUIInstance.Show()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If IsNonPersistent = False Then
            WindowManagerHandler.SaveAllGUIPluginSettings(UserWorkspaceSetting)
        End If
    End Sub

    Private Sub CSDockPanelHosting_Paint(sender As Object, e As PaintEventArgs) Handles CSDockPanelHosting.Paint
        SetWindowTitle(HostnameOrIPCtl.Text)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        WindowManagerHandler.SendRaiseActionsToPlugins(HostnameOrIPCtl.Text)
    End Sub
End Class
