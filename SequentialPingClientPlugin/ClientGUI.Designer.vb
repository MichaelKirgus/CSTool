﻿'Copyright (C) 2019-2021 Michael Kirgus
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
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.SeqPingWorker = New System.ComponentModel.BackgroundWorker()
        Me.RefreshTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyEntryToClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyIPToClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyIpv6ToClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ClearOutputToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutoScrollToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.SaveOutputToFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(0, 0)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(435, 230)
        Me.ListBox1.TabIndex = 0
        '
        'SeqPingWorker
        '
        Me.SeqPingWorker.WorkerReportsProgress = True
        Me.SeqPingWorker.WorkerSupportsCancellation = True
        '
        'RefreshTimer
        '
        Me.RefreshTimer.Interval = 500
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyEntryToClipboardToolStripMenuItem, Me.CopyIPToClipboardToolStripMenuItem, Me.CopyIpv6ToClipboardToolStripMenuItem, Me.ToolStripSeparator1, Me.RefreshToolStripMenuItem, Me.AutoScrollToolStripMenuItem, Me.ToolStripSeparator2, Me.ClearOutputToolStripMenuItem, Me.ToolStripSeparator3, Me.SaveOutputToFileToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(200, 176)
        '
        'CopyEntryToClipboardToolStripMenuItem
        '
        Me.CopyEntryToClipboardToolStripMenuItem.Name = "CopyEntryToClipboardToolStripMenuItem"
        Me.CopyEntryToClipboardToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.CopyEntryToClipboardToolStripMenuItem.Text = "Copy entry to clipboard"
        '
        'CopyIPToClipboardToolStripMenuItem
        '
        Me.CopyIPToClipboardToolStripMenuItem.Name = "CopyIPToClipboardToolStripMenuItem"
        Me.CopyIPToClipboardToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.CopyIPToClipboardToolStripMenuItem.Text = "Copy IPv4 to clipboard"
        '
        'CopyIpv6ToClipboardToolStripMenuItem
        '
        Me.CopyIpv6ToClipboardToolStripMenuItem.Name = "CopyIpv6ToClipboardToolStripMenuItem"
        Me.CopyIpv6ToClipboardToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.CopyIpv6ToClipboardToolStripMenuItem.Text = "Copy IPv6 to clipboard"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(196, 6)
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Checked = True
        Me.RefreshToolStripMenuItem.CheckOnClick = True
        Me.RefreshToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(196, 6)
        '
        'ClearOutputToolStripMenuItem
        '
        Me.ClearOutputToolStripMenuItem.Name = "ClearOutputToolStripMenuItem"
        Me.ClearOutputToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.ClearOutputToolStripMenuItem.Text = "Clear output"
        '
        'AutoScrollToolStripMenuItem
        '
        Me.AutoScrollToolStripMenuItem.Checked = True
        Me.AutoScrollToolStripMenuItem.CheckOnClick = True
        Me.AutoScrollToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AutoScrollToolStripMenuItem.Name = "AutoScrollToolStripMenuItem"
        Me.AutoScrollToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.AutoScrollToolStripMenuItem.Text = "Auto-Scroll"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(196, 6)
        '
        'SaveOutputToFileToolStripMenuItem
        '
        Me.SaveOutputToFileToolStripMenuItem.Name = "SaveOutputToFileToolStripMenuItem"
        Me.SaveOutputToFileToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.SaveOutputToFileToolStripMenuItem.Text = "Save output to file..."
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "Text file|*.txt|Log file|*.log"
        Me.SaveFileDialog1.RestoreDirectory = True
        Me.SaveFileDialog1.SupportMultiDottedExtensions = True
        '
        'ClientGUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ListBox1)
        Me.Name = "ClientGUI"
        Me.Size = New System.Drawing.Size(435, 230)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ListBox1 As Windows.Forms.ListBox
    Friend WithEvents SeqPingWorker As ComponentModel.BackgroundWorker
    Friend WithEvents RefreshTimer As Windows.Forms.Timer
    Friend WithEvents ContextMenuStrip1 As Windows.Forms.ContextMenuStrip
    Friend WithEvents CopyEntryToClipboardToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyIPToClipboardToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As Windows.Forms.ToolStripSeparator
    Friend WithEvents RefreshToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyIpv6ToClipboardToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ClearOutputToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoScrollToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As Windows.Forms.ToolStripSeparator
    Friend WithEvents SaveOutputToFileToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialog1 As Windows.Forms.SaveFileDialog
End Class
