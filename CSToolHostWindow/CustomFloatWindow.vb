'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports WeifenLuo.WinFormsUI.Docking
Imports System.Windows.Forms

Public Class CustomFloatWindow
    Inherits FloatWindow

    Public Sub New(ByVal dockPanel As DockPanel, ByVal pane As DockPane)
        MyBase.New(dockPanel, pane)
        DoubleClickTitleBarToDock = False
        FormBorderStyle = FormBorderStyle.Sizable
    End Sub

    Public Sub New(ByVal dockPanel As DockPanel, ByVal pane As DockPane, ByVal bounds As Rectangle)
        MyBase.New(dockPanel, pane, bounds)
        DoubleClickTitleBarToDock = False
        FormBorderStyle = FormBorderStyle.Sizable
    End Sub
End Class



