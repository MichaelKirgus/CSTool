'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Runtime.InteropServices
Imports CSToolPluginLib
Imports CSToolWindowManager

Public Class SettingsManager
    Public _Parent As MainForm
    Public _CurrentInstance As CSToolPluginLib.ICSToolInterface
    Public _CurrentInstanceEdited As Boolean = False

    <DllImport("uxtheme", CharSet:=CharSet.Unicode)>
    Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal textSubAppName As String, ByVal textSubIdList As String) As Integer
    End Function
    Public Sub SetUXThemeForAllListViews()
        Try
            SetWindowTheme(PlugList.Handle, "explorer", Nothing)
            SetWindowTheme(EnvVarList.Handle, "explorer", Nothing)
        Catch ex As Exception
        End Try
    End Sub

    Public Function LoadPluginsToGUI(ByVal ExListView As ListView) As Boolean
        ExListView.BeginUpdate()
        ExListView.Items.Clear()

        Try
            Dim EnvironmentPlugs As List(Of CSToolPluginLib.ICSToolInterface)
            EnvironmentPlugs = _Parent.WindowManagerHandler.GetPluginsByType(CSToolPluginLib.ICSToolInterface.PluginTypeEnum.EnvironmentManager, _Parent.WindowManagerHandler.PluginManager.PluginCollection)
            Dim CredentialPlugs As List(Of CSToolPluginLib.ICSToolInterface)
            CredentialPlugs = _Parent.WindowManagerHandler.GetPluginsByType(CSToolPluginLib.ICSToolInterface.PluginTypeEnum.CredentialManager, _Parent.WindowManagerHandler.PluginManager.PluginCollection)
            Dim GUIWindowPlugs As List(Of CSToolPluginLib.ICSToolInterface)
            GUIWindowPlugs = _Parent.WindowManagerHandler.GetPluginsByType(CSToolPluginLib.ICSToolInterface.PluginTypeEnum.GUIWindow, _Parent.WindowManagerHandler.PluginManager.PluginCollection)

            If Not EnvironmentPlugs.Count = 0 Then
                For index = 0 To EnvironmentPlugs.Count - 1
                    Dim jj As New ListViewItem
                    jj.Group = ExListView.Groups(0)
                    jj.Text = EnvironmentPlugs(index).PluginName
                    jj.SubItems.Add(EnvironmentPlugs(index).PluginVersion)
                    jj.SubItems.Add(EnvironmentPlugs(index).PluginPublisher)
                    jj.SubItems.Add(EnvironmentPlugs(index).PluginGUID)
                    jj.Tag = EnvironmentPlugs(index).PluginGUID

                    ExListView.Items.Add(jj)
                Next
            End If

            If Not CredentialPlugs.Count = 0 Then
                For index = 0 To CredentialPlugs.Count - 1
                    Dim jj As New ListViewItem
                    jj.Group = ExListView.Groups(1)
                    jj.Text = CredentialPlugs(index).PluginName
                    jj.SubItems.Add(CredentialPlugs(index).PluginVersion)
                    jj.SubItems.Add(CredentialPlugs(index).PluginPublisher)
                    jj.SubItems.Add(CredentialPlugs(index).PluginGUID)
                    jj.Tag = CredentialPlugs(index).PluginGUID

                    ExListView.Items.Add(jj)
                Next
            End If

            If Not GUIWindowPlugs.Count = 0 Then
                For index = 0 To GUIWindowPlugs.Count - 1
                    Dim jj As New ListViewItem
                    jj.Group = ExListView.Groups(2)
                    jj.Text = GUIWindowPlugs(index).PluginName
                    jj.SubItems.Add(GUIWindowPlugs(index).PluginVersion)
                    jj.SubItems.Add(GUIWindowPlugs(index).PluginPublisher)
                    jj.SubItems.Add(GUIWindowPlugs(index).PluginGUID)
                    jj.Tag = GUIWindowPlugs(index).PluginGUID

                    ExListView.Items.Add(jj)
                Next
            End If

            ExListView.EndUpdate()
            Return True
        Catch ex As Exception
            ExListView.EndUpdate()
            Return False
        End Try
    End Function

    Private Sub SettingsManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserSettingBox.Text = _Parent.UserSettings.SettingName

        LoadPluginsToGUI(PlugList)
        If Not PlugList.Items.Count = 0 Then
            PlugList.Items(0).Selected = True
        End If

        SetUXThemeForAllListViews()
    End Sub

    Public Function LoadEnvVarsToGUI(ByVal PluginInstance As CSToolPluginLib.ICSToolInterface, ByVal ExListView As ListView) As Boolean
        Try
            ExListView.BeginUpdate()
            ExListView.Items.Clear()

            If PluginInstance IsNot Nothing Then
                Dim EnvTemp As New List(Of EnvironmentEntry)
                EnvTemp.AddRange(PluginInstance.EnvironmentProviderClass.EnvironmentVariables)

                For index = 0 To EnvTemp.Count - 1
                    Dim qq As New ListViewItem
                    qq.Text = EnvTemp(index).ValueName
                    qq.SubItems.Add(EnvTemp(index).ValueData)
                    qq.SubItems.Add(EnvTemp(index).IsSystemEnvironmentData.ToString)
                    qq.Checked = EnvTemp(index).Enabled

                    ExListView.Items.Add(qq)
                Next
            End If

            ExListView.EndUpdate()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SaveEnvVarsFromGUI(ByVal PluginInstance As CSToolPluginLib.ICSToolInterface, ByVal ExListView As ListView) As Boolean
        Try
            If PluginInstance IsNot Nothing Then
                If _CurrentInstanceEdited And PluginInstance.EnvironmentProviderClass.IsProviderReadOnly = False Then
                    PluginInstance.EnvironmentProviderClass.EnvironmentVariables.Clear()

                    For index = 0 To ExListView.Items.Count - 1
                        Dim hh As New EnvironmentEntry
                        hh.Enabled = ExListView.Items(index).Checked
                        hh.ValueName = ExListView.Items(index).Text
                        hh.ValueData = ExListView.Items(index).SubItems(1).Text
                        hh.IsSystemEnvironmentData = ExListView.Items(index).SubItems(2).Text
                        PluginInstance.EnvironmentProviderClass.EnvironmentVariables.Add(hh)
                    Next
                End If

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub PlugList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PlugList.SelectedIndexChanged
        Try
            Dim CurrSelectedPlug As CSToolPluginLib.ICSToolInterface
            Dim CurrSelGUID As String
            CurrSelGUID = PlugList.SelectedItems(0).Tag
            CurrSelectedPlug = _Parent.WindowManagerHandler.GetPluginByGUID(CurrSelGUID, _Parent.WindowManagerHandler.PluginManager.PluginCollection)
            ToolStripButton6.Enabled = False
            Select Case CurrSelectedPlug.PluginType
                Case CSToolPluginLib.ICSToolInterface.PluginTypeEnum.GUIWindow
                    EnvVarList.Items.Clear()
                    ClearEditCtl()
                    _CurrentInstance = Nothing
                Case ICSToolInterface.PluginTypeEnum.EnvironmentManager
                    SaveEnvVarsFromGUI(_CurrentInstance, EnvVarList)
                    _CurrentInstance = CurrSelectedPlug
                    ClearEditCtl()
                    LoadEnvVarsToGUI(_CurrentInstance, EnvVarList)
                Case ICSToolInterface.PluginTypeEnum.CredentialManager
                    ToolStripButton6.Enabled = True
            End Select

            If _CurrentInstance IsNot Nothing Then
                SetGUIEditState(Not _CurrentInstance.EnvironmentProviderClass.IsProviderReadOnly)
                If Not _CurrentInstance.PluginType = ICSToolInterface.PluginTypeEnum.GUIWindow Then
                    PropertyGrid1.SelectedObject = CurrSelectedPlug.UserSettingsClass
                Else
                    PropertyGrid1.SelectedObject = Nothing
                End If
            Else
                SetGUIEditState(False)
                PropertyGrid1.SelectedObject = Nothing
            End If

            If Not EnvVarList.Items.Count = 0 Then
                EnvVarList.Items(0).Selected = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SetGUIEditState(ByVal EditAllowed As Boolean)
        SplitContainer2.Panel2.Enabled = EditAllowed
        SplitContainer2.Panel2Collapsed = Not EditAllowed
        EnvVarList.CheckBoxes = EditAllowed
        ToolStripButton4.Enabled = EditAllowed
        ToolStripButton5.Enabled = EditAllowed
    End Sub

    Private Sub EnvVarList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles EnvVarList.SelectedIndexChanged
        'Set GUI state
        Try
            If Not EnvVarList.Items.Count = 0 Then
                Dim jj As ListViewItem
                jj = EnvVarList.SelectedItems(0)
                CheckBox1.Checked = jj.Checked
                TextBox1.Text = jj.Text
                TextBox2.Text = jj.SubItems(1).Text
                CheckBox2.Checked = jj.SubItems(2).Text
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ClearEditCtl()
        Try
            CheckBox1.Checked = False
            TextBox1.Text = ""
            TextBox2.Text = ""
            CheckBox2.Checked = False
        Catch ex As Exception
        End Try
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        EnvVarList.SelectedItems(0).Checked = CheckBox1.Checked
        _CurrentInstanceEdited = True
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        EnvVarList.SelectedItems(0).Text = TextBox1.Text
        _CurrentInstanceEdited = True
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        EnvVarList.SelectedItems(0).SubItems(1).Text = TextBox2.Text
        _CurrentInstanceEdited = True
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        EnvVarList.SelectedItems(0).SubItems(2).Text = CheckBox2.Checked
        _CurrentInstanceEdited = True
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        Dim jj As New ListViewItem
        jj.Text = "<new>"
        jj.SubItems.Add("")
        jj.SubItems.Add("False")
        jj.Checked = True

        EnvVarList.Items.Add(jj)
        _CurrentInstanceEdited = True
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        EnvVarList.Items.Remove(EnvVarList.SelectedItems(0))
        _CurrentInstanceEdited = True
    End Sub

    Private Sub EnvVarList_ItemCheck(sender As Object, e As ItemCheckEventArgs)
        _CurrentInstanceEdited = True
    End Sub

    Private Sub EnvVarList_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles EnvVarList.ItemChecked
        _CurrentInstanceEdited = True
    End Sub

    Private Sub SettingsManager_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SaveEnvVarsFromGUI(_CurrentInstance, EnvVarList)
    End Sub
    Public Sub OpenCredWindow()
        Try
            Dim CurrSelectedPlug As CSToolPluginLib.ICSToolInterface
            Dim CurrSelGUID As String
            CurrSelGUID = PlugList.SelectedItems(0).Tag
            CurrSelectedPlug = _Parent.WindowManagerHandler.GetPluginByGUID(CurrSelGUID, _Parent.WindowManagerHandler.PluginManager.PluginCollection)

            CurrSelectedPlug.UserCredentialWindow.ShowDialog()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        OpenCredWindow()
    End Sub
End Class