'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports System.Text
Imports CSToolEnvironmentManager
Imports WeifenLuo.WinFormsUI.Docking

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public EnvManager As New EnvironmentManager
    Public CurrentIPHostname As String = ""

    Public Sub RaiseAction(ByVal IPOrHostname As String, Optional ByVal IsRefresh As Boolean = False)
        CurrentIPHostname = IPOrHostname

        If _Settings.RaiseActions Then
            If Not IPOrHostname = "" Then
                If Not _Settings.RaiseActionURL = "" Then
                    If _Settings.RaiseURLRefreshIfHostnameChanged Then
                        If _Settings.UseCustomAuthentification Then
                            Dim userName As String = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.UseCustomAuthentificationUsername)
                            Dim password As String = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.UseCustomAuthentificationPassword)
                            Dim hdr As String = "Authorization: Basic " & Convert.ToBase64String(Encoding.ASCII.GetBytes(userName & ":" & password)) & System.Environment.NewLine
                            WebBrowser1.Navigate(String.Format(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RaiseActionURL), userName, password), Nothing, Nothing, hdr)
                        Else
                            WebBrowser1.Navigate(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RaiseActionURL))
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Public Sub RefreshGUI()
        ToolStripContainer1.TopToolStripPanelVisible = _Settings.ShowNavigationToolbar

        If Not _Settings.InitialURL = "" Then
            If _Settings.UseCustomAuthentification Then
                Dim userName As String = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.UseCustomAuthentificationUsername)
                Dim password As String = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.UseCustomAuthentificationPassword)
                Dim hdr As String = "Authorization: Basic " & Convert.ToBase64String(Encoding.ASCII.GetBytes(userName & ":" & password)) & System.Environment.NewLine
                WebBrowser1.Navigate(String.Format(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.InitialURL), userName, password), Nothing, Nothing, hdr)
            Else
                WebBrowser1.Navigate(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.InitialURL))
            End If
        End If
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsNothing(_Settings) Then
            If Not _Settings.InitialTitle = "" Then
                Me.ParentForm.Text = _Settings.InitialTitle
                _ParentInstance.CurrentWindowTitle = _Settings.InitialTitle
            End If

            If _Settings.LoadURLAtStart Then
                RefreshGUI()
            End If
        End If
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        If _Settings.ShowWebsiteTitleInWindowTitle Then
            ToolStripComboBox1.Text = e.Url.ToString
            _ParentInstance.CurrentWindowTitle = WebBrowser1.DocumentTitle
            ParentForm.Text = WebBrowser1.DocumentTitle

            ToolStripButton2.Enabled = WebBrowser1.CanGoBack
            ToolStripButton3.Enabled = WebBrowser1.CanGoForward
        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If Not _Settings.InitialURL = "" Then
            WebBrowser1.Navigate(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.InitialURL))
        End If
    End Sub

    Private Sub ToolStripComboBox1_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles ToolStripComboBox1.KeyDown
        If e.KeyCode = 13 Then
            If Not ToolStripComboBox1.Text = "" Then
                WebBrowser1.Navigate(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, ToolStripComboBox1.Text))

                e.Handled = True
                e.SuppressKeyPress = True
            End If
        End If
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        WebBrowser1.Refresh()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        If WebBrowser1.CanGoBack Then
            WebBrowser1.GoBack()
        End If
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        If WebBrowser1.CanGoForward Then
            WebBrowser1.GoForward()
        End If
    End Sub
    Private Sub ToolStrip1_SizeChanged(sender As Object, e As EventArgs) Handles ToolStrip1.SizeChanged
        Dim newsize As New Size(ToolStripContainer1.Width - 130, ToolStripComboBox1.Size.Height)
        ToolStripComboBox1.Size = newsize
        ToolStrip1.Hide()
        ToolStrip1.Show()
    End Sub
End Class
