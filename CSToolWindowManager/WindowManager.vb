'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO.Compression
Imports System.Windows
Imports System.Windows.Forms
Imports CSToolEnvironmentManager
Imports CSToolHostWindow
Imports CSToolLogLib
Imports CSToolLogLib.LogSettings
Imports CSToolPluginHelper
Imports CSToolPluginLib
Imports WeifenLuo.WinFormsUI.Docking

Public Class WindowManager
    Public PluginManager As New PluginHelper
    Public WithEvents PluginActionRaiseWorker As New BackgroundWorker
    Public WithEvents PluginActionRefreshWorker As New BackgroundWorker
    Public _LogManager As LogLib
    Public _DockingContent As DockPanel
    Public _UserProfilePath As String = ""
    Public _UserSettingName As String = "Default"
    Public _EnvironmentEntries As List(Of EnvironmentEntry)
    Public _CredentialEntries As List(Of CredentialEntry)
    Public _IsNonPersistent As Boolean = False

    Private Delegate Function ActionRaiseWorkerDelegate(ByVal DockingContent As DockPanel, ByVal HostOrIP As String) As Boolean

    Public Property GUIPluginDir As String = My.Application.Info.DirectoryPath & "\GUIPlugins"
    Public Property CredentialPluginDir As String = My.Application.Info.DirectoryPath & "\CredentialPlugins"
    Public Property EnvironmentPluginDir As String = My.Application.Info.DirectoryPath & "\EnvironmentPlugins"

    Public LastAddedPluginName As String = ""
    Public LastAddedPluginGUID As String = ""
    Public LastAddedPluginInstanceGUID As String = ""
    Public PluginActionRaiseInProgress As Boolean = False
    Public PluginGUIRefreshInProgress As Boolean = False

    Private Function ActionRaiseWorkerFunc(ByVal DockingContent As DockPanel, ByVal HostOrIP As String) As Boolean
        If DockingContent.InvokeRequired Then
            Dim ActionRaiseWorkerObj As New ActionRaiseWorkerDelegate(AddressOf ActionRaiseWorkerFunc)
            Return DockingContent.Invoke(ActionRaiseWorkerObj, New Object() {DockingContent, HostOrIP})
        Else
            Return SendRaiseActionsToPlugins(HostOrIP)
        End If
    End Function

    Public Function InitAllPlugins() As Boolean
        Dim LoadEnvPluginsResult As Boolean
        Dim LoadCredPluginsResult As Boolean
        Dim LoadGUIPluginsResult As Boolean

        LoadEnvPluginsResult = InitAllEnvironmentPlugins()
        LoadCredPluginsResult = InitAllCredentialPlugins()
        LoadGUIPluginsResult = InitAllGUIPlugins()

        If LoadEnvPluginsResult = True And LoadCredPluginsResult = True And LoadGUIPluginsResult = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function InitAllGUIPlugins() As Boolean
        _LogManager.WriteLogEntry("Load all GUI plugins from directory " & GUIPluginDir, GetType(WindowManager), LogEntryTypeEnum.Info, LogEntryLevelEnum.Advanced)
        If PluginManager.LoadAllPlugins(GUIPluginDir) Then
            _LogManager.WriteLogEntry("Load all GUI plugins from directory " & GUIPluginDir & " successful", GetType(WindowManager), LogEntryTypeEnum.Info, LogEntryLevelEnum.Advanced)
            Return True
        Else
            _LogManager.WriteLogEntry("Load all GUI plugins from directory " & GUIPluginDir & " failed.", GetType(WindowManager), LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Essential, PluginManager.LastErrorObj)
            Return False
        End If
    End Function

    Public Function InitAllCredentialPlugins() As Boolean
        _LogManager.WriteLogEntry("Load all credential plugins from directory " & CredentialPluginDir, GetType(WindowManager), LogEntryTypeEnum.Info, LogEntryLevelEnum.Advanced)
        If PluginManager.LoadAllPlugins(CredentialPluginDir) Then
            _LogManager.WriteLogEntry("Load all credential plugins from directory " & CredentialPluginDir & " successful", GetType(WindowManager), LogEntryTypeEnum.Info, LogEntryLevelEnum.Advanced)
            Return True
        Else
            _LogManager.WriteLogEntry("Load all credential plugins from directory " & CredentialPluginDir & " failed.", GetType(WindowManager), LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Essential, PluginManager.LastErrorObj)
            Return False
        End If
    End Function
    Public Function InitAllEnvironmentPlugins() As Boolean
        _LogManager.WriteLogEntry("Load all environment plugins from directory " & EnvironmentPluginDir, GetType(WindowManager), LogEntryTypeEnum.Info, LogEntryLevelEnum.Advanced)
        If PluginManager.LoadAllPlugins(EnvironmentPluginDir) Then
            _LogManager.WriteLogEntry("Load all environment plugins from directory " & EnvironmentPluginDir & " successful", GetType(WindowManager), LogEntryTypeEnum.Info, LogEntryLevelEnum.Advanced)
            Return True
        Else
            _LogManager.WriteLogEntry("Load all environment plugins from directory " & EnvironmentPluginDir & " failed.", GetType(WindowManager), LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Essential, PluginManager.LastErrorObj)
            Return False
        End If
    End Function

    Public Function GetEnvironmentEntriesFromEnvironmentPlugins() As List(Of EnvironmentEntry)
        Try
            Dim EnvCollection As New List(Of EnvironmentEntry)

            For index = 0 To PluginManager.PluginCollection.Count - 1
                If PluginManager.PluginCollection(index).PluginType = CSToolPluginLib.ICSToolInterface.PluginTypeEnum.EnvironmentManager Then
                    EnvCollection.AddRange(PluginManager.PluginCollection(index).EnvironmentProviderClass.EnvironmentVariables)
                End If
            Next

            Return EnvCollection
        Catch ex As Exception
            Return New List(Of EnvironmentEntry)
        End Try
    End Function

    Public Function GetCredentialEntriesFromCredentialPlugins(ByVal PluginCollection As List(Of CSToolPluginLib.ICSToolInterface), ByVal ReturnCredentials As Boolean) As List(Of CredentialEntry)
        Try
            Dim CredCollection As New List(Of CredentialEntry)

            For index = 0 To PluginCollection.Count - 1
                If PluginCollection(index).PluginType = CSToolPluginLib.ICSToolInterface.PluginTypeEnum.CredentialManager Then
                    PluginCollection(index).CurrentLogInstance = _LogManager
                    If ReturnCredentials Then
                        Dim CEntry As List(Of CredentialEntry)
                        If PluginCollection(index).UserCredentialSet Then
                            CEntry = PluginCollection(index).GetCredential()
                        Else
                            PluginCollection(index).UserCredentialWindow.ShowDialog()
                            CEntry = PluginCollection(index).GetCredential()
                        End If

                        CredCollection.AddRange(CEntry)
                    Else
                        If Not PluginCollection(index).UserCredentialSet Then
                            PluginCollection(index).UserCredentialWindow.ShowDialog()
                        End If
                    End If
                End If
            Next

            Return CredCollection
        Catch ex As Exception
            Return New List(Of CredentialEntry)
        End Try
    End Function

    Public Function GetGUIPluginByGUID(ByVal PluginGUID As String, ByVal PluginCollection As List(Of CSToolPluginLib.ICSToolInterface)) As CSToolPluginLib.ICSToolInterface
        For index = 0 To PluginCollection.Count - 1
            If PluginCollection(index).PluginType = CSToolPluginLib.ICSToolInterface.PluginTypeEnum.GUIWindow Then
                If PluginCollection(index).PluginGUID = PluginGUID Then
                    Return PluginCollection(index)
                End If
            End If
        Next
        Return Nothing
    End Function

    Public Function GetPluginIndexByGUID(ByVal PluginGUID As String, ByVal PluginCollection As List(Of CSToolPluginLib.ICSToolInterface)) As Integer
        For index = 0 To PluginCollection.Count - 1
            If PluginCollection(index).PluginGUID = PluginGUID Then
                Return index
            End If
        Next
        Return Nothing
    End Function
    Public Function GetPluginsByType(ByVal PluginType As CSToolPluginLib.ICSToolInterface.PluginTypeEnum, ByVal TargetPluginCollection As List(Of CSToolPluginLib.ICSToolInterface)) As List(Of CSToolPluginLib.ICSToolInterface)
        Try
            Dim PlugCollection As New List(Of CSToolPluginLib.ICSToolInterface)

            For index = 0 To TargetPluginCollection.Count - 1
                If TargetPluginCollection(index).PluginType = PluginType Then
                    PlugCollection.Add(TargetPluginCollection(index))
                End If
            Next

            Return PlugCollection
        Catch ex As Exception
            Return New List(Of CSToolPluginLib.ICSToolInterface)
        End Try
    End Function

    Public Function GetPluginByGUID(ByVal PluginGUID As String, ByVal TargetPluginCollection As List(Of CSToolPluginLib.ICSToolInterface)) As CSToolPluginLib.ICSToolInterface
        Try
            For index = 0 To TargetPluginCollection.Count - 1
                If TargetPluginCollection(index).PluginGUID = PluginGUID Then
                    Return TargetPluginCollection(index)
                End If
            Next

            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetPluginByName(ByVal PluginName As String, ByVal TargetPluginCollection As List(Of CSToolPluginLib.ICSToolInterface)) As CSToolPluginLib.ICSToolInterface
        Try
            For index = 0 To TargetPluginCollection.Count - 1
                If TargetPluginCollection(index).PluginName = PluginName Then
                    Return TargetPluginCollection(index)
                End If
            Next

            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetPluginIndexByName(ByVal PluginName As String, ByVal TargetPluginCollection As List(Of CSToolPluginLib.ICSToolInterface)) As Integer
        Try
            For index = 0 To TargetPluginCollection.Count - 1
                If TargetPluginCollection(index).PluginName = PluginName Then
                    Return index
                End If
            Next

            Return 0
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function FillGUIWithWindowGUIPlugins(ByVal ToolstripControl As ToolStripComboBox) As Boolean
        Try
            ToolstripControl.Items.Clear()
            ToolstripControl.BeginUpdate()

            If Not PluginManager.PluginCollection.Count = 0 Then
                For index = 0 To PluginManager.PluginCollection.Count - 1
                    If PluginManager.PluginCollection(index).PluginType = CSToolPluginLib.ICSToolInterface.PluginTypeEnum.GUIWindow Then
                        ToolstripControl.Items.Add(PluginManager.PluginCollection(index).PluginName)
                    End If
                Next
            End If

            ToolstripControl.EndUpdate()

            ToolstripControl.SelectedIndex = 0

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function IsGUIPluginWindowVisible(ByVal PluginIndex As Integer) As Boolean
        Dim WantedPlugin As CSToolPluginLib.ICSToolInterface
        WantedPlugin = PluginManager.PluginCollection(PluginIndex)

        Try
            For index = 0 To _DockingContent.Contents.Count - 1
                Try
                    Dim HostingWindowObj As DockContent
                    HostingWindowObj = _DockingContent.Contents(index)
                    If Not HostingWindowObj.IsDisposed Then
                        Dim PluginInterfaceObj As CSToolPluginLib.ICSToolInterface
                        PluginInterfaceObj = HostingWindowObj.Tag

                        If Not IsNothing(PluginInterfaceObj) Then
                            If WantedPlugin.PluginName = PluginInterfaceObj.PluginName Then
                                Return True
                            End If
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CloseAllOpenWindows() As Boolean
        Try
            For index = 0 To _DockingContent.Contents.Count - 1
                Try
                    Dim HostingWindowObj As DockContent
                    HostingWindowObj = _DockingContent.Contents(index)
                    If Not HostingWindowObj.IsDisposed Then
                        Dim DockHostWindowsObj As DockingHostWindow
                        DockHostWindowsObj = HostingWindowObj
                        DockHostWindowsObj.Close()
                    End If
                Catch ex As Exception
                End Try
            Next

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetPluginHostWindowByInstanceGUID(ByVal InstanceGUID As String) As DockingHostWindow
        Try
            For index = 0 To _DockingContent.Contents.Count - 1
                Try
                    Dim HostingWindowObj As DockContent
                    HostingWindowObj = _DockingContent.Contents(index)
                    If Not HostingWindowObj.IsDisposed Then
                        Dim DockHostWindowsObj As DockingHostWindow
                        DockHostWindowsObj = HostingWindowObj

                        If DockHostWindowsObj.InstanceGUID = InstanceGUID Then
                            Return DockHostWindowsObj
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next

            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CloseAllOpenWindowsByDockingState(ByVal State As DockState) As Boolean
        Try
            For index = 0 To _DockingContent.Contents.Count - 1
                Try
                    Dim HostingWindowObj As DockContent
                    HostingWindowObj = _DockingContent.Contents(index)
                    If Not HostingWindowObj.IsDisposed Then
                        If HostingWindowObj.DockState = State Then
                            Dim DockHostWindowsObj As DockingHostWindow
                            DockHostWindowsObj = HostingWindowObj
                            DockHostWindowsObj.Close()
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CloseAllOpenWindowsExceptActiveWindow() As Boolean
        Try
            For index = 0 To _DockingContent.Contents.Count - 1
                Try
                    Dim HostingWindowObj As DockContent
                    HostingWindowObj = _DockingContent.Contents(index)
                    If Not HostingWindowObj.IsDisposed Then
                        If Not HostingWindowObj.IsActivated Then
                            Dim DockHostWindowsObj As DockingHostWindow
                            DockHostWindowsObj = HostingWindowObj
                            DockHostWindowsObj.Close()
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ExportWorkspace(ByVal Filename As String, Optional ByVal SettingName As String = "Default") As Boolean
        Try
            Dim settingsdir As String
            If SettingName = "Default" Then
                settingsdir = _UserProfilePath
            Else
                settingsdir = _UserProfilePath & "\" & SettingName
            End If

            ZipFile.CreateFromDirectory(settingsdir, Filename)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ImportWorkspace(ByVal Filename As String, Optional ByVal SettingName As String = "Default", Optional ByVal PurgeOldFiles As Boolean = True) As Boolean
        Try
            Dim settingsdir As String
            If SettingName = "Default" Then
                settingsdir = _UserProfilePath
            Else
                settingsdir = _UserProfilePath & "\" & SettingName
            End If

            If IO.Directory.Exists(settingsdir) Then
                If PurgeOldFiles Then
                    Dim files As String()
                    files = IO.Directory.GetFiles(settingsdir)
                    For index = 0 To files.Count - 1
                        IO.File.Delete(files(index))
                    Next
                End If
            Else
                IO.Directory.CreateDirectory(settingsdir)
            End If

            ZipFile.ExtractToDirectory(Filename, settingsdir)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetAllDockingWindows() As List(Of DockingHostWindow)
        Try
            Dim DockHostWindowColl As New List(Of DockingHostWindow)

            For index = 0 To _DockingContent.Contents.Count - 1
                Try
                    Dim HostingWindowObj As DockContent
                    HostingWindowObj = _DockingContent.Contents(index)
                    If Not HostingWindowObj.IsDisposed Then
                        DockHostWindowColl.Add(HostingWindowObj)
                    End If
                Catch ex As Exception
                End Try
            Next

            Return DockHostWindowColl
        Catch ex As Exception
            Return New List(Of DockingHostWindow)
        End Try
    End Function

    Public Function GetActiveWindow() As DockingHostWindow
        Try
            Dim HostingWindowObj As DockContent
            HostingWindowObj = _DockingContent.ActiveContent

            Return HostingWindowObj
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetActiveWindowPluginInstance() As CSToolPluginLib.ICSToolInterface
        Try
            Dim HostingWindowObj As DockContent
            HostingWindowObj = _DockingContent.ActiveContent

            Dim PluginInterfaceObj As CSToolPluginLib.ICSToolInterface
            PluginInterfaceObj = HostingWindowObj.Tag
            Return PluginInterfaceObj
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function SaveAllGUIPluginSettings(Optional ByVal UserSettingName As String = "Default", Optional ByVal ForceSave As Boolean = False) As Boolean
        Try
            _LogManager.WriteLogEntry("Enumerate " & _DockingContent.Contents.Count & " GUI plugins...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

            For index = 0 To _DockingContent.Contents.Count - 1
                Try
                    Dim HostingWindowObj As DockContent
                    HostingWindowObj = _DockingContent.Contents(index)
                    Dim DockHostWindowsObj As DockingHostWindow
                    DockHostWindowsObj = HostingWindowObj

                    If Not HostingWindowObj.IsDisposed Then
                        Dim PluginInterfaceObj As CSToolPluginLib.ICSToolInterface
                        PluginInterfaceObj = HostingWindowObj.Tag

                        If Not IsNothing(PluginInterfaceObj) Then
                            If PluginInterfaceObj.PluginType = ICSToolInterface.PluginTypeEnum.GUIWindow Then
                                If ForceSave Or PluginInterfaceObj.PluginSettingsChanged Then
                                    _LogManager.WriteLogEntry("Save settings from " & PluginInterfaceObj.PluginName & " to file.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                    PluginInterfaceObj.SavePluginSettings(_UserProfilePath & "\" & PluginInterfaceObj.PluginGUID & "_" & DockHostWindowsObj.InstanceGUID & ".xml")
                                    PluginInterfaceObj.PluginSettingsChanged = False
                                Else
                                    _LogManager.WriteLogEntry("Skip save settings from " & PluginInterfaceObj.PluginName & " to file.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                End If
                            End If
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next

            Return False
        Catch ex As Exception
            _LogManager.WriteLogEntry("Error.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
            Return False
        End Try
    End Function

    Public Function SendRaiseActionsToPlugins(ByVal HostnameOrIP As String) As Boolean
        Try
            'First raise actions to environment plugins
            _LogManager.WriteLogEntry("Send hostname/IP " & HostnameOrIP & " to environment plugins...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            Dim EnvPlugs As List(Of CSToolPluginLib.ICSToolInterface)
            EnvPlugs = GetPluginsByType(ICSToolInterface.PluginTypeEnum.EnvironmentManager, PluginManager.PluginCollection)

            _LogManager.WriteLogEntry("Enumerate " & EnvPlugs.Count & " environment plugins...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            For index = 0 To EnvPlugs.Count - 1
                _LogManager.WriteLogEntry("Get result from " & EnvPlugs(index).PluginName & " environment plugin...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                EnvPlugs(index).RaiseActions(HostnameOrIP)
            Next

            For index = 0 To _DockingContent.Contents.Count - 1
                Try
                    Dim HostingWindowObj As DockContent
                    HostingWindowObj = _DockingContent.Contents(index)
                    Dim PluginInterfaceObj As CSToolPluginLib.ICSToolInterface
                    PluginInterfaceObj = HostingWindowObj.Tag
                    If Not IsNothing(PluginInterfaceObj) Then
                        If PluginInterfaceObj.SupportsRaisingActions Then
                            If PluginInterfaceObj.RaisingActionsEnabled Then
                                _LogManager.WriteLogEntry("Send hostname/IP " & HostnameOrIP & " to GUI plugin " & PluginInterfaceObj.PluginName & " ...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                PluginInterfaceObj.RaiseActions(HostnameOrIP)
                            End If
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next

            Return False
        Catch ex As Exception
            _LogManager.WriteLogEntry("Error.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
            Return False
        End Try
    End Function

    Public Function RemoveStaleGUIPluginFiles(Optional ByVal UserSettingName As String = "Default", Optional ThreadSpinWait As Integer = 0) As Boolean
        Try
            _LogManager.WriteLogEntry("Check for stale plugin settings at user setting " & _UserSettingName & " ...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

            Dim validinstanceguids As New List(Of String)

            For index = 0 To _DockingContent.Contents.Count - 1
                Try
                    Dim HostingWindowObj As DockContent
                    HostingWindowObj = _DockingContent.Contents(index)
                    Dim DockHostWindowsObj As DockingHostWindow
                    DockHostWindowsObj = HostingWindowObj
                    Dim PluginInterfaceObj As CSToolPluginLib.ICSToolInterface
                    PluginInterfaceObj = HostingWindowObj.Tag
                    If Not IsNothing(PluginInterfaceObj) Then
                        validinstanceguids.Add(DockHostWindowsObj.InstanceGUID)
                    End If
                Catch ex As Exception
                End Try
            Next

            Dim environmentplugs As List(Of ICSToolInterface)
            environmentplugs = GetPluginsByType(ICSToolInterface.PluginTypeEnum.EnvironmentManager, PluginManager.PluginCollection)

            For index = 0 To environmentplugs.Count - 1
                validinstanceguids.Add(environmentplugs(index).PluginGUID)
            Next

            Dim credentialplugs As List(Of ICSToolInterface)
            credentialplugs = GetPluginsByType(ICSToolInterface.PluginTypeEnum.CredentialManager, PluginManager.PluginCollection)

            For index = 0 To credentialplugs.Count - 1
                validinstanceguids.Add(credentialplugs(index).PluginGUID)
            Next

            Dim allpluginsettings As String()
            allpluginsettings = IO.Directory.GetFiles(_UserProfilePath)

            For index = 0 To allpluginsettings.Count - 1
                Threading.Thread.Sleep(ThreadSpinWait)
                Dim currsettings As New IO.FileInfo(allpluginsettings(index))
                If currsettings.Name.Contains("_") And currsettings.Extension.ToLower = ".xml" Then
                    Dim isok As Boolean = False

                    For index2 = 0 To validinstanceguids.Count - 1
                        If currsettings.Name.Contains(validinstanceguids(index2)) Then
                            isok = True
                        End If
                    Next

                    If isok = False Then
                        _LogManager.WriteLogEntry("File " & allpluginsettings(index) & " is stale, remove it...", Me.GetType, LogEntryTypeEnum.Warning, LogEntryLevelEnum.Debug)
                        IO.File.Delete(allpluginsettings(index))
                    Else
                        _LogManager.WriteLogEntry("File " & allpluginsettings(index) & " is ok, leave it.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    End If
                End If
            Next

            _LogManager.WriteLogEntry("Successful checked for stale plugin settings at user setting " & _UserSettingName & " ...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

            Return True
        Catch ex As Exception
            _LogManager.WriteLogEntry("Error.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
            Return False
        End Try
    End Function

    Public Function SendRaiseActionsToPluginsAsync(ByVal HostnameOrIP As String) As Boolean
        _LogManager.WriteLogEntry("Send hostname/IP " & HostnameOrIP & " asynchronous to plugins...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

        If PluginActionRaiseWorker.IsBusy = False Then
            _LogManager.WriteLogEntry("Asynchronous worker is not busy, start thread.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            PluginActionRaiseInProgress = True
            PluginActionRaiseWorker.RunWorkerAsync(HostnameOrIP)
            _LogManager.WriteLogEntry("Thread successfully started.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            Return True
        Else
            _LogManager.WriteLogEntry("Error.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
            Return False
        End If
    End Function

    Private Sub DoRaiseActionWorkerThread(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles PluginActionRaiseWorker.DoWork
        e.Result = ActionRaiseWorkerFunc(_DockingContent, e.Argument)
    End Sub

    Private Sub DoRaiseActionWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles PluginActionRaiseWorker.RunWorkerCompleted
        PluginActionRaiseInProgress = False
    End Sub

    Public Function RefreshAllWindowTitlesToPlugins() As Boolean
        Try
            For index = 0 To _DockingContent.Contents.Count - 1
                Try
                    Dim HostingWindowObj As DockContent
                    HostingWindowObj = _DockingContent.Contents(index)
                    Dim DockHostWindowsObj As DockingHostWindow
                    DockHostWindowsObj = HostingWindowObj

                    DockHostWindowsObj.ShowWindowTitle(True)
                Catch ex As Exception
                End Try
            Next

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SendRefreshToPlugins(ByVal RefreshGUIPlugins As Boolean) As Boolean
        Try
            If RefreshGUIPlugins Then
                For index = 0 To _DockingContent.Contents.Count - 1
                    Try
                        Dim HostingWindowObj As DockContent
                        HostingWindowObj = _DockingContent.Contents(index)
                        Dim PluginInterfaceObj As CSToolPluginLib.ICSToolInterface
                        PluginInterfaceObj = HostingWindowObj.Tag

                        If Not IsNothing(PluginInterfaceObj) Then
                            _LogManager.WriteLogEntry("Refresh GUI plugin " & PluginInterfaceObj.PluginName & " ...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                            PluginInterfaceObj.RefreshGUI()
                        End If
                    Catch ex As Exception
                    End Try
                Next
            End If

            Return False
        Catch ex As Exception
            _LogManager.WriteLogEntry("Error.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
            Return False
        End Try
    End Function

    Public Function AddPluginWindowToGUI(ByVal HostControl As DockPanel, ByVal HostOrIP As String, ByVal PluginName As String, ByVal TargetPluginCollection As List(Of CSToolPluginLib.ICSToolInterface), Optional ByVal UserSettingName As String = "Default",
            Optional DockingType As WeifenLuo.WinFormsUI.Docking.DockState = DockState.Document, Optional IsIndependent As Boolean = False, Optional PluginSettingsPath As String = "", Optional ForceRaiseRefresh As Boolean = False, Optional ForceRaiseActions As Boolean = False,
            Optional ByVal InitialWindowSizeWidth As Integer = 350, Optional ByVal InitialWindowSizeHeight As Integer = 350, Optional ByVal InitialWindowLocationX As Integer = 50, Optional ByVal InitialWindowLocationY As Integer = 50,
            Optional ByVal InitialWindowState As FormWindowState = FormWindowState.Normal) As Boolean

        Try
            Dim plugin As CSToolPluginLib.ICSToolInterface
            plugin = PluginManager.CreateNewPluginInstance(GetPluginIndexByName(PluginName, TargetPluginCollection))

            If plugin.MultiInstance = False Then
                If IsGUIPluginWindowVisible(GetPluginIndexByName(PluginName, TargetPluginCollection)) Then
                    Return False
                End If
            End If

            If plugin.NeedsEnvironmentVariables Then
                Dim EnvProviderTemp As New EnvironmentManager
                plugin.EnvironmentRuntimeVariables = EnvProviderTemp.GetEnvironmentVarsFromPlugins(TargetPluginCollection, HostOrIP)
            End If

            If IsIndependent Then
                If plugin.SupportOpenInIndependentWindow Then
                    Dim HostWindow As New HostWindow
                    HostWindow.AppInit = True
                    HostWindow.Tag = plugin
                    HostWindow._UserProfilePath = _UserProfilePath
                    HostWindow._PluginSettingsFile = _UserProfilePath & "\" & plugin.PluginGUID & "_" & HostWindow.InstanceGUID & ".xml"
                    HostWindow._IsNonPersistent = _IsNonPersistent

                    plugin.CurrentLogInstance = New LogLib
                    plugin.PluginSettingsChanged = True

                    If Not PluginSettingsPath = "" Then
                        plugin.LoadPluginSettings(PluginSettingsPath)
                    End If

                    Dim sizepos As New Point(InitialWindowLocationX, InitialWindowLocationY)
                    HostWindow.Size = sizepos
                    HostWindow.Width = InitialWindowSizeWidth
                    HostWindow.Height = InitialWindowSizeHeight
                    HostWindow.WindowState = InitialWindowState

                    HostWindow.Show()

                    plugin.LoadPlugin()

                    If Not HostOrIP = "" Or ForceRaiseActions Then
                        plugin.RaiseActions(HostOrIP)
                    End If
                    If ForceRaiseRefresh Then
                        plugin.RefreshGUI()
                    End If
                Else
                    MsgBox("This plugin does not support this action!")
                    Return False
                End If
            Else
                If Not (DockingType = DockState.Float And plugin.SupportFloat = False) Then
                    Dim HostWindow As New DockingHostWindow
                    HostWindow.AppInit = True
                    HostWindow.Tag = plugin
                    HostWindow._UserProfilePath = _UserProfilePath
                    HostWindow._PluginSettingsFile = _UserProfilePath & "\" & plugin.PluginGUID & "_" & HostWindow.InstanceGUID & ".xml"
                    HostWindow._IsNonPersistent = _IsNonPersistent

                    plugin.CurrentLogInstance = _LogManager
                    plugin.PluginSettingsChanged = True

                    If Not PluginSettingsPath = "" Then
                        plugin.LoadPluginSettings(PluginSettingsPath)
                    End If

                    Dim sizepos As New Point(InitialWindowLocationX, InitialWindowLocationY)
                    HostWindow.Size = sizepos
                    HostWindow.Width = InitialWindowSizeWidth
                    HostWindow.Height = InitialWindowSizeHeight
                    HostWindow.WindowState = InitialWindowState

                    HostWindow.Show(HostControl, DockingType)

                    plugin.LoadPlugin()

                    LastAddedPluginInstanceGUID = HostWindow.InstanceGUID

                    If Not HostOrIP = "" Or ForceRaiseActions Then
                        plugin.RaiseActions(HostOrIP)
                    End If
                    If ForceRaiseRefresh Then
                        plugin.RefreshGUI()
                    End If
                Else
                    MsgBox("This plugin does not support this action!")
                    Return False
                End If
            End If

            LastAddedPluginName = plugin.PluginName
            LastAddedPluginGUID = plugin.PluginGUID


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SaveWindowLayoutToXML(Optional ByVal FileName As String = "Layout.xml", Optional ByVal UseDefinedUserSettingsFolder As Boolean = True, Optional ByVal UserSettingName As String = "Default") As Boolean
        Try
            If UseDefinedUserSettingsFolder Then
                _DockingContent.SaveAsXml(_UserProfilePath & "\" & FileName)
            Else
                _DockingContent.SaveAsXml(FileName)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadWindowLayoutFromXML(Optional ByVal FileName As String = "Layout.xml", Optional ByVal UseDefinedUserSettingsFolder As Boolean = True, Optional ByVal UserSettingName As String = "Default") As Boolean
        Try
            'Load layout
            Dim ddc As WeifenLuo.WinFormsUI.Docking.DeserializeDockContent
            ddc = New WeifenLuo.WinFormsUI.Docking.DeserializeDockContent(AddressOf Get_content)

            If UseDefinedUserSettingsFolder Then
                _DockingContent.LoadFromXml(_UserProfilePath & "\" & FileName, ddc)
            Else
                _DockingContent.LoadFromXml(FileName, ddc)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadEnvironmentPluginsSettings(Optional ByVal UserSettingName As String = "Default") As Boolean
        Try
            For index = 0 To PluginManager.PluginCollection.Count - 1
                If PluginManager.PluginCollection(index).PluginType = CSToolPluginLib.ICSToolInterface.PluginTypeEnum.EnvironmentManager Then
                    PluginManager.PluginCollection(index).LoadPluginSettings(_UserProfilePath & "\" & PluginManager.PluginCollection(index).PluginGUID & "_" & PluginManager.PluginCollection(index).PluginName & ".xml")
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadCredentialPluginsSettings(Optional ByVal UserSettingName As String = "Default") As Boolean
        Try
            For index = 0 To PluginManager.PluginCollection.Count - 1
                If PluginManager.PluginCollection(index).PluginType = CSToolPluginLib.ICSToolInterface.PluginTypeEnum.CredentialManager Then
                    PluginManager.PluginCollection(index).CurrentLogInstance = _LogManager
                    PluginManager.PluginCollection(index).LoadPluginSettings(_UserProfilePath & "\" & PluginManager.PluginCollection(index).PluginGUID & "_" & PluginManager.PluginCollection(index).PluginName & ".xml")
                    PluginManager.PluginCollection(index).LoadPlugin()
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadEnvironmentPluginSettings(ByVal PluginInterface As CSToolPluginLib.ICSToolInterface, Optional ByVal UserSettingName As String = "Default") As Boolean
        Try
            Return PluginInterface.LoadPluginSettings(_UserProfilePath & "\" & PluginInterface.PluginGUID & "_" & PluginInterface.PluginName & ".xml")
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function SaveEnvironmentPluginSettings(ByVal PluginInterface As CSToolPluginLib.ICSToolInterface, Optional ByVal UserSettingName As String = "Default") As Boolean
        Try
            Return PluginInterface.SavePluginSettings(_UserProfilePath & "\" & PluginInterface.PluginGUID & "_" & PluginInterface.PluginName & ".xml")
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function SaveEnvironmentPluginsSettings(Optional ByVal UserSettingName As String = "Default") As Boolean
        Try
            For index = 0 To PluginManager.PluginCollection.Count - 1
                If PluginManager.PluginCollection(index).PluginType = CSToolPluginLib.ICSToolInterface.PluginTypeEnum.EnvironmentManager Then
                    PluginManager.PluginCollection(index).SavePluginSettings(_UserProfilePath & "\" & PluginManager.PluginCollection(index).PluginGUID & "_" & PluginManager.PluginCollection(index).PluginName & ".xml")
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SaveCredentialPluginsSettings(Optional ByVal UserSettingName As String = "Default") As Boolean
        Try
            For index = 0 To PluginManager.PluginCollection.Count - 1
                If PluginManager.PluginCollection(index).PluginType = CSToolPluginLib.ICSToolInterface.PluginTypeEnum.CredentialManager Then
                    PluginManager.PluginCollection(index).SavePluginSettings(_UserProfilePath & "\" & PluginManager.PluginCollection(index).PluginGUID & "_" & PluginManager.PluginCollection(index).PluginName & ".xml")
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function Get_content(ByVal persiststring) As WeifenLuo.WinFormsUI.Docking.IDockContent
        Try
            Dim DockWindow As New DockingHostWindow

            'Window was loaded from docking suite
            Dim settingsarray As Array
            settingsarray = persiststring.Split("|")
            Dim PluginGUID As String
            PluginGUID = settingsarray(0)
            DockWindow.InstanceGUID = settingsarray(1)
            DockWindow.SettingsKey = settingsarray(3)

            Dim ContentPlugin As ICSToolInterface
            ContentPlugin = PluginManager.CreateNewPluginInstance(GetPluginIndexByGUID(PluginGUID, PluginManager.PluginCollection))
            ContentPlugin.CurrentLogInstance = _LogManager

            If ContentPlugin.PluginType = ICSToolInterface.PluginTypeEnum.GUIWindow Then
                DockWindow.PluginHandler = ContentPlugin
                DockWindow._PluginSettingsFile = _UserProfilePath & "\" & ContentPlugin.PluginGUID & "_" & DockWindow.InstanceGUID & ".xml"
                _LogManager.WriteLogEntry("Load plugin " & ContentPlugin.PluginName & " with GUID " & ContentPlugin.PluginGUID & " and settings " & _UserProfilePath & "\" & _UserSettingName & "\" & ContentPlugin.PluginGUID & "_" & DockWindow.InstanceGUID & ".xml", GetType(WindowManager), LogEntryTypeEnum.Info, LogEntryLevelEnum.Advanced)
                ContentPlugin.LoadPlugin()

                Return DockWindow
            Else
                Return Nothing
            End If
        Catch ex As Exception
            _LogManager.WriteLogEntry("Loading plugin: Error.", GetType(WindowManager), LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Advanced)
            Return Nothing
        End Try
    End Function

    Public Function LoadFormLocationAndSize(ByVal Form As Form, ByVal LoadLocation As Boolean, ByVal LoadSize As Boolean, Optional ByVal XHeight As Integer = 0,
                                            Optional ByVal XWidth As Integer = 0, Optional ByVal PosX As Integer = 0, Optional ByVal PosY As Integer = 0,
                                            Optional ByVal XNormalHeight As Integer = 0,
                                            Optional ByVal XNormalWidth As Integer = 0, Optional ByVal PosNormalX As Integer = 0, Optional ByVal PosNormalY As Integer = 0,
                                            Optional ByVal WindowStateObj As FormWindowState = FormWindowState.Normal) As Boolean
        'This function loads window size and location of a form

        Try
            'check if we have a maximized or minimized window (restore last normal size)
            If (PosX < 0 And PosY < 0) And Not WindowStateObj = FormWindowState.Normal Then
                PosX = PosNormalX
                PosY = PosNormalY
                XWidth = XNormalWidth
                XHeight = XNormalHeight
            End If

            'check if we restore valid (visible) locations
            If (PosX + XWidth - 5) < My.Computer.Screen.WorkingArea.X Then
                PosX = My.Computer.Screen.WorkingArea.X + 10
            End If

            If (PosY + XHeight - 5) < My.Computer.Screen.WorkingArea.Y Then
                PosY = My.Computer.Screen.WorkingArea.Y + 10
            End If
            If Not My.Computer.Screen.WorkingArea.Contains(PosX, PosY) Then
                PosX = My.Computer.Screen.WorkingArea.X + 10
                PosY = My.Computer.Screen.WorkingArea.Y + 10
            End If

            'Init new values
            Dim newpos As System.Drawing.Point
            newpos = New Point(PosX, PosY)

            Dim newsize As System.Drawing.Size
            newsize = New Size(XWidth, XHeight)

            'Set properties
            If LoadLocation = True Then
                Form.Location = newpos
            End If

            If LoadSize = True Then
                Form.Size = newsize
            End If

            Form.WindowState = WindowStateObj

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SaveFormLocationAndSize(ByVal Form As Form, ByRef SizeObj As Size, ByRef LocationObj As Point, ByRef WindowStateObj As FormWindowState) As Boolean
        Try
            SizeObj = New Size(Form.Width, Form.Height)
            LocationObj = New Point(Form.Location.X, Form.Location.Y)
            WindowStateObj = Form.WindowState

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
