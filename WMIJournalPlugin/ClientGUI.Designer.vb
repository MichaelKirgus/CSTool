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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyHostnameToClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyLastLoggedOnUserToClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ClearListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GetWMIInfo = New System.ComponentModel.BackgroundWorker()
        Me.PingCheck = New System.ComponentModel.BackgroundWorker()
        Me.ClearItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.ListView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(470, 221)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Date/Time"
        Me.ColumnHeader1.Width = 127
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Hostname/IP"
        Me.ColumnHeader2.Width = 119
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "LoggedOnUser"
        Me.ColumnHeader3.Width = 163
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyHostnameToClipboardToolStripMenuItem, Me.CopyLastLoggedOnUserToClipboardToolStripMenuItem, Me.ToolStripSeparator1, Me.ClearItemToolStripMenuItem, Me.ClearListToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(273, 98)
        '
        'CopyHostnameToClipboardToolStripMenuItem
        '
        Me.CopyHostnameToClipboardToolStripMenuItem.Name = "CopyHostnameToClipboardToolStripMenuItem"
        Me.CopyHostnameToClipboardToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.CopyHostnameToClipboardToolStripMenuItem.Text = "Copy hostname to clipboard"
        '
        'CopyLastLoggedOnUserToClipboardToolStripMenuItem
        '
        Me.CopyLastLoggedOnUserToClipboardToolStripMenuItem.Name = "CopyLastLoggedOnUserToClipboardToolStripMenuItem"
        Me.CopyLastLoggedOnUserToClipboardToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.CopyLastLoggedOnUserToClipboardToolStripMenuItem.Text = "Copy last logged on user to clipboard"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(269, 6)
        '
        'ClearListToolStripMenuItem
        '
        Me.ClearListToolStripMenuItem.Name = "ClearListToolStripMenuItem"
        Me.ClearListToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.ClearListToolStripMenuItem.Text = "Clear list"
        '
        'GetWMIInfo
        '
        Me.GetWMIInfo.WorkerReportsProgress = True
        Me.GetWMIInfo.WorkerSupportsCancellation = True
        '
        'PingCheck
        '
        Me.PingCheck.WorkerReportsProgress = True
        Me.PingCheck.WorkerSupportsCancellation = True
        '
        'ClearItemToolStripMenuItem
        '
        Me.ClearItemToolStripMenuItem.Name = "ClearItemToolStripMenuItem"
        Me.ClearItemToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.ClearItemToolStripMenuItem.Text = "Remove item"
        '
        'ClientGUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ListView1)
        Me.Name = "ClientGUI"
        Me.Size = New System.Drawing.Size(470, 221)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ListView1 As Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As Windows.Forms.ColumnHeader
    Friend WithEvents ContextMenuStrip1 As Windows.Forms.ContextMenuStrip
    Friend WithEvents CopyHostnameToClipboardToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyLastLoggedOnUserToClipboardToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ClearListToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GetWMIInfo As ComponentModel.BackgroundWorker
    Friend WithEvents PingCheck As ComponentModel.BackgroundWorker
    Friend WithEvents ClearItemToolStripMenuItem As Windows.Forms.ToolStripMenuItem
End Class
