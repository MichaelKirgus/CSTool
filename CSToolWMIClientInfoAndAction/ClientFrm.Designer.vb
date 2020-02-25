<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClientFrm
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ClientFrm))
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.hddssdgroup = New System.Windows.Forms.GroupBox()
        Me.UsedSpaceD = New System.Windows.Forms.ProgressBar()
        Me.LblD = New System.Windows.Forms.Label()
        Me.LblC = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.UsedSpaceC = New System.Windows.Forms.ProgressBar()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Clientstate = New System.Windows.Forms.TextBox()
        Me.actionpan = New System.Windows.Forms.Panel()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.hddssdgroup.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.actionpan.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.TextBox8)
        Me.GroupBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.Location = New System.Drawing.Point(2, 54)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(171, 46)
        Me.GroupBox7.TabIndex = 25
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Letzter Start"
        '
        'TextBox8
        '
        Me.TextBox8.Location = New System.Drawing.Point(6, 18)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.ReadOnly = True
        Me.TextBox8.Size = New System.Drawing.Size(159, 22)
        Me.TextBox8.TabIndex = 2
        Me.TextBox8.Text = "Keine Daten"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.TextBox6)
        Me.GroupBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.Location = New System.Drawing.Point(400, 158)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(75, 46)
        Me.GroupBox6.TabIndex = 24
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "RAM"
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(6, 18)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.ReadOnly = True
        Me.TextBox6.Size = New System.Drawing.Size(63, 22)
        Me.TextBox6.TabIndex = 8
        Me.TextBox6.Text = "Keine Daten"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.TextBox7)
        Me.GroupBox5.Controls.Add(Me.TextBox5)
        Me.GroupBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(2, 158)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(392, 46)
        Me.GroupBox5.TabIndex = 23
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Betriebssystem"
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(332, 18)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.ReadOnly = True
        Me.TextBox7.Size = New System.Drawing.Size(54, 22)
        Me.TextBox7.TabIndex = 7
        Me.TextBox7.Text = "Keine Daten"
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(6, 18)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.TextBox5.Size = New System.Drawing.Size(320, 22)
        Me.TextBox5.TabIndex = 6
        Me.TextBox5.Text = "Keine Daten"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.TextBox4)
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(179, 106)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(296, 46)
        Me.GroupBox4.TabIndex = 22
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "CPU"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(6, 18)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(284, 22)
        Me.TextBox4.TabIndex = 5
        Me.TextBox4.Text = "Keine Daten"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TextBox3)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(2, 106)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(171, 46)
        Me.GroupBox3.TabIndex = 21
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Modell"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(6, 18)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(159, 22)
        Me.TextBox3.TabIndex = 4
        Me.TextBox3.Text = "Keine Daten"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TextBox2)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(179, 54)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(157, 46)
        Me.GroupBox2.TabIndex = 20
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Hersteller"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(6, 18)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(145, 22)
        Me.TextBox2.TabIndex = 3
        Me.TextBox2.Text = "Keine Daten"
        '
        'hddssdgroup
        '
        Me.hddssdgroup.Controls.Add(Me.UsedSpaceD)
        Me.hddssdgroup.Controls.Add(Me.LblD)
        Me.hddssdgroup.Controls.Add(Me.LblC)
        Me.hddssdgroup.Controls.Add(Me.Label3)
        Me.hddssdgroup.Controls.Add(Me.Label2)
        Me.hddssdgroup.Controls.Add(Me.UsedSpaceC)
        Me.hddssdgroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hddssdgroup.Location = New System.Drawing.Point(342, 2)
        Me.hddssdgroup.Name = "hddssdgroup"
        Me.hddssdgroup.Size = New System.Drawing.Size(133, 98)
        Me.hddssdgroup.TabIndex = 19
        Me.hddssdgroup.TabStop = False
        Me.hddssdgroup.Text = "HDD/SSD"
        '
        'UsedSpaceD
        '
        Me.UsedSpaceD.Location = New System.Drawing.Point(26, 57)
        Me.UsedSpaceD.MarqueeAnimationSpeed = 25
        Me.UsedSpaceD.Name = "UsedSpaceD"
        Me.UsedSpaceD.Size = New System.Drawing.Size(101, 12)
        Me.UsedSpaceD.TabIndex = 5
        '
        'LblD
        '
        Me.LblD.AutoEllipsis = True
        Me.LblD.AutoSize = True
        Me.LblD.Location = New System.Drawing.Point(5, 70)
        Me.LblD.Name = "LblD"
        Me.LblD.Size = New System.Drawing.Size(81, 16)
        Me.LblD.TabIndex = 3
        Me.LblD.Text = "Keine Daten"
        '
        'LblC
        '
        Me.LblC.AutoEllipsis = True
        Me.LblC.AutoSize = True
        Me.LblC.Location = New System.Drawing.Point(6, 32)
        Me.LblC.Name = "LblC"
        Me.LblC.Size = New System.Drawing.Size(81, 16)
        Me.LblC.TabIndex = 2
        Me.LblC.Text = "Keine Daten"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 16)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "D:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(20, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "C:"
        '
        'UsedSpaceC
        '
        Me.UsedSpaceC.Location = New System.Drawing.Point(26, 19)
        Me.UsedSpaceC.MarqueeAnimationSpeed = 25
        Me.UsedSpaceC.Name = "UsedSpaceC"
        Me.UsedSpaceC.Size = New System.Drawing.Size(101, 12)
        Me.UsedSpaceC.TabIndex = 4
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Clientstate)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(334, 46)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Aktueller Benutzer"
        '
        'Clientstate
        '
        Me.Clientstate.Location = New System.Drawing.Point(6, 18)
        Me.Clientstate.Name = "Clientstate"
        Me.Clientstate.ReadOnly = True
        Me.Clientstate.Size = New System.Drawing.Size(322, 22)
        Me.Clientstate.TabIndex = 1
        Me.Clientstate.Text = "Keine Daten"
        '
        'actionpan
        '
        Me.actionpan.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.actionpan.Controls.Add(Me.Button9)
        Me.actionpan.Controls.Add(Me.Button8)
        Me.actionpan.Controls.Add(Me.Button7)
        Me.actionpan.Controls.Add(Me.Button6)
        Me.actionpan.Controls.Add(Me.Button1)
        Me.actionpan.Controls.Add(Me.Button2)
        Me.actionpan.Controls.Add(Me.Button5)
        Me.actionpan.Controls.Add(Me.Button3)
        Me.actionpan.Controls.Add(Me.Button4)
        Me.actionpan.Location = New System.Drawing.Point(1, 205)
        Me.actionpan.Name = "actionpan"
        Me.actionpan.Size = New System.Drawing.Size(478, 50)
        Me.actionpan.TabIndex = 26
        '
        'Button9
        '
        Me.Button9.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button9.Location = New System.Drawing.Point(308, 3)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(55, 41)
        Me.Button9.TabIndex = 10
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button8.Location = New System.Drawing.Point(369, 24)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(46, 20)
        Me.Button8.TabIndex = 9
        Me.Button8.Text = "D:"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button7.Location = New System.Drawing.Point(369, 3)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(46, 20)
        Me.Button7.TabIndex = 1
        Me.Button7.Text = "C:"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button6.Location = New System.Drawing.Point(421, 3)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(55, 41)
        Me.Button6.TabIndex = 8
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button1.Location = New System.Drawing.Point(3, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(55, 41)
        Me.Button1.TabIndex = 2
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button2.Location = New System.Drawing.Point(64, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(55, 41)
        Me.Button2.TabIndex = 3
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button5.Location = New System.Drawing.Point(247, 3)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(55, 41)
        Me.Button5.TabIndex = 7
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button3.Location = New System.Drawing.Point(125, 3)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(55, 41)
        Me.Button3.TabIndex = 4
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button4.Location = New System.Drawing.Point(186, 3)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(55, 41)
        Me.Button4.TabIndex = 5
        Me.Button4.UseVisualStyleBackColor = True
        '
        'ClientFrm
        '
        Me.ClientSize = New System.Drawing.Size(481, 258)
        Me.Controls.Add(Me.actionpan)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.hddssdgroup)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ClientFrm"
        Me.Text = "WMI Client Info"
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.hddssdgroup.ResumeLayout(False)
        Me.hddssdgroup.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.actionpan.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox7 As Windows.Forms.GroupBox
    Friend WithEvents TextBox8 As Windows.Forms.TextBox
    Friend WithEvents GroupBox6 As Windows.Forms.GroupBox
    Friend WithEvents TextBox6 As Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As Windows.Forms.GroupBox
    Friend WithEvents TextBox7 As Windows.Forms.TextBox
    Friend WithEvents TextBox5 As Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents TextBox4 As Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents TextBox3 As Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents TextBox2 As Windows.Forms.TextBox
    Friend WithEvents hddssdgroup As Windows.Forms.GroupBox
    Friend WithEvents UsedSpaceD As Windows.Forms.ProgressBar
    Friend WithEvents LblD As Windows.Forms.Label
    Friend WithEvents LblC As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents UsedSpaceC As Windows.Forms.ProgressBar
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents Clientstate As Windows.Forms.TextBox
    Friend WithEvents actionpan As Windows.Forms.Panel
    Friend WithEvents Button9 As Windows.Forms.Button
    Friend WithEvents Button8 As Windows.Forms.Button
    Friend WithEvents Button7 As Windows.Forms.Button
    Friend WithEvents Button6 As Windows.Forms.Button
    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents Button2 As Windows.Forms.Button
    Friend WithEvents Button5 As Windows.Forms.Button
    Friend WithEvents Button3 As Windows.Forms.Button
    Friend WithEvents Button4 As Windows.Forms.Button
End Class
