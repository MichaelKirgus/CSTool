'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.DirectoryServices.AccountManagement
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports CSToolLogLib
Imports CSToolPluginLib

Public Module ClientModule
    Public Class Client
        Implements ICSToolInterface
        Public SettingsHandle As New SettingsClass
        Private LifetimePluginGUID As String = ""
        Private FirstLoad As Boolean = True
        Public EvProvider As New EnvironmentProvider
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
                Return "Windows Login Strings Provider"
            End Get
        End Property

        Public ReadOnly Property WindowTitle As String Implements ICSToolInterface.WindowTitle
            Get
                Return "Windows Login Strings Provider"
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
                    If SettingsHandle.GetFullUsername Then
                        Dim FullUserName As New EnvironmentEntry
                        FullUserName.ValueName = "CurrentAppDomainFullUsername"
                        FullUserName.ValueData = UserPrincipal.Current.DisplayName
                        EvProvider.EnvironmentVariables.Add(FullUserName)
                    End If
                    If SettingsHandle.GetUsernameName Then
                        Dim FullUsernameName As New EnvironmentEntry
                        FullUsernameName.ValueName = "CurrentAppDomainFullUsernameName"
                        FullUsernameName.ValueData = UserPrincipal.Current.Name
                        EvProvider.EnvironmentVariables.Add(FullUsernameName)
                    End If
                    If SettingsHandle.GetUsernameMiddlename Then
                        Dim FullUsernameMiddlename As New EnvironmentEntry
                        FullUsernameMiddlename.ValueName = "CurrentAppDomainFullUsernameMiddlename"
                        FullUsernameMiddlename.ValueData = UserPrincipal.Current.MiddleName
                        EvProvider.EnvironmentVariables.Add(FullUsernameMiddlename)
                    End If
                    If SettingsHandle.GetUsernameGivenName Then
                        Dim FullUsernameGivenName As New EnvironmentEntry
                        FullUsernameGivenName.ValueName = "CurrentAppDomainFullUsernameGivenName"
                        FullUsernameGivenName.ValueData = UserPrincipal.Current.MiddleName
                        EvProvider.EnvironmentVariables.Add(FullUsernameGivenName)
                    End If
                    If SettingsHandle.GetUsernameSurname Then
                        Dim FullUsernameSurname As New EnvironmentEntry
                        FullUsernameSurname.ValueName = "CurrentAppDomainFullUsernameSurname"
                        FullUsernameSurname.ValueData = UserPrincipal.Current.Surname
                        EvProvider.EnvironmentVariables.Add(FullUsernameSurname)
                    End If
                    If SettingsHandle.GetUserMail Then
                        Dim UserMail As New EnvironmentEntry
                        UserMail.ValueName = "CurrentAppDomainUserMail"
                        UserMail.ValueData = UserPrincipal.Current.EmailAddress
                        EvProvider.EnvironmentVariables.Add(UserMail)
                    End If
                    If SettingsHandle.GetUsernameSID Then
                        Dim UsernameSID As New EnvironmentEntry
                        UsernameSID.ValueName = "CurrentAppDomainUsernameSID"
                        UsernameSID.ValueData = UserPrincipal.Current.Sid.Value
                        EvProvider.EnvironmentVariables.Add(UsernameSID)
                    End If
                    If SettingsHandle.GetUsernameEmployeeID Then
                        Dim EmployeeID As New EnvironmentEntry
                        EmployeeID.ValueName = "CurrentAppDomainUsernameEmployeeID"
                        EmployeeID.ValueData = UserPrincipal.Current.EmployeeId
                        EvProvider.EnvironmentVariables.Add(EmployeeID)
                    End If
                    If SettingsHandle.GetUsernameSAMAccountName Then
                        Dim SAMAccountName As New EnvironmentEntry
                        SAMAccountName.ValueName = "CurrentAppDomainUsernameSAMAccountName"
                        SAMAccountName.ValueData = UserPrincipal.Current.SamAccountName
                        EvProvider.EnvironmentVariables.Add(SAMAccountName)
                    End If

                    FirstLoad = False
                    Return EvProvider
                Else
                    Return EvProvider
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
            Try
                Dim XML As New XmlSerializer(SettingsClass.GetType)
                Dim FS As New FileStream(Filename, FileMode.Create)
                XML.Serialize(FS, SettingsHandle)
                FS.Close()

                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function LoadPluginSettings(Optional Filename As String = "") As Boolean Implements ICSToolInterface.LoadPluginSettings
            Try
                Dim objStreamReader As New StreamReader(Filename)
                Dim p2 As New SettingsClass
                Dim x As New XmlSerializer(p2.GetType)
                p2 = x.Deserialize(objStreamReader)
                objStreamReader.Close()

                SettingsHandle = p2
                Return True
            Catch ex As Exception
                Return False
            End Try
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

