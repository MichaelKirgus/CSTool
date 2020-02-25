'Copyright (C) 2019-2020 Michael Kirgus
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
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.CustomItemsContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RunTaskAsync = New System.ComponentModel.BackgroundWorker()
        Me.autofill = New System.Windows.Forms.Timer(Me.components)
        Me.RaiseCustomActionAsync = New System.ComponentModel.BackgroundWorker()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.Button2)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button1)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button3)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button4)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button6)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button9)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button5)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(430, 49)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'Button2
        '
        Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button2.Image = Global.WMIClientActionsPlugin.My.Resources.Resources.system_shutdown_6
        Me.Button2.Location = New System.Drawing.Point(3, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(55, 41)
        Me.Button2.TabIndex = 12
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button1.Image = Global.WMIClientActionsPlugin.My.Resources.Resources.system_reboot_2
        Me.Button1.Location = New System.Drawing.Point(64, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(55, 41)
        Me.Button1.TabIndex = 11
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button3.Image = Global.WMIClientActionsPlugin.My.Resources.Resources.system_switch_user_3
        Me.Button3.Location = New System.Drawing.Point(125, 3)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(55, 41)
        Me.Button3.TabIndex = 13
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button4.Image = Global.WMIClientActionsPlugin.My.Resources.Resources.system_suspend_hibernate
        Me.Button4.Location = New System.Drawing.Point(186, 3)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(55, 41)
        Me.Button4.TabIndex = 14
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button6.Image = Global.WMIClientActionsPlugin.My.Resources.Resources.add_to_desktop
        Me.Button6.Location = New System.Drawing.Point(247, 3)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(55, 41)
        Me.Button6.TabIndex = 16
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button9.Image = Global.WMIClientActionsPlugin.My.Resources.Resources.ultravnc_png
        Me.Button9.Location = New System.Drawing.Point(308, 3)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(55, 41)
        Me.Button9.TabIndex = 17
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button5.ContextMenuStrip = Me.CustomItemsContext
        Me.Button5.Image = Global.WMIClientActionsPlugin.My.Resources.Resources.games_solve
        Me.Button5.Location = New System.Drawing.Point(369, 3)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(55, 41)
        Me.Button5.TabIndex = 15
        Me.Button5.UseVisualStyleBackColor = True
        '
        'CustomItemsContext
        '
        Me.CustomItemsContext.Name = "CustomItemsContext"
        Me.CustomItemsContext.Size = New System.Drawing.Size(61, 4)
        '
        'RunTaskAsync
        '
        Me.RunTaskAsync.WorkerReportsProgress = True
        Me.RunTaskAsync.WorkerSupportsCancellation = True
        '
        'autofill
        '
        Me.autofill.Interval = 200
        '
        'RaiseCustomActionAsync
        '
        Me.RaiseCustomActionAsync.WorkerReportsProgress = True
        Me.RaiseCustomActionAsync.WorkerSupportsCancellation = True
        '
        'ClientGUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Name = "ClientGUI"
        Me.Size = New System.Drawing.Size(430, 49)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FlowLayoutPanel1 As Windows.Forms.FlowLayoutPanel
    Friend WithEvents Button2 As Windows.Forms.Button
    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents Button3 As Windows.Forms.Button
    Friend WithEvents Button4 As Windows.Forms.Button
    Friend WithEvents Button6 As Windows.Forms.Button
    Friend WithEvents Button9 As Windows.Forms.Button
    Friend WithEvents Button5 As Windows.Forms.Button
    Friend WithEvents RunTaskAsync As ComponentModel.BackgroundWorker
    Friend WithEvents autofill As Windows.Forms.Timer
    Friend WithEvents CustomItemsContext As Windows.Forms.ContextMenuStrip
    Friend WithEvents RaiseCustomActionAsync As ComponentModel.BackgroundWorker
End Class
