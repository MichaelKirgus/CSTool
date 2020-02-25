'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Windows.Forms
Imports CSToolCryptHelper
Imports CSToolEnvironmentManager

Public Class CustomCredFrm
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public _ParentClient As Client
    Public EnvManager As New EnvironmentManager
    Private Sub CustomCredFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PropertyGrid1.SelectedObject = _Settings
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If ComboBox1.SelectedIndex = 0 Then
                Dim crypthelp As New Base64Helper
                enctext.Text = crypthelp.ToBase64(plaintext.Text)
            End If
            If ComboBox1.SelectedIndex = 1 Then
                Dim md5helper As New MD5HashHelper
                Dim AESHelper As New EncryptDecryptHelper
                Dim MD5Str As String
                MD5Str = md5helper.GetMD5FromFile(Application.ExecutablePath)
                enctext.Text = AESHelper.EncryptTextToBase64(MD5Str, plaintext.Text)
            End If
            If ComboBox1.SelectedIndex = 2 Then
                Dim AESHelper As New EncryptDecryptHelper
                enctext.Text = AESHelper.EncryptTextToBase64(psktxt.Text, plaintext.Text)
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class