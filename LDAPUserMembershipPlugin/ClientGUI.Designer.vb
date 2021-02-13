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
        Me.GetWMIInfoCurrentUser = New System.ComponentModel.BackgroundWorker()
        Me.GetADInfoAsync = New System.ComponentModel.BackgroundWorker()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FillGroupMembershipToGUI = New System.ComponentModel.BackgroundWorker()
        Me.ConnectionClosed = New System.Windows.Forms.PictureBox()
        Me.LoadImage = New System.Windows.Forms.PictureBox()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.ConnectionClosed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LoadImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GetWMIInfoCurrentUser
        '
        Me.GetWMIInfoCurrentUser.WorkerReportsProgress = True
        Me.GetWMIInfoCurrentUser.WorkerSupportsCancellation = True
        '
        'GetADInfoAsync
        '
        Me.GetADInfoAsync.WorkerReportsProgress = True
        Me.GetADInfoAsync.WorkerSupportsCancellation = True
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(0, 29)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(556, 288)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripTextBox1, Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip1.Size = New System.Drawing.Size(556, 29)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(150, 29)
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Group"
        Me.ColumnHeader1.Width = 523
        '
        'FillGroupMembershipToGUI
        '
        Me.FillGroupMembershipToGUI.WorkerReportsProgress = True
        Me.FillGroupMembershipToGUI.WorkerSupportsCancellation = True
        '
        'ConnectionClosed
        '
        Me.ConnectionClosed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ConnectionClosed.Image = Global.LDAPUserMembershipPlugin.My.Resources.Resources.icon_x_circle_64x64
        Me.ConnectionClosed.Location = New System.Drawing.Point(0, 29)
        Me.ConnectionClosed.Name = "ConnectionClosed"
        Me.ConnectionClosed.Size = New System.Drawing.Size(556, 288)
        Me.ConnectionClosed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ConnectionClosed.TabIndex = 4
        Me.ConnectionClosed.TabStop = False
        '
        'LoadImage
        '
        Me.LoadImage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LoadImage.Image = Global.LDAPUserMembershipPlugin.My.Resources.Resources.Microsoft_1_5s_125px
        Me.LoadImage.Location = New System.Drawing.Point(0, 29)
        Me.LoadImage.Name = "LoadImage"
        Me.LoadImage.Size = New System.Drawing.Size(556, 288)
        Me.LoadImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LoadImage.TabIndex = 3
        Me.LoadImage.TabStop = False
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = Global.LDAPUserMembershipPlugin.My.Resources.Resources.icon_search_22x22
        Me.ToolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton1.Text = "Search user"
        '
        'ClientGUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ConnectionClosed)
        Me.Controls.Add(Me.LoadImage)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "ClientGUI"
        Me.Size = New System.Drawing.Size(556, 317)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.ConnectionClosed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LoadImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GetWMIInfoCurrentUser As ComponentModel.BackgroundWorker
    Friend WithEvents GetADInfoAsync As ComponentModel.BackgroundWorker
    Friend WithEvents ListView1 As Windows.Forms.ListView
    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents ToolStripTextBox1 As Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripButton1 As Windows.Forms.ToolStripButton
    Friend WithEvents ColumnHeader1 As Windows.Forms.ColumnHeader
    Friend WithEvents FillGroupMembershipToGUI As ComponentModel.BackgroundWorker
    Friend WithEvents LoadImage As Windows.Forms.PictureBox
    Friend WithEvents ConnectionClosed As Windows.Forms.PictureBox
End Class
