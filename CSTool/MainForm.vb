'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.ComponentModel
Imports CSCustomActionHelper
Imports CSTemplateManager
Imports CSTemplateManager.TemplateCollectionSettings
Imports CSToolApplicationSettingsLib
Imports CSToolApplicationSettingsManager
Imports CSToolEnvironmentManager
Imports CSToolHostWindow
Imports CSToolLauncherLib
Imports CSToolLogGUILib
Imports CSToolLogLib
Imports CSToolPingHelper
Imports CSToolPluginLib
Imports CSToolPluginStandaloneHelper
Imports CSToolSettingsConsoleLib
Imports CSToolUserSettingsLib
Imports CSToolUserSettingsManager
Imports CSToolWindowManager
Imports CSToolWorkspaceManager
Imports WeifenLuo.WinFormsUI.Docking

Public Class MainForm
    Public LogManager As New LogLib
    Public WindowManagerHandler As New WindowManager
    Public ApplicationSettingManager As New ApplicationSettingsManager
    Public ApplicationSettings As New ApplicationSettings
    Public WorkspaceSettingsHandler As New WorkspaceHandler
    Public UserSettingManager As New UserSettingsManager
    Public EnvironmentManager As New EnvironmentManager
    Public PingCheckManager As New PingHelper
    Public UserSettings As New UserSettings
    Public CustomActionsHandler As New CustomActionHelper
    Public CustomActionsAutostartHandler As New CustomActionHelper
    Public UserTemplateManager As New TemplateManager
    Public LauncherLibHandler As New LauncherLib

    Public LayoutFilename = "Layout.xml"
    Public Workspaces As New List(Of UserSettings)
    Public CurrentUserSettingName As String = "Default"
    Public CurrentUsername As String = ""
    Public CurrentUserProfilePath As String = ""
    Public CurrentUserProfileBasicPath As String = ""
    Public IsFormLoading As Boolean = True
    Public IsChild As Boolean = False
    Public ParentInstance As MainForm = Nothing
    Public IsNonPersistent As Boolean = False
    Public RestoreSettings As Boolean = True
    Public SettingsSaved As Boolean = False
    Public LastWindowState As FormWindowState = FormWindowState.Normal
    Public InstanceTag As String = ""

    Public CurrentLoadActionState As String = ""

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

    End Sub

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
            newinst.StartInfo.WorkingDirectory = Application.StartupPath
            newinst.StartInfo.Arguments = Environment.CommandLine

            If Not UserSettingName = "" Then
                If Not newinst.StartInfo.Arguments.EndsWith(" ") Then
                    newinst.StartInfo.Arguments += " "
                End If
                newinst.StartInfo.Arguments += "/settingsname " & UserSettingName
            Else

            End If
            If NonPersistent Then
                If Not newinst.StartInfo.Arguments.EndsWith(" ") Then
                    newinst.StartInfo.Arguments += " "
                End If
                newinst.StartInfo.Arguments += "/nonpersistent"
            End If
            If Not HostOrIP = "" Then
                If Not newinst.StartInfo.Arguments.EndsWith(" ") Then
                    newinst.StartInfo.Arguments += " "
                End If
                newinst.StartInfo.Arguments += "/hostname " & HostOrIP
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
                If arglist(ind).ToLower = "/userprofiledir" Then
                    ApplicationSettings.UserProfileDir = (arglist(ind + 1))
                End If
                If arglist(ind).ToLower = "/instancetag" Then
                    InstanceTag = (arglist(ind + 1))
                End If
                If arglist(ind).ToLower = "/lockfile" Then
                    ApplicationSettings.LauncherLockfile = arglist(ind + 1)
                End If
                If arglist(ind).ToLower = "/ignorelockfile" Then
                    ApplicationSettings.LauncherLockfile = ""
                End If
                If arglist(ind).ToLower = "/userworkspacetemplatesdir" Then
                    ApplicationSettings.UserWorkspaceTemplatesDir = (arglist(ind + 1))
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
                    ShowEnvironmentTesterForm()
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

        'Prevent main application from executed by elevated user (if specified)
        If IsChild = False Then
            If ApplicationSettings.PreventMainAppFromRunningElevated And LauncherLibHandler.IsCurrentUserAdmin Then
                LauncherLibHandler.ShowElevatedAppWarningMsg()
                Application.ExitThread()
            End If
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

        'Get user settings environment strings
        CurrentLoadActionState = "Retrieve user environment settings..."
        Dim UserProfileBasicPath As String = UserSettingManager.GetInitUserBasePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure)
        Dim UserProfileWorkspacePath As String = UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername, CurrentUserSettingName)
        Dim UserProfileWorkspaceSettingsFile As String = UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, True, CurrentUsername, CurrentUserSettingName)
        Dim UserProfileWorkspaceLayoutFile As String = UserProfileWorkspacePath & "\" & LayoutFilename

        Debug.WriteLine(UserProfileBasicPath)
        Debug.WriteLine(UserProfileWorkspacePath)
        Debug.WriteLine(UserProfileWorkspaceSettingsFile)
        Debug.WriteLine(UserProfileWorkspaceLayoutFile)

        'Set global user settings variable
        Dim dirinf As New IO.DirectoryInfo(UserProfileWorkspacePath)
        CurrentUserProfilePath = dirinf.FullName
        CurrentUserProfileBasicPath = UserProfileBasicPath

        'Set user profile in window manager
        WindowManagerHandler._UserProfilePath = UserProfileWorkspacePath

        If RestoreSettings Then
            'Load user (default) settings
            CurrentLoadActionState = "Searching for user settings..."
            If Not IO.File.Exists(UserProfileWorkspaceSettingsFile) Then
                IO.Directory.CreateDirectory(UserProfileWorkspacePath)
                'Check, if we have an valid initial template dir
                If IO.Directory.Exists(ApplicationSettings.UserInitialTemplateDir) Then
                    'Copy content to new user profile path
                    CurrentLoadActionState = "Copy user settings..."
                    'Get all workspaces from directory
                    Dim InitialTemplateWorkspaceCollection As IO.DirectoryInfo()
                    Dim InitialTemplateWorkspaceDirInfo As New IO.DirectoryInfo(ApplicationSettings.UserInitialTemplateDir)
                    InitialTemplateWorkspaceCollection = InitialTemplateWorkspaceDirInfo.GetDirectories

                    'Copy all initial workspaces to users profile path
                    If Not InitialTemplateWorkspaceCollection.Count = 0 Then
                        For index = 0 To InitialTemplateWorkspaceCollection.Count - 1
                            CurrentLoadActionState = "Copy workspace (" & (index + 1) & " from " & InitialTemplateWorkspaceCollection.Count & ")"
                            My.Computer.FileSystem.CopyDirectory(InitialTemplateWorkspaceCollection(index).FullName, UserProfileBasicPath & "\" & InitialTemplateWorkspaceCollection(index).Name)
                        Next
                    End If

                    If IO.File.Exists(UserProfileWorkspaceSettingsFile) Then
                        'We found a valid initial user settings file...
                        CurrentLoadActionState = "Apply initial user settings..."
                        UserSettings = UserSettingManager.LoadSettings(UserProfileWorkspaceSettingsFile)
                        CurrentLoadActionState = "Resetting window positions..."
                        UserSettings.LastNormalWindowSize = ApplicationSettings.InitialWindowSize
                        UserSettings.LastNormalWindowLocation = ApplicationSettings.InitialWindowLocation
                        UserSettings.LastWindowLocation = ApplicationSettings.InitialWindowLocation
                        UserSettings.LastWindowSize = ApplicationSettings.InitialWindowSize
                        UserSettings.LastWindowState = ApplicationSettings.InitialWindowState
                    End If
                Else
                    'Create user (default) template folder if not exists
                    If Not IO.Directory.Exists(UserProfileWorkspacePath) Then
                        IO.Directory.CreateDirectory(UserProfileWorkspacePath)
                    End If

                    'No initial template, start with an empty one and save it.
                    UserSettingManager.SaveSettings(UserSettings, UserProfileWorkspaceSettingsFile)
                End If
            Else
                'We hava an valid settings file, load it.
                CurrentLoadActionState = "Loading user settings..."
                UserSettings = UserSettingManager.LoadSettings(UserProfileWorkspaceSettingsFile)
            End If

            'Load logging settings to log manager
            LogManager.LogSettings = UserSettings.LogSettings
            LogManager.ReInitLogSystem()

            'Set user profile in window manager
            'This profile is the default.
            CurrentLoadActionState = "Loading user settings in window manager..."
            WindowManagerHandler._UserProfilePath = UserProfileWorkspacePath

            'Collect all additional workspaces
            CurrentLoadActionState = "Loading additional workspaces..."
            Workspaces = WorkspaceSettingsHandler.GetAllWorkspaces(UserProfileBasicPath)

            'Set SettingName (workspace ID)
            WindowManagerHandler._UserSettingName = CurrentUserSettingName

            'Check if additional workspaces should be spawn
            If Not Workspaces.Count = 1 Then
                If IsChild = False And CurrentUserSettingName = "Default" Then
                    If Not My.Computer.Keyboard.ShiftKeyDown Then
                        CurrentLoadActionState = "Check for additional needed workspaces..."
                        For index = 0 To Workspaces.Count - 1
                            If Workspaces(index).Autostart Then
                                CurrentLoadActionState = "Spawn workspace " & Workspaces(index).TemplateName

                                Select Case Workspaces(index).StartType
                                    Case UserSettings.StartTypeEnum.NewWindow
                                        OpenNewWindow(False, Workspaces(index).SettingName)
                                    Case UserSettings.StartTypeEnum.NewInstance
                                        SpawnNewProcessInstance(Workspaces(index).SettingName)
                                End Select
                            End If
                        Next
                    End If
                End If
            End If
            'Get Window size and location from settings
            CurrentLoadActionState = "Restore form size and location..."
            WindowManagerHandler.LoadFormLocationAndSize(Me, True, True, UserSettings.LastWindowSize.Height, UserSettings.LastWindowSize.Width, UserSettings.LastWindowLocation.X, UserSettings.LastWindowLocation.Y,
                                                             UserSettings.LastNormalWindowSize.Height, UserSettings.LastNormalWindowSize.Width, UserSettings.LastNormalWindowLocation.X, UserSettings.LastNormalWindowLocation.Y,
                                                             UserSettings.LastWindowState)
        Else
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
            If IO.File.Exists(UserProfileWorkspaceLayoutFile) Then
                CurrentLoadActionState = "Loading workspace layout..."
                WindowManagerHandler.LoadWindowLayoutFromXML(LayoutFilename, True, WindowManagerHandler._UserSettingName)
            End If
        End If

        'Create environment variables cache and load it to all open gui plugin instances
        CurrentLoadActionState = "Loading environment variables cache..."
        EnvironmentManager.SetEnvironmentVarsInPlugins(WindowManagerHandler.PluginManager.PluginCollection, CSDockPanelHosting.Contents)

        'Load custom actions
        LoadCustomActions()

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
        LoadTemplates()

        'Set initial window state for raising windows state events
        LastWindowState = Me.WindowState

        'Set buttons and icons settings from workspace settings
        CurrentLoadActionState = "Set GUI items state..."
        LoadGUIStateFromUserSettings()

        'Resume form and controls events
        IsFormLoading = False
        CSDockPanelHosting.CausesValidation = True

        'Set window title (child, non-persistent-mode, etc...)
        CurrentLoadActionState = "Set window title..."
        SetWindowTitle("")

        'Set (initial) titles to plugin windows
        CurrentLoadActionState = "Set window titles (plugins)..."
        WindowManagerHandler.RefreshAllWindowTitlesToPlugins()

        'Check, if hostname/ip was set via command line args and raise actions
        If Not HostnameOrIPCtl.Text = "" Then
            PerformRaiseActions()
        End If

        'Check if lockfile detection wanted
        If Not IsChild Then
            If Not ApplicationSettings.LauncherLockfile = "" Then
                If Not CheckForLockfile.IsBusy Then
                    CheckForLockfile.RunWorkerAsync()
                End If
            End If
        End If

        'Start worker for remove stale settings from settings folder
        If Not IsChild And Not IsNonPersistent And RestoreSettings Then
            If Not RemoveStaleSettings.IsBusy Then
                RemoveStaleSettings.RunWorkerAsync()
            End If
        End If

        CurrentLoadActionState = "Finished!"

        'Should template manager window opened?
        If UserSettings.ShowWorkplaceTemplateForm Then
            OpenWorkspaceTemplateFormSub()
        End If
    End Sub

    Public Sub LoadTemplates()
        CurrentLoadActionState = "Loading user templates..."
        If IsChild Then
            UserTemplateManager.CurrentTemplates = ParentInstance.UserTemplateManager.CurrentTemplates
        Else
            UserTemplateManager.CurrentTemplates = UserTemplateManager.GetTemplates(ApplicationSettings.UserTemplatesDir, WindowManagerHandler.PluginManager.PluginCollection)
        End If

        'Load pinned user templates (to create new form instances faster)
        If UserSettings.LoadPinnedTemplates Then
            CurrentLoadActionState = "Loading pinned user templates..."
            If IsChild Then
                ClonePinnedTemplatesFromParentToChild()
            Else
                LoadAllPinnedTemplates()
            End If
        End If
    End Sub

    Public Sub LoadCustomActions()
        'Clear custom actions collection
        CustomActionsHandler._CustomActionsCollection.Clear()

        'Check if we have to load central custom actions
        If Not UserSettings.CentralCustomActions = "" Then
            'Load central custom actions
            CurrentLoadActionState = "Loading central custom actions..."
            Dim CentralCustomActionsObj As CentralCustomActions

            'Central custom actions local?
            Dim centralfilepath As String = UserSettings.CentralCustomActions
            If Not IO.File.Exists(centralfilepath) Then
                'Search in launcher startup path (use profile path to detect)
                Dim profilesdirobj As New IO.DirectoryInfo(ApplicationSettings.UserProfileDir)
                If IO.File.Exists(profilesdirobj.Parent.FullName & "\" & centralfilepath) Then
                    centralfilepath = profilesdirobj.Parent.FullName & "\" & centralfilepath
                End If
            End If
            CentralCustomActionsObj = UserSettingManager.LoadCentralCustomActions(centralfilepath)
            CustomActionsHandler._CustomActionsCollection.AddRange(CentralCustomActionsObj.CustomActions)
        End If

        CustomActionsHandler._LogManager = LogManager

        If Not UserSettings.CustomActions.Count = 0 Then
            'Load additional custom actions in workspace
            CurrentLoadActionState = "Loading custom actions..."
            CustomActionsHandler._CustomActionsCollection.AddRange(UserSettings.CustomActions)
            CustomActionsHandler._ShowWarningOnCustomActions = UserSettings.ShowWarningOnCustomActions
        End If

        If Not UserSettings.CustomActions.Count = 0 Or Not UserSettings.CentralCustomActions = "" Then
            'Load custom actions
            CustomActionsHandler.LoadCustomItems(CustomItemsContext)
        End If

        If (Not UserSettings.CustomActions.Count = 0) Or (Not CustomItemsContext.Items.Count = 0) Then
            'Load environment variables to custom actions class
            CurrentLoadActionState = "Loading environment variables for custom actions..."
            CustomActionsHandler._EnvironmentRuntimeVariables = EnvironmentManager.GetEnvironmentVarsFromPlugins(WindowManagerHandler.PluginManager.PluginCollection)
        End If
    End Sub

    Public Sub SaveSettings()
        'Save environment variables settings to file
        WindowManagerHandler.SaveEnvironmentPluginsSettings(WindowManagerHandler._UserSettingName)

        'Save credential plugin settings to file
        WindowManagerHandler.SaveCredentialPluginsSettings(WindowManagerHandler._UserSettingName)

        'Save window gui plugin settings
        WindowManagerHandler.SaveAllGUIPluginSettings(WindowManagerHandler._UserSettingName)

        'Save window layout and serialize plugin settings
        WindowManagerHandler.SaveWindowLayoutToXML("Layout.xml", True, WindowManagerHandler._UserSettingName)
    End Sub

    Public Function CloseForm() As Boolean
        Dim SavingAppForm As New SavingFrm
        SavingAppForm.Show()
        Application.DoEvents()

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
                            SavingAppForm.Close()
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
            WindowManagerHandler.SaveFormLocationAndSize(Me, UserSettings.LastWindowSize, UserSettings.LastWindowLocation, UserSettings.LastWindowState)

            'Save top toolbar settings to settings class
            WriteGUIStateToUserSettings()

            'Save user settings to file
            UserSettingManager.SaveSettings(UserSettings, CurrentUserProfilePath & "\" & UserSettingManager.UserSettingsFile)

            'Flush log file (if enabled)
            LogManager.CloseStreams()

            'Stop file lock worker
            If CheckForLockfile.IsBusy Then
                CheckForLockfile.CancelAsync()
            End If

            'Stop remove stale settings worker
            If RemoveStaleSettings.IsBusy Then
                RemoveStaleSettings.CancelAsync()
            End If
        End If

        'Delete temp. files
        DeleteNonPersistentFilesFromLocalFolder()

        SavingAppForm.Close()

        Return True
    End Function

    Public Function DeleteNonPersistentFilesFromLocalFolder() As Boolean
        Try
            If Not ApplicationSettings.NonPersistentFilesCollection.Count = 0 Then
                For index = 0 To ApplicationSettings.NonPersistentFilesCollection.Count - 1
                    If IO.File.Exists(My.Application.Info.DirectoryPath & "\" & ApplicationSettings.NonPersistentFilesCollection(index).FileName) Then
                        Try
                            IO.File.Delete(My.Application.Info.DirectoryPath & "\" & ApplicationSettings.NonPersistentFilesCollection(index).FileName)
                        Catch ex As Exception
                        End Try
                    End If
                Next
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
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
        If UserSettings.UseAsyncPluginMessaging Then
            'Start background worker
            If Not RaiseActionsAsyncWorker.IsBusy = True Then
                RaiseActionsAsyncWorker.RunWorkerAsync(HostnameOrIP)
            End If
        Else
            'Raise actions sync
            WindowManagerHandler.SendRaiseActionsToPlugins(HostnameOrIP)
        End If
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
        End If
        If Not UserSettings.SettingName = "Default" Then
            Me.Text += " (" & UserSettings.TemplateName & ")"
        End If
        If IsNonPersistent Then
            Me.Text += " [Non-Persistent]"
        End If
        If IsChild Then
            Me.Text += " [Child]"
        End If
        If Not InstanceTag = "" Then
            Me.Text += " - " & InstanceTag
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
        If e.CloseReason = CloseReason.FormOwnerClosing Or e.CloseReason = CloseReason.WindowsShutDown Or e.CloseReason = CloseReason.TaskManagerClosing Or e.CloseReason = CloseReason.None Or e.CloseReason = CloseReason.UserClosing Then
            If Not SettingsSaved Then
                If Not SaveSettingsAsyncWorker.IsBusy Then
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
                        If Not SaveSettingsAsyncWorker.IsBusy Then
                            If IsChild Then
                                SaveSettingsAsyncWorker.RunWorkerAsync(1)
                            Else
                                SaveSettingsAsyncWorker.RunWorkerAsync(0)
                            End If
                        End If
                    End If

                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            End If
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
        Select Case e.KeyCode
            Case 13
                RaiseClientAction(HostnameOrIPCtl.Text)

                e.Handled = True
                e.SuppressKeyPress = True
            Case Keys.Delete
                HostnameOrIPCtl.Text = ""
                RaiseClientAction(HostnameOrIPCtl.Text)

                e.Handled = True
                e.SuppressKeyPress = True
            Case Keys.F5
                WindowManagerHandler.SendRefreshToPlugins(True)

                e.Handled = True
                e.SuppressKeyPress = True
            Case Keys.F9
                SpawnNewProcessInstance("", False, (HostnameOrIPCtl.Text))

                e.Handled = True
                e.SuppressKeyPress = True
            Case Keys.F10
                OpenNewWindow(True, "", HostnameOrIPCtl.Text, True)

                e.Handled = True
                e.SuppressKeyPress = True
        End Select
    End Sub

    Public Function AddItemToWorkspace(ByVal AddType As DockState, ByVal PluginName As String, Optional HostOrIP As String = "", Optional ByVal IndependentWindow As Boolean = False, Optional Override As Boolean = False, Optional OverrideStyle As DefaultWindowStyleEnum = DefaultWindowStyleEnum.DockDocument, Optional OverrideWindowState As ProcessWindowStyle = ProcessWindowStyle.Normal) As Boolean
        Dim InitTemplate As TemplateCollectionSettings
        InitTemplate = UserTemplateManager.GetMasterTemplateFromCollection(UserTemplateManager.CurrentTemplates, ToolStripComboBox1.SelectedItem)
        If InitTemplate.TemplateName = "" Then
            'No initial template found, load empty settings
            If Override Then
                Select Case OverrideStyle
                    Case DefaultWindowStyleEnum.DockBottom
                        Return WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostOrIP, PluginName, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, DockState.DockBottom)
                    Case DefaultWindowStyleEnum.DockDocument
                        Return WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostOrIP, PluginName, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, DockState.Document)
                    Case DefaultWindowStyleEnum.DockLeft
                        Return WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostOrIP, PluginName, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, DockState.DockLeft)
                    Case DefaultWindowStyleEnum.DockRight
                        Return WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostOrIP, PluginName, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, DockState.DockRight)
                    Case DefaultWindowStyleEnum.DockTop
                        Return WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostOrIP, PluginName, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, DockState.DockTop)
                    Case DefaultWindowStyleEnum.FloatWindow
                        Return WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostOrIP, PluginName, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, DockState.Float)
                    Case DefaultWindowStyleEnum.IndependentWindow
                        Return WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostOrIP, PluginName, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, DockState.Unknown, True)
                    Case DefaultWindowStyleEnum.StandaloneNonPersistentProcessWindow
                        Dim NewHostSpawnInstance As New StandaloneInstanceBuilder
                        Dim pluginpath As String
                        pluginpath = WindowManagerHandler.PluginManager.GetPluginFilepathFromInterfaceInstance(WindowManagerHandler.GetPluginByName(PluginName, WindowManagerHandler.PluginManager.PluginCollection))
                        Return NewHostSpawnInstance.SpawnStandaloneInstance(pluginpath, "", CurrentUserProfileBasicPath, ApplicationSettings.EnvironmentPluginDir, ApplicationSettings.CredentialPluginDir, True, WindowManagerHandler._UserSettingName, InstanceTag, "", OverrideWindowState)
                    Case DefaultWindowStyleEnum.StandaloneProcessWindow
                        Dim NewHostSpawnInstance As New StandaloneInstanceBuilder
                        Dim pluginpath As String
                        pluginpath = WindowManagerHandler.PluginManager.GetPluginFilepathFromInterfaceInstance(WindowManagerHandler.GetPluginByName(PluginName, WindowManagerHandler.PluginManager.PluginCollection))
                        Return NewHostSpawnInstance.SpawnStandaloneInstance(pluginpath, "", CurrentUserProfileBasicPath, ApplicationSettings.EnvironmentPluginDir, ApplicationSettings.CredentialPluginDir, False, WindowManagerHandler._UserSettingName, InstanceTag, "", OverrideWindowState)
                    Case Else
                        Return False
                End Select
            Else
                Return WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostOrIP, PluginName, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, AddType, IndependentWindow)
            End If
        Else
            'Initial template found
            Dim PluginInstance As ICSToolInterface
            Dim TemplatePluginFile As String
            PluginInstance = WindowManagerHandler.GetPluginByName(PluginName, WindowManagerHandler.PluginManager.PluginCollection)
            TemplatePluginFile = UserTemplateManager.GetPluginTemplateSettingsFilePath(ApplicationSettings.UserTemplatesDir, PluginInstance, InitTemplate)

            Dim TemplateManager As New UserTemplateManager
            TemplateManager._parent = Me

            If Override Then
                Return TemplateManager.AddItemFromTemplateToWorkspace(InitTemplate.TemplateGUID, CSDockPanelHosting, InitTemplate.PluginName, WindowManagerHandler.GetPluginsByType(ICSToolInterface.PluginTypeEnum.GUIWindow, WindowManagerHandler.PluginManager.PluginCollection), WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, OverrideStyle)
            Else
                Return WindowManagerHandler.AddPluginWindowToGUI(CSDockPanelHosting, HostOrIP, PluginName, WindowManagerHandler.PluginManager.PluginCollection, WindowManagerHandler._UserSettingName, AddType, IndependentWindow, TemplatePluginFile)
            End If
        End If
    End Function

    Private Sub NewWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewWindowToolStripMenuItem.Click
        AddItemToWorkspace(DockState.Float, ToolStripComboBox1.SelectedItem, HostnameOrIPCtl.Text)
    End Sub

    Private Sub ToolStripButton3_ButtonClick(sender As Object, e As EventArgs) Handles ToolStripButton3.ButtonClick
        AddItemToWorkspace(DockState.Document, ToolStripComboBox1.SelectedItem, HostnameOrIPCtl.Text)
    End Sub

    Private Sub DockleftToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DockleftToolStripMenuItem.Click
        AddItemToWorkspace(DockState.DockLeft, ToolStripComboBox1.SelectedItem, HostnameOrIPCtl.Text)
    End Sub

    Private Sub DockrightToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DockrightToolStripMenuItem.Click
        AddItemToWorkspace(DockState.DockRight, ToolStripComboBox1.SelectedItem, HostnameOrIPCtl.Text)
    End Sub

    Private Sub DockbottomToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DockbottomToolStripMenuItem.Click
        AddItemToWorkspace(DockState.DockBottom, ToolStripComboBox1.SelectedItem, HostnameOrIPCtl.Text)
    End Sub

    Private Sub DocktopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DocktopToolStripMenuItem.Click
        AddItemToWorkspace(DockState.DockTop, ToolStripComboBox1.SelectedItem, HostnameOrIPCtl.Text)
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

    Public Sub ShowEnvironmentTesterForm()
        Dim EnvTesterFrm As New EnvironmentVarTesterFrm
        EnvTesterFrm._parent = Me
        EnvTesterFrm.Show()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        ShowEnvironmentTesterForm()
    End Sub

    Private Sub NewIndependentWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewIndependentWindowToolStripMenuItem.Click
        AddItemToWorkspace(DockState.Unknown, ToolStripComboBox1.SelectedItem, HostnameOrIPCtl.Text, True)
    End Sub

    Private Sub DockdocumentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DockdocumentToolStripMenuItem.Click
        AddItemToWorkspace(DockState.Document, ToolStripComboBox1.SelectedItem, HostnameOrIPCtl.Text)
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
        If Not CustomItemsContext.Items.Count = 0 Then
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

        'Load custom actions
        LoadCustomActions()
    End Sub

    Public Sub OpenAddFromTemplateWindow()
        Dim newtempfrm As New UserTemplateManager
        newtempfrm._parent = Me
        newtempfrm.EditMode = False
        newtempfrm.InitPluginTemplateIndex = ToolStripComboBox1.SelectedIndex
        newtempfrm.Show()
    End Sub

    Public Function OpenNewWindow(ByVal CloneSettingsAndLayout As Boolean, Optional ByVal UserSettingName As String = "", Optional ByVal HostOrIP As String = "", Optional ByVal NonPersistent As Boolean = False) As Boolean
        Try
            Dim newins As New MainForm
            If Not CloneSettingsAndLayout Then
                newins.RestoreSettings = False
                If UserSettingName = "Default" Then
                    newins.IsNonPersistent = True
                Else
                    newins.CurrentUserSettingName = UserSettingName
                End If
            Else
                newins.CurrentUserSettingName = UserSettingName
            End If

            newins.IsNonPersistent = NonPersistent

            newins.IsChild = True
            newins.ParentInstance = Me

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
        OpenNewWindow(False, "", "", True)
    End Sub

    Private Sub CloneWorkspaceToNewWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloneWorkspaceToNewWindowToolStripMenuItem.Click
        OpenNewWindow(True, UserSettings.SettingName, HostnameOrIPCtl.Text, True)
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
            If IO.Directory.Exists(CurrentUserProfilePath & "\BIN") Then
                Dim files As String()
                files = IO.Directory.GetFiles(CurrentUserProfilePath & "\BIN")
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

    Public Function RestartApp() As Boolean
        Try
            Dim newinstance As New Process
            newinstance.StartInfo.FileName = Application.ExecutablePath
            newinstance.StartInfo.WorkingDirectory = Application.StartupPath
            newinstance.StartInfo.Arguments = Environment.CommandLine

            If newinstance.Start() Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub RestoreInitialTemplateWorkspace(ByVal PurgeUserProfile As Boolean, Optional ByVal RestartAppAfterReset As Boolean = False, Optional ByVal WorkspaceToReset As String = "Default")
        Try
            If PurgeUserProfile Then
                'Delete user profile
                Dim parentDir As New IO.DirectoryInfo(UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername))
                My.Computer.FileSystem.DeleteDirectory(parentDir.Parent.FullName, FileIO.DeleteDirectoryOption.DeleteAllContents)

                'Ensure that no settings will be saved if application exits
                IsNonPersistent = True

                If RestartAppAfterReset Then
                    'Restart application
                    If RestartApp() Then
                        Me.Close()
                    End If
                End If
            Else
                'Delete workspace folder in user profile
                Dim workspacedir As String
                workspacedir = UserSettingManager.GetUserSettingsFilePath(ApplicationSettings.UserProfileDir, ApplicationSettings.UseUserDomainInFolderStructure, False, CurrentUsername, WorkspaceToReset)

                If IO.Directory.Exists(workspacedir) Then
                    My.Computer.FileSystem.DeleteDirectory(workspacedir, FileIO.DeleteDirectoryOption.DeleteAllContents)

                    'Ensure that no settings will be saved if application exits
                    IsNonPersistent = True

                    If RestartAppAfterReset Then
                        'Restart application
                        If RestartApp() Then
                            Me.Close()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Function LoadAllPinnedTemplates() As Boolean
        Try
            Dim pinnedtemplates As List(Of TemplateCollectionSettings)
            pinnedtemplates = UserTemplateManager.GetPinnedTemplates(UserTemplateManager.CurrentTemplates)

            UserTemplateManager._CurrentPinnedTemplates = pinnedtemplates

            ToolStripButton12.DropDownItems.Clear()

            If Not pinnedtemplates.Count = 0 Then
                For index = 0 To pinnedtemplates.Count - 1
                    Dim templatemenuctl As New ToolStripMenuItem
                    templatemenuctl.Text = pinnedtemplates(index).TemplateName
                    Dim pluginimage As Image
                    pluginimage = WindowManagerHandler.GetPluginByName(pinnedtemplates(index).PluginName, WindowManagerHandler.GetPluginsByType(ICSToolInterface.PluginTypeEnum.GUIWindow, WindowManagerHandler.PluginManager.PluginCollection)).UserControlIcon.ToBitmap
                    templatemenuctl.Image = pluginimage.Clone
                    templatemenuctl.ToolTipText = pinnedtemplates(index).TemplateDescription
                    templatemenuctl.Tag = pinnedtemplates(index)
                    ToolStripButton12.DropDownItems.Add(templatemenuctl)
                    AddHandler templatemenuctl.Click, AddressOf TemplateMenuEntryClicked
                Next
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ClonePinnedTemplatesFromParentToChild()
        Try
            UserTemplateManager._CurrentPinnedTemplates = ParentInstance.UserTemplateManager._CurrentPinnedTemplates

            If Not UserTemplateManager._CurrentPinnedTemplates.Count = 0 Then
                For index = 0 To UserTemplateManager._CurrentPinnedTemplates.Count - 1
                    Dim templatemenuctl As New ToolStripMenuItem
                    templatemenuctl.Text = UserTemplateManager._CurrentPinnedTemplates(index).TemplateName
                    Dim pluginimage As Image
                    pluginimage = WindowManagerHandler.GetPluginByName(UserTemplateManager._CurrentPinnedTemplates(index).PluginName, WindowManagerHandler.GetPluginsByType(ICSToolInterface.PluginTypeEnum.GUIWindow, WindowManagerHandler.PluginManager.PluginCollection)).UserControlIcon.ToBitmap
                    templatemenuctl.Image = pluginimage.Clone
                    templatemenuctl.ToolTipText = UserTemplateManager._CurrentPinnedTemplates(index).TemplateDescription
                    templatemenuctl.Tag = UserTemplateManager._CurrentPinnedTemplates(index)
                    ToolStripButton12.DropDownItems.Add(templatemenuctl)
                    AddHandler templatemenuctl.Click, AddressOf TemplateMenuEntryClicked
                Next
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub TemplateMenuEntryClicked(sender As Object, e As EventArgs)
        Try
            Dim templateobj As TemplateCollectionSettings
            templateobj = sender.Tag
            Dim plugininstanceobj As ICSToolInterface
            plugininstanceobj = WindowManagerHandler.GetPluginByName(templateobj.PluginName, WindowManagerHandler.GetPluginsByType(ICSToolInterface.PluginTypeEnum.GUIWindow, WindowManagerHandler.PluginManager.PluginCollection))

            Dim newtemplatemangr As New UserTemplateManager
            newtemplatemangr._parent = Me

            newtemplatemangr.AddItemFromTemplateToWorkspace(templateobj.TemplateGUID, CSDockPanelHosting, plugininstanceobj.PluginName, WindowManagerHandler.GetPluginsByType(ICSToolInterface.PluginTypeEnum.GUIWindow, WindowManagerHandler.PluginManager.PluginCollection), WindowManagerHandler.PluginManager.PluginCollection, UserSettings.SettingName, templateobj.DefaultWindowStyle)
        Catch ex As Exception
        End Try
    End Sub

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

    Private Sub SaveWorkspaceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveWorkspaceToolStripMenuItem.Click
        SaveSettings()
    End Sub

    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs) Handles ToolStripButton9.Click
        Dim profilemngrfrm As New ProfileManagerForm
        profilemngrfrm._parent = Me
        profilemngrfrm._UserProfilesLocation = ApplicationSettings.UserProfileDir
        profilemngrfrm.Show()
    End Sub

    Private Sub ApplicationSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ApplicationSettingsToolStripMenuItem.Click
        Dim appsettingsfrm As New AppSettingsFrm
        appsettingsfrm.ApplicationSettingsFile = ApplicationSettingManager.GetAppSettingsFilePath
        appsettingsfrm.Show()
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        Dim AboutFrmInstance As New AboutFrm
        AboutFrmInstance.ShowDialog()
    End Sub

    Private Sub ShowApplicationRuntimeInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowApplicationRuntimeInfoToolStripMenuItem.Click
        Dim runtimedlg As New RuntimeInfoForm
        runtimedlg._parent = Me
        runtimedlg.Show()
    End Sub

    Private Sub RestoreInitialTemplateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestoreInitialTemplateToolStripMenuItem.Click
        Dim confirmmsg As MsgBoxResult
        confirmmsg = MsgBox("Reset current plugin and workspace layout? The application will restart.", MsgBoxStyle.YesNo)
        If confirmmsg = MsgBoxResult.Yes Then
            RestoreInitialTemplateWorkspace(False, True, CurrentUserSettingName)
        End If
    End Sub

    Private Sub ResetUserProfileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetUserProfileToolStripMenuItem.Click
        Dim confirmmsg As MsgBoxResult
        confirmmsg = MsgBox("Reset user profile? All workspaces and plugin settings will be lost. The application will restart.", MsgBoxStyle.YesNo)
        If confirmmsg = MsgBoxResult.Yes Then
            RestoreInitialTemplateWorkspace(True, True)
        End If
    End Sub

    Private Sub ToolStripButton8_DropDownOpening(sender As Object, e As EventArgs) Handles ToolStripButton8.DropDownOpening
        'Main menu should open at form client area...
        ToolStripButton8.DropDownDirection = ToolStripDropDownDirection.BelowLeft
    End Sub

    Private Sub ReloadUserTemplatesAndActionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReloadUserTemplatesAndActionsToolStripMenuItem.Click
        'Reload all external actions and templates
        LoadTemplates()
        LoadCustomActions()
    End Sub

    Private Sub CheckForLockfile_DoWork(sender As Object, e As DoWorkEventArgs) Handles CheckForLockfile.DoWork
        Try
            Do While e.Cancel = False
                If Not ApplicationSettings.LauncherLockfile = "" Then
                    Dim islocked As Boolean = False
                    If IO.File.Exists(ApplicationSettings.LauncherLockfile) Then
                        islocked = True
                    End If
                    'Search in launcher startup path (use profile path to detect)
                    Dim profilesdirobj As New IO.DirectoryInfo(ApplicationSettings.UserProfileDir)
                    If IO.File.Exists(profilesdirobj.Parent.FullName & "\" & ApplicationSettings.LauncherLockfile) Then
                        islocked = True
                    End If

                    If islocked Then
                        Dim warningmsg As String
                        warningmsg = IO.File.ReadAllText(ApplicationSettings.LauncherLockfile)
                        If Not warningmsg = "" Then
                            MsgBox(warningmsg, MsgBoxStyle.MsgBoxSetForeground)
                        Else
                            MsgBox("This application is currently not available.")
                        End If

                        e.Result = True

                        Exit Do
                    End If
                Else
                    e.Result = False
                    Exit Try
                End If

                Dim waitcnt As Integer = 0
                Do While e.Cancel = False And waitcnt < 60
                    Threading.Thread.Sleep(1000)
                    waitcnt += 1
                Loop
            Loop
        Catch ex As Exception
            e.Result = False
        End Try
    End Sub

    Private Sub CheckForLockfile_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles CheckForLockfile.RunWorkerCompleted
        If e.Result Then
            'Exit application
            Me.Close()
        End If
    End Sub

    Private Sub RaiseActionsAsyncWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles RaiseActionsAsyncWorker.DoWork
        'Raise actions async and wait
        WindowManagerHandler.SendRaiseActionsToPluginsAsync(e.Argument)
        Do While WindowManagerHandler.PluginActionRaiseInProgress
            Threading.Thread.Sleep(10)
        Loop
    End Sub

    Private Sub RemoveStaleSettings_DoWork(sender As Object, e As DoWorkEventArgs) Handles RemoveStaleSettings.DoWork
        WindowManagerHandler.RemoveStaleGUIPluginFiles(WindowManagerHandler._UserSettingName)
    End Sub

    Private Sub SaveSettingsAsyncWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles SaveSettingsAsyncWorker.DoWork
        Dim closemode As Integer
        closemode = e.Argument
        Dim resultlist As New List(Of Object)
        resultlist.Add(CloseForm())
        resultlist.Add(closemode)

        e.Result = resultlist
    End Sub

    Private Sub SaveSettingsAsyncWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles SaveSettingsAsyncWorker.RunWorkerCompleted
        Dim resultlist As List(Of Object)
        resultlist = e.Result

        If resultlist(0) Then
            If resultlist(1) = 0 Then
                SettingsSaved = True
                Application.Exit()
            End If
            If resultlist(1) = 1 Then
                SettingsSaved = True
                Me.Close()
            End If
        Else
            SettingsSaved = False
        End If
    End Sub

    Private Sub NewProcessWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewProcessWindowToolStripMenuItem.Click
        AddItemToWorkspace(DockState.Unknown, ToolStripComboBox1.SelectedItem, HostnameOrIPCtl.Text, False, True, DefaultWindowStyleEnum.StandaloneProcessWindow)
    End Sub

    Public Sub OpenWorkspaceTemplateFormSub()
        Dim worktemplatemngr As New WorkspaceTemplateForm
        worktemplatemngr._parent = Me
        worktemplatemngr.ShowDialog()
    End Sub

    Private Sub ToolStripButton13_Click(sender As Object, e As EventArgs) Handles ToolStripButton13.Click
        OpenWorkspaceTemplateFormSub()
    End Sub

    Private Sub ControlVisibleMenuItemsContext_Opening(sender As Object, e As CancelEventArgs) Handles ControlVisibleMenuItemsContext.Opening
        ShowGUIStateAtContextMenu()
    End Sub

    Public Sub ShowGUIStateAtContextMenu()
        ShowHostnameIPLabelToolStripMenuItem.Checked = ToolStripLabel1.Visible
        ShowHostnameIPTextboxToolStripMenuItem.Checked = HostnameOrIPCtl.Visible
        ShowHostnameIPSearchIconToolStripMenuItem.Checked = ToolStripButton1.Visible
        ShowHostnameIPRefreshIconToolStripMenuItem.Checked = ToolStripButton4.Visible
        ShowCustomActionIconToolStripMenuItem.Checked = ToolStripButton2.Visible
        ShowTemplateManagerIconToolStripMenuItem.Checked = ToolStripButton12.Visible
        ShowAddItemIconToolStripMenuItem.Checked = ToolStripButton3.Visible
        ShowAddItemIconToolStripMenuItem.Checked = ToolStripComboBox1.Visible
        ShowRemoveItemIconToolStripMenuItem.Checked = ToolStripButton6.Visible
        ShowWorkspaceTemplateManagerIconToolStripMenuItem.Checked = ToolStripButton13.Visible
        ShowWindowManagerIconToolStripMenuItem.Checked = ToolStripButton11.Visible
        ShowNewInstanceIconToolStripMenuItem.Checked = ToolStripButton5.Visible
        ShowUserProfileManagerIconToolStripMenuItem.Checked = ToolStripButton9.Visible
        ShowLockunlockWorkpsaceIconToolStripMenuItem.Checked = ToolStripButton10.Visible
    End Sub

    Public Sub LoadGUIStateFromUserSettings()
        ToolStripLabel1.Visible = UserSettings.MainFormGUISettings.ShowHostnameIPLabel
        HostnameOrIPCtl.Visible = UserSettings.MainFormGUISettings.ShowHostnameIPTextbox
        ToolStripButton1.Visible = UserSettings.MainFormGUISettings.ShowHostnameIPSearchIcon
        ToolStripButton4.Visible = UserSettings.MainFormGUISettings.ShowHostnameIPRefreshIcon
        ToolStripButton2.Visible = UserSettings.MainFormGUISettings.ShowCustomActionIcon
        ToolStripSeparator2.Visible = UserSettings.MainFormGUISettings.ShowCustomActionIcon
        ToolStripButton12.Visible = UserSettings.MainFormGUISettings.ShowTemplateManagerIcon
        ToolStripButton3.Visible = UserSettings.MainFormGUISettings.ShowAddItemIcon
        ToolStripComboBox1.Visible = UserSettings.MainFormGUISettings.ShowAddItemIcon
        ToolStripSeparator3.Visible = UserSettings.MainFormGUISettings.ShowAddItemIcon
        ToolStripButton6.Visible = UserSettings.MainFormGUISettings.ShowRemoveItemIcon
        ToolStripButton13.Visible = UserSettings.MainFormGUISettings.ShowWorkspaceTemplateManagerIcon
        ToolStripSeparator4.Visible = UserSettings.MainFormGUISettings.ShowWorkspaceTemplateManagerIcon
        ToolStripButton11.Visible = UserSettings.MainFormGUISettings.ShowWindowManagerIcon
        ToolStripSeparator16.Visible = UserSettings.MainFormGUISettings.ShowWindowManagerIcon
        ToolStripButton5.Visible = UserSettings.MainFormGUISettings.ShowNewInstanceIcon
        ToolStripButton9.Visible = UserSettings.MainFormGUISettings.ShowUserProfileManagerIcon
        ToolStripButton10.Visible = UserSettings.MainFormGUISettings.ShowLockunlockWorkpsaceIcon
    End Sub

    Public Sub WriteGUIStateToUserSettings()
        UserSettings.MainFormGUISettings.ShowHostnameIPLabel = ToolStripLabel1.Visible
        UserSettings.MainFormGUISettings.ShowHostnameIPTextbox = HostnameOrIPCtl.Visible
        UserSettings.MainFormGUISettings.ShowHostnameIPSearchIcon = ToolStripButton1.Visible
        UserSettings.MainFormGUISettings.ShowHostnameIPRefreshIcon = ToolStripButton4.Visible
        UserSettings.MainFormGUISettings.ShowCustomActionIcon = ToolStripButton2.Visible
        UserSettings.MainFormGUISettings.ShowTemplateManagerIcon = ToolStripButton12.Visible
        UserSettings.MainFormGUISettings.ShowAddItemIcon = ToolStripButton3.Visible
        UserSettings.MainFormGUISettings.ShowAddItemIcon = ToolStripComboBox1.Visible
        UserSettings.MainFormGUISettings.ShowRemoveItemIcon = ToolStripButton6.Visible
        UserSettings.MainFormGUISettings.ShowWorkspaceTemplateManagerIcon = ToolStripButton13.Visible
        UserSettings.MainFormGUISettings.ShowWindowManagerIcon = ToolStripButton11.Visible
        UserSettings.MainFormGUISettings.ShowNewInstanceIcon = ToolStripButton5.Visible
        UserSettings.MainFormGUISettings.ShowUserProfileManagerIcon = ToolStripButton9.Visible
        UserSettings.MainFormGUISettings.ShowLockunlockWorkpsaceIcon = ToolStripButton10.Visible
    End Sub

    Private Sub ShowHostnameIPTextboxToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowHostnameIPTextboxToolStripMenuItem.Click
        HostnameOrIPCtl.Visible = ShowHostnameIPTextboxToolStripMenuItem.Checked
    End Sub

    Private Sub ShowHostnameIPLabelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowHostnameIPLabelToolStripMenuItem.Click
        ToolStripLabel1.Visible = ShowHostnameIPLabelToolStripMenuItem.Checked
    End Sub

    Private Sub ShowHostnameIPSearchIconToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowHostnameIPSearchIconToolStripMenuItem.Click
        ToolStripButton1.Visible = ShowHostnameIPSearchIconToolStripMenuItem.Checked
    End Sub

    Private Sub ShowHostnameIPRefreshIconToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowHostnameIPRefreshIconToolStripMenuItem.Click
        ToolStripButton4.Visible = ShowHostnameIPRefreshIconToolStripMenuItem.Checked
    End Sub

    Private Sub ShowCustomActionIconToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowCustomActionIconToolStripMenuItem.Click
        ToolStripButton2.Visible = ShowCustomActionIconToolStripMenuItem.Checked
        ToolStripSeparator2.Visible = ShowCustomActionIconToolStripMenuItem.Checked
    End Sub

    Private Sub ShowTemplateManagerIconToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowTemplateManagerIconToolStripMenuItem.Click
        ToolStripButton12.Visible = ShowTemplateManagerIconToolStripMenuItem.Checked
    End Sub

    Private Sub ShowAddItemIconToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowAddItemIconToolStripMenuItem.Click
        ToolStripButton3.Visible = ShowAddItemIconToolStripMenuItem.Checked
        ToolStripComboBox1.Visible = ShowAddItemIconToolStripMenuItem.Checked
        ToolStripSeparator3.Visible = ShowAddItemIconToolStripMenuItem.Checked
    End Sub

    Private Sub ShowRemoveItemIconToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowRemoveItemIconToolStripMenuItem.Click
        ToolStripButton6.Visible = ShowRemoveItemIconToolStripMenuItem.Checked
    End Sub

    Private Sub ShowWorkspaceTemplateManagerIconToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowWorkspaceTemplateManagerIconToolStripMenuItem.Click
        ToolStripButton13.Visible = ShowWorkspaceTemplateManagerIconToolStripMenuItem.Checked
        ToolStripSeparator4.Visible = ShowWorkspaceTemplateManagerIconToolStripMenuItem.Checked
    End Sub

    Private Sub ShowWindowManagerIconToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowWindowManagerIconToolStripMenuItem.Click
        ToolStripButton11.Visible = ShowWindowManagerIconToolStripMenuItem.Checked
        ToolStripSeparator16.Visible = ShowWindowManagerIconToolStripMenuItem.Checked
    End Sub

    Private Sub ShowNewInstanceIconToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowNewInstanceIconToolStripMenuItem.Click
        ToolStripButton5.Visible = ShowNewInstanceIconToolStripMenuItem.Checked
    End Sub

    Private Sub ShowUserProfileManagerIconToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowUserProfileManagerIconToolStripMenuItem.Click
        ToolStripButton9.Visible = ShowUserProfileManagerIconToolStripMenuItem.Checked
    End Sub

    Private Sub ShowLockunlockWorkpsaceIconToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowLockunlockWorkpsaceIconToolStripMenuItem.Click
        ToolStripButton10.Visible = ShowLockunlockWorkpsaceIconToolStripMenuItem.Checked
    End Sub

    Private Sub ControlVisibleMenuItemsContext_Closing(sender As Object, e As ToolStripDropDownClosedEventArgs) Handles ControlVisibleMenuItemsContext.Closed
        WriteGUIStateToUserSettings()
    End Sub

    Private Sub CurrentSelctedDockingWindowChanged() Handles CSDockPanelHosting.ActiveContentChanged
        Dim currentplug As CSToolPluginLib.ICSToolInterface
        currentplug = WindowManagerHandler.GetActiveWindowPluginInstance
        If Not IsNothing(currentplug) Then
            ToolStripComboBox1.SelectedItem = currentplug.PluginName
        End If
    End Sub
End Class
