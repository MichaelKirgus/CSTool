﻿'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.HostnameOrIPCtl = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.SearchInNewWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchInNewInstanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripButton12 = New System.Windows.Forms.ToolStripSplitButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripSplitButton()
        Me.NewIndependentWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.DockdocumentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DockleftToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DockrightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DockbottomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocktopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
        Me.NewProcessWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripSplitButton()
        Me.CloseAllOtherWindowsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.CloseAllWindowsDocumentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseAllWindowsfloatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.CloseAllWindowsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton13 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator16 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton11 = New System.Windows.Forms.ToolStripSplitButton()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.NewEmptyWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloneWorkspaceToNewWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.RestoreLastClosedWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestoreInitialTemplateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.ResetUserProfileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripSplitButton()
        Me.PluginSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TemplateSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WorkplaceSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ApplicationSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.EnvironmentVariablesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowApplicationRuntimeInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExportWorkspaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReloadUserTemplatesAndActionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveWorkspaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportWorkspaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitWithoutWarning = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseChildsWithoutWarningToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton9 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton10 = New System.Windows.Forms.ToolStripButton()
        Me.DockingSuiteVS2015LightTheme = New WeifenLuo.WinFormsUI.Docking.VS2015LightTheme()
        Me.DockingSuiteVS2015BlueTheme = New WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme()
        Me.CSDockPanelHosting = New WeifenLuo.WinFormsUI.Docking.DockPanel()
        Me.DockingMainMenuContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddItemFromTemplatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VS2015DarkTheme1 = New WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme()
        Me.VisualStudioToolStripExtender1 = New WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender(Me.components)
        Me.CustomItemsContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExportWorkspaceFile = New System.Windows.Forms.SaveFileDialog()
        Me.ImportWorkspaceFile = New System.Windows.Forms.OpenFileDialog()
        Me.CheckForLockfile = New System.ComponentModel.BackgroundWorker()
        Me.RaiseActionsAsyncWorker = New System.ComponentModel.BackgroundWorker()
        Me.RemoveStaleSettings = New System.ComponentModel.BackgroundWorker()
        Me.SaveSettingsAsyncWorker = New System.ComponentModel.BackgroundWorker()
        Me.ControlVisibleMenuItemsContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowHostnameIPLabelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowHostnameIPTextboxToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowHostnameIPSearchIconToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowHostnameIPRefreshIconToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator17 = New System.Windows.Forms.ToolStripSeparator()
        Me.ShowCustomActionIconToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator18 = New System.Windows.Forms.ToolStripSeparator()
        Me.ShowTemplateManagerIconToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowAddItemIconToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowRemoveItemIconToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator19 = New System.Windows.Forms.ToolStripSeparator()
        Me.ShowWorkspaceTemplateManagerIconToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator20 = New System.Windows.Forms.ToolStripSeparator()
        Me.ShowWindowManagerIconToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowNewInstanceIconToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator21 = New System.Windows.Forms.ToolStripSeparator()
        Me.ShowUserProfileManagerIconToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowLockunlockWorkpsaceIconToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.CSDockPanelHosting, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DockingMainMenuContext.SuspendLayout()
        Me.ControlVisibleMenuItemsContext.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ContextMenuStrip = Me.ControlVisibleMenuItemsContext
        Me.ToolStrip1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.HostnameOrIPCtl, Me.ToolStripButton1, Me.ToolStripButton4, Me.ToolStripSeparator2, Me.ToolStripButton2, Me.ToolStripSeparator3, Me.ToolStripComboBox1, Me.ToolStripButton12, Me.ToolStripButton3, Me.ToolStripButton6, Me.ToolStripSeparator4, Me.ToolStripButton13, Me.ToolStripSeparator16, Me.ToolStripButton11, Me.ToolStripButton5, Me.ToolStripButton7, Me.ToolStripSeparator1, Me.ToolStripButton8, Me.ToolStripSeparator5, Me.ToolStripButton9, Me.ToolStripButton10})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip1.Size = New System.Drawing.Size(1007, 29)
        Me.ToolStrip1.TabIndex = 0
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStripLabel1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(85, 26)
        Me.ToolStripLabel1.Text = "Hostname/IP:"
        '
        'HostnameOrIPCtl
        '
        Me.HostnameOrIPCtl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.HostnameOrIPCtl.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HostnameOrIPCtl.Name = "HostnameOrIPCtl"
        Me.HostnameOrIPCtl.Size = New System.Drawing.Size(159, 29)
        Me.HostnameOrIPCtl.ToolTipText = "Please enter hostname or ip-address." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Shortcuts:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ENTF: Clear hostname/ip" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "F5: " &
    "Refresh GUI" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "F9: Open new instance with entered hostname/ip" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "F10: Open new windo" &
    "w with entered hostname/ip"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.DropDownButtonWidth = 15
        Me.ToolStripButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SearchInNewWindowToolStripMenuItem, Me.SearchInNewInstanceToolStripMenuItem})
        Me.ToolStripButton1.Image = Global.CSTool.My.Resources.Resources.icon_search_22x22
        Me.ToolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(42, 26)
        Me.ToolStripButton1.Text = "Get information"
        '
        'SearchInNewWindowToolStripMenuItem
        '
        Me.SearchInNewWindowToolStripMenuItem.Name = "SearchInNewWindowToolStripMenuItem"
        Me.SearchInNewWindowToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
        Me.SearchInNewWindowToolStripMenuItem.Text = "Search in new window"
        '
        'SearchInNewInstanceToolStripMenuItem
        '
        Me.SearchInNewInstanceToolStripMenuItem.Name = "SearchInNewInstanceToolStripMenuItem"
        Me.SearchInNewInstanceToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
        Me.SearchInNewInstanceToolStripMenuItem.Text = "Search in new instance"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Image = Global.CSTool.My.Resources.Resources.icon_refresh_22x22
        Me.ToolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton4.Text = "Refresh all windows"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = Global.CSTool.My.Resources.Resources.icon_grid_22x22
        Me.ToolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton2.Text = "Get custom actions"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripComboBox1
        '
        Me.ToolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ToolStripComboBox1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripComboBox1.Name = "ToolStripComboBox1"
        Me.ToolStripComboBox1.Size = New System.Drawing.Size(199, 29)
        Me.ToolStripComboBox1.ToolTipText = "Choose window plugin"
        '
        'ToolStripButton12
        '
        Me.ToolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton12.DropDownButtonWidth = 15
        Me.ToolStripButton12.Image = Global.CSTool.My.Resources.Resources.icon_archive_22x22
        Me.ToolStripButton12.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton12.Name = "ToolStripButton12"
        Me.ToolStripButton12.Size = New System.Drawing.Size(42, 26)
        Me.ToolStripButton12.Text = "Add item from template..."
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.DropDownButtonWidth = 15
        Me.ToolStripButton3.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewIndependentWindowToolStripMenuItem, Me.NewWindowToolStripMenuItem, Me.ToolStripSeparator6, Me.DockdocumentToolStripMenuItem, Me.DockleftToolStripMenuItem, Me.DockrightToolStripMenuItem, Me.DockbottomToolStripMenuItem, Me.DocktopToolStripMenuItem, Me.ToolStripSeparator15, Me.NewProcessWindowToolStripMenuItem})
        Me.ToolStripButton3.Image = Global.CSTool.My.Resources.Resources.icon_plus_square_22x22
        Me.ToolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(42, 26)
        Me.ToolStripButton3.Text = "Add plugin item (docking)"
        '
        'NewIndependentWindowToolStripMenuItem
        '
        Me.NewIndependentWindowToolStripMenuItem.Name = "NewIndependentWindowToolStripMenuItem"
        Me.NewIndependentWindowToolStripMenuItem.Size = New System.Drawing.Size(239, 22)
        Me.NewIndependentWindowToolStripMenuItem.Text = "New independent window"
        '
        'NewWindowToolStripMenuItem
        '
        Me.NewWindowToolStripMenuItem.Name = "NewWindowToolStripMenuItem"
        Me.NewWindowToolStripMenuItem.Size = New System.Drawing.Size(239, 22)
        Me.NewWindowToolStripMenuItem.Text = "New window"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(236, 6)
        '
        'DockdocumentToolStripMenuItem
        '
        Me.DockdocumentToolStripMenuItem.Name = "DockdocumentToolStripMenuItem"
        Me.DockdocumentToolStripMenuItem.Size = New System.Drawing.Size(239, 22)
        Me.DockdocumentToolStripMenuItem.Text = "Dock (document)"
        '
        'DockleftToolStripMenuItem
        '
        Me.DockleftToolStripMenuItem.Name = "DockleftToolStripMenuItem"
        Me.DockleftToolStripMenuItem.Size = New System.Drawing.Size(239, 22)
        Me.DockleftToolStripMenuItem.Text = "Dock (left)"
        '
        'DockrightToolStripMenuItem
        '
        Me.DockrightToolStripMenuItem.Name = "DockrightToolStripMenuItem"
        Me.DockrightToolStripMenuItem.Size = New System.Drawing.Size(239, 22)
        Me.DockrightToolStripMenuItem.Text = "Dock (right)"
        '
        'DockbottomToolStripMenuItem
        '
        Me.DockbottomToolStripMenuItem.Name = "DockbottomToolStripMenuItem"
        Me.DockbottomToolStripMenuItem.Size = New System.Drawing.Size(239, 22)
        Me.DockbottomToolStripMenuItem.Text = "Dock (bottom)"
        '
        'DocktopToolStripMenuItem
        '
        Me.DocktopToolStripMenuItem.Name = "DocktopToolStripMenuItem"
        Me.DocktopToolStripMenuItem.Size = New System.Drawing.Size(239, 22)
        Me.DocktopToolStripMenuItem.Text = "Dock (top)"
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(236, 6)
        '
        'NewProcessWindowToolStripMenuItem
        '
        Me.NewProcessWindowToolStripMenuItem.Name = "NewProcessWindowToolStripMenuItem"
        Me.NewProcessWindowToolStripMenuItem.Size = New System.Drawing.Size(239, 22)
        Me.NewProcessWindowToolStripMenuItem.Text = "Standalone process window"
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton6.DropDownButtonWidth = 15
        Me.ToolStripButton6.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CloseAllOtherWindowsToolStripMenuItem, Me.ToolStripSeparator11, Me.CloseAllWindowsDocumentToolStripMenuItem, Me.CloseAllWindowsfloatToolStripMenuItem, Me.ToolStripSeparator12, Me.CloseAllWindowsToolStripMenuItem})
        Me.ToolStripButton6.Image = Global.CSTool.My.Resources.Resources.icon_minus_square_22x22
        Me.ToolStripButton6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(42, 26)
        Me.ToolStripButton6.Text = "close plugin item (window)"
        '
        'CloseAllOtherWindowsToolStripMenuItem
        '
        Me.CloseAllOtherWindowsToolStripMenuItem.Name = "CloseAllOtherWindowsToolStripMenuItem"
        Me.CloseAllOtherWindowsToolStripMenuItem.Size = New System.Drawing.Size(249, 22)
        Me.CloseAllOtherWindowsToolStripMenuItem.Text = "Close all other windows"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(246, 6)
        '
        'CloseAllWindowsDocumentToolStripMenuItem
        '
        Me.CloseAllWindowsDocumentToolStripMenuItem.Name = "CloseAllWindowsDocumentToolStripMenuItem"
        Me.CloseAllWindowsDocumentToolStripMenuItem.Size = New System.Drawing.Size(249, 22)
        Me.CloseAllWindowsDocumentToolStripMenuItem.Text = "Close all windows (document)"
        '
        'CloseAllWindowsfloatToolStripMenuItem
        '
        Me.CloseAllWindowsfloatToolStripMenuItem.Name = "CloseAllWindowsfloatToolStripMenuItem"
        Me.CloseAllWindowsfloatToolStripMenuItem.Size = New System.Drawing.Size(249, 22)
        Me.CloseAllWindowsfloatToolStripMenuItem.Text = "Close all windows (float)"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(246, 6)
        '
        'CloseAllWindowsToolStripMenuItem
        '
        Me.CloseAllWindowsToolStripMenuItem.Name = "CloseAllWindowsToolStripMenuItem"
        Me.CloseAllWindowsToolStripMenuItem.Size = New System.Drawing.Size(249, 22)
        Me.CloseAllWindowsToolStripMenuItem.Text = "Close all windows"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripButton13
        '
        Me.ToolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton13.Image = Global.CSTool.My.Resources.Resources.icon_desktop_22x22
        Me.ToolStripButton13.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton13.Name = "ToolStripButton13"
        Me.ToolStripButton13.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton13.Text = "Workspace template manager"
        '
        'ToolStripSeparator16
        '
        Me.ToolStripSeparator16.Name = "ToolStripSeparator16"
        Me.ToolStripSeparator16.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripButton11
        '
        Me.ToolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton11.DropDownButtonWidth = 15
        Me.ToolStripButton11.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem2, Me.ToolStripSeparator9, Me.NewEmptyWindowToolStripMenuItem, Me.CloneWorkspaceToNewWindowToolStripMenuItem, Me.ToolStripSeparator10, Me.RestoreLastClosedWindowToolStripMenuItem, Me.RestoreInitialTemplateToolStripMenuItem, Me.ToolStripSeparator14, Me.ResetUserProfileToolStripMenuItem})
        Me.ToolStripButton11.Image = Global.CSTool.My.Resources.Resources.icon_browser_22x22
        Me.ToolStripButton11.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton11.Name = "ToolStripButton11"
        Me.ToolStripButton11.Size = New System.Drawing.Size(42, 26)
        Me.ToolStripButton11.Text = "Templates/Workspaces"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(362, 22)
        Me.ToolStripMenuItem2.Text = "Window manager"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(359, 6)
        '
        'NewEmptyWindowToolStripMenuItem
        '
        Me.NewEmptyWindowToolStripMenuItem.Name = "NewEmptyWindowToolStripMenuItem"
        Me.NewEmptyWindowToolStripMenuItem.Size = New System.Drawing.Size(362, 22)
        Me.NewEmptyWindowToolStripMenuItem.Text = "New empty window (non-persistent)"
        '
        'CloneWorkspaceToNewWindowToolStripMenuItem
        '
        Me.CloneWorkspaceToNewWindowToolStripMenuItem.Name = "CloneWorkspaceToNewWindowToolStripMenuItem"
        Me.CloneWorkspaceToNewWindowToolStripMenuItem.Size = New System.Drawing.Size(362, 22)
        Me.CloneWorkspaceToNewWindowToolStripMenuItem.Text = "Clone workspace to new window (non-persistent)"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(359, 6)
        '
        'RestoreLastClosedWindowToolStripMenuItem
        '
        Me.RestoreLastClosedWindowToolStripMenuItem.Name = "RestoreLastClosedWindowToolStripMenuItem"
        Me.RestoreLastClosedWindowToolStripMenuItem.Size = New System.Drawing.Size(362, 22)
        Me.RestoreLastClosedWindowToolStripMenuItem.Text = "Restore last closed window"
        '
        'RestoreInitialTemplateToolStripMenuItem
        '
        Me.RestoreInitialTemplateToolStripMenuItem.Name = "RestoreInitialTemplateToolStripMenuItem"
        Me.RestoreInitialTemplateToolStripMenuItem.Size = New System.Drawing.Size(362, 22)
        Me.RestoreInitialTemplateToolStripMenuItem.Text = "Restore initial template"
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(359, 6)
        '
        'ResetUserProfileToolStripMenuItem
        '
        Me.ResetUserProfileToolStripMenuItem.Name = "ResetUserProfileToolStripMenuItem"
        Me.ResetUserProfileToolStripMenuItem.Size = New System.Drawing.Size(362, 22)
        Me.ResetUserProfileToolStripMenuItem.Text = "Reset user profile"
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = Global.CSTool.My.Resources.Resources.icon_external_link_22x22
        Me.ToolStripButton5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton5.Text = "New instance"
        '
        'ToolStripButton7
        '
        Me.ToolStripButton7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton7.Image = Global.CSTool.My.Resources.Resources.icon_information_22x22
        Me.ToolStripButton7.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton7.Name = "ToolStripButton7"
        Me.ToolStripButton7.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton7.Text = "Info"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton8.DropDownButtonWidth = 18
        Me.ToolStripButton8.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PluginSettingsToolStripMenuItem, Me.TemplateSettingsToolStripMenuItem, Me.WorkplaceSettingsToolStripMenuItem, Me.ApplicationSettingsToolStripMenuItem, Me.LogSettingsToolStripMenuItem, Me.ToolStripSeparator8, Me.EnvironmentVariablesToolStripMenuItem, Me.ToolStripMenuItem1, Me.ShowApplicationRuntimeInfoToolStripMenuItem, Me.ShowLogToolStripMenuItem, Me.ToolStripSeparator13, Me.ExportWorkspaceToolStripMenuItem, Me.ReloadUserTemplatesAndActionsToolStripMenuItem, Me.SaveWorkspaceToolStripMenuItem, Me.ImportWorkspaceToolStripMenuItem, Me.ToolStripSeparator7, Me.ExitWithoutWarning, Me.CloseChildsWithoutWarningToolStripMenuItem, Me.SaveSettingsToolStripMenuItem})
        Me.ToolStripButton8.Image = Global.CSTool.My.Resources.Resources.icon_menu_22x22
        Me.ToolStripButton8.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.Size = New System.Drawing.Size(45, 26)
        Me.ToolStripButton8.Text = "Options"
        '
        'PluginSettingsToolStripMenuItem
        '
        Me.PluginSettingsToolStripMenuItem.Image = Global.CSTool.My.Resources.Resources.icon_puzzle_16x16
        Me.PluginSettingsToolStripMenuItem.Name = "PluginSettingsToolStripMenuItem"
        Me.PluginSettingsToolStripMenuItem.Size = New System.Drawing.Size(278, 22)
        Me.PluginSettingsToolStripMenuItem.Text = "Plug-in settings..."
        '
        'TemplateSettingsToolStripMenuItem
        '
        Me.TemplateSettingsToolStripMenuItem.Image = Global.CSTool.My.Resources.Resources.icon_archive_16x16
        Me.TemplateSettingsToolStripMenuItem.Name = "TemplateSettingsToolStripMenuItem"
        Me.TemplateSettingsToolStripMenuItem.Size = New System.Drawing.Size(278, 22)
        Me.TemplateSettingsToolStripMenuItem.Text = "Template settings..."
        '
        'WorkplaceSettingsToolStripMenuItem
        '
        Me.WorkplaceSettingsToolStripMenuItem.Image = Global.CSTool.My.Resources.Resources.icon_desktop_16x16
        Me.WorkplaceSettingsToolStripMenuItem.Name = "WorkplaceSettingsToolStripMenuItem"
        Me.WorkplaceSettingsToolStripMenuItem.Size = New System.Drawing.Size(278, 22)
        Me.WorkplaceSettingsToolStripMenuItem.Text = "Workplace settings..."
        '
        'ApplicationSettingsToolStripMenuItem
        '
        Me.ApplicationSettingsToolStripMenuItem.Image = Global.CSTool.My.Resources.Resources.icon_server_16x16
        Me.ApplicationSettingsToolStripMenuItem.Name = "ApplicationSettingsToolStripMenuItem"
        Me.ApplicationSettingsToolStripMenuItem.Size = New System.Drawing.Size(278, 22)
        Me.ApplicationSettingsToolStripMenuItem.Text = "Application settings..."
        '
        'LogSettingsToolStripMenuItem
        '
        Me.LogSettingsToolStripMenuItem.Image = Global.CSTool.My.Resources.Resources.icon_file_16x16
        Me.LogSettingsToolStripMenuItem.Name = "LogSettingsToolStripMenuItem"
        Me.LogSettingsToolStripMenuItem.Size = New System.Drawing.Size(278, 22)
        Me.LogSettingsToolStripMenuItem.Text = "Log settings..."
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(275, 6)
        '
        'EnvironmentVariablesToolStripMenuItem
        '
        Me.EnvironmentVariablesToolStripMenuItem.Image = Global.CSTool.My.Resources.Resources.icon_inbox_16x16
        Me.EnvironmentVariablesToolStripMenuItem.Name = "EnvironmentVariablesToolStripMenuItem"
        Me.EnvironmentVariablesToolStripMenuItem.Size = New System.Drawing.Size(278, 22)
        Me.EnvironmentVariablesToolStripMenuItem.Text = "Environment variables..."
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Image = Global.CSTool.My.Resources.Resources.icon_search_16x16
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(278, 22)
        Me.ToolStripMenuItem1.Text = "Test environment strings..."
        '
        'ShowApplicationRuntimeInfoToolStripMenuItem
        '
        Me.ShowApplicationRuntimeInfoToolStripMenuItem.Image = Global.CSTool.My.Resources.Resources.icon_information_16x16
        Me.ShowApplicationRuntimeInfoToolStripMenuItem.Name = "ShowApplicationRuntimeInfoToolStripMenuItem"
        Me.ShowApplicationRuntimeInfoToolStripMenuItem.Size = New System.Drawing.Size(278, 22)
        Me.ShowApplicationRuntimeInfoToolStripMenuItem.Text = "Show application runtime info..."
        '
        'ShowLogToolStripMenuItem
        '
        Me.ShowLogToolStripMenuItem.Image = Global.CSTool.My.Resources.Resources.icon_menu_16x16
        Me.ShowLogToolStripMenuItem.Name = "ShowLogToolStripMenuItem"
        Me.ShowLogToolStripMenuItem.Size = New System.Drawing.Size(278, 22)
        Me.ShowLogToolStripMenuItem.Text = "Show Log..."
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(275, 6)
        '
        'ExportWorkspaceToolStripMenuItem
        '
        Me.ExportWorkspaceToolStripMenuItem.Image = Global.CSTool.My.Resources.Resources.icon_upload_16x16
        Me.ExportWorkspaceToolStripMenuItem.Name = "ExportWorkspaceToolStripMenuItem"
        Me.ExportWorkspaceToolStripMenuItem.Size = New System.Drawing.Size(278, 22)
        Me.ExportWorkspaceToolStripMenuItem.Text = "Export workspace..."
        '
        'ReloadUserTemplatesAndActionsToolStripMenuItem
        '
        Me.ReloadUserTemplatesAndActionsToolStripMenuItem.Image = Global.CSTool.My.Resources.Resources.icon_refresh_16x16
        Me.ReloadUserTemplatesAndActionsToolStripMenuItem.Name = "ReloadUserTemplatesAndActionsToolStripMenuItem"
        Me.ReloadUserTemplatesAndActionsToolStripMenuItem.Size = New System.Drawing.Size(278, 22)
        Me.ReloadUserTemplatesAndActionsToolStripMenuItem.Text = "Reload user templates and actions"
        '
        'SaveWorkspaceToolStripMenuItem
        '
        Me.SaveWorkspaceToolStripMenuItem.Image = Global.CSTool.My.Resources.Resources.icon_check_circle_16x16
        Me.SaveWorkspaceToolStripMenuItem.Name = "SaveWorkspaceToolStripMenuItem"
        Me.SaveWorkspaceToolStripMenuItem.Size = New System.Drawing.Size(278, 22)
        Me.SaveWorkspaceToolStripMenuItem.Text = "Save workspace"
        '
        'ImportWorkspaceToolStripMenuItem
        '
        Me.ImportWorkspaceToolStripMenuItem.Image = Global.CSTool.My.Resources.Resources.icon_download_16x16
        Me.ImportWorkspaceToolStripMenuItem.Name = "ImportWorkspaceToolStripMenuItem"
        Me.ImportWorkspaceToolStripMenuItem.Size = New System.Drawing.Size(278, 22)
        Me.ImportWorkspaceToolStripMenuItem.Text = "Import workspace..."
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(275, 6)
        '
        'ExitWithoutWarning
        '
        Me.ExitWithoutWarning.Checked = True
        Me.ExitWithoutWarning.CheckOnClick = True
        Me.ExitWithoutWarning.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ExitWithoutWarning.Name = "ExitWithoutWarning"
        Me.ExitWithoutWarning.Size = New System.Drawing.Size(278, 22)
        Me.ExitWithoutWarning.Text = "Exit without warning"
        '
        'CloseChildsWithoutWarningToolStripMenuItem
        '
        Me.CloseChildsWithoutWarningToolStripMenuItem.CheckOnClick = True
        Me.CloseChildsWithoutWarningToolStripMenuItem.Name = "CloseChildsWithoutWarningToolStripMenuItem"
        Me.CloseChildsWithoutWarningToolStripMenuItem.Size = New System.Drawing.Size(278, 22)
        Me.CloseChildsWithoutWarningToolStripMenuItem.Text = "Close childs without warning"
        '
        'SaveSettingsToolStripMenuItem
        '
        Me.SaveSettingsToolStripMenuItem.Checked = True
        Me.SaveSettingsToolStripMenuItem.CheckOnClick = True
        Me.SaveSettingsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SaveSettingsToolStripMenuItem.Name = "SaveSettingsToolStripMenuItem"
        Me.SaveSettingsToolStripMenuItem.Size = New System.Drawing.Size(278, 22)
        Me.SaveSettingsToolStripMenuItem.Text = "Save settings"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripButton9
        '
        Me.ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton9.Image = Global.CSTool.My.Resources.Resources.icon_group_22x22
        Me.ToolStripButton9.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton9.Name = "ToolStripButton9"
        Me.ToolStripButton9.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton9.Text = "User management"
        '
        'ToolStripButton10
        '
        Me.ToolStripButton10.CheckOnClick = True
        Me.ToolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton10.Image = Global.CSTool.My.Resources.Resources.icon_lock_open_22x22
        Me.ToolStripButton10.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton10.Name = "ToolStripButton10"
        Me.ToolStripButton10.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButton10.Text = "Lock/unlock workspace"
        '
        'CSDockPanelHosting
        '
        Me.CSDockPanelHosting.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.CSDockPanelHosting.CausesValidation = False
        Me.CSDockPanelHosting.ContextMenuStrip = Me.DockingMainMenuContext
        Me.CSDockPanelHosting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CSDockPanelHosting.DockBackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.CSDockPanelHosting.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CSDockPanelHosting.Location = New System.Drawing.Point(0, 29)
        Me.CSDockPanelHosting.Margin = New System.Windows.Forms.Padding(4)
        Me.CSDockPanelHosting.Name = "CSDockPanelHosting"
        Me.CSDockPanelHosting.Padding = New System.Windows.Forms.Padding(6)
        Me.CSDockPanelHosting.ShowAutoHideContentOnHover = False
        Me.CSDockPanelHosting.ShowDocumentIcon = True
        Me.CSDockPanelHosting.Size = New System.Drawing.Size(1007, 538)
        Me.CSDockPanelHosting.TabIndex = 3
        Me.CSDockPanelHosting.Theme = Me.VS2015DarkTheme1
        '
        'DockingMainMenuContext
        '
        Me.DockingMainMenuContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddItemFromTemplatesToolStripMenuItem})
        Me.DockingMainMenuContext.Name = "DockingMainMenuContext"
        Me.DockingMainMenuContext.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.DockingMainMenuContext.Size = New System.Drawing.Size(251, 26)
        '
        'AddItemFromTemplatesToolStripMenuItem
        '
        Me.AddItemFromTemplatesToolStripMenuItem.Image = Global.CSTool.My.Resources.Resources.icon_archive_16x16
        Me.AddItemFromTemplatesToolStripMenuItem.Name = "AddItemFromTemplatesToolStripMenuItem"
        Me.AddItemFromTemplatesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert
        Me.AddItemFromTemplatesToolStripMenuItem.Size = New System.Drawing.Size(250, 22)
        Me.AddItemFromTemplatesToolStripMenuItem.Text = "Add item from templates..."
        '
        'VisualStudioToolStripExtender1
        '
        Me.VisualStudioToolStripExtender1.DefaultRenderer = Nothing
        '
        'CustomItemsContext
        '
        Me.CustomItemsContext.Name = "CustomItemsContext"
        Me.CustomItemsContext.Size = New System.Drawing.Size(61, 4)
        '
        'ExportWorkspaceFile
        '
        Me.ExportWorkspaceFile.DefaultExt = "zip"
        Me.ExportWorkspaceFile.Filter = "ZIP-File|*.zip"
        Me.ExportWorkspaceFile.RestoreDirectory = True
        '
        'ImportWorkspaceFile
        '
        Me.ImportWorkspaceFile.DefaultExt = "zip"
        Me.ImportWorkspaceFile.Filter = "ZIP-File|*.zip"
        Me.ImportWorkspaceFile.RestoreDirectory = True
        Me.ImportWorkspaceFile.SupportMultiDottedExtensions = True
        '
        'CheckForLockfile
        '
        Me.CheckForLockfile.WorkerReportsProgress = True
        Me.CheckForLockfile.WorkerSupportsCancellation = True
        '
        'RaiseActionsAsyncWorker
        '
        Me.RaiseActionsAsyncWorker.WorkerReportsProgress = True
        Me.RaiseActionsAsyncWorker.WorkerSupportsCancellation = True
        '
        'RemoveStaleSettings
        '
        Me.RemoveStaleSettings.WorkerReportsProgress = True
        Me.RemoveStaleSettings.WorkerSupportsCancellation = True
        '
        'SaveSettingsAsyncWorker
        '
        Me.SaveSettingsAsyncWorker.WorkerReportsProgress = True
        Me.SaveSettingsAsyncWorker.WorkerSupportsCancellation = True
        '
        'ControlVisibleMenuItemsContext
        '
        Me.ControlVisibleMenuItemsContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowHostnameIPLabelToolStripMenuItem, Me.ShowHostnameIPTextboxToolStripMenuItem, Me.ShowHostnameIPSearchIconToolStripMenuItem, Me.ShowHostnameIPRefreshIconToolStripMenuItem, Me.ToolStripSeparator17, Me.ShowCustomActionIconToolStripMenuItem, Me.ToolStripSeparator18, Me.ShowTemplateManagerIconToolStripMenuItem, Me.ShowAddItemIconToolStripMenuItem, Me.ShowRemoveItemIconToolStripMenuItem, Me.ToolStripSeparator19, Me.ShowWorkspaceTemplateManagerIconToolStripMenuItem, Me.ToolStripSeparator20, Me.ShowWindowManagerIconToolStripMenuItem, Me.ShowNewInstanceIconToolStripMenuItem, Me.ToolStripSeparator21, Me.ShowUserProfileManagerIconToolStripMenuItem, Me.ShowLockunlockWorkpsaceIconToolStripMenuItem})
        Me.ControlVisibleMenuItemsContext.Name = "ControlVisibleMenuItemsContext"
        Me.ControlVisibleMenuItemsContext.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ControlVisibleMenuItemsContext.Size = New System.Drawing.Size(313, 342)
        '
        'ShowHostnameIPLabelToolStripMenuItem
        '
        Me.ShowHostnameIPLabelToolStripMenuItem.CheckOnClick = True
        Me.ShowHostnameIPLabelToolStripMenuItem.Name = "ShowHostnameIPLabelToolStripMenuItem"
        Me.ShowHostnameIPLabelToolStripMenuItem.Size = New System.Drawing.Size(312, 22)
        Me.ShowHostnameIPLabelToolStripMenuItem.Text = "Show ""Hostname/IP"" label"
        '
        'ShowHostnameIPTextboxToolStripMenuItem
        '
        Me.ShowHostnameIPTextboxToolStripMenuItem.CheckOnClick = True
        Me.ShowHostnameIPTextboxToolStripMenuItem.Name = "ShowHostnameIPTextboxToolStripMenuItem"
        Me.ShowHostnameIPTextboxToolStripMenuItem.Size = New System.Drawing.Size(312, 22)
        Me.ShowHostnameIPTextboxToolStripMenuItem.Text = "Show ""Hostname/IP"" text box"
        '
        'ShowHostnameIPSearchIconToolStripMenuItem
        '
        Me.ShowHostnameIPSearchIconToolStripMenuItem.CheckOnClick = True
        Me.ShowHostnameIPSearchIconToolStripMenuItem.Name = "ShowHostnameIPSearchIconToolStripMenuItem"
        Me.ShowHostnameIPSearchIconToolStripMenuItem.Size = New System.Drawing.Size(312, 22)
        Me.ShowHostnameIPSearchIconToolStripMenuItem.Text = "Show ""Hostanme/IP"" search icon"
        '
        'ShowHostnameIPRefreshIconToolStripMenuItem
        '
        Me.ShowHostnameIPRefreshIconToolStripMenuItem.CheckOnClick = True
        Me.ShowHostnameIPRefreshIconToolStripMenuItem.Name = "ShowHostnameIPRefreshIconToolStripMenuItem"
        Me.ShowHostnameIPRefreshIconToolStripMenuItem.Size = New System.Drawing.Size(312, 22)
        Me.ShowHostnameIPRefreshIconToolStripMenuItem.Text = "Show ""Hotsname/IP"" refresh icon"
        '
        'ToolStripSeparator17
        '
        Me.ToolStripSeparator17.Name = "ToolStripSeparator17"
        Me.ToolStripSeparator17.Size = New System.Drawing.Size(309, 6)
        '
        'ShowCustomActionIconToolStripMenuItem
        '
        Me.ShowCustomActionIconToolStripMenuItem.CheckOnClick = True
        Me.ShowCustomActionIconToolStripMenuItem.Name = "ShowCustomActionIconToolStripMenuItem"
        Me.ShowCustomActionIconToolStripMenuItem.Size = New System.Drawing.Size(312, 22)
        Me.ShowCustomActionIconToolStripMenuItem.Text = "Show custom action icon"
        '
        'ToolStripSeparator18
        '
        Me.ToolStripSeparator18.Name = "ToolStripSeparator18"
        Me.ToolStripSeparator18.Size = New System.Drawing.Size(309, 6)
        '
        'ShowTemplateManagerIconToolStripMenuItem
        '
        Me.ShowTemplateManagerIconToolStripMenuItem.CheckOnClick = True
        Me.ShowTemplateManagerIconToolStripMenuItem.Name = "ShowTemplateManagerIconToolStripMenuItem"
        Me.ShowTemplateManagerIconToolStripMenuItem.Size = New System.Drawing.Size(312, 22)
        Me.ShowTemplateManagerIconToolStripMenuItem.Text = "Show template manager icon"
        '
        'ShowAddItemIconToolStripMenuItem
        '
        Me.ShowAddItemIconToolStripMenuItem.CheckOnClick = True
        Me.ShowAddItemIconToolStripMenuItem.Name = "ShowAddItemIconToolStripMenuItem"
        Me.ShowAddItemIconToolStripMenuItem.Size = New System.Drawing.Size(312, 22)
        Me.ShowAddItemIconToolStripMenuItem.Text = "Show ""Add item"" icon + plugin selection box"
        '
        'ShowRemoveItemIconToolStripMenuItem
        '
        Me.ShowRemoveItemIconToolStripMenuItem.CheckOnClick = True
        Me.ShowRemoveItemIconToolStripMenuItem.Name = "ShowRemoveItemIconToolStripMenuItem"
        Me.ShowRemoveItemIconToolStripMenuItem.Size = New System.Drawing.Size(312, 22)
        Me.ShowRemoveItemIconToolStripMenuItem.Text = "Show ""Remove item"" icon"
        '
        'ToolStripSeparator19
        '
        Me.ToolStripSeparator19.Name = "ToolStripSeparator19"
        Me.ToolStripSeparator19.Size = New System.Drawing.Size(309, 6)
        '
        'ShowWorkspaceTemplateManagerIconToolStripMenuItem
        '
        Me.ShowWorkspaceTemplateManagerIconToolStripMenuItem.CheckOnClick = True
        Me.ShowWorkspaceTemplateManagerIconToolStripMenuItem.Name = "ShowWorkspaceTemplateManagerIconToolStripMenuItem"
        Me.ShowWorkspaceTemplateManagerIconToolStripMenuItem.Size = New System.Drawing.Size(312, 22)
        Me.ShowWorkspaceTemplateManagerIconToolStripMenuItem.Text = "Show workspace template manager icon"
        '
        'ToolStripSeparator20
        '
        Me.ToolStripSeparator20.Name = "ToolStripSeparator20"
        Me.ToolStripSeparator20.Size = New System.Drawing.Size(309, 6)
        '
        'ShowWindowManagerIconToolStripMenuItem
        '
        Me.ShowWindowManagerIconToolStripMenuItem.CheckOnClick = True
        Me.ShowWindowManagerIconToolStripMenuItem.Name = "ShowWindowManagerIconToolStripMenuItem"
        Me.ShowWindowManagerIconToolStripMenuItem.Size = New System.Drawing.Size(312, 22)
        Me.ShowWindowManagerIconToolStripMenuItem.Text = "Show window manager icon"
        '
        'ShowNewInstanceIconToolStripMenuItem
        '
        Me.ShowNewInstanceIconToolStripMenuItem.CheckOnClick = True
        Me.ShowNewInstanceIconToolStripMenuItem.Name = "ShowNewInstanceIconToolStripMenuItem"
        Me.ShowNewInstanceIconToolStripMenuItem.Size = New System.Drawing.Size(312, 22)
        Me.ShowNewInstanceIconToolStripMenuItem.Text = "Show new instance icon"
        '
        'ToolStripSeparator21
        '
        Me.ToolStripSeparator21.Name = "ToolStripSeparator21"
        Me.ToolStripSeparator21.Size = New System.Drawing.Size(309, 6)
        '
        'ShowUserProfileManagerIconToolStripMenuItem
        '
        Me.ShowUserProfileManagerIconToolStripMenuItem.CheckOnClick = True
        Me.ShowUserProfileManagerIconToolStripMenuItem.Name = "ShowUserProfileManagerIconToolStripMenuItem"
        Me.ShowUserProfileManagerIconToolStripMenuItem.Size = New System.Drawing.Size(312, 22)
        Me.ShowUserProfileManagerIconToolStripMenuItem.Text = "Show user profile manager icon"
        '
        'ShowLockunlockWorkpsaceIconToolStripMenuItem
        '
        Me.ShowLockunlockWorkpsaceIconToolStripMenuItem.CheckOnClick = True
        Me.ShowLockunlockWorkpsaceIconToolStripMenuItem.Name = "ShowLockunlockWorkpsaceIconToolStripMenuItem"
        Me.ShowLockunlockWorkpsaceIconToolStripMenuItem.Size = New System.Drawing.Size(312, 22)
        Me.ShowLockunlockWorkpsaceIconToolStripMenuItem.Text = "Show lock/unlock workpsace icon"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1007, 567)
        Me.Controls.Add(Me.CSDockPanelHosting)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CSTool"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.CSDockPanelHosting, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DockingMainMenuContext.ResumeLayout(False)
        Me.ControlVisibleMenuItemsContext.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents DockingSuiteVS2015BlueTheme As VS2015BlueTheme
    Friend WithEvents DockingSuiteVS2015LightTheme As VS2015LightTheme
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripComboBox1 As ToolStripComboBox
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripButton5 As ToolStripButton
    Friend WithEvents CSDockPanelHosting As DockPanel
    Friend WithEvents VisualStudioToolStripExtender1 As VisualStudioToolStripExtender
    Friend WithEvents ToolStripButton7 As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripButton9 As ToolStripButton
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents ToolStripButton10 As ToolStripButton
    Friend WithEvents HostnameOrIPCtl As ToolStripComboBox
    Friend WithEvents ToolStripButton8 As ToolStripSplitButton
    Friend WithEvents PluginSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EnvironmentVariablesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripButton3 As ToolStripSplitButton
    Friend WithEvents NewWindowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents DockleftToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DockrightToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DockbottomToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DocktopToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripButton6 As ToolStripSplitButton
    Friend WithEvents ToolStripButton4 As ToolStripButton
    Friend WithEvents VS2015DarkTheme1 As VS2015DarkTheme
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents NewIndependentWindowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DockdocumentToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripButton11 As ToolStripSplitButton
    Friend WithEvents ExitWithoutWarning As ToolStripMenuItem
    Friend WithEvents CustomItemsContext As ContextMenuStrip
    Friend WithEvents ApplicationSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents TemplateSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowLogToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents WorkplaceSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewEmptyWindowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloneWorkspaceToNewWindowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents DockingMainMenuContext As ContextMenuStrip
    Friend WithEvents AddItemFromTemplatesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As ToolStripSeparator
    Friend WithEvents RestoreLastClosedWindowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RestoreInitialTemplateToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LogSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloseChildsWithoutWarningToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloseAllOtherWindowsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents CloseAllWindowsDocumentToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloseAllWindowsfloatToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As ToolStripSeparator
    Friend WithEvents CloseAllWindowsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator13 As ToolStripSeparator
    Friend WithEvents ExportWorkspaceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImportWorkspaceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportWorkspaceFile As SaveFileDialog
    Friend WithEvents ImportWorkspaceFile As OpenFileDialog
    Friend WithEvents ToolStripButton12 As ToolStripSplitButton
    Friend WithEvents ToolStripButton1 As ToolStripSplitButton
    Friend WithEvents SearchInNewWindowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SearchInNewInstanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveWorkspaceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowApplicationRuntimeInfoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator14 As ToolStripSeparator
    Friend WithEvents ResetUserProfileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReloadUserTemplatesAndActionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CheckForLockfile As System.ComponentModel.BackgroundWorker
    Friend WithEvents RaiseActionsAsyncWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents RemoveStaleSettings As System.ComponentModel.BackgroundWorker
    Friend WithEvents SaveSettingsAsyncWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStripSeparator15 As ToolStripSeparator
    Friend WithEvents NewProcessWindowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripButton13 As ToolStripButton
    Friend WithEvents ToolStripSeparator16 As ToolStripSeparator
    Friend WithEvents ControlVisibleMenuItemsContext As ContextMenuStrip
    Friend WithEvents ShowHostnameIPLabelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowHostnameIPTextboxToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowHostnameIPSearchIconToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowHostnameIPRefreshIconToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator17 As ToolStripSeparator
    Friend WithEvents ShowCustomActionIconToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator18 As ToolStripSeparator
    Friend WithEvents ShowTemplateManagerIconToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowAddItemIconToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowRemoveItemIconToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator19 As ToolStripSeparator
    Friend WithEvents ShowWorkspaceTemplateManagerIconToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator20 As ToolStripSeparator
    Friend WithEvents ShowWindowManagerIconToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowNewInstanceIconToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator21 As ToolStripSeparator
    Friend WithEvents ShowUserProfileManagerIconToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowLockunlockWorkpsaceIconToolStripMenuItem As ToolStripMenuItem
End Class
