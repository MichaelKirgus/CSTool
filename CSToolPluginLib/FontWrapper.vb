'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing

Public Class SerializableFont
    Public Property FontFamily As String
    Public Property GraphicsUnit As GraphicsUnit
    Public Property Size As Single
    Public Property Style As FontStyle

    Private Sub New()
    End Sub

    Public Sub New(ByVal f As Font)
        FontFamily = f.FontFamily.Name
        GraphicsUnit = f.Unit
        Size = f.Size
        Style = f.Style
    End Sub

    Public Shared Function FromFont(ByVal f As Font) As SerializableFont
        Return New SerializableFont(f)
    End Function

    Public Function ToFont() As Font
        Return New Font(FontFamily, Size, Style, GraphicsUnit)
    End Function
End Class
