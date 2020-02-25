'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.DirectoryServices
Imports System.DirectoryServices.Protocols
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports CSToolCryptHelper
Imports CSToolEnvironmentManager
Imports CSToolLogLib.LogSettings
Imports CSToolWMIHelper

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public WMIHandler As New WMIHandler
    Public EnvManager As New EnvironmentManager
    Public CurrentIPHostname As String = ""
    Public CurrentUsernameWithDomain As String = ""
    Public CurrentUsernameWithoutDomain As String = ""
    Private Delegate Sub AddListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem, ByVal EnsureVisible As Boolean)
    Private Delegate Sub RemoveListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem)
    Private Delegate Sub BeginUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub EndUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub ClearListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Function GetItemFromIndexDelegate(ByVal ListViewCtl As ListView, ByVal Index As Integer) As ListViewItem

    <DllImport("uxtheme", CharSet:=CharSet.Unicode)>
    Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal textSubAppName As String, ByVal textSubIdList As String) As Integer
    End Function
    Public Sub SetUXThemeForAllListViews()
        Try
            SetWindowTheme(ListView1.Handle, "explorer", Nothing)
        Catch ex As Exception
        End Try
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

    Private Sub AddListViewItem(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem, Optional ByVal EnsureVisible As Boolean = False)
        If ListViewCtl.InvokeRequired Then
            Dim delAddListViewItem As New AddListViewItemDelegate(AddressOf AddListViewItem)
            ListViewCtl.Invoke(delAddListViewItem, New Object() {ListViewCtl, ListViewItemCtl, EnsureVisible})
        Else
            Dim lvi As New ListViewItem(Text)
            ListViewCtl.Items.Add(ListViewItemCtl)
            If EnsureVisible Then
                ListViewCtl.EnsureVisible(ListViewCtl.Items.Count - 1)
            End If
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

    Public Sub RaiseAction(ByVal IPOrHostname As String, Optional ByVal IsRefresh As Boolean = False)
        CurrentIPHostname = IPOrHostname

        If Not IPOrHostname = "" Then
            If _Settings.GetCurrentUserOverWMI Then
                If Not GetWMIInfoCurrentUser.IsBusy Then
                    LoadImage.Visible = True
                    ConnectionClosed.Visible = False
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("Start WMI-Worker for host " & CurrentIPHostname, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    GetWMIInfoCurrentUser.RunWorkerAsync(IPOrHostname)
                End If
            Else
                If Not GetADInfoAsync.IsBusy Then
                    LoadImage.Visible = True
                    ConnectionClosed.Visible = False
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("Start LDAP-Worker for host " & CurrentIPHostname, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    GetADInfoAsync.RunWorkerAsync()
                End If
            End If
        Else
            LoadImage.Visible = False
            ConnectionClosed.Visible = True
        End If
    End Sub

    Public Sub RefreshGUI()

    End Sub

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
                searcher.Attributes.Add("MemberOf")

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
                searcher.PropertiesToLoad.Add("MemberOf")

                _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAP: Waiting for LDAP-Result...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                result = searcher.FindOne
                _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAP: Result received.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

                Return result
            End If
        Catch ex As Exception
            _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAP: Error." & CurrentIPHostname, Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
            Return Nothing
        End Try
    End Function

    Private Sub GetADInfoAsync_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles GetADInfoAsync.DoWork
        e.Result = GetADInfo()
    End Sub

    Private Sub GetWMIInfoCurrentUser_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles GetWMIInfoCurrentUser.DoWork
        e.Result = WMIHandler.GetDetails(e.Argument, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.WMICurrentLoggedOnUserClassName),
                                         _Settings.WMICurrentLoggedOnUserSection, _Settings.WMIQueryTimeout,
                                         _Settings.UseCustomWMIConnectionOptions, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername),
                                         EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword),
                                         EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority),
                                         _Settings.CustomWMIConnectionOptionsAuthentication, _Settings.CustomWMIConnectionOptionsImpersonation)
    End Sub

    Private Sub GetWMIInfoCurrentUser_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles GetWMIInfoCurrentUser.RunWorkerCompleted
        If Not e.Result = "" Then
            _ParentInstance.CurrentLogInstance.WriteLogEntry("LDAP/WMI: Add current username and domain to environment...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            Dim userstr As String
            userstr = e.Result
            CurrentUsernameWithDomain = userstr
            Try
                CurrentUsernameWithoutDomain = userstr.Split("\")(1)
            Catch ex As Exception
            End Try
            ToolStripTextBox1.Text = CurrentUsernameWithoutDomain

            If Not GetADInfoAsync.IsBusy Then
                GetADInfoAsync.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub FillGroupMembershipToGUI_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles FillGroupMembershipToGUI.DoWork
        Try
            BeginUpdateListView(ListView1)
            ClearListView(ListView1)

            Dim result As SearchResult = e.Argument
            For i As Integer = 0 To result.Properties("MemberOf").Count - 1
                Dim sProp As String = result.Properties("MemberOf")(i)
                Dim itm As New ListViewItem
                itm.Text = sProp.Substring(3, sProp.IndexOf(",") - 3)
                AddListViewItem(ListView1, itm, False)
            Next

            EndUpdateListView(ListView1)
            e.Result = True
        Catch ex As Exception
            e.Result = False
        End Try
    End Sub

    Private Sub FillGroupMembershipToGUI_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles FillGroupMembershipToGUI.RunWorkerCompleted
        If e.Result Then
            LoadImage.Visible = False
            ConnectionClosed.Visible = False
        Else
            LoadImage.Visible = False
            ConnectionClosed.Visible = True
        End If
        If _Settings.AutoResizeColumns Then
            ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        End If
    End Sub

    Private Sub GetADInfoAsync_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles GetADInfoAsync.RunWorkerCompleted
        Try
            If _Settings.LDAPType = LDAPTypeEnum.LDAP Then
                Dim result As SearchResult
                result = e.Result
                If Not result.Properties.Count = 0 Then
                    If Not FillGroupMembershipToGUI.IsBusy Then
                        FillGroupMembershipToGUI.RunWorkerAsync(result)
                    End If
                Else
                    LoadImage.Visible = False
                    ConnectionClosed.Visible = True
                End If
            Else
                Dim result As SearchResultEntry
                result = e.Result
                If Not result.Attributes.Count = 0 Then
                    If Not FillGroupMembershipToGUI.IsBusy Then
                        FillGroupMembershipToGUI.RunWorkerAsync(result)
                    End If
                Else
                    LoadImage.Visible = False
                    ConnectionClosed.Visible = True
                End If
            End If
        Catch ex As Exception
            LoadImage.Visible = False
            ConnectionClosed.Visible = True
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            CurrentUsernameWithDomain = ""
            CurrentUsernameWithoutDomain = ToolStripTextBox1.Text

            If Not GetADInfoAsync.IsBusy Then
                LoadImage.Visible = True
                ConnectionClosed.Visible = False
                GetADInfoAsync.RunWorkerAsync()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUXThemeForAllListViews()
    End Sub
End Class
