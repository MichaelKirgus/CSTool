'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.DirectoryServices.Protocols
Imports System.Management

<Serializable> Public Class Settings
    Public Property RaiseActions As Boolean = True
    Public Property LDAPType As LDAPTypeEnum = LDAPTypeEnum.LDAP
    Public Property LDAPSServer As String = ""
    Public Property LDAPSServerPort As Integer = 636
    Public Property LDAPSServerAuthType As AuthType = AuthType.Negotiate
    Public Property ADInfoRootEntry As String = "LDAP://192.168.10.1/dc=TESTGROUP,DC=intra"
    Public Property DirectorySearcherFilterString As String = "(%and% (objectClass=user)(sAMAccountName=%CurrentUsernameWithoutDomain%))"
    Public Property DirectorySearcherCustomAuthentification As Boolean = False
    Public Property DirectorySearcherCustomAuthentificationUsername As String = ""
    Public Property DirectorySearcherCustomAuthentificationPassword As String = ""
    Public Property ClientTimeout As Integer = 5000
    Public Property PageSize As Integer = 10
    Public Property ServerPageTimeLimit As Integer = 5000
    Public Property ServerTimeLimit As Integer = 5000
    Public Property SizeLimit As Integer = 10
    Public Property CacheResults As Boolean = True
    Public Property GetCurrentUserOverWMI As Boolean = True
    Public Property WMICurrentLoggedOnUserSection As String = "Win32_ComputerSystem"
    Public Property WMICurrentLoggedOnUserClassName As String = "UserName"
    Public Property WMIQueryTimeout As Integer = 1000
    Public Property UseCustomWMIConnectionOptions As Boolean = False
    Public Property CustomWMIConnectionOptionsUsername As String = ""
    Public Property CustomWMIConnectionOptionsPassword As String = ""
    Public Property CustomWMIConnectionOptionsAuthority As String = ""
    Public Property CustomWMIConnectionOptionsAuthentication As AuthenticationLevel = AuthenticationLevel.Packet
    Public Property CustomWMIConnectionOptionsImpersonation As ImpersonationLevel = ImpersonationLevel.Default
    Public Property AutoResizeColumns As Boolean = True
End Class

<Serializable> Public Enum LDAPTypeEnum
    LDAP = 0
    LDAPS = 1
End Enum
