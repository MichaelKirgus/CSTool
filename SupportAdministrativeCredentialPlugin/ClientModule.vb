'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports AdysTech.CredentialManager
Imports CSToolCryptHelper
Imports CSToolLogLib
Imports CSToolLogLib.LogSettings
Imports CSToolPluginLib

Public Module ClientModule
    Public Class Client
        Implements ICSToolInterface
        Public SettingsHandle As New Settings
        Public IsUserCredentialSet As Boolean = False
        Public LoginFrmHandler As New LoginFrm
        Public LoginCredHandle As New CredentialEntry
        Public CredHandler As New CredentialHandler
        Public EnvironmentRuntimeVar As New List(Of KeyValuePair(Of String, String))
        Private LifetimePluginGUID As String = ""
        Public LogInstanceHandler As LogLib
        Public CredentialKey As String = "SupportAdminADCredentials"
        Public WindowsCredentialMessage As String = "Additional login information is required to use this tool. Please enter your administrative active directory user data."
        Public WindowsCredentialCaption As String = "Support-Admin-AD-Credentials"

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
                Return "Support Administrative Credential Provider"
            End Get
        End Property

        Public ReadOnly Property WindowTitle As String Implements ICSToolInterface.WindowTitle
            Get
                Return "Support Administrative Credential Provider"
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
                Return IsUserCredentialSet
            End Get
        End Property

        Public ReadOnly Property UserCredentialWindow As Form Implements ICSToolInterface.UserCredentialWindow
            Get
                LoginFrmHandler.Tag = LoginCredHandle
                LoginFrmHandler._ParentInstance = Me
                LoginFrmHandler._ParentClient = Me
                LoginFrmHandler._Settings = SettingsHandle
                Return LoginFrmHandler
            End Get
        End Property

        Public ReadOnly Property EnvironmentProviderClass As EnvironmentProvider Implements ICSToolInterface.EnvironmentProviderClass
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public Property EnvironmentRuntimeVariables As List(Of KeyValuePair(Of String, String)) Implements ICSToolInterface.EnvironmentRuntimeVariables
            Get
                Return EnvironmentRuntimeVar
            End Get
            Set(value As List(Of KeyValuePair(Of String, String)))
                EnvironmentRuntimeVar = value
            End Set
        End Property

        Public ReadOnly Property NeedsEnvironmentVariables As Boolean Implements ICSToolInterface.NeedsEnvironmentVariables
            Get
                Return True
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
                Return ICSToolInterface.PluginTypeEnum.CredentialManager
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

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Function SavePluginSettings(Optional Filename As String = "") As Boolean Implements ICSToolInterface.SavePluginSettings
            Try
                Dim XML As New XmlSerializer(SettingsHandle.GetType)
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
                Dim p2 As New Settings
                Dim x As New XmlSerializer(p2.GetType)
                p2 = x.Deserialize(objStreamReader)
                objStreamReader.Close()

                SettingsHandle = p2
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function LoadCredentials() As List(Of String)
            Try
                Dim result As New List(Of String)

                Select Case SettingsHandle.CredentialLocation
                    Case Settings.CredentialLocationEnum.PluginSettings
                        LogInstanceHandler.WriteLogEntry("Load credentials from plugin settings file...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        Select Case SettingsHandle.CredentialEncryptionType
                            Case Settings.EncryptionTypeEnum.None
                                LogInstanceHandler.WriteLogEntry("Load credentials from plain-text...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                result.Add(SettingsHandle.CredentialUsername)
                                result.Add(SettingsHandle.CredentialPassword)
                                Return result
                            Case Settings.EncryptionTypeEnum.Base64
                                LogInstanceHandler.WriteLogEntry("Load credentials from base64...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim Base64HelperObj As New Base64Helper
                                result.Add(Base64HelperObj.FromBase64(SettingsHandle.CredentialUsername))
                                result.Add(Base64HelperObj.FromBase64(SettingsHandle.CredentialPassword))
                                Return result
                            Case Settings.EncryptionTypeEnum.MD5Hash
                                LogInstanceHandler.WriteLogEntry("Load credentials from md5-hash-encrypted string...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                Dim MD5Str As String
                                MD5Str = MD5HashHandlerObj.GetMD5FromFile(Application.ExecutablePath)
                                result.Add(AESHelper.DecryptTextFromBase64(MD5Str, SettingsHandle.CredentialUsername))
                                result.Add(AESHelper.DecryptTextFromBase64(MD5Str, SettingsHandle.CredentialPassword))
                                Return result
                            Case Settings.EncryptionTypeEnum.AES256PSK
                                LogInstanceHandler.WriteLogEntry("Load credentials from AES256 (PSK)...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper

                                result.Add(AESHelper.DecryptTextFromBase64(SettingsHandle.CredentialEncryptionPSK, SettingsHandle.CredentialUsername))
                                result.Add(AESHelper.DecryptTextFromBase64(SettingsHandle.CredentialEncryptionPSK, SettingsHandle.CredentialPassword))
                                Return result
                        End Select
                    Case Settings.CredentialLocationEnum.UserAppData
                        LogInstanceHandler.WriteLogEntry("Load credentials from users appdata folder...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        Select Case SettingsHandle.CredentialEncryptionType
                            Case Settings.EncryptionTypeEnum.None
                                LogInstanceHandler.WriteLogEntry("Load credentials from plain-text...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim filecontent As Array
                                filecontent = IO.File.ReadAllLines(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & SettingsHandle.UserAppDataFilename, System.Text.Encoding.UTF8)
                                result.Add(filecontent(0))
                                result.Add(filecontent(1))
                                Return result
                            Case Settings.EncryptionTypeEnum.Base64
                                LogInstanceHandler.WriteLogEntry("Load credentials from base64...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim Base64HelperObj As New Base64Helper
                                Dim filecontent As Array
                                filecontent = IO.File.ReadAllLines(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & SettingsHandle.UserAppDataFilename, System.Text.Encoding.UTF8)
                                result.Add(Base64HelperObj.FromBase64(filecontent(0)))
                                result.Add(Base64HelperObj.FromBase64(filecontent(1)))
                                Return result
                            Case Settings.EncryptionTypeEnum.MD5Hash
                                LogInstanceHandler.WriteLogEntry("Load credentials from md5-hash-encrypted string...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                Dim MD5Str As String
                                MD5Str = MD5HashHandlerObj.GetMD5FromFile(Application.ExecutablePath)
                                Dim filecontent As Array
                                filecontent = IO.File.ReadAllLines(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & SettingsHandle.UserAppDataFilename, System.Text.Encoding.UTF8)
                                result.Add(AESHelper.DecryptTextFromBase64(MD5Str, filecontent(0)))
                                result.Add(AESHelper.DecryptTextFromBase64(MD5Str, filecontent(1)))
                                Return result
                            Case Settings.EncryptionTypeEnum.AES256PSK
                                LogInstanceHandler.WriteLogEntry("Load credentials from AES256 (PSK)...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                Dim MD5Str As String
                                MD5Str = MD5HashHandlerObj.GetMD5FromFile(Application.ExecutablePath)
                                Dim filecontent As Array
                                filecontent = IO.File.ReadAllLines(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & SettingsHandle.UserAppDataFilename, System.Text.Encoding.UTF8)
                                result.Add(AESHelper.DecryptTextFromBase64(SettingsHandle.CredentialEncryptionPSK, filecontent(0)))
                                result.Add(AESHelper.DecryptTextFromBase64(SettingsHandle.CredentialEncryptionPSK, filecontent(1)))
                                Return result
                        End Select
                    Case Settings.CredentialLocationEnum.UserRegistry
                        LogInstanceHandler.WriteLogEntry("Load credentials from users registry...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        Select Case SettingsHandle.CredentialEncryptionType
                            Case Settings.EncryptionTypeEnum.None
                                LogInstanceHandler.WriteLogEntry("Load credentials from plain-text...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                result.Add(My.Computer.Registry.CurrentUser.GetValue("HKEY_CURRENT_USER\" & SettingsHandle.UserRegistrySubKey & "\" & SettingsHandle.UserRegistryKey & "_Username"))
                                result.Add(My.Computer.Registry.CurrentUser.GetValue("HKEY_CURRENT_USER\" & SettingsHandle.UserRegistrySubKey & "\" & SettingsHandle.UserRegistryKey & "_Password"))
                                Return result
                            Case Settings.EncryptionTypeEnum.Base64
                                LogInstanceHandler.WriteLogEntry("Load credentials from base64...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim Base64HelperObj As New Base64Helper
                                result.Add(Base64HelperObj.FromBase64(My.Computer.Registry.CurrentUser.GetValue("HKEY_CURRENT_USER\" & SettingsHandle.UserRegistrySubKey & "\" & SettingsHandle.UserRegistryKey & "_Username")))
                                result.Add(Base64HelperObj.FromBase64(My.Computer.Registry.CurrentUser.GetValue("HKEY_CURRENT_USER\" & SettingsHandle.UserRegistrySubKey & "\" & SettingsHandle.UserRegistryKey & "_Password")))
                                Return result
                            Case Settings.EncryptionTypeEnum.MD5Hash
                                LogInstanceHandler.WriteLogEntry("Load credentials from md5-hash-encrypted string...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                Dim MD5Str As String
                                MD5Str = MD5HashHandlerObj.GetMD5FromFile(Application.ExecutablePath)
                                result.Add(AESHelper.DecryptTextFromBase64(MD5Str, My.Computer.Registry.CurrentUser.GetValue("HKEY_CURRENT_USER\" & SettingsHandle.UserRegistrySubKey & "\" & SettingsHandle.UserRegistryKey & "_Username")))
                                result.Add(AESHelper.DecryptTextFromBase64(MD5Str, My.Computer.Registry.CurrentUser.GetValue("HKEY_CURRENT_USER\" & SettingsHandle.UserRegistrySubKey & "\" & SettingsHandle.UserRegistryKey & "_Password")))
                                Return result
                            Case Settings.EncryptionTypeEnum.AES256PSK
                                LogInstanceHandler.WriteLogEntry("Load credentials from AES256 (PSK)...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                result.Add(AESHelper.DecryptTextFromBase64(SettingsHandle.CredentialEncryptionPSK, My.Computer.Registry.CurrentUser.GetValue("HKEY_CURRENT_USER\" & SettingsHandle.UserRegistrySubKey & "\" & SettingsHandle.UserRegistryKey & "_Username")))
                                result.Add(AESHelper.DecryptTextFromBase64(SettingsHandle.CredentialEncryptionPSK, My.Computer.Registry.CurrentUser.GetValue("HKEY_CURRENT_USER\" & SettingsHandle.UserRegistrySubKey & "\" & SettingsHandle.UserRegistryKey & "_Password")))
                                Return result
                        End Select
                    Case Settings.CredentialLocationEnum.WindowsCredentialManager
                        LogInstanceHandler.WriteLogEntry("Load credentials from windows credential manager...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        Try
                            Dim savecred As New Net.NetworkCredential
                            savecred = CredentialManager.GetCredentials(CredentialKey)
                            If IsNothing(savecred) Then
                                LogInstanceHandler.WriteLogEntry("No credentials found, show windows-build-in login form...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                savecred = CredentialManager.PromptForCredentials(CredentialKey, SettingsHandle.CheckSaveCredentailsOnLoginPrompt, WindowsCredentialMessage, WindowsCredentialCaption)
                                CredentialManager.SaveCredentials(CredentialKey, savecred)
                            End If
                            LogInstanceHandler.WriteLogEntry("Successful getting credentials from login form.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                            result.Add(savecred.UserName)
                            result.Add(savecred.Password)
                            Return result
                        Catch ex As Exception
                        End Try
                End Select

                Return New List(Of String)
            Catch ex As Exception
                Return New List(Of String)
            End Try
        End Function

        Public Function SaveCredentials(ByVal Username As String, ByVal Password As String) As Boolean
            Try
                Select Case SettingsHandle.CredentialLocation
                    Case Settings.CredentialLocationEnum.PluginSettings
                        LogInstanceHandler.WriteLogEntry("Save credentials to plugin settings file...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        Select Case SettingsHandle.CredentialEncryptionType
                            Case Settings.EncryptionTypeEnum.None
                                LogInstanceHandler.WriteLogEntry("Save credentials to plain-text...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                SettingsHandle.CredentialUsername = Username
                                SettingsHandle.CredentialPassword = Password
                                Return True
                            Case Settings.EncryptionTypeEnum.Base64
                                LogInstanceHandler.WriteLogEntry("Save credentials to base64...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim Base64HelperObj As New Base64Helper
                                SettingsHandle.CredentialUsername = Base64HelperObj.ToBase64(Username)
                                SettingsHandle.CredentialPassword = Base64HelperObj.ToBase64(Password)
                                Return True
                            Case Settings.EncryptionTypeEnum.MD5Hash
                                LogInstanceHandler.WriteLogEntry("Save credentials to md5-hash-encrypted string...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                Dim MD5Str As String
                                MD5Str = MD5HashHandlerObj.GetMD5FromFile(Application.ExecutablePath)
                                SettingsHandle.CredentialUsername = AESHelper.EncryptTextToBase64(MD5Str, Username)
                                SettingsHandle.CredentialPassword = AESHelper.EncryptTextToBase64(MD5Str, Password)
                                Return True
                            Case Settings.EncryptionTypeEnum.AES256PSK
                                LogInstanceHandler.WriteLogEntry("Save credentials to AES256 (PSK)...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                SettingsHandle.CredentialUsername = AESHelper.EncryptTextToBase64(SettingsHandle.CredentialEncryptionPSK, Username)
                                SettingsHandle.CredentialPassword = AESHelper.EncryptTextToBase64(SettingsHandle.CredentialEncryptionPSK, Password)
                                Return True
                        End Select
                    Case Settings.CredentialLocationEnum.UserAppData
                        LogInstanceHandler.WriteLogEntry("Save credentials to users appdata folder...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        Select Case SettingsHandle.CredentialEncryptionType
                            Case Settings.EncryptionTypeEnum.None
                                LogInstanceHandler.WriteLogEntry("Save credentials to plain-text...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                IO.File.WriteAllText(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & SettingsHandle.UserAppDataFilename, Username & vbNewLine & Password, System.Text.Encoding.UTF8)
                                Return True
                            Case Settings.EncryptionTypeEnum.Base64
                                LogInstanceHandler.WriteLogEntry("Save credentials to base64...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim Base64HelperObj As New Base64Helper
                                IO.File.WriteAllText(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & SettingsHandle.UserAppDataFilename, Base64HelperObj.ToBase64(Username) & vbNewLine & Base64HelperObj.ToBase64(Password), System.Text.Encoding.UTF8)
                                Return True
                            Case Settings.EncryptionTypeEnum.MD5Hash
                                LogInstanceHandler.WriteLogEntry("Save credentials to md5-hash-encrypted string...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                Dim MD5Str As String
                                MD5Str = MD5HashHandlerObj.GetMD5FromFile(Application.ExecutablePath)
                                IO.File.WriteAllText(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & SettingsHandle.UserAppDataFilename, AESHelper.EncryptTextToBase64(MD5Str, Username) & vbNewLine & AESHelper.EncryptTextToBase64(MD5Str, Password), System.Text.Encoding.UTF8)
                                Return True
                            Case Settings.EncryptionTypeEnum.AES256PSK
                                LogInstanceHandler.WriteLogEntry("Save credentials to AES256 (PSK)...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                IO.File.WriteAllText(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & SettingsHandle.UserAppDataFilename, AESHelper.EncryptTextToBase64(SettingsHandle.CredentialEncryptionPSK, Username) & vbNewLine & AESHelper.EncryptTextToBase64(SettingsHandle.CredentialEncryptionPSK, Password), System.Text.Encoding.UTF8)
                                Return True
                        End Select
                    Case Settings.CredentialLocationEnum.UserRegistry
                        LogInstanceHandler.WriteLogEntry("Save credentials to users registry...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        Select Case SettingsHandle.CredentialEncryptionType
                            Case Settings.EncryptionTypeEnum.None
                                LogInstanceHandler.WriteLogEntry("Save credentials to plain-text...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                My.Computer.Registry.CurrentUser.CreateSubKey(SettingsHandle.UserRegistrySubKey, True)
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & SettingsHandle.UserRegistrySubKey,
                                  SettingsHandle.UserRegistryKey & "_Username", Username)
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & SettingsHandle.UserRegistrySubKey,
                                SettingsHandle.UserRegistryKey & "_Password", Password)
                                Return True
                            Case Settings.EncryptionTypeEnum.Base64
                                LogInstanceHandler.WriteLogEntry("Save credentials to base64...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim Base64HelperObj As New Base64Helper
                                My.Computer.Registry.CurrentUser.CreateSubKey(SettingsHandle.UserRegistrySubKey, True)
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & SettingsHandle.UserRegistrySubKey,
                                  SettingsHandle.UserRegistryKey & "_Username", Base64HelperObj.ToBase64(Username))
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & SettingsHandle.UserRegistrySubKey,
                                SettingsHandle.UserRegistryKey & "_Password", Base64HelperObj.ToBase64(Password))
                                Return True
                            Case Settings.EncryptionTypeEnum.MD5Hash
                                LogInstanceHandler.WriteLogEntry("Save credentials to md5-hash-encrypted string...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                Dim MD5Str As String
                                MD5Str = MD5HashHandlerObj.GetMD5FromFile(Application.ExecutablePath)
                                My.Computer.Registry.CurrentUser.CreateSubKey(SettingsHandle.UserRegistrySubKey, True)
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & SettingsHandle.UserRegistrySubKey,
                                  SettingsHandle.UserRegistryKey & "_Username", AESHelper.EncryptTextToBase64(MD5Str, Username))
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & SettingsHandle.UserRegistrySubKey,
                                SettingsHandle.UserRegistryKey & "_Password", AESHelper.EncryptTextToBase64(MD5Str, Password))
                                Return True
                            Case Settings.EncryptionTypeEnum.AES256PSK
                                LogInstanceHandler.WriteLogEntry("Save credentials to AES256 (PSK)...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                My.Computer.Registry.CurrentUser.CreateSubKey(SettingsHandle.UserRegistrySubKey, True)
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & SettingsHandle.UserRegistrySubKey,
                                  SettingsHandle.UserRegistryKey & "_Username", AESHelper.EncryptTextToBase64(SettingsHandle.CredentialEncryptionPSK, Username))
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & SettingsHandle.UserRegistrySubKey,
                                SettingsHandle.UserRegistryKey & "_Password", AESHelper.EncryptTextToBase64(SettingsHandle.CredentialEncryptionPSK, Password))
                                Return True
                        End Select
                    Case Settings.CredentialLocationEnum.WindowsCredentialManager
                        LogInstanceHandler.WriteLogEntry("Save credentials to windows credential manager...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        Dim savecred As New Net.NetworkCredential
                        savecred.UserName = Username
                        savecred.Password = Password
                        Return CredentialManager.SaveCredentials(CredentialKey, savecred)
                End Select

                Return False
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
            Try
                If SettingsHandle.Activated Then
                    Dim results As List(Of String)
                    results = LoadCredentials()

                    If Not results.Count = 0 Then
                        LoginCredHandle.Username = CredHandler.ConvertStringInSecureString(results(0))
                        LoginCredHandle.Password = CredHandler.ConvertStringInSecureString(results(1))
                    End If

                    If Not IsNothing(LoginCredHandle.Username) Then
                        If Not LoginCredHandle.Username.Length = 0 And Not LoginCredHandle.Password.Length = 0 Then
                            IsUserCredentialSet = True
                        Else
                            IsUserCredentialSet = False
                        End If
                    Else
                        IsUserCredentialSet = False
                    End If

                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
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
            LoginCredHandle.CredentialKey = CredentialKey
            LoadPlugin()
            Dim newcredlist As New List(Of CredentialEntry)
            newcredlist.Add(LoginCredHandle)
            Return newcredlist
        End Function
    End Class
End Module

