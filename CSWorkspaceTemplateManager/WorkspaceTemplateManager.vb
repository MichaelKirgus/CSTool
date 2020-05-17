'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolUserSettingsLib
Imports CSToolUserSettingsManager

Public Class WorkspaceTemplateManager
    Public WorkspaceSettingsFilename As String = "UserSettings.xml"
    Public WorkspaceTemplateImageFilename As String = "Thumbnail.png"

    Public Function GetTemplates(ByVal WorkspaceTemplatePath As String) As List(Of WorkspaceTemplateClass)
        Try
            Dim templatecoll As New List(Of WorkspaceTemplateClass)

            If IO.Directory.Exists(WorkspaceTemplatePath) Then
                Dim dircoll As String()
                dircoll = IO.Directory.GetDirectories(WorkspaceTemplatePath)
                If Not dircoll.Count = 0 Then
                    For index = 0 To dircoll.Count - 1
                        If IO.File.Exists(dircoll(index) & "\" & WorkspaceSettingsFilename) Then
                            Dim newtemp As New WorkspaceTemplateClass
                            newtemp.TemplateUserSettingsPath = dircoll(index) & "\" & WorkspaceSettingsFilename

                            Dim usersettingshandler As New UserSettingsManager
                            Dim workspaceclass As UserSettings
                            workspaceclass = usersettingshandler.LoadSettings(dircoll(index) & "\" & WorkspaceSettingsFilename)

                            newtemp.TemplateName = workspaceclass.TemplateName
                            newtemp.TemplateDescription = workspaceclass.TemplateDescription

                            If IO.File.Exists(dircoll(index) & "\" & WorkspaceTemplateImageFilename) Then
                                newtemp.TemplateImagePath = dircoll(index) & "\" & WorkspaceTemplateImageFilename
                            End If

                            templatecoll.Add(newtemp)
                        End If
                    Next
                End If
            End If

            Return templatecoll
        Catch ex As Exception
            Return New List(Of WorkspaceTemplateClass)
        End Try
    End Function
End Class
