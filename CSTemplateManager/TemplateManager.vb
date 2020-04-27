'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolPluginLib
Imports CSToolWindowManager

Public Class TemplateManager
    Public TemplateHandlerObj As New TemplateHandler
    Public WindowHandlerObj As New WindowManager

    Public CurrentTemplates As List(Of TemplateCollectionEntry)
    Public _CurrentPinnedTemplates As List(Of TemplateCollectionSettings)

    Public Function GetTemplates(ByVal TemplatePath As String, ByVal PluginCollection As List(Of ICSToolInterface)) As List(Of TemplateCollectionEntry)
        Try
            Dim TemplateListObj As New List(Of TemplateCollectionEntry)

            If IO.Directory.Exists(TemplatePath) Then
                Dim plugguids As String()
                plugguids = IO.Directory.GetDirectories(TemplatePath)
                If Not plugguids.Count = 0 Then
                    For index = 0 To plugguids.Count - 1
                        Dim TemplateEntryObj As New TemplateCollectionEntry
                        Dim dirinfo As New IO.DirectoryInfo(plugguids(index))
                        Dim currentplugin As ICSToolInterface
                        currentplugin = WindowHandlerObj.GetPluginByGUID(dirinfo.Name, PluginCollection)
                        If Not IsNothing(currentplugin) Then
                            TemplateEntryObj.PluginName = currentplugin.PluginName
                            Dim subfolders As String()
                            subfolders = IO.Directory.GetDirectories(dirinfo.FullName)
                            If Not subfolders.Count = 0 Then
                                For subindex = 0 To subfolders.Count - 1
                                    Dim TemplateSettingsObj As TemplateCollectionSettings
                                    TemplateSettingsObj = TemplateHandlerObj.LoadSettings(subfolders(subindex) & "\TemplateSettings.xml")
                                    TemplateEntryObj.PluginTemplates.Add(TemplateSettingsObj)
                                Next
                            End If
                            TemplateListObj.Add(TemplateEntryObj)
                        End If
                    Next
                End If
            End If

            Return TemplateListObj
        Catch ex As Exception
            Return New List(Of TemplateCollectionEntry)
        End Try
    End Function

    Public Function GetPinnedTemplates(ByVal Collection As List(Of TemplateCollectionEntry)) As List(Of TemplateCollectionSettings)
        Try
            Dim resultlist As New List(Of TemplateCollectionSettings)

            If Not Collection.Count = 0 Then
                For index = 0 To Collection.Count - 1
                    If Not Collection(index).PluginTemplates.Count = 0 Then
                        For plugind = 0 To Collection(index).PluginTemplates.Count - 1
                            If Collection(index).PluginTemplates(plugind).PinToShortcutMenu Then
                                Collection(index).PluginTemplates(plugind).PluginName = Collection(index).PluginName
                                resultlist.Add(Collection(index).PluginTemplates(plugind))
                            End If
                        Next
                    End If
                Next
            End If

            Return resultlist
        Catch ex As Exception
            Return New List(Of TemplateCollectionSettings)
        End Try
    End Function

    Public Function RefreshTemplateInCollection(ByVal Collection As List(Of TemplateCollectionEntry), ByVal TemplateGUID As String, ByVal TemplateSettingsObj As TemplateCollectionSettings) As Boolean
        Try
            If Not Collection.Count = 0 Then
                For index = 0 To Collection.Count - 1
                    If Not Collection(index).PluginTemplates.Count = 0 Then
                        For plugind = 0 To Collection(index).PluginTemplates.Count - 1
                            If Collection(index).PluginTemplates(plugind).TemplateGUID = TemplateGUID Then
                                Collection(index).PluginTemplates(plugind) = TemplateSettingsObj
                                Exit For
                            End If
                        Next
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function AddTemplateToCollection(ByVal Collection As List(Of TemplateCollectionEntry), ByVal Plugin As ICSToolInterface, ByVal TemplateSettingsObj As TemplateCollectionSettings) As Boolean
        Try
            If Not Collection.Count = 0 Then
                Dim found As Boolean = False
                For index = 0 To Collection.Count - 1
                    If Collection(index).PluginName = Plugin.PluginName Then
                        found = True
                        Collection(index).PluginTemplates.Add(TemplateSettingsObj)
                    End If
                Next
                If found = False Then
                    Dim TemplateEntryObj As New TemplateCollectionEntry
                    TemplateEntryObj.PluginName = Plugin.PluginName
                    TemplateEntryObj.PluginTemplates.Add(TemplateSettingsObj)
                    Collection.Add(TemplateEntryObj)
                End If
                Return True
            Else
                Dim TemplateEntryObj As New TemplateCollectionEntry
                TemplateEntryObj.PluginName = Plugin.PluginName
                TemplateEntryObj.PluginTemplates.Add(TemplateSettingsObj)
                Collection.Add(TemplateEntryObj)
                Return True
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function RemoveTemplateFromCollection(ByVal Collection As List(Of TemplateCollectionEntry), ByVal TemplateGUID As String) As Boolean
        Try
            If Not Collection.Count = 0 Then
                For index = 0 To Collection.Count - 1
                    If Not Collection(index).PluginTemplates.Count = 0 Then
                        For plugind = 0 To Collection(index).PluginTemplates.Count - 1
                            If Collection(index).PluginTemplates(plugind).TemplateGUID = TemplateGUID Then
                                Collection(index).PluginTemplates.Remove(Collection(index).PluginTemplates(plugind))
                                Exit For
                            End If
                        Next
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetAllTemplatesFromPluginName(ByVal Collection As List(Of TemplateCollectionEntry), ByVal PluginName As String) As List(Of TemplateCollectionSettings)
        Try
            Dim collobj As New List(Of TemplateCollectionSettings)

            If Not Collection.Count = 0 Then
                For index = 0 To Collection.Count - 1
                    If Collection(index).PluginName = PluginName Then
                        If Not Collection(index).PluginTemplates.Count = 0 Then
                            collobj.AddRange(Collection(index).PluginTemplates)
                        End If
                    End If
                Next
            End If

            Return collobj
        Catch ex As Exception
            Return New List(Of TemplateCollectionSettings)
        End Try
    End Function

    Public Function GetPluginTemplateSettingsFilePath(ByVal TemplatePath As String, ByVal Plugin As ICSToolInterface, ByVal TemplateSettingsObj As TemplateCollectionSettings) As String
        Try
            Dim foldername As String
            If TemplateSettingsObj.TemplateGUID = "" Then
                Return ""
            Else
                foldername = TemplateSettingsObj.TemplateGUID
                Return TemplatePath & "\" & Plugin.PluginGUID & "\" & foldername & "\PluginSettings.xml"
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function SaveTemplate(ByVal TemplatePath As String, ByVal Plugin As ICSToolInterface, ByVal TemplateSettingsObj As TemplateCollectionSettings, ByVal PluginSettingsObj As Object) As String
        Try
            If Not IO.Directory.Exists(TemplatePath) Then
                IO.Directory.CreateDirectory(TemplatePath)
            End If
            If Not IO.Directory.Exists(TemplatePath & "\" & Plugin.PluginGUID) Then
                IO.Directory.CreateDirectory(TemplatePath & "\" & Plugin.PluginGUID)
            End If
            Dim foldername As String
            If TemplateSettingsObj.TemplateGUID = "" Then
                foldername = Guid.NewGuid.ToString
                If Not IO.Directory.Exists(TemplatePath & "\" & Plugin.PluginGUID & "\" & foldername) Then
                    IO.Directory.CreateDirectory(TemplatePath & "\" & Plugin.PluginGUID & "\" & foldername)
                End If
                TemplateSettingsObj.TemplateGUID = foldername
            Else
                foldername = TemplateSettingsObj.TemplateGUID
            End If
            TemplateHandlerObj.SaveSettings(TemplateSettingsObj, TemplatePath & "\" & Plugin.PluginGUID & "\" & foldername & "\TemplateSettings.xml")
            Plugin.UserSettingsClass = PluginSettingsObj
            Plugin.SavePluginSettings(TemplatePath & "\" & Plugin.PluginGUID & "\" & foldername & "\PluginSettings.xml")

            Return foldername
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function GetTemplateSettingsFromTemplateGUID(ByVal TemplatePath As String, ByVal Plugin As ICSToolInterface, TemplateGUID As String) As TemplateCollectionSettings
        Try
            Return TemplateHandlerObj.LoadSettings(TemplatePath & "\" & Plugin.PluginGUID & "\" & TemplateGUID & "\TemplateSettings.xml")
        Catch ex As Exception
            Return New TemplateCollectionSettings
        End Try
    End Function

    Public Function GetPluginSettingsFromTemplateGUID(ByVal TemplatePath As String, ByVal Plugin As ICSToolInterface, TemplateGUID As String) As Object
        Try
            Plugin.LoadPluginSettings(TemplatePath & "\" & Plugin.PluginGUID & "\" & TemplateGUID & "\PluginSettings.xml")
            Return Plugin.UserSettingsClass
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function DuplicateTemplate(ByVal TemplatePath As String, ByVal Plugin As ICSToolInterface, ByVal TargetTemplateGUID As String, ByVal NewTemplateSettingsObj As TemplateCollectionSettings) As Boolean
        Try
            Dim foldername As String
            foldername = Guid.NewGuid.ToString
            If Not IO.Directory.Exists(TemplatePath & "\" & Plugin.PluginGUID & "\" & foldername) Then
                My.Computer.FileSystem.CopyDirectory(TemplatePath & "\" & Plugin.PluginGUID & "\" & TargetTemplateGUID, TemplatePath & "\" & Plugin.PluginGUID & "\" & foldername)
                NewTemplateSettingsObj.TemplateGUID = foldername
                TemplateHandlerObj.SaveSettings(NewTemplateSettingsObj, TemplatePath & "\" & Plugin.PluginGUID & "\" & foldername & "\TemplateSettings.xml")
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeleteTemplate(ByVal TemplatePath As String, ByVal Plugin As ICSToolInterface, ByVal TemplateGUID As String) As Boolean
        Try
            My.Computer.FileSystem.DeleteDirectory(TemplatePath & "\" & Plugin.PluginGUID & "\" & TemplateGUID, FileIO.DeleteDirectoryOption.DeleteAllContents)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
