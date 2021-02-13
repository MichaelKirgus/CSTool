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
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SaveToFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFromFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SaveChangesBeforeExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearTextToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.RichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBox1.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(394, 226)
        Me.RichTextBox1.TabIndex = 0
        Me.RichTextBox1.Text = ""
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToFileToolStripMenuItem, Me.ClearTextToolStripMenuItem, Me.OpenFromFileToolStripMenuItem, Me.ToolStripSeparator1, Me.SaveChangesBeforeExitToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(205, 98)
        '
        'SaveToFileToolStripMenuItem
        '
        Me.SaveToFileToolStripMenuItem.Image = Global.RTFNotepadPlugin.My.Resources.Resources.icon_download_16x16
        Me.SaveToFileToolStripMenuItem.Name = "SaveToFileToolStripMenuItem"
        Me.SaveToFileToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.SaveToFileToolStripMenuItem.Text = "Save to file..."
        '
        'OpenFromFileToolStripMenuItem
        '
        Me.OpenFromFileToolStripMenuItem.Image = Global.RTFNotepadPlugin.My.Resources.Resources.icon_upload_16x16
        Me.OpenFromFileToolStripMenuItem.Name = "OpenFromFileToolStripMenuItem"
        Me.OpenFromFileToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.OpenFromFileToolStripMenuItem.Text = "Open from file..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(201, 6)
        '
        'SaveChangesBeforeExitToolStripMenuItem
        '
        Me.SaveChangesBeforeExitToolStripMenuItem.CheckOnClick = True
        Me.SaveChangesBeforeExitToolStripMenuItem.Image = Global.RTFNotepadPlugin.My.Resources.Resources.icon_clock_16x16
        Me.SaveChangesBeforeExitToolStripMenuItem.Name = "SaveChangesBeforeExitToolStripMenuItem"
        Me.SaveChangesBeforeExitToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.SaveChangesBeforeExitToolStripMenuItem.Text = "Save changes before exit"
        '
        'ClearTextToolStripMenuItem
        '
        Me.ClearTextToolStripMenuItem.Image = Global.RTFNotepadPlugin.My.Resources.Resources.icon_file_blank_16x16
        Me.ClearTextToolStripMenuItem.Name = "ClearTextToolStripMenuItem"
        Me.ClearTextToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.ClearTextToolStripMenuItem.Text = "Clear text"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "RTF-Files|*.rtf"
        Me.SaveFileDialog1.RestoreDirectory = True
        Me.SaveFileDialog1.SupportMultiDottedExtensions = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "RTF-Files|*.rtf"
        Me.OpenFileDialog1.RestoreDirectory = True
        Me.OpenFileDialog1.SupportMultiDottedExtensions = True
        '
        'ClientGUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RichTextBox1)
        Me.Name = "ClientGUI"
        Me.Size = New System.Drawing.Size(394, 226)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RichTextBox1 As Windows.Forms.RichTextBox
    Friend WithEvents ContextMenuStrip1 As Windows.Forms.ContextMenuStrip
    Friend WithEvents SaveToFileToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFromFileToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As Windows.Forms.ToolStripSeparator
    Friend WithEvents SaveChangesBeforeExitToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearTextToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialog1 As Windows.Forms.SaveFileDialog
    Friend WithEvents OpenFileDialog1 As Windows.Forms.OpenFileDialog
End Class
