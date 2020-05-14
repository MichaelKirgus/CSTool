'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Management
Imports CSToolWMIHelper

<Serializable> Public Class SettingsClass
    Public Property RaiseActions As Boolean = True
    Public Property ResetGUIBeforeGetData As Boolean = True
    Public Property WMIRefreshInterval As Integer = 3000
    Public Property WMIQueryTimeout As Integer = 2000
    Public Property CheckHostIsAlive As Boolean = True
    Public Property CheckHostIsAlivePingTimeout As Integer = 100
    Public Property UseCustomWMIConnectionOptions As Boolean = False
    Public Property WMICPUUsagePercentSection As String = "Win32_Processor"
    Public Property WMICPUUsagePercentClassName As String = "LoadPercentage"
    Public Property WMITotalSystemMemorySection As String = "Win32_OperatingSystem"
    Public Property WMITotalSystemMemoryClassName As String = "TotalVisibleMemorySize"
    Public Property WMIFreeSystemMemorySection As String = "Win32_OperatingSystem"
    Public Property WMIFreeSystemMemoryClassName As String = "FreePhysicalMemory"
    Public Property ShowWMIBatteryCharge As Boolean = True
    Public Property ShowPageFileInfo As Boolean = True
    Public Property WMIBatteryEstimatedChargeRemainingSection As String = "Win32_Battery"
    Public Property WMIBatteryEstimatedChargeRemainingClassName As String = "EstimatedChargeRemaining"
    Public Property WMIPageFileAllocatedBaseSizeSection As String = "Win32_PageFileUsage"
    Public Property WMIPageFileAllocatedBaseSizeClassName As String = "AllocatedBaseSize"
    Public Property WMIPageFileCurrentUsageSection As String = "Win32_PageFileUsage"
    Public Property WMIPageFileCurrentUsageClassName As String = "CurrentUsage"
    Public Property CPUUsageSize As Integer = 160
    Public Property SystemMemorySize As Integer = 220
    Public Property BatterySize As Integer = 160
    Public Property PageFileSize As Integer = 220
    Public Property CustomWMIConnectionOptionsUsername As String = ""
    Public Property CustomWMIConnectionOptionsPassword As String = ""
    Public Property CustomWMIConnectionOptionsAuthority As String = ""
    Public Property CustomWMIConnectionOptionsAuthentication As AuthenticationLevel = AuthenticationLevel.Packet
    Public Property CustomWMIConnectionOptionsImpersonation As ImpersonationLevel = ImpersonationLevel.Default
End Class

