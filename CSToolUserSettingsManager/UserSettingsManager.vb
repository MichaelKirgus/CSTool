'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.IO
Imports System.Xml.Serialization
Imports CSToolUserSettingsLib
Imports CSToolWindowsCredentialHelper

Public Class UserSettingsManager
    Public UserSettingsFile As String = "UserSettings.xml"
    Public CredentialBuildInHelperInstance As New CredentialBuildInHelper
    Public Function LoadSettings(ByVal Filename As String) As UserSettings
        Try
            Dim objStreamReader As New StreamReader(Filename)
            Dim p2 As New UserSettings
            Dim x As New XmlSerializer(p2.GetType)
            p2 = x.Deserialize(objStreamReader)
            objStreamReader.Close()

            Return p2
        Catch ex As Exception
            Return New UserSettings
        End Try
    End Function

    Public Function SaveSettings(ByVal Settingsx As UserSettings, ByVal Filename As String) As Boolean
        Try
            Dim XML As New XmlSerializer(Settingsx.GetType)
            Dim FS As New FileStream(Filename, FileMode.Create)
            XML.Serialize(FS, Settingsx)
            FS.Close()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadCentralCustomActions(ByVal Filename As String) As CentralCustomActions
        Try
            Dim objStreamReader As New StreamReader(Filename)
            Dim p2 As New CentralCustomActions
            Dim x As New XmlSerializer(p2.GetType)
            p2 = x.Deserialize(objStreamReader)
            objStreamReader.Close()

            Return p2
        Catch ex As Exception
            Return New CentralCustomActions
        End Try
    End Function

    Public Function SaveCentralCustomActions(ByVal Settingsx As CentralCustomActions, ByVal Filename As String) As Boolean
        Try
            Dim XML As New XmlSerializer(Settingsx.GetType)
            Dim FS As New FileStream(Filename, FileMode.Create)
            XML.Serialize(FS, Settingsx)
            FS.Close()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetUserSettingsFilePath(ByVal BasePath As String, ByVal UseDomain As Boolean, Optional ByVal WithFilename As Boolean = True, Optional ByVal OverridingUsername As String = "", Optional ByVal UserSettingName As String = "Default") As String
        'Loading user settings
        If OverridingUsername = "" Then
            If WithFilename Then
                If UseDomain Then
                    Return BasePath & "\" & CredentialBuildInHelperInstance.GetDomainName.ToUpper & "\" & CredentialBuildInHelperInstance.GetUserName.ToLower & "\" & UserSettingName & "\" & UserSettingsFile
                Else
                    Return BasePath & "\" & CredentialBuildInHelperInstance.GetUserName.ToLower & "\" & UserSettingName & "\" & UserSettingsFile
                End If
            Else
                If UseDomain Then
                    Return BasePath & "\" & CredentialBuildInHelperInstance.GetDomainName.ToUpper & "\" & CredentialBuildInHelperInstance.GetUserName.ToLower & "\" & UserSettingName
                Else
                    Return BasePath & "\" & CredentialBuildInHelperInstance.GetUserName.ToLower & "\" & UserSettingName
                End If
            End If
        Else
            If WithFilename Then
                If UseDomain Then
                    Return BasePath & "\" & CredentialBuildInHelperInstance.GetDomainName.ToUpper & "\" & OverridingUsername & "\" & UserSettingName & "\" & UserSettingsFile
                Else
                    Return BasePath & "\" & OverridingUsername & "\" & UserSettingName & "\" & UserSettingsFile
                End If
            Else
                If UseDomain Then
                    Return BasePath & "\" & CredentialBuildInHelperInstance.GetDomainName.ToUpper & "\" & OverridingUsername & "\" & UserSettingName
                Else
                    Return BasePath & "\" & OverridingUsername & "\" & UserSettingName
                End If
            End If
        End If
    End Function

    Public Function GetInitUserSettingsPath(ByVal BasePath As String, ByVal UseDomain As Boolean, Optional ByVal OverridingUsername As String = "", Optional ByVal UserSettingName As String = "Default") As String
        'Loading user settings
        If OverridingUsername = "" Then
            If UseDomain Then
                Return BasePath & "\" & CredentialBuildInHelperInstance.GetDomainName.ToUpper & "\" & CredentialBuildInHelperInstance.GetUserName.ToLower & "\" & UserSettingName
            Else
                Return BasePath & "\" & CredentialBuildInHelperInstance.GetUserName.ToLower & "\" & UserSettingName
            End If
        Else
            If UseDomain Then
                Return BasePath & "\" & CredentialBuildInHelperInstance.GetDomainName.ToUpper & "\" & OverridingUsername & "\" & UserSettingName
            Else
                Return BasePath & "\" & OverridingUsername & "\" & UserSettingName
            End If
        End If
    End Function
End Class
