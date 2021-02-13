'Copyright (C) 2019-2021 Michael Kirgus
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
    Public WebView2Init As Boolean = False

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

        'Only navigate to URL if the WebView2Core ist ready. If not, a refresh will be triggered.
        If Not IsNothing(WebView2.CoreWebView2) Then
            WebView2.CoreWebView2.Settings.IsScriptEnabled = _Settings.EnableJavaScript

            Try
                If Not _Settings.InitialURL = "" Then
                    Dim newuri As New Uri(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.InitialURL))
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("Refresh: Navigate to " & newuri.AbsolutePath & " - Without authentication", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    WebView2.CoreWebView2.Navigate(newuri.ToString)
                End If
            Catch ex As Exception
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Navigate to URI: Error.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
            End Try
        End If
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitWebBrowserPluginSettings()
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

    Private Sub WebBrowser1_InitCompleted(sender As Object, e As Object) Handles WebView2.CoreWebView2InitializationCompleted
        InitWebBrowserPluginSettings()
        _ParentInstance.CurrentLogInstance.WriteLogEntry("Finished WebView2 initialization", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

        _ParentInstance.CurrentLogInstance.WriteLogEntry("Set GUI settings...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
        WebView2.CoreWebView2.Settings.IsScriptEnabled = _Settings.EnableJavaScript
        WebView2.CoreWebView2.Settings.AreDefaultContextMenusEnabled = _Settings.ContextMenuEnabled
        WebView2.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = _Settings.DefaultScriptsDialogsEnabled
        WebView2.CoreWebView2.Settings.AreDevToolsEnabled = _Settings.DeveloperToolsEnabled
        WebView2.CoreWebView2.Settings.AreHostObjectsAllowed = _Settings.HostObjectsEnabled
        WebView2.CoreWebView2.Settings.IsBuiltInErrorPageEnabled = _Settings.BuildInErrorPagesEnabled
        WebView2.CoreWebView2.Settings.IsStatusBarEnabled = _Settings.StatusBarEnabled
        WebView2.CoreWebView2.Settings.IsWebMessageEnabled = _Settings.WebMessageEnabled
        WebView2.CoreWebView2.Settings.IsZoomControlEnabled = _Settings.ZoomEnabled
        _ParentInstance.CurrentLogInstance.WriteLogEntry("GUI settings applied.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
    End Sub

    Public Sub InitWebBrowserPluginSettings()
        If Not IsNothing(_Settings) Then
            _ParentInstance.CurrentLogInstance.WriteLogEntry("Start initialization of WebView2...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            If WebView2Init = False Then
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Create WebView2Core objects...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                Dim cproper As New CoreWebView2CreationProperties
                Dim cenviron As New CoreWebView2EnvironmentOptions
                Dim cenvinstance As CoreWebView2Environment
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Set WebView2Core language...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                cproper.BrowserExecutableFolder = _Settings.BrowserExecutableFolder
                cproper.Language = _Settings.BrowserLanguage
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Set WebView2Core user data directory...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                cproper.UserDataFolder = _Settings.UserDataFolder
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Set WebView2Core SSO settings...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                cenviron.AllowSingleSignOnUsingOSPrimaryAccount = _Settings.AllowSingleSignOnUsingOSPrimaryAccount

                _ParentInstance.CurrentLogInstance.WriteLogEntry("Set WebView2Core creation properties...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                WebView2.CreationProperties = cproper

                _ParentInstance.CurrentLogInstance.WriteLogEntry("Create async WebView2Core environment...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                Dim environtask As Task(Of CoreWebView2Environment)
                environtask = CoreWebView2Environment.CreateAsync("", "", cenviron)
                environtask.Wait()
                cenvinstance = environtask.Result
                _ParentInstance.CurrentLogInstance.WriteLogEntry("WebView2Core environment created.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

                Dim inittask As Task
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Create async WebView2 instance...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                inittask = WebView2.EnsureCoreWebView2Async(cenvinstance)

                _ParentInstance.CurrentLogInstance.WriteLogEntry("WebView2 instance created.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

                WebView2Init = True
            End If

            If Not _Settings.InitialTitle = "" Then
                Me.ParentForm.Text = _Settings.InitialTitle
                _ParentInstance.CurrentWindowTitle = _Settings.InitialTitle
            End If

            If _Settings.LoadURLAtStart Then
                RefreshGUI()
            End If

            SetSizeToWebView2()
        End If
    End Sub

    Public Sub SetSizeToWebView2()
        WebView2.Dock = Windows.Forms.DockStyle.None
        WebView2.Size = New Size(ToolStripContainer1.ContentPanel.Width, ToolStripContainer1.ContentPanel.Height)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If Not _Settings.InitialURL = "" Then
            Try
                WebView2.Source = New Uri(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.InitialURL))
            Catch ex As Exception
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Navigate to URI: Error.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
            End Try
        End If
    End Sub

    Private Sub ToolStripComboBox1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ToolStripComboBox1.KeyDown
        If e.KeyCode = 13 Then
            If Not ToolStripComboBox1.Text = "" Then
                Try
                    WebView2.Source = New Uri(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, ToolStripComboBox1.Text))

                    e.Handled = True
                    e.SuppressKeyPress = True
                Catch ex As Exception
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("Navigate to URI: Error.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
                End Try
            End If
        End If
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        WebView2.Reload()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        WebView2.GoBack()
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        WebView2.GoForward()
    End Sub

    Private Sub ToolStrip1_SizeChanged(sender As Object, e As EventArgs) Handles ToolStrip1.SizeChanged
        Dim newsize As New Size(ToolStripContainer1.Width - 130, ToolStripComboBox1.Size.Height)
        ToolStripComboBox1.Size = newsize
        ToolStrip1.Hide()
        ToolStrip1.Show()
    End Sub

    Private Sub ClientGUI_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        SetSizeToWebView2()
    End Sub

    Private Sub ToolStripContainer1_ContentPanel_Resize(sender As Object, e As EventArgs) Handles ToolStripContainer1.ContentPanel.Resize
        SetSizeToWebView2()
    End Sub
End Class
