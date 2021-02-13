'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.DirectoryServices
Imports System.DirectoryServices.Protocols
Imports System.Drawing
Imports System.Net
Imports CSToolCryptHelper
Imports CSToolEnvironmentManager
Imports CSToolWMIHelper
Imports CSToolLogLib.LogSettings

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public WMIHandler As New WMIHandler
    Public EnvManager As New EnvironmentManager
    Public CurrentIPHostname As String = ""
    Public CurrentUsernameWithDomain As String = ""
    Public CurrentUsernameWithoutDomain As String = ""

    Public Sub RaiseAction(ByVal IPOrHostname As String, Optional ByVal IsRefresh As Boolean = False)
        CurrentIPHostname = IPOrHostname

        If _Settings.ResetGUIBeforeGetData And Not IsRefresh Then
            ResetItems()
        End If

        If Not IPOrHostname = "" Then
            If Not _Settings.LDAPInfoEntryCollection.Count = 0 Then
                If _Settings.GetCurrentUserOverWMI Then
                    If Not GetWMIInfoCurrentUser.IsBusy Then
                        _ParentInstance.CurrentLogInstance.WriteLogEntry("Start WMI-Worker for host " & CurrentIPHostname, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        GetWMIInfoCurrentUser.RunWorkerAsync(IPOrHostname)
                    End If
                Else
                    If Not GetADInfoAsync.IsBusy Then
                        _ParentInstance.CurrentLogInstance.WriteLogEntry("Start LDAP-Worker for host " & CurrentIPHostname, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        SetLoadingState()
                        GetADInfoAsync.RunWorkerAsync()
                    End If
                End If
            End If

            If _Settings.RefreshADInformation Then
                RefreshLDAPinfo.Start()
            End If
        Else
            RefreshLDAPinfo.Stop()
            ResetItems()
        End If
    End Sub

    Public Sub RefreshGUI()
        LoadItems()
        ResetItems()

        RefreshLDAPinfo.Interval = _Settings.RefreshADInformationInterval
    End Sub

    Public Function LoadItems() As Boolean
        Try
            FlowLayoutPanel1.Controls.Clear()

            For index = 0 To _Settings.LDAPInfoEntryCollection.Count - 1
                Dim gg As New ADInfoItem
                gg.GroupBox1.Text = _Settings.LDAPInfoEntryCollection(index).DisplayName
                gg.Width = _Settings.LDAPInfoEntryCollection(index).DisplaySize

                FlowLayoutPanel1.Controls.Add(gg)
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ResetItems() As Boolean
        Try
            For index = 0 To FlowLayoutPanel1.Controls.Count - 1
                Dim gg As ADInfoItem
                gg = FlowLayoutPanel1.Controls(index)
                gg.ValueTxt.Text = "No data"
                gg.ValueTxt.BackColor = Color.FromName("Control")
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SetLoadingState() As Boolean
        Try
            For index = 0 To FlowLayoutPanel1.Controls.Count - 1
                Dim gg As ADInfoItem
                gg = FlowLayoutPanel1.Controls(index)
                gg.ValueTxt.BackColor = Color.LightYellow
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetADInfo() As Object
        Try
            If _Settings.LDAPType = LDAPTypeEnum.LDAPS Then
                _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAPS/WMI: Connect to server...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

                Dim ldapconn As LdapConnection = Nothing
                Dim ident As New LdapDirectoryIdentifier(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.LDAPSServer), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.LDAPSServerPort))
                If _Settings.DirectorySearcherCustomAuthentification Then
                    Dim netcreds As New NetworkCredential
                    netcreds.UserName = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.DirectorySearcherCustomAuthentificationUsername)
                    netcreds.Password = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.DirectorySearcherCustomAuthentificationPassword)
                    ldapconn = New LdapConnection(ident, netcreds, _Settings.LDAPSServerAuthType)
                Else
                    ldapconn = New LdapConnection(ident)
                End If

                ldapconn.Bind()
                Dim searcher As New SearchRequest()
                Dim sPath As String = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.ADInfoRootEntry)

                Dim tmpruntimevars As New List(Of KeyValuePair(Of String, String))
                tmpruntimevars.Add(New KeyValuePair(Of String, String)("%CurrentUsernameWithDomain%", CurrentUsernameWithDomain))
                tmpruntimevars.Add(New KeyValuePair(Of String, String)("%CurrentUsernameWithoutDomain%", CurrentUsernameWithoutDomain))

                _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAPS/WMI: Combine environment strings with username and domain...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                searcher.Filter = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.DirectorySearcherFilterString, tmpruntimevars)
                searcher.SizeLimit = _Settings.SizeLimit

                _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAPS/WMI: Building searching properties...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                For index = 0 To _Settings.LDAPInfoEntryCollection.Count - 1
                    searcher.Attributes.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.LDAPInfoEntryCollection(index).Attribute, tmpruntimevars))
                Next

                _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAPS: Waiting for LDAP-Result...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                Dim respobj As SearchResponse
                respobj = ldapconn.SendRequest(searcher)
                Dim searchentry As SearchResultEntry
                searchentry = respobj.Entries(0)

                _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAP: Result received.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                Return searchentry
            Else
                _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAP/WMI: Connect to server...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                Dim sPath As String = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.ADInfoRootEntry)

                Dim de As DirectoryEntry = Nothing
                If _Settings.DirectorySearcherCustomAuthentification Then
                    Dim enchandler As New CredentialHandler
                    Dim pPwd As IntPtr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(enchandler.ConvertStringInSecureString(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.DirectorySearcherCustomAuthentificationPassword)))
                    de = New DirectoryEntry(sPath, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.DirectorySearcherCustomAuthentificationUsername), System.Runtime.InteropServices.Marshal.PtrToStringBSTR(pPwd))
                Else
                    de = New DirectoryEntry(sPath)
                End If

                Dim searcher As New DirectorySearcher(de)
                Dim result As SearchResult

                Dim tmpruntimevars As New List(Of KeyValuePair(Of String, String))
                tmpruntimevars.Add(New KeyValuePair(Of String, String)("%CurrentUsernameWithDomain%", CurrentUsernameWithDomain))
                tmpruntimevars.Add(New KeyValuePair(Of String, String)("%CurrentUsernameWithoutDomain%", CurrentUsernameWithoutDomain))

                _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAP/WMI: Combine environment strings with username and domain...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                searcher.Filter = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.DirectorySearcherFilterString, tmpruntimevars)
                searcher.ClientTimeout = New TimeSpan(0, 0, 0, 0, _Settings.ClientTimeout)
                searcher.PageSize = _Settings.PageSize
                searcher.ServerPageTimeLimit = New TimeSpan(0, 0, 0, 0, _Settings.ServerPageTimeLimit)
                searcher.ServerTimeLimit = New TimeSpan(0, 0, 0, 0, _Settings.ServerTimeLimit)
                searcher.SizeLimit = _Settings.SizeLimit
                searcher.CacheResults = _Settings.CacheResults

                _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAP/WMI: Building searching properties...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                For index = 0 To _Settings.LDAPInfoEntryCollection.Count - 1
                    searcher.PropertiesToLoad.Add(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.LDAPInfoEntryCollection(index).Attribute, tmpruntimevars))
                Next

                _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAP: Waiting for LDAP-Result...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                result = searcher.FindOne()
                _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAP: Result received.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

                Return result
            End If
        Catch ex As Exception
            _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAP: Error." & CurrentIPHostname, Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
            Return Nothing
        End Try
    End Function

    Private Sub GetADInfoAsync_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles GetADInfoAsync.DoWork
        e.Result = GetADInfo()
    End Sub

    Private Sub GetWMIInfoCurrentUser_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles GetWMIInfoCurrentUser.DoWork
        e.Result = WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMICurrentLoggedOnUserClassName),
                                         _Settings.WMICurrentLoggedOnUserSection, _Settings.WMIQueryTimeout,
                                         _Settings.UseCustomWMIConnectionOptions, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername),
                                         EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword),
                                         EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority),
                                         _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation)
    End Sub

    Private Sub GetWMIInfoCurrentUser_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles GetWMIInfoCurrentUser.RunWorkerCompleted
        If Not e.Result = "" Then
            _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAP/WMI: Add current username and domain to environment...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            Dim userstr As String
            userstr = e.Result
            CurrentUsernameWithDomain = userstr
            Try
                CurrentUsernameWithoutDomain = userstr.Split("\")(1)
            Catch ex As Exception
            End Try

            If Not GetADInfoAsync.IsBusy Then
                GetADInfoAsync.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub GetADInfoAsync_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles GetADInfoAsync.RunWorkerCompleted
        Try
            If _Settings.LDAPType = LDAPTypeEnum.LDAP Then
                Dim result As SearchResult
                result = e.Result

                Dim ind As Integer = 0

                For Each item As LDAPInfoEntry In _Settings.LDAPInfoEntryCollection
                    Try
                        Dim name As ResultPropertyValueCollection = result.Properties(item.Attribute)
                        Dim ctl As ADInfoItem
                        ctl = FlowLayoutPanel1.Controls.Item(ind)
                        If Not name(item.Index) = "" Then
                            ctl.ValueTxt.Text = name(item.Index)
                            ctl.ValueTxt.BackColor = Color.LightGreen
                        Else
                            ctl.ValueTxt.Text = "No data"
                        End If
                    Catch ex As Exception
                    End Try
                    ind += 1
                Next
            Else
                Dim result As SearchResultEntry
                result = e.Result

                Dim ind As Integer = 0

                For Each item As LDAPInfoEntry In _Settings.LDAPInfoEntryCollection
                    Try
                        Dim name As SearchResultAttributeCollection = result.Attributes
                        Dim ctl As ADInfoItem
                        ctl = FlowLayoutPanel1.Controls.Item(ind)
                        If Not name(item.Index).Name = "" Then
                            ctl.ValueTxt.Text = name(item.Index).Name
                            ctl.ValueTxt.BackColor = Color.LightGreen
                        Else
                            ctl.ValueTxt.Text = "No data"
                        End If
                    Catch ex As Exception
                    End Try
                    ind += 1
                Next
            End If
        Catch ex As Exception
            _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAP: Error." & CurrentIPHostname, Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
        End Try
    End Sub

    Private Sub RefreshLDAPinfo_Tick(sender As Object, e As EventArgs) Handles RefreshLDAPinfo.Tick
        RefreshLDAPinfo.Stop()
        If Not CurrentIPHostname = "" Then
            RaiseAction(CurrentIPHostname, True)
        End If
        RefreshLDAPinfo.Start()
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshGUI()
    End Sub

    Private Sub ClientGUI_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        If Not IsNothing(_Settings) Then
            If _Settings.RefreshLayoutAtSizeChange Then
                FlowLayoutPanel1.Refresh()
            End If
        End If
    End Sub
End Class
