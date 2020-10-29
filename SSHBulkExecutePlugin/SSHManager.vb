'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Public Class SSHManager
    Public WithEvents SSHClientSession As Renci.SshNet.SshClient = Nothing
    Public LastError As Exception
    Public CurrentAsyncResult As IAsyncResult = Nothing
    Public OutputConsoleResults As New List(Of String)
    Public OutputExitCodeResults As New List(Of Integer)
    Public Sub Init(ByVal HostInfo As SSHHostItem)
        If HostInfo.EtablishConnectionAtStart Then
            SSHClientSession = New Renci.SshNet.SshClient(CreateConnectionObject(HostInfo))
            SSHClientSession.Connect()
        End If
    End Sub
    Public Function ConnectToServer() As Boolean
        If Not SSHClientSession.IsConnected Then
            SSHClientSession.Connect()
        End If

        Return SSHClientSession.IsConnected
    End Function

    Public Function CloseConnection(ByVal Force As Boolean) As Boolean
        If SSHClientSession.IsConnected Then
            If Not IsNothing(CurrentAsyncResult) Then
                If CurrentAsyncResult.IsCompleted = False Then
                    If Force = False Then
                        Return False
                    End If
                End If
            End If

            SSHClientSession.Disconnect()
        End If

        Return True
    End Function

    Public Function CreateConnectionObject(ByVal HostInfo As SSHHostItem) As Renci.SshNet.ConnectionInfo
        Try
            Dim SSHConnObj As Renci.SshNet.ConnectionInfo

            If HostInfo.SSHLoginPassword = "" Then
                'Authentification via key file or plain key
                If HostInfo.SSHLoginPrivateKeyFile = "" Then
                    'Login via plain key
                    Dim privkeyfile As New Renci.SshNet.PrivateKeyFile(New System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(HostInfo.SSHLoginPrivateKeyPlain)))
                    SSHConnObj = New Renci.SshNet.ConnectionInfo(HostInfo.Hostname, HostInfo.SSHLoginUsername, New Renci.SshNet.PrivateKeyAuthenticationMethod(HostInfo.SSHLoginUsername, privkeyfile))
                Else
                    'Login via (encrypted) key file
                    Dim privkeyfile As New Renci.SshNet.PrivateKeyFile(IO.File.OpenRead(HostInfo.SSHLoginPrivateKeyFile), HostInfo.SSHLoginPrivateKeyFilePassword)
                    SSHConnObj = New Renci.SshNet.ConnectionInfo(HostInfo.Hostname, HostInfo.SSHLoginUsername, New Renci.SshNet.PrivateKeyAuthenticationMethod(HostInfo.SSHLoginUsername, privkeyfile))
                End If
            Else
                SSHConnObj = New Renci.SshNet.ConnectionInfo(HostInfo.Hostname, HostInfo.SSHLoginUsername, New Renci.SshNet.PasswordAuthenticationMethod(HostInfo.SSHLoginUsername, HostInfo.SSHLoginPassword))
            End If

            SSHConnObj.Timeout = New TimeSpan(0, 0, HostInfo.ConnectionTimeout)

            Return SSHConnObj
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ExecuteCommand(ByVal Command As SSHExecuteItem, ByVal WaitForExit As Boolean) As Boolean
        Try
            Dim execobj As Renci.SshNet.SshCommand

            If Not SSHClientSession.IsConnected Then
                SSHClientSession.Connect()
            End If

            execobj = SSHClientSession.CreateCommand(Command.Command)

            execobj.CommandTimeout = New TimeSpan(0, 0, Command.Timeout)

            If WaitForExit Then
                execobj.Execute()
                OutputConsoleResults.Add(execobj.Result)
                OutputExitCodeResults.Add(execobj.ExitStatus)
            Else
                CurrentAsyncResult = execobj.BeginExecute
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetLastConsoleOutput() As String
        If Not OutputConsoleResults.Count = 0 Then
            Return OutputConsoleResults(OutputConsoleResults.Count - 1)
        Else
            Return ""
        End If
    End Function

    Public Function GetLastConsoleExitCode() As String
        If Not OutputExitCodeResults.Count = 0 Then
            Return OutputExitCodeResults(OutputExitCodeResults.Count - 1)
        Else
            Return ""
        End If
    End Function

    Public Sub ClearOutputs()
        OutputConsoleResults.Clear()
        OutputExitCodeResults.Clear()
    End Sub

    Public Sub ConnectionSessionError(ByVal sender As Object, ByVal e As Renci.SshNet.Common.ExceptionEventArgs) Handles SSHClientSession.ErrorOccurred
        LastError = e.Exception
    End Sub
End Class
