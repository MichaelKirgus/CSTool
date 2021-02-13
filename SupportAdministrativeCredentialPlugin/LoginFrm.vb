'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolEnvironmentManager

Public Class LoginFrm
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public _ParentClient As Client
    Public EnvManager As New EnvironmentManager

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If CheckBox1.Checked Then
            _ParentClient.SaveCredentials(UsernameTextBox.Text, PasswordTextBox.Text)
        End If

        _ParentClient.LoginCredHandle.Username = _ParentClient.CredHandler.ConvertStringInSecureString(UsernameTextBox.Text)
        _ParentClient.LoginCredHandle.Password = _ParentClient.CredHandler.ConvertStringInSecureString(PasswordTextBox.Text)

        _ParentClient.IsUserCredentialSet = True

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        _ParentClient.IsUserCredentialSet = False
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub LoadCredsToGUI()
        Dim results As List(Of String)
        results = _ParentClient.LoadCredentials

        If Not results.Count = 0 Then
            If Not results(0) = "" And Not results(0) = "" Then
                UsernameTextBox.Text = results(0)
                PasswordTextBox.Text = results(1)
            Else
                UsernameTextBox.Text = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.DefaultUsername)
            End If
        Else
            UsernameTextBox.Text = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.DefaultUsername)
        End If
    End Sub

    Private Sub LoginFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCredsToGUI()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If PasswordTextBox.UseSystemPasswordChar Then
            PasswordTextBox.UseSystemPasswordChar = False
        Else
            PasswordTextBox.UseSystemPasswordChar = True
        End If
    End Sub
End Class
