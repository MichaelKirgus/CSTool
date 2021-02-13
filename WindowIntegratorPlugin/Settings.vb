'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Serializable> Public Class Settings
    Public Property Filename As String
    Public Property FilenameStartDelay As Integer = 100
    Public Property OnStartWait As Boolean = True
    Public Property WaitForInputIdle As Boolean = True
    Public Property BeforeIntegrationDelay As Integer = 0
    Public Property ManualIntegration As Boolean = False
    Public Property UseCurrentActiveWindowHandle As Boolean = False
    Public Property UseExtendedStartInfo As Boolean = False
    Public Property ActivateProcess As Boolean = False
    Public Property ProcessWorkingDir As String = ""
    Public Property UseShellExecute As Boolean = True
    Public Property ProcessArguments As String = ""
    Public Property LoadUserProfile As Boolean = False
    Public Property ProcessWindowStyle As Diagnostics.ProcessWindowStyle
    Public Property ProcessDomain As String = ""
    Public Property ProcessUsername As String = ""
    Public Property ProcessUsernamePassword As String = ""
    Public Property RescanForProcessHandle As Boolean = False
    Public Property RescanForProcessHandleProcessName As String = ""
    Public Property SearchWindow As Boolean = False
    Public Property WindowText As String = ""
    Public Property ExitApplicationOnExit As Boolean = True
    Public Property ExitApplicationProcessName As String = ""
    Public Property RestartApplication As Boolean = False
    Public Property ExitProcessIfIntegrationFails As Boolean = True
    Public Property InitialWindowTitle As String = "Window Integrator"
    Public Property ShowMainWindowTitleInWindowTitle As Boolean = True
End Class
