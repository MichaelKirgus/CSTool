'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolCryptHelper
Imports CSToolPluginLib
Imports WeifenLuo.WinFormsUI.Docking

Public Class EnvironmentManager
    Public Function SetEnvironmentVarsInPlugins(ByVal TargetPluginCollection As List(Of CSToolPluginLib.ICSToolInterface), ByVal DockingContentColl As DockContentCollection, Optional ByVal HostOrIP As String = "") As Boolean
        Dim CurrEnvVars As List(Of KeyValuePair(Of String, String))
        CurrEnvVars = GetEnvironmentVarsFromPlugins(TargetPluginCollection, HostOrIP)

        For index = 0 To DockingContentColl.Count - 1
            Try
                Dim HostingWindowObj As DockContent
                HostingWindowObj = DockingContentColl(index)
                If Not HostingWindowObj.IsDisposed Then
                    Dim PluginInterfaceObj As CSToolPluginLib.ICSToolInterface
                    PluginInterfaceObj = HostingWindowObj.Tag

                    If Not IsNothing(PluginInterfaceObj) Then
                        If PluginInterfaceObj.NeedsEnvironmentVariables Then
                            PluginInterfaceObj.EnvironmentRuntimeVariables = CurrEnvVars
                        End If
                    End If
                End If
            Catch ex As Exception
            End Try
        Next

        Return True
    End Function

    Public Function GetEnvironmentVarsFromPlugins(ByVal TargetPluginCollection As List(Of CSToolPluginLib.ICSToolInterface), Optional ByVal HostOrIP As String = "") As List(Of KeyValuePair(Of String, String))
        Try
            Dim NewEnvList As New List(Of KeyValuePair(Of String, String))
            Dim CredHandler As New CredentialHandler

            'Get from environment plugins
            For index = 0 To TargetPluginCollection.Count - 1
                If TargetPluginCollection(index).PluginType = CSToolPluginLib.ICSToolInterface.PluginTypeEnum.EnvironmentManager Then
                    If Not HostOrIP = "" Then
                        TargetPluginCollection(index).RaiseActions(HostOrIP)
                    End If
                    Dim PluginEnvObj As CSToolPluginLib.EnvironmentProvider
                    PluginEnvObj = TargetPluginCollection(index).EnvironmentProviderClass
                    For index2 = 0 To PluginEnvObj.EnvironmentVariables.Count - 1
                        If PluginEnvObj.EnvironmentVariables(index2).Enabled Then
                            If PluginEnvObj.EnvironmentVariables(index2).IsSystemEnvironmentData Then
                                NewEnvList.Add(New KeyValuePair(Of String, String)("%" & PluginEnvObj.EnvironmentVariables(index2).ValueName & "%", Environment.ExpandEnvironmentVariables(PluginEnvObj.EnvironmentVariables(index2).ValueData)))
                            Else
                                NewEnvList.Add(New KeyValuePair(Of String, String)("%" & PluginEnvObj.EnvironmentVariables(index2).ValueName & "%", PluginEnvObj.EnvironmentVariables(index2).ValueData))
                            End If
                        End If
                    Next
                End If
                If TargetPluginCollection(index).PluginType = CSToolPluginLib.ICSToolInterface.PluginTypeEnum.CredentialManager Then
                    Dim PluginCredObj As List(Of CSToolPluginLib.CredentialEntry)
                    PluginCredObj = TargetPluginCollection(index).GetCredential

                    If Not PluginCredObj.Count = 0 Then
                        For credind = 0 To PluginCredObj.Count - 1
                            NewEnvList.Add(New KeyValuePair(Of String, String)("%" & PluginCredObj(credind).CredentialKey & PluginCredObj(credind).UsernameVarKey & "%", CredHandler.ConvertSecureStringInString(PluginCredObj(credind).Username)))
                            NewEnvList.Add(New KeyValuePair(Of String, String)("%" & PluginCredObj(credind).CredentialKey & PluginCredObj(credind).PasswordVarKey & "%", CredHandler.ConvertSecureStringInString(PluginCredObj(credind).Password)))
                        Next
                    End If
                End If
            Next

            Return NewEnvList
        Catch ex As Exception
            Return New List(Of KeyValuePair(Of String, String))
        End Try
    End Function

    Public Function ResolveEnvironmentVariables(ByVal TargetEnvVars As List(Of KeyValuePair(Of String, String)), ByVal OriginalString As String, Optional ByVal OptionalEnvVars As List(Of KeyValuePair(Of String, String)) = Nothing) As String
        Try
            For index = 0 To TargetEnvVars.Count - 1
                Dim KeyP As KeyValuePair(Of String, String)
                KeyP = TargetEnvVars(index)
                If OriginalString.Contains(KeyP.Key) Then
                    OriginalString = OriginalString.Replace(KeyP.Key, KeyP.Value)
                End If
            Next

            If Not IsNothing(OptionalEnvVars) Then
                If Not OptionalEnvVars.Count = 0 Then
                    For index = 0 To OptionalEnvVars.Count - 1
                        Dim KeyP As KeyValuePair(Of String, String)
                        KeyP = OptionalEnvVars(index)
                        If OriginalString.Contains(KeyP.Key) Then
                            OriginalString = OriginalString.Replace(KeyP.Key, KeyP.Value)
                        End If
                    Next
                End If
            End If

            Return OriginalString
        Catch ex As Exception
            Return OriginalString
        End Try
    End Function

    Public Function GetEnvironmentObjectFromAllEnvironmentVariables(ByVal TargetEnvVars As List(Of KeyValuePair(Of String, String))) As EnvironmentProvider
        Try
            Dim NewObj As New EnvironmentProvider
            NewObj.IsProviderReadOnly = True

            For index = 0 To TargetEnvVars.Count - 1
                Dim NewEntry As New EnvironmentEntry

                Dim KeyP As KeyValuePair(Of String, String)
                KeyP = TargetEnvVars(index)

                NewEntry.ValueName = KeyP.Key
                NewEntry.ValueData = KeyP.Value
                NewObj.EnvironmentVariables.Add(NewEntry)
            Next

            Return NewObj
        Catch ex As Exception
            Return New EnvironmentProvider
        End Try
    End Function
End Class
