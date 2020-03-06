'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports System.IO
Imports System.ServiceProcess
Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports CSToolLogLib
Imports CSToolPluginLib

Public Module ClientModule
    Public Class Client
        Implements ICSToolInterface
        Public SettingsHandle As New Settings
        Public EnvironmentProviderHandler As New EnvironmentProvider
        Private LifetimePluginGUID As String = ""
        Private FirstLoad As Boolean = True
        Public LogInstanceHandler As LogLib

        Public Property CurrentLogInstance As LogLib Implements ICSToolInterface.CurrentLogInstance
            Get
                Return LogInstanceHandler
            End Get
            Set(value As LogLib)
                LogInstanceHandler = value
            End Set
        End Property

        Public ReadOnly Property PluginName As String Implements ICSToolInterface.PluginName
            Get
                Return "VNC Viewer Environment Provider"
            End Get
        End Property

        Public ReadOnly Property WindowTitle As String Implements ICSToolInterface.WindowTitle
            Get
                Return "VNC Viewer Environment Provider"
            End Get
        End Property

        Public ReadOnly Property PluginVersion As String Implements ICSToolInterface.PluginVersion
            Get
                Return My.Application.Info.Version.ToString
            End Get
        End Property

        Public ReadOnly Property PluginPublisher As String Implements ICSToolInterface.PluginPublisher
            Get
                Return My.Application.Info.CompanyName
            End Get
        End Property

        Public ReadOnly Property GUILoaded As Boolean Implements ICSToolInterface.GUILoaded
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property GUIExists As Boolean Implements ICSToolInterface.GUIExists
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property UserControl As Windows.Forms.UserControl Implements ICSToolInterface.UserControl
            Get
                Return New UserControl
            End Get
        End Property

        Public ReadOnly Property UserControlIcon As Icon Implements ICSToolInterface.UserControlIcon
            Get
                Return Nothing
            End Get
        End Property

        Public Property SettingsClass As Object Implements ICSToolInterface.UserSettingsClass
            Get
                Return SettingsHandle
            End Get
            Set(value As Object)
                SettingsHandle = value
            End Set
        End Property

        Public ReadOnly Property MultiInstance As Boolean Implements ICSToolInterface.MultiInstance
            Get
                Return False
            End Get
        End Property

        Public Property PluginGUID As String Implements ICSToolInterface.PluginGUID
            Get
                Return LifetimePluginGUID
            End Get
            Set(value As String)
                LifetimePluginGUID = value
            End Set
        End Property

        Public ReadOnly Property UserCredentialSet As Boolean Implements ICSToolInterface.UserCredentialSet
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property UserCredentialWindow As Form Implements ICSToolInterface.UserCredentialWindow
            Get
                Return New Form
            End Get
        End Property

        Public ReadOnly Property EnvironmentProviderClass As EnvironmentProvider Implements ICSToolInterface.EnvironmentProviderClass
            Get
                If FirstLoad Then
                    EnvironmentProviderHandler.IsProviderReadOnly = True
                    If IO.File.Exists(Environment.ExpandEnvironmentVariables("%ProgramFiles%\TightVNC\tvnviewer.exe")) Then
                        Dim CurrentTightVNCViewerFilename As New EnvironmentEntry
                        CurrentTightVNCViewerFilename.ValueName = "CurrentTightVNCViewerFilename"
                        CurrentTightVNCViewerFilename.ValueData = Environment.ExpandEnvironmentVariables("%ProgramFiles%\TightVNC\tvnviewer.exe")
                        EnvironmentProviderHandler.EnvironmentVariables.Add(CurrentTightVNCViewerFilename)

                        Dim CurrentTightVNCViewerVersion As New EnvironmentEntry
                        CurrentTightVNCViewerVersion.ValueName = "CurrentTightVNCViewerVersion"
                        Dim oo As FileVersionInfo
                        oo = FileVersionInfo.GetVersionInfo(Environment.ExpandEnvironmentVariables("%ProgramFiles%\TightVNC\tvnviewer.exe"))
                        CurrentTightVNCViewerVersion.ValueData = Environment.ExpandEnvironmentVariables(oo.FileVersion.ToString())
                        EnvironmentProviderHandler.EnvironmentVariables.Add(CurrentTightVNCViewerVersion)
                    Else
                    End If
                    If IO.File.Exists(Environment.ExpandEnvironmentVariables("%ProgramFiles%\uvnc bvba\UltraVNC\vncviewer.exe")) Then
                        Dim CurrentUltraVNCViewerFilename As New EnvironmentEntry
                        CurrentUltraVNCViewerFilename.ValueName = "CurrentUltraVNCViewerFilename"
                        CurrentUltraVNCViewerFilename.ValueData = Environment.ExpandEnvironmentVariables("%ProgramFiles%\uvnc bvba\UltraVNC\vncviewer.exe")
                        EnvironmentProviderHandler.EnvironmentVariables.Add(CurrentUltraVNCViewerFilename)

                        Dim CurrentUltraVNCViewerVersion As New EnvironmentEntry
                        CurrentUltraVNCViewerVersion.ValueName = "CurrentUltraVNCViewerVersion"
                        Dim oo As FileVersionInfo
                        oo = FileVersionInfo.GetVersionInfo(Environment.ExpandEnvironmentVariables("%ProgramFiles%\uvnc bvba\UltraVNC\vncviewer.exe"))
                        CurrentUltraVNCViewerVersion.ValueData = Environment.ExpandEnvironmentVariables(oo.FileVersion.ToString())
                        EnvironmentProviderHandler.EnvironmentVariables.Add(CurrentUltraVNCViewerVersion)
                    End If

                    Try
                        Dim jj As New ServiceController("tvnserver")
                        Dim IsTightVNCServerServiceInstalled As New EnvironmentEntry
                        Dim IsTightVNCServerServiceRunning As New EnvironmentEntry
                        IsTightVNCServerServiceRunning.ValueName = "IsTightVNCServerServiceRunning"

                        If jj.Status = ServiceControllerStatus.Running Then
                            IsTightVNCServerServiceRunning.ValueData = "true"
                        Else
                            IsTightVNCServerServiceRunning.ValueData = "false"
                        End If
                        IsTightVNCServerServiceInstalled.ValueName = "IsTightVNCServerServiceInstalled"
                        IsTightVNCServerServiceInstalled.ValueData = "true"

                        EnvironmentProviderHandler.EnvironmentVariables.Add(IsTightVNCServerServiceInstalled)
                        EnvironmentProviderHandler.EnvironmentVariables.Add(IsTightVNCServerServiceRunning)
                    Catch ex As Exception
                        Dim IsTightVNCServerServiceInstalled As New EnvironmentEntry
                        IsTightVNCServerServiceInstalled.ValueName = "IsTightVNCServerServiceInstalled"
                        IsTightVNCServerServiceInstalled.ValueData = "false"
                        EnvironmentProviderHandler.EnvironmentVariables.Add(IsTightVNCServerServiceInstalled)
                        Dim IsTightVNCServerServiceRunning As New EnvironmentEntry
                        IsTightVNCServerServiceRunning.ValueName = "IsTightVNCServerServiceRunning"
                        IsTightVNCServerServiceRunning.ValueData = "false"
                        EnvironmentProviderHandler.EnvironmentVariables.Add(IsTightVNCServerServiceRunning)
                    End Try

                    Try
                        Dim jj As New ServiceController("uvnc_service")
                        Dim IsUltraVNCServerServiceInstalled As New EnvironmentEntry
                        Dim IsUltraVNCServerServiceRunning As New EnvironmentEntry
                        IsUltraVNCServerServiceRunning.ValueName = "IsUltraVNCServerServiceRunning"

                        If jj.Status = ServiceControllerStatus.Running Then
                            IsUltraVNCServerServiceRunning.ValueData = "true"
                        Else
                            IsUltraVNCServerServiceRunning.ValueData = "false"
                        End If
                        IsUltraVNCServerServiceInstalled.ValueName = "IsUltraVNCServerServiceInstalled"
                        IsUltraVNCServerServiceInstalled.ValueData = "true"

                        EnvironmentProviderHandler.EnvironmentVariables.Add(IsUltraVNCServerServiceInstalled)
                        EnvironmentProviderHandler.EnvironmentVariables.Add(IsUltraVNCServerServiceRunning)
                    Catch ex As Exception
                        Dim IsUltraVNCServerServiceInstalled As New EnvironmentEntry
                        IsUltraVNCServerServiceInstalled.ValueName = "IsUltraVNCServerServiceInstalled"
                        IsUltraVNCServerServiceInstalled.ValueData = "false"
                        EnvironmentProviderHandler.EnvironmentVariables.Add(IsUltraVNCServerServiceInstalled)
                        Dim IsUltraVNCServerServiceRunning As New EnvironmentEntry
                        IsUltraVNCServerServiceRunning.ValueName = "IsUltraVNCServerServiceRunning"
                        IsUltraVNCServerServiceRunning.ValueData = "false"
                        EnvironmentProviderHandler.EnvironmentVariables.Add(IsUltraVNCServerServiceRunning)
                    End Try

                    Dim UltraVNCServiceName As New EnvironmentEntry
                    UltraVNCServiceName.ValueName = "UltraVNCServiceName"
                    UltraVNCServiceName.ValueData = "uvnc_service"
                    EnvironmentProviderHandler.EnvironmentVariables.Add(UltraVNCServiceName)
                    Dim TightVNCServiceName As New EnvironmentEntry
                    TightVNCServiceName.ValueName = "TightVNCServiceName"
                    TightVNCServiceName.ValueData = "tvnserver"
                    EnvironmentProviderHandler.EnvironmentVariables.Add(TightVNCServiceName)

                    Dim KeyEnter As New EnvironmentEntry
                    KeyEnter.ValueName = "KEY_ENTER"
                    KeyEnter.ValueData = "{ENTER}"
                    EnvironmentProviderHandler.EnvironmentVariables.Add(KeyEnter)
                    Dim KeyTab As New EnvironmentEntry
                    KeyTab.ValueName = "KEY_TAB"
                    KeyTab.ValueData = "{TAB}"
                    EnvironmentProviderHandler.EnvironmentVariables.Add(KeyTab)

                    FirstLoad = False
                    Return EnvironmentProviderHandler
                Else
                    Return EnvironmentProviderHandler
                End If
            End Get
        End Property

        Public Property EnvironmentRuntimeVariables As List(Of KeyValuePair(Of String, String)) Implements ICSToolInterface.EnvironmentRuntimeVariables
            Get
                Throw New NotImplementedException()
            End Get
            Set(value As List(Of KeyValuePair(Of String, String)))
                Throw New NotImplementedException()
            End Set
        End Property

        Public ReadOnly Property NeedsEnvironmentVariables As Boolean Implements ICSToolInterface.NeedsEnvironmentVariables
            Get
                Return False
            End Get
        End Property

        Public Property CurrentWindowTitle As String Implements ICSToolInterface.CurrentWindowTitle
            Get
                Throw New NotImplementedException()
            End Get
            Set(value As String)
                Throw New NotImplementedException()
            End Set
        End Property

        Public ReadOnly Property SupportFloat As Boolean Implements ICSToolInterface.SupportFloat
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public ReadOnly Property SupportOpenInIndependentWindow As Boolean Implements ICSToolInterface.SupportOpenInIndependentWindow
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Private ReadOnly Property ICSToolInterface_PluginType As ICSToolInterface.PluginTypeEnum Implements ICSToolInterface.PluginType
            Get
                Return ICSToolInterface.PluginTypeEnum.EnvironmentManager
            End Get
        End Property

        Public ReadOnly Property SupportsRaisingActions As Boolean Implements ICSToolInterface.SupportsRaisingActions
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public Property RaisingActionsEnabled As Boolean Implements ICSToolInterface.RaisingActionsEnabled
            Get
                Throw New NotImplementedException()
            End Get
            Set(value As Boolean)
                Throw New NotImplementedException()
            End Set
        End Property

        Public Property PluginSettingsChanged As Boolean Implements ICSToolInterface.PluginSettingsChanged
            Get
                Throw New NotImplementedException()
            End Get
            Set(value As Boolean)
                Throw New NotImplementedException()
            End Set
        End Property

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Function SavePluginSettings(Optional Filename As String = "") As Boolean Implements ICSToolInterface.SavePluginSettings
            Return True
        End Function

        Public Function LoadPluginSettings(Optional Filename As String = "") As Boolean Implements ICSToolInterface.LoadPluginSettings
            Return True
        End Function

        Public Function RefreshGUI() As Boolean Implements ICSToolInterface.RefreshGUI
            Throw New NotImplementedException()
        End Function

        Public Function LockGUI() As Boolean Implements ICSToolInterface.LockGUI
            Throw New NotImplementedException()
        End Function

        Public Function UnlockGUI() As Boolean Implements ICSToolInterface.UnlockGUI
            Throw New NotImplementedException()
        End Function

        Public Function RaiseActions(HostnameOrIP As String) As Boolean Implements ICSToolInterface.RaiseActions
            Return True
        End Function

        Public Function LoadPlugin() As Boolean Implements ICSToolInterface.LoadPlugin
            Throw New NotImplementedException()
        End Function

        Public Function UnloadPlugin() As Boolean Implements ICSToolInterface.UnloadPlugin
            Throw New NotImplementedException()
        End Function

        Public Function GetNewSettingsOnjectInstance() As Object Implements ICSToolInterface.GetNewSettingsObjectInstance
            Throw New NotImplementedException()
        End Function

        Public Function GetNewUserControlOnjectInstance() As UserControl Implements ICSToolInterface.GetNewUserControlObjectInstance
            Throw New NotImplementedException()
        End Function

        Public Function GetNewInterfaceObjectInstance() As Object Implements ICSToolInterface.GetNewInterfaceObjectInstance
            Return New ClientModule.Client
        End Function

        Public Function RaiseCustomAction(UserStr As String) As Boolean Implements ICSToolInterface.RaiseCustomAction
            Throw New NotImplementedException()
        End Function

        Public Function GetCredential() As List(Of CredentialEntry) Implements ICSToolInterface.GetCredential
            Throw New NotImplementedException()
        End Function
    End Class
End Module

