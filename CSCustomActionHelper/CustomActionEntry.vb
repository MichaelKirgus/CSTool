'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Public Class CustomActionEntry
    Public Property Name As String = ""
    Public Property IconPath As String = ""
    Public Property WaitForExit As Boolean = True
    Public Property Command As String = ""
    Public Property WorkingDir As String = ""
    Public Property CommandContainsArguments As Boolean = False
    Public Property RunAsUsername As String = ""
    Public Property RunAsPassword As String = ""
    Public Property RunAsDomain As String = ""
    Public Property UseShellExecute As Boolean = False
    Public Property CreateNoWindow As Boolean = True
    Public Property ErrorDialog As Boolean = False
    Public Property LoadUserProfile As Boolean = True
    Public Property UseRunAsVerb As Boolean = False
    Public Property CustomItemExecTimeout As Integer = 0
    Public Property WarningMessage As String = ""
End Class
