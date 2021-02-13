'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.IO

Public Class PluginHelper

    Public PluginCollection As New List(Of CSToolPluginLib.ICSToolInterface)
    Public AssemblyCollection As New List(Of System.Reflection.Assembly)
    Public TypeCollection As New List(Of Type)
    Public LastErrorObj As ErrObject

    Public Function LoadAllPlugins(ByVal PluginDir As String) As Boolean
        Try
            If IO.Directory.Exists(PluginDir) Then
                For Each plugin As String In Directory.GetFiles(PluginDir, "*Plugin.dll")
                    Dim myplugin As CSToolPluginLib.ICSToolInterface
                    myplugin = PluginConnector.LoadPlugIn(plugin)
                    PluginCollection.Add(myplugin)
                Next
            End If

            AssemblyCollection = PluginConnector.AssemblyCollection
            TypeCollection = PluginConnector.TypeCollection

            Return True
        Catch ex As Exception
            LastErrorObj = Err()
            Return False
        End Try
    End Function

    Public Function GetPluginFilepathFromInterfaceInstance(ByVal InterfaceInstance As CSToolPluginLib.ICSToolInterface) As String
        Try
            If Not PluginCollection.Count = 0 And Not AssemblyCollection.Count = 0 Then
                For index = 0 To PluginCollection.Count - 1
                    If PluginCollection(index).PluginName = InterfaceInstance.PluginName Then
                        Return AssemblyCollection(index).Location
                    End If
                Next
                Return ""
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function LoadPluginFile(ByVal PluginFile As String) As Boolean
        Try
            If IO.File.Exists(PluginFile) Then
                Dim myplugin As CSToolPluginLib.ICSToolInterface
                myplugin = PluginConnector.LoadPlugIn(PluginFile)
                PluginCollection.Add(myplugin)
            End If

            AssemblyCollection = PluginConnector.AssemblyCollection
            TypeCollection = PluginConnector.TypeCollection

            Return True
        Catch ex As Exception
            LastErrorObj = Err()
            Return False
        End Try
    End Function

    Public Function CreateNewPluginInstance(ByVal PluginIndex As Integer) As CSToolPluginLib.ICSToolInterface
        Try
            Dim NewInterface As CSToolPluginLib.ICSToolInterface
            NewInterface = CType(AssemblyCollection(PluginIndex).CreateInstance(TypeCollection(PluginIndex).FullName), CSToolPluginLib.ICSToolInterface)
            NewInterface.PluginGUID = TypeCollection(PluginIndex).GUID.ToString

            Return NewInterface
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function LoadPlugin(ByVal PluginName As String) As Boolean
        Try
            For index = 0 To PluginCollection.Count - 1
                Dim jj As CSToolPluginLib.ICSToolInterface
                jj = PluginCollection(index)

                If jj.PluginName = PluginName Then
                    jj.LoadPluginSettings()
                    jj.LoadPlugin()
                End If
            Next

            Return True
        Catch ex As Exception
            LastErrorObj = Err()
            Return False
        End Try
    End Function

    Public Function UnloadAllPlugins() As Boolean
        Try
            For index = 0 To PluginCollection.Count - 1
                Dim jj As CSToolPluginLib.ICSToolInterface
                jj = PluginCollection(index)
                If jj.GUILoaded Then
                    jj.UnloadPlugin()
                End If
            Next

            Return True
        Catch ex As Exception
            LastErrorObj = Err()
            Return False
        End Try
    End Function

    Public Function SaveAllPlugInSettings() As Boolean
        Try
            For index = 0 To PluginCollection.Count - 1
                Dim jj As CSToolPluginLib.ICSToolInterface
                jj = PluginCollection(index)
                If jj.GUILoaded Then
                    jj.SavePluginSettings()
                End If
            Next

            Return True
        Catch ex As Exception
            LastErrorObj = Err()
            Return False
        End Try
    End Function

End Class
