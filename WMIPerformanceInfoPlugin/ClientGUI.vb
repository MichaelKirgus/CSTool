'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports System.Windows.Forms
Imports CSToolEnvironmentManager
Imports CSToolPingHelper

Public Class ClientGUI
    Public _Settings As SettingsClass
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
        End If

        If Not IsRefresh Then
            SetAllBuildInItemsToLoading()
        End If

        If _Settings.CheckHostIsAlive Then
            If Not PingCheck.IsBusy Then
                PingCheck.RunWorkerAsync()
            End If
        Else
            RefreshWMIInfo.Start()
        End If
    End Sub

    Public Function SetAllBuildInItemsToFailed() As Boolean
        Try
            MemoryBar.Style = Windows.Forms.ProgressBarStyle.Continuous
            MemoryBar.Value = 0
            CPULoadBar.Style = Windows.Forms.ProgressBarStyle.Continuous
            CPULoadBar.Value = 0
            BatteryLevelBar.Style = Windows.Forms.ProgressBarStyle.Continuous
            BatteryLevelBar.Value = 0
            PageFileBar.Style = Windows.Forms.ProgressBarStyle.Continuous
            PageFileBar.Value = 0
            MemoryConsumptionLbl.Text = "Error"
            CPULoadPercentLbl.Text = "Error"
            BatteryLevelLbl.Text = "Error"
            PageFileLbl.Text = "Error"

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SetAllBuildInItemsToLoading() As Boolean
        Try
            MemoryBar.Style = Windows.Forms.ProgressBarStyle.Marquee
            CPULoadBar.Style = Windows.Forms.ProgressBarStyle.Marquee
            BatteryLevelBar.Style = Windows.Forms.ProgressBarStyle.Marquee
            PageFileBar.Style = ProgressBarStyle.Marquee
            MemoryConsumptionLbl.Text = "No data"
            CPULoadPercentLbl.Text = "No data"
            BatteryLevelLbl.Text = "No data"
            PageFileLbl.Text = "No data"

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub RefreshGUI()
        RefreshWMIInfo.Interval = _Settings.WMIRefreshInterval
        GroupBox1.Width = _Settings.CPUUsageSize
        GroupBox2.Width = _Settings.SystemMemorySize
        GroupBox3.Width = _Settings.BatterySize
        GroupBox3.Visible = _Settings.ShowWMIBatteryCharge
    End Sub

    Private Sub GetPerformanceWMIData_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles GetPerformanceWMIData.DoWork
        Dim result As New List(Of Object)

        result.Add(WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMICPUUsagePercentClassName),
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMICPUUsagePercentSection), _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions,
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername),
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword),
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority),
        _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation))

        result.Add(WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMITotalSystemMemoryClassName),
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMITotalSystemMemorySection), _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions,
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername),
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword),
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority),
        _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation))

        result.Add(WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIFreeSystemMemoryClassName),
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIFreeSystemMemorySection), _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions,
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername),
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword),
        EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority),
        _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation))

        If _Settings.ShowWMIBatteryCharge Then
            result.Add(WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIBatteryEstimatedChargeRemainingClassName),
            EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIBatteryEstimatedChargeRemainingSection), _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions,
            EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername),
            EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword),
            EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority),
            _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation))
        End If

        If _Settings.ShowPageFileInfo Then
            result.Add(WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIPageFileAllocatedBaseSizeClassName),
            EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIPageFileAllocatedBaseSizeSection), _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions,
            EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername),
            EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword),
            EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority),
            _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation))

            result.Add(WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIPageFileCurrentUsageClassName),
            EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMIPageFileCurrentUsageSection), _Settings.WMIQueryTimeout, _Settings.UseCustomWMIConnectionOptions,
            EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername),
            EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword),
            EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority),
            _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation))
        End If

        e.Result = result
    End Sub

    Private Sub PingCheck_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles PingCheck.DoWork
        Dim PingObj As New PingHelper
        e.Result = PingObj.Ping(CurrentIPHostname, _Settings.CheckHostIsAlivePingTimeout, False)
    End Sub

    Private Sub PingCheck_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles PingCheck.RunWorkerCompleted
        Try
            If e.Result Then
                If Not GetPerformanceWMIData.IsBusy Then
                    GetPerformanceWMIData.RunWorkerAsync(CurrentIPHostname)
                End If
            Else
                SetAllBuildInItemsToFailed()
            End If
            RefreshWMIInfo.Start()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RefreshWMIInfo_Tick(sender As Object, e As EventArgs) Handles RefreshWMIInfo.Tick
        RaiseAction(CurrentIPHostname, True)
    End Sub

    Private Sub GetPerformanceWMIData_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles GetPerformanceWMIData.RunWorkerCompleted
        Try
            Dim resultobj As List(Of Object)
            resultobj = e.Result

            If Not resultobj(0) = "" Then
                CPULoadBar.Style = Windows.Forms.ProgressBarStyle.Continuous
                CPULoadBar.Value = resultobj(0)
                CPULoadPercentLbl.Text = resultobj(0) & " %"
            Else
                CPULoadBar.Style = Windows.Forms.ProgressBarStyle.Continuous
                CPULoadBar.Value = 0
                CPULoadPercentLbl.Text = "No data"
            End If
            If Not resultobj(1) = "" And Not resultobj(2) = "" Then
                Dim totalmem As Int64
                Dim freemem As Int64
                totalmem = resultobj(1)
                freemem = resultobj(2)

                Dim usedmem As Int64
                usedmem = totalmem - freemem

                MemoryBar.Style = Windows.Forms.ProgressBarStyle.Continuous
                MemoryBar.Maximum = totalmem
                MemoryBar.Value = usedmem

                Dim lblstr As String
                lblstr = WMIHandler.GetRightSizeFormat(totalmem * 1024) & "/" & WMIHandler.GetRightSizeFormat(usedmem * 1024) & "/" & WMIHandler.GetRightSizeFormat(freemem * 1024)

                MemoryConsumptionLbl.Text = lblstr
            Else
                MemoryBar.Style = Windows.Forms.ProgressBarStyle.Continuous
                MemoryBar.Value = 0
                MemoryConsumptionLbl.Text = "No data"
            End If
            Dim offsetcnt As Integer = 0
            If _Settings.ShowWMIBatteryCharge Then
                offsetcnt += 1
                If Not resultobj(3) = "" Then
                    Dim currlevel As Integer
                    currlevel = 100 - resultobj(3)

                    BatteryLevelBar.Style = ProgressBarStyle.Continuous
                    BatteryLevelBar.Value = currlevel
                    BatteryLevelLbl.Text = resultobj(3) & " remaining, " & currlevel & " % full"
                Else
                    BatteryLevelBar.Style = ProgressBarStyle.Continuous
                    BatteryLevelBar.Value = 0
                    BatteryLevelLbl.Text = "No data"
                End If
            End If
            If _Settings.ShowPageFileInfo Then
                If Not resultobj(3 + offsetcnt) = "" And Not resultobj(4 + offsetcnt) = "" Then
                    Dim currallocated As Int64
                    Dim currused As Int64
                    currallocated = resultobj(3 + offsetcnt)
                    currused = resultobj(4 + offsetcnt)

                    PageFileBar.Style = Windows.Forms.ProgressBarStyle.Continuous
                    PageFileBar.Maximum = currallocated
                    PageFileBar.Value = currused

                    PageFileLbl.Text = WMIHandler.GetRightSizeFormat(currallocated * 1024 * 1024) & " allocated, " & WMIHandler.GetRightSizeFormat(currused * 1024 * 1024) & " used."
                Else
                    PageFileBar.Style = Windows.Forms.ProgressBarStyle.Continuous
                    PageFileBar.Value = 0
                    PageFileLbl.Text = "No data"
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WMIHandler._LogInstanceHandler = _ParentInstance.CurrentLogInstance
        RefreshGUI()
    End Sub
End Class
