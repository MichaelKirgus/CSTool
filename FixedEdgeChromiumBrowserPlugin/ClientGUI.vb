'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports System.Text
Imports CSToolEnvironmentManager
Imports CSToolLogLib.LogSettings
Imports Microsoft.Web.WebView2.Core
Imports Microsoft.Web.WebView2.WinForms

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public EnvManager As New EnvironmentManager
    Public CurrentIPHostname As String = ""

    Public Sub RaiseAction(ByVal IPOrHostname As String, Optional ByVal IsRefresh As Boolean = False)
        CurrentIPHostname = IPOrHostname

        Try
            If _Settings.RaiseActions Then
                If Not IPOrHostname = "" Then
                    If Not _Settings.RaiseActionURL = "" Then
                        If _Settings.RaiseURLRefreshIfHostnameChanged Then
                            Dim newuri As New Uri(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RaiseActionURL))
                            _ParentInstance.CurrentLogInstance.WriteLogEntry("Action: Navigate to " & newuri.AbsolutePath & " - Without authentication", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                            WebView2.Source = newuri
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Navigate to URI: Error.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
        End Try
    End Sub
    Public Sub RefreshGUI()
        ToolStripContainer1.TopToolStripPanelVisible = _Settings.ShowNavigationToolbar
        WebView2.CoreWebView2.Settings.IsScriptEnabled = _Settings.EnableJavaScript

        Try
            If Not _Settings.InitialURL = "" Then
                Dim newuri As New Uri(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.InitialURL))
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Refresh: Navigate to " & newuri.AbsolutePath & " - Without authentication", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                WebView2.Source = newuri
            End If
        Catch ex As Exception
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Navigate to URI: Error.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
        End Try
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsNothing(_Settings) Then
            Dim cproper As New CoreWebView2CreationProperties
            cproper.BrowserExecutableFolder = "MicrosoftWebView2FixedVersionRuntime_64"
            cproper.Language = _Settings.BrowserLanguage

            WebView2.CreationProperties = cproper

            If Not _Settings.InitialTitle = "" Then
                Me.ParentForm.Text = _Settings.InitialTitle
                _ParentInstance.CurrentWindowTitle = _Settings.InitialTitle
            End If

            If _Settings.LoadURLAtStart Then
                RefreshGUI()
            End If
        End If
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) Handles WebView2.NavigationCompleted
        If _Settings.ShowWebsiteTitleInWindowTitle Then
            If e.IsSuccess Then
                ToolStripComboBox1.Text = WebView2.CoreWebView2.Source
                _ParentInstance.CurrentWindowTitle = WebView2.CoreWebView2.DocumentTitle
                ParentForm.Text = WebView2.CoreWebView2.DocumentTitle
            End If
        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click

    End Sub

    Private Sub ToolStripComboBox1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ToolStripComboBox1.KeyDown

    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click

    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click

    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click

    End Sub

    Private Sub ToolStrip1_SizeChanged(sender As Object, e As EventArgs) Handles ToolStrip1.SizeChanged

    End Sub
End Class
