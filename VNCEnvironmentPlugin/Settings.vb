'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Serializable> Public Class SettingsClass
    Public Property GetCurrentTightVNCViewerVersion As Boolean = True
    Public Property GetCurrentUltraVNCViewerVersion As Boolean = True
    Public Property GetTightVNCServiceState As Boolean = True
    Public Property GetUltraVNCServiceState As Boolean = True
    Public Property ReturnKeyboardControl As Boolean = True
    Public Property TightVNCViewerExecutablePath As String = "%ProgramFiles%\TightVNC\tvnviewer.exe"
    Public Property UltraVNCViewerExecutablePath As String = "%ProgramFiles%\uvnc bvba\UltraVNC\vncviewer.exe"
    Public Property TightVNCServiceName As String = "tvnserver"
    Public Property UltraVNCServiceName As String = "uvnc_service"
    Public Property GetTightVNCServiceName As String = True
    Public Property GetUltraVNCServiceName As String = True
End Class
