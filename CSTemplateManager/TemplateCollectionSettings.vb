'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports System.Windows.Forms

Public Class TemplateCollectionSettings
    Implements ICloneable

    Public Property TemplateName As String = ""
    Public Property TemplateGUID As String = ""
    Public Property TemplateDescription As String = ""
    Public Property DefaultWindowStyle As DefaultWindowStyleEnum = DefaultWindowStyleEnum.DockDocument
    Public Property InitialWindowSize As New Size(350, 350)
    Public Property InitialWindowLocation As New Point(50, 50)
    Public Property InitialWindowState As FormWindowState = FormWindowState.Normal
    Public Property ForceInitialRaiseAction As Boolean = False
    Public Property ForceInitialRefresh As Boolean = False
    Public Property PinToShortcutMenu As Boolean = False
    Public Property MasterTemplate As Boolean = False

    Public PluginName As String = ""

    Public Enum DefaultWindowStyleEnum
        IndependentWindow = 0
        FloatWindow = 1
        DockDocument = 2
        DockTop = 3
        DockBottom = 4
        DockRight = 5
        DockLeft = 6
        StandaloneProcessWindow = 7
        StandaloneNonPersistentProcessWindow = 8
    End Enum

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MemberwiseClone()
    End Function
End Class
