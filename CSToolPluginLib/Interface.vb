'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports System.Windows.Forms
Imports CSToolLogLib
Imports WeifenLuo.WinFormsUI.Docking

Public Interface ICSToolInterface
    'Functions
    Function LoadPlugin() As Boolean
    Function UnloadPlugin() As Boolean
    Function SavePluginSettings(Optional ByVal Filename As String = "") As Boolean
    Function LoadPluginSettings(Optional ByVal Filename As String = "") As Boolean
    Function GetNewSettingsObjectInstance() As Object
    Function GetNewUserControlObjectInstance() As UserControl
    Function GetNewInterfaceObjectInstance() As Object
    Function RefreshGUI() As Boolean
    Function LockGUI() As Boolean
    Function UnlockGUI() As Boolean
    Function RaiseActions(ByVal HostnameOrIP As String) As Boolean
    Function RaiseCustomAction(ByVal UserStr As String) As Boolean
    Function GetCredential() As List(Of CredentialEntry)

    'Properties
    ReadOnly Property PluginName() As String
    ReadOnly Property PluginVersion() As String
    ReadOnly Property PluginPublisher() As String
    ReadOnly Property GUILoaded() As Boolean
    ReadOnly Property GUIExists() As Boolean
    ReadOnly Property MultiInstance() As Boolean
    ReadOnly Property PluginType As PluginTypeEnum
    ReadOnly Property WindowTitle As String
    ReadOnly Property UserControl As UserControl
    ReadOnly Property UserControlIcon As Icon
    ReadOnly Property SupportFloat As Boolean
    ReadOnly Property SupportOpenInIndependentWindow As Boolean
    ReadOnly Property UserCredentialSet As Boolean
    ReadOnly Property UserCredentialWindow As Form
    ReadOnly Property EnvironmentProviderClass As EnvironmentProvider
    ReadOnly Property NeedsEnvironmentVariables As Boolean
    ReadOnly Property SupportsRaisingActions As Boolean


    'Life-Time Properties
    Property PluginGUID As String
    Property UserSettingsClass As Object
    Property CurrentWindowTitle As String
    Property CurrentLogInstance As LogLib
    Property RaisingActionsEnabled As Boolean
    Property PluginSettingsChanged As Boolean
    Property EnvironmentRuntimeVariables As List(Of KeyValuePair(Of String, String))

    Enum PluginTypeEnum
        GUIWindow = 0
        CredentialManager = 1
        EnvironmentManager = 2
    End Enum
End Interface
