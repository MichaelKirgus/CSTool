'Copyright (C) 2019-2021 Michael Kirgus
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.hddssdgroup = New System.Windows.Forms.GroupBox()
        Me.LblC = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.UsedSpaceC = New System.Windows.Forms.ProgressBar()
        Me.GetWMIInfoCurrentUser = New System.ComponentModel.BackgroundWorker()
        Me.GetWMIInfoManufacturer = New System.ComponentModel.BackgroundWorker()
        Me.GetWMIInfoModel = New System.ComponentModel.BackgroundWorker()
        Me.GetWMIOS = New System.ComponentModel.BackgroundWorker()
        Me.GetWMIInfoCPU = New System.ComponentModel.BackgroundWorker()
        Me.GetWMIInfoRAM = New System.ComponentModel.BackgroundWorker()
        Me.GetWMIInfoLastBoot = New System.ComponentModel.BackgroundWorker()
        Me.GetWMIInfoSSDSpace = New System.ComponentModel.BackgroundWorker()
        Me.RefreshWMIInfo = New System.Windows.Forms.Timer(Me.components)
        Me.GetWMIInfoIsCurrentUserAdmin = New System.ComponentModel.BackgroundWorker()
        Me.GetCustomWMIItems = New System.ComponentModel.BackgroundWorker()
        Me.PingCheck = New System.ComponentModel.BackgroundWorker()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.hddssdgroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TextBox2)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(223, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(214, 45)
        Me.GroupBox2.TabIndex = 21
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Manufacturer"
        '
        'TextBox2
        '
        Me.TextBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox2.Location = New System.Drawing.Point(3, 18)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(208, 22)
        Me.TextBox2.TabIndex = 3
        Me.TextBox2.Text = "No data"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.AutoSize = True
        Me.TableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(0, 0)
        Me.TableLayoutPanel2.TabIndex = 28
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(214, 45)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Current user"
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(3, 18)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(208, 22)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.Text = "No data"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TextBox3)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(3, 54)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(214, 45)
        Me.GroupBox3.TabIndex = 23
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Model"
        '
        'TextBox3
        '
        Me.TextBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox3.Location = New System.Drawing.Point(3, 18)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(208, 22)
        Me.TextBox3.TabIndex = 4
        Me.TextBox3.Text = "No data"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBox1)
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBox2)
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBox3)
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBox5)
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBox4)
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBox6)
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBox7)
        Me.FlowLayoutPanel1.Controls.Add(Me.hddssdgroup)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(445, 263)
        Me.FlowLayoutPanel1.TabIndex = 29
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.TextBox5)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(223, 54)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(214, 45)
        Me.GroupBox5.TabIndex = 25
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "OS"
        '
        'TextBox5
        '
        Me.TextBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox5.Location = New System.Drawing.Point(3, 18)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.TextBox5.Size = New System.Drawing.Size(208, 22)
        Me.TextBox5.TabIndex = 6
        Me.TextBox5.Text = "No data"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.TextBox4)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(3, 105)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(214, 49)
        Me.GroupBox4.TabIndex = 34
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "CPU"
        '
        'TextBox4
        '
        Me.TextBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox4.Location = New System.Drawing.Point(3, 18)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(208, 22)
        Me.TextBox4.TabIndex = 5
        Me.TextBox4.Text = "No data"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.TextBox6)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.Location = New System.Drawing.Point(223, 105)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(214, 49)
        Me.GroupBox6.TabIndex = 35
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "RAM"
        '
        'TextBox6
        '
        Me.TextBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox6.Location = New System.Drawing.Point(3, 18)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.ReadOnly = True
        Me.TextBox6.Size = New System.Drawing.Size(208, 22)
        Me.TextBox6.TabIndex = 8
        Me.TextBox6.Text = "No data"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.TextBox8)
        Me.GroupBox7.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.Location = New System.Drawing.Point(3, 160)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(214, 49)
        Me.GroupBox7.TabIndex = 36
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Last boot"
        '
        'TextBox8
        '
        Me.TextBox8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox8.Location = New System.Drawing.Point(3, 18)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.ReadOnly = True
        Me.TextBox8.Size = New System.Drawing.Size(208, 22)
        Me.TextBox8.TabIndex = 2
        Me.TextBox8.Text = "No data"
        '
        'hddssdgroup
        '
        Me.hddssdgroup.Controls.Add(Me.LblC)
        Me.hddssdgroup.Controls.Add(Me.Label2)
        Me.hddssdgroup.Controls.Add(Me.UsedSpaceC)
        Me.hddssdgroup.Dock = System.Windows.Forms.DockStyle.Top
        Me.hddssdgroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hddssdgroup.Location = New System.Drawing.Point(223, 160)
        Me.hddssdgroup.Name = "hddssdgroup"
        Me.hddssdgroup.Size = New System.Drawing.Size(214, 49)
        Me.hddssdgroup.TabIndex = 37
        Me.hddssdgroup.TabStop = False
        Me.hddssdgroup.Text = "HDD/SSD"
        '
        'LblC
        '
        Me.LblC.AutoEllipsis = True
        Me.LblC.AutoSize = True
        Me.LblC.Location = New System.Drawing.Point(6, 31)
        Me.LblC.Name = "LblC"
        Me.LblC.Size = New System.Drawing.Size(56, 16)
        Me.LblC.TabIndex = 2
        Me.LblC.Text = "No data"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(20, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "C:"
        '
        'UsedSpaceC
        '
        Me.UsedSpaceC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsedSpaceC.Location = New System.Drawing.Point(26, 18)
        Me.UsedSpaceC.MarqueeAnimationSpeed = 25
        Me.UsedSpaceC.Name = "UsedSpaceC"
        Me.UsedSpaceC.Size = New System.Drawing.Size(182, 12)
        Me.UsedSpaceC.TabIndex = 4
        '
        'GetWMIInfoCurrentUser
        '
        Me.GetWMIInfoCurrentUser.WorkerReportsProgress = True
        Me.GetWMIInfoCurrentUser.WorkerSupportsCancellation = True
        '
        'GetWMIInfoManufacturer
        '
        Me.GetWMIInfoManufacturer.WorkerReportsProgress = True
        Me.GetWMIInfoManufacturer.WorkerSupportsCancellation = True
        '
        'GetWMIInfoModel
        '
        Me.GetWMIInfoModel.WorkerReportsProgress = True
        Me.GetWMIInfoModel.WorkerSupportsCancellation = True
        '
        'GetWMIOS
        '
        Me.GetWMIOS.WorkerReportsProgress = True
        Me.GetWMIOS.WorkerSupportsCancellation = True
        '
        'GetWMIInfoCPU
        '
        Me.GetWMIInfoCPU.WorkerReportsProgress = True
        Me.GetWMIInfoCPU.WorkerSupportsCancellation = True
        '
        'GetWMIInfoRAM
        '
        Me.GetWMIInfoRAM.WorkerReportsProgress = True
        Me.GetWMIInfoRAM.WorkerSupportsCancellation = True
        '
        'GetWMIInfoLastBoot
        '
        Me.GetWMIInfoLastBoot.WorkerReportsProgress = True
        Me.GetWMIInfoLastBoot.WorkerSupportsCancellation = True
        '
        'GetWMIInfoSSDSpace
        '
        Me.GetWMIInfoSSDSpace.WorkerReportsProgress = True
        Me.GetWMIInfoSSDSpace.WorkerSupportsCancellation = True
        '
        'RefreshWMIInfo
        '
        Me.RefreshWMIInfo.Interval = 1000
        '
        'GetWMIInfoIsCurrentUserAdmin
        '
        Me.GetWMIInfoIsCurrentUserAdmin.WorkerReportsProgress = True
        Me.GetWMIInfoIsCurrentUserAdmin.WorkerSupportsCancellation = True
        '
        'GetCustomWMIItems
        '
        Me.GetCustomWMIItems.WorkerReportsProgress = True
        Me.GetCustomWMIItems.WorkerSupportsCancellation = True
        '
        'PingCheck
        '
        Me.PingCheck.WorkerReportsProgress = True
        Me.PingCheck.WorkerSupportsCancellation = True
        '
        'ClientGUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Name = "ClientGUI"
        Me.Size = New System.Drawing.Size(445, 263)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.hddssdgroup.ResumeLayout(False)
        Me.hddssdgroup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents TextBox2 As Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel2 As Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents TextBox3 As Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents TextBox1 As Windows.Forms.TextBox
    Friend WithEvents FlowLayoutPanel1 As Windows.Forms.FlowLayoutPanel
    Friend WithEvents GroupBox5 As Windows.Forms.GroupBox
    Friend WithEvents TextBox5 As Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents TextBox4 As Windows.Forms.TextBox
    Friend WithEvents GroupBox6 As Windows.Forms.GroupBox
    Friend WithEvents TextBox6 As Windows.Forms.TextBox
    Friend WithEvents GroupBox7 As Windows.Forms.GroupBox
    Friend WithEvents TextBox8 As Windows.Forms.TextBox
    Friend WithEvents hddssdgroup As Windows.Forms.GroupBox
    Friend WithEvents LblC As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents UsedSpaceC As Windows.Forms.ProgressBar
    Friend WithEvents GetWMIInfoCurrentUser As ComponentModel.BackgroundWorker
    Friend WithEvents GetWMIInfoManufacturer As ComponentModel.BackgroundWorker
    Friend WithEvents GetWMIInfoModel As ComponentModel.BackgroundWorker
    Friend WithEvents GetWMIOS As ComponentModel.BackgroundWorker
    Friend WithEvents GetWMIInfoCPU As ComponentModel.BackgroundWorker
    Friend WithEvents GetWMIInfoRAM As ComponentModel.BackgroundWorker
    Friend WithEvents GetWMIInfoLastBoot As ComponentModel.BackgroundWorker
    Friend WithEvents GetWMIInfoSSDSpace As ComponentModel.BackgroundWorker
    Friend WithEvents RefreshWMIInfo As Windows.Forms.Timer
    Friend WithEvents GetWMIInfoIsCurrentUserAdmin As ComponentModel.BackgroundWorker
    Friend WithEvents GetCustomWMIItems As ComponentModel.BackgroundWorker
    Friend WithEvents PingCheck As ComponentModel.BackgroundWorker
End Class
