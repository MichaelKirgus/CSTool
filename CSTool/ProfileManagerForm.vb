'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Runtime.InteropServices
Imports CSToolPluginLib
Imports CSToolUserSettingsLib
Imports CSToolUserSettingsManager

Public Class ProfileManagerForm
    Public UserrSettingsManagerHandler As New UserSettingsManager
    Public CurrentUserSettingsObj As UserSettings = Nothing
    Public CurrentUserSettingsPath As String = ""
    Public _parent As MainForm = Nothing
    Public _UserProfilesLocation As String = "Profiles"

    Private Delegate Sub AddListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem, ByVal EnsureVisible As Boolean)
    Private Delegate Sub AddTreeViewItemDelegate(ByVal TreeViewCtl As TreeView, TreeViewItemCtl As TreeNode)
    Private Delegate Sub RemoveListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem)
    Private Delegate Sub RemoveTreeViewItemDelegate(ByVal TreeViewCtl As TreeView, TreeViewItemCtl As TreeNode)
    Private Delegate Sub BeginUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub EndUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub BeginUpdateTreeViewDelegate(ByVal TreeViewCtl As TreeView)
    Private Delegate Sub EndUpdateTreeViewDelegate(ByVal TreeViewCtl As TreeView)
    Private Delegate Sub ClearListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub ClearTreeViewDelegate(ByVal TreeViewCtl As TreeView)
    Private Delegate Function GetItemFromIndexDelegate(ByVal ListViewCtl As ListView, ByVal Index As Integer) As ListViewItem
    Private Delegate Sub SetGroupOfListViewItemDelegate(ByVal ListViewCtl As ListView, ByVal GroupListItem As ListViewItem, ByVal GroupItem As ListViewGroup)
    Private Delegate Sub AddGroupToListViewDelegate(ByVal ListViewCtl As ListView, ByVal GroupItem As ListViewGroup)
    <DllImport("uxtheme", CharSet:=CharSet.Unicode)>
    Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal textSubAppName As String, ByVal textSubIdList As String) As Integer
    End Function
    Public Sub SetUXThemeForAllControls()
        Try
            SetWindowTheme(ListView1.Handle, "explorer", Nothing)
            SetWindowTheme(TreeView1.Handle, "explorer", Nothing)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub AddGroupToListView(ByVal ListViewCtl As ListView, ByVal GroupItem As ListViewGroup)
        If ListViewCtl.InvokeRequired Then
            Dim AddGroupToListViewObj As New AddGroupToListViewDelegate(AddressOf AddGroupToListView)
            ListViewCtl.Invoke(AddGroupToListViewObj, New Object() {ListViewCtl, GroupItem})
        Else
            ListViewCtl.Groups.Add(GroupItem)
        End If
    End Sub

    Private Sub SetGroupOfListViewItem(ByVal ListViewCtl As ListView, ByVal GroupListItem As ListViewItem, ByVal GroupItem As ListViewGroup)
        If ListViewCtl.InvokeRequired Then
            Dim SetGroupOfListViewItemObj As New SetGroupOfListViewItemDelegate(AddressOf SetGroupOfListViewItem)
            ListViewCtl.Invoke(SetGroupOfListViewItemObj, New Object() {ListViewCtl, GroupListItem, GroupItem})
        Else
            ListViewCtl.Items(ListViewCtl.Items.IndexOf(GroupListItem)).Group = GroupItem
        End If
    End Sub
    Private Function GetItemFromIndex(ByVal ListViewCtl As ListView, ByVal Index As Integer) As ListViewItem
        If ListViewCtl.InvokeRequired Then
            Dim GetItemFromIndexObj As New GetItemFromIndexDelegate(AddressOf GetItemFromIndex)
            ListViewCtl.Invoke(GetItemFromIndexObj, New Object() {ListViewCtl, Index})
            Return New ListViewItem
        Else
            Return ListViewCtl.Items(Index)
        End If
    End Function

    Private Sub ClearListView(ByVal ListViewCtl As ListView)
        If ListViewCtl.InvokeRequired Then
            Dim ClearListViewObj As New BeginUpdateListViewDelegate(AddressOf ClearListView)
            ListViewCtl.Invoke(ClearListViewObj, New Object() {ListViewCtl})
        Else
            ListViewCtl.Items.Clear()
            ListViewCtl.Groups.Clear()
        End If
    End Sub

    Private Sub ClearTreeView(ByVal TreeViewCtl As TreeView)
        If TreeViewCtl.InvokeRequired Then
            Dim ClearTreeViewObj As New ClearTreeViewDelegate(AddressOf ClearTreeView)
            TreeViewCtl.Invoke(ClearTreeViewObj, New Object() {TreeViewCtl})
        Else
            TreeViewCtl.Nodes.Clear()
        End If
    End Sub

    Private Sub BeginUpdateListView(ByVal ListViewCtl As ListView)
        Try
            If ListViewCtl.InvokeRequired Then
                Dim BeginUpdateListViewObj As New BeginUpdateListViewDelegate(AddressOf BeginUpdateListView)
                ListViewCtl.Invoke(BeginUpdateListViewObj, New Object() {ListViewCtl})
            Else
                ListViewCtl.BeginUpdate()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub EndUpdateListView(ByVal ListViewCtl As ListView)
        Try
            If ListViewCtl.InvokeRequired Then
                Dim EndUpdateListViewObj As New EndUpdateListViewDelegate(AddressOf EndUpdateListView)
                ListViewCtl.Invoke(EndUpdateListViewObj, New Object() {ListViewCtl})
            Else
                ListViewCtl.EndUpdate()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BeginUpdateTreeView(ByVal TreeViewCtl As TreeView)
        Try
            If TreeViewCtl.InvokeRequired Then
                Dim BeginUpdateTreeViewObj As New BeginUpdateTreeViewDelegate(AddressOf BeginUpdateTreeView)
                TreeViewCtl.Invoke(BeginUpdateTreeViewObj, New Object() {TreeViewCtl})
            Else
                TreeViewCtl.BeginUpdate()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub EndUpdateTreeView(ByVal TreeViewCtl As TreeView)
        Try
            If TreeViewCtl.InvokeRequired Then
                Dim EndUpdateTreeViewObj As New EndUpdateTreeViewDelegate(AddressOf EndUpdateTreeView)
                TreeViewCtl.Invoke(EndUpdateTreeViewObj, New Object() {TreeViewCtl})
            Else
                TreeViewCtl.EndUpdate()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub AddListViewItem(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem, Optional ByVal EnsureVisible As Boolean = False)
        If ListViewCtl.InvokeRequired Then
            Dim delAddListViewItem As New AddListViewItemDelegate(AddressOf AddListViewItem)
            ListViewCtl.Invoke(delAddListViewItem, New Object() {ListViewCtl, ListViewItemCtl, EnsureVisible})
        Else
            ListViewCtl.Items.Add(ListViewItemCtl)
            If EnsureVisible Then
                ListViewCtl.EnsureVisible(ListViewCtl.Items.Count - 1)
            End If
        End If
    End Sub

    Private Sub AddTreeViewItem(ByVal TreeViewCtl As TreeView, TreeViewItemCtl As TreeNode)
        If TreeViewCtl.InvokeRequired Then
            Dim AddTreeViewItemObj As New AddTreeViewItemDelegate(AddressOf AddTreeViewItem)
            TreeViewCtl.Invoke(AddTreeViewItemObj, New Object() {TreeViewCtl, TreeViewItemCtl})
        Else
            TreeViewCtl.Nodes.Add(TreeViewItemCtl)
        End If
    End Sub

    Private Sub RemoveListViewItem(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem)
        If ListViewCtl.InvokeRequired Then
            Dim delRemoveListViewItem As New RemoveListViewItemDelegate(AddressOf RemoveListViewItem)
            ListViewCtl.Invoke(delRemoveListViewItem, New Object() {ListViewCtl, ListViewItemCtl})
        Else
            ListViewCtl.Items.Remove(ListViewItemCtl)
        End If
    End Sub

    Private Sub RemoveTreeViewItem(ByVal TreeViewCtl As TreeView, TreeViewItemCtl As TreeNode)
        If TreeViewCtl.InvokeRequired Then
            Dim RemoveTreeViewItemObj As New RemoveTreeViewItemDelegate(AddressOf RemoveTreeViewItem)
            TreeViewCtl.Invoke(RemoveTreeViewItemObj, New Object() {TreeViewCtl, TreeViewItemCtl})
        Else
            TreeViewCtl.Nodes.Remove(TreeViewItemCtl)
        End If
    End Sub

    Public Function LoadUserProfilesAndWorkspacesFunction() As Boolean
        Try
            BeginUpdateTreeView(TreeView1)
            ClearTreeView(TreeView1)

            If IO.Directory.Exists(_UserProfilesLocation) Then
                Dim layoutfilescoll As String()
                layoutfilescoll = IO.Directory.GetFiles(_UserProfilesLocation, "Layout.xml", IO.SearchOption.AllDirectories)
                If Not layoutfilescoll.Count = 0 Then
                    For index = 0 To layoutfilescoll.Count - 1
                        Try
                            Dim fileinfo As New IO.FileInfo(layoutfilescoll(index))
                            Dim layoutdir As IO.DirectoryInfo = fileinfo.Directory
                            Dim profiledirinf As IO.DirectoryInfo
                            Dim profilename As String
                            profiledirinf = layoutdir.Parent
                            profilename = profiledirinf.Name
                            Dim nodectl As TreeNode = Nothing
                            Dim parentnode As TreeNode()
                            parentnode = TreeView1.Nodes.Find(profilename, False)
                            If Not parentnode.Count = 0 Then
                                nodectl = parentnode(0).Clone
                                RemoveTreeViewItem(TreeView1, parentnode(0))
                            Else
                                nodectl = New TreeNode
                                nodectl.Text = profilename
                                nodectl.Name = profilename
                                nodectl.ToolTipText = profiledirinf.FullName
                                nodectl.ImageIndex = 0
                                nodectl.SelectedImageIndex = 0
                                nodectl.Tag = profiledirinf
                            End If
                            Dim subnode As TreeNode
                            subnode = nodectl.Nodes.Add(layoutdir.Name)
                            subnode.Tag = layoutdir
                            subnode.ToolTipText = layoutdir.FullName
                            subnode.ImageIndex = 1
                            subnode.SelectedImageIndex = 1
                            AddTreeViewItem(TreeView1, nodectl)
                        Catch ex As Exception
                        End Try
                    Next

                    EndUpdateTreeView(TreeView1)

                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetPluginsFromWorkspace(ByVal WorkspaceDir As String) As Boolean
        Try
            BeginUpdateListView(ListView1)
            ClearListView(ListView1)

            If IO.Directory.Exists(WorkspaceDir) Then
                Dim pluginsettingscoll As String()
                pluginsettingscoll = IO.Directory.GetFiles(WorkspaceDir, "*.xml", IO.SearchOption.TopDirectoryOnly)
                If Not pluginsettingscoll.Count = 0 Then
                    For index = 0 To pluginsettingscoll.Count - 1
                        If Not pluginsettingscoll(index).EndsWith("Layout.xml") Then
                            Try
                                Dim filename As String
                                filename = pluginsettingscoll(index)
                                Dim filenameinfo As New IO.FileInfo(filename)
                                filename = filename.Split("\")(filename.Split("\").Count - 1)
                                filename = filename.Replace(".xml", "")
                                Dim plugdata As String()
                                plugdata = filename.Split("_")
                                Dim plugininstance As ICSToolInterface
                                plugininstance = _parent.WindowManagerHandler.GetPluginByGUID(plugdata(0), _parent.WindowManagerHandler.PluginManager.PluginCollection)
                                Dim pluginitm As New ListViewItem
                                pluginitm.Text = plugininstance.PluginName
                                pluginitm.ImageIndex = 0
                                pluginitm.ToolTipText = filename
                                pluginitm.Tag = plugininstance
                                pluginitm.SubItems.Add(plugininstance.PluginGUID)
                                If plugininstance.PluginType = ICSToolInterface.PluginTypeEnum.GUIWindow Then
                                    pluginitm.SubItems.Add(plugdata(1))
                                Else
                                    pluginitm.SubItems.Add("")
                                End If
                                pluginitm.SubItems.Add(pluginsettingscoll(index))
                                pluginitm.SubItems.Add(filenameinfo.LastWriteTime.ToString)
                                pluginitm.SubItems.Add(filenameinfo.CreationTime.ToString)
                                AddListViewItem(ListView1, pluginitm, False)
                            Catch ex As Exception
                            End Try
                        End If
                    Next

                    EndUpdateListView(ListView1)

                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetWorkspaceInfo(ByVal ProfilePath As String) As Boolean
        Try
            If IO.Directory.Exists(ProfilePath) Then
                If IO.File.Exists(ProfilePath & "\UserSettings.xml") Then
                    CurrentUserSettingsObj = UserrSettingsManagerHandler.LoadSettings(ProfilePath & "\UserSettings.xml")
                    CurrentUserSettingsPath = ProfilePath & "\UserSettings.xml"
                    XmlEditor2.ResetText()
                    XmlEditor2.Text = IO.File.ReadAllText(ProfilePath & "\UserSettings.xml")
                    XmlEditor2.AllowXmlFormatting = False
                    XmlEditor2.AllowXmlFormatting = True
                    XmlEditor2.Refresh()

                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub ProfileManagerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReloadUserProfilesAndWorkspaces()
        SetUXThemeForAllControls()
    End Sub

    Public Sub ReloadUserProfilesAndWorkspaces()
        If Not LoadProfilesAndWorkspaces.IsBusy = True Then
            LoadImage1.Visible = True
            LoadProfilesAndWorkspaces.RunWorkerAsync()
        End If
    End Sub

    Private Sub LoadProfilesAndWorkspaces_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadProfilesAndWorkspaces.DoWork
        e.Result = LoadUserProfilesAndWorkspacesFunction()
    End Sub

    Private Sub LoadProfilesAndWorkspaces_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadProfilesAndWorkspaces.RunWorkerCompleted
        LoadImage1.Visible = False
        If Not TreeView1.Nodes.Count = 0 Then
            TreeView1.SelectedNode = TreeView1.Nodes(0)
            If Not TreeView1.Nodes(0).Nodes.Count = 0 Then
                TreeView1.SelectedNode = TreeView1.Nodes(0).Nodes(0)
            End If
        End If
    End Sub

    Private Sub LoadPlugins_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadPlugins.DoWork
        e.Result = GetPluginsFromWorkspace(e.Argument)
    End Sub

    Public Sub SetLoading()
        LoadImage1.Visible = True
        LoadImage2.Visible = True
        LoadImage3.Visible = True
        LoadImage4.Visible = True
        ToolStrip1.Enabled = False
        ToolStrip2.Enabled = False
        ToolStrip3.Enabled = False
        ToolStrip4.Enabled = False
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        If Not IsNothing(TreeView1.SelectedNode) Then
            If Not TreeView1.SelectedNode.Level = 0 Then
                Dim profiledirinf As IO.DirectoryInfo
                profiledirinf = TreeView1.SelectedNode.Tag
                If Not LoadPlugins.IsBusy = True Then
                    LoadImage2.Visible = True
                    LoadPlugins.RunWorkerAsync(profiledirinf.FullName)
                End If
            Else
                TreeView1.SelectedNode.Expand()
            End If
            If Not LoadWorkspaceSettings.IsBusy = True Then
                Dim profiledirinf As IO.DirectoryInfo
                If Not IsNothing(TreeView1.SelectedNode.Parent) Then
                    profiledirinf = TreeView1.SelectedNode.Parent.Tag
                Else
                    profiledirinf = TreeView1.SelectedNode.Tag
                End If
                If Not CurrentUserSettingsPath = profiledirinf.FullName Then
                    LoadImage3.Visible = True
                    LoadWorkspaceSettings.RunWorkerAsync(profiledirinf.FullName)
                End If
            End If
        End If
    End Sub

    Private Sub LoadPlugins_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadPlugins.RunWorkerCompleted
        LoadImage2.Visible = False
        If Not ListView1.Items.Count = 0 Then
            ListView1.Items(0).Selected = True
        End If
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
    End Sub

    Public Sub ReloadSelectedPluginSettings()
        If Not ListView1.SelectedItems.Count = 0 Then
            LoadImage4.Visible = True
            Dim plugininstance As ICSToolInterface
            plugininstance = ListView1.SelectedItems(0).Tag
            plugininstance = _parent.WindowManagerHandler.PluginManager.CreateNewPluginInstance(_parent.WindowManagerHandler.GetPluginIndexByGUID(plugininstance.PluginGUID, _parent.WindowManagerHandler.PluginManager.PluginCollection))
            plugininstance.LoadPluginSettings(ListView1.SelectedItems(0).SubItems(3).Text)
            PropertyGrid2.SelectedObject = plugininstance.UserSettingsClass

            XmlEditor1.Text = IO.File.ReadAllText(ListView1.SelectedItems(0).SubItems(3).Text)

            LoadImage4.Visible = False
        End If
    End Sub

    Public Sub SaveSelectedPluginSettings()
        If Not ListView1.SelectedItems.Count = 0 Then
            Dim plugininstance As ICSToolInterface
            plugininstance = ListView1.SelectedItems(0).Tag
            plugininstance = _parent.WindowManagerHandler.PluginManager.CreateNewPluginInstance(_parent.WindowManagerHandler.GetPluginIndexByGUID(plugininstance.PluginGUID, _parent.WindowManagerHandler.PluginManager.PluginCollection))
            plugininstance.UserSettingsClass = PropertyGrid2.SelectedObject
            plugininstance.SavePluginSettings(ListView1.SelectedItems(0).SubItems(3).Text)
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        ReloadSelectedPluginSettings()
    End Sub

    Private Sub LoadWorkspaceSettings_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadWorkspaceSettings.DoWork
        e.Result = GetWorkspaceInfo(e.Argument)
    End Sub

    Private Sub LoadWorkspaceSettings_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadWorkspaceSettings.RunWorkerCompleted
        PropertyGrid1.SelectedObject = CurrentUserSettingsObj

        LoadImage3.Visible = False
    End Sub

    Private Sub ToolStripButton14_Click(sender As Object, e As EventArgs) Handles ToolStripButton14.Click
        If ToolStripButton14.Checked Then
            SplitContainer4.Panel2Collapsed = False
            XmlEditor1.AllowXmlFormatting = False
            XmlEditor1.AllowXmlFormatting = True
            XmlEditor1.Refresh()
        Else
            SplitContainer4.Panel2Collapsed = True
        End If
    End Sub

    Private Sub ToolStripButton16_Click(sender As Object, e As EventArgs) Handles ToolStripButton16.Click
        If Not ListView1.SelectedItems.Count = 0 Then
            XmlEditor1.Text = IO.File.ReadAllText(ListView1.SelectedItems(0).SubItems(3).Text)
        End If
    End Sub

    Private Sub ToolStripButton15_Click(sender As Object, e As EventArgs) Handles ToolStripButton15.Click
        If Not ListView1.SelectedItems.Count = 0 Then
            IO.File.WriteAllText(ListView1.SelectedItems(0).SubItems(3).Text, XmlEditor1.Text)
        End If
    End Sub

    Private Sub ToolStripButton19_Click(sender As Object, e As EventArgs) Handles ToolStripButton19.Click
        If Not CurrentUserSettingsPath = "" Then
            UserrSettingsManagerHandler.SaveSettings(CurrentUserSettingsObj, CurrentUserSettingsPath)
        End If
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        If Not CurrentUserSettingsPath = "" Then
            CurrentUserSettingsObj = UserrSettingsManagerHandler.LoadSettings(CurrentUserSettingsPath)
            PropertyGrid1.SelectedObject = CurrentUserSettingsObj
        End If
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        If Not IsNothing(TreeView1.SelectedNode) Then
            If Not TreeView1.SelectedNode.Level = 0 Then
                Dim profiledirinf As IO.DirectoryInfo
                profiledirinf = TreeView1.SelectedNode.Tag
                If Not LoadPlugins.IsBusy = True Then
                    LoadImage2.Visible = True
                    LoadPlugins.RunWorkerAsync(profiledirinf.FullName)
                End If
            End If
        End If
    End Sub

    Private Sub ToolStripButton21_Click(sender As Object, e As EventArgs) Handles ToolStripButton21.Click
        ReloadSelectedPluginSettings()
    End Sub

    Private Sub ToolStripButton20_Click(sender As Object, e As EventArgs) Handles ToolStripButton20.Click
        SaveSelectedPluginSettings()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        ReloadUserProfilesAndWorkspaces()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        XmlEditor2.AllowXmlFormatting = False
        XmlEditor2.AllowXmlFormatting = True
        XmlEditor2.Refresh()
    End Sub
End Class