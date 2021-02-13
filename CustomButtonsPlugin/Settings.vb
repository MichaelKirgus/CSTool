'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports System.Windows.Forms
Imports CSCustomActionHelper

<Serializable> Public Class Settings
    Public Property InitialTitle As String = ""
    Public Property CustomButtonsCollection As New List(Of CustomButtonEntry)
End Class

<Serializable> Public Class CustomButtonEntry
    Public Property ButtonText As String = ""
    Public Property ButtonFlatStyle As FlatStyle = FlatStyle.Standard
    Public Property ButtonWidth As Integer = 50
    Public Property ButtonHeight As Integer = 50
    Public Property ButtonIconFile As String = ""
    Public Property ButtonIconFileAlignment As ContentAlignment = ContentAlignment.MiddleCenter
    Public Property ButtonTextAlignment As ContentAlignment = ContentAlignment.MiddleCenter
    Public Property ButtonImageTextRelation As TextImageRelation = TextImageRelation.Overlay
    Public Property RaisingActions As New List(Of CustomActionEntry)
End Class
