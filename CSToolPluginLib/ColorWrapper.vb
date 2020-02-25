'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.ComponentModel
Imports System.Drawing

<EditorBrowsable(System.ComponentModel.EditorBrowsableState.Always)>
<Serializable> Public Class ColorWrapper
    Private color As Color

    Public Sub New(ByVal color As Color)
        Me.color = color
    End Sub

    Public Sub New()
    End Sub

    Public Property Argb As Integer
        Get
            Return color.ToArgb()
        End Get
        Set(ByVal value As Integer)
            color = Color.FromArgb(value)
        End Set
    End Property
End Class
