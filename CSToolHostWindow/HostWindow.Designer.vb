'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HostWindow
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HostWindow))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.HostFormPluginMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EinstellungenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllowCloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HostFormPluginMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.ContextMenuStrip = Me.HostFormPluginMenu
        Me.Button1.Image = Global.CSToolHostWindow.My.Resources.Resources.icon_menu_16x16
        Me.Button1.Location = New System.Drawing.Point(779, 429)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(22, 22)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = True
        '
        'HostFormPluginMenu
        '
        Me.HostFormPluginMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EinstellungenToolStripMenuItem, Me.SaveSettingsToolStripMenuItem, Me.AllowCloseToolStripMenuItem, Me.ToolStripSeparator1, Me.RefreshToolStripMenuItem})
        Me.HostFormPluginMenu.Name = "HostFormPluginMenu"
        Me.HostFormPluginMenu.Size = New System.Drawing.Size(181, 120)
        '
        'EinstellungenToolStripMenuItem
        '
        Me.EinstellungenToolStripMenuItem.Image = Global.CSToolHostWindow.My.Resources.Resources.icon_edit_16x16
        Me.EinstellungenToolStripMenuItem.Name = "EinstellungenToolStripMenuItem"
        Me.EinstellungenToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.EinstellungenToolStripMenuItem.Text = "Settings"
        '
        'SaveSettingsToolStripMenuItem
        '
        Me.SaveSettingsToolStripMenuItem.CheckOnClick = True
        Me.SaveSettingsToolStripMenuItem.Name = "SaveSettingsToolStripMenuItem"
        Me.SaveSettingsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SaveSettingsToolStripMenuItem.Text = "Save settings"
        '
        'AllowCloseToolStripMenuItem
        '
        Me.AllowCloseToolStripMenuItem.Checked = True
        Me.AllowCloseToolStripMenuItem.CheckOnClick = True
        Me.AllowCloseToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AllowCloseToolStripMenuItem.Name = "AllowCloseToolStripMenuItem"
        Me.AllowCloseToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.AllowCloseToolStripMenuItem.Text = "Allow close"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(177, 6)
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Image = Global.CSToolHostWindow.My.Resources.Resources.icon_refresh_16x16
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'HostWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.ContextMenuStrip = Me.HostFormPluginMenu
        Me.Controls.Add(Me.Button1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "HostWindow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HostWindow"
        Me.HostFormPluginMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents HostFormPluginMenu As Windows.Forms.ContextMenuStrip
    Friend WithEvents EinstellungenToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As Windows.Forms.ToolStripSeparator
    Friend WithEvents RefreshToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveSettingsToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents AllowCloseToolStripMenuItem As Windows.Forms.ToolStripMenuItem
End Class
