'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ManualWQLQueryDlg
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ManualWQLQueryDlg))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.SCCMPropSplashLabel = New System.Windows.Forms.Label()
        Me.SCCMPropSplash = New System.Windows.Forms.PictureBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.PropContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExportInDateiToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveTextfileDlg = New System.Windows.Forms.SaveFileDialog()
        Me.PropertyLoader = New System.ComponentModel.BackgroundWorker()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.SCCMPropSplash, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PropContextMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.ToolStripComboBox1, Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(580, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(42, 22)
        Me.ToolStripLabel1.Text = "Query:"
        '
        'ToolStripComboBox1
        '
        Me.ToolStripComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ToolStripComboBox1.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripComboBox1.Items.AddRange(New Object() {"SELECT Name, LastLogonUserName, LastLogonTimestamp, MACAddresses, IPAddresses, Cl" &
                "ientVersion FROM SMS_R_System", "SELECT ResourceID FROM SMS_R_System", "SELECT SMS_Collection.* FROM SMS_FullCollectionMembership, SMS_Collection", "SELECT * FROM SMS_FullCollectionMembership INNER JOIN SMS_R_System ON SMS_FullCol" &
                "lectionMembership.ResourceID = SMS_R_System.ResourceID", "SELECT * FROM SMS_Collection"})
        Me.ToolStripComboBox1.Name = "ToolStripComboBox1"
        Me.ToolStripComboBox1.Size = New System.Drawing.Size(500, 25)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_search_16x16
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton1.Text = "Run"
        '
        'SCCMPropSplashLabel
        '
        Me.SCCMPropSplashLabel.AutoSize = True
        Me.SCCMPropSplashLabel.BackColor = System.Drawing.Color.White
        Me.SCCMPropSplashLabel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.SCCMPropSplashLabel.Location = New System.Drawing.Point(0, 176)
        Me.SCCMPropSplashLabel.Name = "SCCMPropSplashLabel"
        Me.SCCMPropSplashLabel.Size = New System.Drawing.Size(121, 13)
        Me.SCCMPropSplashLabel.TabIndex = 11
        Me.SCCMPropSplashLabel.Text = "No connection to server"
        '
        'SCCMPropSplash
        '
        Me.SCCMPropSplash.BackColor = System.Drawing.Color.White
        Me.SCCMPropSplash.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCCMPropSplash.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.NoStat
        Me.SCCMPropSplash.Location = New System.Drawing.Point(0, 25)
        Me.SCCMPropSplash.Name = "SCCMPropSplash"
        Me.SCCMPropSplash.Size = New System.Drawing.Size(580, 164)
        Me.SCCMPropSplash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.SCCMPropSplash.TabIndex = 10
        Me.SCCMPropSplash.TabStop = False
        '
        'ListView1
        '
        Me.ListView1.ContextMenuStrip = Me.PropContextMenu
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(0, 25)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(580, 164)
        Me.ListView1.TabIndex = 12
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
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
        'SaveTextfileDlg
        '
        Me.SaveTextfileDlg.Filter = "CSV-Datei|*.csv|Textdatei|*.txt"
        Me.SaveTextfileDlg.RestoreDirectory = True
        Me.SaveTextfileDlg.SupportMultiDottedExtensions = True
        '
        'PropertyLoader
        '
        Me.PropertyLoader.WorkerReportsProgress = True
        Me.PropertyLoader.WorkerSupportsCancellation = True
        '
        'ManualWQLQueryDlg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(580, 189)
        Me.Controls.Add(Me.SCCMPropSplashLabel)
        Me.Controls.Add(Me.SCCMPropSplash)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(596, 228)
        Me.Name = "ManualWQLQueryDlg"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Custom query"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.SCCMPropSplash, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PropContextMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents ToolStripComboBox1 As ToolStripComboBox
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents SCCMPropSplashLabel As Label
    Friend WithEvents SCCMPropSplash As PictureBox
    Friend WithEvents ListView1 As ListView
    Friend WithEvents SaveTextfileDlg As SaveFileDialog
    Friend WithEvents PropContextMenu As ContextMenuStrip
    Friend WithEvents ExportInDateiToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents PropertyLoader As System.ComponentModel.BackgroundWorker
End Class
