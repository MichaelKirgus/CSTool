'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Public Class PluginConnector
    Public Shared AssemblyCollection As New List(Of System.Reflection.Assembly)
    Public Shared TypeCollection As New List(Of Type)

    Public Shared Function LoadPlugIn(ByVal strFile As String) As CSToolPluginLib.ICSToolInterface
        Dim vPlugIn As CSToolPluginLib.ICSToolInterface
        Dim a As System.Reflection.Assembly = System.Reflection.Assembly.LoadFile(strFile)
        Dim types() As Type = a.GetTypes()
        For Each pType As Type In types
            Try
                If pType.FullName.EndsWith("ClientModule+Client") Then
                    'Now try loading dll...
                    vPlugIn = CType(a.CreateInstance(pType.FullName), CSToolPluginLib.ICSToolInterface)
                    vPlugIn.PluginGUID = pType.GUID.ToString
                    AssemblyCollection.Add(a)
                    TypeCollection.Add(pType)
                    'dll is valid
                    Return vPlugIn
                End If
            Catch ex As Exception
            End Try
        Next

        Return Nothing
    End Function
End Class
