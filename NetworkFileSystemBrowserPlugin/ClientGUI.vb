'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolEnvironmentManager

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public EnvManager As New EnvironmentManager

    Public RemoteBrowser1 As RemoteFileBrowserCtl = Nothing
    Public RemoteBrowser2 As RemoteFileBrowserCtl = Nothing

    Public Sub RaiseAction(ByVal IPOrHostname As String)
        If Not IPOrHostname = "" Then
            If Not _Settings.InitialDirectoryLeftBrowser1 = "" Then
                RemoteBrowser1.ShareURI = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RaiseActionDirectoryLeftBrowser1)
                RemoteBrowser1.ToolStripComboBox1.Text = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RaiseActionDirectoryLeftBrowser1)
                RemoteBrowser1.LoadFolders()
            End If
            If Not _Settings.InitialDirectoryRightBrowser2 = "" Then
                RemoteBrowser2.ShareURI = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RaiseActionDirectoryRightBrowser2)
                RemoteBrowser2.ToolStripComboBox1.Text = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RaiseActionDirectoryRightBrowser2)
                RemoteBrowser2.LoadFolders()
            End If
        End If
    End Sub
    Public Sub RefreshGUI(Optional ByVal Init As Boolean = False)
        RemoteBrowser1.LoadFileInfo = _Settings.LeftBrowser1LoadFileInfo
        RemoteBrowser1.LoadDirInfo = _Settings.LeftBrowser1LoadDirectoryInfo
        RemoteBrowser1.AutoSizeColumns = _Settings.LeftBrowser1AutoSizeColumns
        RemoteBrowser1.ShareAuthentification = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.LeftBrowser1AuthType)
        RemoteBrowser1.ShareDomain = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.LeftBrowser1Domain)
        RemoteBrowser1.ShareUsername = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.LeftBrowser1Username)
        RemoteBrowser1.SharePassword = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.LeftBrowser1Password)

        RemoteBrowser2.LoadFileInfo = _Settings.RightBrowser2LoadFileInfo
        RemoteBrowser2.LoadDirInfo = _Settings.RightBrowser2LoadDirectoryInfo
        RemoteBrowser2.AutoSizeColumns = _Settings.RightBrowser2AutoSizeColumns
        RemoteBrowser2.ShareAuthentification = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RightBrowser2AuthType)
        RemoteBrowser2.ShareDomain = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RightBrowser2Domain)
        RemoteBrowser2.ShareUsername = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RightBrowser2Username)
        RemoteBrowser2.SharePassword = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RightBrowser2Password)

        If Init Then
            If Not _Settings.InitialDirectoryLeftBrowser1 = "" Then
                RemoteBrowser1.ShareURI = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.InitialDirectoryLeftBrowser1)
                RemoteBrowser1.ToolStripComboBox1.Text = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.InitialDirectoryLeftBrowser1)
                RemoteBrowser1.LoadFolders()
            End If
            If Not _Settings.InitialDirectoryRightBrowser2 = "" Then
                RemoteBrowser2.ShareURI = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.InitialDirectoryRightBrowser2)
                RemoteBrowser2.ToolStripComboBox1.Text = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.InitialDirectoryRightBrowser2)
                RemoteBrowser2.LoadFolders()
            End If
        End If
        If Not _Settings.InitialTitle = "" Then
            Me.ParentForm.Text = _Settings.InitialTitle
            _ParentInstance.CurrentWindowTitle = _Settings.InitialTitle
        End If
        Select Case _Settings.ViewStyle
            Case Settings.ViewStyleEnum.ShowAllBrowsers
                SplitContainer1.Panel1Collapsed = False
                SplitContainer1.Panel2Collapsed = False
            Case Settings.ViewStyleEnum.ShowOnlyLeftBrowser
                SplitContainer1.Panel1Collapsed = False
                SplitContainer1.Panel2Collapsed = True
            Case Settings.ViewStyleEnum.ShowOnlyRightBrowser
                SplitContainer1.Panel1Collapsed = True
                SplitContainer1.Panel2Collapsed = False
        End Select
        SplitContainer1.Orientation = _Settings.SplitViewStyle
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RemoteBrowser1 = New RemoteFileBrowserCtl
        RemoteBrowser2 = New RemoteFileBrowserCtl

        SplitContainer1.Panel1.Controls.Add(RemoteBrowser1)
        SplitContainer1.Panel2.Controls.Add(RemoteBrowser2)

        RemoteBrowser1.Dock = Windows.Forms.DockStyle.Fill
        RemoteBrowser2.Dock = Windows.Forms.DockStyle.Fill

        RemoteBrowser1._parentCtl = Me
        RemoteBrowser2._parentCtl = Me

        RefreshGUI(True)
    End Sub
End Class
