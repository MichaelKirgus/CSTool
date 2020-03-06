'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DockingHostWindow
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DockingHostWindow))
        Me.HostFormPluginMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EinstellungenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutoReloadSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllowCloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReciveActionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExportSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.WatchingWorker = New System.ComponentModel.BackgroundWorker()
        Me.WatchingSettingsLoadWorker = New System.ComponentModel.BackgroundWorker()
        Me.HostFormPluginMenu.SuspendLayout()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'HostFormPluginMenu
        '
        Me.HostFormPluginMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EinstellungenToolStripMenuItem, Me.SaveSettingsToolStripMenuItem, Me.AutoReloadSettingsToolStripMenuItem, Me.AllowCloseToolStripMenuItem, Me.ToolStripSeparator1, Me.RefreshToolStripMenuItem, Me.ReciveActionsToolStripMenuItem, Me.ToolStripSeparator2, Me.ExportSettingsToolStripMenuItem, Me.ImportSettingsToolStripMenuItem})
        Me.HostFormPluginMenu.Name = "HostFormPluginMenu"
        Me.HostFormPluginMenu.Size = New System.Drawing.Size(186, 192)
        '
        'EinstellungenToolStripMenuItem
        '
        Me.EinstellungenToolStripMenuItem.Image = Global.CSToolHostWindow.My.Resources.Resources.icon_edit_16x161
        Me.EinstellungenToolStripMenuItem.Name = "EinstellungenToolStripMenuItem"
        Me.EinstellungenToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.EinstellungenToolStripMenuItem.Text = "Settings"
        '
        'SaveSettingsToolStripMenuItem
        '
        Me.SaveSettingsToolStripMenuItem.Checked = True
        Me.SaveSettingsToolStripMenuItem.CheckOnClick = True
        Me.SaveSettingsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SaveSettingsToolStripMenuItem.Name = "SaveSettingsToolStripMenuItem"
        Me.SaveSettingsToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.SaveSettingsToolStripMenuItem.Text = "Save settings"
        '
        'AutoReloadSettingsToolStripMenuItem
        '
        Me.AutoReloadSettingsToolStripMenuItem.Checked = True
        Me.AutoReloadSettingsToolStripMenuItem.CheckOnClick = True
        Me.AutoReloadSettingsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AutoReloadSettingsToolStripMenuItem.Name = "AutoReloadSettingsToolStripMenuItem"
        Me.AutoReloadSettingsToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.AutoReloadSettingsToolStripMenuItem.Text = "Auto-Reload settings"
        '
        'AllowCloseToolStripMenuItem
        '
        Me.AllowCloseToolStripMenuItem.Checked = True
        Me.AllowCloseToolStripMenuItem.CheckOnClick = True
        Me.AllowCloseToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AllowCloseToolStripMenuItem.Name = "AllowCloseToolStripMenuItem"
        Me.AllowCloseToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.AllowCloseToolStripMenuItem.Text = "Allow close"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(182, 6)
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Image = Global.CSToolHostWindow.My.Resources.Resources.icon_refresh_16x161
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'ReciveActionsToolStripMenuItem
        '
        Me.ReciveActionsToolStripMenuItem.Checked = True
        Me.ReciveActionsToolStripMenuItem.CheckOnClick = True
        Me.ReciveActionsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ReciveActionsToolStripMenuItem.Image = Global.CSToolHostWindow.My.Resources.Resources.icon_notification_16x161
        Me.ReciveActionsToolStripMenuItem.Name = "ReciveActionsToolStripMenuItem"
        Me.ReciveActionsToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.ReciveActionsToolStripMenuItem.Text = "Recive actions"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(182, 6)
        '
        'ExportSettingsToolStripMenuItem
        '
        Me.ExportSettingsToolStripMenuItem.Image = Global.CSToolHostWindow.My.Resources.Resources.icon_upload_16x16
        Me.ExportSettingsToolStripMenuItem.Name = "ExportSettingsToolStripMenuItem"
        Me.ExportSettingsToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.ExportSettingsToolStripMenuItem.Text = "Export settings..."
        '
        'ImportSettingsToolStripMenuItem
        '
        Me.ImportSettingsToolStripMenuItem.Image = Global.CSToolHostWindow.My.Resources.Resources.icon_download_16x16
        Me.ImportSettingsToolStripMenuItem.Name = "ImportSettingsToolStripMenuItem"
        Me.ImportSettingsToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.ImportSettingsToolStripMenuItem.Text = "Import settings..."
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "xml"
        Me.OpenFileDialog1.Filter = "XML-Files|*.xml"
        Me.OpenFileDialog1.SupportMultiDottedExtensions = True
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "xml"
        Me.SaveFileDialog1.Filter = "XML-Files|*.xml"
        Me.SaveFileDialog1.RestoreDirectory = True
        Me.SaveFileDialog1.SupportMultiDottedExtensions = True
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.NotifyFilter = System.IO.NotifyFilters.LastWrite
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'WatchingWorker
        '
        Me.WatchingWorker.WorkerReportsProgress = True
        Me.WatchingWorker.WorkerSupportsCancellation = True
        '
        'WatchingSettingsLoadWorker
        '
        Me.WatchingSettingsLoadWorker.WorkerReportsProgress = True
        Me.WatchingSettingsLoadWorker.WorkerSupportsCancellation = True
        '
        'DockingHostWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DockingHostWindow"
        Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document
        Me.TabPageContextMenuStrip = Me.HostFormPluginMenu
        Me.HostFormPluginMenu.ResumeLayout(False)
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents HostFormPluginMenu As Windows.Forms.ContextMenuStrip
    Friend WithEvents EinstellungenToolStripMenuItem As Windows.Forms.ToolStripMenuItem

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        InstanceGUID = Guid.NewGuid.ToString
    End Sub

    Friend WithEvents ToolStripSeparator1 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ReciveActionsToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveSettingsToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents AllowCloseToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ExportSettingsToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportSettingsToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As Windows.Forms.SaveFileDialog
    Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
    Friend WithEvents AutoReloadSettingsToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents WatchingWorker As ComponentModel.BackgroundWorker
    Friend WithEvents WatchingSettingsLoadWorker As ComponentModel.BackgroundWorker
End Class
