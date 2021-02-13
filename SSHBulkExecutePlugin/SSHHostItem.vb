'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Serializable> Public Class SSHHostItem
    Public Property Hostname As String = ""
    Public Property Description As String = ""
    Public Property CheckHost As Boolean = True
    Public Property PingTimeout As Integer = 100
    Public Property AllowExecute As Boolean = True

    Public Property EstablishConnectionAtStart As Boolean = False
    Public Property TryEstablishConnectionIfNoPing As Boolean = False
    Public Property ConnectionTimeout As Integer = 10
    Public Property SSHLoginUsername As String = ""
    Public Property SSHLoginPassword As String = ""
    Public Property SSHLoginPrivateKeyPlain As String = ""
    Public Property SSHLoginPrivateKeyFile As String = ""
    Public Property SSHLoginPrivateKeyFilePassword As String = ""
    Public Property SessionEncoding As SessionEncodingEnum = SessionEncodingEnum.UTF8
    Public Property CloseConnectionAfterExecute As Boolean = False

    Public Enum SessionEncodingEnum As Integer
        UTF8 = 0
        UTF32 = 1
        UTF7 = 2
        ASCII = 3
        BigEndianUnicode = 4
        Unicode = 5
    End Enum
End Class
