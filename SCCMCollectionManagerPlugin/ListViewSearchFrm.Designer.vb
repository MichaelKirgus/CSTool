'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ListViewSearchFrm
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.ResultCountLbl = New System.Windows.Forms.Label()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader22 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.AddMemberContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ZuweisenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AlleGeräteMitDieserCollectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopySelectedMembershipsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddSelectedCollectionToClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.AddMemberContextMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(680, 44)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search text"
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextBox1.Location = New System.Drawing.Point(3, 16)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(674, 20)
        Me.TextBox1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(613, 173)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "&Search"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.CheckBox4)
        Me.GroupBox2.Controls.Add(Me.CheckBox3)
        Me.GroupBox2.Controls.Add(Me.CheckBox2)
        Me.GroupBox2.Controls.Add(Me.ComboBox1)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.RadioButton2)
        Me.GroupBox2.Controls.Add(Me.RadioButton1)
        Me.GroupBox2.Controls.Add(Me.CheckBox1)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 54)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(680, 113)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Settings"
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Checked = True
        Me.CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox2.Location = New System.Drawing.Point(148, 42)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(89, 17)
        Me.CheckBox2.TabIndex = 5
        Me.CheckBox2.Text = "Select results"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"All groups"})
        Me.ComboBox1.Location = New System.Drawing.Point(6, 84)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(668, 21)
        Me.ComboBox1.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Search in Group:"
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(148, 19)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(84, 17)
        Me.RadioButton2.TabIndex = 2
        Me.RadioButton2.Text = "Exact match"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(6, 19)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(108, 17)
        Me.RadioButton1.TabIndex = 1
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Item contains text"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(6, 42)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(96, 17)
        Me.CheckBox1.TabIndex = 0
        Me.CheckBox1.Text = "Case-Sensitive"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'ResultCountLbl
        '
        Me.ResultCountLbl.AutoSize = True
        Me.ResultCountLbl.Location = New System.Drawing.Point(11, 178)
        Me.ResultCountLbl.Name = "ResultCountLbl"
        Me.ResultCountLbl.Size = New System.Drawing.Size(0, 13)
        Me.ResultCountLbl.TabIndex = 3
        '
        'ListView2
        '
        Me.ListView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader22})
        Me.ListView2.ContextMenuStrip = Me.AddMemberContextMenu
        Me.ListView2.FullRowSelect = True
        Me.ListView2.HideSelection = False
        Me.ListView2.Location = New System.Drawing.Point(8, 205)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(680, 280)
        Me.ListView2.TabIndex = 4
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Collection"
        Me.ColumnHeader3.Width = 242
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
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Checked = True
        Me.CheckBox3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox3.Location = New System.Drawing.Point(283, 42)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(171, 17)
        Me.CheckBox3.TabIndex = 6
        Me.CheckBox3.Text = "Show results in search window"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Checked = True
        Me.CheckBox4.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox4.Location = New System.Drawing.Point(477, 42)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(83, 17)
        Me.CheckBox4.TabIndex = 7
        Me.CheckBox4.Text = "Clear results"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'AddMemberContextMenu
        '
        Me.AddMemberContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ZuweisenToolStripMenuItem, Me.AlleGeräteMitDieserCollectionToolStripMenuItem, Me.CopySelectedMembershipsToolStripMenuItem1, Me.AddSelectedCollectionToClipboardToolStripMenuItem})
        Me.AddMemberContextMenu.Name = "ContextMenuStrip1"
        Me.AddMemberContextMenu.Size = New System.Drawing.Size(273, 92)
        '
        'ZuweisenToolStripMenuItem
        '
        Me.ZuweisenToolStripMenuItem.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_plus_square_16x16
        Me.ZuweisenToolStripMenuItem.Name = "ZuweisenToolStripMenuItem"
        Me.ZuweisenToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.ZuweisenToolStripMenuItem.Text = "Assign collection to device"
        '
        'AlleGeräteMitDieserCollectionToolStripMenuItem
        '
        Me.AlleGeräteMitDieserCollectionToolStripMenuItem.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_search_16x16
        Me.AlleGeräteMitDieserCollectionToolStripMenuItem.Name = "AlleGeräteMitDieserCollectionToolStripMenuItem"
        Me.AlleGeräteMitDieserCollectionToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.AlleGeräteMitDieserCollectionToolStripMenuItem.Text = "Show all devices with this collection..."
        '
        'CopySelectedMembershipsToolStripMenuItem1
        '
        Me.CopySelectedMembershipsToolStripMenuItem1.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_duplicate_16x16_cleared
        Me.CopySelectedMembershipsToolStripMenuItem1.Name = "CopySelectedMembershipsToolStripMenuItem1"
        Me.CopySelectedMembershipsToolStripMenuItem1.Size = New System.Drawing.Size(272, 22)
        Me.CopySelectedMembershipsToolStripMenuItem1.Text = "Copy selected memberships"
        '
        'AddSelectedCollectionToClipboardToolStripMenuItem
        '
        Me.AddSelectedCollectionToClipboardToolStripMenuItem.Image = Global.SCCMCollectionManagerPlugin.My.Resources.Resources.icon_plus_circle_16x16
        Me.AddSelectedCollectionToClipboardToolStripMenuItem.Name = "AddSelectedCollectionToClipboardToolStripMenuItem"
        Me.AddSelectedCollectionToClipboardToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.AddSelectedCollectionToClipboardToolStripMenuItem.Text = "Add selected collection to clipboard"
        '
        'ListViewSearchFrm
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(700, 497)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.ResultCountLbl)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ListViewSearchFrm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Search item in collection"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.AddMemberContextMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents ResultCountLbl As Label
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents ListView2 As ListView
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents ColumnHeader22 As ColumnHeader
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents AddMemberContextMenu As ContextMenuStrip
    Friend WithEvents ZuweisenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AlleGeräteMitDieserCollectionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopySelectedMembershipsToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents AddSelectedCollectionToClipboardToolStripMenuItem As ToolStripMenuItem
End Class
