'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Serializable> Public Class Settings
    Public Property Activated As Boolean = True
    Public Property AllowSavingCredentials As Boolean = True
    Public Property CheckSaveCredentailsOnLoginPrompt As Boolean = True
    Public Property CredentialLocation As CredentialLocationEnum = CredentialLocationEnum.UserAppData
    Public Property CredentialEncryptionType As EncryptionTypeEnum = EncryptionTypeEnum.AES256PSK
    Public Property WindowsCredentialManagerTag As String = "CSToolRDPCredentials"
    Public Property DefaultUsername As String = ""
    Public Property UserAppDataFilename As String = "RDPCredentialData.dat"
    Public Property UserRegistrySubKey As String = ""
    Public Property UserRegistryKey As String = ""
    Public Property CredentialEncryptionPSK As String = "CSTool"
    Public Property CredentialUsername As String = ""
    Public Property CredentialPassword As String = ""

    Public Enum EncryptionTypeEnum As Integer
        None = 0
        Base64 = 1
        MD5Hash = 2
        AES256PSK = 3
        WindowsCredentialManager = 4
    End Enum

    Public Enum CredentialLocationEnum As Integer
        PluginSettings = 0
        UserAppData = 1
        UserRegistry = 2
        WindowsCredentialManager = 3
    End Enum
End Class
