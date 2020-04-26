'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.ComponentModel
Imports System.Drawing
Imports System.Security
Imports System.Windows.Forms
Imports CSToolCryptHelper
Imports CSToolEnvironmentManager
Imports CSToolLogLib
Imports CSToolLogLib.LogSettings

Public Class CustomActionHelper
    Public _CurrentIPHostname As String = ""
    Public _CustomActionsCollection As New List(Of CustomActionEntry)
    Public _ShowWarningOnCustomActions As Boolean = True
    Public _EnvironmentRuntimeVariables As List(Of KeyValuePair(Of String, String))
    Public _LogManager As LogLib
    Public EnvManager As New EnvironmentManager
    Public WithEvents RaiseCustomActionAsync As New BackgroundWorker

    Public Function LoadCustomItems(ByVal ContextMenuStripCtl As ContextMenuStrip, Optional ByVal ClearItems As Boolean = True) As Boolean
        Try
            If ClearItems Then
                ContextMenuStripCtl.Items.Clear()
            End If

            For Each itm As CustomActionEntry In _CustomActionsCollection
                Try
                    Dim rr As ToolStripItem
                    rr = ContextMenuStripCtl.Items.Add(itm.Name)
                    If Not itm.IconPath = "" Then
                        Try
                            Dim tt As Image
                            tt = Image.FromFile(itm.IconPath)
                            rr.Image = tt
                        Catch ex As Exception
                        End Try
                    End If

                    rr.Tag = itm
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
            If Not RaiseCustomActionAsync.IsBusy = True Then
                RaiseCustomActionAsync.RunWorkerAsync(sender.tag)
            Else
                MsgBox("Custom action is currently pending, please wait!", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RaiseCustomActionAsync_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles RaiseCustomActionAsync.DoWork
        Try
            Dim arg1 As CustomActionEntry
            arg1 = e.Argument

            RaiseCustomItem(arg1)
            e.Result = arg1
        Catch ex As Exception
        End Try
    End Sub

    Public Function RaiseCustomItem(ByVal CustomItemx As CustomActionEntry) As Boolean
        Try
            Dim isok = False

            If CustomItemx.WarningMessage = "" Then
                isok = True
            Else
                If _ShowWarningOnCustomActions = True Then
                    Dim resmsg As MsgBoxResult
                    resmsg = MsgBox(CustomItemx.WarningMessage, MsgBoxStyle.YesNo)
                    If resmsg = MsgBoxResult.Yes Then
                        isok = True
                    End If
                Else
                    isok = True
                End If
            End If

            If isok = True Then
                Dim kk As New Process
                If CustomItemx.CommandContainsArguments Then
                    Dim args As Array
                    args = EnvManager.ResolveEnvironmentVariables(_EnvironmentRuntimeVariables, CustomItemx.Command).Split(" ")
                    Dim filename As String = args(0)

                    kk.StartInfo.FileName = filename
                    Dim allargs As String = ""
                    For index = 1 To args.Length - 1
                        allargs += " " & args(index)
                    Next
                    kk.StartInfo.Arguments = allargs
                Else
                    kk.StartInfo.FileName = CustomItemx.Command
                End If

                If CustomItemx.UseShellExecute Then
                    kk.StartInfo.UseShellExecute = True
                Else
                    kk.StartInfo.UseShellExecute = False
                    kk.StartInfo.WorkingDirectory = CustomItemx.WorkingDir
                End If

                kk.StartInfo.CreateNoWindow = CustomItemx.CreateNoWindow
                kk.StartInfo.ErrorDialog = CustomItemx.ErrorDialog
                kk.StartInfo.LoadUserProfile = CustomItemx.LoadUserProfile

                If CustomItemx.UseRunAsVerb Then
                    kk.StartInfo.Verb = "runas"
                End If

                If Not CustomItemx.RunAsUsername = "" Then
                    kk.StartInfo.Domain = EnvManager.ResolveEnvironmentVariables(_EnvironmentRuntimeVariables, CustomItemx.RunAsDomain)
                    kk.StartInfo.UserName = EnvManager.ResolveEnvironmentVariables(_EnvironmentRuntimeVariables, CustomItemx.RunAsUsername)

                    Dim secpass As SecureString
                    Dim sechelper As New CredentialHandler
                    secpass = sechelper.ConvertStringInSecureString(EnvManager.ResolveEnvironmentVariables(_EnvironmentRuntimeVariables, CustomItemx.RunAsPassword))
                    secpass.MakeReadOnly()

                    kk.StartInfo.Password = secpass
                End If

                _LogManager.WriteLogEntry("Execute command " & EnvManager.ResolveEnvironmentVariables(_EnvironmentRuntimeVariables, CustomItemx.Command), GetType(CustomActionHelper), LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

                If kk.Start() Then
                    _LogManager.WriteLogEntry("Execute command " & EnvManager.ResolveEnvironmentVariables(_EnvironmentRuntimeVariables, CustomItemx.Command) & " successful.", GetType(CustomActionHelper), LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    If CustomItemx.WaitForExit Then
                        _LogManager.WriteLogEntry("Execute command " & EnvManager.ResolveEnvironmentVariables(_EnvironmentRuntimeVariables, CustomItemx.Command) & ": Waiting for termination...", GetType(CustomActionHelper), LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        kk.WaitForExit(CustomItemx.CustomItemExecTimeout)
                        _LogManager.WriteLogEntry("Execute command " & EnvManager.ResolveEnvironmentVariables(_EnvironmentRuntimeVariables, CustomItemx.Command) & ": Terminated.", GetType(CustomActionHelper), LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            _LogManager.WriteLogEntry("Execute command " & EnvManager.ResolveEnvironmentVariables(_EnvironmentRuntimeVariables, CustomItemx.Command) & " failed.", GetType(CustomActionHelper), LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Essential, Err)
            Return False
        End Try
    End Function
End Class
