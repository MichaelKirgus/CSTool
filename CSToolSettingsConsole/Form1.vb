Imports CSToolSettingsConsoleLib

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim globsettingsfrm As New AppSettingsFrm
        globsettingsfrm.ApplicationSettingsFile = "AppSettings.xml"
        globsettingsfrm.Show()
        Me.Close()
    End Sub
End Class
