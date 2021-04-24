'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolEnvironmentManager
Imports CSToolLogLib.LogSettings

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public EnvManager As New EnvironmentManager
    Public CurrentIPHostname As String = ""

    Public Sub RaiseAction(ByVal IPOrHostname As String, Optional ByVal IsRefresh As Boolean = False)
        CurrentIPHostname = IPOrHostname
    End Sub
    Public Sub RefreshGUI(Optional ByVal AutoConnect As Boolean = True)

    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim rdphandle As New FreeRDP.Core.ConnectionSettings
        Dim rdpq As New FreeRDP.Core.RDP

        rdpq.Connect(_Settings.ServerName, _Settings.RDPDomain, _Settings.RDPUsername, _Settings.RDPPassword)

    End Sub
End Class
