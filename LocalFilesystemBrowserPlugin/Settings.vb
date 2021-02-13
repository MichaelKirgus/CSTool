'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Windows.Forms

<Serializable> Public Class Settings
    Public Property InitialTitle As String = ""
    Public Property RaiseActionsLeftBrowser1 As Boolean = True
    Public Property RaiseActionsRightBrowser2 As Boolean = True
    Public Property SplitViewStyle As Orientation = Orientation.Vertical
    Public Property ViewStyle As ViewStyleEnum = ViewStyleEnum.ShowAllBrowsers
    Public Property InitialDirectoryLeftBrowser1 As String = ""
    Public Property InitialDirectoryRightBrowser2 As String = ""
    Public Property RaiseActionDirectoryLeftBrowser1 As String = ""
    Public Property RaiseActionDirectoryRightBrowser2 As String = ""

    <Serializable> Public Enum ViewStyleEnum As Integer
        ShowOnlyLeftBrowser = 0
        ShowOnlyRightBrowser = 1
        ShowAllBrowsers = 2
    End Enum
End Class


