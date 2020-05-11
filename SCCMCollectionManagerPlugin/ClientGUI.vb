'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolEnvironmentManager
Imports System.IO
Imports System.Management
Imports System.Runtime.InteropServices
Imports System.Security
Imports System.Text
Imports Microsoft.ConfigurationManagement.ManagementProvider
Imports Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine
Imports CSToolWMIHelper

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public EnvManager As New EnvironmentManager
    Public CurrentIPHostname As String = ""

    <DllImport("uxtheme", CharSet:=CharSet.Unicode)>
    Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal textSubAppName As String, ByVal textSubIdList As String) As Integer
    End Function

    Private Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal process As IntPtr, ByVal minimumWorkingSetSize As Integer, ByVal maximumWorkingSetSize As Integer) As Integer

    Dim CurrentSMSConnection As WqlConnectionManager = Nothing
    Dim SearchingClientname As String = ""
    Dim WatchingClientname As String = ""

    Dim IsConnectionToSMSEtablished As Boolean = False
    Dim IsCollectionsReceived As Boolean = False
    Dim IsCollectionMembershipReceived As Boolean = False
    Dim IsClientPackageStateReceived As Boolean = False
    Dim IsServerPackageStateReceived = False
    Dim IsClientCollectionReceived As Boolean = False

    Dim SkippedCollectionsCount As Integer = 0
    Dim SkippedClientCollectionsCount As Integer = 0

    Dim CurrentSMSCollection As WqlQueryResultsObject = Nothing
    Dim CurrentSMSClientCollectionMembership As WqlQueryResultsObject = Nothing
    Dim CurrentClientsFromCollectionMembership As WqlQueryResultsObject = Nothing

    Private Delegate Sub AddListViewItemDelegate(ByVal ListViewCtl As ListView, ListViewItemCtl As ListViewItem, ByVal EnsureVisible As Boolean)
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

        If _Settings.RaiseActions Then
            RefreshGUI()
        End If
    End Sub
    Public Sub RefreshGUI()
        Try
            If Not _Settings.SMSServer = "" Then
                If IsConnectionToSMSEtablished = False Then
                    ResetAllGUIItems()
                    SetSMSCollectionLoadGUIState(True)
                    ConnectToSMSMServerWorker.RunWorkerAsync()
                End If

                If Not CurrentIPHostname = "" Then
                    ToolStripTextBox2.Text = CurrentIPHostname
                    TriggerClientCollectionMembershipsWorker()
                    TriggerCollectClientPackageState()
                    TriggerCollectServerPackageState()

                    SetOptimalListViewStyle()
                End If

                AutoSizeLogColumns()
            Else
                ResetAllGUIItems()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub UnloadPlugin()
        CloseServerConnection()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        _Settings.SMSServer = ToolStripTextBox1.Text

        If CurrentSMSConnection Is Nothing Then
            If ConnectToSMSMServerWorker.IsBusy = False Then
                SetSMSCollectionLoadGUIState(True)
                ConnectToSMSMServerWorker.RunWorkerAsync()
            End If
        Else
            TriggerSMSCollectionsWorker()
        End If
    End Sub

    Public Function SetOptimalListViewStyle() As Boolean
        Try
            ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            ListView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            ListView4.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            ListView5.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            ListView6.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SortListView(ByVal ListViewCtl As ListView, Optional ByVal SortOrderValue As SortOrder = SortOrder.Ascending) As Boolean
        Try
            ListViewCtl.Sorting = SortOrderValue
            ListViewCtl.Sort()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function HideEmptyColumns(ByVal ListViewCtl As ListView) As Boolean
        Try
            For index = 0 To ListViewCtl.Items(0).SubItems.Count - 1
                If ListViewCtl.Items(0).SubItems(index).Text = "" Then
                    ListViewCtl.Columns.Item(index + 1).Width = 0
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CloseServerConnection() As Boolean
        Try
            If IsConnectionToSMSEtablished Then
                PostLogText("Verbindung zum Server schließen...", False)
                CurrentSMSConnection.Close()
                PostLogText("Verbindung zum Server erfolgreich beendet.", False)
            End If

            Return True
        Catch ex As Exception
            PostLogText("Fehler: Verbindung zum Server konnte nicht beendet werden: " & ex.Message, True)
            Return False
        End Try
    End Function

    Public Function LoadCollectionsFromServer(Optional ByVal FilterResults As Boolean = True, Optional ByVal FilterNoMemberCollections As Boolean = False, Optional ByVal GroupResults As Boolean = True) As Boolean
        Try
            SkippedCollectionsCount = 0

            PostLogText("Collections abrufen...", False)
            Dim GetCollectionQuery = "SELECT * FROM SMS_Collection"
            CurrentSMSCollection = CurrentSMSConnection.QueryProcessor.ExecuteQuery(GetCollectionQuery)
            PostLogText("Collections erfolgreich abgerufen...", False)

            PostLogText("Lade Collections...", False)

            BeginUpdateListView(ListView2)
            ClearListView(ListView2)

            If _Settings.CleanALLEntrysFromLists Then
                For Each item As WqlResultObject In CurrentSMSCollection
                    If Not item.PropertyList("Name").ToLower.StartsWith("all ") Then
                        Dim qq As New ListViewItem
                        qq.Text = item.PropertyList("Name")
                        qq.SubItems.Add(item.PropertyList("MemberCount"))
                        qq.SubItems.Add(item.PropertyList("CollectionID"))
                        qq.SubItems.Add(item.PropertyList("Comment"))
                        qq.SubItems.Add(item.PropertyList("CollectionType"))
                        qq.Tag = item

                        qq.ImageIndex = 11

                        AddListViewItem(ListView2, qq)
                    Else
                        SkippedCollectionsCount += 1
                    End If
                Next
            Else
                For Each item As WqlResultObject In CurrentSMSCollection
                    Dim qq As New ListViewItem
                    qq.Text = item.PropertyList("Name")
                    qq.SubItems.Add(item.PropertyList("MemberCount"))
                    qq.SubItems.Add(item.PropertyList("CollectionID"))
                    qq.SubItems.Add(item.PropertyList("Comment"))
                    qq.SubItems.Add(item.PropertyList("CollectionType"))
                    qq.Tag = item

                    qq.ImageIndex = 11

                    AddListViewItem(ListView2, qq)
                Next
            End If

            PostLogText("Collections erfolgreich geladen.", False)

            If FilterNoMemberCollections And Not ListView2.Items.Count = 0 Then
                PostLogText("Collections filtern (" & ListView2.Items.Count & " ungefiltert)...", False)

                For ind = ListView2.Items.Count - 1 To 0 Step -1
                    If ListView2.Items(ind).SubItems(0).Text = "0" Then
                        RemoveListViewItem(ListView2, ListView2.Items(ind))
                    End If
                Next

                PostLogText("Collections erfolgreich gefiltert (" & ListView2.Items.Count & " sichtbar).", False)
            End If

            If FilterResults And (Not _Settings.AvailableCollectionsFilter.Count = 0 And Not ListView2.Items.Count = 0) Then
                PostLogText("Collections filtern (" & ListView2.Items.Count & " ungefiltert)...", False)

                For ind = ListView2.Items.Count - 1 To 0 Step -1
                    Dim remove As Boolean = True
                    Dim curritem As ListViewItem
                    curritem = ListView2.Items(ind)

                    For index = 0 To _Settings.AvailableCollectionsFilter.Count - 1
                        If _Settings.AvailableCollectionsFilter(index).Enabled Then
                            If curritem.Text.Contains(_Settings.AvailableCollectionsFilter(index).FilterText) Then
                                remove = False
                                Exit For
                            End If
                        End If
                    Next

                    If remove Then
                        RemoveListViewItem(ListView2, curritem)
                    End If
                Next

                PostLogText("Collections erfolgreich gefiltert (" & ListView2.Items.Count & " sichtbar).", False)
            End If

            If Not _Settings.AvailableCollectionsGroups.Count = 0 And Not ListView2.Items.Count = 0 Then
                Dim grcoll As New List(Of ListViewGroup)

                For grindex = 0 To _Settings.AvailableCollectionsGroups.Count - 1
                    If _Settings.AvailableCollectionsGroups(grindex).Enabled Then
                        Dim newgritm As New ListViewGroup
                        newgritm.Name = _Settings.AvailableCollectionsGroups(grindex).GroupName
                        newgritm.Header = _Settings.AvailableCollectionsGroups(grindex).GroupName
                        grcoll.Add(newgritm)
                    End If
                Next

                For index = 0 To grcoll.Count - 1
                    AddGroupToListView(ListView2, grcoll(index))
                Next

                For index = 0 To ListView2.Items.Count - 1
                    For grindex = 0 To grcoll.Count - 1
                        If ListView2.Items(index).Text.Contains(_Settings.AvailableCollectionsGroups(grindex).GroupText) Then
                            SetGroupOfListViewItem(ListView2, ListView2.Items(index), grcoll(grindex))
                        End If
                    Next
                Next
            End If

            EndUpdateListView(ListView2)

            Return True
        Catch ex As Exception
            PostLogText("Auflistung der Collection nicht möglich: " & ex.Message, True)
            Return False
        End Try
    End Function

    Public Function CollectCollectionMemberships(Optional ByVal FilterResults As Boolean = True, Optional ByVal GroupResults As Boolean = True) As Boolean
        Try
            SkippedClientCollectionsCount = 0

            PostLogText("Collection-Mitgliedschaften abrufen...", False)
            Dim GetCollectionQuery = "SELECT SMS_Collection.* FROM SMS_FullCollectionMembership, SMS_Collection where name = '" & CurrentIPHostname & "' and SMS_FullCollectionMembership.CollectionID = SMS_Collection.CollectionID"
            CurrentSMSClientCollectionMembership = CurrentSMSConnection.QueryProcessor.ExecuteQuery(GetCollectionQuery)
            PostLogText("Collection-Mitgliedschaften erfolgreich abgerufen.", False)

            PostLogText("Lade Collection-Mitgliedschaften...", False)

            BeginUpdateListView(ListView4)
            ClearListView(ListView4)

            If _Settings.CleanALLEntrysFromLists Then
                For Each item As WqlResultObject In CurrentSMSClientCollectionMembership
                    If Not item.PropertyList("Name").ToLower.StartsWith("all ") Then
                        Dim qq As New ListViewItem
                        qq.Text = item.PropertyList("Name")
                        qq.SubItems.Add(item.PropertyList("MemberCount"))
                        qq.SubItems.Add(item.PropertyList("CollectionID"))
                        qq.SubItems.Add(item.PropertyList("Comment"))
                        qq.SubItems.Add(item.PropertyList("CollectionType"))
                        qq.Tag = item

                        qq.ImageIndex = 1

                        AddListViewItem(ListView4, qq)
                    Else
                        SkippedClientCollectionsCount += 1
                    End If
                Next
            Else
                For Each item As WqlResultObject In CurrentSMSClientCollectionMembership
                    Dim qq As New ListViewItem
                    qq.Text = item.PropertyList("Name")
                    qq.SubItems.Add(item.PropertyList("MemberCount"))
                    qq.SubItems.Add(item.PropertyList("CollectionID"))
                    qq.SubItems.Add(item.PropertyList("Comment"))
                    qq.SubItems.Add(item.PropertyList("CollectionType"))
                    qq.Tag = item

                    qq.ImageIndex = 1

                    AddListViewItem(ListView4, qq)
                Next
            End If

            If FilterResults Then
                If Not _Settings.DeviceCollectionsFilter.Count = 0 And Not ListView4.Items.Count = 0 Then
                    For ind = ListView4.Items.Count - 1 To 0 Step -1
                        Dim remove As Boolean = True
                        Dim curritem As ListViewItem
                        curritem = ListView4.Items(ind)

                        For index = 0 To _Settings.DeviceCollectionsFilter.Count - 1
                            If _Settings.DeviceCollectionsFilter(index).Enabled Then
                                If curritem.Text.Contains(_Settings.DeviceCollectionsFilter(index).FilterText) Then
                                    remove = False
                                    Exit For
                                End If
                            End If
                        Next

                        If remove Then
                            RemoveListViewItem(ListView4, curritem)
                        End If
                    Next
                End If
            End If

            If GroupResults Then
                If Not _Settings.DeviceCollectionsGroups.Count = 0 And Not ListView4.Items.Count = 0 Then
                    Dim grcoll As New List(Of ListViewGroup)

                    For grindex = 0 To _Settings.DeviceCollectionsGroups.Count - 1
                        If _Settings.DeviceCollectionsGroups(grindex).Enabled Then
                            Dim newgritm As New ListViewGroup
                            newgritm.Name = _Settings.DeviceCollectionsGroups(grindex).GroupName
                            newgritm.Header = _Settings.DeviceCollectionsGroups(grindex).GroupName
                            grcoll.Add(newgritm)
                        End If
                    Next

                    For index = 0 To grcoll.Count - 1
                        AddGroupToListView(ListView4, grcoll(index))
                    Next

                    For index = 0 To ListView4.Items.Count - 1
                        For grindex = 0 To grcoll.Count - 1
                            If ListView4.Items(index).Text.Contains(_Settings.DeviceCollectionsGroups(grindex).GroupText) Then
                                SetGroupOfListViewItem(ListView4, ListView4.Items(index), grcoll(grindex))
                            End If
                        Next
                    Next
                End If
            End If

            EndUpdateListView(ListView4)
            PostLogText("Collection-Mitgliedschaften erfolgreich geladen.", False)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetAllClientsFromCollectionMembership(ByVal CollectionID As String) As WqlQueryResultsObject
        Try
            Dim CollectionClientMembershipsResult As WqlQueryResultsObject
            PostLogText("Collection-Mitgliedschaften abrufen...", False)
            Dim CollectionClientMemberships = "SELECT * FROM SMS_FullCollectionMembership INNER JOIN SMS_R_System ON SMS_FullCollectionMembership.ResourceID = SMS_R_System.ResourceID where SMS_FullCollectionMembership.CollectionID = '" & CollectionID & "'"
            CollectionClientMembershipsResult = CurrentSMSConnection.QueryProcessor.ExecuteQuery(CollectionClientMemberships)

            Return CollectionClientMembershipsResult
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetCollectionMembershipsCount(ByVal CurrClientname As String) As Integer
        Try
            Dim Countresult As WqlQueryResultsObject
            Dim GetCollectionCountQuery = "SELECT SMS_Collection.* FROM SMS_FullCollectionMembership, SMS_Collection where name = '" & CurrClientname & "' and SMS_FullCollectionMembership.CollectionID = SMS_Collection.CollectionID"
            Countresult = CurrentSMSConnection.QueryProcessor.ExecuteQuery(GetCollectionCountQuery)

            Dim count As Integer = 0
            For Each item As WqlResultObject In Countresult
                count += 1
            Next

            Return count
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetClientnameResourceID() As String
        Try
            PostLogText("Ermittle Resource-ID des Clients " & CurrentIPHostname & " ...", False)
            Dim GetClientnameResourceIDQuery = "SELECT ResourceID FROM SMS_R_System Where Name = '" & CurrentIPHostname & "'"
            Dim ResID As WqlQueryResultsObject = CurrentSMSConnection.QueryProcessor.ExecuteQuery(GetClientnameResourceIDQuery)
            PostLogText("Resource-ID des Clients " & CurrentIPHostname & " erfolgreich ermittelt.", False)

            For Each item As WqlResultObject In ResID
                Return item.PropertyList(item.PropertyNames(0))
            Next

            Return -1
        Catch ex As Exception
            PostLogText("Resource-ID des Clients " & CurrentIPHostname & " konnte nicht ermittelt werden: " & ex.Message, False)
            Return -1
        End Try
    End Function

    Public Function GetSpecificClientResourceID(ByVal Hostname As String) As String
        Try
            Dim GetClientnameResourceIDQuery = "SELECT ResourceID FROM SMS_R_System Where Name = '" & Hostname & "'"
            Dim ResID As WqlQueryResultsObject = CurrentSMSConnection.QueryProcessor.ExecuteQuery(GetClientnameResourceIDQuery)

            For Each item As WqlResultObject In ResID
                Return item.PropertyList(item.PropertyNames(0))
            Next

            Return -1
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Public Function GetAllClients() As WqlQueryResultsObject
        Try
            Return CurrentSMSConnection.QueryProcessor.ExecuteQuery("SELECT Name, LastLogonUserName, LastLogonTimestamp, MACAddresses, IPAddresses, ClientVersion FROM SMS_R_System")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetCurrentClientPackageStateFromServer(ByVal ComputerName As String) As WqlQueryResultsObject
        Try
            Return CurrentSMSConnection.QueryProcessor.ExecuteQuery("SELECT CollectionName, PackageName, StatusDescription, StatusType, SummarizationTime FROM SMS_ClassicDeploymentAssetDetails Where DeviceName = '" & ComputerName & "'")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetCurrentClientPackageStateFromServer2(ByVal ComputerName As String) As WqlQueryResultsObject
        Try
            Return CurrentSMSConnection.QueryProcessor.ExecuteQuery("SELECT * FROM SMS_AppDeploymentAssetDetails Where MachineName = '" & ComputerName & "'")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetCurrentClientAdvertisementStateFromServer(ByVal ComputerName As String) As WqlQueryResultsObject
        Try
            Return CurrentSMSConnection.QueryProcessor.ExecuteQuery("SELECT msg.MachineName, ad.AdvertisementName, ad.ProgramName FROM SMS_StatusMessage msg JOIN SMS_StatMsgAttributes attr ON msg.RecordID = attr.RecordID JOIN SMS_Advertisement ad ON attr.AttributeValue = ad.AdvertisementID WHERE msg.MachineName = '" & ComputerName & "' AND msg.Component = 'Software Distribution' AND (msg.MessageID = 10008 or msg.MessageID = 10009 or msg.MessageID = 11102 or msg.MessageID = 11031) AND attr.AttributeID = 401")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetClientsFromSearchPattern(ByVal SearchQuery As String) As WqlQueryResultsObject
        Try
            Return CurrentSMSConnection.QueryProcessor.ExecuteQuery("SELECT Name, LastLogonUserName, LastLogonTimestamp, MACAddresses, IPAddresses, ClientVersion FROM SMS_R_System Where Name Like '%" & SearchQuery & "%'")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetClientsFromUsernamePattern(ByVal SearchQuery As String) As WqlQueryResultsObject
        Try
            Return CurrentSMSConnection.QueryProcessor.ExecuteQuery("SELECT Name, LastLogonUserName, LastLogonTimestamp, MACAddresses, IPAddresses, ClientVersion FROM SMS_R_System Where LastLogonUserName Like '%" & SearchQuery & "%'")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetCollectionID(ByVal CollObj As WqlResultObject) As String
        Try
            Return CollObj.PropertyList("CollectionID")
        Catch ex As Exception
            PostLogText("Das Ermitteln der Collection-ID von " & CollObj.PropertyList("Name") & " ist nicht möglich:" & ex.Message, True)
            Return ""
        End Try
    End Function


    Private Function AddToCollection(ByVal CollectionID As String, ByVal ComputerResourceId As String, ByVal ComputerName As String, ByVal connection As WqlConnectionManager) As Boolean
        Try
            PostLogText("Client " & ComputerName & " (" & ComputerResourceId & ") zu Collection ID " & CollectionID & " hinzufügen...", False)

            Dim oNewRule As IResultObject = connection.CreateEmbeddedObjectInstance("SMS_CollectionRuleDirect")
            oNewRule("ResourceClassName").StringValue = "SMS_R_System"
            oNewRule("RuleName").StringValue = ComputerName
            oNewRule("ResourceID").StringValue = ComputerResourceId

            Dim inParams As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
            inParams.Add("collectionRule", oNewRule)

            Dim oCollection As IResultObject = connection.GetInstance("SMS_Collection.CollectionID='" & CollectionID & "'")
            oCollection.ExecuteMethod("AddMembershipRule", inParams)

            PostLogText("Client " & ComputerName & " (" & ComputerResourceId & ") zu Collection ID " & CollectionID & " hinzugefügt.", False)

            Return True
        Catch ex As Exception
            PostLogText("Fehler: Der Client " & ComputerName & " (" & ComputerResourceId & ") konnte nicht zu Collection ID " & CollectionID & " hinzugefügt werden: " & ex.Message, True)
            Return False
        End Try
    End Function

    Public Function RemoveFromCollection(ByVal CollectionID As String, ByVal ComputerResourceID As String, ByVal connection As WqlConnectionManager) As Boolean
        Try
            PostLogText("Client mit der ID " & ComputerResourceID & " von Collection ID " & CollectionID & " entfernen...", False)

            Dim collection As IResultObject = connection.GetInstance("SMS_Collection.CollectionID='" & CollectionID & "'")
            Dim collectionRule As IResultObject = connection.CreateEmbeddedObjectInstance("SMS_CollectionRuleDirect")
            collectionRule("ResourceID").IntegerValue = ComputerResourceID
            Dim inParams As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
            inParams.Add("collectionRule", collectionRule)
            collection.ExecuteMethod("DeleteMemberShipRule", inParams)

            PostLogText("Client mit der ID " & ComputerResourceID & " von Collection ID " & CollectionID & " erfolgreich entfernt.", False)

            Return True
        Catch ex As Exception
            PostLogText("Fehler: Client mit der ID " & ComputerResourceID & " von Collection ID " & CollectionID & " konnte nicht entfernt werden: " & ex.Message, True)
            Return False
        End Try
    End Function

    Public Function TriggerPolicyClientRefresh(ByVal ComputerResourceID As String, ByVal connection As WqlConnectionManager) As Boolean
        Try
            Dim triggerpolicy As IResultObject = connection.GetInstance("InitiateClientOperation")
            Dim inParams As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
            Dim ResIDs() As Integer = {ComputerResourceID}
            inParams("Type") = 8
            inParams("TargetResourceIDs") = ResIDs
            inParams("RandomizationWindow") = 0

            triggerpolicy.ExecuteMethod("InitiateClientOperation", inParams)

            PostLogText("Aktualisierung der Policy für Client mit der ID " & ComputerResourceID & " auf SMS erfolgreich angefordert.", False)

            Return True
        Catch ex As Exception
            PostLogText("Fehler: Client mit der ID " & ComputerResourceID & ": Aktualisierung der Policy auf SMS nicht erfolgreich. " & ex.Message, True)
            Return False
        End Try
    End Function

    Public Function Connect(ByVal serverName As String, ByVal userName As String, ByVal userPassword As String) As WqlConnectionManager
        Try
            Dim namedValues As SmsNamedValuesDictionary = New SmsNamedValuesDictionary()
            Dim connection As WqlConnectionManager = New WqlConnectionManager(namedValues)

            If System.Net.Dns.GetHostName().ToUpper() = serverName.ToUpper() Then
                connection.Connect(serverName)
            Else
                connection.Connect(serverName, userName, userPassword)
            End If

            Return connection
        Catch ex As SmsException
            PostLogText("Fehler während der Aushandlung der Verbindung: " & ex.Message, True)
            Return Nothing
        Catch ex As UnauthorizedAccessException
            PostLogText("Authentifizierungsfehler: " & ex.Message, True)
            Return Nothing
        End Try
    End Function

    Public Function GetDomainName() As String
        'Diese Funktion ermittelt die Domäne des Benutzers, welche die Anwendung gerade ausführt.
        Try
            Dim searcher As New ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_ComputerSystem")
            For Each queryObj As ManagementObject In searcher.Get()
                Return queryObj("Domain").ToString()
            Next
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function GetApplicationsFromServer(Hostname As String) As Boolean
        Try
            BeginUpdateListView(ListView6)
            ClearListView(ListView6)

            Dim serverresult As WqlQueryResultsObject
            serverresult = GetCurrentClientPackageStateFromServer(Hostname)

            If Not serverresult Is Nothing Then
                For Each item As WqlResultObject In serverresult
                    Dim qq As New ListViewItem
                    qq.Text = item.PropertyList("CollectionName")
                    qq.SubItems.Add("")
                    qq.SubItems.Add("")
                    qq.SubItems.Add("")
                    qq.SubItems.Add("")
                    qq.SubItems.Add(item.PropertyList("StatusDescription"))
                    qq.SubItems.Add(item.PropertyList("SummarizationTime"))
                    qq.SubItems.Add("")

                    If item.PropertyList("StatusType") = 1 Then
                        qq.ImageIndex = 4
                    End If
                    If item.PropertyList("StatusType") = 2 Then
                        qq.ImageIndex = 1
                    End If
                    If item.PropertyList("StatusType") = 4 Then
                        qq.ImageIndex = 3
                    End If
                    If item.PropertyList("StatusType") = 5 Then
                        qq.ImageIndex = 2
                    End If

                    AddListViewItem(ListView6, qq)
                Next

                PostLogText("(Teil 1 von 2) Clientstatus von SMS abgerufen.", False)
            Else
                PostLogText("Fehler Clientstatus: Clientstatus nicht ermittelbar.", True)
                Return False
            End If

            Dim serverresult2 As WqlQueryResultsObject
            serverresult2 = GetCurrentClientPackageStateFromServer2(Hostname)

            If Not serverresult2 Is Nothing Then
                For Each item As WqlResultObject In serverresult2
                    Dim qq As New ListViewItem
                    qq.Text = item.PropertyList("CollectionName")

                    If item.PropertyList("DeploymentIntent") = 1 Then
                        qq.SubItems.Add("Install")
                    End If
                    If item.PropertyList("DeploymentIntent") = 2 Then
                        qq.SubItems.Add("Uninstall")
                    End If
                    If item.PropertyList("DeploymentIntent") = 3 Then
                        qq.SubItems.Add("Preflight")
                    End If
                    If item.PropertyList("InstalledState") = 1 Then
                        qq.SubItems.Add("Uninstall")
                    End If
                    If item.PropertyList("InstalledState") = 2 Then
                        qq.SubItems.Add("Install")
                    End If
                    If item.PropertyList("InstalledState") = 3 Then
                        qq.SubItems.Add("Unknown")
                    End If

                    qq.SubItems.Add(item.PropertyList("StartTime"))

                    If item.PropertyList("StatusType") = 1 Then
                        qq.ImageIndex = 4
                    End If
                    If item.PropertyList("StatusType") = 2 Then
                        qq.ImageIndex = 1
                    End If
                    If item.PropertyList("StatusType") = 4 Then
                        qq.ImageIndex = 3
                    End If
                    If item.PropertyList("StatusType") = 5 Or item.PropertyList("StatusType") = 3 Then
                        qq.ImageIndex = 2
                    End If

                    AddListViewItem(ListView6, qq)
                Next

                PostLogText("(Teil 2 von 2) Clientstatus von SMS abgerufen.", False)

                Return True
            Else
                PostLogText("Fehler Clientstatus: Clientstatus nicht ermittelbar.", True)
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetApplicationsFromClient(Hostname As String) As Boolean
        Try
            BeginUpdateListView(ListView1)
            ClearListView(ListView1)

            Dim options As ConnectionOptions
            options = New ConnectionOptions()
            options.Password = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.SMSPassword)
            options.Username = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.SMSUsername)
            options.Timeout = New TimeSpan(0, 0, 5)

            Dim scope As ManagementScope
            If (Hostname.ToLower = My.Computer.Name.ToLower) Or (Hostname.ToLower = "localhost") Then
                scope = New ManagementScope("\\" & Hostname & "\root\ccm\ClientSDK")
            Else
                scope = New ManagementScope("\\" & Hostname & "\root\ccm\ClientSDK", options)
            End If

            If My.Computer.Network.Ping(Hostname, 1500) Then
                scope.Connect()
            Else
                'Der Client kann nicht direkt abgerufen werden, deshalb wird der aktuelle (letzte) Status vom SMS-Server ermittelt:
                PostLogText("Clientstatus: Client nicht erreichbar.", False)

                Return False
            End If

            Dim query As ObjectQuery = New ObjectQuery("SELECT * FROM CCM_Application")
            Dim searcher = New ManagementObjectSearcher(scope, query)

            For Each queryObj As ManagementObject In searcher.Get()
                Try
                    Dim jj As New ListViewItem
                    jj.Text = queryObj("FullName")
                    Try
                        jj.SubItems.Add(queryObj("Publisher"))
                    Catch ex As Exception
                        jj.SubItems.Add("")
                    End Try
                    Try
                        jj.SubItems.Add(queryObj("SoftwareVersion"))
                    Catch ex As Exception
                        jj.SubItems.Add("")
                    End Try
                    Try
                        jj.SubItems.Add(queryObj("Revision"))
                    Catch ex As Exception
                        jj.SubItems.Add("")
                    End Try

                    Try
                        jj.SubItems.Add(queryObj("ResolvedState"))
                        jj.SubItems.Add(queryObj("InstallState"))

                        If queryObj("ResolvedState") = "None" Then
                            jj.ImageIndex = 3
                            Exit Try
                        End If

                        If queryObj("ResolvedState") = "NotInstalled" Then
                            jj.ImageIndex = 6
                            Exit Try
                        End If

                        If queryObj("InstallState") = "Installed" Then
                            jj.ImageIndex = 4
                        Else
                            jj.ImageIndex = 6
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        Dim LastEvalTimeDt As DateTime =
                    ManagementDateTimeConverter.ToDateTime(queryObj("LastEvalTime"))
                        Dim LastInstallTimeDt As DateTime =
                    ManagementDateTimeConverter.ToDateTime(queryObj("LastInstallTime"))

                        jj.SubItems.Add(LastEvalTimeDt.ToString)
                        jj.SubItems.Add(LastInstallTimeDt.ToString)
                    Catch ex As Exception
                    End Try

                    Try
                        jj.Tag = queryObj("Id")
                    Catch ex As Exception
                    End Try

                    AddListViewItem(ListView1, jj)
                Catch ex As Exception
                End Try
            Next

            EndUpdateListView(ListView1)

            Return True
        Catch err As Exception
            PostLogText("Fehler Clientstatus: " & err.Message, True)
            Return False
        End Try
    End Function

    Public Function PostLogText(ByVal Text As String, ByVal IsError As Boolean) As Boolean
        Try
            Dim oo As New ListViewItem
            oo.Text = DateAndTime.Now
            oo.SubItems.Add(Threading.Thread.CurrentThread.ManagedThreadId)
            oo.SubItems.Add(Text)

            If IsError Then
                oo.ForeColor = Color.Red
            End If

            AddListViewItem(ListView3, oo, True)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ConvertToUnsecureString(ByVal SecureStringx As Security.SecureString) As String
        Dim unmanagedString As String = ""

        If Not SecureStringx Is Nothing Then
            If Not SecureStringx.Length = 0 Then
                Try
                    unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(SecureStringx)
                    Return Marshal.PtrToStringUni(unmanagedString)
                Catch
                    Return ""
                Finally
                    Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString)
                    FlushMemory()
                End Try
            Else
                Return ""
            End If
        Else
            Return ""
        End If
    End Function

    Public Function ConvertToSecureString(ByVal PlainText As String) As SecureString
        Try
            Dim jj As New SecureString

            For Each item As String In PlainText
                jj.AppendChar(item)
            Next

            Return jj
        Catch ex As Exception
            Return New SecureString
        End Try
    End Function

    Public Sub FlushMemory()
        GC.Collect()
        GC.WaitForPendingFinalizers()
        If (Environment.OSVersion.Platform = PlatformID.Win32NT) Then
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1)
        End If
    End Sub

    Public Function ForceRefreshPolicies(Hostname As String, Optional UseSMSServer As Boolean = True, Optional UseClient As Boolean = True) As Boolean
        Try
            If UseSMSServer Then
                PostLogText("Aktualisierungsvorgang über SMS-Client anfordern...", False)

                TriggerPolicyClientRefresh(GetClientnameResourceID(), CurrentSMSConnection)
            End If

            If UseClient Then
                PostLogText("Aktualisierungsvorgang auf SMS-Client starten...", False)

                Dim PolicyUpdateHandler As New WMIHandler
                For ind = 0 To 1
                    PolicyUpdateHandler.ExecWMIMethod(Hostname, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RefreshClientPoliciesWMIExecScope),
                                                      EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RefreshClientPoliciestWMIExecQuery),
                                                      EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RefreshClientPoliciesWMIExecMethod),
                                                      EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RefreshClientPoliciesWMIExecArgumentValue),
                                                      EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.RefreshClientPoliciesWMIExecArgumentData),
                                                      _Settings.UseCustomWMIConnectionOptions,
                                                      EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsUsername),
                                                      EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsPassword),
                                                      EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomWMIConnectionOptionsAuthority),
                                                      _Settings.CustomWMIConnectionOptionsAuthentication,
                                                      _Settings.CustomWMIConnectionOptionsImpersonation)
                Next

                PostLogText("Aktualisierungsvorgang auf SMS-Client erfolgreich.", False)

                Return True
            End If

        Catch ex As Exception
            PostLogText("Fehler: Aktualisierungsvorgang auf SMS-Client: " & ex.Message, True)
            Return False
        End Try
    End Function


    Public Function SaveListviewContentToFile(ByVal Filename As String, ByVal ListviewCtl As ListView, Optional ByVal Seperator As String = ",") As Boolean
        Try
            Dim tw As TextWriter = New StreamWriter(Filename)
            Dim listViewContent As StringBuilder = New StringBuilder()

            Dim columnhead As String = ""
            For index = 0 To ListviewCtl.Columns.Count - 1
                If Not index = ListviewCtl.Columns.Count - 1 Then
                    columnhead += ListviewCtl.Columns(index).Text & Seperator
                Else
                    columnhead += ListviewCtl.Columns(index).Text
                End If
            Next

            tw.WriteLine(columnhead)

            For item As Integer = 0 To ListviewCtl.Items.Count - 1

                For subitem As Integer = 0 To ListviewCtl.Columns.Count - 1
                    listViewContent.Append(ListviewCtl.Items(item).SubItems(subitem).Text)
                    If subitem < ListviewCtl.Columns.Count - 1 Then listViewContent.Append(Seperator)
                Next

                tw.WriteLine(listViewContent)
                listViewContent = New StringBuilder()
            Next

            tw.Close()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ToolStripTextBox2.Text = CurrentIPHostname
            ToolStripTextBox1.Text = _Settings.SMSServer

            If Not _Settings.AvailableCollectionsFilter.Count = 0 Then
                ToolStripButton10.Checked = True
            End If
            If _Settings.FilterCollectionsWithNoMembers Then
                ToolStripButton12.Checked = True
            End If
            ToolStripButton14.Checked = _Settings.SortCollections
            LiveModeButton.Checked = _Settings.EnableLiveMode
            ToolStripButton15.Checked = _Settings.ShowDevicesAndUsernameSearchPanel
            SplitContainer4.Panel1Collapsed = Not _Settings.ShowDevicesAndUsernameSearchPanel
            ToolStripButton16.Visible = _Settings.ShowCloneCollectionsButton

            SetUXThemeForAllListViews()

            RefreshGUI()
            RaiseAction(CurrentIPHostname)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        CurrentIPHostname = ToolStripTextBox2.Text
        TriggerClientCollectionMembershipsWorker()
        TriggerCollectClientPackageState()
        TriggerCollectServerPackageState()

        SetOptimalListViewStyle()
    End Sub

    Public Sub SetGroupMemberCollectionsGUIState(ByVal State As Boolean)
        GroupMemberSplash.Visible = Not State
        GroupMemberSplashLabel.Visible = Not State

        LiveModeButton.Enabled = State
        ToolStripButton3.Enabled = State
        ToolStripButton6.Enabled = State
    End Sub

    Public Sub SetClientStatePackageGUIState(ByVal State As Boolean)
        ClientPackageSplash.Visible = Not State
        ClientPackageSplashLabel.Visible = Not State
    End Sub

    Public Sub SetServerStatePackageGUIState(ByVal State As Boolean)
        ServerPackageSplash.Visible = Not State
        ServerPackageSplashLabel.Visible = Not State
    End Sub

    Public Sub SetSMSCollectionGUIState(ByVal State As Boolean)
        SMSCollectionSplash.Visible = Not State
        SMSCollectionSplashLabel.Visible = Not State

        ToolStripTextBox2.Enabled = State
        ToolStripButton2.Enabled = State
        ToolStripButton1.Enabled = State
        ToolStripButton9.Enabled = State
    End Sub

    Public Sub SetSMSClientsGUIState(ByVal State As Boolean)
        SCCMClientsSplash.Visible = Not State
        SCCMClientsSplashLabel.Visible = Not State

        ToolStripButton7.Enabled = State
        ToolStripButton8.Enabled = State
        ToolStripTextBox3.Enabled = State
    End Sub

    Public Sub SetSMSCollectionLoadGUIState(ByVal State As Boolean)
        If State Then
            SMSCollectionSplash.Image = My.Resources.adminstatus
            SMSCollectionSplashLabel.Text = "Loading..."
        Else
            SMSCollectionSplash.Image = My.Resources.NoStat
            SMSCollectionSplashLabel.Text = "No data"
        End If
    End Sub

    Public Sub SetClientStatePackageLoadGUIState(ByVal State As Boolean)
        If State Then
            ClientPackageSplash.Image = My.Resources.adminstatus
            ClientPackageSplashLabel.Text = "Loading..."
        Else
            ClientPackageSplash.Image = My.Resources.NoStat
            ClientPackageSplashLabel.Text = "No data"
        End If
    End Sub

    Public Sub SetServerStatePackageLoadGUIState(ByVal State As Boolean)
        If State Then
            ServerPackageSplash.Image = My.Resources.adminstatus
            ServerPackageSplashLabel.Text = "Loading..."
        Else
            ServerPackageSplash.Image = My.Resources.NoStat
            ServerPackageSplashLabel.Text = "No data"
        End If
    End Sub

    Public Sub SetGroupMemberCollectionsLoadGUIState(ByVal State As Boolean)
        If State Then
            GroupMemberSplash.Image = My.Resources.adminstatus
            GroupMemberSplashLabel.Text = "Loading..."
        Else
            GroupMemberSplash.Image = My.Resources.NoStat
            GroupMemberSplashLabel.Text = "No data"
        End If
    End Sub

    Public Sub SetSMSClientsLoadGUIState(ByVal State As Boolean)
        If State Then
            SCCMClientsSplash.Image = My.Resources.adminstatus
            SCCMClientsSplashLabel.Text = "Loading..."
        Else
            SCCMClientsSplash.Image = My.Resources.NoStat
            SCCMClientsSplashLabel.Text = "No data"
        End If
    End Sub

    Public Sub ResetAllGUIItems()
        SetSMSClientsLoadGUIState(False)
        SetSMSClientsGUIState(False)
        SetSMSCollectionLoadGUIState(False)
        SetClientStatePackageLoadGUIState(False)
        SetGroupMemberCollectionsLoadGUIState(False)
        SetSMSCollectionGUIState(False)
        SetClientStatePackageGUIState(False)
        SetGroupMemberCollectionsGUIState(False)
        SetServerStatePackageGUIState(False)
        SetServerStatePackageLoadGUIState(False)
    End Sub

    Public Sub SetUXThemeForAllListViews()
        Try
            SetWindowTheme(ListView1.Handle, "explorer", Nothing)
            SetWindowTheme(ListView2.Handle, "explorer", Nothing)
            SetWindowTheme(ListView3.Handle, "explorer", Nothing)
            SetWindowTheme(ListView4.Handle, "explorer", Nothing)
            SetWindowTheme(ListView5.Handle, "explorer", Nothing)
            SetWindowTheme(ListView6.Handle, "explorer", Nothing)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ZuweisenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZuweisenToolStripMenuItem.Click
        If AddToCollectionWorker.IsBusy = False Then
            AddToCollectionWorker.RunWorkerAsync()
        Else
            PostLogText("Es läuft bereits eine Zuweisung. Bitte warten Sie, bis die Collection-Mitgliedschaft aktualisiert wurde.", True)
        End If
    End Sub

    Private Sub EntfernenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EntfernenToolStripMenuItem.Click
        If DeleteFromCollectionWorker.IsBusy = False Then
            DeleteFromCollectionWorker.RunWorkerAsync()
        End If
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        ForceRefreshPolicies(CurrentIPHostname)
    End Sub

    Private Sub ToolStripTextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles ToolStripTextBox2.KeyDown
        If e.KeyValue = Keys.Enter Then
            ToolStripButton1.PerformClick()
        End If
    End Sub

    Private Sub ListView3_SizeChanged(sender As Object, e As EventArgs) Handles ListView3.SizeChanged
        AutoSizeLogColumns()
    End Sub

    Public Sub AutoSizeLogColumns()
        ListView3.Columns.Item(2).Width = ListView3.Width - ListView3.Columns.Item(0).Width - ListView3.Columns.Item(1).Width - 25
    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        TriggerClientCollectionMembershipsWorker()
    End Sub

    Public Function WaitForMembershipUpdate(ByVal WantedCount As Integer) As Boolean
        Try
            Dim TryCount As Integer = 0

            Do While True
                Threading.Thread.Sleep(1000)

                Dim CurrCollectionCount
                CurrCollectionCount = GetCollectionMembershipsCount(WatchingClientname)

                If CurrCollectionCount = WantedCount Then
                    Return True
                End If

                If TryCount > _Settings.SMSCollectionCollectTimeout Then
                    Return False
                End If

                TryCount += 1
            Loop

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub CollectionMembershipWatcher_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs)
        If e.Result = 1 Then
            PostLogText("Bestätigung von SMS: Collection erfolgreich bearbeitet.", False)
            ToolStripButton6.PerformClick()
            If LiveModeButton.Checked Then
                ForceRefreshPolicies(CurrentIPHostname)
            End If
        End If
        If e.Result = 2 Then
            PostLogText("Die Collection konnte in der angegebenen Zeitspanne nicht bearbeitet werden.", True)
        End If
    End Sub

    Private Sub ToolStripButton4_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        If ToolStripButton4.Checked Then
            SplitContainer1.Panel2Collapsed = False
        Else
            SplitContainer1.Panel2Collapsed = True
        End If
    End Sub

    Private Sub GetAllClientsWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles GetAllClientsWorker.DoWork
        Try
            Dim AllClients As WqlQueryResultsObject
            AllClients = GetAllClients()

            'Clients der Auflistung hinzufügen

            BeginUpdateListView(ListView5)
            ClearListView(ListView5)

            For Each item As WqlResultObject In AllClients
                Dim ww As New ListViewItem
                ww.Text = item.PropertyList("Name")
                ww.SubItems.Add(item.PropertyList("LastLogonUserName"))
                ww.SubItems.Add(item.PropertyList("LastLogonTimestamp"))
                ww.SubItems.Add(item.PropertyList("MACAddresses"))
                ww.SubItems.Add(item.PropertyList("IPAddresses"))
                ww.SubItems.Add(item.PropertyList("ClientVersion"))
                ww.ImageIndex = 7

                AddListViewItem(ListView5, ww)
            Next
        Catch ex As Exception
            PostLogText("Fehler: " & ex.Message, True)
        End Try
    End Sub

    Private Sub GetAllClientsWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles GetAllClientsWorker.RunWorkerCompleted
        EndUpdateListView(ListView5)
        HideEmptyColumns(ListView5)

        If ListView5.Items.Count = 0 Then
            IsClientCollectionReceived = False
            SetSMSClientsLoadGUIState(False)
            SetSMSClientsGUIState(False)

            'Die Suche nach Elementen weiterhin ermöglichen
            ToolStripButton7.Enabled = True
            ToolStripButton8.Enabled = True
            ToolStripTextBox3.Enabled = True
        Else
            IsClientCollectionReceived = True
            SetSMSClientsGUIState(True)
            SetSMSClientsLoadGUIState(False)

            CheckForOnly1Item()
        End If
    End Sub

    Private Sub ToolStripTextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles ToolStripTextBox3.KeyDown
        If e.KeyValue = Keys.Enter Then
            ToolStripButton8.PerformClick()
        End If
    End Sub

    Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs) Handles ToolStripButton8.Click
        TriggerSMSClientsQueryWorker()
    End Sub

    Private Sub ConnectToSMSMServerWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles ConnectToSMSMServerWorker.DoWork
        PostLogText("Verbinde mit SMS " & _Settings.SMSServer & " ...", False)
        CurrentSMSConnection = Connect(_Settings.SMSServer, EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.SMSUsername), EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.SMSPassword))
    End Sub

    Private Sub ConnectToSMSMServerWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles ConnectToSMSMServerWorker.RunWorkerCompleted
        If CurrentSMSConnection Is Nothing Then
            IsConnectionToSMSEtablished = False
            PostLogText("Fehler: Verbindung mit SMS " & _Settings.SMSServer, True)
            SetSMSCollectionLoadGUIState(False)

            'Benutzer ermöglichen, einen anderen SMS-Server einzugeben...
            ToolStripTextBox1.Enabled = True
            ToolStripButton2.Enabled = True
        Else
            IsConnectionToSMSEtablished = True
            'Verbindung erfolgreich hergestellt, Worker starten...
            PostLogText("Verbindung mit SMS " & _Settings.SMSServer & " erfolgreich.", False)
            SetSMSClientsGUIState(True)

            TriggerSMSCollectionsWorker()
            TriggerClientCollectionMembershipsWorker()

            If _Settings.LoadSMSClients Then
                TriggerSMSClientsWorker()
            End If
        End If
    End Sub

    Public Sub TriggerSMSCollectionsWorker()
        If IsConnectionToSMSEtablished Then
            If CollectSMSCollectionsWorker.IsBusy = False Then
                SetSMSCollectionGUIState(False)
                SetSMSCollectionLoadGUIState(True)
                CollectSMSCollectionsWorker.RunWorkerAsync()
            End If
        Else
            PostLogText("Fehler: Es besteht keine Verbindung zum SMS " & _Settings.SMSServer, True)
        End If
    End Sub

    Public Sub TriggerClientCollectionMembershipsWorker()
        If IsConnectionToSMSEtablished Then
            If CollectClientCollectionMembershipsWorker.IsBusy = False Then
                SetGroupMemberCollectionsGUIState(False)
                SetGroupMemberCollectionsLoadGUIState(True)
                CollectClientCollectionMembershipsWorker.RunWorkerAsync()
            End If
        Else
            PostLogText("Fehler: Es besteht keine Verbindung zum SMS " & _Settings.SMSServer, True)
        End If
    End Sub

    Public Sub TriggerCollectClientPackageState()
        If CollectClientPackageStateWorker.IsBusy = False Then
            SetClientStatePackageGUIState(False)
            SetClientStatePackageLoadGUIState(True)
            CollectClientPackageStateWorker.RunWorkerAsync()
        End If
    End Sub

    Public Sub TriggerCollectServerPackageState()
        If CollectServerPackageStateWorker.IsBusy = False Then
            SetServerStatePackageGUIState(False)
            SetServerStatePackageLoadGUIState(True)
            CollectServerPackageStateWorker.RunWorkerAsync()
        End If
    End Sub

    Public Sub TriggerSMSClientsWorker()
        If IsConnectionToSMSEtablished Then
            If GetAllClientsWorker.IsBusy = False Then
                SetSMSClientsGUIState(False)
                SetSMSClientsLoadGUIState(True)
                GetAllClientsWorker.RunWorkerAsync()
            End If
        Else
            PostLogText("Fehler: Es besteht keine Verbindung zum SMS " & _Settings.SMSServer, True)
        End If
    End Sub

    Public Sub TriggerSMSClientsQueryWorker()
        If IsConnectionToSMSEtablished Then
            If GetAllClientsWorker.IsBusy = False And CollectAllClientsFromCollection.IsBusy = False Then
                SetSMSClientsGUIState(False)
                SetSMSClientsLoadGUIState(True)

                SearchingClientname = ToolStripTextBox3.Text
                CollectClientSearchQueryWorker.RunWorkerAsync()
            End If
        Else
            PostLogText("Fehler: Es besteht keine Verbindung zum SMS " & _Settings.SMSServer, True)
        End If
    End Sub

    Public Sub TriggerCollectAllClientsFromCollection()
        If IsConnectionToSMSEtablished Then
            If GetAllClientsWorker.IsBusy = False And CollectAllClientsFromCollection.IsBusy = False Then
                SetSMSClientsGUIState(False)
                SetSMSClientsLoadGUIState(True)

                CollectAllClientsFromCollection.RunWorkerAsync()
            End If
        Else
            PostLogText("Fehler: Es besteht keine Verbindung zum SMS " & _Settings.SMSServer, True)
        End If
    End Sub

    Private Sub CollectSMSCollectionsWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles CollectSMSCollectionsWorker.DoWork
        LoadCollectionsFromServer(ToolStripButton10.Checked, ToolStripButton12.Checked, _Settings.SortCollections)
    End Sub

    Private Sub CollectSMSCollectionsWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles CollectSMSCollectionsWorker.RunWorkerCompleted
        If ListView2.Items.Count = 0 Then
            IsCollectionsReceived = False
            SetSMSCollectionGUIState(False)
            SetSMSCollectionLoadGUIState(False)
        Else
            IsCollectionsReceived = True
            SetSMSCollectionGUIState(True)
            SetSMSCollectionLoadGUIState(False)
        End If
        SetOptimalListViewStyle()
        HideEmptyColumns(ListView2)
        If _Settings.SortCollections Then
            SortListView(ListView2)
        End If
    End Sub

    Private Sub CollectClientCollectionMembershipsWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles CollectClientCollectionMembershipsWorker.DoWork
        CollectCollectionMemberships(ToolStripButton5.Checked, ToolStripButton13.Checked)
    End Sub

    Private Sub CollectClientCollectionMembershipsWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles CollectClientCollectionMembershipsWorker.RunWorkerCompleted
        If ListView4.Items.Count = 0 Then
            IsCollectionMembershipReceived = False
            SetGroupMemberCollectionsGUIState(False)
            SetGroupMemberCollectionsLoadGUIState(False)

            'Benutzer ermöglichen, einen neuen Clientnamen einzugeben:
            ToolStripTextBox2.Enabled = True
            ToolStripButton1.Enabled = True
            ToolStripButton6.Enabled = True
        Else
            IsCollectionMembershipReceived = True
            SetGroupMemberCollectionsGUIState(True)
            SetGroupMemberCollectionsLoadGUIState(False)
        End If
        SetOptimalListViewStyle()
        HideEmptyColumns(ListView5)
    End Sub

    Private Sub CollectClientPackageState_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles CollectClientPackageStateWorker.DoWork
        PostLogText("Aktueller Clientstatus (" & CurrentIPHostname & ") von Client ermitteln...", False)
        GetApplicationsFromClient(CurrentIPHostname)
    End Sub

    Private Sub CollectClientPackageState_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles CollectClientPackageStateWorker.RunWorkerCompleted
        EndUpdateListView(ListView1)
        If ListView1.Items.Count = 0 Then
            IsClientPackageStateReceived = False
            SetClientStatePackageGUIState(False)
            SetClientStatePackageLoadGUIState(False)

            'Benutzer ermöglichen, einen neuen Clientnamen einzugeben:
            ToolStripTextBox2.Enabled = True
            ToolStripButton1.Enabled = True
            ToolStripButton6.Enabled = True
        Else
            IsClientPackageStateReceived = True
            SetClientStatePackageGUIState(True)
            SetClientStatePackageLoadGUIState(False)
        End If
        SetOptimalListViewStyle()
        HideEmptyColumns(ListView5)
    End Sub

    Private Sub AddToCollectionWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles AddToCollectionWorker.DoWork
        For Each item As ListViewItem In ListView2.SelectedItems
            Dim collid As String
            Dim obj As WqlResultObject
            obj = item.Tag

            collid = obj.PropertyList("CollectionID")
            If AddToCollection(collid, GetClientnameResourceID, CurrentIPHostname, CurrentSMSConnection) Then
                Dim jj As ListViewItem
                jj = item.Clone

                jj.BackColor = Color.LightGreen
                AddListViewItem(ListView4, jj)
            End If
        Next

        If _Settings.SMSVerifyCollectionMembership Then
            Dim NewItemCount As Integer
            NewItemCount = ListView4.Items.Count + SkippedClientCollectionsCount

            PostLogText("Warten auf Bestätigung von SMS...", False)
            WatchingClientname = CurrentIPHostname
            If WaitForMembershipUpdate(NewItemCount) Then
                PostLogText("Bestätigung von SMS: Collection erfolgreich zugewiesen", False)
                If LiveModeButton.Checked Then
                    ForceRefreshPolicies(CurrentIPHostname, True, IsClientPackageStateReceived)
                End If
            Else
                PostLogText("Die Collection konnte in der angegebenen Zeitspanne nicht zugewiesen werden.", True)
            End If
        Else
            If LiveModeButton.Checked Then
                ForceRefreshPolicies(CurrentIPHostname, True, IsClientPackageStateReceived)
            End If
        End If
    End Sub

    Private Sub DeleteFromCollectionWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles DeleteFromCollectionWorker.DoWork
        Dim remcnt As Integer = 0

        For Each item As ListViewItem In ListView4.SelectedItems
            Dim collid As String
            Dim obj As WqlResultObject
            obj = item.Tag

            collid = obj.PropertyList("CollectionID")

            If RemoveFromCollection(collid, GetClientnameResourceID, CurrentSMSConnection) Then
                item.BackColor = Color.LightCoral
            End If

            remcnt += 1
        Next

        If _Settings.SMSVerifyCollectionMembership Then
            Dim NewItemCount As Integer
            NewItemCount = ListView4.Items.Count - remcnt + SkippedClientCollectionsCount

            PostLogText("Warten auf Bestätigung von SMS...", False)
            WatchingClientname = CurrentIPHostname
            If WaitForMembershipUpdate(NewItemCount) Then
                PostLogText("Bestätigung von SMS: Collection erfolgreich entfernt.", False)
                If LiveModeButton.Checked Then
                    ForceRefreshPolicies(CurrentIPHostname, True, IsClientPackageStateReceived)
                End If
            Else
                PostLogText("Die Collection konnte in der angegebenen Zeitspanne nicht entfernt werden.", True)
            End If
        Else
            If LiveModeButton.Checked Then
                ForceRefreshPolicies(CurrentIPHostname, True, IsClientPackageStateReceived)
            End If
        End If
    End Sub

    Private Sub AddToCollectionWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles AddToCollectionWorker.RunWorkerCompleted
        TriggerClientCollectionMembershipsWorker()
        SetOptimalListViewStyle()
    End Sub

    Private Sub DeleteFromCollectionWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles DeleteFromCollectionWorker.RunWorkerCompleted
        TriggerClientCollectionMembershipsWorker()
        SetOptimalListViewStyle()
    End Sub

    Private Sub ToolStripTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ToolStripTextBox1.KeyDown
        If e.KeyValue = Keys.Enter Then
            ToolStripButton2.PerformClick()
        End If
    End Sub

    Private Sub ListView5_Click(sender As Object, e As EventArgs) Handles ListView5.Click
        Try
            If Not ListView5.SelectedItems.Count = 0 Then
                CurrentIPHostname = ToolStripTextBox2.Text
                ToolStripTextBox2.Text = ListView5.SelectedItems(0).Text
                ToolStripButton1.PerformClick()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            ToolStripButton1.PerformClick()
        End If
        If e.KeyCode = Keys.F6 Then
            ToolStripButton6.PerformClick()
        End If
        If e.KeyCode = Keys.F7 Then
            ToolStripButton3.PerformClick()
        End If
    End Sub

    Private Sub CollectClientSearchQuery_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles CollectClientSearchQueryWorker.DoWork
        Try
            Dim AllQueryClients As WqlQueryResultsObject
            AllQueryClients = GetClientsFromSearchPattern(SearchingClientname)

            Dim AllQueryUsernames As WqlQueryResultsObject
            AllQueryUsernames = GetClientsFromUsernamePattern(SearchingClientname)

            'Clients der Auflistung hinzufügen

            BeginUpdateListView(ListView5)
            ClearListView(ListView5)

            'Geräte mit passendem Clientnamen hinzufügen

            If AllQueryClients IsNot Nothing Then
                For Each item As WqlResultObject In AllQueryClients
                    Dim ww As New ListViewItem
                    ww.Text = item.PropertyList("Name")
                    ww.SubItems.Add(item.PropertyList("LastLogonUserName"))
                    ww.SubItems.Add(item.PropertyList("LastLogonTimestamp"))
                    ww.SubItems.Add(item.PropertyList("MACAddresses"))
                    ww.SubItems.Add(item.PropertyList("IPAddresses"))
                    ww.SubItems.Add(item.PropertyList("ClientVersion"))
                    ww.ImageIndex = 7

                    AddListViewItem(ListView5, ww)
                Next
            End If

            'Geräte mit passendem Username hinzufügen
            If AllQueryUsernames IsNot Nothing Then
                For Each item As WqlResultObject In AllQueryUsernames
                    Dim ww As New ListViewItem
                    ww.Text = item.PropertyList("Name")
                    ww.SubItems.Add(item.PropertyList("LastLogonUserName"))
                    ww.SubItems.Add(item.PropertyList("LastLogonTimestamp"))
                    ww.SubItems.Add(item.PropertyList("MACAddresses"))
                    ww.SubItems.Add(item.PropertyList("IPAddresses"))
                    ww.SubItems.Add(item.PropertyList("ClientVersion"))
                    ww.ImageIndex = 7

                    AddListViewItem(ListView5, ww)
                Next
            End If

        Catch ex As Exception
            PostLogText("Fehler: " & ex.Message, True)
        End Try
    End Sub

    Private Sub CollectClientSearchQueryWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles CollectClientSearchQueryWorker.RunWorkerCompleted
        EndUpdateListView(ListView5)

        If ListView5.Items.Count = 0 Then
            SetSMSClientsLoadGUIState(False)
            SetSMSClientsGUIState(False)

            ToolStripButton7.Enabled = True
            ToolStripButton8.Enabled = True
            ToolStripTextBox3.Enabled = True

            PostLogText("Kein Client gefunden.", True)
        Else
            SetSMSClientsGUIState(True)
            SetSMSClientsLoadGUIState(False)

            CheckForOnly1Item()
        End If

        SetOptimalListViewStyle()
        HideEmptyColumns(ListView5)
    End Sub

    Public Sub CheckForOnly1Item()
        If ListView5.Items.Count = 1 Then
            'Es ist nur ein Element vorhanden, direkt das Element laden:
            ListView5.Items(0).Selected = True

            CurrentIPHostname = ToolStripTextBox2.Text
            ToolStripTextBox2.Text = ListView5.Items(0).Text
            ToolStripButton1.PerformClick()
        End If
    End Sub

    Private Sub AlleGeräteMitDieserCollectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlleGeräteMitDieserCollectionToolStripMenuItem.Click
        TriggerCollectAllClientsFromCollection()
    End Sub

    Private Sub LogSpeichernToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogSpeichernToolStripMenuItem.Click
        SaveTextfileDlg.ShowDialog()
        If Not SaveTextfileDlg.FileName = "" Then
            SaveListviewContentToFile(SaveTextfileDlg.FileName, ListView3)
        End If
    End Sub

    Private Sub ExportInDateiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportInDateiToolStripMenuItem.Click
        SaveTextfileDlg.ShowDialog()
        If Not SaveTextfileDlg.FileName = "" Then
            SaveListviewContentToFile(SaveTextfileDlg.FileName, ListView2)
        End If
    End Sub

    Private Sub ExportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToolStripMenuItem.Click
        SaveTextfileDlg.ShowDialog()
        If Not SaveTextfileDlg.FileName = "" Then
            SaveListviewContentToFile(SaveTextfileDlg.FileName, ListView4)
        End If
    End Sub

    Private Sub ExportInDateiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExportInDateiToolStripMenuItem1.Click
        SaveTextfileDlg.ShowDialog()
        If Not SaveTextfileDlg.FileName = "" Then
            SaveListviewContentToFile(SaveTextfileDlg.FileName, ListView1)
        End If
    End Sub

    Private Sub CollectAllClientsFromCollection_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles CollectAllClientsFromCollection.DoWork
        Try
            Dim kk As WqlResultObject
            kk = ListView2.SelectedItems(0).Tag

            CurrentClientsFromCollectionMembership = GetAllClientsFromCollectionMembership(kk.PropertyList("CollectionID"))

            BeginUpdateListView(ListView5)
            ClearListView(ListView5)

            For Each item As WqlResultObject In CurrentClientsFromCollectionMembership
                Dim ww As New ListViewItem
                ww.Text = item.GetSingleItem("SMS_R_System").PropertyList("Name")
                ww.SubItems.Add(item.GetSingleItem("SMS_R_System").PropertyList("LastLogonUserName"))
                ww.SubItems.Add(item.GetSingleItem("SMS_R_System").PropertyList("LastLogonTimestamp"))
                ww.SubItems.Add(item.GetSingleItem("SMS_R_System").PropertyList("LastLogonUserName"))
                ww.SubItems.Add(item.GetSingleItem("SMS_R_System").PropertyList("LastLogonTimestamp"))
                ww.SubItems.Add(item.GetSingleItem("SMS_R_System").PropertyList("MACAddresses"))
                ww.SubItems.Add(item.GetSingleItem("SMS_R_System").PropertyList("IPAddresses"))
                ww.SubItems.Add(item.GetSingleItem("SMS_R_System").PropertyList("ClientVersion"))
                ww.ImageIndex = 7

                AddListViewItem(ListView5, ww)
            Next
        Catch ex As Exception
            PostLogText("Fehler: " & ex.Message, True)
        End Try
    End Sub

    Private Sub CollectAllClientsFromCollection_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles CollectAllClientsFromCollection.RunWorkerCompleted
        EndUpdateListView(ListView5)

        If ListView5.Items.Count = 0 Then
            IsClientCollectionReceived = False
            SetSMSClientsLoadGUIState(False)
            SetSMSClientsGUIState(False)
        Else
            IsClientCollectionReceived = True
            SetSMSClientsGUIState(True)
            SetSMSClientsLoadGUIState(False)

            CheckForOnly1Item()
        End If

        SetOptimalListViewStyle()
        HideEmptyColumns(ListView5)
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        TriggerSMSClientsWorker()
    End Sub

    Private Sub ObjekteigenschaftenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObjekteigenschaftenToolStripMenuItem.Click
        Try
            Dim ww As WqlResultObject
            ww = ListView2.SelectedItems(0).Tag
            Dim gg As New ItemPropDlg
            gg.PropertyObj = ww
            gg.Show()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs) Handles ToolStripButton9.Click
        Dim kk As New ManualWQLQueryDlg
        kk.CurrentSMSConnection = CurrentSMSConnection
        kk.Show()
    End Sub

    Private Sub ObjekteigenschaftenToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ObjekteigenschaftenToolStripMenuItem1.Click
        Try
            Dim ww As WqlResultObject
            ww = ListView4.SelectedItems(0).Tag
            Dim gg As New ItemPropDlg
            gg.PropertyObj = ww
            gg.Show()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ObjekteigenschaftenToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ObjekteigenschaftenToolStripMenuItem2.Click
        Try
            Dim ww As WqlResultObject
            ww = ListView1.SelectedItems(0).Tag
            Dim gg As New ItemPropDlg
            gg.PropertyObj = ww
            gg.Show()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CollectServerPackageStateWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles CollectServerPackageStateWorker.DoWork
        PostLogText("Aktueller Clientstatus (" & CurrentIPHostname & ") von SMS " & _Settings.SMSServer & " ermitteln...", False)
        GetApplicationsFromServer(CurrentIPHostname)
    End Sub

    Private Sub CollectServerPackageStateWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles CollectServerPackageStateWorker.RunWorkerCompleted
        EndUpdateListView(ListView6)
        If ListView6.Items.Count = 0 Then
            IsServerPackageStateReceived = False
            SetServerStatePackageGUIState(False)
            SetServerStatePackageLoadGUIState(False)
        Else
            IsServerPackageStateReceived = True
            SetServerStatePackageGUIState(True)
            SetServerStatePackageLoadGUIState(False)
        End If
        SetOptimalListViewStyle()
        HideEmptyColumns(ListView5)
    End Sub

    Private Sub NurClientstatusAktualisierenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NurClientstatusAktualisierenToolStripMenuItem.Click
        TriggerCollectClientPackageState()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        TriggerCollectServerPackageState()
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        SaveTextfileDlg.ShowDialog()
        If Not SaveTextfileDlg.FileName = "" Then
            SaveListviewContentToFile(SaveTextfileDlg.FileName, ListView6)
        End If
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Try
            Dim ww As WqlResultObject
            ww = ListView6.SelectedItems(0).Tag
            Dim gg As New ItemPropDlg
            gg.PropertyObj = ww
            gg.Show()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton11_Click(sender As Object, e As EventArgs) Handles ToolStripButton11.Click
        Dim kk As New ListViewSearchFrm
        kk.ListViewCtl = ListView2
        kk.Show()
    End Sub

    Private Sub ToolStripButton15_Click(sender As Object, e As EventArgs) Handles ToolStripButton15.Click
        SplitContainer4.Panel1Collapsed = Not ToolStripButton15.Checked
    End Sub

    Public Function WakeUpClient(ByVal ComputerResourceId As String, ByVal SMSServerCollectionID As String, ByVal connection As WqlConnectionManager) As Boolean
        Try
            Dim oCollection As IResultObject = connection.GetInstance("SMS_SleepServer")
            Dim inParams As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
            inParams.Add("MachineIDs", ComputerResourceId)
            inParams.Add("CollectionID", SMSServerCollectionID)
            oCollection.ExecuteMethod("MachinesToWakeup", inParams)

            Return True
        Catch ex As Exception
            PostLogText("Error: " & ex.Message, True)
            Return False
        End Try
    End Function

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        If WakeUpClient(GetClientnameResourceID, _Settings.ClientWakeUpServerCollectionID, CurrentSMSConnection) Then
            MsgBox("Client Wake-Up successful!")
        Else
            MsgBox("Client Wake-Up failed. Please check configuration.")
        End If
    End Sub

    Private Sub ToolStripButton14_Click(sender As Object, e As EventArgs) Handles ToolStripButton14.Click
        If ToolStripButton14.Checked Then
            _Settings.SortCollections = True
        Else
            _Settings.SortCollections = False
        End If
    End Sub

    Private Sub CloneCollectionFromDeviceWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles CloneCollectionFromDeviceWorker.DoWork
        Try
            Dim arglist As List(Of String)
            arglist = e.Argument

            Dim GetCollectionQuery = "SELECT SMS_Collection.* FROM SMS_FullCollectionMembership, SMS_Collection where name = '" & arglist(0) & "' and SMS_FullCollectionMembership.CollectionID = SMS_Collection.CollectionID"
            CurrentSMSClientCollectionMembership = CurrentSMSConnection.QueryProcessor.ExecuteQuery(GetCollectionQuery)

            Dim collectionlist As New List(Of String)
            Dim cloneobjlist As New List(Of WqlResultObject)

            If Not _Settings.CloneCollectionsFilter.Count = 0 Then
                For Each item As WqlResultObject In CurrentSMSClientCollectionMembership
                    For index = 0 To _Settings.CloneCollectionsFilter.Count - 1
                        If _Settings.CloneCollectionsFilter(index).Enabled Then
                            If item.PropertyList("Name").Contains(_Settings.CloneCollectionsFilter(index).FilterText) Then
                                collectionlist.Add(item.PropertyList("CollectionID"))
                                cloneobjlist.Add(item)
                            End If
                        End If
                    Next
                Next
            Else
                For Each item As WqlResultObject In CurrentSMSClientCollectionMembership
                    collectionlist.Add(item.PropertyList("CollectionID"))
                Next
            End If

            Dim destinationclientres As String
            destinationclientres = GetSpecificClientResourceID(arglist(1))

            For index = 0 To collectionlist.Count - 1
                If AddToCollection(collectionlist(index), destinationclientres, arglist(1), CurrentSMSConnection) Then
                    Dim qq As New ListViewItem
                    qq.Text = cloneobjlist(index).PropertyList("Name")
                    qq.SubItems.Add(cloneobjlist(index).PropertyList("MemberCount"))
                    qq.SubItems.Add(cloneobjlist(index).PropertyList("CollectionID"))
                    qq.SubItems.Add(cloneobjlist(index).PropertyList("Comment"))
                    qq.SubItems.Add(cloneobjlist(index).PropertyList("CollectionType"))
                    qq.Tag = cloneobjlist(index)
                    qq.ImageIndex = 1
                    qq.BackColor = Color.LightGreen

                    AddListViewItem(ListView4, qq)
                End If
            Next

            If LiveModeButton.Checked Then
                ForceRefreshPolicies(arglist(1), True, IsClientPackageStateReceived)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CloneCollectionFromDeviceWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles CloneCollectionFromDeviceWorker.RunWorkerCompleted
        TriggerClientCollectionMembershipsWorker()
        SetOptimalListViewStyle()
    End Sub

    Private Sub ToolStripButton16_Click(sender As Object, e As EventArgs) Handles ToolStripButton16.Click
        Try
            Dim arglist As New List(Of String)
            arglist.Add(ListView5.SelectedItems(0).Text)
            arglist.Add(ToolStripTextBox2.Text)

            If arglist(0).ToLower = arglist(1).ToLower Then
                MsgBox("Cannot clone client to itself!", MsgBoxStyle.Exclamation)
                Exit Try
            End If

            Dim clonemsgresult As MsgBoxResult
            clonemsgresult = MsgBox("Do you want to clone the collections from device " & arglist(0) & " to device " & arglist(1) & "?", MsgBoxStyle.YesNo)

            If clonemsgresult = MsgBoxResult.Yes Then
                If Not CloneCollectionFromDeviceWorker.IsBusy Then
                    CloneCollectionFromDeviceWorker.RunWorkerAsync(arglist)
                Else
                    MsgBox("System is busy, please wait.", MsgBoxStyle.Exclamation)
                End If
            End If
        Catch ex As Exception
            MsgBox("No target device selected!", MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub SortAscendingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SortAscendingToolStripMenuItem.Click
        SortListView(ListView2, SortOrder.Ascending)
    End Sub

    Private Sub SortDescendingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SortDescendingToolStripMenuItem.Click
        SortListView(ListView2, SortOrder.Descending)
    End Sub
End Class
