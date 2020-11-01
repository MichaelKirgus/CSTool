Imports System.IO
Imports CSToolUserSettingsLib
Imports CSToolUserSettingsManager

Public Class WorkspaceHandler
    Public UserSettingsHandler As New UserSettingsManager

    Public Function GetAllWorkspaces(ByVal BasePath As String) As List(Of UserSettings)
        Try
            Dim returnlist As New List(Of UserSettings)
            Dim dirinf As New IO.DirectoryInfo(BasePath)
            Dim dircoll As DirectoryInfo()
            dircoll = dirinf.GetDirectories

            Dim defaultsetting As UserSettings = Nothing

            If Not dircoll.Count = 0 Then
                For index = 0 To dircoll.Count - 1
                    Dim workspaceobj As UserSettings
                    workspaceobj = UserSettingsHandler.LoadSettings(dircoll(index).FullName & "\" & UserSettingsHandler.UserSettingsFile)

                    If workspaceobj.SettingName = "Default" Then
                        defaultsetting = workspaceobj
                    Else
                        returnlist.Add(workspaceobj)
                    End If
                Next

                returnlist.Insert(0, defaultsetting)
            Else
                Return New List(Of UserSettings)
            End If

            Return returnlist
        Catch ex As Exception
            Return New List(Of UserSettings)
        End Try
    End Function

    Public Function GetWorkspaceFromGUID(ByVal SettingsCollection As List(Of UserSettings), ByVal GUID As String) As UserSettings
        Try
            If Not SettingsCollection.Count = 0 Then
                For index = 0 To SettingsCollection.Count - 1
                    If SettingsCollection(index).SettingName = GUID Then
                        Return SettingsCollection(index)
                    End If
                Next
            End If

            Return New UserSettings
        Catch ex As Exception
            Return New UserSettings
        End Try
    End Function

    Public Function GetWorkspaceFromTemplateName(ByVal SettingsCollection As List(Of UserSettings), ByVal TemplateName As String) As UserSettings
        Try
            If Not SettingsCollection.Count = 0 Then
                For index = 0 To SettingsCollection.Count - 1
                    If SettingsCollection(index).TemplateName = TemplateName Then
                        Return SettingsCollection(index)
                    End If
                Next
            End If

            Return New UserSettings
        Catch ex As Exception
            Return New UserSettings
        End Try
    End Function
End Class
