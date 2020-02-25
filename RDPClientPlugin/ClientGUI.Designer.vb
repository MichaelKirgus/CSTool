'Copyright (C) 2019-2020 Michael Kirgus
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.RaiseAutoConnect = New System.ComponentModel.BackgroundWorker()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripTextBox2 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripLabel4 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripTextBox3 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.RDPBox = New System.Windows.Forms.Panel()
        Me.LoadImage = New System.Windows.Forms.PictureBox()
        Me.ConnectionClosed = New System.Windows.Forms.PictureBox()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.LoadImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ConnectionClosed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RaiseAutoConnect
        '
        Me.RaiseAutoConnect.WorkerReportsProgress = True
        Me.RaiseAutoConnect.WorkerSupportsCancellation = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.ToolStripComboBox1, Me.ToolStripLabel2, Me.ToolStripTextBox1, Me.ToolStripLabel3, Me.ToolStripTextBox2, Me.ToolStripLabel4, Me.ToolStripTextBox3, Me.ToolStripSeparator1, Me.ToolStripButton3, Me.ToolStripButton2, Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(866, 29)
        Me.ToolStrip1.TabIndex = 4
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(48, 26)
        Me.ToolStripLabel1.Text = "Server:"
        '
        'ToolStripComboBox1
        '
        Me.ToolStripComboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ToolStripComboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl
        Me.ToolStripComboBox1.DropDownWidth = 250
        Me.ToolStripComboBox1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripComboBox1.Name = "ToolStripComboBox1"
        Me.ToolStripComboBox1.Size = New System.Drawing.Size(150, 29)
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(56, 26)
        Me.ToolStripLabel2.Text = "Domain:"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(120, 29)
        Me.ToolStripTextBox1.Text = "<not set>"
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(70, 26)
        Me.ToolStripLabel3.Text = "Username:"
        '
        'ToolStripTextBox2
        '
        Me.ToolStripTextBox2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox2.Name = "ToolStripTextBox2"
        Me.ToolStripTextBox2.Size = New System.Drawing.Size(120, 29)
        Me.ToolStripTextBox2.Text = "<not set>"
        '
        'ToolStripLabel4
        '
        Me.ToolStripLabel4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel4.Name = "ToolStripLabel4"
        Me.ToolStripLabel4.Size = New System.Drawing.Size(67, 26)
        Me.ToolStripLabel4.Text = "Password:"
        '
        'ToolStripTextBox3
        '
        Me.ToolStripTextBox3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox3.Name = "ToolStripTextBox3"
        Me.ToolStripTextBox3.Size = New System.Drawing.Size(120, 29)
        Me.ToolStripTextBox3.Text = "<not set>"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Enabled = False
        Me.ToolStripButton3.Image = Global.RDPClientPlugin.My.Resources.Resources.icon_external_link_22x22
        Me.ToolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton3.Text = "Fullscreen"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Enabled = False
        Me.ToolStripButton2.Image = Global.RDPClientPlugin.My.Resources.Resources.icon_x_circle_22x22
        Me.ToolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton2.Text = "Disconnect"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = Global.RDPClientPlugin.My.Resources.Resources.icon_check_circle_22x22
        Me.ToolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton1.Text = "Connect"
        '
        'RDPBox
        '
        Me.RDPBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RDPBox.Location = New System.Drawing.Point(0, 29)
        Me.RDPBox.Name = "RDPBox"
        Me.RDPBox.Size = New System.Drawing.Size(866, 466)
        Me.RDPBox.TabIndex = 5
        '
        'LoadImage
        '
        Me.LoadImage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LoadImage.Image = Global.RDPClientPlugin.My.Resources.Resources.Microsoft_1_5s_125px
        Me.LoadImage.Location = New System.Drawing.Point(0, 29)
        Me.LoadImage.Name = "LoadImage"
        Me.LoadImage.Size = New System.Drawing.Size(866, 466)
        Me.LoadImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LoadImage.TabIndex = 2
        Me.LoadImage.TabStop = False
        '
        'ConnectionClosed
        '
        Me.ConnectionClosed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ConnectionClosed.Image = Global.RDPClientPlugin.My.Resources.Resources.icon_x_circle_128x128
        Me.ConnectionClosed.Location = New System.Drawing.Point(0, 29)
        Me.ConnectionClosed.Name = "ConnectionClosed"
        Me.ConnectionClosed.Size = New System.Drawing.Size(866, 466)
        Me.ConnectionClosed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ConnectionClosed.TabIndex = 3
        Me.ConnectionClosed.TabStop = False
        Me.ConnectionClosed.Visible = False
        '
        'ClientGUI
        '
        Me.Controls.Add(Me.LoadImage)
        Me.Controls.Add(Me.ConnectionClosed)
        Me.Controls.Add(Me.RDPBox)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "ClientGUI"
        Me.Size = New System.Drawing.Size(866, 495)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.LoadImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ConnectionClosed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LoadImage As Windows.Forms.PictureBox
    Friend WithEvents RaiseAutoConnect As ComponentModel.BackgroundWorker
    Friend WithEvents ConnectionClosed As Windows.Forms.PictureBox
    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripComboBox1 As Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripButton1 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripLabel2 As Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripTextBox1 As Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripLabel3 As Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripTextBox2 As Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripLabel4 As Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripTextBox3 As Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator1 As Windows.Forms.ToolStripSeparator
    Friend WithEvents RDPBox As Windows.Forms.Panel
    Friend WithEvents ToolStripButton2 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As Windows.Forms.ToolStripButton
End Class
