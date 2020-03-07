'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Management

<Serializable> Public Class Settings
    Public Property SMSServer As String = ""
    Public Property SMSCollectionCollectTimeout As Integer = 29
    Public Property Clientname As String = ""
    Public Property SMSUsername As String = ""
    Public Property SMSPassword As String = ""
    Public Property RaiseActions As Boolean = True
    Public Property EnableLiveMode As Boolean = False
    Public Property SMSVerifyCollectionMembership As Boolean = True
    Public Property LoadSMSClients As Boolean = True
    Public Property CleanALLEntrysFromLists As Boolean = True
    Public Property AvailableCollectionsFilter As New List(Of CollectionFilterEntry)
    Public Property DeviceCollectionsFilter As New List(Of CollectionFilterEntry)
    Public Property AvailableCollectionsGroups As New List(Of CollectionGroupEntry)
    Public Property DeviceCollectionsGroups As New List(Of CollectionGroupEntry)
    Public Property SortCollections As Boolean = True
    Public Property SortClientMemberships As Boolean = True
    Public Property RefreshClientPoliciesWMIExecScope As String = "\\%Clientname%\root\ccm"
    Public Property RefreshClientPoliciestWMIExecQuery As String = "sms_client"
    Public Property RefreshClientPoliciesWMIExecMethod As String = "CALL"
    Public Property RefreshClientPoliciesWMIExecArgumentValue As String = "TriggerSchedule"
    Public Property RefreshClientPoliciesWMIExecArgumentData As String = "{00000000-0000-0000-0000-000000000021}"
    Public Property UseCustomWMIConnectionOptions As Boolean = False
    Public Property CustomWMIConnectionOptionsUsername As String = ""
    Public Property CustomWMIConnectionOptionsPassword As String = ""
    Public Property CustomWMIConnectionOptionsAuthority As String = ""
    Public Property CustomWMIConnectionOptionsAuthentication As AuthenticationLevel = AuthenticationLevel.Packet
    Public Property CustomWMIConnectionOptionsImpersonation As ImpersonationLevel = ImpersonationLevel.Default
    Public Property FilterCollectionsWithNoMembers As Boolean = False
    Public Property ShowDevicesAndUsernameSearchPanel As Boolean = True
End Class

<Serializable> Public Class CollectionFilterEntry
    Public Property FilterName As String = ""
    Public Property FilterText As String = ""
    Public Property Enabled As Boolean = True
End Class

<Serializable> Public Class CollectionGroupEntry
    Public Property GroupName As String = ""
    Public Property GroupText As String = ""
    Public Property Enabled As Boolean = True
End Class
