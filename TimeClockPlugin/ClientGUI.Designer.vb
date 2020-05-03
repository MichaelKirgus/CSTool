'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ClientGUI
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ClientGUI))
        Me.TimeClockPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ResetTimeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResetNotificationsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SimulateTimeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReCalculateTimesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.SetSystemBootTimeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UseFirstAppStartupTimeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TotalWorktimeLbl = New System.Windows.Forms.TextBox()
        Me.StartWorktimeLbl = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.LunchTimeCountdownLbl = New System.Windows.Forms.TextBox()
        Me.LunchTimeLbl = New System.Windows.Forms.TextBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.BreakfastCountdownLbl = New System.Windows.Forms.TextBox()
        Me.BreakfastTimePickerCtl = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.WithoutBreaksCountdownLbl = New System.Windows.Forms.TextBox()
        Me.WithoutBreaksLbl = New System.Windows.Forms.TextBox()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.LaunchCountdownLbl = New System.Windows.Forms.TextBox()
        Me.LunchTimePickerCtl = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RegEndCountdownLbl = New System.Windows.Forms.TextBox()
        Me.RegEndLbl = New System.Windows.Forms.TextBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.CustomTimeCountdownLbl = New System.Windows.Forms.TextBox()
        Me.CustomEndTimePickerCtl = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.MaxEndCountdownLbl = New System.Windows.Forms.TextBox()
        Me.MaxEndLbl = New System.Windows.Forms.TextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ProgressBar2 = New System.Windows.Forms.ProgressBar()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.TimeClockPanel.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TimeClockPanel
        '
        Me.TimeClockPanel.AutoScroll = True
        Me.TimeClockPanel.ContextMenuStrip = Me.ContextMenuStrip1
        Me.TimeClockPanel.Controls.Add(Me.GroupBox1)
        Me.TimeClockPanel.Controls.Add(Me.GroupBox2)
        Me.TimeClockPanel.Controls.Add(Me.GroupBox7)
        Me.TimeClockPanel.Controls.Add(Me.GroupBox8)
        Me.TimeClockPanel.Controls.Add(Me.GroupBox5)
        Me.TimeClockPanel.Controls.Add(Me.GroupBox3)
        Me.TimeClockPanel.Controls.Add(Me.GroupBox6)
        Me.TimeClockPanel.Controls.Add(Me.GroupBox4)
        Me.TimeClockPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TimeClockPanel.Location = New System.Drawing.Point(0, 0)
        Me.TimeClockPanel.Name = "TimeClockPanel"
        Me.TimeClockPanel.Size = New System.Drawing.Size(488, 148)
        Me.TimeClockPanel.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ResetTimeToolStripMenuItem, Me.ResetNotificationsToolStripMenuItem, Me.SimulateTimeToolStripMenuItem, Me.ReCalculateTimesToolStripMenuItem, Me.ToolStripSeparator2, Me.SetSystemBootTimeToolStripMenuItem, Me.UseFirstAppStartupTimeToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(207, 142)
        '
        'ResetTimeToolStripMenuItem
        '
        Me.ResetTimeToolStripMenuItem.Image = Global.TimeClockPlugin.My.Resources.Resources.icon_x_circle_16x16
        Me.ResetTimeToolStripMenuItem.Name = "ResetTimeToolStripMenuItem"
        Me.ResetTimeToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.ResetTimeToolStripMenuItem.Text = "Reset time"
        '
        'ResetNotificationsToolStripMenuItem
        '
        Me.ResetNotificationsToolStripMenuItem.Image = Global.TimeClockPlugin.My.Resources.Resources.icon_announcement_16x16
        Me.ResetNotificationsToolStripMenuItem.Name = "ResetNotificationsToolStripMenuItem"
        Me.ResetNotificationsToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.ResetNotificationsToolStripMenuItem.Text = "Reset notifications"
        '
        'SimulateTimeToolStripMenuItem
        '
        Me.SimulateTimeToolStripMenuItem.Image = Global.TimeClockPlugin.My.Resources.Resources.icon_clock_16x16
        Me.SimulateTimeToolStripMenuItem.Name = "SimulateTimeToolStripMenuItem"
        Me.SimulateTimeToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.SimulateTimeToolStripMenuItem.Text = "Set start time..."
        '
        'ReCalculateTimesToolStripMenuItem
        '
        Me.ReCalculateTimesToolStripMenuItem.Image = Global.TimeClockPlugin.My.Resources.Resources.icon_refresh_16x16
        Me.ReCalculateTimesToolStripMenuItem.Name = "ReCalculateTimesToolStripMenuItem"
        Me.ReCalculateTimesToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.ReCalculateTimesToolStripMenuItem.Text = "Recalculate times"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(203, 6)
        '
        'SetSystemBootTimeToolStripMenuItem
        '
        Me.SetSystemBootTimeToolStripMenuItem.Image = Global.TimeClockPlugin.My.Resources.Resources.icon_moon_16x16
        Me.SetSystemBootTimeToolStripMenuItem.Name = "SetSystemBootTimeToolStripMenuItem"
        Me.SetSystemBootTimeToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.SetSystemBootTimeToolStripMenuItem.Text = "Use system boot time"
        '
        'UseFirstAppStartupTimeToolStripMenuItem
        '
        Me.UseFirstAppStartupTimeToolStripMenuItem.Image = Global.TimeClockPlugin.My.Resources.Resources.icon_browser_16x16
        Me.UseFirstAppStartupTimeToolStripMenuItem.Name = "UseFirstAppStartupTimeToolStripMenuItem"
        Me.UseFirstAppStartupTimeToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.UseFirstAppStartupTimeToolStripMenuItem.Text = "Use first app startup time"
        '
        'GroupBox1
        '
        Me.GroupBox1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GroupBox1.Controls.Add(Me.TotalWorktimeLbl)
        Me.GroupBox1.Controls.Add(Me.StartWorktimeLbl)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(115, 62)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Start"
        '
        'TotalWorktimeLbl
        '
        Me.TotalWorktimeLbl.Dock = System.Windows.Forms.DockStyle.Top
        Me.TotalWorktimeLbl.Location = New System.Drawing.Point(3, 40)
        Me.TotalWorktimeLbl.Name = "TotalWorktimeLbl"
        Me.TotalWorktimeLbl.ReadOnly = True
        Me.TotalWorktimeLbl.Size = New System.Drawing.Size(109, 22)
        Me.TotalWorktimeLbl.TabIndex = 1
        '
        'StartWorktimeLbl
        '
        Me.StartWorktimeLbl.Dock = System.Windows.Forms.DockStyle.Top
        Me.StartWorktimeLbl.Location = New System.Drawing.Point(3, 18)
        Me.StartWorktimeLbl.Name = "StartWorktimeLbl"
        Me.StartWorktimeLbl.ReadOnly = True
        Me.StartWorktimeLbl.Size = New System.Drawing.Size(109, 22)
        Me.StartWorktimeLbl.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GroupBox2.Controls.Add(Me.LunchTimeCountdownLbl)
        Me.GroupBox2.Controls.Add(Me.LunchTimeLbl)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(124, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(115, 66)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Lunch time"
        '
        'LunchTimeCountdownLbl
        '
        Me.LunchTimeCountdownLbl.Dock = System.Windows.Forms.DockStyle.Top
        Me.LunchTimeCountdownLbl.Location = New System.Drawing.Point(3, 40)
        Me.LunchTimeCountdownLbl.Name = "LunchTimeCountdownLbl"
        Me.LunchTimeCountdownLbl.ReadOnly = True
        Me.LunchTimeCountdownLbl.Size = New System.Drawing.Size(109, 22)
        Me.LunchTimeCountdownLbl.TabIndex = 1
        '
        'LunchTimeLbl
        '
        Me.LunchTimeLbl.Dock = System.Windows.Forms.DockStyle.Top
        Me.LunchTimeLbl.Location = New System.Drawing.Point(3, 18)
        Me.LunchTimeLbl.Name = "LunchTimeLbl"
        Me.LunchTimeLbl.ReadOnly = True
        Me.LunchTimeLbl.Size = New System.Drawing.Size(109, 22)
        Me.LunchTimeLbl.TabIndex = 0
        '
        'GroupBox7
        '
        Me.GroupBox7.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GroupBox7.Controls.Add(Me.BreakfastCountdownLbl)
        Me.GroupBox7.Controls.Add(Me.BreakfastTimePickerCtl)
        Me.GroupBox7.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.Location = New System.Drawing.Point(245, 3)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(115, 66)
        Me.GroupBox7.TabIndex = 6
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Breakfast"
        '
        'BreakfastCountdownLbl
        '
        Me.BreakfastCountdownLbl.Dock = System.Windows.Forms.DockStyle.Top
        Me.BreakfastCountdownLbl.Location = New System.Drawing.Point(3, 40)
        Me.BreakfastCountdownLbl.Name = "BreakfastCountdownLbl"
        Me.BreakfastCountdownLbl.ReadOnly = True
        Me.BreakfastCountdownLbl.Size = New System.Drawing.Size(109, 22)
        Me.BreakfastCountdownLbl.TabIndex = 2
        '
        'BreakfastTimePickerCtl
        '
        Me.BreakfastTimePickerCtl.Dock = System.Windows.Forms.DockStyle.Top
        Me.BreakfastTimePickerCtl.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.BreakfastTimePickerCtl.Location = New System.Drawing.Point(3, 18)
        Me.BreakfastTimePickerCtl.Name = "BreakfastTimePickerCtl"
        Me.BreakfastTimePickerCtl.ShowUpDown = True
        Me.BreakfastTimePickerCtl.Size = New System.Drawing.Size(109, 22)
        Me.BreakfastTimePickerCtl.TabIndex = 3
        Me.BreakfastTimePickerCtl.Value = New Date(2020, 5, 3, 9, 30, 0, 0)
        '
        'GroupBox5
        '
        Me.GroupBox5.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GroupBox5.Controls.Add(Me.WithoutBreaksCountdownLbl)
        Me.GroupBox5.Controls.Add(Me.WithoutBreaksLbl)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(3, 75)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(115, 66)
        Me.GroupBox5.TabIndex = 4
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Without breaks"
        '
        'WithoutBreaksCountdownLbl
        '
        Me.WithoutBreaksCountdownLbl.Dock = System.Windows.Forms.DockStyle.Top
        Me.WithoutBreaksCountdownLbl.Location = New System.Drawing.Point(3, 40)
        Me.WithoutBreaksCountdownLbl.Name = "WithoutBreaksCountdownLbl"
        Me.WithoutBreaksCountdownLbl.ReadOnly = True
        Me.WithoutBreaksCountdownLbl.Size = New System.Drawing.Size(109, 22)
        Me.WithoutBreaksCountdownLbl.TabIndex = 2
        '
        'WithoutBreaksLbl
        '
        Me.WithoutBreaksLbl.Dock = System.Windows.Forms.DockStyle.Top
        Me.WithoutBreaksLbl.Location = New System.Drawing.Point(3, 18)
        Me.WithoutBreaksLbl.Name = "WithoutBreaksLbl"
        Me.WithoutBreaksLbl.ReadOnly = True
        Me.WithoutBreaksLbl.Size = New System.Drawing.Size(109, 22)
        Me.WithoutBreaksLbl.TabIndex = 0
        '
        'GroupBox8
        '
        Me.GroupBox8.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GroupBox8.Controls.Add(Me.LaunchCountdownLbl)
        Me.GroupBox8.Controls.Add(Me.LunchTimePickerCtl)
        Me.GroupBox8.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox8.Location = New System.Drawing.Point(366, 3)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(115, 66)
        Me.GroupBox8.TabIndex = 7
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Lunch"
        '
        'LaunchCountdownLbl
        '
        Me.LaunchCountdownLbl.Dock = System.Windows.Forms.DockStyle.Top
        Me.LaunchCountdownLbl.Location = New System.Drawing.Point(3, 40)
        Me.LaunchCountdownLbl.Name = "LaunchCountdownLbl"
        Me.LaunchCountdownLbl.ReadOnly = True
        Me.LaunchCountdownLbl.Size = New System.Drawing.Size(109, 22)
        Me.LaunchCountdownLbl.TabIndex = 2
        '
        'LunchTimePickerCtl
        '
        Me.LunchTimePickerCtl.Dock = System.Windows.Forms.DockStyle.Top
        Me.LunchTimePickerCtl.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.LunchTimePickerCtl.Location = New System.Drawing.Point(3, 18)
        Me.LunchTimePickerCtl.Name = "LunchTimePickerCtl"
        Me.LunchTimePickerCtl.ShowUpDown = True
        Me.LunchTimePickerCtl.Size = New System.Drawing.Size(109, 22)
        Me.LunchTimePickerCtl.TabIndex = 3
        Me.LunchTimePickerCtl.Value = New Date(2020, 5, 3, 12, 30, 0, 0)
        '
        'GroupBox3
        '
        Me.GroupBox3.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GroupBox3.Controls.Add(Me.RegEndCountdownLbl)
        Me.GroupBox3.Controls.Add(Me.RegEndLbl)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(124, 75)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(115, 66)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Regular end"
        '
        'RegEndCountdownLbl
        '
        Me.RegEndCountdownLbl.Dock = System.Windows.Forms.DockStyle.Top
        Me.RegEndCountdownLbl.Location = New System.Drawing.Point(3, 40)
        Me.RegEndCountdownLbl.Name = "RegEndCountdownLbl"
        Me.RegEndCountdownLbl.ReadOnly = True
        Me.RegEndCountdownLbl.Size = New System.Drawing.Size(109, 22)
        Me.RegEndCountdownLbl.TabIndex = 2
        '
        'RegEndLbl
        '
        Me.RegEndLbl.Dock = System.Windows.Forms.DockStyle.Top
        Me.RegEndLbl.Location = New System.Drawing.Point(3, 18)
        Me.RegEndLbl.Name = "RegEndLbl"
        Me.RegEndLbl.ReadOnly = True
        Me.RegEndLbl.Size = New System.Drawing.Size(109, 22)
        Me.RegEndLbl.TabIndex = 0
        '
        'GroupBox6
        '
        Me.GroupBox6.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GroupBox6.Controls.Add(Me.CustomTimeCountdownLbl)
        Me.GroupBox6.Controls.Add(Me.CustomEndTimePickerCtl)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.Location = New System.Drawing.Point(245, 75)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(115, 66)
        Me.GroupBox6.TabIndex = 5
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Custom end"
        '
        'CustomTimeCountdownLbl
        '
        Me.CustomTimeCountdownLbl.Dock = System.Windows.Forms.DockStyle.Top
        Me.CustomTimeCountdownLbl.Location = New System.Drawing.Point(3, 40)
        Me.CustomTimeCountdownLbl.Name = "CustomTimeCountdownLbl"
        Me.CustomTimeCountdownLbl.ReadOnly = True
        Me.CustomTimeCountdownLbl.Size = New System.Drawing.Size(109, 22)
        Me.CustomTimeCountdownLbl.TabIndex = 2
        '
        'CustomEndTimePickerCtl
        '
        Me.CustomEndTimePickerCtl.Dock = System.Windows.Forms.DockStyle.Top
        Me.CustomEndTimePickerCtl.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.CustomEndTimePickerCtl.Location = New System.Drawing.Point(3, 18)
        Me.CustomEndTimePickerCtl.Name = "CustomEndTimePickerCtl"
        Me.CustomEndTimePickerCtl.ShowUpDown = True
        Me.CustomEndTimePickerCtl.Size = New System.Drawing.Size(109, 22)
        Me.CustomEndTimePickerCtl.TabIndex = 3
        Me.CustomEndTimePickerCtl.Value = New Date(2020, 5, 3, 17, 0, 0, 0)
        '
        'GroupBox4
        '
        Me.GroupBox4.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GroupBox4.Controls.Add(Me.MaxEndCountdownLbl)
        Me.GroupBox4.Controls.Add(Me.MaxEndLbl)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(366, 75)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(115, 66)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Max. end"
        '
        'MaxEndCountdownLbl
        '
        Me.MaxEndCountdownLbl.Dock = System.Windows.Forms.DockStyle.Top
        Me.MaxEndCountdownLbl.Location = New System.Drawing.Point(3, 40)
        Me.MaxEndCountdownLbl.Name = "MaxEndCountdownLbl"
        Me.MaxEndCountdownLbl.ReadOnly = True
        Me.MaxEndCountdownLbl.Size = New System.Drawing.Size(109, 22)
        Me.MaxEndCountdownLbl.TabIndex = 2
        '
        'MaxEndLbl
        '
        Me.MaxEndLbl.Dock = System.Windows.Forms.DockStyle.Top
        Me.MaxEndLbl.Location = New System.Drawing.Point(3, 18)
        Me.MaxEndLbl.Name = "MaxEndLbl"
        Me.MaxEndLbl.ReadOnly = True
        Me.MaxEndLbl.Size = New System.Drawing.Size(109, 22)
        Me.MaxEndLbl.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 148)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ProgressBar1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ProgressBar2)
        Me.SplitContainer1.Size = New System.Drawing.Size(488, 16)
        Me.SplitContainer1.SplitterDistance = 380
        Me.SplitContainer1.SplitterWidth = 2
        Me.SplitContainer1.TabIndex = 5
        '
        'ProgressBar1
        '
        Me.ProgressBar1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 0)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(380, 16)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 6
        '
        'ProgressBar2
        '
        Me.ProgressBar2.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ProgressBar2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ProgressBar2.Location = New System.Drawing.Point(0, 0)
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.Size = New System.Drawing.Size(106, 16)
        Me.ProgressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar2.TabIndex = 7
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "TimeClock"
        '
        'ClientGUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TimeClockPanel)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "ClientGUI"
        Me.Size = New System.Drawing.Size(488, 164)
        Me.TimeClockPanel.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TimeClockPanel As Windows.Forms.FlowLayoutPanel
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents StartWorktimeLbl As Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents LunchTimeLbl As Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents RegEndLbl As Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents MaxEndLbl As Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As Windows.Forms.GroupBox
    Friend WithEvents WithoutBreaksLbl As Windows.Forms.TextBox
    Friend WithEvents LunchTimeCountdownLbl As Windows.Forms.TextBox
    Friend WithEvents RegEndCountdownLbl As Windows.Forms.TextBox
    Friend WithEvents MaxEndCountdownLbl As Windows.Forms.TextBox
    Friend WithEvents WithoutBreaksCountdownLbl As Windows.Forms.TextBox
    Friend WithEvents SplitContainer1 As Windows.Forms.SplitContainer
    Friend WithEvents ProgressBar1 As Windows.Forms.ProgressBar
    Friend WithEvents ProgressBar2 As Windows.Forms.ProgressBar
    Friend WithEvents BackgroundWorker1 As ComponentModel.BackgroundWorker
    Friend WithEvents ContextMenuStrip1 As Windows.Forms.ContextMenuStrip
    Friend WithEvents ResetTimeToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents TotalWorktimeLbl As Windows.Forms.TextBox
    Friend WithEvents SimulateTimeToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents NotifyIcon1 As Windows.Forms.NotifyIcon
    Friend WithEvents SetSystemBootTimeToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ResetNotificationsToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As Windows.Forms.ToolStripSeparator
    Friend WithEvents UseFirstAppStartupTimeToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox6 As Windows.Forms.GroupBox
    Friend WithEvents CustomTimeCountdownLbl As Windows.Forms.TextBox
    Friend WithEvents CustomEndTimePickerCtl As Windows.Forms.DateTimePicker
    Friend WithEvents ReCalculateTimesToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox7 As Windows.Forms.GroupBox
    Friend WithEvents BreakfastCountdownLbl As Windows.Forms.TextBox
    Friend WithEvents BreakfastTimePickerCtl As Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox8 As Windows.Forms.GroupBox
    Friend WithEvents LaunchCountdownLbl As Windows.Forms.TextBox
    Friend WithEvents LunchTimePickerCtl As Windows.Forms.DateTimePicker
End Class
