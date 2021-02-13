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
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.GetSQLDataAsync = New System.ComponentModel.BackgroundWorker()
        Me.LoadDataInGUI = New System.ComponentModel.BackgroundWorker()
        Me.ConnectionClosed = New System.Windows.Forms.PictureBox()
        Me.LoadImage = New System.Windows.Forms.PictureBox()
        Me.RefreshSQLDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.CustomGroupsPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.ConnectionClosed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LoadImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(711, 228)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshSQLDataToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(165, 26)
        '
        'GetSQLDataAsync
        '
        Me.GetSQLDataAsync.WorkerReportsProgress = True
        Me.GetSQLDataAsync.WorkerSupportsCancellation = True
        '
        'LoadDataInGUI
        '
        Me.LoadDataInGUI.WorkerReportsProgress = True
        Me.LoadDataInGUI.WorkerSupportsCancellation = True
        '
        'ConnectionClosed
        '
        Me.ConnectionClosed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ConnectionClosed.Image = Global.SQLClientPlugin.My.Resources.Resources.icon_exclamation_128x128
        Me.ConnectionClosed.Location = New System.Drawing.Point(0, 0)
        Me.ConnectionClosed.Name = "ConnectionClosed"
        Me.ConnectionClosed.Size = New System.Drawing.Size(711, 457)
        Me.ConnectionClosed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ConnectionClosed.TabIndex = 5
        Me.ConnectionClosed.TabStop = False
        Me.ConnectionClosed.Visible = False
        '
        'LoadImage
        '
        Me.LoadImage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LoadImage.Image = Global.SQLClientPlugin.My.Resources.Resources.Microsoft_1_5s_125px
        Me.LoadImage.Location = New System.Drawing.Point(0, 0)
        Me.LoadImage.Name = "LoadImage"
        Me.LoadImage.Size = New System.Drawing.Size(711, 457)
        Me.LoadImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LoadImage.TabIndex = 3
        Me.LoadImage.TabStop = False
        '
        'RefreshSQLDataToolStripMenuItem
        '
        Me.RefreshSQLDataToolStripMenuItem.Image = Global.SQLClientPlugin.My.Resources.Resources.icon_refresh_16x16
        Me.RefreshSQLDataToolStripMenuItem.Name = "RefreshSQLDataToolStripMenuItem"
        Me.RefreshSQLDataToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.RefreshSQLDataToolStripMenuItem.Text = "Refresh SQL Data"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ListView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.CustomGroupsPanel)
        Me.SplitContainer1.Size = New System.Drawing.Size(711, 457)
        Me.SplitContainer1.SplitterDistance = 228
        Me.SplitContainer1.TabIndex = 6
        '
        'CustomGroupsPanel
        '
        Me.CustomGroupsPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CustomGroupsPanel.Location = New System.Drawing.Point(0, 0)
        Me.CustomGroupsPanel.Name = "CustomGroupsPanel"
        Me.CustomGroupsPanel.Size = New System.Drawing.Size(711, 225)
        Me.CustomGroupsPanel.TabIndex = 0
        '
        'ClientGUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ConnectionClosed)
        Me.Controls.Add(Me.LoadImage)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "ClientGUI"
        Me.Size = New System.Drawing.Size(711, 457)
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.ConnectionClosed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LoadImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ListView1 As Windows.Forms.ListView
    Friend WithEvents GetSQLDataAsync As ComponentModel.BackgroundWorker
    Friend WithEvents LoadImage As Windows.Forms.PictureBox
    Friend WithEvents ContextMenuStrip1 As Windows.Forms.ContextMenuStrip
    Friend WithEvents RefreshSQLDataToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConnectionClosed As Windows.Forms.PictureBox
    Friend WithEvents LoadDataInGUI As ComponentModel.BackgroundWorker
    Friend WithEvents SplitContainer1 As Windows.Forms.SplitContainer
    Friend WithEvents CustomGroupsPanel As Windows.Forms.FlowLayoutPanel
End Class
