'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Data.SqlClient
Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Imports System.Security
Imports System.Windows.Forms
Imports CSToolCryptHelper
Imports CSToolEnvironmentManager
Imports CSToolLogLib.LogSettings

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public EnvManager As New EnvironmentManager
    Public SQLConnection As SqlConnection
    Public SQLCredential As SqlCredential
    Public CredHandler As New CredentialHandler
    Public CurrentIPHostname As String = ""
    Public IsDBConnected As Boolean = False
    Public FirstLoad As Boolean = True
    Private Delegate Sub AddListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem, ByVal EnsureVisible As Boolean)
    Private Delegate Sub AddListViewColumnDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ColumnHeader)
    Private Delegate Sub RemoveListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem)
    Private Delegate Sub BeginUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub EndUpdateListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Sub ClearListViewDelegate(ByVal ListViewCtl As ListView)
    Private Delegate Function GetItemFromIndexDelegate(ByVal ListViewCtl As ListView, ByVal Index As Integer) As ListViewItem
    Private Delegate Sub SetGroupOfListViewItemDelegate(ByVal ListViewCtl As ListView, ByVal GroupListItem As ListViewItem, ByVal GroupItem As ListViewGroup)
    Private Delegate Sub AddGroupToListViewDelegate(ByVal ListViewCtl As ListView, ByVal GroupItem As ListViewGroup)

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
            ListViewCtl.Columns.Clear()
            ListViewCtl.Items.Clear()
            ListViewCtl.Groups.Clear()
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

    Private Sub AddListViewColumn(ByVal ListViewCtl As ListView, ListViewColumnCtl As ColumnHeader)
        If ListViewCtl.InvokeRequired Then
            Dim delAddListViewColumn As New AddListViewColumnDelegate(AddressOf AddListViewColumn)
            ListViewCtl.Invoke(delAddListViewColumn, New Object() {ListViewCtl, ListViewColumnCtl})
        Else
            ListViewCtl.Columns.Add(ListViewColumnCtl)
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

    <DllImport("uxtheme", CharSet:=CharSet.Unicode)>
    Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal textSubAppName As String, ByVal textSubIdList As String) As Integer
    End Function

    Public Shared Function NotNull(Of T)(ByVal Value As T, ByVal DefaultValue As T) As T
        If Value Is Nothing OrElse IsDBNull(Value) Then
            Return DefaultValue
        Else
            Return Value
        End If
    End Function

    Public Sub SetUXThemeForListView()
        Try
            SetWindowTheme(ListView1.Handle, "explorer", Nothing)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsNothing(_Settings) Then
            SetGUIState()

            If Not _Settings.SQLConnectionString = "" And Not _Settings.SQLSelectString = "" Then
                If Not _Settings.InitialTitle = "" Then
                    Me.ParentForm.Text = _Settings.InitialTitle
                    _ParentInstance.CurrentWindowTitle = _Settings.InitialTitle
                End If

                If _Settings.ConnectAtLoad Then
                    RefreshGUI()
                Else
                    LoadImage.Visible = False
                    ConnectionClosed.Visible = True
                End If
            Else
                LoadImage.Visible = False
                ConnectionClosed.Visible = True
            End If
        End If
        SetUXThemeForListView()
    End Sub

    Public Sub SetGUIState()
        SplitContainer1.Orientation = _Settings.SplitViewStyle

        Select Case _Settings.ResultStyle
            Case ResultStyleEnum.ShowGroupsAndColumns
                SplitContainer1.Panel1Collapsed = False
                SplitContainer1.Panel2Collapsed = False
            Case ResultStyleEnum.ShowOnlyColumns
                SplitContainer1.Panel1Collapsed = False
                SplitContainer1.Panel2Collapsed = True
            Case ResultStyleEnum.ShowOnlyGroups
                SplitContainer1.Panel1Collapsed = True
                SplitContainer1.Panel2Collapsed = False
        End Select
    End Sub

    Private Function TestForServer(ByVal address As String, ByVal port As Integer, ByVal timeout As Integer) As Boolean
        Dim result = False

        Try
            Using socket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                Dim asyncResult As IAsyncResult = socket.BeginConnect(address, port, Nothing, Nothing)
                result = asyncResult.AsyncWaitHandle.WaitOne(timeout, True)
                socket.Close()
            End Using

            Return result
        Catch
            Return False
        End Try
    End Function

    Public Function ConnectToDB() As Boolean
        Try
            If _Settings.TestForSQLServer Then
                _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Test for Server...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                If Not TestForServer(_Settings.TestForSQLServerHostname, _Settings.TestForSQLServerPort, _Settings.TestForSQLServerTimeout) Then
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Server port is closed.", Me.GetType, LogEntryTypeEnum.Warning, LogEntryLevelEnum.Debug)
                    Return False
                Else
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Server port is open.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                End If
            End If

            If Not _Settings.FirstLoadDelay = 0 Then
                If FirstLoad Then
                    FirstLoad = False
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: First start delay...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    Threading.Thread.Sleep(_Settings.FirstLoadDelay)
                End If
            End If
            If Not FirstLoad Then
                If Not _Settings.SQLQueryDelay = 0 Then
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Query delay...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                    Threading.Thread.Sleep(_Settings.SQLQueryDelay)
                End If
            End If

            If _Settings.UseCustomSQLCredentials Then
                _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Using custom credentials to log-in...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                Dim secstr As SecureString
                secstr = CredHandler.ConvertStringInSecureString(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomSQLCredentialsPassword))
                secstr.MakeReadOnly()

                If Not IsNothing(SQLConnection) Then
                    Try
                        SQLConnection.Close()
                        SQLConnection = Nothing
                    Catch ex As Exception
                        _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Error closing DB connection.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
                    End Try
                End If

                SQLCredential = New SqlCredential(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomSQLCredentialsUsername), secstr)
                SQLConnection = New SqlConnection(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.SQLConnectionString), SQLCredential)
                _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Connection established with custom credentials.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            Else
                _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Using build-in credentials...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                SQLConnection = New SqlConnection(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.SQLConnectionString))
                _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Connection established with build-in credentials.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            End If

            _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Open session...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            SQLConnection.Open()
            _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Open session successful.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            Return True
        Catch ex As Exception
            _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Error.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
            Return False
        End Try
    End Function

    Public Function GetSQLData() As List(Of List(Of Object))
        Try
            Dim itms As New List(Of List(Of Object))
            _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Building SQL-Command...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            Dim command As New SqlCommand(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.SQLSelectString), SQLConnection)
            command.CommandTimeout = _Settings.SQLCommandTimeout
            _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Execute query " & command.CommandText, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            Dim reader As SqlDataReader = command.ExecuteReader()
            _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Execute query " & command.CommandText & " successful.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            Dim schematable As DataTable
            _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Get schema table info...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            schematable = reader.GetSchemaTable()
            _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Get schema table info successful.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

            Try
                Dim index As Integer = schematable.Columns.IndexOf("ColumnName")
                Dim columnName As DataColumn = schematable.Columns(index)

                _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Building table column collection...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

                Dim columns As New List(Of Object)
                For Each myField As DataRow In schematable.Rows
                    Dim columnNameValue As String = myField(columnName).ToString()
                    columns.Add(columnNameValue)
                Next

                _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Adding " & columns.Count & " columns to collection...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

                itms.Add(columns)
            Catch ex As Exception
                _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Error adding columns to collection.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug, Err)
            End Try

            _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Reading SQL data rows...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

            While reader.Read()
                Try
                    Dim newline As New List(Of Object)
                    For index = 0 To reader.FieldCount - 1
                        newline.Add(reader(index))
                    Next

                    itms.Add(newline)
                Catch ex As Exception
                    _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Error adding row to collection.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug, Err)
                End Try
            End While

            _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Successful collected " & itms.Count & " rows.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

            command.Dispose()
            reader.Close()

            Return itms
        Catch ex As Exception
            _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Reading SQL error.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug, Err)
            Return New List(Of List(Of Object))
        End Try
    End Function

    Public Function FillSQLDataToGUI(ByVal Data As List(Of List(Of Object))) As Boolean
        Try
            BeginUpdateListView(ListView1)
            ClearListView(ListView1)

            If Not Data.Count = 0 Then
                _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: SQL data ist not empty, loading " & Data(0).Count & " columns and " & Data.Count & " rows...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

                If _Settings.AutoCreatingColumns Then
                    For index = 0 To Data(0).Count - 1
                        Dim col As New ColumnHeader
                        col.Name = index
                        If Not Convert.IsDBNull(Data(0)(index)) Then
                            col.Text = Data(0)(index)
                        Else
                            col.Text = _Settings.NullValue
                        End If
                        AddListViewColumn(ListView1, col)
                    Next
                Else
                    If Not _Settings.CustomColumnCollection.Count = 0 Then
                        For index = 0 To _Settings.CustomColumnCollection.Count - 1
                            Dim colitm As New ColumnHeader
                            colitm.Name = _Settings.CustomColumnCollection(index).Name
                            colitm.Text = _Settings.CustomColumnCollection(index).ColumnHeaderText
                            colitm.Width = _Settings.CustomColumnCollection(index).ColumnWidth
                            If Not _Settings.CustomColumnCollection(index).DisplayIndex = -1 Then
                                colitm.DisplayIndex = _Settings.CustomColumnCollection(index).DisplayIndex
                            End If
                            colitm.TextAlign = _Settings.CustomColumnCollection(index).TextAlign
                            AddListViewColumn(ListView1, colitm)
                        Next
                    End If
                End If

                _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Loading SQL columns successful.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

                For index = 1 To Data.Count - 1
                    Dim rowitm As New ListViewItem
                    rowitm.Name = index

                    If Not Convert.IsDBNull(Data(index)(0)) Then
                        rowitm.Text = Data(index)(0)
                    End If

                    If Data(index).Count > 1 Then
                        For colind = 1 To Data(index).Count - 1
                            If Not Convert.IsDBNull(Data(index)(colind)) Then
                                rowitm.SubItems.Add(Data(index)(colind))
                            Else
                                rowitm.SubItems.Add(_Settings.NullValue)
                            End If
                        Next
                    End If

                    AddListViewItem(ListView1, rowitm)
                Next
            End If

            _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Loading SQL rows successful.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)

            EndUpdateListView(ListView1)
            Return True
        Catch ex As Exception
            Try
                EndUpdateListView(ListView1)
            Catch ex2 As Exception
            End Try
            _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Error enumerate SQL table data.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug, Err)
            Return False
        End Try
    End Function

    Public Function CloseConnectionToDB() As Boolean
        Try
            _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Close connection...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            If Not IsNothing(SQLConnection) Then
                SQLConnection.Close()
            End If
            _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Close connection successful.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            Return True
        Catch ex As Exception
            _ParentInstance.CurrentLogInstance.WriteLogEntry("SQL: Close connection: Error.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug, Err)
            Return False
        End Try
    End Function

    Public Sub UnloadPlugin()
        CloseConnectionToDB()
        IsDBConnected = False
    End Sub

    Public Sub RaiseAction(ByVal IPorHostname As String)
        CurrentIPHostname = IPorHostname

        If _Settings.RaiseActions Then
            If Not (_Settings.UseCustomSQLCredentials = True And _Settings.CustomSQLCredentialsUsername = "") Then
                If Not _Settings.SQLConnectionString = "" And Not _Settings.SQLSelectString = "" Then
                    RefreshGUI()
                End If
            End If
        End If
    End Sub

    Public Function LoadItems() As Boolean
        Try
            CustomGroupsPanel.Controls.Clear()

            If Not _Settings.GroupTextCollection.Count = 0 Then
                For index = 0 To _Settings.GroupTextCollection.Count - 1
                    Dim gg As New CustomInfoItm
                    gg.GroupBox1.Text = _Settings.GroupTextCollection(index).GroupText
                    gg.Width = _Settings.GroupTextCollection(index).GroupWidth
                    gg.ValueTxt.Tag = _Settings.GroupTextCollection(index)

                    CustomGroupsPanel.Controls.Add(gg)
                    gg.Dock = DockStyle.Top
                Next
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function FillDataInGroupItems() As Boolean
        Try
            If Not CustomGroupsPanel.Controls.Count = 0 Then
                For index = 0 To CustomGroupsPanel.Controls.Count - 1
                    Dim ctl As CustomInfoItm
                    ctl = CustomGroupsPanel.Controls(index)
                    Try
                        Dim itmobj As CustomGroupEntry
                        itmobj = ctl.ValueTxt.Tag
                        ctl.ValueTxt.Text = ListView1.SelectedItems(0).SubItems(itmobj.ValueIndex).Text
                    Catch ex As Exception
                        ctl.ValueTxt.Text = "No data"
                    End Try
                Next
                Return True
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub RefreshGUI()
        If Not IsNothing(_Settings) Then
            If Not _Settings.InitialTitle = "" Then
                _ParentInstance.CurrentWindowTitle = _Settings.InitialTitle
                Me.ParentForm.Text = _Settings.InitialTitle
            End If

            SetGUIState()
            LoadItems()

            If Not GetSQLDataAsync.IsBusy Then
                LoadImage.Visible = True
                If IsDBConnected Then
                    GetSQLDataAsync.RunWorkerAsync()
                Else
                    If ConnectToDB() Then
                        IsDBConnected = True
                        GetSQLDataAsync.RunWorkerAsync()
                    Else
                        IsDBConnected = False
                        ConnectionClosed.Visible = True
                        LoadImage.Visible = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub GetSQLDataAsync_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles GetSQLDataAsync.DoWork
        e.Result = GetSQLData()
    End Sub

    Private Sub GetSQLDataAsync_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles GetSQLDataAsync.RunWorkerCompleted
        If Not LoadDataInGUI.IsBusy Then
            LoadDataInGUI.RunWorkerAsync(e.Result)
        End If
    End Sub

    Private Sub RefreshSQLDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshSQLDataToolStripMenuItem.Click
        RefreshGUI()
    End Sub

    Private Sub LoadDataInGUI_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadDataInGUI.DoWork
        e.Result = FillSQLDataToGUI(e.Argument)
    End Sub

    Private Sub LoadDataInGUI_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadDataInGUI.RunWorkerCompleted
        If e.Result = True Then
            If _Settings.AutoResizeColumns Then
                _ParentInstance.CurrentLogInstance.WriteLogEntry("Auto-Size columns in ListView...", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            End If
            If _Settings.SelectFirstRow And (Not ListView1.Items.Count = 0) Then
                ListView1.Items(0).Selected = True
            End If
            If _Settings.ShowErrorIfNoRow And ListView1.Items.Count = 0 Then
                ConnectionClosed.Visible = True
                LoadImage.Visible = False
            Else
                ConnectionClosed.Visible = False
                LoadImage.Visible = False
            End If
        Else
            ConnectionClosed.Visible = True
            LoadImage.Visible = False
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        FillDataInGroupItems()
    End Sub
End Class
