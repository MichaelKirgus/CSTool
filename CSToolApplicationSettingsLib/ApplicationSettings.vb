'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolLogLib

<Serializable> Public Class ApplicationSettings
    Public Property GUIPluginDir As String = "GUIPlugins"
    Public Property CredentialPluginDir As String = "CredentialPlugins"
    Public Property EnvironmentPluginDir As String = "EnvironmentPlugins"
    Public Property UserProfileDir As String = "Profiles"
    Public Property UserTemplatesDir As String = "UserPluginTemplates"
    Public Property UserInitialTemplateDir As String = "UserInitialTemplate"
    Public Property UseUserDomainInFolderStructure As Boolean = False
    Public Property EnableLauncherSync As Boolean = False
    Public Property LauncherSyncPath As String = "%ProgramFiles%\Michael Kirgus\CSTool"
    Public Property LauncherSyncNeedsElevation As Boolean = True
    Public Property LauncherDeleteFilesFromSyncPath As Boolean = True
    Public Property LauncherIncludeFolderCollection As New List(Of LauncherIncludeFolderEntry)
    Public Property LauncherCreateMainApplicationShortcutOnDesktop As Boolean = True
    Public Property LogSettings As New LogSettings
End Class

<Serializable> Public Class LauncherIncludeFolderEntry
    Public Property FolderName As String = ""
    Public Property Recursive As Boolean = True
End Class
