'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ItemPropDlg
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Me.PropContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExportInDateiToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SCCMPropSplash = New System.Windows.Forms.PictureBox()
        Me.SCCMPropSplashLabel = New System.Windows.Forms.Label()
        Me.PropertyLoader = New System.ComponentModel.BackgroundWorker()
        Me.SaveTextfileDlg = New System.Windows.Forms.SaveFileDialog()
        Me.PropContextMenu.SuspendLayout()
        CType(Me.SCCMPropSplash, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListView1.ContextMenuStrip = Me.PropContextMenu
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(729, 251)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Eigenschaft"
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Wert"
        '
        'PropContextMenu
        '
        Me.PropContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportInDateiToolStripMenuItem1})
        Me.PropContextMenu.Name = "ClientPackageStateContextMenu"
        Me.PropContextMenu.Size = New System.Drawing.Size(161, 26)
        '
        'ExportInDateiToolStripMenuItem1
        '
        Me.ExportInDateiToolStripMenuItem1.Name = "ExportInDateiToolStripMenuItem1"
        Me.ExportInDateiToolStripMenuItem1.Size = New System.Drawing.Size(160, 22)
        Me.ExportInDateiToolStripMenuItem1.Text = "Export in Datei..."
        '
        'SCCMPropSplash
        '
        Me.SCCMPropSplash.BackColor = System.Drawing.Color.White
        Me.SCCMPropSplash.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCCMPropSplash.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.NoStat
        Me.SCCMPropSplash.Location = New System.Drawing.Point(0, 0)
        Me.SCCMPropSplash.Name = "SCCMPropSplash"
        Me.SCCMPropSplash.Size = New System.Drawing.Size(729, 251)
        Me.SCCMPropSplash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.SCCMPropSplash.TabIndex = 8
        Me.SCCMPropSplash.TabStop = False
        '
        'SCCMPropSplashLabel
        '
        Me.SCCMPropSplashLabel.AutoSize = True
        Me.SCCMPropSplashLabel.BackColor = System.Drawing.Color.White
        Me.SCCMPropSplashLabel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.SCCMPropSplashLabel.Location = New System.Drawing.Point(0, 238)
        Me.SCCMPropSplashLabel.Name = "SCCMPropSplashLabel"
        Me.SCCMPropSplashLabel.Size = New System.Drawing.Size(121, 13)
        Me.SCCMPropSplashLabel.TabIndex = 9
        Me.SCCMPropSplashLabel.Text = "No connection to server"
        '
        'PropertyLoader
        '
        Me.PropertyLoader.WorkerReportsProgress = True
        Me.PropertyLoader.WorkerSupportsCancellation = True
        '
        'SaveTextfileDlg
        '
        Me.SaveTextfileDlg.Filter = "CSV-Datei|*.csv|Textdatei|*.txt"
        Me.SaveTextfileDlg.RestoreDirectory = True
        Me.SaveTextfileDlg.SupportMultiDottedExtensions = True
        '
        'ItemPropDlg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(729, 251)
        Me.Controls.Add(Me.SCCMPropSplashLabel)
        Me.Controls.Add(Me.SCCMPropSplash)
        Me.Controls.Add(Me.ListView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "ItemPropDlg"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Properties"
        Me.PropContextMenu.ResumeLayout(False)
        CType(Me.SCCMPropSplash, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ListView1 As ListView
    Friend WithEvents SCCMPropSplash As PictureBox
    Friend WithEvents SCCMPropSplashLabel As Label
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents PropertyLoader As System.ComponentModel.BackgroundWorker
    Friend WithEvents PropContextMenu As ContextMenuStrip
    Friend WithEvents ExportInDateiToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents SaveTextfileDlg As SaveFileDialog
End Class
