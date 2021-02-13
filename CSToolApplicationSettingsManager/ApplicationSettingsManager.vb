'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.IO
Imports System.Xml.Serialization
Imports CSToolApplicationSettingsLib

Public Class ApplicationSettingsManager
    Public Property AppSettingFile = "AppSettings.xml"

    Public Function LoadSettings(ByVal Filename As String) As ApplicationSettings
        Try
            Dim objStreamReader As New StreamReader(Filename)
            Dim p2 As New ApplicationSettings
            Dim x As New XmlSerializer(p2.GetType)
            p2 = x.Deserialize(objStreamReader)
            objStreamReader.Close()

            Return p2
        Catch ex As Exception
            Return New ApplicationSettings
        End Try
    End Function

    Public Function SaveSettings(ByVal Settingsx As ApplicationSettings, ByVal Filename As String) As Boolean
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

    Public Function GetAppSettingsFilePath() As String
        Try
            'Loading app settings from all users app data path
            If IO.File.Exists(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & AppSettingFile) Then
                Return My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & AppSettingFile
            Else
                'Loading app settings from current user app data path
                If IO.File.Exists(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & AppSettingFile) Then
                    Return My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & AppSettingFile
                Else
                    'Loading from app settings-file from working dir
                    Return AppSettingFile
                End If
            End If
        Catch ex As Exception
            Return AppSettingFile
        End Try
    End Function
End Class
