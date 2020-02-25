'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Net
Imports System.Security

Public Class CredentialHandler
    Public Function ConvertStringInSecureString(ByVal Password As String) As SecureString
        Dim Converter As New NetworkCredential("", Password)
        Return Converter.SecurePassword
    End Function

    Public Function ConvertSecureStringInString(ByVal SecureStr As SecureString) As String
        Dim Converter As New NetworkCredential("", SecureStr)
        Return Converter.Password
    End Function
End Class
