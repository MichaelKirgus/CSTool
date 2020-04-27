'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Windows.Forms
Imports CSToolEnvironmentManager

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public EnvManager As New EnvironmentManager

    Public StartTime As Date
    Public LunchTime As Date
    Public RegularEndTime As Date
    Public MaxEndTime As Date
    Public WithoutBreaksTime As Date

    Delegate Sub SetTextBoxTextDelegate(ByVal TextBoxCtl As TextBox, ByVal text As String)
    Delegate Sub SetProgressbarValueDelegate(ByVal ProgressCtl As ProgressBar, ByVal value As Integer)

    Public Sub SetTextboxText(ByVal TextBoxCtl As TextBox, ByVal text As String)
        Try
            If TextBoxCtl.InvokeRequired Then
                Dim d As SetTextBoxTextDelegate = New SetTextBoxTextDelegate(AddressOf SetTextboxText)
                Me.Invoke(d, New Object() {TextBoxCtl, text})
            Else
                TextBoxCtl.Text = text
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SetProgressbarValue(ByVal ProgressCtl As ProgressBar, ByVal value As Integer)
        Try
            If ProgressCtl.InvokeRequired Then
                Dim d As SetProgressbarValueDelegate = New SetProgressbarValueDelegate(AddressOf SetProgressbarValue)
                Me.Invoke(d, New Object() {ProgressCtl, value})
            Else
                ProgressCtl.Value = value
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub CheckForNotifications()
        Try
            For index = 0 To _Settings.CustomNotifications.Count - 1
                If _Settings.CustomNotifications(index).Enable And _Settings.CustomNotifications(index).WasFired = False Then
                    If _Settings.CustomNotifications(index).FireAtElapsedMinute <= DateAndTime.Now.Subtract(StartTime).TotalMinutes Then
                        NotifyIcon1.Visible = True
                        NotifyIcon1.ShowBalloonTip(2000, _Settings.CustomNotifications(index).NotificationTitle, _Settings.CustomNotifications(index).NotificationText, _Settings.CustomNotifications(index).NotificationType)
                        NotifyIcon1.Visible = False
                        _Settings.CustomNotifications(index).WasFired = True
                    End If
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ClearNotificationFireState()
        Try
            For index = 0 To _Settings.CustomNotifications.Count - 1
                _Settings.CustomNotifications(index).WasFired = False
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Function CalculateStaticTimes(ByVal SimulateStartTime As Boolean) As Boolean
        Try
            If Not SimulateStartTime Then
                If _Settings.UseSystemBootupTime Then
                    Dim tmpsysdate As New Date
                    tmpsysdate = DateTime.Now.AddMilliseconds(-Environment.TickCount)
                    If _Settings.UseLocalFileTimestampIfOlder Then
                        Dim tmpfiledate As New Date
                        tmpfiledate = GetStartTimestamp(_Settings.WorktimeTag)
                        Dim result As Integer
                        result = DateTime.Compare(tmpfiledate, tmpsysdate)
                        If result < 0 Then
                            StartTime = tmpfiledate
                        Else
                            StartTime = tmpsysdate
                        End If
                    Else
                        StartTime = tmpsysdate
                    End If
                Else
                    StartTime = GetStartTimestamp(_Settings.WorktimeTag)
                    SaveStartTimestamp(StartTime, _Settings.WorktimeTag)
                End If
            End If

            StartWorktimeLbl.Text = StartTime.ToShortTimeString

            LunchTime = StartTime.AddMinutes(_Settings.NormalWorktimeMaxSpan).AddMinutes(_Settings.StartWorktimeOffset)
            RegularEndTime = StartTime.AddMinutes(_Settings.StartWorktimeOffset).AddMinutes(_Settings.NormalWorktimeSpan).AddMinutes(_Settings.NormalWorktimeBreakfastSpan).AddMinutes(_Settings.NormalWorktimeLunchSpan).AddMinutes(_Settings.EndWorktimeOffset)
            MaxEndTime = StartTime.AddMinutes(_Settings.StartWorktimeOffset).AddMinutes(_Settings.MaxWorktimeSpan).AddMinutes(_Settings.NormalWorktimeBreakfastSpan).AddMinutes(_Settings.NormalWorktimeLunchSpan).AddMinutes(_Settings.MaxWorktimeAdditionalBreakSpan).AddMinutes(_Settings.EndWorktimeOffset)
            WithoutBreaksTime = StartTime.AddMinutes(_Settings.StartWorktimeOffset).AddMinutes(_Settings.NormalWorktimeSpan).AddMinutes(_Settings.EndWorktimeOffset)

            LunchTimeLbl.Text = LunchTime.ToShortTimeString
            RegEndLbl.Text = RegularEndTime.ToShortTimeString
            MaxEndLbl.Text = MaxEndTime.ToShortTimeString
            WithoutBreaksLbl.Text = WithoutBreaksTime.ToShortTimeString

            ProgressBar1.Value = 0
            ProgressBar2.Value = 0
            If RegularEndTime.Subtract(DateAndTime.Now).TotalSeconds + DateAndTime.Now.Subtract(StartTime).TotalSeconds > 0 Then
                ProgressBar1.Maximum = RegularEndTime.Subtract(DateAndTime.Now).TotalSeconds + DateAndTime.Now.Subtract(StartTime).TotalSeconds
            End If
            If MaxEndTime.Subtract(DateAndTime.Now).TotalSeconds - RegularEndTime.Subtract(DateAndTime.Now).TotalSeconds > 0 Then
                ProgressBar2.Maximum = MaxEndTime.Subtract(DateAndTime.Now).TotalSeconds - RegularEndTime.Subtract(DateAndTime.Now).TotalSeconds
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SaveStartTimestamp(ByVal StartTime As Date, ByVal TimeTag As String) As Boolean
        Try
            Dim filename As String
            filename = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & TimeTag & ".tmp"
            IO.File.WriteAllText(filename, StartTime.ToFileTime)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetStartTimestamp(ByVal TimeTag As String) As Date
        Try
            Dim filename As String
            filename = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & TimeTag & ".tmp"
            Dim finfo As New IO.FileInfo(filename)
            If finfo.LastWriteTime.Date.ToShortDateString = DateAndTime.Now.Date.ToShortDateString Then
                Dim datestr As Long
                datestr = IO.File.ReadAllText(filename)

                Return Date.FromFileTime(datestr)
            Else
                IO.File.Delete(filename)
                Return DateAndTime.Now
            End If
        Catch ex As Exception
            Return DateAndTime.Now
        End Try
    End Function

    Public Function ResetTimestamp(ByVal TimeTag As String) As Boolean
        Try
            Dim filename As String
            filename = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & TimeTag & ".tmp"
            Dim finfo As New IO.FileInfo(filename)
            IO.File.Delete(filename)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub RefreshGUI()
        NotifyIcon1.Visible = _Settings.AlwaysShowTrayIcon
    End Sub

    Public Sub UnloadPlugin()
        If BackgroundWorker1.IsBusy Then
            BackgroundWorker1.CancelAsync()
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        While e.Cancel = False
            Try
                SetTextboxText(LunchTimeCountdownLbl, LunchTime.Subtract(DateAndTime.Now).ToString("g").Split(",")(0).Replace("-", "+ "))
                SetTextboxText(RegEndCountdownLbl, RegularEndTime.Subtract(DateAndTime.Now).ToString("g").Split(",")(0).Replace("-", "+ "))
                SetTextboxText(MaxEndCountdownLbl, MaxEndTime.Subtract(DateAndTime.Now).ToString("g").Split(",")(0).Replace("-", "+ "))
                SetTextboxText(WithoutBreaksCountdownLbl, WithoutBreaksTime.Subtract(DateAndTime.Now).ToString("g").Split(",")(0).Replace("-", "+ "))
                SetTextboxText(TotalWorktimeLbl, DateAndTime.Now.Subtract(StartTime).ToString("g").Split(",")(0).Replace("-", "+ "))

                If DateAndTime.Now.Subtract(StartTime).TotalSeconds <= ProgressBar1.Maximum Then
                    SetProgressbarValue(ProgressBar1, DateAndTime.Now.Subtract(StartTime).TotalSeconds)
                Else
                    SetProgressbarValue(ProgressBar1, ProgressBar1.Maximum)
                    If DateAndTime.Now.Subtract(StartTime).TotalSeconds - ProgressBar1.Maximum <= ProgressBar2.Maximum Then
                        SetProgressbarValue(ProgressBar2, DateAndTime.Now.Subtract(StartTime).TotalSeconds - ProgressBar1.Maximum)
                    Else
                        SetProgressbarValue(ProgressBar2, ProgressBar2.Maximum)
                    End If
                End If

                If Not _Settings.CustomNotifications.Count = 0 Then
                    CheckForNotifications()
                End If

                NotifyIcon1.Text = RegularEndTime.Subtract(DateAndTime.Now).ToString("g").Split(",")(0).Replace("-", "+ ")

                Threading.Thread.Sleep(_Settings.RefreshInterval)
            Catch ex As Exception
            End Try
        End While
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NotifyIcon1.Visible = _Settings.AlwaysShowTrayIcon
        If Not _Settings.WindowTitle = "" Then
            _ParentInstance.CurrentWindowTitle = _Settings.WindowTitle
        End If
        GroupBox2.Visible = _Settings.ShowBreakfast
        CalculateStaticTimes(False)
        If Not BackgroundWorker1.IsBusy Then
            BackgroundWorker1.RunWorkerAsync()
        End If
    End Sub

    Private Sub ResetTimeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetTimeToolStripMenuItem.Click
        ResetTimestamp(_Settings.WorktimeTag)
        CalculateStaticTimes(False)
        ClearNotificationFireState()
    End Sub

    Private Sub SimulateTimeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SimulateTimeToolStripMenuItem.Click
        Dim simfrm As New SimulateTime
        simfrm._parent = Me
        simfrm.Show()
    End Sub

    Private Sub SetSystemBootTimeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetSystemBootTimeToolStripMenuItem.Click
        StartTime = DateTime.Now.AddMilliseconds(-Environment.TickCount)
        CalculateStaticTimes(True)
        ClearNotificationFireState()
    End Sub

    Private Sub ResetNotificationsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetNotificationsToolStripMenuItem.Click
        ClearNotificationFireState()
    End Sub

    Private Sub UseFirstAppStartupTimeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UseFirstAppStartupTimeToolStripMenuItem.Click
        StartTime = GetStartTimestamp(_Settings.WorktimeTag)
        CalculateStaticTimes(True)
        ClearNotificationFireState()
    End Sub
End Class
