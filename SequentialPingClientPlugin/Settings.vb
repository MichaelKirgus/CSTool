'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.ComponentModel
Imports System.Drawing
Imports CSToolPluginLib
<EditorBrowsable(System.ComponentModel.EditorBrowsableState.Always)>
<Serializable> Public Class Settings
    Public Property PingTimeout As Integer = 500
    Public Property AutoRefresh As Boolean = True
    Public Property AutRefreshInterval As Integer = 500
    Public Property MaxItems As Integer = 50
    Public Property ShowDateAndTime As Boolean = True
    Public Property Font As SerializableFont = New SerializableFont(SystemFonts.DefaultFont)
    Public Property DefaultForeColor As ColorWrapper = New ColorWrapper(Color.FromName("WindowText"))
    Public Property FontForeColorIfPingOK As ColorWrapper = New ColorWrapper(Color.FromName("WindowText"))
    Public Property FontForeColorIfPingError As ColorWrapper = New ColorWrapper(Color.FromName("WindowText"))
    Public Property DefaultBackColor As ColorWrapper = New ColorWrapper(Color.FromName("Window"))
    Public Property BackColorIfPingOK As ColorWrapper = New ColorWrapper(Color.LightGreen)
    Public Property BackColorIfPingError As ColorWrapper = New ColorWrapper(Color.LightCoral)
End Class
