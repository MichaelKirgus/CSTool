'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Serializable> Public Class UserMainFormGUISettings
    Implements ICloneable
    Public Property ShowHostnameIPLabel As Boolean = True
    Public Property ShowHostnameIPTextbox As Boolean = True
    Public Property ShowHostnameIPSearchIcon As Boolean = True
    Public Property ShowHostnameIPRefreshIcon As Boolean = True
    Public Property ShowCustomActionIcon As Boolean = True
    Public Property ShowTemplateManagerIcon As Boolean = True
    Public Property ShowAddItemIcon As Boolean = True
    Public Property ShowRemoveItemIcon As Boolean = True
    Public Property ShowWorkspaceTemplateManagerIcon As Boolean = True
    Public Property ShowWindowManagerIcon As Boolean = True
    Public Property ShowNewInstanceIcon As Boolean = True
    Public Property ShowUserProfileManagerIcon As Boolean = True
    Public Property ShowLockunlockWorkpsaceIcon As Boolean = True

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MemberwiseClone()
    End Function
End Class
