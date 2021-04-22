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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ClientGUI))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.SCCMClientsSplashLabel = New System.Windows.Forms.Label()
        Me.SCCMClientsSplash = New System.Windows.Forms.PictureBox()
        Me.ListView5 = New System.Windows.Forms.ListView()
        Me.ColumnHeader19 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader20 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader21 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader26 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader27 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ClientItemMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripTextBox3 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton9 = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.GroupMemberSplashLabel = New System.Windows.Forms.Label()
        Me.GroupMemberSplash = New System.Windows.Forms.PictureBox()
        Me.ListView4 = New System.Windows.Forms.ListView()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader17 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader18 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader23 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.RemoveMemershipContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EntfernenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ObjekteigenschaftenToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopySelectedMembershipsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteMembershipsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ClientPackageSplashLabel = New System.Windows.Forms.Label()
        Me.ClientPackageSplash = New System.Windows.Forms.PictureBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader13 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader14 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader15 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader16 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ClientPackageStateContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.NurClientstatusAktualisierenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ObjekteigenschaftenToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportInDateiToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.ServerPackageSplashLabel = New System.Windows.Forms.Label()
        Me.ServerPackageSplash = New System.Windows.Forms.PictureBox()
        Me.ListView6 = New System.Windows.Forms.ListView()
        Me.ColumnHeader28 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader32 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader33 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader35 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ServerPackageStateContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripTextBox2 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.LiveModeButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton13 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton15 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton16 = New System.Windows.Forms.ToolStripButton()
        Me.SMSCollectionSplashLabel = New System.Windows.Forms.Label()
        Me.SMSCollectionSplash = New System.Windows.Forms.PictureBox()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader22 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.AddMemberContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ZuweisenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AlleGeräteMitDieserCollectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopySelectedMembershipsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ObjekteigenschaftenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.SortAscendingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SortDescendingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExportInDateiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton10 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton14 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton12 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton11 = New System.Windows.Forms.ToolStripButton()
        Me.ListView3 = New System.Windows.Forms.ListView()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader24 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader25 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LogContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.LogSpeichernToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CollectServerPackageStateWorker = New System.ComponentModel.BackgroundWorker()
        Me.SaveTextfileDlg = New System.Windows.Forms.SaveFileDialog()
        Me.CollectClientSearchQueryWorker = New System.ComponentModel.BackgroundWorker()
        Me.DeleteFromCollectionWorker = New System.ComponentModel.BackgroundWorker()
        Me.AddToCollectionWorker = New System.ComponentModel.BackgroundWorker()
        Me.CollectClientPackageStateWorker = New System.ComponentModel.BackgroundWorker()
        Me.CollectClientCollectionMembershipsWorker = New System.ComponentModel.BackgroundWorker()
        Me.CollectAllClientsFromCollection = New System.ComponentModel.BackgroundWorker()
        Me.CollectSMSCollectionsWorker = New System.ComponentModel.BackgroundWorker()
        Me.GetAllClientsWorker = New System.ComponentModel.BackgroundWorker()
        Me.ConnectToSMSMServerWorker = New System.ComponentModel.BackgroundWorker()
        Me.CloneCollectionFromDeviceWorker = New System.ComponentModel.BackgroundWorker()
        Me.PasteCollectionsWorker = New System.ComponentModel.BackgroundWorker()
        Me.RefreshCollectionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.SCCMClientsSplash, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ClientItemMenu.SuspendLayout()
        Me.ToolStrip3.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.GroupMemberSplash, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RemoveMemershipContextMenu.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.ClientPackageSplash, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ClientPackageStateContextMenu.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.ServerPackageSplash, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ServerPackageStateContextMenu.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.SMSCollectionSplash, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AddMemberContextMenu.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.LogContextMenu.SuspendLayout()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ListView3)
        Me.SplitContainer1.Panel2Collapsed = True
        Me.SplitContainer1.Size = New System.Drawing.Size(1222, 759)
        Me.SplitContainer1.SplitterDistance = 635
        Me.SplitContainer1.SplitterWidth = 2
        Me.SplitContainer1.TabIndex = 3
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer4)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SMSCollectionSplashLabel)
        Me.SplitContainer2.Panel2.Controls.Add(Me.SMSCollectionSplash)
        Me.SplitContainer2.Panel2.Controls.Add(Me.ListView2)
        Me.SplitContainer2.Panel2.Controls.Add(Me.ToolStrip1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1222, 759)
        Me.SplitContainer2.SplitterDistance = 777
        Me.SplitContainer2.SplitterWidth = 2
        Me.SplitContainer2.TabIndex = 0
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
        Me.SplitContainer4.Panel1.Controls.Add(Me.SCCMClientsSplashLabel)
        Me.SplitContainer4.Panel1.Controls.Add(Me.SCCMClientsSplash)
        Me.SplitContainer4.Panel1.Controls.Add(Me.ListView5)
        Me.SplitContainer4.Panel1.Controls.Add(Me.ToolStrip3)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer4.Panel2.Controls.Add(Me.ToolStrip2)
        Me.SplitContainer4.Size = New System.Drawing.Size(777, 759)
        Me.SplitContainer4.SplitterDistance = 194
        Me.SplitContainer4.SplitterWidth = 2
        Me.SplitContainer4.TabIndex = 4
        '
        'SCCMClientsSplashLabel
        '
        Me.SCCMClientsSplashLabel.AutoSize = True
        Me.SCCMClientsSplashLabel.BackColor = System.Drawing.Color.White
        Me.SCCMClientsSplashLabel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.SCCMClientsSplashLabel.Location = New System.Drawing.Point(0, 181)
        Me.SCCMClientsSplashLabel.Name = "SCCMClientsSplashLabel"
        Me.SCCMClientsSplashLabel.Size = New System.Drawing.Size(121, 13)
        Me.SCCMClientsSplashLabel.TabIndex = 8
        Me.SCCMClientsSplashLabel.Text = "No connection to server"
        '
        'SCCMClientsSplash
        '
        Me.SCCMClientsSplash.BackColor = System.Drawing.Color.White
        Me.SCCMClientsSplash.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCCMClientsSplash.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.adminstatus
        Me.SCCMClientsSplash.Location = New System.Drawing.Point(0, 25)
        Me.SCCMClientsSplash.Name = "SCCMClientsSplash"
        Me.SCCMClientsSplash.Size = New System.Drawing.Size(777, 169)
        Me.SCCMClientsSplash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.SCCMClientsSplash.TabIndex = 7
        Me.SCCMClientsSplash.TabStop = False
        '
        'ListView5
        '
        Me.ListView5.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader19, Me.ColumnHeader20, Me.ColumnHeader21, Me.ColumnHeader5, Me.ColumnHeader26, Me.ColumnHeader27})
        Me.ListView5.ContextMenuStrip = Me.ClientItemMenu
        Me.ListView5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView5.FullRowSelect = True
        Me.ListView5.HideSelection = False
        Me.ListView5.Location = New System.Drawing.Point(0, 25)
        Me.ListView5.Name = "ListView5"
        Me.ListView5.Size = New System.Drawing.Size(777, 169)
        Me.ListView5.SmallImageList = Me.ImageList1
        Me.ListView5.TabIndex = 1
        Me.ListView5.UseCompatibleStateImageBehavior = False
        Me.ListView5.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader19
        '
        Me.ColumnHeader19.Text = "Name"
        Me.ColumnHeader19.Width = 156
        '
        'ColumnHeader20
        '
        Me.ColumnHeader20.Text = "Last logged on user"
        Me.ColumnHeader20.Width = 161
        '
        'ColumnHeader21
        '
        Me.ColumnHeader21.Text = "Last logon date"
        Me.ColumnHeader21.Width = 80
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "MAC"
        '
        'ColumnHeader26
        '
        Me.ColumnHeader26.Text = "IP"
        '
        'ColumnHeader27
        '
        Me.ColumnHeader27.Text = "Client-Version"
        '
        'ClientItemMenu
        '
        Me.ClientItemMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem4})
        Me.ClientItemMenu.Name = "ClientItemMenu"
        Me.ClientItemMenu.Size = New System.Drawing.Size(156, 26)
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_announcement_16x16
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(163, 30)
        Me.ToolStripMenuItem4.Text = "Wake-Up client"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "user-invisible.png")
        Me.ImageList1.Images.SetKeyName(1, "user-invisible-2.png")
        Me.ImageList1.Images.SetKeyName(2, "user-offline.png")
        Me.ImageList1.Images.SetKeyName(3, "user-offline-2.png")
        Me.ImageList1.Images.SetKeyName(4, "user-online.png")
        Me.ImageList1.Images.SetKeyName(5, "user-online-2.png")
        Me.ImageList1.Images.SetKeyName(6, "user-available_yellow.ico")
        Me.ImageList1.Images.SetKeyName(7, "computer-5.png")
        Me.ImageList1.Images.SetKeyName(8, "package-available.png")
        Me.ImageList1.Images.SetKeyName(9, "package-broken.png")
        Me.ImageList1.Images.SetKeyName(10, "package-installed-updated.png")
        Me.ImageList1.Images.SetKeyName(11, "package-green.png")
        Me.ImageList1.Images.SetKeyName(12, "server-chart.png")
        Me.ImageList1.Images.SetKeyName(13, "software-update-available.png")
        Me.ImageList1.Images.SetKeyName(14, "software-update-available-2.png")
        '
        'ToolStrip3
        '
        Me.ToolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel3, Me.ToolStripTextBox3, Me.ToolStripButton8, Me.ToolStripSeparator4, Me.ToolStripButton7, Me.ToolStripButton4, Me.ToolStripButton9})
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Size = New System.Drawing.Size(777, 25)
        Me.ToolStrip3.TabIndex = 0
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(139, 22)
        Me.ToolStripLabel3.Text = "Search device/username:"
        '
        'ToolStripTextBox3
        '
        Me.ToolStripTextBox3.Enabled = False
        Me.ToolStripTextBox3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStripTextBox3.Name = "ToolStripTextBox3"
        Me.ToolStripTextBox3.Size = New System.Drawing.Size(150, 25)
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.Enabled = False
        Me.ToolStripButton8.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_search_16x16
        Me.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.Size = New System.Drawing.Size(62, 22)
        Me.ToolStripButton8.Text = "&Search"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton7
        '
        Me.ToolStripButton7.Enabled = False
        Me.ToolStripButton7.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_refresh_16x16
        Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton7.Name = "ToolStripButton7"
        Me.ToolStripButton7.Size = New System.Drawing.Size(120, 22)
        Me.ToolStripButton7.Text = "&Reload all devices"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton4.CheckOnClick = True
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_clock_16x16
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton4.Text = "Show log (on/off)"
        '
        'ToolStripButton9
        '
        Me.ToolStripButton9.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton9.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_link_16x16
        Me.ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton9.Name = "ToolStripButton9"
        Me.ToolStripButton9.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton9.Text = "Custom WQL-Query..."
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.GroupMemberSplashLabel)
        Me.SplitContainer3.Panel1.Controls.Add(Me.GroupMemberSplash)
        Me.SplitContainer3.Panel1.Controls.Add(Me.ListView4)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.TabControl1)
        Me.SplitContainer3.Size = New System.Drawing.Size(777, 538)
        Me.SplitContainer3.SplitterDistance = 256
        Me.SplitContainer3.SplitterWidth = 2
        Me.SplitContainer3.TabIndex = 3
        '
        'GroupMemberSplashLabel
        '
        Me.GroupMemberSplashLabel.AutoSize = True
        Me.GroupMemberSplashLabel.BackColor = System.Drawing.Color.White
        Me.GroupMemberSplashLabel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupMemberSplashLabel.Location = New System.Drawing.Point(0, 243)
        Me.GroupMemberSplashLabel.Name = "GroupMemberSplashLabel"
        Me.GroupMemberSplashLabel.Size = New System.Drawing.Size(121, 13)
        Me.GroupMemberSplashLabel.TabIndex = 6
        Me.GroupMemberSplashLabel.Text = "No connection to server"
        '
        'GroupMemberSplash
        '
        Me.GroupMemberSplash.BackColor = System.Drawing.Color.White
        Me.GroupMemberSplash.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupMemberSplash.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.adminstatus
        Me.GroupMemberSplash.Location = New System.Drawing.Point(0, 0)
        Me.GroupMemberSplash.Name = "GroupMemberSplash"
        Me.GroupMemberSplash.Size = New System.Drawing.Size(777, 256)
        Me.GroupMemberSplash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.GroupMemberSplash.TabIndex = 5
        Me.GroupMemberSplash.TabStop = False
        '
        'ListView4
        '
        Me.ListView4.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2, Me.ColumnHeader9, Me.ColumnHeader17, Me.ColumnHeader18, Me.ColumnHeader23})
        Me.ListView4.ContextMenuStrip = Me.RemoveMemershipContextMenu
        Me.ListView4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView4.FullRowSelect = True
        Me.ListView4.HideSelection = False
        Me.ListView4.Location = New System.Drawing.Point(0, 0)
        Me.ListView4.Name = "ListView4"
        Me.ListView4.Size = New System.Drawing.Size(777, 256)
        Me.ListView4.SmallImageList = Me.ImageList1
        Me.ListView4.TabIndex = 4
        Me.ListView4.UseCompatibleStateImageBehavior = False
        Me.ListView4.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Collection"
        Me.ColumnHeader2.Width = 370
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Members"
        '
        'ColumnHeader17
        '
        Me.ColumnHeader17.Text = "ID"
        '
        'ColumnHeader18
        '
        Me.ColumnHeader18.Text = "Comment"
        '
        'ColumnHeader23
        '
        Me.ColumnHeader23.Text = "Type"
        '
        'RemoveMemershipContextMenu
        '
        Me.RemoveMemershipContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EntfernenToolStripMenuItem, Me.ObjekteigenschaftenToolStripMenuItem1, Me.ToolStripSeparator11, Me.CopySelectedMembershipsToolStripMenuItem, Me.PasteMembershipsToolStripMenuItem, Me.ToolStripSeparator6, Me.ExportToolStripMenuItem})
        Me.RemoveMemershipContextMenu.Name = "ContextMenuStrip2"
        Me.RemoveMemershipContextMenu.Size = New System.Drawing.Size(244, 126)
        '
        'EntfernenToolStripMenuItem
        '
        Me.EntfernenToolStripMenuItem.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_minus_square_16x16
        Me.EntfernenToolStripMenuItem.Name = "EntfernenToolStripMenuItem"
        Me.EntfernenToolStripMenuItem.Size = New System.Drawing.Size(251, 30)
        Me.EntfernenToolStripMenuItem.Text = "Delete membership from device"
        '
        'ObjekteigenschaftenToolStripMenuItem1
        '
        Me.ObjekteigenschaftenToolStripMenuItem1.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_information_16x16
        Me.ObjekteigenschaftenToolStripMenuItem1.Name = "ObjekteigenschaftenToolStripMenuItem1"
        Me.ObjekteigenschaftenToolStripMenuItem1.Size = New System.Drawing.Size(251, 30)
        Me.ObjekteigenschaftenToolStripMenuItem1.Text = "Item properties..."
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(248, 6)
        '
        'CopySelectedMembershipsToolStripMenuItem
        '
        Me.CopySelectedMembershipsToolStripMenuItem.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_duplicate_16x16
        Me.CopySelectedMembershipsToolStripMenuItem.Name = "CopySelectedMembershipsToolStripMenuItem"
        Me.CopySelectedMembershipsToolStripMenuItem.Size = New System.Drawing.Size(251, 30)
        Me.CopySelectedMembershipsToolStripMenuItem.Text = "Copy selected memberships"
        '
        'PasteMembershipsToolStripMenuItem
        '
        Me.PasteMembershipsToolStripMenuItem.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_clipboard_16x16
        Me.PasteMembershipsToolStripMenuItem.Name = "PasteMembershipsToolStripMenuItem"
        Me.PasteMembershipsToolStripMenuItem.Size = New System.Drawing.Size(251, 30)
        Me.PasteMembershipsToolStripMenuItem.Text = "Paste memberships"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(248, 6)
        '
        'ExportToolStripMenuItem
        '
        Me.ExportToolStripMenuItem.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_news_16x16
        Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        Me.ExportToolStripMenuItem.Size = New System.Drawing.Size(251, 30)
        Me.ExportToolStripMenuItem.Text = "Export to file..."
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(777, 280)
        Me.TabControl1.TabIndex = 8
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ClientPackageSplashLabel)
        Me.TabPage1.Controls.Add(Me.ClientPackageSplash)
        Me.TabPage1.Controls.Add(Me.ListView1)
        Me.TabPage1.ImageIndex = 7
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(769, 254)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Collections from device"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ClientPackageSplashLabel
        '
        Me.ClientPackageSplashLabel.AutoSize = True
        Me.ClientPackageSplashLabel.BackColor = System.Drawing.Color.White
        Me.ClientPackageSplashLabel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ClientPackageSplashLabel.Location = New System.Drawing.Point(0, 241)
        Me.ClientPackageSplashLabel.Name = "ClientPackageSplashLabel"
        Me.ClientPackageSplashLabel.Size = New System.Drawing.Size(124, 13)
        Me.ClientPackageSplashLabel.TabIndex = 8
        Me.ClientPackageSplashLabel.Text = "No connection to device"
        '
        'ClientPackageSplash
        '
        Me.ClientPackageSplash.BackColor = System.Drawing.Color.White
        Me.ClientPackageSplash.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ClientPackageSplash.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.adminstatus
        Me.ClientPackageSplash.Location = New System.Drawing.Point(0, 0)
        Me.ClientPackageSplash.Name = "ClientPackageSplash"
        Me.ClientPackageSplash.Size = New System.Drawing.Size(769, 254)
        Me.ClientPackageSplash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ClientPackageSplash.TabIndex = 6
        Me.ClientPackageSplash.TabStop = False
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader10, Me.ColumnHeader11, Me.ColumnHeader12, Me.ColumnHeader13, Me.ColumnHeader14, Me.ColumnHeader15, Me.ColumnHeader16})
        Me.ListView1.ContextMenuStrip = Me.ClientPackageStateContextMenu
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(769, 254)
        Me.ListView1.SmallImageList = Me.ImageList1
        Me.ListView1.TabIndex = 2
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Full name"
        Me.ColumnHeader1.Width = 211
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Manufacturer"
        Me.ColumnHeader10.Width = 106
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "Version"
        Me.ColumnHeader11.Width = 86
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "Revision"
        Me.ColumnHeader12.Width = 92
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "Policy state"
        '
        'ColumnHeader14
        '
        Me.ColumnHeader14.Text = "Current state"
        '
        'ColumnHeader15
        '
        Me.ColumnHeader15.Text = "Last checked"
        Me.ColumnHeader15.Width = 111
        '
        'ColumnHeader16
        '
        Me.ColumnHeader16.Text = "Last installed"
        Me.ColumnHeader16.Width = 105
        '
        'ClientPackageStateContextMenu
        '
        Me.ClientPackageStateContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NurClientstatusAktualisierenToolStripMenuItem, Me.ToolStripSeparator7, Me.ObjekteigenschaftenToolStripMenuItem2, Me.ExportInDateiToolStripMenuItem1})
        Me.ClientPackageStateContextMenu.Name = "ClientPackageStateContextMenu"
        Me.ClientPackageStateContextMenu.Size = New System.Drawing.Size(211, 76)
        '
        'NurClientstatusAktualisierenToolStripMenuItem
        '
        Me.NurClientstatusAktualisierenToolStripMenuItem.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_refresh_16x16
        Me.NurClientstatusAktualisierenToolStripMenuItem.Name = "NurClientstatusAktualisierenToolStripMenuItem"
        Me.NurClientstatusAktualisierenToolStripMenuItem.Size = New System.Drawing.Size(218, 30)
        Me.NurClientstatusAktualisierenToolStripMenuItem.Text = "Only refresh device data..."
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(215, 6)
        '
        'ObjekteigenschaftenToolStripMenuItem2
        '
        Me.ObjekteigenschaftenToolStripMenuItem2.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_information_16x16
        Me.ObjekteigenschaftenToolStripMenuItem2.Name = "ObjekteigenschaftenToolStripMenuItem2"
        Me.ObjekteigenschaftenToolStripMenuItem2.Size = New System.Drawing.Size(218, 30)
        Me.ObjekteigenschaftenToolStripMenuItem2.Text = "Item properties..."
        '
        'ExportInDateiToolStripMenuItem1
        '
        Me.ExportInDateiToolStripMenuItem1.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_news_16x16
        Me.ExportInDateiToolStripMenuItem1.Name = "ExportInDateiToolStripMenuItem1"
        Me.ExportInDateiToolStripMenuItem1.Size = New System.Drawing.Size(218, 30)
        Me.ExportInDateiToolStripMenuItem1.Text = "Export to file..."
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ServerPackageSplashLabel)
        Me.TabPage2.Controls.Add(Me.ServerPackageSplash)
        Me.TabPage2.Controls.Add(Me.ListView6)
        Me.TabPage2.ImageIndex = 12
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(769, 254)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Collections from server"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'ServerPackageSplashLabel
        '
        Me.ServerPackageSplashLabel.AutoSize = True
        Me.ServerPackageSplashLabel.BackColor = System.Drawing.Color.White
        Me.ServerPackageSplashLabel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ServerPackageSplashLabel.Location = New System.Drawing.Point(0, 241)
        Me.ServerPackageSplashLabel.Name = "ServerPackageSplashLabel"
        Me.ServerPackageSplashLabel.Size = New System.Drawing.Size(121, 13)
        Me.ServerPackageSplashLabel.TabIndex = 8
        Me.ServerPackageSplashLabel.Text = "No connection to server"
        '
        'ServerPackageSplash
        '
        Me.ServerPackageSplash.BackColor = System.Drawing.Color.White
        Me.ServerPackageSplash.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ServerPackageSplash.Location = New System.Drawing.Point(0, 0)
        Me.ServerPackageSplash.Name = "ServerPackageSplash"
        Me.ServerPackageSplash.Size = New System.Drawing.Size(769, 254)
        Me.ServerPackageSplash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ServerPackageSplash.TabIndex = 7
        Me.ServerPackageSplash.TabStop = False
        '
        'ListView6
        '
        Me.ListView6.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader28, Me.ColumnHeader32, Me.ColumnHeader33, Me.ColumnHeader35})
        Me.ListView6.ContextMenuStrip = Me.ServerPackageStateContextMenu
        Me.ListView6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView6.FullRowSelect = True
        Me.ListView6.HideSelection = False
        Me.ListView6.Location = New System.Drawing.Point(0, 0)
        Me.ListView6.Name = "ListView6"
        Me.ListView6.Size = New System.Drawing.Size(769, 254)
        Me.ListView6.SmallImageList = Me.ImageList1
        Me.ListView6.TabIndex = 3
        Me.ListView6.UseCompatibleStateImageBehavior = False
        Me.ListView6.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader28
        '
        Me.ColumnHeader28.Text = "Full name"
        Me.ColumnHeader28.Width = 211
        '
        'ColumnHeader32
        '
        Me.ColumnHeader32.Text = "Policy state"
        Me.ColumnHeader32.Width = 91
        '
        'ColumnHeader33
        '
        Me.ColumnHeader33.Text = "Current state"
        Me.ColumnHeader33.Width = 96
        '
        'ColumnHeader35
        '
        Me.ColumnHeader35.Text = "Last installed"
        Me.ColumnHeader35.Width = 105
        '
        'ServerPackageStateContextMenu
        '
        Me.ServerPackageStateContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripSeparator8, Me.ToolStripMenuItem2, Me.ToolStripMenuItem3})
        Me.ServerPackageStateContextMenu.Name = "ClientPackageStateContextMenu"
        Me.ServerPackageStateContextMenu.Size = New System.Drawing.Size(199, 76)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_refresh_16x16
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(206, 30)
        Me.ToolStripMenuItem1.Text = "Only refresh server data"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(203, 6)
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_information_16x16
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(206, 30)
        Me.ToolStripMenuItem2.Text = "Item properties..."
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_news_16x16
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(206, 30)
        Me.ToolStripMenuItem3.Text = "Export to file..."
        '
        'ToolStrip2
        '
        Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel2, Me.ToolStripTextBox2, Me.ToolStripSeparator2, Me.ToolStripButton1, Me.ToolStripButton6, Me.ToolStripSeparator3, Me.ToolStripButton3, Me.LiveModeButton, Me.ToolStripButton5, Me.ToolStripButton13, Me.ToolStripButton15, Me.ToolStripButton16})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(777, 25)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(45, 22)
        Me.ToolStripLabel2.Text = "Device:"
        '
        'ToolStripTextBox2
        '
        Me.ToolStripTextBox2.Enabled = False
        Me.ToolStripTextBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStripTextBox2.Name = "ToolStripTextBox2"
        Me.ToolStripTextBox2.Size = New System.Drawing.Size(100, 25)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Enabled = False
        Me.ToolStripButton1.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_check_circle_16x16
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(95, 22)
        Me.ToolStripButton1.Text = "&Connect (F5)"
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.Enabled = False
        Me.ToolStripButton6.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_view_16x16
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(161, 22)
        Me.ToolStripButton6.Text = "Only load &collections (F6)"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Enabled = False
        Me.ToolStripButton3.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_announcement_16x16
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(128, 22)
        Me.ToolStripButton3.Text = "&Apply changes (F7)"
        '
        'LiveModeButton
        '
        Me.LiveModeButton.Checked = True
        Me.LiveModeButton.CheckOnClick = True
        Me.LiveModeButton.CheckState = System.Windows.Forms.CheckState.Checked
        Me.LiveModeButton.Enabled = False
        Me.LiveModeButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.LiveModeButton.Name = "LiveModeButton"
        Me.LiveModeButton.Size = New System.Drawing.Size(68, 22)
        Me.LiveModeButton.Text = "&Live-Mode"
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.Checked = True
        Me.ToolStripButton5.CheckOnClick = True
        Me.ToolStripButton5.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_filter_16x16
        Me.ToolStripButton5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton5.Text = "Filter collections"
        '
        'ToolStripButton13
        '
        Me.ToolStripButton13.Checked = True
        Me.ToolStripButton13.CheckOnClick = True
        Me.ToolStripButton13.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton13.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_book_16x16
        Me.ToolStripButton13.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton13.Name = "ToolStripButton13"
        Me.ToolStripButton13.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton13.Text = "Group collections"
        '
        'ToolStripButton15
        '
        Me.ToolStripButton15.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton15.Checked = True
        Me.ToolStripButton15.CheckOnClick = True
        Me.ToolStripButton15.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton15.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_view_16x16
        Me.ToolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton15.Name = "ToolStripButton15"
        Me.ToolStripButton15.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton15.Text = "Show search device/username panel"
        '
        'ToolStripButton16
        '
        Me.ToolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton16.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_duplicate_16x16
        Me.ToolStripButton16.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton16.Name = "ToolStripButton16"
        Me.ToolStripButton16.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton16.Text = "Duplicate collections from device (up) to device (down)"
        '
        'SMSCollectionSplashLabel
        '
        Me.SMSCollectionSplashLabel.AutoSize = True
        Me.SMSCollectionSplashLabel.BackColor = System.Drawing.Color.White
        Me.SMSCollectionSplashLabel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.SMSCollectionSplashLabel.Location = New System.Drawing.Point(0, 746)
        Me.SMSCollectionSplashLabel.Name = "SMSCollectionSplashLabel"
        Me.SMSCollectionSplashLabel.Size = New System.Drawing.Size(121, 13)
        Me.SMSCollectionSplashLabel.TabIndex = 7
        Me.SMSCollectionSplashLabel.Text = "No connection to server"
        '
        'SMSCollectionSplash
        '
        Me.SMSCollectionSplash.BackColor = System.Drawing.Color.White
        Me.SMSCollectionSplash.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SMSCollectionSplash.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.adminstatus
        Me.SMSCollectionSplash.Location = New System.Drawing.Point(0, 25)
        Me.SMSCollectionSplash.Name = "SMSCollectionSplash"
        Me.SMSCollectionSplash.Size = New System.Drawing.Size(443, 734)
        Me.SMSCollectionSplash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.SMSCollectionSplash.TabIndex = 6
        Me.SMSCollectionSplash.TabStop = False
        '
        'ListView2
        '
        Me.ListView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader22})
        Me.ListView2.ContextMenuStrip = Me.AddMemberContextMenu
        Me.ListView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView2.FullRowSelect = True
        Me.ListView2.HideSelection = False
        Me.ListView2.Location = New System.Drawing.Point(0, 25)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(443, 734)
        Me.ListView2.SmallImageList = Me.ImageList1
        Me.ListView2.TabIndex = 3
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Collection"
        Me.ColumnHeader3.Width = 370
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Members"
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "ID"
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Comment"
        '
        'ColumnHeader22
        '
        Me.ColumnHeader22.Text = "Type"
        '
        'AddMemberContextMenu
        '
        Me.AddMemberContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ZuweisenToolStripMenuItem, Me.AlleGeräteMitDieserCollectionToolStripMenuItem, Me.CopySelectedMembershipsToolStripMenuItem1, Me.ObjekteigenschaftenToolStripMenuItem, Me.ToolStripSeparator10, Me.SortAscendingToolStripMenuItem, Me.SortDescendingToolStripMenuItem, Me.RefreshCollectionsToolStripMenuItem, Me.ToolStripSeparator5, Me.ExportInDateiToolStripMenuItem})
        Me.AddMemberContextMenu.Name = "ContextMenuStrip1"
        Me.AddMemberContextMenu.Size = New System.Drawing.Size(273, 192)
        '
        'ZuweisenToolStripMenuItem
        '
        Me.ZuweisenToolStripMenuItem.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_plus_square_16x16
        Me.ZuweisenToolStripMenuItem.Name = "ZuweisenToolStripMenuItem"
        Me.ZuweisenToolStripMenuItem.Size = New System.Drawing.Size(280, 30)
        Me.ZuweisenToolStripMenuItem.Text = "Assign collection to device"
        '
        'AlleGeräteMitDieserCollectionToolStripMenuItem
        '
        Me.AlleGeräteMitDieserCollectionToolStripMenuItem.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_search_16x16
        Me.AlleGeräteMitDieserCollectionToolStripMenuItem.Name = "AlleGeräteMitDieserCollectionToolStripMenuItem"
        Me.AlleGeräteMitDieserCollectionToolStripMenuItem.Size = New System.Drawing.Size(280, 30)
        Me.AlleGeräteMitDieserCollectionToolStripMenuItem.Text = "Show all devices with this collection..."
        '
        'CopySelectedMembershipsToolStripMenuItem1
        '
        Me.CopySelectedMembershipsToolStripMenuItem1.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_duplicate_16x16
        Me.CopySelectedMembershipsToolStripMenuItem1.Name = "CopySelectedMembershipsToolStripMenuItem1"
        Me.CopySelectedMembershipsToolStripMenuItem1.Size = New System.Drawing.Size(280, 30)
        Me.CopySelectedMembershipsToolStripMenuItem1.Text = "Copy selected memberships"
        '
        'ObjekteigenschaftenToolStripMenuItem
        '
        Me.ObjekteigenschaftenToolStripMenuItem.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_information_16x16
        Me.ObjekteigenschaftenToolStripMenuItem.Name = "ObjekteigenschaftenToolStripMenuItem"
        Me.ObjekteigenschaftenToolStripMenuItem.Size = New System.Drawing.Size(280, 30)
        Me.ObjekteigenschaftenToolStripMenuItem.Text = "Item properties..."
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(277, 6)
        '
        'SortAscendingToolStripMenuItem
        '
        Me.SortAscendingToolStripMenuItem.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_cheveron_up_16x16
        Me.SortAscendingToolStripMenuItem.Name = "SortAscendingToolStripMenuItem"
        Me.SortAscendingToolStripMenuItem.Size = New System.Drawing.Size(280, 30)
        Me.SortAscendingToolStripMenuItem.Text = "Sort ascending"
        '
        'SortDescendingToolStripMenuItem
        '
        Me.SortDescendingToolStripMenuItem.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_cheveron_down_16x16
        Me.SortDescendingToolStripMenuItem.Name = "SortDescendingToolStripMenuItem"
        Me.SortDescendingToolStripMenuItem.Size = New System.Drawing.Size(280, 30)
        Me.SortDescendingToolStripMenuItem.Text = "Sort descending"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(277, 6)
        '
        'ExportInDateiToolStripMenuItem
        '
        Me.ExportInDateiToolStripMenuItem.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_news_16x16
        Me.ExportInDateiToolStripMenuItem.Name = "ExportInDateiToolStripMenuItem"
        Me.ExportInDateiToolStripMenuItem.Size = New System.Drawing.Size(280, 30)
        Me.ExportInDateiToolStripMenuItem.Text = "Export to file..."
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.ToolStripTextBox1, Me.ToolStripSeparator1, Me.ToolStripButton2, Me.ToolStripSeparator9, Me.ToolStripButton10, Me.ToolStripButton14, Me.ToolStripButton12, Me.ToolStripButton11})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(443, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(33, 22)
        Me.ToolStripLabel1.Text = "SMS:"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Enabled = False
        Me.ToolStripTextBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(100, 25)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Enabled = False
        Me.ToolStripButton2.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_refresh_16x16
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(138, 22)
        Me.ToolStripButton2.Text = "&Reload all collections"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton10
        '
        Me.ToolStripButton10.Checked = True
        Me.ToolStripButton10.CheckOnClick = True
        Me.ToolStripButton10.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton10.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_filter_16x16
        Me.ToolStripButton10.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton10.Name = "ToolStripButton10"
        Me.ToolStripButton10.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton10.Text = "Filter collections"
        '
        'ToolStripButton14
        '
        Me.ToolStripButton14.Checked = True
        Me.ToolStripButton14.CheckOnClick = True
        Me.ToolStripButton14.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton14.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_book_16x16
        Me.ToolStripButton14.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton14.Name = "ToolStripButton14"
        Me.ToolStripButton14.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton14.Text = "Group collections"
        '
        'ToolStripButton12
        '
        Me.ToolStripButton12.CheckOnClick = True
        Me.ToolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton12.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_news_16x16
        Me.ToolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton12.Name = "ToolStripButton12"
        Me.ToolStripButton12.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton12.Text = "Show only collections with members"
        '
        'ToolStripButton11
        '
        Me.ToolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton11.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_search_16x16
        Me.ToolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton11.Name = "ToolStripButton11"
        Me.ToolStripButton11.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton11.Text = "Search catalog..."
        '
        'ListView3
        '
        Me.ListView3.BackColor = System.Drawing.Color.Black
        Me.ListView3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView3.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4, Me.ColumnHeader24, Me.ColumnHeader25})
        Me.ListView3.ContextMenuStrip = Me.LogContextMenu
        Me.ListView3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView3.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView3.ForeColor = System.Drawing.Color.White
        Me.ListView3.FullRowSelect = True
        Me.ListView3.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListView3.HideSelection = False
        Me.ListView3.Location = New System.Drawing.Point(0, 0)
        Me.ListView3.Name = "ListView3"
        Me.ListView3.Size = New System.Drawing.Size(150, 46)
        Me.ListView3.TabIndex = 0
        Me.ListView3.UseCompatibleStateImageBehavior = False
        Me.ListView3.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Date/Time"
        Me.ColumnHeader4.Width = 130
        '
        'ColumnHeader24
        '
        Me.ColumnHeader24.Text = "Thread-ID"
        Me.ColumnHeader24.Width = 35
        '
        'ColumnHeader25
        '
        Me.ColumnHeader25.Text = "Output"
        Me.ColumnHeader25.Width = 80
        '
        'LogContextMenu
        '
        Me.LogContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LogSpeichernToolStripMenuItem})
        Me.LogContextMenu.Name = "LogContextMenu"
        Me.LogContextMenu.Size = New System.Drawing.Size(128, 26)
        '
        'LogSpeichernToolStripMenuItem
        '
        Me.LogSpeichernToolStripMenuItem.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_news_16x16
        Me.LogSpeichernToolStripMenuItem.Name = "LogSpeichernToolStripMenuItem"
        Me.LogSpeichernToolStripMenuItem.Size = New System.Drawing.Size(135, 30)
        Me.LogSpeichernToolStripMenuItem.Text = "Save log..."
        '
        'CollectServerPackageStateWorker
        '
        Me.CollectServerPackageStateWorker.WorkerReportsProgress = True
        Me.CollectServerPackageStateWorker.WorkerSupportsCancellation = True
        '
        'SaveTextfileDlg
        '
        Me.SaveTextfileDlg.Filter = "CSV-File (Comma-separated)|*.csv|Textfile (Comma-separated)|*.txt|CSV-File (Semik" &
    "olon-separated)|*.csv"
        Me.SaveTextfileDlg.RestoreDirectory = True
        Me.SaveTextfileDlg.SupportMultiDottedExtensions = True
        '
        'CollectClientSearchQueryWorker
        '
        Me.CollectClientSearchQueryWorker.WorkerReportsProgress = True
        Me.CollectClientSearchQueryWorker.WorkerSupportsCancellation = True
        '
        'DeleteFromCollectionWorker
        '
        Me.DeleteFromCollectionWorker.WorkerReportsProgress = True
        Me.DeleteFromCollectionWorker.WorkerSupportsCancellation = True
        '
        'AddToCollectionWorker
        '
        Me.AddToCollectionWorker.WorkerReportsProgress = True
        Me.AddToCollectionWorker.WorkerSupportsCancellation = True
        '
        'CollectClientPackageStateWorker
        '
        Me.CollectClientPackageStateWorker.WorkerReportsProgress = True
        Me.CollectClientPackageStateWorker.WorkerSupportsCancellation = True
        '
        'CollectClientCollectionMembershipsWorker
        '
        Me.CollectClientCollectionMembershipsWorker.WorkerReportsProgress = True
        Me.CollectClientCollectionMembershipsWorker.WorkerSupportsCancellation = True
        '
        'CollectAllClientsFromCollection
        '
        Me.CollectAllClientsFromCollection.WorkerReportsProgress = True
        Me.CollectAllClientsFromCollection.WorkerSupportsCancellation = True
        '
        'CollectSMSCollectionsWorker
        '
        Me.CollectSMSCollectionsWorker.WorkerReportsProgress = True
        Me.CollectSMSCollectionsWorker.WorkerSupportsCancellation = True
        '
        'GetAllClientsWorker
        '
        Me.GetAllClientsWorker.WorkerReportsProgress = True
        Me.GetAllClientsWorker.WorkerSupportsCancellation = True
        '
        'ConnectToSMSMServerWorker
        '
        Me.ConnectToSMSMServerWorker.WorkerReportsProgress = True
        Me.ConnectToSMSMServerWorker.WorkerSupportsCancellation = True
        '
        'CloneCollectionFromDeviceWorker
        '
        Me.CloneCollectionFromDeviceWorker.WorkerReportsProgress = True
        Me.CloneCollectionFromDeviceWorker.WorkerSupportsCancellation = True
        '
        'PasteCollectionsWorker
        '
        Me.PasteCollectionsWorker.WorkerReportsProgress = True
        Me.PasteCollectionsWorker.WorkerSupportsCancellation = True
        '
        'RefreshCollectionsToolStripMenuItem
        '
        Me.RefreshCollectionsToolStripMenuItem.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_refresh_16x16
        Me.RefreshCollectionsToolStripMenuItem.Name = "RefreshCollectionsToolStripMenuItem"
        Me.RefreshCollectionsToolStripMenuItem.Size = New System.Drawing.Size(280, 30)
        Me.RefreshCollectionsToolStripMenuItem.Text = "Refresh collections"
        '
        'ClientGUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "ClientGUI"
        Me.Size = New System.Drawing.Size(1222, 759)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel1.PerformLayout()
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.Panel2.PerformLayout()
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.SCCMClientsSplash, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ClientItemMenu.ResumeLayout(False)
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.GroupMemberSplash, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RemoveMemershipContextMenu.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.ClientPackageSplash, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ClientPackageStateContextMenu.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.ServerPackageSplash, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ServerPackageStateContextMenu.ResumeLayout(False)
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        CType(Me.SMSCollectionSplash, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AddMemberContextMenu.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.LogContextMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents SplitContainer4 As SplitContainer
    Friend WithEvents SCCMClientsSplashLabel As Label
    Friend WithEvents SCCMClientsSplash As PictureBox
    Friend WithEvents ListView5 As ListView
    Friend WithEvents ColumnHeader19 As ColumnHeader
    Friend WithEvents ColumnHeader20 As ColumnHeader
    Friend WithEvents ColumnHeader21 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader26 As ColumnHeader
    Friend WithEvents ColumnHeader27 As ColumnHeader
    Friend WithEvents ToolStrip3 As ToolStrip
    Friend WithEvents ToolStripLabel3 As ToolStripLabel
    Friend WithEvents ToolStripTextBox3 As ToolStripTextBox
    Friend WithEvents ToolStripButton8 As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripButton7 As ToolStripButton
    Friend WithEvents ToolStripButton4 As ToolStripButton
    Friend WithEvents ToolStripButton9 As ToolStripButton
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents GroupMemberSplashLabel As Label
    Friend WithEvents GroupMemberSplash As PictureBox
    Friend WithEvents ListView4 As ListView
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader9 As ColumnHeader
    Friend WithEvents ColumnHeader17 As ColumnHeader
    Friend WithEvents ColumnHeader18 As ColumnHeader
    Friend WithEvents ColumnHeader23 As ColumnHeader
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents ClientPackageSplashLabel As Label
    Friend WithEvents ClientPackageSplash As PictureBox
    Friend WithEvents ListView1 As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader10 As ColumnHeader
    Friend WithEvents ColumnHeader11 As ColumnHeader
    Friend WithEvents ColumnHeader12 As ColumnHeader
    Friend WithEvents ColumnHeader13 As ColumnHeader
    Friend WithEvents ColumnHeader14 As ColumnHeader
    Friend WithEvents ColumnHeader15 As ColumnHeader
    Friend WithEvents ColumnHeader16 As ColumnHeader
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents ServerPackageSplashLabel As Label
    Friend WithEvents ServerPackageSplash As PictureBox
    Friend WithEvents ListView6 As ListView
    Friend WithEvents ColumnHeader28 As ColumnHeader
    Friend WithEvents ColumnHeader32 As ColumnHeader
    Friend WithEvents ColumnHeader33 As ColumnHeader
    Friend WithEvents ColumnHeader35 As ColumnHeader
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents ToolStripLabel2 As ToolStripLabel
    Friend WithEvents ToolStripTextBox2 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents ToolStripButton6 As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripButton3 As ToolStripButton
    Friend WithEvents LiveModeButton As ToolStripButton
    Friend WithEvents SMSCollectionSplashLabel As Label
    Friend WithEvents SMSCollectionSplash As PictureBox
    Friend WithEvents ListView2 As ListView
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents ColumnHeader22 As ColumnHeader
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents ToolStripTextBox1 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents ToolStripButton10 As ToolStripButton
    Friend WithEvents ToolStripButton12 As ToolStripButton
    Friend WithEvents ToolStripButton11 As ToolStripButton
    Friend WithEvents ListView3 As ListView
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader24 As ColumnHeader
    Friend WithEvents ColumnHeader25 As ColumnHeader
    Friend WithEvents CollectServerPackageStateWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents SaveTextfileDlg As SaveFileDialog
    Friend WithEvents CollectClientSearchQueryWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents DeleteFromCollectionWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents AddToCollectionWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents CollectClientPackageStateWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents CollectClientCollectionMembershipsWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents CollectAllClientsFromCollection As System.ComponentModel.BackgroundWorker
    Friend WithEvents CollectSMSCollectionsWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents GetAllClientsWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents LogContextMenu As ContextMenuStrip
    Friend WithEvents LogSpeichernToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AddMemberContextMenu As ContextMenuStrip
    Friend WithEvents ZuweisenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AlleGeräteMitDieserCollectionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ObjekteigenschaftenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents ExportInDateiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ServerPackageStateContextMenu As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents ClientPackageStateContextMenu As ContextMenuStrip
    Friend WithEvents NurClientstatusAktualisierenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ObjekteigenschaftenToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents ExportInDateiToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents RemoveMemershipContextMenu As ContextMenuStrip
    Friend WithEvents EntfernenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ObjekteigenschaftenToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents ExportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConnectToSMSMServerWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents ToolStripButton5 As ToolStripButton
    Friend WithEvents ToolStripButton13 As ToolStripButton
    Friend WithEvents ToolStripButton14 As ToolStripButton
    Friend WithEvents ToolStripButton15 As ToolStripButton
    Friend WithEvents ClientItemMenu As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem4 As ToolStripMenuItem
    Friend WithEvents ToolStripButton16 As ToolStripButton
    Friend WithEvents CloneCollectionFromDeviceWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStripSeparator10 As ToolStripSeparator
    Friend WithEvents SortAscendingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SortDescendingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents CopySelectedMembershipsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PasteMembershipsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PasteCollectionsWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents CopySelectedMembershipsToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents RefreshCollectionsToolStripMenuItem As ToolStripMenuItem
End Class
