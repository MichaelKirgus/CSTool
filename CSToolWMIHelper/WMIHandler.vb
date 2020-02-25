'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Management
Imports CSToolLogLib
Imports CSToolLogLib.LogSettings

Public Class WMIHandler
    Public _LogInstanceHandler As LogLib = Nothing

    Public Function GetDetails(ByVal Hostname As String, ByVal ClassName As String, ByVal Selection As String, Optional ByVal WMITimeout As Integer = 1000,
                               Optional UseCustomWMIConnectionOptions As Boolean = False, Optional CustomWMIConnectionOptionsUsername As String = "",
                               Optional ByVal CustomWMIConnectionOptionsPassword As String = "", Optional ByVal CustomWMIConnectionOptionsAuthority As String = "",
                               Optional ByVal CustomWMIConnectionOptionsAuthentication As AuthenticationLevel = AuthenticationLevel.Packet,
                               Optional ByVal CustomWMIConnectionOptionsImpersonation As ImpersonationLevel = ImpersonationLevel.Default) As String
        Try
            If Not IsNothing(_LogInstanceHandler) Then
                _LogInstanceHandler.WriteLogEntry("WMIQuery: Hostname: " & Hostname & " Classname: " & ClassName & " Selection: " & Selection, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            End If

            Dim tmpstr As String = ""
            Dim islocal As Boolean = False

            Dim scope As ManagementScope
            If Hostname.ToLower = "localhost" Or Hostname = "127.0.0.1" Or Hostname.ToLower = My.Computer.Name.ToLower Then
                scope = New ManagementScope("root\CIMV2")
                islocal = True
            Else
                scope = New ManagementScope("\\" & Hostname & "\root\cimv2")
            End If

            If UseCustomWMIConnectionOptions And Not islocal Then
                Dim myConnectionOptions As New ConnectionOptions
                myConnectionOptions.EnablePrivileges = True
                myConnectionOptions.Username = CustomWMIConnectionOptionsUsername
                myConnectionOptions.Password = CustomWMIConnectionOptionsPassword
                myConnectionOptions.Authority = CustomWMIConnectionOptionsAuthority
                myConnectionOptions.Authentication = CustomWMIConnectionOptionsAuthentication
                myConnectionOptions.Impersonation = CustomWMIConnectionOptionsImpersonation
                scope.Options = myConnectionOptions
            End If

            scope.Connect()

            Dim query As New ObjectQuery("SELECT " & ClassName & " FROM " & Selection)
            Dim searcher As New ManagementObjectSearcher(scope, query)

            searcher.Options.Timeout = New TimeSpan(0, 0, 0, 0, WMITimeout)
            searcher.Options.ReturnImmediately = True

            If ClassName.Contains(",") Then
                Dim ClassColl As Array
                Dim trimstr As String = ClassName.Replace(" ", "")
                ClassColl = trimstr.Split(",")
                For Each queryObj As ManagementObject In searcher.Get()
                    For index = 0 To ClassColl.Length - 1
                        tmpstr += queryObj(ClassColl(index))
                        If Not ClassColl.Length - 1 = index Then
                            tmpstr += ", "
                        End If
                    Next
                Next
            Else
                For Each queryObj As ManagementObject In searcher.Get()
                    tmpstr = queryObj(ClassName)
                Next
            End If

            If Not IsNothing(_LogInstanceHandler) Then
                _LogInstanceHandler.WriteLogEntry("WMIQuery result: " & tmpstr, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            End If

            Return tmpstr
        Catch ex As Exception
            If Not IsNothing(_LogInstanceHandler) Then
                _LogInstanceHandler.WriteLogEntry("WMIQuery result: No data or connection error.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug)
            End If
            Return ""
        End Try
    End Function

    Public Function ExecWMIMethod(ByVal Hostname As String, ByVal WMIExecScope As String, ByVal WMIExecQuery As String, ByVal WMIMethod As String, ByVal WMIArgumentValue As String, ByVal WMIArgumentData As String,
                                  Optional ByVal UseCustomWMIConnectionOptions As Boolean = False, Optional ByVal CustomWMIConnectionOptionsUsername As String = "",
                                  Optional ByVal CustomWMIConnectionOptionsPassword As String = "", Optional ByVal CustomWMIConnectionOptionsAuthority As String = "",
                                  Optional ByVal CustomWMIConnectionOptionsAuthentication As AuthenticationLevel = AuthenticationLevel.Packet,
                                  Optional ByVal CustomWMIConnectionOptionsImpersonation As ImpersonationLevel = ImpersonationLevel.Default) As Boolean
        Try
            If Not IsNothing(_LogInstanceHandler) Then
                _LogInstanceHandler.WriteLogEntry("WMIExec: Hostname: " & Hostname & " WMIExecScope: " & WMIExecScope & " WMIExecQuery: " & WMIExecQuery & " WMIMethod: " & WMIMethod & " WMIArgumentValue: " & WMIArgumentValue & " WMIArgumentData: " & WMIArgumentData, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            End If
            Dim islocal As Boolean = False
            Dim classInstance As ManagementClass = Nothing
            If Hostname.ToLower = "localhost" Or Hostname = "127.0.0.1" Then
                classInstance = New ManagementClass("root\CIMV2", WMIExecQuery, Nothing)
                islocal = True
            Else
                classInstance = New ManagementClass(WMIExecScope, WMIExecQuery, Nothing)
            End If

            If UseCustomWMIConnectionOptions And Not islocal Then
                Dim myConnectionOptions As New ConnectionOptions
                myConnectionOptions.EnablePrivileges = True
                myConnectionOptions.Username = CustomWMIConnectionOptionsUsername
                myConnectionOptions.Password = CustomWMIConnectionOptionsPassword
                myConnectionOptions.Authority = CustomWMIConnectionOptionsAuthority
                myConnectionOptions.Authentication = CustomWMIConnectionOptionsAuthentication
                myConnectionOptions.Impersonation = CustomWMIConnectionOptionsImpersonation
                classInstance.Scope.Options = myConnectionOptions
            End If

            ' Obtain [in] parameters for the method
            Dim inParams As ManagementBaseObject = classInstance.GetMethodParameters(WMIMethod)
            Debug.WriteLine(inParams)

            If Not WMIArgumentValue = "" Then
                inParams(WMIArgumentValue) = WMIArgumentData
            End If

            ' Execute the method and obtain the return values.
            Dim outParams As ManagementBaseObject = classInstance.InvokeMethod(WMIMethod, inParams, Nothing)
            If Not IsNothing(_LogInstanceHandler) Then
                _LogInstanceHandler.WriteLogEntry("WMIExec: Command invoked.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            End If

            Return True
        Catch err As ManagementException
            If Not IsNothing(_LogInstanceHandler) Then
                _LogInstanceHandler.WriteLogEntry("WMIExec: Error run WMI command.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug)
            End If
            Return False
        End Try
    End Function

    Public Function CheckIfDriveIsSSD(ByVal Hostname As String, ByVal WMIExecScope As String, WMIExecQuery As String, ByVal WinSATValue As Integer, Optional ByVal WMITimeout As Integer = 1000,
                                      Optional UseCustomWMIConnectionOptions As Boolean = False, Optional CustomWMIConnectionOptionsUsername As String = "",
                                      Optional ByVal CustomWMIConnectionOptionsPassword As String = "", Optional ByVal CustomWMIConnectionOptionsAuthority As String = "",
                                      Optional ByVal CustomWMIConnectionOptionsAuthentication As AuthenticationLevel = AuthenticationLevel.Packet,
                                      Optional ByVal CustomWMIConnectionOptionsImpersonation As ImpersonationLevel = ImpersonationLevel.Default) As Boolean
        'Diese Funktion überprüft, ob der Windows-Leistungsindex der Festplatte höher als 5,9 ist.
        'Wenn ja, handelt es sich um eine SSD.

        Try
            If Not IsNothing(_LogInstanceHandler) Then
                _LogInstanceHandler.WriteLogEntry("WMIExec: Hostname: " & Hostname & " WMIExecScope: " & WMIExecScope & " WMIExecQuery: " & WMIExecQuery, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            End If
            Dim islocal As Boolean = False
            Dim scope As ManagementScope
            If Hostname.ToLower = "localhost" Or Hostname = "127.0.0.1" Or Hostname.ToLower = My.Computer.Name.ToLower Then
                scope = New ManagementScope("root\CIMV2")
                islocal = True
            Else
                scope = New ManagementScope(WMIExecScope)
            End If

            If UseCustomWMIConnectionOptions And Not islocal Then
                Dim myConnectionOptions As New ConnectionOptions
                myConnectionOptions.EnablePrivileges = True
                myConnectionOptions.Username = CustomWMIConnectionOptionsUsername
                myConnectionOptions.Password = CustomWMIConnectionOptionsPassword
                myConnectionOptions.Authority = CustomWMIConnectionOptionsAuthority
                myConnectionOptions.Authentication = CustomWMIConnectionOptionsAuthentication
                myConnectionOptions.Impersonation = CustomWMIConnectionOptionsImpersonation
                scope.Options = myConnectionOptions
            End If

            scope.Connect()
            Dim query As New ObjectQuery(WMIExecQuery)
            Dim searcher As New ManagementObjectSearcher(scope, query)
            searcher.Options.Timeout = New TimeSpan(0, 0, 0, 0, WMITimeout)
            searcher.Options.ReturnImmediately = True

            Dim result As Decimal
            For Each queryObj As ManagementObject In searcher.Get()
                result = queryObj("DiskScore")
            Next

            If result > WinSATValue Then
                If Not IsNothing(_LogInstanceHandler) Then
                    _LogInstanceHandler.WriteLogEntry("SSD detected.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                End If
                Return True
            Else
                If Not IsNothing(_LogInstanceHandler) Then
                    _LogInstanceHandler.WriteLogEntry("HDD detected.", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                End If
                Return False
            End If

            If Not IsNothing(_LogInstanceHandler) Then
                _LogInstanceHandler.WriteLogEntry("No data.", Me.GetType, LogEntryTypeEnum.Warning, LogEntryLevelEnum.Debug)
            End If
            Return False
        Catch ex As Exception
            If Not IsNothing(_LogInstanceHandler) Then
                _LogInstanceHandler.WriteLogEntry("WMI: Error", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug)
            End If
            Return False
        End Try
    End Function

    Public Function CheckIfUserIsAdmin(ByVal Hostname As String, ByVal WMIExecScope As String, WMIExecQuery As String, ByVal Username As String, Optional ByVal WMITimeout As Integer = 1000,
                                       Optional UseCustomWMIConnectionOptions As Boolean = False, Optional CustomWMIConnectionOptionsUsername As String = "",
                                       Optional ByVal CustomWMIConnectionOptionsPassword As String = "", Optional ByVal CustomWMIConnectionOptionsAuthority As String = "",
                                       Optional ByVal CustomWMIConnectionOptionsAuthentication As AuthenticationLevel = AuthenticationLevel.Packet,
                                       Optional ByVal CustomWMIConnectionOptionsImpersonation As ImpersonationLevel = ImpersonationLevel.Default) As Boolean
        Try
            Try
                If Not IsNothing(_LogInstanceHandler) Then
                    _LogInstanceHandler.WriteLogEntry("WMIExec: Hostname: " & Hostname & " WMIExecScope: " & WMIExecScope & " WMIExecQuery: " & WMIExecQuery, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                End If
                Dim islocal As Boolean = False
                Dim scope As ManagementScope
                If Hostname.ToLower = "localhost" Or Hostname = "127.0.0.1" Or Hostname.ToLower = My.Computer.Name.ToLower Then
                    scope = New ManagementScope("root\CIMV2")
                    islocal = True
                Else
                    scope = New ManagementScope(WMIExecScope)
                End If

                If UseCustomWMIConnectionOptions And Not islocal Then
                    Dim myConnectionOptions As New ConnectionOptions
                    myConnectionOptions.EnablePrivileges = True
                    myConnectionOptions.Username = CustomWMIConnectionOptionsUsername
                    myConnectionOptions.Password = CustomWMIConnectionOptionsPassword
                    myConnectionOptions.Authority = CustomWMIConnectionOptionsAuthority
                    myConnectionOptions.Authentication = CustomWMIConnectionOptionsAuthentication
                    myConnectionOptions.Impersonation = CustomWMIConnectionOptionsImpersonation
                    scope.Options = myConnectionOptions
                End If

                scope.Connect()
                Dim query As New ObjectQuery(WMIExecQuery)
                Dim searcher As New ManagementObjectSearcher(scope, query)

                searcher.Options.Timeout = New TimeSpan(0, 0, 0, 0, WMITimeout)
                searcher.Options.ReturnImmediately = True

                Dim result As New List(Of String)
                For Each queryObj As ManagementObject In searcher.Get()
                    result.Add(queryObj("GroupComponent") & "," & queryObj("PartComponent"))
                Next

                Dim splituser As String
                splituser = Username.Split("\")(1)

                For index = 0 To result.Count - 1
                    If result(index).Contains(splituser) Then
                        If Not IsNothing(_LogInstanceHandler) Then
                            _LogInstanceHandler.WriteLogEntry("WMIExec: CheckIfUserIsAdmin: Current user is administrator", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
                        End If
                        Return True
                    End If
                Next
            Catch err As ManagementException
                If Not IsNothing(_LogInstanceHandler) Then
                    _LogInstanceHandler.WriteLogEntry("WMIExec: Connection error.", Me.GetType, LogEntryTypeEnum.ErrorL, LogEntryLevelEnum.Debug)
                End If
            End Try

            If Not IsNothing(_LogInstanceHandler) Then
                _LogInstanceHandler.WriteLogEntry("WMIExec: CheckIfUserIsAdmin: Current user is NOT administrator", Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ConvertWMIDateToDateTime(ByVal WMIDateStr As String) As Date
        Try
            Return Management.ManagementDateTimeConverter.ToDateTime(WMIDateStr)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConvertWMIDateToLocalDateTime(ByVal WMIDateStr As String) As Date
        Try
            Return Management.ManagementDateTimeConverter.ToDateTime(WMIDateStr).ToLocalTime
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetRightSizeFormat(ByVal size_bytes As Int64, Optional ByVal DecimalPlaces As Integer = 2) As String
        'Diese Funktion generiert aus einer Anzahl von Bytes eine lesbare Größenangabe in Bytes/KB/MB/GB/TB
        Try
            Dim newstr As String = ""
            Dim newdec As Double = 0
            Dim by As Decimal = size_bytes
            If by > 1024 Then
                Dim kb As Double = by / 1024
                If kb > 1024 Then
                    Dim mb As Double = kb / 1024
                    If mb > 1024 Then
                        Dim gb As Double = mb / 1024
                        If gb > 1024 Then
                            Dim tb As Double = gb / 1024
                            newdec = Math.Round(tb, DecimalPlaces)
                            newstr += newdec & " TB"
                        Else
                            newdec = Math.Round(gb, DecimalPlaces)
                            newstr += Math.Round(gb, DecimalPlaces).ToString & " GB"
                        End If
                    Else
                        newdec = Math.Round(mb, DecimalPlaces)
                        newstr += Math.Round(mb, DecimalPlaces).ToString & " MB"
                    End If
                Else
                    newdec = Math.Round(kb, DecimalPlaces)
                    newstr += Math.Round(kb, DecimalPlaces).ToString & " KB"
                End If
            Else
                newdec = Math.Round(by, DecimalPlaces)
                newstr += Math.Round(by, DecimalPlaces).ToString & " Bytes"
            End If

            Return newstr
        Catch ex As Exception
            Return "0 KB"
        End Try
    End Function
End Class
