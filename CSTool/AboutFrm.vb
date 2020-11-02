'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Public Class AboutFrm
    Private Sub AboutFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            AppVersionLbl.Text = My.Application.Info.Version.ToString

            TextBox1.Text = IO.File.ReadAllText("Licenses\License_MainApp.txt")
            TextBox2.Text = IO.File.ReadAllText("Licenses\License_DockPanelSuite.txt")
            TextBox3.Text = IO.File.ReadAllText("Licenses\License_Chromium.txt")
            TextBox4.Text = IO.File.ReadAllText("Licenses\License_AdysTechCredentialManager.txt")
            TextBox5.Text = IO.File.ReadAllText("Licenses\License_CefSharp.txt")
            TextBox6.Text = IO.File.ReadAllText("Licenses\License_Gecko.txt")
            TextBox7.Text = IO.File.ReadAllText("Licenses\License_Heroicons.txt")
            TextBox8.Text = IO.File.ReadAllText("Licenses\License_SSHNET.txt")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim giturl As New Process
        giturl.StartInfo.FileName = "https://github.com/MichaelKirgus/CSTool"
        giturl.Start()
    End Sub
End Class