﻿'Copyright (C) 2019-2021 Michael Kirgus
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
Imports CustomCredentialsPlugin.Settings

Public Module ClientModule
    Public Class Client
        Implements ICSToolInterface
        Public SettingsHandle As New Settings
        Public IsUserCredentialSet As Boolean = False
        Public LoginFrmHandler As New CustomCredFrm
        Public LoginCredHandle As New List(Of CredentialEntry)
        Public CredHandler As New CredentialHandler
        Public EnvironmentRuntimeVar As New List(Of KeyValuePair(Of String, String))
        Private LifetimePluginGUID As String = ""
        Public LogInstanceHandler As LogLib
        Public IsDisabled As Boolean = False

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
                Return "Custom Credentials Provider"
            End Get
        End Property

        Public ReadOnly Property WindowTitle As String Implements ICSToolInterface.WindowTitle
            Get
                Return "Custom Credentials Provider"
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

        Public ReadOnly Property UserControl As UserControl Implements ICSToolInterface.UserControl
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

        Public Function LoadCredentials(ByVal CredEntry As CustomCredentialEntry) As List(Of String)
            Try
                Dim result As New List(Of String)

                Select Case CredEntry.CredentialLocation
                    Case Settings.CredentialLocationEnum.PluginSettings
                        LogInstanceHandler.WriteLogEntry("Load credentials from plugin settings file...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        Select Case CredEntry.CredentialEncryptionType
                            Case Settings.EncryptionTypeEnum.None
                                LogInstanceHandler.WriteLogEntry("Load credentials from plain-text...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                result.Add(CredEntry.CredentialUsername)
                                result.Add(CredEntry.CredentialPassword)
                                Return result
                            Case Settings.EncryptionTypeEnum.Base64
                                LogInstanceHandler.WriteLogEntry("Load credentials from base64...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim Base64HelperObj As New Base64Helper
                                result.Add(Base64HelperObj.FromBase64(CredEntry.CredentialUsername))
                                result.Add(Base64HelperObj.FromBase64(CredEntry.CredentialPassword))
                                Return result
                            Case Settings.EncryptionTypeEnum.MD5Hash
                                LogInstanceHandler.WriteLogEntry("Load credentials from md5-hash-encrypted string...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                Dim MD5Str As String
                                MD5Str = MD5HashHandlerObj.GetMD5FromFile(Application.ExecutablePath)
                                result.Add(AESHelper.DecryptTextFromBase64(MD5Str, CredEntry.CredentialUsername))
                                result.Add(AESHelper.DecryptTextFromBase64(MD5Str, CredEntry.CredentialPassword))
                                Return result
                            Case Settings.EncryptionTypeEnum.AES256PSK
                                LogInstanceHandler.WriteLogEntry("Load credentials from AES256 (PSK)...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper

                                result.Add(AESHelper.DecryptTextFromBase64(CredEntry.CredentialEncryptionPSK, CredEntry.CredentialUsername))
                                result.Add(AESHelper.DecryptTextFromBase64(CredEntry.CredentialEncryptionPSK, CredEntry.CredentialPassword))
                                Return result
                        End Select
                    Case Settings.CredentialLocationEnum.UserAppData
                        LogInstanceHandler.WriteLogEntry("Load credentials from users appdata folder...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        Select Case CredEntry.CredentialEncryptionType
                            Case Settings.EncryptionTypeEnum.None
                                LogInstanceHandler.WriteLogEntry("Load credentials from plain-text...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim filecontent As Array
                                filecontent = IO.File.ReadAllLines(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & CredEntry.UserAppDataFilename, System.Text.Encoding.UTF8)
                                result.Add(filecontent(0))
                                result.Add(filecontent(1))
                                Return result
                            Case Settings.EncryptionTypeEnum.Base64
                                LogInstanceHandler.WriteLogEntry("Load credentials from base64...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim Base64HelperObj As New Base64Helper
                                Dim filecontent As Array
                                filecontent = IO.File.ReadAllLines(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & CredEntry.UserAppDataFilename, System.Text.Encoding.UTF8)
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
                                filecontent = IO.File.ReadAllLines(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & CredEntry.UserAppDataFilename, System.Text.Encoding.UTF8)
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
                                filecontent = IO.File.ReadAllLines(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & CredEntry.UserAppDataFilename, System.Text.Encoding.UTF8)
                                result.Add(AESHelper.DecryptTextFromBase64(CredEntry.CredentialEncryptionPSK, filecontent(0)))
                                result.Add(AESHelper.DecryptTextFromBase64(CredEntry.CredentialEncryptionPSK, filecontent(1)))
                                Return result
                        End Select
                    Case Settings.CredentialLocationEnum.UserRegistry
                        LogInstanceHandler.WriteLogEntry("Load credentials from users registry...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        Select Case CredEntry.CredentialEncryptionType
                            Case Settings.EncryptionTypeEnum.None
                                LogInstanceHandler.WriteLogEntry("Load credentials from plain-text...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                result.Add(My.Computer.Registry.CurrentUser.GetValue("HKEY_CURRENT_USER\" & CredEntry.UserRegistrySubKey & "\" & CredEntry.UserRegistryKey & "_Username"))
                                result.Add(My.Computer.Registry.CurrentUser.GetValue("HKEY_CURRENT_USER\" & CredEntry.UserRegistrySubKey & "\" & CredEntry.UserRegistryKey & "_Password"))
                                Return result
                            Case Settings.EncryptionTypeEnum.Base64
                                LogInstanceHandler.WriteLogEntry("Load credentials from base64...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim Base64HelperObj As New Base64Helper
                                result.Add(Base64HelperObj.FromBase64(My.Computer.Registry.CurrentUser.GetValue("HKEY_CURRENT_USER\" & CredEntry.UserRegistrySubKey & "\" & CredEntry.UserRegistryKey & "_Username")))
                                result.Add(Base64HelperObj.FromBase64(My.Computer.Registry.CurrentUser.GetValue("HKEY_CURRENT_USER\" & CredEntry.UserRegistrySubKey & "\" & CredEntry.UserRegistryKey & "_Password")))
                                Return result
                            Case Settings.EncryptionTypeEnum.MD5Hash
                                LogInstanceHandler.WriteLogEntry("Load credentials from md5-hash-encrypted string...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                Dim MD5Str As String
                                MD5Str = MD5HashHandlerObj.GetMD5FromFile(Application.ExecutablePath)
                                result.Add(AESHelper.DecryptTextFromBase64(MD5Str, My.Computer.Registry.CurrentUser.GetValue("HKEY_CURRENT_USER\" & CredEntry.UserRegistrySubKey & "\" & CredEntry.UserRegistryKey & "_Username")))
                                result.Add(AESHelper.DecryptTextFromBase64(MD5Str, My.Computer.Registry.CurrentUser.GetValue("HKEY_CURRENT_USER\" & CredEntry.UserRegistrySubKey & "\" & CredEntry.UserRegistryKey & "_Password")))
                                Return result
                            Case Settings.EncryptionTypeEnum.AES256PSK
                                LogInstanceHandler.WriteLogEntry("Load credentials from AES256 (PSK)...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                result.Add(AESHelper.DecryptTextFromBase64(CredEntry.CredentialEncryptionPSK, My.Computer.Registry.CurrentUser.GetValue("HKEY_CURRENT_USER\" & CredEntry.UserRegistrySubKey & "\" & CredEntry.UserRegistryKey & "_Username")))
                                result.Add(AESHelper.DecryptTextFromBase64(CredEntry.CredentialEncryptionPSK, My.Computer.Registry.CurrentUser.GetValue("HKEY_CURRENT_USER\" & CredEntry.UserRegistrySubKey & "\" & CredEntry.UserRegistryKey & "_Password")))
                                Return result
                        End Select
                    Case Settings.CredentialLocationEnum.WindowsCredentialManager
                        LogInstanceHandler.WriteLogEntry("Load credentials from windows credential manager...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        Dim savecred As Net.NetworkCredential = Nothing
                        Try
                            savecred = CredentialManager.GetCredentials(CredEntry.CredentialKey)
                        Catch ex As Exception
                        End Try
                        If IsNothing(savecred) Then
                            LogInstanceHandler.WriteLogEntry("No credentials found, show windows-build-in login form...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                            savecred = CredentialManager.PromptForCredentials(CredEntry.CredentialKey, CredEntry.CheckSaveCredentailsOnLoginPrompt, CredEntry.WindowsCredentialMessage, CredEntry.WindowsCredentialCaption)
                            CredentialManager.SaveCredentials(CredEntry.CredentialKey, savecred)
                        End If
                        LogInstanceHandler.WriteLogEntry("Successful getting credentials from login form.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        result.Add(savecred.UserName)
                        result.Add(savecred.Password)
                        Return result
                End Select

                Return New List(Of String)
            Catch ex As Exception
                Return New List(Of String)
            End Try
        End Function

        Public Function SaveCredentials(ByVal CredEntry As CustomCredentialEntry, ByVal Username As String, ByVal Password As String) As Boolean
            Try
                Select Case CredEntry.CredentialLocation
                    Case Settings.CredentialLocationEnum.PluginSettings
                        LogInstanceHandler.WriteLogEntry("Save credentials to plugin settings file...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        Select Case CredEntry.CredentialEncryptionType
                            Case Settings.EncryptionTypeEnum.None
                                LogInstanceHandler.WriteLogEntry("Save credentials to plain-text...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                CredEntry.CredentialUsername = Username
                                CredEntry.CredentialPassword = Password
                                Return True
                            Case Settings.EncryptionTypeEnum.Base64
                                LogInstanceHandler.WriteLogEntry("Save credentials to base64...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim Base64HelperObj As New Base64Helper
                                CredEntry.CredentialUsername = Base64HelperObj.ToBase64(Username)
                                CredEntry.CredentialPassword = Base64HelperObj.ToBase64(Password)
                                Return True
                            Case Settings.EncryptionTypeEnum.MD5Hash
                                LogInstanceHandler.WriteLogEntry("Save credentials to md5-hash-encrypted string...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                Dim MD5Str As String
                                MD5Str = MD5HashHandlerObj.GetMD5FromFile(Application.ExecutablePath)
                                CredEntry.CredentialUsername = AESHelper.EncryptTextToBase64(MD5Str, Username)
                                CredEntry.CredentialPassword = AESHelper.EncryptTextToBase64(MD5Str, Password)
                                Return True
                            Case Settings.EncryptionTypeEnum.AES256PSK
                                LogInstanceHandler.WriteLogEntry("Save credentials to AES256 (PSK)...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                CredEntry.CredentialUsername = AESHelper.EncryptTextToBase64(CredEntry.CredentialEncryptionPSK, Username)
                                CredEntry.CredentialPassword = AESHelper.EncryptTextToBase64(CredEntry.CredentialEncryptionPSK, Password)
                                Return True
                        End Select
                    Case Settings.CredentialLocationEnum.UserAppData
                        LogInstanceHandler.WriteLogEntry("Save credentials to users appdata folder...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        Select Case CredEntry.CredentialEncryptionType
                            Case Settings.EncryptionTypeEnum.None
                                LogInstanceHandler.WriteLogEntry("Save credentials to plain-text...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                IO.File.WriteAllText(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & CredEntry.UserAppDataFilename, Username & vbNewLine & Password, System.Text.Encoding.UTF8)
                                Return True
                            Case Settings.EncryptionTypeEnum.Base64
                                LogInstanceHandler.WriteLogEntry("Save credentials to base64...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim Base64HelperObj As New Base64Helper
                                IO.File.WriteAllText(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & CredEntry.UserAppDataFilename, Base64HelperObj.ToBase64(Username) & vbNewLine & Base64HelperObj.ToBase64(Password), System.Text.Encoding.UTF8)
                                Return True
                            Case Settings.EncryptionTypeEnum.MD5Hash
                                LogInstanceHandler.WriteLogEntry("Save credentials to md5-hash-encrypted string...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                Dim MD5Str As String
                                MD5Str = MD5HashHandlerObj.GetMD5FromFile(Application.ExecutablePath)
                                IO.File.WriteAllText(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & CredEntry.UserAppDataFilename, AESHelper.EncryptTextToBase64(MD5Str, Username) & vbNewLine & AESHelper.EncryptTextToBase64(MD5Str, Password), System.Text.Encoding.UTF8)
                                Return True
                            Case Settings.EncryptionTypeEnum.AES256PSK
                                LogInstanceHandler.WriteLogEntry("Save credentials to AES256 (PSK)...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                IO.File.WriteAllText(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & CredEntry.UserAppDataFilename, AESHelper.EncryptTextToBase64(CredEntry.CredentialEncryptionPSK, Username) & vbNewLine & AESHelper.EncryptTextToBase64(CredEntry.CredentialEncryptionPSK, Password), System.Text.Encoding.UTF8)
                                Return True
                        End Select
                    Case Settings.CredentialLocationEnum.UserRegistry
                        LogInstanceHandler.WriteLogEntry("Save credentials to users registry...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        Select Case CredEntry.CredentialEncryptionType
                            Case Settings.EncryptionTypeEnum.None
                                LogInstanceHandler.WriteLogEntry("Save credentials to plain-text...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                My.Computer.Registry.CurrentUser.CreateSubKey(CredEntry.UserRegistrySubKey, True)
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & CredEntry.UserRegistrySubKey,
                                  CredEntry.UserRegistryKey & "_Username", Username)
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & CredEntry.UserRegistrySubKey,
                                CredEntry.UserRegistryKey & "_Password", Password)
                                Return True
                            Case Settings.EncryptionTypeEnum.Base64
                                LogInstanceHandler.WriteLogEntry("Save credentials to base64...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim Base64HelperObj As New Base64Helper
                                My.Computer.Registry.CurrentUser.CreateSubKey(CredEntry.UserRegistrySubKey, True)
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & CredEntry.UserRegistrySubKey,
                                  CredEntry.UserRegistryKey & "_Username", Base64HelperObj.ToBase64(Username))
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & CredEntry.UserRegistrySubKey,
                                CredEntry.UserRegistryKey & "_Password", Base64HelperObj.ToBase64(Password))
                                Return True
                            Case Settings.EncryptionTypeEnum.MD5Hash
                                LogInstanceHandler.WriteLogEntry("Save credentials to md5-hash-encrypted string...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                Dim MD5Str As String
                                MD5Str = MD5HashHandlerObj.GetMD5FromFile(Application.ExecutablePath)
                                My.Computer.Registry.CurrentUser.CreateSubKey(CredEntry.UserRegistrySubKey, True)
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & CredEntry.UserRegistrySubKey,
                                  CredEntry.UserRegistryKey & "_Username", AESHelper.EncryptTextToBase64(MD5Str, Username))
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & CredEntry.UserRegistrySubKey,
                                CredEntry.UserRegistryKey & "_Password", AESHelper.EncryptTextToBase64(MD5Str, Password))
                                Return True
                            Case Settings.EncryptionTypeEnum.AES256PSK
                                LogInstanceHandler.WriteLogEntry("Save credentials to AES256 (PSK)...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim MD5HashHandlerObj As New MD5HashHelper
                                Dim AESHelper As New EncryptDecryptHelper
                                My.Computer.Registry.CurrentUser.CreateSubKey(CredEntry.UserRegistrySubKey, True)
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & CredEntry.UserRegistrySubKey,
                                  CredEntry.UserRegistryKey & "_Username", AESHelper.EncryptTextToBase64(CredEntry.CredentialEncryptionPSK, Username))
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & CredEntry.UserRegistrySubKey,
                                CredEntry.UserRegistryKey & "_Password", AESHelper.EncryptTextToBase64(CredEntry.CredentialEncryptionPSK, Password))
                                Return True
                        End Select
                    Case Settings.CredentialLocationEnum.WindowsCredentialManager
                        LogInstanceHandler.WriteLogEntry("Save credentials to windows credential manager...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        Dim savecred As New Net.NetworkCredential
                        savecred.UserName = Username
                        savecred.Password = Password
                        Dim result As ICredential
                        result = CredentialManager.SaveCredentials(CredEntry.CredentialKey, savecred)
                        If result.TargetName = CredEntry.CredentialKey Then
                            Return True
                        Else
                            Return False
                        End If
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
                    LoginCredHandle.Clear()
                    If Not IsNothing(SettingsHandle.CustomCredentialsCollection) Then
                        If Not SettingsHandle.CustomCredentialsCollection.Count = 0 Then
                            For index = 0 To SettingsHandle.CustomCredentialsCollection.Count - 1
                                LogInstanceHandler.WriteLogEntry("Load custom credential " & SettingsHandle.CustomCredentialsCollection(index).CredentialKey & " ...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                                Dim custcred As New CredentialEntry
                                Dim results As List(Of String)
                                results = LoadCredentials(SettingsHandle.CustomCredentialsCollection(index))
                                custcred.CredentialKey = SettingsHandle.CredentialKeyPart & SettingsHandle.CustomCredentialsCollection(index).CredentialKey

                                If Not results.Count = 0 Then
                                    custcred.Username = CredHandler.ConvertStringInSecureString(results(0))
                                    custcred.Password = CredHandler.ConvertStringInSecureString(results(1))
                                End If

                                If Not IsNothing(custcred.Username) Then
                                    If Not custcred.Username.Length = 0 And Not custcred.Password.Length = 0 Then
                                        IsUserCredentialSet = True
                                    Else
                                        IsUserCredentialSet = False
                                    End If
                                Else
                                    IsUserCredentialSet = False
                                End If

                                LoginCredHandle.Add(custcred)
                            Next

                            Return True
                        End If
                    Else
                        Return False
                    End If
                Else
                    IsDisabled = True
                    IsUserCredentialSet = True
                    Return False
                End If

                Return True
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
            LoadPlugin()
            Return LoginCredHandle
        End Function
    End Class
End Module

