'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Runtime.InteropServices
Imports CSToolHostWindow

Public Class WindowManagerForm
    Public _parent As MainForm
    <DllImport("uxtheme", CharSet:=CharSet.Unicode)>
    Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal textSubAppName As String, ByVal textSubIdList As String) As Integer
    End Function
    Public Sub SetUXThemeForListView()
        Try
            SetWindowTheme(ListView1.Handle, "explorer", Nothing)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub WindowManagerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAllWindowsToGUI()
        SetUXThemeForListView()
    End Sub

    Public Function LoadAllWindowsToGUI() As Boolean
        Try
            ListView1.BeginUpdate()
            ListView1.Items.Clear()

            Dim allforms As List(Of DockingHostWindow)
            allforms = _parent.WindowManagerHandler.GetAllDockingWindows()

            If Not allforms.Count = 0 Then
                For index = 0 To allforms.Count - 1
                    Dim itm As New ListViewItem
                    itm.Text = allforms(index).TabText
                    itm.SubItems.Add(allforms(index).PluginHandler.PluginName)
                    itm.SubItems.Add(allforms(index).DockState.ToString)
                    itm.SubItems.Add(allforms(index)._PluginSettingsFile)
                    itm.SubItems.Add(allforms(index).PluginHandler.PluginGUID)
                    itm.SubItems.Add(allforms(index).InstanceGUID)
                    itm.Tag = allforms(index)
                    ListView1.Items.Add(itm)
                Next
            End If

            ListView1.EndUpdate()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            If Not ListView1.SelectedItems.Count = 0 Then
                Dim dockform As DockingHostWindow
                dockform = ListView1.SelectedItems(0).Tag
                PropertyGrid1.SelectedObject = dockform
                PropertyGrid2.SelectedObject = dockform.PluginHandler.UserSettingsClass
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Try
            If Not ListView1.SelectedItems.Count = 0 Then
                Dim dockform As DockingHostWindow
                dockform = ListView1.SelectedItems(0).Tag
                dockform.PluginHandler.RefreshGUI()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        Try
            If Not ListView1.SelectedItems.Count = 0 Then
                Dim dockform As DockingHostWindow
                dockform = ListView1.SelectedItems(0).Tag
                dockform.PluginHandler.RaiseActions(ToolStripTextBox1.Text)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Try
            If Not ListView1.SelectedItems.Count = 0 Then
                Dim dockform As DockingHostWindow
                dockform = ListView1.SelectedItems(0).Tag
                dockform.Close()
            End If
        Catch ex As Exception
        End Try
        LoadAllWindowsToGUI()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            If Not ListView1.SelectedItems.Count = 0 Then
                Dim dockform As DockingHostWindow
                dockform = ListView1.SelectedItems(0).Tag

                _parent.WindowManagerHandler.AddPluginWindowToGUI(_parent.CSDockPanelHosting, _parent.HostnameOrIPCtl.Text, dockform.PluginHandler.PluginName, _parent.WindowManagerHandler.PluginManager.PluginCollection, _parent.UserSettings.SettingName, dockform.DockState, False, dockform._PluginSettingsFile)
            End If
        Catch ex As Exception
        End Try
        LoadAllWindowsToGUI()
    End Sub

    Private Sub ListView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDoubleClick
        Try
            If Not ListView1.SelectedItems.Count = 0 Then
                Dim dockform As DockingHostWindow
                dockform = ListView1.SelectedItems(0).Tag
                dockform.Activate()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        If ToolStripButton5.Checked Then
            Me.TopMost = True
        Else
            Me.TopMost = False
        End If
    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        LoadAllWindowsToGUI()
    End Sub

    Private Sub PropertyGrid2_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles PropertyGrid2.PropertyValueChanged
        Try
            If Not ListView1.SelectedItems.Count = 0 Then
                Dim dockform As DockingHostWindow
                dockform = ListView1.SelectedItems(0).Tag
                dockform.PluginHandler.PluginSettingsChanged = True
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class