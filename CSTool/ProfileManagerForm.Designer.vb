'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProfileManagerForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProfileManagerForm))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.LoadImage1 = New System.Windows.Forms.PictureBox()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton9 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton10 = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.LoadImage2 = New System.Windows.Forms.PictureBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton11 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton12 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton13 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton14 = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.LoadImage3 = New System.Windows.Forms.PictureBox()
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.LoadImage4 = New System.Windows.Forms.PictureBox()
        Me.PropertyGrid2 = New System.Windows.Forms.PropertyGrid()
        Me.ToolStrip4 = New System.Windows.Forms.ToolStrip()
        Me.XmlEditor1 = New XmlEditor()
        Me.LoadProfilesAndWorkspaces = New System.ComponentModel.BackgroundWorker()
        Me.LoadPlugins = New System.ComponentModel.BackgroundWorker()
        Me.LoadWorkspaceSettings = New System.ComponentModel.BackgroundWorker()
        Me.ToolStrip5 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton15 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton16 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton17 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton18 = New System.Windows.Forms.ToolStripButton()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.XmlEditor2 = New XmlEditor()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton19 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton20 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton21 = New System.Windows.Forms.ToolStripButton()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.LoadImage1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.LoadImage2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.LoadImage3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip3.SuspendLayout()
        CType(Me.LoadImage4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip4.SuspendLayout()
        Me.ToolStrip5.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.LoadImage1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TreeView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ToolStrip1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer4)
        Me.SplitContainer1.Size = New System.Drawing.Size(1108, 450)
        Me.SplitContainer1.SplitterDistance = 218
        Me.SplitContainer1.TabIndex = 0
        '
        'LoadImage1
        '
        Me.LoadImage1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LoadImage1.Image = Global.CSTool.My.Resources.Resources.Microsoft_1_5s_125px
        Me.LoadImage1.Location = New System.Drawing.Point(0, 29)
        Me.LoadImage1.Name = "LoadImage1"
        Me.LoadImage1.Size = New System.Drawing.Size(218, 421)
        Me.LoadImage1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LoadImage1.TabIndex = 4
        Me.LoadImage1.TabStop = False
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.ImageIndex = 0
        Me.TreeView1.ImageList = Me.ImageList1
        Me.TreeView1.Location = New System.Drawing.Point(0, 29)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.SelectedImageIndex = 0
        Me.TreeView1.Size = New System.Drawing.Size(218, 421)
        Me.TreeView1.TabIndex = 1
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "icon-user_16x16.png")
        Me.ImageList1.Images.SetKeyName(1, "icon-browser_16x16.png")
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton5, Me.ToolStripButton6, Me.ToolStripSeparator1, Me.ToolStripButton7, Me.ToolStripButton8, Me.ToolStripSeparator2, Me.ToolStripButton9, Me.ToolStripButton10})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip1.Size = New System.Drawing.Size(218, 29)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = Global.CSTool.My.Resources.Resources.icon_refresh_22x22
        Me.ToolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton1.Text = "Refresh profiles and workspaces"
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = Global.CSTool.My.Resources.Resources.icon_user_plus_22x22
        Me.ToolStripButton5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton5.Text = "Add new profile..."
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton6.Image = Global.CSTool.My.Resources.Resources.icon_user_minus_22x22
        Me.ToolStripButton6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton6.Text = "Remove profile"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripButton7
        '
        Me.ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton7.Image = Global.CSTool.My.Resources.Resources.icon_plus_circle_22x22
        Me.ToolStripButton7.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton7.Name = "ToolStripButton7"
        Me.ToolStripButton7.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton7.Text = "Add new workspace to existing profile..."
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton8.Image = Global.CSTool.My.Resources.Resources.icon_minus_circle_22x22
        Me.ToolStripButton8.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton8.Text = "Delete workspace"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripButton9
        '
        Me.ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton9.Image = Global.CSTool.My.Resources.Resources.icon_download_22x22
        Me.ToolStripButton9.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton9.Name = "ToolStripButton9"
        Me.ToolStripButton9.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton9.Text = "Import workspace from file..."
        '
        'ToolStripButton10
        '
        Me.ToolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton10.Image = Global.CSTool.My.Resources.Resources.icon_upload_22x22
        Me.ToolStripButton10.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton10.Name = "ToolStripButton10"
        Me.ToolStripButton10.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton10.Text = "Export workspace to file..."
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.XmlEditor1)
        Me.SplitContainer4.Panel2.Controls.Add(Me.ToolStrip5)
        Me.SplitContainer4.Panel2Collapsed = True
        Me.SplitContainer4.Size = New System.Drawing.Size(886, 450)
        Me.SplitContainer4.SplitterDistance = 359
        Me.SplitContainer4.TabIndex = 2
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.LoadImage2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ListView1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ToolStrip2)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(886, 450)
        Me.SplitContainer2.SplitterDistance = 559
        Me.SplitContainer2.TabIndex = 1
        '
        'LoadImage2
        '
        Me.LoadImage2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LoadImage2.Image = Global.CSTool.My.Resources.Resources.Microsoft_1_5s_125px
        Me.LoadImage2.Location = New System.Drawing.Point(0, 29)
        Me.LoadImage2.Name = "LoadImage2"
        Me.LoadImage2.Size = New System.Drawing.Size(559, 421)
        Me.LoadImage2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LoadImage2.TabIndex = 5
        Me.LoadImage2.TabStop = False
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(0, 29)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(559, 421)
        Me.ListView1.SmallImageList = Me.ImageList1
        Me.ListView1.TabIndex = 1
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Plugin name"
        Me.ColumnHeader1.Width = 126
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Plugin GUID"
        Me.ColumnHeader2.Width = 95
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Plugin instance GUID"
        Me.ColumnHeader3.Width = 122
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Settings path"
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Last changed"
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Created"
        '
        'ToolStrip2
        '
        Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton2, Me.ToolStripButton4, Me.ToolStripSeparator3, Me.ToolStripButton11, Me.ToolStripButton12, Me.ToolStripSeparator4, Me.ToolStripButton13, Me.ToolStripSeparator5, Me.ToolStripButton14})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip2.Size = New System.Drawing.Size(559, 29)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = Global.CSTool.My.Resources.Resources.icon_refresh_22x22
        Me.ToolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton2.Text = "Refresh plugins"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Image = Global.CSTool.My.Resources.Resources.icon_view_22x22
        Me.ToolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton4.Text = "Open profile in new instance"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripButton11
        '
        Me.ToolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton11.Image = Global.CSTool.My.Resources.Resources.icon_duplicate_22x22
        Me.ToolStripButton11.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton11.Name = "ToolStripButton11"
        Me.ToolStripButton11.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton11.Text = "Close plugin settings to all plugins in workspace"
        '
        'ToolStripButton12
        '
        Me.ToolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton12.Image = Global.CSTool.My.Resources.Resources.icon_link_22x22
        Me.ToolStripButton12.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton12.Name = "ToolStripButton12"
        Me.ToolStripButton12.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton12.Text = "Clone current plugin to all plugins in all workspaces"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripButton13
        '
        Me.ToolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton13.Image = Global.CSTool.My.Resources.Resources.icon_search_22x22
        Me.ToolStripButton13.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton13.Name = "ToolStripButton13"
        Me.ToolStripButton13.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton13.Text = "Search for string in plugin settings"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripButton14
        '
        Me.ToolStripButton14.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton14.CheckOnClick = True
        Me.ToolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton14.Image = Global.CSTool.My.Resources.Resources.icon_edit_22x22
        Me.ToolStripButton14.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton14.Name = "ToolStripButton14"
        Me.ToolStripButton14.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton14.Text = "Show XML editor"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.TabControl1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.ToolStrip3)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.LoadImage4)
        Me.SplitContainer3.Panel2.Controls.Add(Me.PropertyGrid2)
        Me.SplitContainer3.Panel2.Controls.Add(Me.ToolStrip4)
        Me.SplitContainer3.Size = New System.Drawing.Size(323, 450)
        Me.SplitContainer3.SplitterDistance = 225
        Me.SplitContainer3.TabIndex = 2
        '
        'LoadImage3
        '
        Me.LoadImage3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LoadImage3.Image = Global.CSTool.My.Resources.Resources.Microsoft_1_5s_125px
        Me.LoadImage3.Location = New System.Drawing.Point(3, 3)
        Me.LoadImage3.Name = "LoadImage3"
        Me.LoadImage3.Size = New System.Drawing.Size(309, 164)
        Me.LoadImage3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LoadImage3.TabIndex = 5
        Me.LoadImage3.TabStop = False
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGrid1.HelpVisible = False
        Me.PropertyGrid1.Location = New System.Drawing.Point(3, 3)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(309, 164)
        Me.PropertyGrid1.TabIndex = 2
        Me.PropertyGrid1.ToolbarVisible = False
        '
        'ToolStrip3
        '
        Me.ToolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton3, Me.ToolStripSeparator7, Me.ToolStripButton19})
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip3.Size = New System.Drawing.Size(323, 29)
        Me.ToolStrip3.TabIndex = 1
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = Global.CSTool.My.Resources.Resources.icon_refresh_22x22
        Me.ToolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton3.Text = "Refresh workspace settings and plugin settings"
        '
        'LoadImage4
        '
        Me.LoadImage4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LoadImage4.Image = Global.CSTool.My.Resources.Resources.Microsoft_1_5s_125px
        Me.LoadImage4.Location = New System.Drawing.Point(0, 29)
        Me.LoadImage4.Name = "LoadImage4"
        Me.LoadImage4.Size = New System.Drawing.Size(323, 192)
        Me.LoadImage4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.LoadImage4.TabIndex = 5
        Me.LoadImage4.TabStop = False
        '
        'PropertyGrid2
        '
        Me.PropertyGrid2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGrid2.HelpVisible = False
        Me.PropertyGrid2.Location = New System.Drawing.Point(0, 29)
        Me.PropertyGrid2.Name = "PropertyGrid2"
        Me.PropertyGrid2.Size = New System.Drawing.Size(323, 192)
        Me.PropertyGrid2.TabIndex = 3
        Me.PropertyGrid2.ToolbarVisible = False
        '
        'ToolStrip4
        '
        Me.ToolStrip4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip4.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton21, Me.ToolStripSeparator8, Me.ToolStripButton20})
        Me.ToolStrip4.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip4.Name = "ToolStrip4"
        Me.ToolStrip4.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip4.Size = New System.Drawing.Size(323, 29)
        Me.ToolStrip4.TabIndex = 2
        Me.ToolStrip4.Text = "ToolStrip4"
        '
        'XmlEditor1
        '
        Me.XmlEditor1.AllowXmlFormatting = True
        Me.XmlEditor1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XmlEditor1.Location = New System.Drawing.Point(0, 29)
        Me.XmlEditor1.Name = "XmlEditor1"
        Me.XmlEditor1.ReadOnly = False
        Me.XmlEditor1.Size = New System.Drawing.Size(886, 58)
        Me.XmlEditor1.TabIndex = 0
        '
        'LoadProfilesAndWorkspaces
        '
        Me.LoadProfilesAndWorkspaces.WorkerReportsProgress = True
        Me.LoadProfilesAndWorkspaces.WorkerSupportsCancellation = True
        '
        'LoadPlugins
        '
        Me.LoadPlugins.WorkerReportsProgress = True
        Me.LoadPlugins.WorkerSupportsCancellation = True
        '
        'LoadWorkspaceSettings
        '
        Me.LoadWorkspaceSettings.WorkerReportsProgress = True
        Me.LoadWorkspaceSettings.WorkerSupportsCancellation = True
        '
        'ToolStrip5
        '
        Me.ToolStrip5.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip5.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton15, Me.ToolStripSeparator6, Me.ToolStripButton16, Me.ToolStripButton17, Me.ToolStripButton18})
        Me.ToolStrip5.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip5.Name = "ToolStrip5"
        Me.ToolStrip5.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip5.Size = New System.Drawing.Size(886, 29)
        Me.ToolStrip5.TabIndex = 1
        Me.ToolStrip5.Text = "ToolStrip5"
        '
        'ToolStripButton15
        '
        Me.ToolStripButton15.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton15.Image = Global.CSTool.My.Resources.Resources.icon_check_circle_22x22
        Me.ToolStripButton15.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton15.Name = "ToolStripButton15"
        Me.ToolStripButton15.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton15.Text = "Save XML to plugin settings file"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripButton16
        '
        Me.ToolStripButton16.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton16.Image = Global.CSTool.My.Resources.Resources.icon_refresh_22x22
        Me.ToolStripButton16.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton16.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton16.Name = "ToolStripButton16"
        Me.ToolStripButton16.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton16.Text = "Reload XML file"
        '
        'ToolStripButton17
        '
        Me.ToolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton17.Image = Global.CSTool.My.Resources.Resources.icon_upload_22x22
        Me.ToolStripButton17.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton17.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton17.Name = "ToolStripButton17"
        Me.ToolStripButton17.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton17.Text = "Import file content..."
        '
        'ToolStripButton18
        '
        Me.ToolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton18.Image = Global.CSTool.My.Resources.Resources.icon_download_22x22
        Me.ToolStripButton18.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton18.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton18.Name = "ToolStripButton18"
        Me.ToolStripButton18.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton18.Text = "Export file content..."
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 29)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(323, 196)
        Me.TabControl1.TabIndex = 6
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.LoadImage3)
        Me.TabPage1.Controls.Add(Me.PropertyGrid1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(315, 170)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Object properties"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.XmlEditor2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(315, 170)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "XML"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'XmlEditor2
        '
        Me.XmlEditor2.AllowXmlFormatting = True
        Me.XmlEditor2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XmlEditor2.Location = New System.Drawing.Point(3, 3)
        Me.XmlEditor2.Name = "XmlEditor2"
        Me.XmlEditor2.ReadOnly = False
        Me.XmlEditor2.Size = New System.Drawing.Size(309, 164)
        Me.XmlEditor2.TabIndex = 0
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripButton19
        '
        Me.ToolStripButton19.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton19.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton19.Image = Global.CSTool.My.Resources.Resources.icon_check_circle_22x22
        Me.ToolStripButton19.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton19.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton19.Name = "ToolStripButton19"
        Me.ToolStripButton19.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton19.Text = "Save workspace settings"
        '
        'ToolStripButton20
        '
        Me.ToolStripButton20.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton20.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton20.Image = Global.CSTool.My.Resources.Resources.icon_check_circle_22x22
        Me.ToolStripButton20.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton20.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton20.Name = "ToolStripButton20"
        Me.ToolStripButton20.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton20.Text = "Save plugin settings"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripButton21
        '
        Me.ToolStripButton21.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton21.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton21.Image = Global.CSTool.My.Resources.Resources.icon_refresh_22x22
        Me.ToolStripButton21.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton21.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton21.Name = "ToolStripButton21"
        Me.ToolStripButton21.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton21.Text = "Refresh plugin settings"
        '
        'ProfileManagerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1108, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ProfileManagerForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Profile Manager"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.LoadImage1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.Panel2.PerformLayout()
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer4.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.LoadImage2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.LoadImage3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        CType(Me.LoadImage4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip4.ResumeLayout(False)
        Me.ToolStrip4.PerformLayout()
        Me.ToolStrip5.ResumeLayout(False)
        Me.ToolStrip5.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents ToolStrip3 As ToolStrip
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents ToolStrip4 As ToolStrip
    Friend WithEvents ListView1 As ListView
    Friend WithEvents PropertyGrid1 As PropertyGrid
    Friend WithEvents PropertyGrid2 As PropertyGrid
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents ToolStripButton3 As ToolStripButton
    Friend WithEvents ToolStripButton4 As ToolStripButton
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents LoadImage1 As PictureBox
    Friend WithEvents LoadImage2 As PictureBox
    Friend WithEvents LoadImage3 As PictureBox
    Friend WithEvents LoadImage4 As PictureBox
    Friend WithEvents LoadProfilesAndWorkspaces As System.ComponentModel.BackgroundWorker
    Friend WithEvents LoadPlugins As System.ComponentModel.BackgroundWorker
    Friend WithEvents LoadWorkspaceSettings As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStripButton5 As ToolStripButton
    Friend WithEvents ToolStripButton6 As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripButton7 As ToolStripButton
    Friend WithEvents ToolStripButton8 As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripButton9 As ToolStripButton
    Friend WithEvents ToolStripButton10 As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripButton11 As ToolStripButton
    Friend WithEvents ToolStripButton12 As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripButton13 As ToolStripButton
    Friend WithEvents SplitContainer4 As SplitContainer
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents ToolStripButton14 As ToolStripButton
    Friend WithEvents XmlEditor1 As XmlEditor
    Friend WithEvents ToolStrip5 As ToolStrip
    Friend WithEvents ToolStripButton15 As ToolStripButton
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents ToolStripButton16 As ToolStripButton
    Friend WithEvents ToolStripButton17 As ToolStripButton
    Friend WithEvents ToolStripButton18 As ToolStripButton
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents XmlEditor2 As XmlEditor
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ToolStripButton19 As ToolStripButton
    Friend WithEvents ToolStripButton20 As ToolStripButton
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents ToolStripButton21 As ToolStripButton
End Class
