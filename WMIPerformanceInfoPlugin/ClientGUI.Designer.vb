'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ClientGUI
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CPULoadBar = New System.Windows.Forms.ProgressBar()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.MemoryBar = New System.Windows.Forms.ProgressBar()
        Me.GetPerformanceWMIData = New System.ComponentModel.BackgroundWorker()
        Me.RefreshWMIInfo = New System.Windows.Forms.Timer(Me.components)
        Me.PingCheck = New System.ComponentModel.BackgroundWorker()
        Me.CPULoadPercentLbl = New System.Windows.Forms.Label()
        Me.MemoryConsumptionLbl = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.BatteryLevelLbl = New System.Windows.Forms.Label()
        Me.BatteryLevelBar = New System.Windows.Forms.ProgressBar()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.PageFileLbl = New System.Windows.Forms.Label()
        Me.PageFileBar = New System.Windows.Forms.ProgressBar()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBox1)
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBox2)
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBox3)
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBox4)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(415, 123)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CPULoadPercentLbl)
        Me.GroupBox1.Controls.Add(Me.CPULoadBar)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(176, 51)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "CPU load percentage"
        '
        'CPULoadBar
        '
        Me.CPULoadBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.CPULoadBar.Location = New System.Drawing.Point(3, 18)
        Me.CPULoadBar.Name = "CPULoadBar"
        Me.CPULoadBar.Size = New System.Drawing.Size(170, 12)
        Me.CPULoadBar.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.MemoryConsumptionLbl)
        Me.GroupBox2.Controls.Add(Me.MemoryBar)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(185, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(227, 51)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Memory consumption"
        '
        'MemoryBar
        '
        Me.MemoryBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.MemoryBar.Location = New System.Drawing.Point(3, 18)
        Me.MemoryBar.Name = "MemoryBar"
        Me.MemoryBar.Size = New System.Drawing.Size(221, 12)
        Me.MemoryBar.TabIndex = 0
        '
        'GetPerformanceWMIData
        '
        Me.GetPerformanceWMIData.WorkerReportsProgress = True
        Me.GetPerformanceWMIData.WorkerSupportsCancellation = True
        '
        'RefreshWMIInfo
        '
        Me.RefreshWMIInfo.Interval = 1000
        '
        'PingCheck
        '
        Me.PingCheck.WorkerReportsProgress = True
        Me.PingCheck.WorkerSupportsCancellation = True
        '
        'CPULoadPercentLbl
        '
        Me.CPULoadPercentLbl.AutoEllipsis = True
        Me.CPULoadPercentLbl.AutoSize = True
        Me.CPULoadPercentLbl.Location = New System.Drawing.Point(0, 33)
        Me.CPULoadPercentLbl.Name = "CPULoadPercentLbl"
        Me.CPULoadPercentLbl.Size = New System.Drawing.Size(56, 16)
        Me.CPULoadPercentLbl.TabIndex = 1
        Me.CPULoadPercentLbl.Text = "No data"
        '
        'MemoryConsumptionLbl
        '
        Me.MemoryConsumptionLbl.AutoEllipsis = True
        Me.MemoryConsumptionLbl.AutoSize = True
        Me.MemoryConsumptionLbl.Location = New System.Drawing.Point(0, 32)
        Me.MemoryConsumptionLbl.Name = "MemoryConsumptionLbl"
        Me.MemoryConsumptionLbl.Size = New System.Drawing.Size(56, 16)
        Me.MemoryConsumptionLbl.TabIndex = 2
        Me.MemoryConsumptionLbl.Text = "No data"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.BatteryLevelLbl)
        Me.GroupBox3.Controls.Add(Me.BatteryLevelBar)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(3, 60)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(176, 51)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Battery level"
        '
        'BatteryLevelLbl
        '
        Me.BatteryLevelLbl.AutoEllipsis = True
        Me.BatteryLevelLbl.AutoSize = True
        Me.BatteryLevelLbl.Location = New System.Drawing.Point(0, 33)
        Me.BatteryLevelLbl.Name = "BatteryLevelLbl"
        Me.BatteryLevelLbl.Size = New System.Drawing.Size(56, 16)
        Me.BatteryLevelLbl.TabIndex = 1
        Me.BatteryLevelLbl.Text = "No data"
        '
        'BatteryLevelBar
        '
        Me.BatteryLevelBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.BatteryLevelBar.Location = New System.Drawing.Point(3, 18)
        Me.BatteryLevelBar.Name = "BatteryLevelBar"
        Me.BatteryLevelBar.Size = New System.Drawing.Size(170, 12)
        Me.BatteryLevelBar.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.PageFileLbl)
        Me.GroupBox4.Controls.Add(Me.PageFileBar)
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(185, 60)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(227, 51)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Page file consumption"
        '
        'PageFileLbl
        '
        Me.PageFileLbl.AutoEllipsis = True
        Me.PageFileLbl.AutoSize = True
        Me.PageFileLbl.Location = New System.Drawing.Point(0, 32)
        Me.PageFileLbl.Name = "PageFileLbl"
        Me.PageFileLbl.Size = New System.Drawing.Size(56, 16)
        Me.PageFileLbl.TabIndex = 2
        Me.PageFileLbl.Text = "No data"
        '
        'PageFileBar
        '
        Me.PageFileBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.PageFileBar.Location = New System.Drawing.Point(3, 18)
        Me.PageFileBar.Name = "PageFileBar"
        Me.PageFileBar.Size = New System.Drawing.Size(221, 12)
        Me.PageFileBar.TabIndex = 0
        '
        'ClientGUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Name = "ClientGUI"
        Me.Size = New System.Drawing.Size(415, 123)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FlowLayoutPanel1 As Windows.Forms.FlowLayoutPanel
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents CPULoadBar As Windows.Forms.ProgressBar
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents MemoryBar As Windows.Forms.ProgressBar
    Friend WithEvents GetPerformanceWMIData As ComponentModel.BackgroundWorker
    Friend WithEvents RefreshWMIInfo As Windows.Forms.Timer
    Friend WithEvents PingCheck As ComponentModel.BackgroundWorker
    Friend WithEvents CPULoadPercentLbl As Windows.Forms.Label
    Friend WithEvents MemoryConsumptionLbl As Windows.Forms.Label
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents BatteryLevelLbl As Windows.Forms.Label
    Friend WithEvents BatteryLevelBar As Windows.Forms.ProgressBar
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents PageFileLbl As Windows.Forms.Label
    Friend WithEvents PageFileBar As Windows.Forms.ProgressBar
End Class
