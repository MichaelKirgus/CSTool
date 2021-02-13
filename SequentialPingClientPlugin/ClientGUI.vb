'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports CSToolLogLib.LogSettings
Imports CSToolPingHelper

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public PingManager As New PingHelper
    Public CurrentIPHostname As String = ""
    Public CurrentIPv4 As String = ""
    Public CurrentIPv6 As String = ""

    Sub RaiseAction(ByVal IPOrHostname As String, Optional ByVal IsRefresh As Boolean = False)
        If Not IsRefresh Then
            ListBox1.BackColor = Color.FromArgb(_Settings.DefaultBackColor.Argb)
            ListBox1.ForeColor = Color.FromArgb(_Settings.DefaultForeColor.Argb)
            ListBox1.Items.Clear()
        End If

        If Not IPOrHostname = "" Then
            CurrentIPHostname = IPOrHostname
            If Not SeqPingWorker.IsBusy Then
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Start ping worker for host " & CurrentIPHostname, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                SeqPingWorker.RunWorkerAsync(IPOrHostname)
            End If

            If _Settings.AutoRefresh Then
                If RefreshToolStripMenuItem.Checked Then
                    RefreshTimer.Start()
                End If
            End If
        Else
            RefreshTimer.Stop()
        End If
    End Sub

    Private Sub SeqPingWorker_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles SeqPingWorker.DoWork
        e.Result = PingManager.Ping(e.Argument, _Settings.PingTimeout, True)
    End Sub

    Private Sub SeqPingWorker_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles SeqPingWorker.RunWorkerCompleted
        Dim DateT As String = ""
        If _Settings.ShowDateAndTime Then
            DateT = DateAndTime.Now.ToString & " "
        End If

        If Not IsNothing(PingManager.IpAddressV4) Then
            CurrentIPv4 = PingManager.IpAddressV4
        Else
            CurrentIPv4 = ""
        End If
        If Not IsNothing(PingManager.IpAddressV6) Then
            CurrentIPv6 = PingManager.IpAddressV6
        Else
            CurrentIPv6 = ""
        End If

        If e.Result Then
            ListBox1.Items.Add(DateT & PingManager.HostName & " [" & PingManager.IpAddressV4 & "] [ " & PingManager.IpAddressV6 & " ] " & PingManager.ResponseTime & " ms")
            ListBox1.BackColor = Color.FromArgb(_Settings.BackColorIfPingOK.Argb)
            ListBox1.ForeColor = Color.FromArgb(_Settings.FontForeColorIfPingOK.Argb)
            If AutoScrollToolStripMenuItem.Checked Then
                ListBox1.SetSelected(ListBox1.Items.Count - 1, True)
            End If
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Ping worker: " & PingManager.HostName & " [" & PingManager.IpAddressV4 & "] [ " & PingManager.IpAddressV6 & " ] " & PingManager.ResponseTime & " ms", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
        Else
            ListBox1.Items.Add(DateT & PingManager.HostName & " [" & PingManager.IpAddressV4 & "] [ " & PingManager.IpAddressV6 & " ] Error: " & PingManager.ErrorDesc)
            ListBox1.BackColor = Color.FromArgb(_Settings.BackColorIfPingError.Argb)
            ListBox1.ForeColor = Color.FromArgb(_Settings.FontForeColorIfPingError.Argb)
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Ping worker: " & " [" & PingManager.IpAddressV4 & "] [ " & PingManager.IpAddressV6 & " ] Error: " & PingManager.ErrorDesc, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
        End If

        If ListBox1.Items.Count >= _Settings.MaxItems Then
            ListBox1.Items.RemoveAt(0)
        End If
    End Sub

    Public Sub RefreshGUI()
        ListBox1.Font = _Settings.Font.ToFont
        ListBox1.ForeColor = Color.FromArgb(_Settings.DefaultForeColor.Argb)
        ListBox1.BackColor = Color.FromArgb(_Settings.DefaultBackColor.Argb)
        RefreshTimer.Interval = _Settings.AutRefreshInterval
        RefreshToolStripMenuItem.Checked = _Settings.AutoRefresh
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshGUI()
    End Sub

    Private Sub RefreshTimer_Tick(sender As Object, e As EventArgs) Handles RefreshTimer.Tick
        RaiseAction(CurrentIPHostname, True)
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        If RefreshToolStripMenuItem.Checked Then
            If Not RefreshTimer.Enabled Then
                RefreshTimer.Start()
            End If
        Else
            If RefreshTimer.Enabled Then
                RefreshTimer.Stop()
            End If
        End If
    End Sub

    Private Sub CopyEntryToClipboardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyEntryToClipboardToolStripMenuItem.Click
        My.Computer.Clipboard.SetText(ListBox1.SelectedItem)
    End Sub

    Private Sub CopyIPToClipboardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyIPToClipboardToolStripMenuItem.Click
        My.Computer.Clipboard.SetText(CurrentIPv4)
    End Sub

    Private Sub CopyIpv6ToClipboardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyIpv6ToClipboardToolStripMenuItem.Click
        My.Computer.Clipboard.SetText(CurrentIPv6)
    End Sub

    Private Sub ClearOutputToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearOutputToolStripMenuItem.Click
        ListBox1.Items.Clear()
    End Sub

    Private Sub SaveOutputToFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveOutputToFileToolStripMenuItem.Click
        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Try
            If Not ListBox1.Items.Count = 0 Then
                Dim logstr As String = ""

                For index = 0 To ListBox1.Items.Count - 1
                    logstr += ListBox1.Items(index) & vbNewLine
                Next

                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, logstr, False)
            End If

            MsgBox("Output saved to " & SaveFileDialog1.FileName, MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Export failed!", MsgBoxStyle.Exclamation)
        End Try
    End Sub
End Class
