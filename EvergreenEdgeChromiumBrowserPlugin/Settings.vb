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
    Public Property AllowSingleSignOnUsingOSPrimaryAccount As Boolean = True
    Public Property UserDataFolder As String = ""
    Public Property UseCustomAuthentification As Boolean = False
    Public Property UseCustomAuthentificationUsername As String = ""
    Public Property UseCustomAuthentificationPassword As String = ""
    Public Property ShowNavigationToolbar As Boolean = True
    Public Property EnableJavaScript As Boolean = True
    Public Property ContextMenuEnabled As Boolean = True
    Public Property StatusBarEnabled As Boolean = True
    Public Property DefaultScriptsDialogsEnabled As Boolean = True
    Public Property DeveloperToolsEnabled As Boolean = False
    Public Property HostObjectsEnabled As Boolean = True
    Public Property BuildInErrorPagesEnabled As Boolean = True
    Public Property ZoomEnabled As Boolean = True
    Public Property WebMessageEnabled As Boolean = True
    Public Property BrowserLanguage As String = ""
End Class
