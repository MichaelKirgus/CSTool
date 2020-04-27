'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Serializable> Public Class Settings
    Public Property InitialTitle As String = ""
    Public Property RaiseActions As Boolean = True
    Public Property InitialURL As String = ""
    Public Property RaiseActionURL As String = ""
    Public Property LoadURLAtStart As Boolean = True
    Public Property RaiseURLRefreshIfHostnameChanged As Boolean = True
    Public Property ShowWebsiteTitleInWindowTitle As Boolean = True
    Public Property UseCustomAuthentification As Boolean = False
    Public Property UseCustomAuthentificationUsername As String = ""
    Public Property UseCustomAuthentificationPassword As String = ""
    Public Property ShowNavigationToolbar As Boolean = True
    Public Property CachePath As String = ""
    Public Property PersistSessionCookies As Boolean = False
    Public Property IgnoreCertificateErrors As Boolean = False
    Public Property DisableGPURendering As Boolean = False
    Public Property WindowlessRendering As Boolean = False
    Public Property UserAgent As String = ""
    Public Property ChromiumExtensions As List(Of ExtensionEntry)
    Public Property EnableLog As Boolean = False
    Public Property Logfile As String = ""
End Class

<Serializable> Public Class ExtensionEntry
    Public Property Name As String = ""
    Public Property JavascriptCode As String = ""
End Class
