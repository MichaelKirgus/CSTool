'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports CSToolEnvironmentManager
Imports CSToolPingHelper
Imports CSToolWMIHelper

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public WMIHandler As New WMIHandler
    Public EnvManager As New EnvironmentManager
    Public CurrentIPHostname As String = ""
    Public CurrentUsername As String = ""
    Public IsHostOnline As Boolean = False
    <DllImport("uxtheme", CharSet:=CharSet.Unicode)>
    Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal textSubAppName As String, ByVal textSubIdList As String) As Integer
    End Function
    Public Sub SetUXThemeForAllListViews()
        Try
            SetWindowTheme(ListView1.Handle, "explorer", Nothing)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub RefreshGUI()

    End Sub

    Public Sub RaiseAction(ByVal HostOrIP As String)
        If Not HostOrIP = "" Then
            If Not CurrentIPHostname = HostOrIP Then
                If Not PingCheck.IsBusy Then
                    CurrentIPHostname = HostOrIP
                    IsHostOnline = False
                    PingCheck.RunWorkerAsync()
                End If
            End If
        End If
    End Sub

    Public Sub AddToListview(ByVal HostOrIP As String, ByVal CurrentLoggedOnUser As String, ByVal IsOnline As Boolean)
        Try
            Dim itm As New ListViewItem
            itm.Text = DateAndTime.Now.ToString
            itm.SubItems.Add(HostOrIP.ToUpper)
            itm.SubItems.Add(CurrentLoggedOnUser)
            If IsOnline Then
                itm.BackColor = Color.LightGreen
            Else
                itm.BackColor = Color.LightCoral
            End If
            ListView1.Items.Add(itm)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GetWMIInfo_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles GetWMIInfo.DoWork
        e.Result = WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMICurrentLoggedOnUserClassName), _Settings.WMICurrentLoggedOnUserSection, _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority), _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation)
    End Sub

    Private Sub PingCheck_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles PingCheck.DoWork
        Dim PingObj As New PingHelper
        e.Result = PingObj.Ping(CurrentIPHostname, _Settings.CheckHostIsAlivePingTimeout, False)
    End Sub

    Private Sub PingCheck_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles PingCheck.RunWorkerCompleted
        IsHostOnline = e.Result
        If Not GetWMIInfo.IsBusy Then
            GetWMIInfo.RunWorkerAsync(CurrentIPHostname)
        End If
    End Sub

    Private Sub GetWMIInfo_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles GetWMIInfo.RunWorkerCompleted
        AddToListview(CurrentIPHostname, e.Result, IsHostOnline)
        CurrentUsername = e.Result
        If _Settings.EnsureVisibleLastItem Then
            Try
                ListView1.Items(ListView1.Items.Count - 1).EnsureVisible()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub ClearItemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearItemToolStripMenuItem.Click
        Try
            ListView1.Items.Remove(ListView1.SelectedItems(0))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClearListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearListToolStripMenuItem.Click
        Try
            ListView1.Items.Clear()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CopyHostnameToClipboardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyHostnameToClipboardToolStripMenuItem.Click
        Try
            My.Computer.Clipboard.SetText(ListView1.SelectedItems(0).SubItems(1).Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CopyLastLoggedOnUserToClipboardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyLastLoggedOnUserToClipboardToolStripMenuItem.Click
        Try
            My.Computer.Clipboard.SetText(ListView1.SelectedItems(0).SubItems(2).Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUXThemeForAllListViews()
    End Sub
End Class
