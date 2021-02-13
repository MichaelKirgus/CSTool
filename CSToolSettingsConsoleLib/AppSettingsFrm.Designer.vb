'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AppSettingsFrm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AppSettingsFrm))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.PropertyGrid2 = New System.Windows.Forms.PropertyGrid()
        Me.ToolStrip4 = New System.Windows.Forms.ToolStrip()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.PropertyGrid3 = New System.Windows.Forms.PropertyGrid()
        Me.ToolStrip5 = New System.Windows.Forms.ToolStrip()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.PropertyGrid4 = New System.Windows.Forms.PropertyGrid()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.PropertyGrid5 = New System.Windows.Forms.PropertyGrid()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog2 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton9 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton10 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton11 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton12 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton13 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton14 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton15 = New System.Windows.Forms.ToolStripButton()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.ToolStrip3.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.ToolStrip4.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.ToolStrip5.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(728, 511)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.PropertyGrid1)
        Me.TabPage1.Controls.Add(Me.ToolStrip3)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(720, 485)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Global application settings"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGrid1.HelpVisible = False
        Me.PropertyGrid1.Location = New System.Drawing.Point(3, 32)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(714, 450)
        Me.PropertyGrid1.TabIndex = 0
        Me.PropertyGrid1.ToolbarVisible = False
        '
        'ToolStrip3
        '
        Me.ToolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton3, Me.ToolStripSeparator2, Me.ToolStripButton9, Me.ToolStripButton10, Me.ToolStripButton11})
        Me.ToolStrip3.Location = New System.Drawing.Point(3, 3)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip3.Size = New System.Drawing.Size(714, 29)
        Me.ToolStrip3.TabIndex = 2
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.PropertyGrid2)
        Me.TabPage2.Controls.Add(Me.ToolStrip4)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(720, 485)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Main application log settings"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'PropertyGrid2
        '
        Me.PropertyGrid2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGrid2.HelpVisible = False
        Me.PropertyGrid2.Location = New System.Drawing.Point(3, 32)
        Me.PropertyGrid2.Name = "PropertyGrid2"
        Me.PropertyGrid2.Size = New System.Drawing.Size(714, 450)
        Me.PropertyGrid2.TabIndex = 1
        Me.PropertyGrid2.ToolbarVisible = False
        '
        'ToolStrip4
        '
        Me.ToolStrip4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip4.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton5, Me.ToolStripButton12})
        Me.ToolStrip4.Location = New System.Drawing.Point(3, 3)
        Me.ToolStrip4.Name = "ToolStrip4"
        Me.ToolStrip4.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip4.Size = New System.Drawing.Size(714, 29)
        Me.ToolStrip4.TabIndex = 3
        Me.ToolStrip4.Text = "ToolStrip4"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.PropertyGrid3)
        Me.TabPage3.Controls.Add(Me.ToolStrip5)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(720, 485)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Launcher application log settings"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'PropertyGrid3
        '
        Me.PropertyGrid3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGrid3.HelpVisible = False
        Me.PropertyGrid3.Location = New System.Drawing.Point(3, 32)
        Me.PropertyGrid3.Name = "PropertyGrid3"
        Me.PropertyGrid3.Size = New System.Drawing.Size(714, 450)
        Me.PropertyGrid3.TabIndex = 1
        Me.PropertyGrid3.ToolbarVisible = False
        '
        'ToolStrip5
        '
        Me.ToolStrip5.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip5.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton6, Me.ToolStripButton13})
        Me.ToolStrip5.Location = New System.Drawing.Point(3, 3)
        Me.ToolStrip5.Name = "ToolStrip5"
        Me.ToolStrip5.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip5.Size = New System.Drawing.Size(714, 29)
        Me.ToolStrip5.TabIndex = 3
        Me.ToolStrip5.Text = "ToolStrip5"
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.PropertyGrid4)
        Me.TabPage4.Controls.Add(Me.ToolStrip1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(720, 485)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Central custom actions"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'PropertyGrid4
        '
        Me.PropertyGrid4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGrid4.HelpVisible = False
        Me.PropertyGrid4.Location = New System.Drawing.Point(3, 32)
        Me.PropertyGrid4.Name = "PropertyGrid4"
        Me.PropertyGrid4.Size = New System.Drawing.Size(714, 450)
        Me.PropertyGrid4.TabIndex = 2
        Me.PropertyGrid4.ToolbarVisible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton14, Me.ToolStripSeparator3, Me.ToolStripButton1, Me.ToolStripButton8, Me.ToolStripButton2, Me.ToolStripSeparator1, Me.ToolStripTextBox1, Me.ToolStripButton4})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 3)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip1.Size = New System.Drawing.Size(714, 29)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(400, 29)
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.PropertyGrid5)
        Me.TabPage5.Controls.Add(Me.ToolStrip2)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(720, 485)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Initial workspace template"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'PropertyGrid5
        '
        Me.PropertyGrid5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGrid5.HelpVisible = False
        Me.PropertyGrid5.Location = New System.Drawing.Point(3, 32)
        Me.PropertyGrid5.Name = "PropertyGrid5"
        Me.PropertyGrid5.Size = New System.Drawing.Size(714, 450)
        Me.PropertyGrid5.TabIndex = 5
        Me.PropertyGrid5.ToolbarVisible = False
        '
        'ToolStrip2
        '
        Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton7, Me.ToolStripButton15})
        Me.ToolStrip2.Location = New System.Drawing.Point(3, 3)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip2.Size = New System.Drawing.Size(714, 29)
        Me.ToolStrip2.TabIndex = 4
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.CheckFileExists = False
        Me.OpenFileDialog1.DefaultExt = "xml"
        Me.OpenFileDialog1.Filter = "XML-File|*.xml"
        Me.OpenFileDialog1.RestoreDirectory = True
        Me.OpenFileDialog1.SupportMultiDottedExtensions = True
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 29)
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "xml"
        Me.SaveFileDialog1.Filter = "XML-File|*.xml"
        Me.SaveFileDialog1.RestoreDirectory = True
        Me.SaveFileDialog1.SupportMultiDottedExtensions = True
        '
        'OpenFileDialog2
        '
        Me.OpenFileDialog2.FileName = "OpenFileDialog2"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = Global.CSToolSettingsConsoleLib.My.Resources.Resources.icon_check_circle_22x22
        Me.ToolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton3.Text = "Save settings"
        '
        'ToolStripButton9
        '
        Me.ToolStripButton9.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton9.Image = Global.CSToolSettingsConsoleLib.My.Resources.Resources.icon_download_22x22
        Me.ToolStripButton9.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton9.Name = "ToolStripButton9"
        Me.ToolStripButton9.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton9.Text = "Import settings from file..."
        '
        'ToolStripButton10
        '
        Me.ToolStripButton10.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton10.Image = Global.CSToolSettingsConsoleLib.My.Resources.Resources.icon_upload_22x22
        Me.ToolStripButton10.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton10.Name = "ToolStripButton10"
        Me.ToolStripButton10.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton10.Text = "Save as..."
        '
        'ToolStripButton11
        '
        Me.ToolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton11.Image = Global.CSToolSettingsConsoleLib.My.Resources.Resources.icon_trash_22x22
        Me.ToolStripButton11.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton11.Name = "ToolStripButton11"
        Me.ToolStripButton11.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton11.Text = "Reset all global application settings"
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = Global.CSToolSettingsConsoleLib.My.Resources.Resources.icon_check_circle_22x22
        Me.ToolStripButton5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton5.Text = "Save settings"
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton6.Image = Global.CSToolSettingsConsoleLib.My.Resources.Resources.icon_check_circle_22x22
        Me.ToolStripButton6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton6.Text = "Save settings"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = Global.CSToolSettingsConsoleLib.My.Resources.Resources.icon_upload_22x22
        Me.ToolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton1.Text = "Load from current file"
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton8.Image = Global.CSToolSettingsConsoleLib.My.Resources.Resources.icon_file_blank_22x22
        Me.ToolStripButton8.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton8.Text = "Create new file and load"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = Global.CSToolSettingsConsoleLib.My.Resources.Resources.icon_download_22x22
        Me.ToolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton2.Text = "save current actions to current file..."
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Image = Global.CSToolSettingsConsoleLib.My.Resources.Resources.icon_more_horiz_22x22
        Me.ToolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton4.Text = "Browse for file..."
        '
        'ToolStripButton7
        '
        Me.ToolStripButton7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton7.Image = Global.CSToolSettingsConsoleLib.My.Resources.Resources.icon_check_circle_22x22
        Me.ToolStripButton7.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton7.Name = "ToolStripButton7"
        Me.ToolStripButton7.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton7.Text = "Save settings"
        '
        'ToolStripButton12
        '
        Me.ToolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton12.Image = Global.CSToolSettingsConsoleLib.My.Resources.Resources.icon_trash_22x22
        Me.ToolStripButton12.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton12.Name = "ToolStripButton12"
        Me.ToolStripButton12.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton12.Text = "Reset main application log settings"
        '
        'ToolStripButton13
        '
        Me.ToolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton13.Image = Global.CSToolSettingsConsoleLib.My.Resources.Resources.icon_trash_22x22
        Me.ToolStripButton13.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton13.Name = "ToolStripButton13"
        Me.ToolStripButton13.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton13.Text = "Reset launcher log settings"
        '
        'ToolStripButton14
        '
        Me.ToolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton14.Image = Global.CSToolSettingsConsoleLib.My.Resources.Resources.icon_trash_22x22
        Me.ToolStripButton14.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton14.Name = "ToolStripButton14"
        Me.ToolStripButton14.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton14.Text = "Reset central custom actions"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripButton15
        '
        Me.ToolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton15.Image = Global.CSToolSettingsConsoleLib.My.Resources.Resources.icon_trash_22x22
        Me.ToolStripButton15.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton15.Name = "ToolStripButton15"
        Me.ToolStripButton15.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton15.Text = "Reset initial workspace template"
        '
        'AppSettingsFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(728, 511)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "AppSettingsFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Application settings"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ToolStrip4.ResumeLayout(False)
        Me.ToolStrip4.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.ToolStrip5.ResumeLayout(False)
        Me.ToolStrip5.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As Windows.Forms.TabControl
    Friend WithEvents TabPage1 As Windows.Forms.TabPage
    Friend WithEvents PropertyGrid1 As Windows.Forms.PropertyGrid
    Friend WithEvents TabPage2 As Windows.Forms.TabPage
    Friend WithEvents PropertyGrid2 As Windows.Forms.PropertyGrid
    Friend WithEvents TabPage3 As Windows.Forms.TabPage
    Friend WithEvents PropertyGrid3 As Windows.Forms.PropertyGrid
    Friend WithEvents TabPage4 As Windows.Forms.TabPage
    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripTextBox1 As Windows.Forms.ToolStripTextBox
    Friend WithEvents PropertyGrid4 As Windows.Forms.PropertyGrid
    Friend WithEvents ToolStripButton4 As Windows.Forms.ToolStripButton
    Friend WithEvents TabPage5 As Windows.Forms.TabPage
    Friend WithEvents ToolStrip3 As Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton3 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip4 As Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton5 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip5 As Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton6 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip2 As Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton7 As Windows.Forms.ToolStripButton
    Friend WithEvents PropertyGrid5 As Windows.Forms.PropertyGrid
    Friend WithEvents OpenFileDialog1 As Windows.Forms.OpenFileDialog
    Friend WithEvents ToolStripButton8 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton9 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton10 As Windows.Forms.ToolStripButton
    Friend WithEvents SaveFileDialog1 As Windows.Forms.SaveFileDialog
    Friend WithEvents OpenFileDialog2 As Windows.Forms.OpenFileDialog
    Friend WithEvents ToolStripButton11 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton12 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton13 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton14 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton15 As Windows.Forms.ToolStripButton
End Class
