'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.

Imports System.ComponentModel
Imports CSCustomActionHelper
Imports CSTemplateManager
Imports CSToolApplicationSettingsLib
Imports CSToolApplicationSettingsManager
Imports CSToolEnvironmentManager
Imports CSToolHostWindow
Imports CSToolLogLib
Imports CSToolPingHelper
Imports CSToolPluginLib
Imports CSToolUserSettingsLib
Imports CSToolUserSettingsManager
Imports CSToolWindowManager

Public Class MainForm
    Public LogManager As New LogLib
    Public WindowManagerHandler As New WindowManager
    Public ApplicationSettingManager As New ApplicationSettingsManager
    Public ApplicationSettings As New ApplicationSettings
    Public UserSettingManager As New UserSettingsManager
    Public EnvironmentManager As New EnvironmentManager
    Public PingCheckManager As New PingHelper
    Public UserSettings As New UserSettings
    Public CustomActionsHandler As New CustomActionHelper
    Public CustomActionsAutostartHandler As New CustomActionHelper
    Public UserTemplateManager As New TemplateManager

    Public CurrentUserSettingName As String = ""
    Public CurrentUsername As String = ""
    Public CurrentUserProfilePath As String = ""
    Public IsFormLoading As Boolean = True
    Public IsChild As Boolean = False
    Public IsNonPersistent As Boolean = False
    Public RestoreSettings As Boolean = True
    Public LastWindowState As FormWindowState = FormWindowState.Normal

    Public CurrentLoadActionState As String = ""

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadForm()
    End Sub

    Public Function GetAllChildForms() As IEnumerable(Of MainForm)
        Try
            Dim frmcoll As IEnumerable(Of MainForm)
            frmcoll = Application.OpenForms.OfType(Of MainForm)

            Return frmcoll
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function SpawnNewProcessInstance(Optional ByVal UserSettingName As String = "", Optional ByVal NonPersistent As Boolean = False, Optional ByVal HostOrIP As String = "") As Boolean
        Try
            Dim newinst As New Process
            newinst.StartInfo.FileName = Application.ExecutablePath
            If Not UserSettingName = "" Then
                newinst.StartInfo.Arguments = "/settingsname " & UserSettingName
            End If
            If NonPersistent Then
                newinst.StartInfo.Arguments += "/nonpersistent"
            End If
            If Not HostOrIP = "" Then
                newinst.StartInfo.Arguments += " /hostname " & HostOrIP
            End If

            Return newinst.Start()
        Catch ex As Exception
            Return False
        End Try
    End Function

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
                If arglist(ind).ToLower = "/appsettingspath" Then
                    ApplicationSettings = ApplicationSettingManager.LoadSettings(arglist(ind + 1))
                End If
                If arglist(ind).ToLower = "/environmentplugindir" Then
                    Dim EnvDirInfo As New IO.DirectoryInfo(arglist(ind + 1))
                    WindowManagerHandler.EnvironmentPluginDir = EnvDirInfo.FullName
                End If
                If arglist(ind).ToLower = "/credentialplugindir" Then
                    Dim CredDirInfo As New IO.DirectoryInfo(arglist(ind + 1))
                    WindowManagerHandler.CredentialPluginDir = CredDirInfo.FullName
                End If
                If arglist(ind).ToLower = "/guiplugindir" Then
                    Dim GUIDirInfo As New IO.DirectoryInfo(arglist(ind + 1))
                    WindowManagerHandler.GUIPluginDir = GUIDirInfo.FullName
                End If
                If arglist(ind).ToLower = "/usertemplatesdir" Then
                    ApplicationSettings.UserTemplatesDir = (arglist(ind + 1))
                End If
                If arglist(ind).ToLower = "/userinitialtemplatedir" Then
                    ApplicationSettings.UserInitialTemplateDir = (arglist(ind + 1))
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ProccessCommandLineArgsUserSettings(ByVal CmdArgs As String()) As Boolean
        Try
            Dim arglist As List(Of String)
            arglist = CmdArgs.ToList

            For ind = 0 To arglist.Count - 1
                If arglist(ind).ToLower = "/usersettingspath" Then
                    ApplicationSettings.UserProfileDir = arglist(ind + 1)
                End If
                If arglist(ind).ToLower = "/nonpersistent" Then
                    IsNonPersistent = True
                End If
                If arglist(ind).ToLower = "/norestore" Then
                    RestoreSettings = False
                End If
                If arglist(ind).ToLower = "/persistsettingsname" Then
                    UserSettings.LastSettingName = arglist(ind + 1)
                End If
                If arglist(ind).ToLower = "/settingsname" Then
                    CurrentUserSettingName = arglist(ind + 1)
                End If
                If arglist(ind).ToLower = "/username" Then
                    CurrentUsername = arglist(ind + 1)
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ProccessCommandLineArgsAppearence(ByVal CmdArgs As String()) As Boolean
        Try
            Dim arglist As List(Of String)
            arglist = CmdArgs.ToList

            For ind = 0 To arglist.Count - 1
                If arglist(ind).ToLower = "/windowstate" Then
                    If arglist(ind + 1).ToLower = "normal" Then
                        Me.WindowState = FormWindowState.Normal
                    End If
                    If arglist(ind + 1).ToLower = "maximized" Then
                        Me.WindowState = FormWindowState.Maximized
                    End If
                    If arglist(ind + 1).ToLower = "minimized" Then
                        Me.WindowState = FormWindowState.Minimized
                    End If
                End If
                If arglist(ind).ToLower = "/showlog" Then
                    ShowLogForm()
                End If
                If arglist(ind).ToLower = "/showenvironmentvars" Then
                    ShowEnvironmentVariablesForm()
                End If
                If arglist(ind).ToLower = "/showenvironmenttester" Then
                    ShwoEnvironmentTesterForm()
                End If
                If arglist(ind).ToLower = "/hostname" Then
                    HostnameOrIPCtl.Text = arglist(ind + 1)
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub LoadForm()
        'Initialize plugin and window manager
        CurrentLoadActionState = "Initialize Log manager..."
        LogManager.InitLogSystem()
        WindowManagerHandler._LogManager = LogManager
        WindowManagerHandler._DockingContent = CSDockPanelHosting

        'Init DockPanel extender
        CurrentLoadActionState = "Initialize docking design..."
        VS2015DarkTheme1.Extender.FloatWindowFactory = New CustomFloatWindowFactory

        'Search for AppSettings...
        CurrentLoadActionState = "Searching app settings..."
        If Not IO.File.Exists(ApplicationSettingManager.GetAppSettingsFilePath) Then
            'Generate new empty default app settings
            ApplicationSettingManager.SaveSettings(ApplicationSettings, ApplicationSettingManager.GetAppSettingsFilePath)
        Else
            'Load app settings
            CurrentLoadActionState = "Loading app settings..."
            ApplicationSettings = ApplicationSettingManager.LoadSettings(ApplicationSettingManager.GetAppSettingsFilePath)
        End If

        'Set logging settings from app settings to log manager
        LogManager.LogSettings = ApplicationSettings.LogSettings
        LogManager.ReInitLogSystem()

        'Load first command line args
        CurrentLoadActionState = "Reading command line args (step 1)..."
        ProccessCommandLineArgsAppBase(Environment.GetCommandLineArgs)

        'Load app global settings
        Dim EnvDirInfo As New IO.DirectoryInfo(ApplicationSettings.EnvironmentPluginDir)
        Dim CredDirInfo As New IO.DirectoryInfo(ApplicationSettings.CredentialPluginDir)
        Dim GUIDirInfo As New IO.DirectoryInfo(ApplicationSettings.GUIPluginDir)

        WindowManagerHandler.EnvironmentPluginDir = EnvDirInfo.FullName
        WindowManagerHandler.CredentialPluginDir = CredDirInfo.FullName
        WindowManagerHandler.GUIPluginDir = GUIDirInfo.FullName

        'Init plugins and fill GUI with windows
        CurrentLoadActionState = "Loading plugin collection..."
        WindowManagerHandler.InitAllPlugins()
        WindowManagerHandler.FillGUIWithWindowGUIPlugins(ToolStripComboBox1)

        'Load next command line args (to check if restore settings or we are in persistent mode)
        CurrentLoadActionState = "Reading command line args (step 2)..."
        ProccessCommandLineArgsUserSettings(Environment.GetCommandLineArgs)

        If RestoreSettings Then
            'Load user (default) settings
            CurrentLoadActionState = "Searching for user settings..."
            If Not IO.File.Exists(UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, True, CurrentUsername)) Then
                IO.Directory.CreateDirectory(UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername))
                'Check, if we have an valid initial template dir
                If IO.Directory.Exists(ApplicationSettings.UserInitialTemplateDir) Then
                    'Copy content to new user profile path
                    CurrentLoadActionState = "Copy user settings..."
                    My.Computer.FileSystem.CopyDirectory(ApplicationSettings.UserInitialTemplateDir, UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername), True)
                Else
                    'No initial template, start with an empty one and save it.
                    UserSettingManager.SaveSettings(UserSettings, UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, True, CurrentUsername))
                End If
            Else
                'We hava an valid settings file, load it.
                CurrentLoadActionState = "Loading user settings..."
                UserSettings = UserSettingManager.LoadSettings(UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, True, CurrentUsername))
            End If

            'Load logging settings to log manager
            LogManager.LogSettings = UserSettings.LogSettings
            LogManager.ReInitLogSystem()

            'Create user (default) template folder if not exists
            If Not IO.Directory.Exists(UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername) & "\" & UserSettings.SettingName) Then
                IO.Directory.CreateDirectory(UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False) & "\" & UserSettings.SettingName)
            End If

            'Set global user settings variable
            Dim dirinf As New IO.DirectoryInfo(UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername))
            CurrentUserProfilePath = dirinf.FullName

            'Set user profile in window manager
            CurrentLoadActionState = "Loading user settings in window manager..."
            WindowManagerHandler._UserProfilePath = UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername)

            If Not CurrentUserSettingName = "" Then
                'Create (default) user settings folder if not exists
                If Not IO.Directory.Exists(UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername) & "\" & CurrentUserSettingName) Then
                    IO.Directory.CreateDirectory(UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername) & "\" & CurrentUserSettingName)
                End If
                'Create (default) docking layout file if not exists
                If Not IO.File.Exists(UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername) & "\" & CurrentUserSettingName & "\Layout.xml") Then
                    WindowManagerHandler.SaveWindowLayoutToXML("Layout.xml", True, CurrentUserSettingName)
                End If
                'Set specific user setting tag (if instance or window was spawned from parent)
                WindowManagerHandler._UserSettingName = CurrentUserSettingName
            Else
                'Create (default) user settings folder if not exists
                If Not IO.Directory.Exists(UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername) & "\" & UserSettings.SettingName) Then
                    IO.Directory.CreateDirectory(UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername) & "\" & UserSettings.SettingName)
                End If
                'Create (default) docking layout file if not exists
                If Not IO.File.Exists(UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername) & "\" & UserSettings.SettingName & "\Layout.xml") Then
                    WindowManagerHandler.SaveWindowLayoutToXML()
                End If
                'Set last user setting to window manager (to restore settings)
                WindowManagerHandler._UserSettingName = UserSettings.LastSettingName
            End If

            'Get Window size and location from settings
            CurrentLoadActionState = "Restore form size and location..."

            If Not CurrentUserSettingName = "" And Not CurrentUserSettingName = "Default" Then
                'Get workspace to load
                If Not UserSettings.UserTemplates.Count = 0 Then
                    For index = 0 To UserSettings.UserTemplates.Count - 1
                        If UserSettings.UserTemplates(index).SettingName = UserSettings.LastSettingName Then
                            'Found setting name, load position and size
                            WindowManagerHandler.LoadFormLocationAndSize(Me, True, True, UserSettings.UserTemplates(index).LastWindowSize.Height,
                                                                         UserSettings.UserTemplates(index).LastWindowSize.Width,
                                                                         UserSettings.UserTemplates(index).LastWindowLocation.X,
                                                                         UserSettings.UserTemplates(index).LastWindowLocation.Y,
                                                                         UserSettings.UserTemplates(index).LastNormalWindowSize.Height,
                                                                         UserSettings.UserTemplates(index).LastNormalWindowSize.Width,
                                                                         UserSettings.UserTemplates(index).LastNormalWindowLocation.X,
                                                                         UserSettings.UserTemplates(index).LastNormalWindowLocation.Y,
                                                                         UserSettings.UserTemplates(index).LastWindowState)

                            Exit For
                        End If
                    Next
                End If
            Else
                WindowManagerHandler.LoadFormLocationAndSize(Me, True, True, UserSettings.LastWindowSize.Height,
                                                             UserSettings.LastWindowSize.Width,
                                                             UserSettings.LastWindowLocation.X,
                                                             UserSettings.LastWindowLocation.Y,
                                                             UserSettings.LastNormalWindowSize.Height,
                                                             UserSettings.LastNormalWindowSize.Width,
                                                             UserSettings.LastNormalWindowLocation.X,
                                                             UserSettings.LastNormalWindowLocation.Y,
                                                             UserSettings.LastWindowState)
            End If

            'Check if additional workspaces or instances should be spawn
            If Not UserSettings.UserTemplates.Count = 0 And IsChild = False And CurrentUserSettingName = "" Then
                CurrentLoadActionState = "Restore additional workspaces..."
                For index = 0 To UserSettings.UserTemplates.Count - 1
                    If UserSettings.UserTemplates(index).Autostart Then
                        If UserSettings.UserTemplates(index).StartType = UserSettings.StartTypeEnum.NewWindow Then
                            OpenNewWindow(False, UserSettings.UserTemplates(index).SettingName)
                        Else
                            SpawnNewProcessInstance(UserSettings.UserTemplates(index).SettingName)
                        End If
                    End If
                Next
            End If
        Else
            'Set global user settings variable
            Dim dirinf As New IO.DirectoryInfo(UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername))
            CurrentUserProfilePath = dirinf.FullName

            'Set user profile in window manager
            WindowManagerHandler._UserProfilePath = UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername)

            'Set last user setting to window manager (to restore settings)
            WindowManagerHandler._UserSettingName = UserSettings.LastSettingName

            WindowManagerHandler._IsNonPersistent = True
        End If

        'Check for additional command line args...
        CurrentLoadActionState = "Reading command line args (step 3)..."
        ProccessCommandLineArgsAppearence(Environment.GetCommandLineArgs)

        'Load environment plugin settings from file (to restore settings)
        CurrentLoadActionState = "Loading environment plugin settings..."
        WindowManagerHandler.LoadEnvironmentPluginsSettings(WindowManagerHandler._UserSettingName)

        'Load environment variables from plugins (after loading settings)
        CurrentLoadActionState = "Loading environment variables from plugins..."
        WindowManagerHandler._EnvironmentEntries = WindowManagerHandler.GetEnvironmentEntriesFromEnvironmentPlugins

        'Load credentials plugins settings
        CurrentLoadActionState = "Loading credential plugin settings..."
        WindowManagerHandler.LoadCredentialPluginsSettings(WindowManagerHandler._UserSettingName)

        'show login form if no login credentials where set
        CurrentLoadActionState = "Check for valid credential plugin settings..."
        WindowManagerHandler.GetCredentialEntriesFromCredentialPlugins(WindowManagerHandler.PluginManager.PluginCollection, False)

        If RestoreSettings Then
            'Load last user setting (template)
            If IO.File.Exists(UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername) & "\" & WindowManagerHandler._UserSettingName & "\Layout.xml") Then
                CurrentLoadActionState = "Loading workspace layout..."
                WindowManagerHandler.LoadWindowLayoutFromXML("Layout.xml", True, WindowManagerHandler._UserSettingName)
            End If
        End If

        'Create environment variables cache and load it to all open gui plugin instances
        CurrentLoadActionState = "Loading environment variables cache..."
        EnvironmentManager.SetEnvironmentVarsInPlugins(WindowManagerHandler.PluginManager.PluginCollection, CSDockPanelHosting.Contents)

        'Load custom actions
        CurrentLoadActionState = "Loading custom actions..."
        CustomActionsHandler._ShowWarningOnCustomActions = UserSettings.ShowWarningOnCustomActions
        CustomActionsHandler._CustomActionsCollection = UserSettings.CustomActions
        CustomActionsHandler._LogManager = LogManager
        CustomActionsHandler.LoadCustomItems(CustomItemsContext)

        If Not CustomActionsHandler._CustomActionsCollection.Count = 0 Then
            'Load environment variables to custom actions class
            CurrentLoadActionState = "Loading environment variables for custom actions..."
            CustomActionsHandler._EnvironmentRuntimeVariables = EnvironmentManager.GetEnvironmentVarsFromPlugins(WindowManagerHandler.PluginManager.PluginCollection)
        End If

        'Load custom actions (autostart)...
        CurrentLoadActionState = "Loading custom autostart actions..."
        CustomActionsAutostartHandler._ShowWarningOnCustomActions = UserSettings.ShowWarningOnCustomActions
        CustomActionsAutostartHandler._CustomActionsCollection = UserSettings.CustomAutostartActions
        CustomActionsAutostartHandler._LogManager = LogManager

        If (Not CustomActionsAutostartHandler._CustomActionsCollection.Count = 0) And UserSettings.EnableCustomAutostartActions Then
            'Load environment variables to custom actions class
            CurrentLoadActionState = "Loading environment variables for custom autostart actions..."
            CustomActionsAutostartHandler._EnvironmentRuntimeVariables = EnvironmentManager.GetEnvironmentVarsFromPlugins(WindowManagerHandler.PluginManager.PluginCollection)

            'Run Tasks
            CurrentLoadActionState = "Execute autostart actions..."
            For index = 0 To CustomActionsAutostartHandler._CustomActionsCollection.Count - 1
                CurrentLoadActionState = "Execute " & (index + 1) & " task of " & CustomActionsAutostartHandler._CustomActionsCollection.Count & " ..."
                Application.DoEvents()
                CustomActionsAutostartHandler.RaiseCustomActionAsync.RunWorkerAsync(CustomActionsAutostartHandler._CustomActionsCollection(index))
                While CustomActionsAutostartHandler.RaiseCustomActionAsync.IsBusy
                    Threading.Thread.Sleep(50)
                End While
            Next
        End If

        'Load user templates (to load new window instance faster)
        CurrentLoadActionState = "Loading user templates..."
        UserTemplateManager.CurrentTemplates = UserTemplateManager.GetTemplates(ApplicationSettings.UserTemplatesDir, WindowManagerHandler.PluginManager.PluginCollection)

        'Set initial window state for raising windows state events
        LastWindowState = Me.WindowState

        'Resume form and controls events
        IsFormLoading = False
        CSDockPanelHosting.CausesValidation = True

        'Set window title (child, non-persistent-mode, etc...)
        CurrentLoadActionState = "Set window title..."
        SetWindowTitle("")

        'Check, if hostname/ip was set via command line args and raise actions
        If Not HostnameOrIPCtl.Text = "" Then
            PerformRaiseActions()
        End If

        CurrentLoadActionState = "Finished!"
    End Sub

    Public Sub SaveSettings()
        'Save environment variables settings to file
        WindowManagerHandler.SaveEnvironmentPluginsSettings(WindowManagerHandler._UserSettingName)

        'Save credentail plugin settings to file
        WindowManagerHandler.SaveCredentialPluginsSettings(WindowManagerHandler._UserSettingName)

        'Save window gui plugin settings
        WindowManagerHandler.SaveAllGUIPluginSettings(WindowManagerHandler._UserSettingName)

        'Save window layout and serialize plugin settings
        WindowManagerHandler.SaveWindowLayoutToXML("Layout.xml", True, WindowManagerHandler._UserSettingName)
    End Sub

    Public Function CloseForm() As Boolean
        If Not IsNonPersistent Then
            If Not CloseChildsWithoutWarningToolStripMenuItem.Checked And IsChild = False Then
                'Check if child windows open
                Dim oforms As IEnumerable(Of MainForm)
                oforms = GetAllChildForms()
                If Not IsNothing(oforms) Then
                    If Not oforms.Count = 1 Then
                        Dim res As MsgBoxResult
                        res = MsgBox("One or more child windows are open. Do you want to close the application?", vbYesNo)
                        If res = MsgBoxResult.No Then
                            'Cancel exit application
                            Return False
                        End If
                    End If
                End If
            End If

            SaveSettings()

            'Save last normal window state
            If Me.WindowState = FormWindowState.Normal Then
                UserSettings.LastNormalWindowLocation = Me.Location
            End If

            'Save window position to user settings class
            If Not CurrentUserSettingName = "" And Not CurrentUserSettingName = "Default" Then
                'Get workspace to save
                If Not UserSettings.UserTemplates.Count = 0 Then
                    For index = 0 To UserSettings.UserTemplates.Count - 1
                        If UserSettings.UserTemplates(index).SettingName = UserSettings.LastSettingName Then
                            'Found setting name, save position and size
                            WindowManagerHandler.SaveFormLocationAndSize(Me, UserSettings.UserTemplates(index).LastWindowSize, UserSettings.UserTemplates(index).LastWindowLocation, UserSettings.UserTemplates(index).LastWindowState)
                            Exit For
                        End If
                    Next
                End If
            Else
                WindowManagerHandler.SaveFormLocationAndSize(Me, UserSettings.LastWindowSize, UserSettings.LastWindowLocation, UserSettings.LastWindowState)
            End If

            'Save user settings to file
            UserSettingManager.SaveSettings(UserSettings, UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure))

            'Flush log file (if enabled)
            LogManager.CloseStreams()
        End If

        Return True
    End Function

    Public Sub ResizeAndMoveHandler()
        If IsFormLoading = False Then
            If Me.WindowState = FormWindowState.Normal Then
                UserSettings.LastNormalWindowLocation = Me.Location
            End If
        End If
    End Sub

    Public Sub PerformRaiseActions()
        ToolStripButton1.Enabled = False
        RaiseClientAction(HostnameOrIPCtl.Text)
        ToolStripButton1.Enabled = True
    End Sub

    Sub RaiseClientAction(ByVal HostnameOrIP As String)
        'Set window title
        SetWindowTitle(HostnameOrIP)
        'Create environment variables cache and load it to all open gui plugin instances
        EnvironmentManager.SetEnvironmentVarsInPlugins(WindowManagerHandler.PluginManager.PluginCollection, CSDockPanelHosting.Contents, HostnameOrIP)
        'Raise actions
        WindowManagerHandler.SendRaiseActionsToPlugins(HostnameOrIP)
        'Set environment vars to custom actions handler
        CustomActionsHandler._EnvironmentRuntimeVariables = EnvironmentManager.GetEnvironmentVarsFromPlugins(WindowManagerHandler.PluginManager.PluginCollection, HostnameOrIP)
        'Add client or ip to history (combobox-item)
        AddClientToHistoryItems(HostnameOrIP)
    End Sub

    Public Sub SetWindowTitle(ByVal Clientname As String)
        If Clientname = "" Then
            Me.Text = "CSTool"
        Else
            Me.Text = Clientname.ToUpper
            If Not CurrentUserSettingName = "" Then
                Me.Text += " (" & CurrentUserSettingName & ")"
            End If
        End If
        If IsNonPersistent Then
            If Not CurrentUserSettingName = "" Then
                Me.Text += " (" & CurrentUserSettingName & ") [Non-Persistent]"
            Else
                Me.Text += " [Non-Persistent]"
            End If
        End If
        If IsChild Then
            Me.Text += " (" & CurrentUserSettingName & ") [Child]"
        End If
    End Sub

    Public Sub AddClientToHistoryItems(ByVal Clientname As String)
        Try
            Dim exists As Boolean = False
            If Not HostnameOrIPCtl.Items.Count = 0 Then
                For index = 0 To HostnameOrIPCtl.Items.Count - 1
                    If HostnameOrIPCtl.Items(index).ToString.ToUpper = Clientname.ToUpper Then
                        exists = True
                        Exit For
                    End If
                Next
            End If
            If Not exists Then
                HostnameOrIPCtl.Items.Add(Clientname)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CSDockPanelHosting_ContentRemoved(sender As Object, e As WeifenLuo.WinFormsUI.Docking.DockContentEventArgs) Handles CSDockPanelHosting.ContentRemoved
        e.Content.DockHandler.Dispose()
    End Sub

    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim isok As Boolean = False
        If ExitWithoutWarning.Checked Then
            isok = True
        Else
            Dim msgresult As MsgBoxResult
            msgresult = MsgBox("Quit application?", MsgBoxStyle.YesNo)
            If msgresult = MsgBoxResult.Yes Then
                isok = True
            End If
        End If

        If isok Then
            If Not CloseForm() Then
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub PluginSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PluginSettingsToolStripMenuItem.Click
        Dim qq As New SettingsManager
        qq._Parent = Me
        qq.ShowDialog()
    End Sub

    Public Sub ShowEnvironmentVariablesForm()
        Dim kk As New EnvironmentState
        kk._Parent = Me
        kk.Show()
    End Sub

    Private Sub EnvironmentVariablesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnvironmentVariablesToolStripMenuItem.Click
        ShowEnvironmentVariablesForm()
    End Sub

    Private Sub HostnameOrIPCtl_KeyDown(sender As Object, e As KeyEventArgs) Handles HostnameOrIPCtl.KeyDown
        If e.KeyCode = 13 Then
            RaiseClientAction(HostnameOrIPCtl.Text)

            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub NewWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewWindowToolStripMenuItem.Click
        WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostnameOrIPCtl.Text, ToolStripComboBox1.SelectedItem, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, WeifenLuo.WinFormsUI.Docking.DockState.Float)
    End Sub

    Private Sub ToolStripButton3_ButtonClick(sender As Object, e As EventArgs) Handles ToolStripButton3.ButtonClick
        WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostnameOrIPCtl.Text, ToolStripComboBox1.SelectedItem, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName)
    End Sub

    Private Sub DockleftToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DockleftToolStripMenuItem.Click
        WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostnameOrIPCtl.Text, ToolStripComboBox1.SelectedItem, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft)
    End Sub

    Private Sub DockrightToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DockrightToolStripMenuItem.Click
        WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostnameOrIPCtl.Text, ToolStripComboBox1.SelectedItem, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, WeifenLuo.WinFormsUI.Docking.DockState.DockRight)
    End Sub

    Private Sub DockbottomToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DockbottomToolStripMenuItem.Click
        WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostnameOrIPCtl.Text, ToolStripComboBox1.SelectedItem, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, WeifenLuo.WinFormsUI.Docking.DockState.DockBottom)
    End Sub

    Private Sub DocktopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DocktopToolStripMenuItem.Click
        WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostnameOrIPCtl.Text, ToolStripComboBox1.SelectedItem, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, WeifenLuo.WinFormsUI.Docking.DockState.DockTop)
    End Sub

    Private Sub MainForm_ResizeBegin(sender As Object, e As EventArgs) Handles MyBase.ResizeBegin
        CSDockPanelHosting.SuspendLayout()
        UserSettings.LastNormalWindowSize = Me.Size
        ResizeAndMoveHandler()
    End Sub

    Private Sub MainForm_Move_1(sender As Object, e As EventArgs) Handles MyBase.Move
        ResizeAndMoveHandler()
    End Sub

    Private Sub MainForm_ResizeEnd(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd
        UserSettings.LastNormalWindowSize = Me.Size
        ResizeAndMoveHandler()
        CSDockPanelHosting.ResumeLayout()
    End Sub

    Private Sub ToolStripButton10_Click(sender As Object, e As EventArgs) Handles ToolStripButton10.Click
        If ToolStripButton10.Checked Then
            CSDockPanelHosting.AllowEndUserDocking = False
            ToolStripButton10.Image = My.Resources.icon_lock_closed_22x22
        Else
            CSDockPanelHosting.AllowEndUserDocking = True
            ToolStripButton10.Image = My.Resources.icon_lock_open_22x22
        End If
    End Sub

    Public Sub ShwoEnvironmentTesterForm()
        Dim EnvTesterFrm As New EnvironmentVarTesterFrm
        EnvTesterFrm._parent = Me
        EnvTesterFrm.Show()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        ShowEnvironmentVariablesForm()
    End Sub

    Private Sub NewIndependentWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewIndependentWindowToolStripMenuItem.Click
        WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostnameOrIPCtl.Text, ToolStripComboBox1.SelectedItem, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, WeifenLuo.WinFormsUI.Docking.DockState.Unknown, True)
    End Sub

    Private Sub DockdocumentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DockdocumentToolStripMenuItem.Click
        WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostnameOrIPCtl.Text, ToolStripComboBox1.SelectedItem, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, WeifenLuo.WinFormsUI.Docking.DockState.Document)
    End Sub

    Private Sub MainForm_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        If IsFormLoading = False Then
            If Not LastWindowState = Me.WindowState Then
                If Me.WindowState = FormWindowState.Normal Then
                    WindowManagerHandler.SendRefreshToPlugins(True)
                End If
            End If
        End If
    End Sub

    Private Sub ToolStripButton4_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        ToolStripButton4.Enabled = False
        WindowManagerHandler.SendRefreshToPlugins(True)
        ToolStripButton4.Enabled = True
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        If Not UserSettings.CustomActions.Count = 0 Then
            CustomItemsContext.Show(MousePosition)
        End If
    End Sub

    Private Sub TemplateSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TemplateSettingsToolStripMenuItem.Click
        Dim newtempfrm As New UserTemplateManager
        newtempfrm._parent = Me
        newtempfrm.EditMode = True
        newtempfrm.Show()
    End Sub

    Public Sub ShowLogForm()
        Dim newlogwindow As New LogForm
        newlogwindow._LogLibInstance = LogManager
        newlogwindow.Show()
    End Sub

    Private Sub ShowLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowLogToolStripMenuItem.Click
        ShowLogForm()
    End Sub

    Private Sub WorkplaceSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WorkplaceSettingsToolStripMenuItem.Click
        Dim WorkspacesManagerFrm As New WorkspaceManager
        WorkspacesManagerFrm._parent = Me
        WorkspacesManagerFrm.ShowDialog()

        'Reload custom actions
        CustomActionsHandler._ShowWarningOnCustomActions = UserSettings.ShowWarningOnCustomActions
        CustomActionsHandler._CustomActionsCollection = UserSettings.CustomActions
        CustomActionsHandler.LoadCustomItems(CustomItemsContext)
    End Sub

    Public Sub OpenAddFromTemplateWindow()
        Dim newtempfrm As New UserTemplateManager
        newtempfrm._parent = Me
        newtempfrm.EditMode = False
        newtempfrm.InitPluginTemplateIndex = ToolStripComboBox1.SelectedIndex
        newtempfrm.Show()
    End Sub

    Public Function OpenNewWindow(ByVal CloneSettingsAndLayout As Boolean, Optional ByVal UserSettingName As String = "", Optional ByVal HostOrIP As String = "") As Boolean
        Try
            Dim newins As New MainForm
            If Not CloneSettingsAndLayout Then
                If UserSettingName = "" Then
                    newins.RestoreSettings = False
                    newins.IsNonPersistent = True
                Else
                    newins.CurrentUserSettingName = UserSettingName
                End If
            End If
            newins.IsChild = True

            newins.Show()
            If Not HostOrIP = "" Then
                newins.HostnameOrIPCtl.Text = HostOrIP
                newins.PerformRaiseActions()
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub NewEmptyWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewEmptyWindowToolStripMenuItem.Click
        OpenNewWindow(False)
    End Sub

    Private Sub CloneWorkspaceToNewWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloneWorkspaceToNewWindowToolStripMenuItem.Click
        OpenNewWindow(True)
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Dim windowmngrfrm As New WindowManagerForm
        windowmngrfrm._parent = Me
        windowmngrfrm.Show()
    End Sub

    Private Sub ToolStripButton11_ButtonClick(sender As Object, e As EventArgs) Handles ToolStripButton11.ButtonClick
        OpenNewWindow(True)
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        SpawnNewProcessInstance()
    End Sub

    Private Sub AddItemFromTemplatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddItemFromTemplatesToolStripMenuItem.Click
        OpenAddFromTemplateWindow()
    End Sub

    Private Sub SaveSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveSettingsToolStripMenuItem.Click
        If SaveSettingsToolStripMenuItem.Checked Then
            IsNonPersistent = False
        Else
            IsNonPersistent = True
        End If
    End Sub

    Public Function RestoreLastClosedWindow() As Boolean
        Try
            If IO.Directory.Exists(CurrentUserProfilePath & "\" & UserSettings.SettingName & "\BIN") Then
                Dim files As String()
                files = IO.Directory.GetFiles(CurrentUserProfilePath & "\" & UserSettings.SettingName & "\BIN")
                If Not files.Count = 0 Then
                    Dim bininf As New IO.FileInfo(files(0))
                    Dim plugguid As String
                    plugguid = bininf.Name
                    plugguid = plugguid.Replace(".xml", "")
                    Dim newplug As ICSToolInterface
                    newplug = WindowManagerHandler.PluginManager.CreateNewPluginInstance(WindowManagerHandler.GetPluginIndexByGUID(plugguid, WindowManagerHandler.PluginManager.PluginCollection))
                    Return WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostnameOrIPCtl.Text, newplug.PluginName, WindowManagerHandler.PluginManager.PluginCollection, UserSettings.SettingName)
                End If
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    'Public Function RestoreInitialTemplateWorkspace()
    '    Try
    '        If IO.Directory.Exists(ApplicationSettings.UserInitialTemplateDir) Then
    '            'Copy content to new user profile path
    '            My.Computer.FileSystem.CopyDirectory(ApplicationSettings.UserInitialTemplateDir, UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False), True)
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Function

    Private Sub RestoreLastClosedWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestoreLastClosedWindowToolStripMenuItem.Click
        If Not RestoreLastClosedWindow() Then
            MsgBox("No recent closed window found or identical plugin instance is loaded!")
        End If
    End Sub

    Private Sub LogSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogSettingsToolStripMenuItem.Click
        Dim logsetfrm As New LogSettingsForm
        logsetfrm._parent = Me
        logsetfrm.Show()
    End Sub

    Private Sub CloseAllOtherWindowsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseAllOtherWindowsToolStripMenuItem.Click
        WindowManagerHandler.CloseAllOpenWindowsExceptActiveWindow()
    End Sub

    Private Sub CloseAllWindowsDocumentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseAllWindowsDocumentToolStripMenuItem.Click
        WindowManagerHandler.CloseAllOpenWindowsByDockingState(WeifenLuo.WinFormsUI.Docking.DockState.Document)
    End Sub

    Private Sub CloseAllWindowsfloatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseAllWindowsfloatToolStripMenuItem.Click
        WindowManagerHandler.CloseAllOpenWindowsByDockingState(WeifenLuo.WinFormsUI.Docking.DockState.Float)
    End Sub

    Private Sub CloseAllWindowsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseAllWindowsToolStripMenuItem.Click
        WindowManagerHandler.CloseAllOpenWindows()
    End Sub

    Private Sub ExportWorkspaceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportWorkspaceToolStripMenuItem.Click
        ExportWorkspaceFile.ShowDialog()
        Dim result As Boolean = False
        If Not ExportWorkspaceFile.FileName = "" Then
            SaveSettings()
            If Not CurrentUserSettingName = "" Then
                result = WindowManagerHandler.ExportWorkspace(ExportWorkspaceFile.FileName, CurrentUserSettingName)
            Else
                result = WindowManagerHandler.ExportWorkspace(ExportWorkspaceFile.FileName)
            End If
            If result Then
                MsgBox("Export successful!")
            Else
                MsgBox("Export failed!")
            End If
        End If
    End Sub

    Private Sub ImportWorkspaceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportWorkspaceToolStripMenuItem.Click
        ImportWorkspaceFile.ShowDialog()
        Dim result As Boolean = False
        If Not ImportWorkspaceFile.FileName = "" Then
            If Not CurrentUserSettingName = "" Then
                result = WindowManagerHandler.ImportWorkspace(ImportWorkspaceFile.FileName, CurrentUserSettingName)
            Else
                result = WindowManagerHandler.ImportWorkspace(ImportWorkspaceFile.FileName)
            End If
            If result Then
                MsgBox("Import successful. The application needs to be restarted.")
                CloseChildsWithoutWarningToolStripMenuItem.Checked = True
                ExitWithoutWarning.Checked = True
                IsNonPersistent = True
                Application.Restart()
            Else
                MsgBox("Import failed!")
            End If
        End If
    End Sub

    Private Sub ToolStripButton12_ButtonClick(sender As Object, e As EventArgs) Handles ToolStripButton12.ButtonClick
        OpenAddFromTemplateWindow()
    End Sub

    Private Sub ToolStripButton2_MouseDown(sender As Object, e As MouseEventArgs) Handles ToolStripButton2.MouseDown
        If e.Button = MouseButtons.Right Then
            If Not UserSettings.CustomActions.Count = 0 Then
                CustomItemsContext.Show(MousePosition)
            End If
        End If
    End Sub

    Private Sub ToolStripButton8_ButtonClick(sender As Object, e As EventArgs) Handles ToolStripButton8.ButtonClick
        ToolStripButton8.ShowDropDown()
    End Sub

    Private Sub ToolStripButton1_ButtonClick(sender As Object, e As EventArgs) Handles ToolStripButton1.ButtonClick
        PerformRaiseActions()
    End Sub

    Private Sub SearchInNewWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchInNewWindowToolStripMenuItem.Click
        OpenNewWindow(True, CurrentUserSettingName, HostnameOrIPCtl.Text)
    End Sub

    Private Sub SearchInNewInstanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchInNewInstanceToolStripMenuItem.Click
        SpawnNewProcessInstance(CurrentUserSettingName, True, HostnameOrIPCtl.Text)
    End Sub
End Class
