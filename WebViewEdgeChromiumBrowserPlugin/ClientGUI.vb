Imports System.Text
Imports CSToolEnvironmentManager

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public EnvManager As New EnvironmentManager
    Public CurrentIPHostname As String = ""

    Public Sub RaiseAction(ByVal IPOrHostname As String, Optional ByVal IsRefresh As Boolean = False)
        CurrentIPHostname = IPOrHostname

        If Not IPOrHostname = "" Then
            If Not _Settings.RaiseActionURL = "" Then
                If _Settings.RaiseURLRefreshIfHostnameChanged Then
                    If _Settings.UseCustomAuthentification Then
                        Dim userName As String = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.UseCustomAuthentificationUsername)
                        Dim password As String = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.UseCustomAuthentificationPassword)
                        Dim hdr As String = "Authorization: Basic " & Convert.ToBase64String(Encoding.ASCII.GetBytes(userName & ":" & password)) & System.Environment.NewLine
                        Dim newuri As New Uri(String.Format(_Settings.RaiseActionURL, userName, password))
                        'WebView1.Navigate(newuri)
                    Else
                        Dim newuri As New Uri(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RaiseActionURL))
                        'WebView1.Navigate(newuri)
                    End If
                End If
            End If
        End If
    End Sub
    Public Sub RefreshGUI()
        ToolStripContainer1.TopToolStripPanelVisible = _Settings.ShowNavigationToolbar

        If _Settings.UseCustomAuthentification Then
            Dim userName As String = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.UseCustomAuthentificationUsername)
            Dim password As String = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.UseCustomAuthentificationPassword)
            Dim hdr As String = "Authorization: Basic " & Convert.ToBase64String(Encoding.ASCII.GetBytes(userName & ":" & password)) & System.Environment.NewLine
            Dim newuri As New Uri(String.Format(_Settings.InitialURL, userName, password))
            'WebView1.Navigate(newuri)
        Else
            If Not _Settings.InitialURL = "" Then
                Dim newuri As New Uri(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.InitialURL))
                ' WebView1.Navigate(newuri)
            End If
        End If
    End Sub
End Class
