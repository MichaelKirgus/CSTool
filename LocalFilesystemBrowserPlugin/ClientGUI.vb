'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports CSToolEnvironmentManager

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public EnvManager As New EnvironmentManager

    Public Function GetParentDir(ByVal CurrentDir As String) As String
        Try
            Dim dirinf As New IO.DirectoryInfo(CurrentDir)
            If Not dirinf.Parent.FullName = CurrentDir Then
                Return dirinf.Parent.FullName
            End If

            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub RaiseAction(ByVal IPOrHostname As String)
        If _Settings.RaiseActionsLeftBrowser1 Then
            FileBrowser1.Navigate(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RaiseActionDirectoryLeftBrowser1))
        End If
        If _Settings.RaiseActionsRightBrowser2 Then
            FileBrowser2.Navigate(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RaiseActionDirectoryRightBrowser2))
        End If
    End Sub
    Public Sub RefreshGUI()
        If Not _Settings.InitialDirectoryLeftBrowser1 = "" Then
            FileBrowser1.Navigate(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.InitialDirectoryLeftBrowser1))
        End If
        If Not _Settings.InitialDirectoryRightBrowser2 = "" Then
            FileBrowser2.Navigate(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.InitialDirectoryRightBrowser2))
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

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        FileBrowser1.Refresh()
    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        FileBrowser2.Refresh()
    End Sub

    Private Sub FileBrowser1_DocumentCompleted(sender As Object, e As Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles FileBrowser1.DocumentCompleted
        ToolStripButton2.Enabled = FileBrowser1.CanGoBack
        ToolStripButton3.Enabled = FileBrowser1.CanGoForward
        ToolStripComboBox1.Text = FileBrowser1.Url.AbsolutePath.Replace("/", "\")
        If Not GetParentDir(FileBrowser1.Url.AbsolutePath) = "" Then
            ToolStripButton7.Enabled = True
        Else
            ToolStripButton7.Enabled = False
        End If
    End Sub

    Private Sub FileBrowser2_DocumentCompleted(sender As Object, e As Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles FileBrowser2.DocumentCompleted
        ToolStripButton1.Enabled = FileBrowser2.CanGoBack
        ToolStripButton5.Enabled = FileBrowser2.CanGoForward
        ToolStripComboBox2.Text = FileBrowser2.Url.AbsolutePath.Replace("/", "\")
        If Not GetParentDir(FileBrowser2.Url.AbsolutePath) = "" Then
            ToolStripButton8.Enabled = True
        Else
            ToolStripButton8.Enabled = False
        End If
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        Dim parentdir As String
        parentdir = GetParentDir(FileBrowser1.Url.AbsolutePath)
        If Not parentdir = "" Then
            FileBrowser1.Navigate(parentdir)
        End If
    End Sub

    Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs) Handles ToolStripButton8.Click
        Dim parentdir As String
        parentdir = GetParentDir(FileBrowser2.Url.AbsolutePath)
        If Not parentdir = "" Then
            FileBrowser2.Navigate(parentdir)
        End If
    End Sub

    Private Sub ToolStrip1_SizeChanged(sender As Object, e As EventArgs) Handles ToolStrip1.SizeChanged
        Try
            Dim newsize As New Size(SplitContainer1.Panel1.Width - 120, ToolStripComboBox1.Size.Height)
            ToolStripComboBox1.Size = newsize
            ToolStrip1.Hide()
            ToolStrip1.Show()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStrip2_SizeChanged(sender As Object, e As EventArgs) Handles ToolStrip2.SizeChanged
        Try
            Dim newsize As New Size(SplitContainer1.Panel2.Width - 120, ToolStripComboBox2.Size.Height)
            ToolStripComboBox2.Size = newsize
            ToolStrip2.Hide()
            ToolStrip2.Show()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshGUI()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        FileBrowser1.GoBack()
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        FileBrowser1.GoForward()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        FileBrowser2.GoBack()
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        FileBrowser2.GoForward()
    End Sub

    Private Sub ToolStripComboBox1_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles ToolStripComboBox1.KeyDown
        If e.KeyCode = 13 Then
            If Not ToolStripComboBox1.Text = "" Then
                FileBrowser1.Navigate(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, ToolStripComboBox1.Text))
                e.Handled = True
                e.SuppressKeyPress = True
            End If
        End If
    End Sub

    Private Sub ToolStripComboBox2_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles ToolStripComboBox2.KeyDown
        If e.KeyCode = 13 Then
            If Not ToolStripComboBox2.Text = "" Then
                FileBrowser2.Navigate(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, ToolStripComboBox2.Text))
                e.Handled = True
                e.SuppressKeyPress = True
            End If
        End If
    End Sub
End Class
