'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Serializable> Public Class Settings
    Public Property RaiseActions As Boolean = False
    Public Property PingAllHostsAtStart As Boolean = True
    Public Property ReCheckAllHostsAfterSettingsChange As Boolean = True
    Public Property HostCollection As New List(Of SSHHostItem)
    Public Property CommandCollection As New List(Of SSHExecuteItem)
End Class
