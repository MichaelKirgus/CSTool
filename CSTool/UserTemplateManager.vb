'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Runtime.InteropServices
Imports CSTemplateManager
Imports CSTemplateManager.TemplateCollectionSettings
Imports CSToolPluginLib
Imports WeifenLuo.WinFormsUI.Docking

Public Class UserTemplateManager
    Public _parent As MainForm
    Public CurrentPluginName As String = ""
    Public CurrentTemplateName As String = ""
    Public CurrentListViewItemIndex As Integer = -1
    Public ClearSelection As Boolean = False
    Public NoSelectionRaising As Boolean = False
    Public EditMode As Boolean = False
    Public InitPluginTemplateIndex As Integer = 0

    <DllImport("uxtheme", CharSet:=CharSet.Unicode)>
    Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal textSubAppName As String, ByVal textSubIdList As String) As Integer
    End Function

    Public Sub SetUXThemeForAllViews()
        Try
            SetWindowTheme(ListView1.Handle, "explorer", Nothing)
            SetWindowTheme(TreeView1.Handle, "explorer", Nothing)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub UserTemplateManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If EditMode Then
            SplitContainer2.Panel2Collapsed = False
        Else
            ToolStripButton1.Enabled = False
            ToolStripButton2.Enabled = False
            ToolStripButton3.Enabled = False
            SplitContainer2.Panel2Collapsed = True
        End If

        LoadTemplatesToGUI(_parent.ApplicationSettings.UserTemplatesDir, False)
        LoadAllEnumItemsToComboBox()
        LoadCustomItems(ContextMenuStrip1)
        SetUXThemeForAllViews()

        TreeView1.SelectedNode = TreeView1.Nodes(InitPluginTemplateIndex)
    End Sub

    Public Function LoadCustomItems(ByVal ContextMenuStripCtl As ContextMenuStrip) As Boolean
        Try
            ContextMenuStripCtl.Items.Clear()

            Dim allstyles As Array
            allstyles = System.Enum.GetNames(GetType(DefaultWindowStyleEnum))

            For index = 0 To allstyles.Length - 1
                Try
                    Dim rr As ToolStripItem
                    rr = ContextMenuStripCtl.Items.Add(allstyles(index))
                    rr.Tag = index
                    AddHandler rr.Click, AddressOf ClickCustomItemEntry
                Catch ex As Exception
                End Try
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub ClickCustomItemEntry(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim curritm As ListViewItem
            curritm = ListView1.SelectedItems(0)
            Dim tempitm As TemplateCollectionSettings
            tempitm = curritm.Tag
            Dim currmenuitm As ToolStripItem
            currmenuitm = sender
            If AddItemFromTemplateToWorkspace(tempitm.TemplateGUID, _parent.CSDockPanelHosting, CurrentPluginName, _parent.WindowManagerHandler.PluginManager.PluginCollection, _parent.UserSettings.SettingName, currmenuitm.Tag) Then
                If ToolStripButton6.Checked Then
                    Me.Close()
                Else
                    Me.Activate()
                End If
            Else
                ShowPluginAddingError()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ShowPluginAddingError()
        MsgBox("Invalid style for plugin or plugin instance is already loaded!" & vbNewLine & "Please choose different style or close plugin instance.")
    End Sub

    Public Function AddItemFromTemplateToWorkspace(ByVal TemplateGUID As String, ByVal DockingHost As DockPanel, ByVal PluginName As String, ByVal TargetPluginCollection As List(Of CSToolPluginLib.ICSToolInterface), Optional ByVal UserSettingName As String = "Default", Optional Style As DefaultWindowStyleEnum = DefaultWindowStyleEnum.DockDocument) As Boolean
        Try
            Dim currtempobj As TemplateCollectionSettings
            Dim currplug As ICSToolInterface
            currplug = _parent.WindowManagerHandler.GetPluginByName(PluginName, TargetPluginCollection)
            currtempobj = _parent.UserTemplateManager.GetTemplateSettingsFromTemplateGUID(_parent.ApplicationSettings.UserTemplatesDir, currplug, TemplateGUID)
            Dim tempsett As String
            tempsett = _parent.UserTemplateManager.GetPluginTemplateSettingsFilePath(_parent.ApplicationSettings.UserTemplatesDir, currplug, currtempobj)

            Dim isok As Boolean
            Select Case Style
                Case DefaultWindowStyleEnum.DockBottom
                    isok = _parent.WindowManagerHandler.AddPluginWindowToGUI(DockingHost, _parent.HostnameOrIPCtl.Text, CurrentPluginName, _parent.WindowManagerHandler.PluginManager.PluginCollection, _parent.UserSettings.SettingName, DockState.DockBottom, False, tempsett)
                Case DefaultWindowStyleEnum.DockDocument
                    isok = _parent.WindowManagerHandler.AddPluginWindowToGUI(DockingHost, _parent.HostnameOrIPCtl.Text, CurrentPluginName, _parent.WindowManagerHandler.PluginManager.PluginCollection, _parent.UserSettings.SettingName, DockState.Document, False, tempsett)
                Case DefaultWindowStyleEnum.DockLeft
                    isok = _parent.WindowManagerHandler.AddPluginWindowToGUI(DockingHost, _parent.HostnameOrIPCtl.Text, CurrentPluginName, _parent.WindowManagerHandler.PluginManager.PluginCollection, _parent.UserSettings.SettingName, DockState.DockLeft, False, tempsett)
                Case DefaultWindowStyleEnum.DockRight
                    isok = _parent.WindowManagerHandler.AddPluginWindowToGUI(DockingHost, _parent.HostnameOrIPCtl.Text, CurrentPluginName, _parent.WindowManagerHandler.PluginManager.PluginCollection, _parent.UserSettings.SettingName, DockState.DockRight, False, tempsett)
                Case DefaultWindowStyleEnum.DockTop
                    isok = _parent.WindowManagerHandler.AddPluginWindowToGUI(DockingHost, _parent.HostnameOrIPCtl.Text, CurrentPluginName, _parent.WindowManagerHandler.PluginManager.PluginCollection, _parent.UserSettings.SettingName, DockState.DockTop, False, tempsett)
                Case DefaultWindowStyleEnum.FloatWindow
                    isok = _parent.WindowManagerHandler.AddPluginWindowToGUI(DockingHost, _parent.HostnameOrIPCtl.Text, CurrentPluginName, _parent.WindowManagerHandler.PluginManager.PluginCollection, _parent.UserSettings.SettingName, DockState.Float, False, tempsett)
                Case DefaultWindowStyleEnum.IndependentWindow
                    isok = _parent.WindowManagerHandler.AddPluginWindowToGUI(DockingHost, _parent.HostnameOrIPCtl.Text, CurrentPluginName, _parent.WindowManagerHandler.PluginManager.PluginCollection, _parent.UserSettings.SettingName, DockState.Unknown, True, tempsett)
            End Select

            Return isok
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadAllEnumItemsToComboBox() As Boolean
        Try
            ComboBox1.BeginUpdate()
            ComboBox1.Items.Clear()

            Dim allstyles As Array
            allstyles = System.Enum.GetValues(GetType(DefaultWindowStyleEnum))

            For index = 0 To allstyles.Length - 1
                ComboBox1.Items.Add(allstyles(index))
            Next

            ComboBox1.EndUpdate()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadTemplatesToGUI(ByVal TemplatePath As String, Optional ByVal RefreshRepo As Boolean = False) As Boolean
        Try
            TreeView1.BeginUpdate()
            TreeView1.Nodes.Clear()
            ImageList1.Images.Clear()

            If RefreshRepo Then
                _parent.UserTemplateManager.CurrentTemplates = _parent.UserTemplateManager.GetTemplates(TemplatePath, _parent.WindowManagerHandler.PluginManager.PluginCollection)
            End If

            If Not _parent.WindowManagerHandler.PluginManager.PluginCollection.Count = 0 Then
                For index = 0 To _parent.WindowManagerHandler.PluginManager.PluginCollection.Count - 1
                    If _parent.WindowManagerHandler.PluginManager.PluginCollection(index).PluginType = ICSToolInterface.PluginTypeEnum.GUIWindow Then
                        Dim newnode As New TreeNode
                        newnode.Text = _parent.WindowManagerHandler.PluginManager.PluginCollection(index).PluginName
                        newnode.Name = _parent.WindowManagerHandler.PluginManager.PluginCollection(index).PluginName
                        newnode.Tag = _parent.WindowManagerHandler.PluginManager.PluginCollection(index)
                        Dim pluginicon As Icon
                        pluginicon = _parent.WindowManagerHandler.PluginManager.PluginCollection(index).UserControlIcon
                        ImageList1.Images.Add(pluginicon)
                        newnode.ImageIndex = ImageList1.Images.Count - 1
                        newnode.SelectedImageIndex = ImageList1.Images.Count - 1
                        TreeView1.Nodes.Add(newnode)
                    End If
                Next
            End If

            If Not _parent.UserTemplateManager.CurrentTemplates.Count = 0 Then
                For index = 0 To _parent.UserTemplateManager.CurrentTemplates.Count - 1
                    Dim refplugin As ICSToolInterface
                    refplugin = _parent.WindowManagerHandler.GetPluginByName(_parent.UserTemplateManager.CurrentTemplates(index).PluginName, _parent.WindowManagerHandler.PluginManager.PluginCollection)
                    Dim parentnode As TreeNode
                    parentnode = TreeView1.Nodes.Find(refplugin.PluginName, False)(0)
                    If Not _parent.UserTemplateManager.CurrentTemplates(index).PluginTemplates.Count = 0 Then
                        For nodesind = 0 To _parent.UserTemplateManager.CurrentTemplates(index).PluginTemplates.Count - 1
                            Dim newtemp As New TreeNode
                            newtemp.Text = _parent.UserTemplateManager.CurrentTemplates(index).PluginTemplates(nodesind).TemplateName
                            newtemp.Tag = _parent.UserTemplateManager.CurrentTemplates(index).PluginTemplates(nodesind).TemplateGUID
                            newtemp.ImageIndex = parentnode.ImageIndex
                            newtemp.SelectedImageIndex = parentnode.ImageIndex
                            parentnode.Nodes.Add(newtemp)
                        Next
                    End If
                Next
            End If

            TreeView1.EndUpdate()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub SelectTreeViewItem()
        Try
            Dim curritm As ListViewItem
            curritm = ListView1.SelectedItems(0)
            Dim tempitm As TemplateCollectionSettings
            tempitm = curritm.Tag
            For index = 0 To TreeView1.Nodes.Count - 1
                If Not TreeView1.Nodes(index).Nodes.Count = 0 Then
                    For indexnod = 0 To TreeView1.Nodes(index).Nodes.Count - 1
                        If TreeView1.Nodes(index).Nodes(indexnod).Tag = tempitm.TemplateGUID Then
                            TreeView1.SelectedNode = TreeView1.Nodes(index).Nodes(indexnod)
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Function LoadTemplatesFromSelectedNodeToGUI() As Boolean
        Try
            ListView1.BeginUpdate()
            ListView1.Items.Clear()

            Dim currparentnode As TreeNode
            If TreeView1.SelectedNode.Level = 0 Then
                currparentnode = TreeView1.SelectedNode
            Else
                currparentnode = TreeView1.SelectedNode.Parent
            End If

            currparentnode.Expand()

            Dim avtemps As List(Of TemplateCollectionSettings)
            avtemps = _parent.UserTemplateManager.GetAllTemplatesFromPluginName(_parent.UserTemplateManager.CurrentTemplates, currparentnode.Text)

            ImageList2.Images.Clear()

            If Not avtemps.Count = 0 Then
                NoTemplatePanel.Visible = False
                ListView1.Visible = True

                Dim pluginicon As Icon
                Dim currplug As ICSToolInterface
                currplug = currparentnode.Tag
                pluginicon = currplug.UserControlIcon
                ImageList2.Images.Add(pluginicon)

                For index = 0 To avtemps.Count - 1
                    Dim newitm As New ListViewItem
                    newitm.Text = avtemps(index).TemplateName
                    newitm.SubItems.Add(avtemps(index).TemplateDescription)
                    newitm.Tag = avtemps(index)
                    newitm.ImageIndex = 0
                    newitm.StateImageIndex = 0
                    ListView1.Items.Add(newitm)
                Next

                Try
                    If TreeView1.SelectedNode.Level = 1 Then
                        ListView1.Items(TreeView1.SelectedNode.Index).Selected = True
                    Else
                        If Not ListView1.Items.Count = 0 And Not IsNothing(ListView1.Items(0).Selected) Then
                            ListView1.Items(0).Selected = True
                        End If
                    End If
                Catch ex As Exception
                End Try
            Else
                NoTemplatePanel.Visible = True
                ListView1.Visible = False
            End If

            ListView1.EndUpdate()
            ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function RemoveTemplateFromNode() As Boolean
        Try
            Dim curritm As ListViewItem
            curritm = ListView1.SelectedItems(0)
            Dim currplug As ICSToolInterface
            Dim currparentnode As TreeNode
            Dim currnode As TreeNode
            If TreeView1.SelectedNode.Level = 0 Then
                currparentnode = TreeView1.SelectedNode
            Else
                currparentnode = TreeView1.SelectedNode.Parent
            End If
            currnode = TreeView1.SelectedNode
            currplug = currparentnode.Tag
            Dim tempitm As TemplateCollectionSettings
            tempitm = curritm.Tag
            _parent.UserTemplateManager.RemoveTemplateFromCollection(_parent.UserTemplateManager.CurrentTemplates, tempitm.TemplateGUID)
            If _parent.UserTemplateManager.DeleteTemplate(_parent.ApplicationSettings.UserTemplatesDir, currplug, tempitm.TemplateGUID) Then
                TreeView1.Nodes.Remove(currnode)
                ListView1.Items.Remove(curritm)
                PropertyGrid1.SelectedObject = Nothing
                PropertyGrid2.SelectedObject = Nothing
                If Not ListView1.Items.Count = 0 Then
                    ListView1.Items(ListView1.Items.Count - 1).Selected = True
                Else
                    ListView1.Items(0).Selected = True
                End If
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SaveTemplate() As Boolean
        Try
            Dim curritm As ListViewItem
            Dim tempitm As TemplateCollectionSettings
            curritm = ListView1.SelectedItems(0)
            tempitm = curritm.Tag
            Dim currplug As ICSToolInterface
            Dim currparentnode As TreeNode
            Dim currnode As TreeNode
            If TreeView1.SelectedNode.Level = 0 Then
                currparentnode = TreeView1.SelectedNode
            Else
                currparentnode = TreeView1.SelectedNode.Parent
            End If
            currnode = TreeView1.SelectedNode
            currplug = currparentnode.Tag
            curritm.Text = tempitm.TemplateName
            curritm.SubItems(1).Text = tempitm.TemplateDescription
            SelectTreeViewItem()
            TreeView1.SelectedNode.Text = tempitm.TemplateName

            _parent.UserTemplateManager.RefreshTemplateInCollection(_parent.UserTemplateManager.CurrentTemplates, tempitm.TemplateGUID, tempitm)
            _parent.UserTemplateManager.SaveTemplate(_parent.ApplicationSettings.UserTemplatesDir, currplug, tempitm, PropertyGrid1.SelectedObject)
            CurrentTemplateName = tempitm.TemplateName

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function AddNewTemplate(ByVal Duplicate As Boolean) As Boolean
        Try
            Dim curritm As ListViewItem = Nothing
            Dim tempitm As TemplateCollectionSettings = Nothing
            If Duplicate Then
                curritm = ListView1.SelectedItems(0)
                tempitm = curritm.Tag
            End If
            Dim currplug As ICSToolInterface
            Dim currparentnode As TreeNode
            Dim currnode As TreeNode
            If TreeView1.SelectedNode.Level = 0 Then
                currparentnode = TreeView1.SelectedNode
            Else
                currparentnode = TreeView1.SelectedNode.Parent
            End If
            currnode = TreeView1.SelectedNode
            currplug = currparentnode.Tag
            Dim newsettingsobj As Object = Nothing
            If Duplicate Then
                newsettingsobj = tempitm
            Else
                newsettingsobj = _parent.WindowManagerHandler.PluginManager.CreateNewPluginInstance(_parent.WindowManagerHandler.GetPluginIndexByGUID(currplug.PluginGUID, _parent.WindowManagerHandler.PluginManager.PluginCollection)).UserSettingsClass
            End If
            Dim isok As Boolean = False
            Dim newplugdupobj As TemplateCollectionSettings = Nothing
            Dim newtemp As New TemplateCollectionSettings
            newtemp.TemplateName = "New template"
            If Duplicate Then
                tempitm.TemplateName = tempitm.TemplateName & " (duplicate)"
                tempitm.TemplateGUID = Guid.NewGuid.ToString
                _parent.UserTemplateManager.AddTemplateToCollection(_parent.UserTemplateManager.CurrentTemplates, currplug, tempitm)
                isok = _parent.UserTemplateManager.DuplicateTemplate(_parent.ApplicationSettings.UserTemplatesDir, currplug, tempitm.TemplateGUID, newsettingsobj)
                newplugdupobj = New TemplateCollectionSettings
            Else
                Dim result As String
                _parent.UserTemplateManager.AddTemplateToCollection(_parent.UserTemplateManager.CurrentTemplates, currplug, newtemp)
                result = _parent.UserTemplateManager.SaveTemplate(_parent.ApplicationSettings.UserTemplatesDir, currplug, newtemp, currplug.UserSettingsClass)
                If Not result = "" Then
                    newplugdupobj = _parent.UserTemplateManager.GetTemplateSettingsFromTemplateGUID(_parent.ApplicationSettings.UserTemplatesDir, currplug, result)
                    isok = True
                End If
            End If
            If isok Then
                Dim newitm As New ListViewItem
                If Duplicate Then
                    newitm.Text = tempitm.TemplateName & " (duplicate)"
                    newitm.SubItems.Add(tempitm.TemplateDescription)
                    newitm.Tag = tempitm
                Else
                    newitm.Text = "New template"
                    newitm.SubItems.Add("")
                    newitm.Tag = newtemp
                End If
                ListView1.Items.Add(newitm)
                If Not ListView1.Items.Count = 0 Then
                    ClearSelection = True
                    ResetListviewSelectedState()
                    ListView1.Items(ListView1.Items.Count - 1).Selected = True
                    ClearSelection = False
                Else
                    ClearSelection = True
                    ListView1.Items(0).Selected = True
                    ClearSelection = False
                End If
                Dim newnode As New TreeNode
                newplugdupobj.TemplateName = "New template"
                newnode.Text = newplugdupobj.TemplateName
                newnode.Tag = newplugdupobj.TemplateGUID
                newnode.ImageIndex = currparentnode.ImageIndex
                newnode.SelectedImageIndex = currparentnode.SelectedImageIndex
                currparentnode.Nodes.Add(newnode)
            Else
                Return False
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub ResetListviewSelectedState()
        If Not ListView1.Items.Count = 0 Then
            For index = 0 To ListView1.Items.Count - 1
                ListView1.Items(index).Selected = False
            Next
        End If
    End Sub


    Public Function ListViewSetPropertiesToGUI() As Boolean
        Try
            Dim curritm As ListViewItem
            curritm = ListView1.SelectedItems(0)
            Dim tempitm As TemplateCollectionSettings
            tempitm = curritm.Tag
            Dim currplug As ICSToolInterface

            Dim currparentnode As TreeNode
            If TreeView1.SelectedNode.Level = 0 Then
                currparentnode = TreeView1.SelectedNode
            Else
                currparentnode = TreeView1.SelectedNode.Parent
            End If
            currplug = currparentnode.Tag

            Dim newsettingsobj As Object
            Dim loadinstance As ICSToolInterface
            loadinstance = _parent.WindowManagerHandler.PluginManager.CreateNewPluginInstance(_parent.WindowManagerHandler.GetPluginIndexByGUID(currplug.PluginGUID, _parent.WindowManagerHandler.PluginManager.PluginCollection))
            loadinstance.LoadPluginSettings(_parent.UserTemplateManager.GetPluginTemplateSettingsFilePath(_parent.ApplicationSettings.UserTemplatesDir, loadinstance, tempitm))

            newsettingsobj = loadinstance.UserSettingsClass

            PropertyGrid2.SelectedObject = tempitm
            PropertyGrid1.SelectedObject = newsettingsobj

            CurrentPluginName = currplug.PluginName
            CurrentTemplateName = tempitm.TemplateName

            ComboBox1.SelectedIndex = tempitm.DefaultWindowStyle

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        LoadTemplatesToGUI(_parent.ApplicationSettings.UserTemplatesDir, True)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        RemoveTemplateFromNode()
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        AddNewTemplate(True)
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        AddNewTemplate(False)
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If (Not ClearSelection) And (Not NoSelectionRaising) Then
            Try
                CurrentListViewItemIndex = ListView1.SelectedItems(0).Index
            Catch ex As Exception
            End Try
            SelectTreeViewItem()
            ListViewSetPropertiesToGUI()
        End If
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        Try
            If Not ListView1.SelectedItems.Count = 0 Then
                If (Not ListView1.SelectedItems(0).Index = CurrentListViewItemIndex) Or ListView1.Items.Count = 0 Then
                    LoadTemplatesFromSelectedNodeToGUI()
                    NoSelectionRaising = True
                    ListView1.Focus()
                    ListView1.Items(CurrentListViewItemIndex).Selected = True
                    ListView1.Focus()
                    NoSelectionRaising = False
                    Exit Sub
                End If
                Dim currparentnode As TreeNode
                If TreeView1.SelectedNode.Level = 0 Then
                    currparentnode = TreeView1.SelectedNode
                Else
                    currparentnode = TreeView1.SelectedNode.Parent
                End If
                If Not currparentnode.Text = CurrentPluginName Then
                    LoadTemplatesFromSelectedNodeToGUI()
                    NoSelectionRaising = True
                    ListView1.Focus()
                    ListView1.Items(CurrentListViewItemIndex).Selected = True
                    ListView1.Focus()
                    NoSelectionRaising = False
                    Exit Sub
                End If
                If TreeView1.SelectedNode.Level = 1 Then
                    LoadTemplatesFromSelectedNodeToGUI()
                    NoSelectionRaising = True
                    ListView1.Focus()
                    ListView1.Items(CurrentListViewItemIndex).Selected = True
                    ListView1.Focus()
                    NoSelectionRaising = False
                End If
            Else
                Try
                    LoadTemplatesFromSelectedNodeToGUI()
                    NoSelectionRaising = True
                    ListView1.Focus()
                    ListView1.Items(CurrentListViewItemIndex).Selected = True
                    ListView1.Focus()
                    NoSelectionRaising = False
                Catch ex2 As Exception
                End Try
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        SaveTemplate()
    End Sub

    Public Sub AddItemfromDefinedDefaultStyle()
        Try
            Dim curritm As ListViewItem
            curritm = ListView1.SelectedItems(0)
            Dim tempitm As TemplateCollectionSettings
            tempitm = curritm.Tag
            If AddItemFromTemplateToWorkspace(tempitm.TemplateGUID, _parent.CSDockPanelHosting, CurrentPluginName, _parent.WindowManagerHandler.PluginManager.PluginCollection, _parent.UserSettings.SettingName, ComboBox1.SelectedIndex) Then
                If ToolStripButton6.Checked Then
                    Me.Close()
                Else
                    Me.Activate()
                End If
            Else
                ShowPluginAddingError()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        AddItemfromDefinedDefaultStyle()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AddItemfromDefinedDefaultStyle()
    End Sub
End Class