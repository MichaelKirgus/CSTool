'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Management

<Serializable> Public Class Settings
    Public Property RaiseActions As Boolean = True
    Public Property ShutdownWMIExecScope As String = "\\%Clientname%\root\CIMV2"
    Public Property ShutdownWMIExecQuery As String = "Win32_Process"
    Public Property ShutdownWMIExecMethod As String = "Create"
    Public Property ShutdownWMIExecArgumentValue As String = "CommandLine"
    Public Property ShutdownWMIExecArgumentData As String = "shutdown.exe /s /f /t 0"
    Public Property ShutdownWMIExecWarningMessage As String = "Do you want to shutdown host &Clientname% ?"
    Public Property RestartWMIExecScope As String = "\\%Clientname%\root\CIMV2"
    Public Property RestartWMIExecQuery As String = "Win32_Process"
    Public Property RestartWMIExecMethod As String = "Create"
    Public Property RestartWMIExecArgumentValue As String = "CommandLine"
    Public Property RestartWMIExecArgumentData As String = "shutdown.exe /r /f /t 0"
    Public Property RestartWMIExecWarningMessage As String = "Do you want to restart host &Clientname% ?"
    Public Property StandbyWMIExecScope As String = "\\%Clientname%\root\CIMV2"
    Public Property StandbyWMIExecQuery As String = "Win32_Process"
    Public Property StandbyWMIExecMethod As String = "Create"
    Public Property StandbyWMIExecArgumentValue As String = "CommandLine"
    Public Property StandbyWMIExecArgumentData As String = "shutdown.exe /h"
    Public Property StandbyWMIExecWarningMessage As String = "Do you want to set host &Clientname% to standby?"
    Public Property LogoffWMIExecScope As String = "\\%Clientname%\root\CIMV2"
    Public Property LogoffWMIExecQuery As String = "Win32_Process"
    Public Property LogoffWMIExecMethod As String = "Create"
    Public Property LogoffWMIExecArgumentValue As String = "CommandLine"
    Public Property LogoffWMIExecArgumentData As String = "cmd.exe /C shutdown /l /f"
    Public Property LogoffWMIExecWarningMessage As String = "Do you want to logoff current user on host &Clientname% ?"
    Public Property StartVNCServiceWMIExecScope As String = "\\%Clientname%\root\CIMV2"
    Public Property StartVNCServiceWMIExecQuery As String = "Win32_Service"
    Public Property StartVNCServiceWMIExecMethod As String = "StartService"
    Public Property StartVNCServiceWMIExecArgumentValue As String = "Name"
    Public Property StartVNCServiceWMIExecArgumentData As String = "%UltraVNCServiceName%"
    Public Property StartVNCServiceWMIExecWarningMessage As String = "Do you want to logoff current user on host &Clientname% ?"
    Public Property CheckIfVNCServiceIsActivated As Boolean = True
    Public Property CheckIfVNCServiceIsActivatedSection As String = "Win32_Service WHERE (Name=%-%%UltraVNCServiceName%%-%,Started=true)"
    Public Property CheckIfVNCServiceIsActivatedClassName As String = "Name, Started"
    Public Property StartVNCViewerShellExec As String = "%CurrentUltraVNCViewerFilename% %Clientname%"
    Public Property StartVNCViewerCustomParameters As String = ""
    Public Property DetectVNCViewerWindow As Boolean = True
    Public Property VNCViewerUsername As String = "%VNCViewerSupportCredentialUsername%"
    Public Property VNCViewerPassword As String = "%VNCViewerSupportCredentialPassword%"
    Public Property AutoFillUltraVNCDomainAuth As Boolean = True
    Public Property VNCViewerAutoFillDelayAfterWindowDetected As Integer = 50
    Public Property VNCViewerAutoFillCtlTimeout As Integer = 10
    Public Property VNCViewerAutoFillCharTimeout As Integer = 2
    Public Property VNCViewerAutoFillSwitchCommand As String = "%KEY_TAB%"
    Public Property VNCViewerAutoFillSubmitCommand As String = "%KEY_ENTER%"
    Public Property DetectVNCViewerWindowCredentialsTitle As String = "UltraVNC Authentication"
    Public Property DetectVNCViewerWindowFailedTitle As String = "VncViewer Message Box"
    Public Property DetectVNCViewerWindowTimeout As Integer = 3000
    Public Property DetectVNCViewerWindowDelay As Integer = 200
    Public Property DetectVNCViewerWindowKillVNCOnFail As Boolean = True
    Public Property DetectVNCViewerWindowSetForeground As Boolean = False
    Public Property DetectVNCViewerSuppressMultipleInstances As Boolean = True
    Public Property ShowWarningOnCustomActions As Boolean = True
    Public Property UseCustomWMIConnectionOptions As Boolean = False
    Public Property CustomWMIConnectionOptionsUsername As String = ""
    Public Property CustomWMIConnectionOptionsPassword As String = ""
    Public Property CustomWMIConnectionOptionsAuthority As String = ""
    Public Property CustomWMIConnectionOptionsAuthentication As AuthenticationLevel = AuthenticationLevel.Packet
    Public Property CustomWMIConnectionOptionsImpersonation As ImpersonationLevel = ImpersonationLevel.Default
    Public Property CustomItemsCollection As New List(Of CustomItem)
End Class

<Serializable> Public Class CustomItem
    Public Property ItemType As CustomItemType = CustomItemType.LegacyExecCommandAndReturnConsoleRedirectionOutput
    Public Property Name As String = ""
    Public Property IconPath As String = ""
    Public Property WaitForExit As Boolean = True
    Public Property ReturnResult As Boolean = False
    Public Property Command As String = ""
    Public Property RunAsUsername As String = ""
    Public Property RunAsPassword As String = ""
    Public Property UseShellExecute As Boolean = False
    Public Property CreateNoWindow As Boolean = True
    Public Property ErrorDialog As Boolean = False
    Public Property LoadUserProfile As Boolean = True
    Public Property CustomItemExecTimeout As Integer = 0
    Public Property WarningMessage As String = ""
    Public Property UseAutoFill As Boolean = False
    Public Property AutoFillValue1 As String = ""
    Public Property AutoFillValue2 As String = ""
    Public Property AutoFillCtlTimeout As Integer = 0
    Public Property AutoFillCharTimeout As Integer = 0
    Public Property AutoFillSwitchCommand As String = ""
    Public Property AutoFillSubmitCommand As String = ""

    Public Enum CustomItemType As Integer
        LegacyExecCommandAndReturnConsoleRedirectionOutput = 0
    End Enum
End Class
