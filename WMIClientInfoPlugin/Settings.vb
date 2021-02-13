'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Management
Imports CSToolWMIHelper

<Serializable> Public Class Settings
    Public Property RaiseActions As Boolean = True
    Public Property ResetGUIBeforeGetData As Boolean = True
    Public Property CurrentLoggedOnUserSize As Integer = 214
    Public Property CurrentManufacturerSize As Integer = 214
    Public Property CurrentModelSize As Integer = 214
    Public Property CurrentOSSize As Integer = 214
    Public Property CurrentCPUSize As Integer = 214
    Public Property CurrentRAMSize As Integer = 214
    Public Property CurrentLastBootSize As Integer = 214
    Public Property CurrentSSDDriveSize As Integer = 214
    Public Property CheckHostIsAlive As Boolean = True
    Public Property CheckHostIsAlivePingTimeout As Integer = 100
    Public Property WMICurrentLoggedOnUserSection As String = "Win32_ComputerSystem"
    Public Property WMICurrentLoggedOnUserClassName As String = "UserName"
    Public Property WMIIsCurrentUserAdminScope As String = "\\%Clientname%\root\CIMV2"
    Public Property WMIIsCurrentUserAdminQuery As String = "SELECT * FROM Win32_GroupUser where (groupcomponent=%-%win32_group.name=\%-%Administratoren\%-%,domain=\%-%%Clientname%\%-%%-%)"
    Public Property WMIManufacturerSection As String = "Win32_ComputerSystem"
    Public Property WMIManufacturerClassName As String = "Manufacturer"
    Public Property WMIModelSection As String = "Win32_ComputerSystem"
    Public Property WMIModelClassName As String = "Model"
    Public Property WMIOSSection As String = "Win32_OperatingSystem"
    Public Property WMIOSClassName As String = "Caption, Version"
    Public Property WMICPUSection As String = "Win32_Processor"
    Public Property WMICPUClassName As String = "Name"
    Public Property WMIRAMSection As String = "Win32_ComputerSystem"
    Public Property WMIRAMClassName As String = "TotalPhysicalMemory"
    Public Property WMILastBootSection As String = "Win32_OperatingSystem"
    Public Property WMILastBootClassName As String = "LastBootUpTime"
    Public Property WMIDetectSSDScope As String = "\\%Clientname%\root\CIMV2"
    Public Property WMIDetectSSDQuery As String = "SELECT * FROM Win32_WinSAT"
    Public Property WMIDetectSSDWinSATValue As Integer = 6
    Public Property WMITotalOSDriveCapacitySection As String = "Win32_LogicalDisk WHERE Caption='C:'"
    Public Property WMITotalOSDriveCapacityClassName As String = "Size"
    Public Property WMIFreeOSDriveSpaceSection As String = "Win32_LogicalDisk WHERE Caption='C:'"
    Public Property WMIFreeOSDriveSpaceClassName As String = "FreeSpace"
    Public Property WMIRefreshInterval As Integer = 1000
    Public Property RefreshStaticWMIDataAtRefreshInterval As Boolean = False
    Public Property WMIQueryTimeout As Integer = 1000
    Public Property UseCustomWMIConnectionOptions As Boolean = False
    Public Property CustomWMIConnectionOptionsUsername As String = ""
    Public Property CustomWMIConnectionOptionsPassword As String = ""
    Public Property CustomWMIConnectionOptionsAuthority As String = ""
    Public Property CustomWMIConnectionOptionsAuthentication As AuthenticationLevel = AuthenticationLevel.Packet
    Public Property CustomWMIConnectionOptionsImpersonation As ImpersonationLevel = ImpersonationLevel.Default
    Public Property CustomWMIQueryCollection As New List(Of CustomWMIQueryItem)

    <Serializable> Public Class CustomWMIQueryItem
        Public Property DisplayName As String = ""
        Public Property DisplaySize As Integer = 214
        Public Property WMISection As String = ""
        Public Property WMIClassName As String = ""
        Public Property RefreshAtRefreshInterval As Boolean = False
    End Class
End Class

