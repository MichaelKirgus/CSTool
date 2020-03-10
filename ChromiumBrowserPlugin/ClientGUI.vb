'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports CefSharp
Imports CefSharp.WinForms
Imports CSToolEnvironmentManager

Public Class ClientGUI
    Public _Settings As Settings
    Public settings As New CefSettings()
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public EnvManager As New EnvironmentManager
    Public CurrentIPHostname As String = ""
    Private FirstBrowserLoad As Boolean = True

    Private WithEvents browser As ChromiumWebBrowser
    Public Sub New()
        Try
            InitializeComponent()
            InitChromium()
            browser = New ChromiumWebBrowser("")
            Me.ToolStripContainer1.ContentPanel.Controls.Add(browser)
            browser.Dock = Windows.Forms.DockStyle.Fill
        Catch ex As Exception
        End Try
    End Sub

    Delegate Sub SetFormTextCallback(ByVal FormCtl As Form, ByVal text As String)
    Delegate Sub SetComboBoxTextCallback(ByVal Ctl As ToolStripComboBox, ByVal text As String)
    Delegate Sub SetToolStripVisibleCallback(ByVal Ctl As ToolStripContainer, ByVal visible As Boolean)
    Delegate Sub SetToolStripButtonEnabledCallback(ByVal Ctl As ToolStripButton, ByVal enabled As Boolean)

    Public Sub InitChromium()
        Try
            If Not CefSharp.Cef.IsInitialized Then
                If Not IsNothing(_Settings) Then
                    If _Settings.ChromiumExtensions.Count = 0 Then
                        For index = 0 To _Settings.ChromiumExtensions.Count - 1
                            settings.RegisterExtension(New V8Extension(_Settings.ChromiumExtensions(index).Name, _Settings.ChromiumExtensions(index).JavascriptCode))
                        Next
                    End If
                    settings.IgnoreCertificateErrors = _Settings.IgnoreCertificateErrors
                    If Not _Settings.CachePath = "" Then
                        settings.CachePath = _Settings.CachePath
                    End If
                    settings.PersistSessionCookies = _Settings.PersistSessionCookies
                    settings.UserAgent = _Settings.UserAgent
                End If

                CefSharp.Cef.Initialize(settings)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SetText(ByVal FormCtl As Form, ByVal text As String)
        Try
            If FormCtl.InvokeRequired Then
                Dim d As SetFormTextCallback = New SetFormTextCallback(AddressOf SetText)
                Me.Invoke(d, New Object() {FormCtl, text})
            Else
                FormCtl.Text = text
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SetComboBoxText(ByVal Ctl As ToolStripComboBox, ByVal text As String)
        Try
            If Ctl.Owner.InvokeRequired Then
                Dim d As SetComboBoxTextCallback = New SetComboBoxTextCallback(AddressOf SetComboBoxText)
                Me.Invoke(d, New Object() {Ctl, text})
            Else
                Ctl.Text = text
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SetToolStripVisible(ByVal Ctl As ToolStripContainer, ByVal visible As Boolean)
        Try
            If Ctl.InvokeRequired Then
                Dim d As SetToolStripVisibleCallback = New SetToolStripVisibleCallback(AddressOf SetToolStripVisible)
                Me.Invoke(d, New Object() {Ctl, visible})
            Else
                Ctl.TopToolStripPanelVisible = visible
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SetToolStripButtonEnabled(ByVal Ctl As ToolStripButton, ByVal enabled As Boolean)
        Try
            If Ctl.Owner.InvokeRequired Then
                Dim d As SetToolStripButtonEnabledCallback = New SetToolStripButtonEnabledCallback(AddressOf SetToolStripButtonEnabled)
                Me.Invoke(d, New Object() {Ctl, enabled})
            Else
                Ctl.Enabled = enabled
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub RaiseAction(ByVal IPOrHostname As String, Optional ByVal IsRefresh As Boolean = False)
        CurrentIPHostname = IPOrHostname

        If _Settings.RaiseActions Then
            If Not IPOrHostname = "" Then
                If Not _Settings.RaiseActionURL = "" Then
                    If _Settings.RaiseURLRefreshIfHostnameChanged Then
                        InitChromium()

                        If _Settings.UseCustomAuthentification Then
                            browser.RequestHandler = New CustomRequestHandler(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.UseCustomAuthentificationUsername),
                                                                              EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.UseCustomAuthentificationPassword))
                            browser.Load(_Settings.RaiseActionURL)
                        Else

                            browser.Load(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RaiseActionURL))
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Public Sub RefreshGUI()
        SetToolStripVisible(ToolStripContainer1, _Settings.ShowNavigationToolbar)

        If Not _Settings.InitialURL = "" Then
            If _Settings.UseCustomAuthentification Then
                If browser.IsBrowserInitialized Then
                    InitChromium()

                    If browser.GetBrowser.MainFrame.IsValid Then
                        browser.RequestHandler = New CustomRequestHandler(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.UseCustomAuthentificationUsername),
                                                                              EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.UseCustomAuthentificationPassword))

                        browser.Load(_Settings.InitialURL)
                    Else
                        HandleResize()
                    End If
                End If
            Else
                If browser.IsBrowserInitialized Then
                    InitChromium()

                    If browser.GetBrowser.MainFrame.IsValid Then
                        browser.Load(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.InitialURL))
                    Else
                        HandleResize()
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub HandleResize()
        Me.ToolStripContainer1.ContentPanel.Controls.Remove(browser)
        browser = New ChromiumWebBrowser(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RaiseActionURL))
        Me.ToolStripContainer1.ContentPanel.Controls.Add(browser)
        browser.Dock = Windows.Forms.DockStyle.Fill
    End Sub

    Private Sub OnBrowserTitleChanged(ByVal sender As Object, ByVal args As TitleChangedEventArgs) Handles browser.TitleChanged
        If _Settings.ShowWebsiteTitleInWindowTitle Then
            _ParentInstance.CurrentWindowTitle = args.Title
            SetText(ParentForm, args.Title)
        End If
    End Sub

    Private Sub AfterBrowserSiteLoaded(ByVal sender As Object, ByVal args As FrameLoadEndEventArgs) Handles browser.FrameLoadEnd
        If Not IsNothing(browser) Then
            SetToolStripButtonEnabled(ToolStripButton2, browser.CanGoBack)
            SetToolStripButtonEnabled(ToolStripButton3, browser.CanGoForward)
        End If
    End Sub

    Private Sub OnBrowserURLChanged(ByVal sender As Object, ByVal args As AddressChangedEventArgs) Handles browser.AddressChanged
        If Not IsNothing(browser) Then
            SetComboBoxText(ToolStripComboBox1, args.Address)
            SetToolStripButtonEnabled(ToolStripButton2, browser.CanGoBack)
            SetToolStripButtonEnabled(ToolStripButton3, browser.CanGoForward)
        End If
    End Sub

    Private Sub OnBrowserInit() Handles browser.IsBrowserInitializedChanged
        If _Settings.LoadURLAtStart And FirstBrowserLoad Then
            RefreshGUI()
        End If
        FirstBrowserLoad = False
    End Sub

    Private Sub ClientGUI_Validated(sender As Object, e As EventArgs) Handles MyBase.Validated
        If Not IsNothing(browser) Then
            If browser.IsBrowserInitialized Then
                If Not browser.GetBrowser.MainFrame.IsValid Then
                    HandleResize()
                End If
            End If
        End If
    End Sub

    Private Sub ClientGUI_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        If Not IsNothing(browser) Then
            If browser.IsBrowserInitialized Then
                If Not browser.GetBrowser.MainFrame.IsValid Then
                    HandleResize()
                End If
            End If
        End If
    End Sub

    Private Sub ToolStripComboBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ToolStripComboBox1.KeyDown
        If e.KeyCode = 13 Then
            If Not IsNothing(browser) Then
                If Not ToolStripComboBox1.Text = "" Then
                    browser.Load(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, ToolStripComboBox1.Text))

                    e.Handled = True
                    e.SuppressKeyPress = True
                End If
            End If
        End If
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        If Not IsNothing(browser) Then
            browser.Reload
        End If
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        If Not IsNothing(browser) Then
            If browser.CanGoBack Then
                browser.Back
            End If
        End If
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        If Not IsNothing(browser) Then
            If browser.CanGoForward Then
                browser.Forward
            End If
        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If Not IsNothing(browser) Then
            If Not _Settings.InitialURL = "" Then
                browser.Load(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.InitialURL))
            End If
        End If
    End Sub

    Private Sub ToolStrip1_SizeChanged(sender As Object, e As EventArgs) Handles ToolStrip1.SizeChanged
        Dim newsize As New Size(ToolStripContainer1.Width - 130, ToolStripComboBox1.Size.Height)
        ToolStripComboBox1.Size = newsize
        ToolStrip1.Hide()
        ToolStrip1.Show()
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsNothing(_Settings) Then
            If Not _Settings.InitialTitle = "" Then
                Me.ParentForm.Text = _Settings.InitialTitle
                _ParentInstance.CurrentWindowTitle = _Settings.InitialTitle
            End If
        End If
    End Sub
End Class
