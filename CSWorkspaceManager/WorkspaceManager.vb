Imports CSToolUserSettingsLib
Imports CSToolUserSettingsManager

Public Class WorkspaceManager
    Dim UserSettingsHandler As New UserSettingsManager

    Public Function GetAllWorkspaces(ByVal UserProfilePath As String) As List(Of UserSettings)
        Try
            Dim workspacescoll As New List(Of UserSettings)
            workspacescoll.Add(UserSettingsHandler.LoadSettings(UserProfilePath & "\UserSettings.xml"))

            Dim folderinf As New IO.DirectoryInfo(UserProfilePath)
            Dim foldercoll As IEnumerable(Of IO.DirectoryInfo)
            foldercoll = folderinf.EnumerateDirectories
            If Not foldercoll.Count = 0 Then
                For index = 0 To foldercoll.Count - 1
                    If Not foldercoll(index).Name = "Default" Then
                        If IO.File.Exists(UserProfilePath & "\" & foldercoll(index).Name & "\UserSettings.xml") Then
                            workspacescoll.Add(UserSettingsHandler.LoadSettings(UserProfilePath & "\" & foldercoll(index).Name & "\UserSettings.xml"))
                        End If
                    End If
                Next
            End If

            Return workspacescoll
        Catch ex As Exception
            Return New List(Of UserSettings)
        End Try
    End Function

    Public Function SaveAllWorkspaces(ByVal UserProfilePath As String, ByVal NewUserSettings As List(Of UserSettings)) As Boolean
        Try
            If Not NewUserSettings.Count = 0 Then
                For index = 0 To NewUserSettings.Count - 1
                    If Not NewUserSettings(index).SettingName = "Default" Then

                    End If
                Next
            End If



            Dim folderinf As New IO.DirectoryInfo(UserProfilePath)
            Dim foldercoll As IEnumerable(Of IO.DirectoryInfo)
            foldercoll = folderinf.EnumerateDirectories
            If Not foldercoll.Count = 0 Then
                For index = 0 To foldercoll.Count - 1
                    If Not foldercoll(index).Name = "Default" Then
                        If IO.File.Exists(UserProfilePath & "\" & foldercoll(index).Name & "\UserSettings.xml") Then
                            workspacescoll.Add(UserSettingsHandler.LoadSettings(UserProfilePath & "\" & foldercoll(index).Name & "\UserSettings.xml"))
                        End If
                    End If
                Next
            End If

            Return workspacescoll
        Catch ex As Exception
            Return New List(Of UserSettings)
        End Try
    End Function
End Class
