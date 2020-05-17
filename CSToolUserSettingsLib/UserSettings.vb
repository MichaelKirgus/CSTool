'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports System.Windows.Forms
Imports CSCustomActionHelper
Imports CSToolLogLib

<Serializable> Public Class UserSettings
    Implements ICloneable

    Public Property LastWindowSize As Size = New Size(800, 450)
    Public Property LastNormalWindowSize As Size = New Size(800, 450)
    Public Property LastWindowLocation As Point = New Point(100, 100)
    Public Property LastNormalWindowLocation As Point = New Point(100, 100)
    Public Property LastWindowState As FormWindowState = FormWindowState.Normal
    Public Property SettingName As String = "Default"
    Public Property LastSettingName As String = "Default"
    Public Property FriendlyUsername As String = ""
    Public Property TemplateName As String = "Default"
    Public Property TemplateDescription As String = "Default (build-in)"
    Public Property Autostart As Boolean = False
    Public Property StartType As StartTypeEnum = StartTypeEnum.NewWindow
    Public Property LogSettings As New LogSettings
    Public Property Plugins As New List(Of PluginSettings)
    Public Property UserTemplates As New List(Of UserSettings)
    Public Property LoadPinnedTemplates As Boolean = True
    Public Property CustomActions As New List(Of CustomActionEntry)
    Public Property CentralCustomActions As String = ""
    Public Property EnableCustomAutostartActions As Boolean = False
    Public Property CustomAutostartActions As New List(Of CustomActionEntry)
    Public Property ShowWarningOnCustomActions As Boolean = True
    Public Property UseAsyncPluginMessaging As Boolean = True
    Public Property ShowWorkplaceTemplateForm As Boolean = False

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MemberwiseClone()
    End Function

    Public Enum StartTypeEnum As Integer
        NewWindow = 0
        NewInstance = 1
    End Enum
End Class
