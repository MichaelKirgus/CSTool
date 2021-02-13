'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.ComponentModel
Imports System.Drawing
Imports System.Management
Imports System.Windows.Forms
Imports CSToolEnvironmentManager
Imports CSToolPingHelper
Imports WMIClientInfoPlugin.Settings

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public WMIHandler As New CSToolWMIHelper.WMIHandler
    Public EnvManager As New EnvironmentManager
    Public CurrentIPHostname As String = ""
    Public CurrentIsRefresh As Boolean = False

    Delegate Sub SetTextCallback(ByVal textBox As TextBox, ByVal text As String)
    Delegate Sub SetBackColoCallback(ByVal textBox As TextBox, ByVal colorobj As Color)

    Public Sub SetText(ByVal textBox As TextBox, ByVal text As String)
        Try
            If textBox.InvokeRequired Then
                Dim d As SetTextCallback = New SetTextCallback(AddressOf SetText)
                Me.Invoke(d, New Object() {textBox, text})
            Else
                textBox.Text = text
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SetBackColor(ByVal textBox As TextBox, ByVal colorobj As Color)
        Try
            If textBox.InvokeRequired Then
                Dim d As SetBackColoCallback = New SetBackColoCallback(AddressOf SetBackColor)
                Me.Invoke(d, New Object() {textBox, colorobj})
            Else
                textBox.BackColor = colorobj
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub RaiseAction(ByVal IPOrHostname As String, Optional ByVal IsRefresh As Boolean = False)
        RefreshWMIInfo.Stop()

        CurrentIPHostname = IPOrHostname
        CurrentIsRefresh = IsRefresh

        If _Settings.ResetGUIBeforeGetData And Not IsRefresh Then
            SetAllBuildInItemsToLoading()
            ResetItems()
        End If

        If Not IsRefresh Then
            SetAllBuildInItemsToLoading()
            SetLoadingState()
        End If

        If _Settings.CheckHostIsAlive Then
            If Not PingCheck.IsBusy Then
                PingCheck.RunWorkerAsync()
            End If
        Else
            RefreshWMIInfo.Start()
        End If
    End Sub

    Public Sub StartWorkers(ByVal IPOrHostname As String, Optional ByVal IsRefresh As Boolean = False, Optional PingOK As Boolean = True)
        If Not IPOrHostname = "" And PingOK Then
            If Not GetWMIInfoCurrentUser.IsBusy Then
                GetWMIInfoCurrentUser.RunWorkerAsync(IPOrHostname)
            End If
            If Not GetWMIInfoLastBoot.IsBusy Then
                GetWMIInfoLastBoot.RunWorkerAsync(IPOrHostname)
            End If
            If Not GetWMIInfoSSDSpace.IsBusy Then
                GetWMIInfoSSDSpace.RunWorkerAsync(IPOrHostname)
            End If
            If Not GetWMIInfoIsCurrentUserAdmin.IsBusy Then
                GetWMIInfoIsCurrentUserAdmin.RunWorkerAsync(IPOrHostname)
            End If
            If (_Settings.RefreshStaticWMIDataAtRefreshInterval And IsRefresh) Or IsRefresh = False Then
                If Not GetWMIInfoManufacturer.IsBusy Then
                    GetWMIInfoManufacturer.RunWorkerAsync(IPOrHostname)
                End If
                If Not GetWMIInfoModel.IsBusy Then
                    GetWMIInfoModel.RunWorkerAsync(IPOrHostname)
                End If
                If Not GetWMIOS.IsBusy Then
                    GetWMIOS.RunWorkerAsync(IPOrHostname)
                End If
                If Not GetWMIInfoCPU.IsBusy Then
                    GetWMIInfoCPU.RunWorkerAsync(IPOrHostname)
                End If
                If Not GetWMIInfoRAM.IsBusy Then
                    GetWMIInfoRAM.RunWorkerAsync(IPOrHostname)
                End If
            End If
            If Not _Settings.CustomWMIQueryCollection.Count = 0 Then
                If Not GetCustomWMIItems.IsBusy Then
                    GetCustomWMIItems.RunWorkerAsync(IPOrHostname)
                End If
            End If
        End If
    End Sub

    Private Sub GetWMIInfoCurrentUser_DoWork(sender As Object, e As DoWorkEventArgs) Handles GetWMIInfoCurrentUser.DoWork
        e.Result = WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMICurrentLoggedOnUserClassName), _Settings.WMICurrentLoggedOnUserSection, _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority), _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation)
    End Sub

    Private Sub GetWMIInfoCurrentUser_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles GetWMIInfoCurrentUser.RunWorkerCompleted
        If Not TextBox1.Text = e.Result Then
            TextBox1.Text = e.Result
        End If
        If e.Result = "" Then
            TextBox1.BackColor = Color.LightCoral
        Else
            TextBox1.BackColor = Color.LightGreen
        End If
    End Sub

    Private Sub GetWMIInfoManufacturer_DoWork(sender As Object, e As DoWorkEventArgs) Handles GetWMIInfoManufacturer.DoWork
        e.Result = WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIManufacturerClassName), _Settings.WMIManufacturerSection, _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority), _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation)
    End Sub

    Private Sub GetWMIInfoManufacturer_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles GetWMIInfoManufacturer.RunWorkerCompleted
        If Not TextBox2.Text = e.Result Then
            TextBox2.Text = e.Result
        End If
        If e.Result = "" Then
            TextBox2.BackColor = Color.LightCoral
        Else
            TextBox2.BackColor = Color.LightGreen
        End If
    End Sub

    Private Sub GetWMIInfoModel_DoWork(sender As Object, e As DoWorkEventArgs) Handles GetWMIInfoModel.DoWork
        e.Result = WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIModelClassName), _Settings.WMIModelSection, _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority), _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation)
    End Sub

    Private Sub GetWMIInfoModel_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles GetWMIInfoModel.RunWorkerCompleted
        If Not TextBox3.Text = e.Result Then
            TextBox3.Text = e.Result
        End If
        If e.Result = "" Then
            TextBox3.BackColor = Color.LightCoral
        Else
            TextBox3.BackColor = Color.LightGreen
        End If
    End Sub

    Private Sub GetWMIOS_DoWork(sender As Object, e As DoWorkEventArgs) Handles GetWMIOS.DoWork
        e.Result = WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIOSClassName), _Settings.WMIOSSection, _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority), _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation)
    End Sub

    Private Sub GetWMIOS_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles GetWMIOS.RunWorkerCompleted
        If Not TextBox5.Text = e.Result Then
            TextBox5.Text = e.Result
        End If
        If e.Result = "" Then
            TextBox5.BackColor = Color.LightCoral
        Else
            TextBox5.BackColor = Color.LightGreen
        End If
    End Sub

    Private Sub GetWMIInfoCPU_DoWork(sender As Object, e As DoWorkEventArgs) Handles GetWMIInfoCPU.DoWork
        e.Result = WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMICPUClassName), _Settings.WMICPUSection, _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority), _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation)
    End Sub

    Private Sub GetWMIInfoCPU_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles GetWMIInfoCPU.RunWorkerCompleted
        If Not TextBox4.Text = e.Result Then
            TextBox4.Text = e.Result
        End If
        If e.Result = "" Then
            TextBox4.BackColor = Color.LightCoral
        Else
            TextBox4.BackColor = Color.LightGreen
        End If
    End Sub

    Private Sub GetWMIInfoRAM_DoWork(sender As Object, e As DoWorkEventArgs) Handles GetWMIInfoRAM.DoWork
        e.Result = WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIRAMClassName), _Settings.WMIRAMSection, _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority), _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation)
    End Sub

    Private Sub GetWMIInfoRAM_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles GetWMIInfoRAM.RunWorkerCompleted
        If Not e.Result = "" Then
            If Not TextBox6.Text = WMIHandler.GetRightSizeFormat(e.Result, 0) Then
                TextBox6.Text = WMIHandler.GetRightSizeFormat(e.Result, 0)
            End If
            If e.Result = "" Then
                TextBox6.BackColor = Color.LightCoral
            Else
                TextBox6.BackColor = Color.LightGreen
            End If
        Else
            TextBox6.BackColor = Color.LightCoral
        End If
    End Sub

    Private Sub GetWMIInfoLastBoot_DoWork(sender As Object, e As DoWorkEventArgs) Handles GetWMIInfoLastBoot.DoWork
        e.Result = WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMILastBootClassName), _Settings.WMILastBootSection, _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority), _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation)
    End Sub

    Private Sub GetWMIInfoLastBoot_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles GetWMIInfoLastBoot.RunWorkerCompleted
        If Not e.Result = "" Then
            If Not TextBox8.Text = WMIHandler.ConvertWMIDateToDateTime(e.Result).ToShortDateString & " " & WMIHandler.ConvertWMIDateToDateTime(e.Result).ToShortTimeString Then
                TextBox8.Text = WMIHandler.ConvertWMIDateToDateTime(e.Result).ToShortDateString & " " & WMIHandler.ConvertWMIDateToDateTime(e.Result).ToShortTimeString
            End If
            If e.Result = "" Then
                TextBox8.BackColor = Color.LightCoral
            Else
                TextBox8.BackColor = Color.LightGreen
            End If
        Else
            TextBox8.BackColor = Color.LightCoral
        End If
    End Sub

    Private Sub GetWMIInfoSSDSpace_DoWork(sender As Object, e As DoWorkEventArgs) Handles GetWMIInfoSSDSpace.DoWork
        Dim result As New List(Of Object)
        result.Add(WMIHandler.CheckIfDriveIsSSD(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIDetectSSDScope),
                                                EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIDetectSSDQuery),
                                                EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIDetectSSDWinSATValue), _Settings.WMIQueryTimeout,
                                                _Settings.UseCustomWMIConnectionOptions, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername),
                                                EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword),
                                                EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority),
                                                _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation))

        result.Add(WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMITotalOSDriveCapacityClassName),
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMITotalOSDriveCapacitySection), _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions,
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername),
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword),
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority),
        _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation))

        result.Add(WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIFreeOSDriveSpaceClassName),
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIFreeOSDriveSpaceSection), _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions,
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername),
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword),
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority),
        _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation))

        e.Result = result
    End Sub

    Private Sub GetWMIInfoSSDSpace_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles GetWMIInfoSSDSpace.RunWorkerCompleted
        If e.Result(0) = True Then
            hddssdgroup.Text = "SSD"
        Else
            hddssdgroup.Text = "HDD"
        End If

        If Not e.Result(1) = "" And Not e.Result(2) = "" Then
            Dim totalspace As Int64
            Dim freespace As Int64
            totalspace = e.Result(1)
            freespace = e.Result(2)

            Dim diff As Int64
            diff = totalspace - freespace

            LblC.Text = WMIHandler.GetRightSizeFormat(totalspace) & " / " & WMIHandler.GetRightSizeFormat(diff) & " / " & WMIHandler.GetRightSizeFormat(freespace)

            UsedSpaceC.Style = Windows.Forms.ProgressBarStyle.Continuous
            UsedSpaceC.Maximum = Math.Round(totalspace / 10000, 0)
            UsedSpaceC.Value = Math.Round(diff / 10000, 0)
        Else
            UsedSpaceC.Style = Windows.Forms.ProgressBarStyle.Continuous
            UsedSpaceC.Value = 0
            LblC.Text = "Error"
        End If
    End Sub

    Private Sub RefreshWMIInfo_Tick(sender As Object, e As EventArgs) Handles RefreshWMIInfo.Tick
        RaiseAction(CurrentIPHostname, True)
    End Sub

    Public Sub RefreshGUI()
        RefreshWMIInfo.Interval = _Settings.WMIRefreshInterval
        GroupBox1.Width = _Settings.CurrentLoggedOnUserSize
        GroupBox2.Width = _Settings.CurrentManufacturerSize
        GroupBox3.Width = _Settings.CurrentModelSize
        GroupBox5.Width = _Settings.CurrentOSSize
        GroupBox4.Width = _Settings.CurrentCPUSize
        GroupBox6.Width = _Settings.CurrentRAMSize
        GroupBox7.Width = _Settings.CurrentLastBootSize
        hddssdgroup.Width = _Settings.CurrentSSDDriveSize

        If Not _Settings.CustomWMIQueryCollection.Count = 0 Then
            LoadItems()
        Else
            ClearCustomItems()
        End If
    End Sub

    Public Function ClearCustomItems() As Boolean
        Try
            For Each item As Control In FlowLayoutPanel1.Controls
                If Not IsNothing(item.Tag) Then
                    If item.Tag = "custom" Then
                        FlowLayoutPanel1.Controls.Remove(item)
                    End If
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SetAllBuildInItemsToFailed() As Boolean
        Try
            UsedSpaceC.Style = Windows.Forms.ProgressBarStyle.Continuous
            UsedSpaceC.Value = 0
            LblC.Text = "Error"
            TextBox1.Text = "Error"
            TextBox2.Text = "Error"
            TextBox3.Text = "Error"
            TextBox4.Text = "Error"
            TextBox5.Text = "Error"
            TextBox6.Text = "Error"
            TextBox8.Text = "Error"
            TextBox1.BackColor = Color.LightCoral
            TextBox2.BackColor = Color.LightCoral
            TextBox3.BackColor = Color.LightCoral
            TextBox4.BackColor = Color.LightCoral
            TextBox5.BackColor = Color.LightCoral
            TextBox6.BackColor = Color.LightCoral
            TextBox8.BackColor = Color.LightCoral

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SetAllBuildInItemsToLoading() As Boolean
        Try
            hddssdgroup.Text = "HDD/SSD"
            UsedSpaceC.Style = Windows.Forms.ProgressBarStyle.Marquee
            LblC.Text = ""
            TextBox1.Text = "No data"
            TextBox2.Text = "No data"
            TextBox3.Text = "No data"
            TextBox4.Text = "No data"
            TextBox5.Text = "No data"
            TextBox6.Text = "No data"
            TextBox8.Text = "No data"
            TextBox1.BackColor = Color.LightYellow
            TextBox2.BackColor = Color.LightYellow
            TextBox3.BackColor = Color.LightYellow
            TextBox4.BackColor = Color.LightYellow
            TextBox5.BackColor = Color.LightYellow
            TextBox6.BackColor = Color.LightYellow
            TextBox8.BackColor = Color.LightYellow

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadItems() As Boolean
        Try
            ClearCustomItems()

            For index = 0 To _Settings.CustomWMIQueryCollection.Count - 1
                Dim gg As New CustomInfoItm
                gg.Tag = "custom"
                gg.GroupBox1.Text = _Settings.CustomWMIQueryCollection(index).DisplayName
                gg.Width = _Settings.CustomWMIQueryCollection(index).DisplaySize
                gg.ValueTxt.Tag = _Settings.CustomWMIQueryCollection(index)

                FlowLayoutPanel1.Controls.Add(gg)
                gg.Dock = DockStyle.Top
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ResetItems() As Boolean
        Try
            For index = 0 To FlowLayoutPanel1.Controls.Count - 1
                Dim ctl As Control
                ctl = FlowLayoutPanel1.Controls(index)
                If Not IsNothing(ctl.Tag) Then
                    If ctl.Tag = "custom" Then
                        Dim gg As CustomInfoItm
                        gg = ctl
                        gg = FlowLayoutPanel1.Controls(index)
                        gg.ValueTxt.Text = "No data"
                        gg.ValueTxt.BackColor = Color.FromName("Control")
                    End If
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SetLoadingState() As Boolean
        Try
            For index = 0 To FlowLayoutPanel1.Controls.Count - 1
                Dim ctl As Control
                ctl = FlowLayoutPanel1.Controls(index)
                If Not IsNothing(ctl.Tag) Then
                    If ctl.Tag = "custom" Then
                        Dim gg As CustomInfoItm
                        gg = ctl
                        gg = FlowLayoutPanel1.Controls(index)
                        gg.ValueTxt.Text = "No data"
                        gg.ValueTxt.BackColor = Color.LightYellow
                    End If
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SetErrorState() As Boolean
        Try
            For index = 0 To FlowLayoutPanel1.Controls.Count - 1
                Dim ctl As Control
                ctl = FlowLayoutPanel1.Controls(index)
                If Not IsNothing(ctl.Tag) Then
                    If ctl.Tag = "custom" Then
                        Dim gg As CustomInfoItm
                        gg = ctl
                        gg = FlowLayoutPanel1.Controls(index)
                        gg.ValueTxt.Text = "Error"
                        gg.ValueTxt.BackColor = Color.LightCoral
                    End If
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WMIHandler._LogInstanceHandler = _ParentInstance.CurrentLogInstance
        RefreshGUI()
    End Sub

    Private Sub GetWMIInfoIsCurrentUserAdmin_DoWork(sender As Object, e As DoWorkEventArgs) Handles GetWMIInfoIsCurrentUserAdmin.DoWork
        e.Result = WMIHandler.CheckIfUserIsAdmin(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIIsCurrentUserAdminScope),
                                                 EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIIsCurrentUserAdminQuery),
                                                 WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMICurrentLoggedOnUserClassName),
                                                _Settings.WMICurrentLoggedOnUserSection), _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions,
                                                EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername),
                                                EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword),
                                                EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority),
                                                _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation)
    End Sub

    Private Sub GetWMIInfoIsCurrentUserAdmin_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles GetWMIInfoIsCurrentUserAdmin.RunWorkerCompleted
        If e.Result Then
            TextBox1.Text += " (Admin)"
        Else
            TextBox1.Text = TextBox1.Text.Replace(" (Admin)", "")
        End If
    End Sub

    Private Sub GetCustomItems_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles GetCustomWMIItems.DoWork
        Try
            For index = 0 To FlowLayoutPanel1.Controls.Count - 1
                Dim resultstr As String = ""
                Dim ctl As Control
                ctl = FlowLayoutPanel1.Controls(index)
                If Not IsNothing(ctl.Tag) Then
                    If ctl.Tag = "custom" Then
                        Dim gg As CustomInfoItm
                        Dim CustomQItem As CustomWMIQueryItem
                        gg = ctl
                        CustomQItem = gg.ValueTxt.Tag
                        If (CustomQItem.RefreshAtRefreshInterval And CurrentIsRefresh) Or CurrentIsRefresh = False Then
                            resultstr = WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, CustomQItem.WMIClassName),
                                                              EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, CustomQItem.WMISection), _Settings.WMIQueryTimeout,
                                                              _Settings.UseCustomWMIConnectionOptions, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables,
                                                              _Settings.CustomWMIConnectionOptionsUsername),
                                                              EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword),
                                                              EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority),
                                                              _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation)
                            SetText(gg.ValueTxt, resultstr)
                            If resultstr = "" Then
                                SetBackColor(gg.ValueTxt, Color.LightCoral)
                            Else
                                SetBackColor(gg.ValueTxt, Color.LightGreen)
                            End If
                        End If
                    End If
                Else
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PingCheck_DoWork(sender As Object, e As DoWorkEventArgs) Handles PingCheck.DoWork
        Dim PingObj As New PingHelper
        e.Result = PingObj.Ping(CurrentIPHostname, _Settings.CheckHostIsAlivePingTimeout, False)
    End Sub

    Private Sub PingCheck_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles PingCheck.RunWorkerCompleted
        Try
            If e.Result Then
                BackColor = Color.LightGreen
                StartWorkers(CurrentIPHostname, CurrentIsRefresh)
            Else
                BackColor = Color.LightCoral
                SetAllBuildInItemsToFailed()
                SetErrorState()
            End If
            RefreshWMIInfo.Start()
        Catch ex As Exception
        End Try
    End Sub
End Class
