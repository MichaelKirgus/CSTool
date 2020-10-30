'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Text
Imports Renci.SshNet

Public Class SSHManager
    Public WithEvents SSHClientSession As Renci.SshNet.SshClient = Nothing
    Public SSHHostFingerprint As String = ""
    Public LastError As Exception
    Public CurrentAsyncResult As IAsyncResult = Nothing
    Public OutputConsoleResults As New List(Of String)
    Public OutputExitCodeResults As New List(Of Integer)
    Public TempHostInfoObj As SSHHostItem
    Public Sub Init(ByVal HostInfo As SSHHostItem)
        SSHClientSession = New Renci.SshNet.SshClient(CreateConnectionObject(HostInfo))
        SSHClientSession.Connect()
    End Sub
    Public Function ConnectToServer() As Boolean
        If Not IsNothing(SSHClientSession) Then
            If Not SSHClientSession.IsConnected Then
                SSHClientSession.Connect()
            End If

            Return SSHClientSession.IsConnected
        Else
            Return False
        End If
    End Function

    Public Function CloseConnection(ByVal Force As Boolean) As Boolean
        If Not IsNothing(SSHClientSession) Then
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
        End If

        Return True
    End Function

    Public Function CreateConnectionObject(ByVal HostInfo As SSHHostItem) As ConnectionInfo
        Try
            TempHostInfoObj = HostInfo
            Dim SSHConnObj As ConnectionInfo

            If HostInfo.SSHLoginPassword = "" Then
                'Authentification via key file or plain key
                If HostInfo.SSHLoginPrivateKeyFile = "" Then
                    'Login via plain key
                    Dim privkeyfile As New PrivateKeyFile(New System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(HostInfo.SSHLoginPrivateKeyPlain)))
                    SSHConnObj = New ConnectionInfo(HostInfo.Hostname, HostInfo.SSHLoginUsername, New PrivateKeyAuthenticationMethod(HostInfo.SSHLoginUsername, privkeyfile))
                Else
                    'Login via (encrypted) key file
                    Dim privkeyfile As New PrivateKeyFile(IO.File.OpenRead(HostInfo.SSHLoginPrivateKeyFile), HostInfo.SSHLoginPrivateKeyFilePassword)
                    SSHConnObj = New ConnectionInfo(HostInfo.Hostname, HostInfo.SSHLoginUsername, New PrivateKeyAuthenticationMethod(HostInfo.SSHLoginUsername, privkeyfile))
                End If
            Else
                Dim kauth = New KeyboardInteractiveAuthenticationMethod(HostInfo.SSHLoginUsername)
                AddHandler kauth.AuthenticationPrompt, AddressOf HandleKeyEvent
                Dim pauth = New PasswordAuthenticationMethod(HostInfo.SSHLoginUsername, HostInfo.SSHLoginPassword)

                SSHConnObj = New ConnectionInfo(HostInfo.Hostname, HostInfo.SSHLoginUsername, kauth, pauth)
            End If

            SSHConnObj.Timeout = New TimeSpan(0, 0, HostInfo.ConnectionTimeout)

            Select Case HostInfo.SessionEncoding
                Case SSHHostItem.SessionEncodingEnum.UTF8
                    SSHConnObj.Encoding = System.Text.Encoding.UTF8
                Case SSHHostItem.SessionEncodingEnum.UTF32
                    SSHConnObj.Encoding = System.Text.Encoding.UTF32
                Case SSHHostItem.SessionEncodingEnum.UTF7
                    SSHConnObj.Encoding = System.Text.Encoding.UTF7
                Case SSHHostItem.SessionEncodingEnum.ASCII
                    SSHConnObj.Encoding = System.Text.Encoding.ASCII
                Case SSHHostItem.SessionEncodingEnum.BigEndianUnicode
                    SSHConnObj.Encoding = System.Text.Encoding.BigEndianUnicode
                Case SSHHostItem.SessionEncodingEnum.Unicode
                    SSHConnObj.Encoding = System.Text.Encoding.Unicode
            End Select

            Return SSHConnObj
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub HandleKeyEvent(sender As Object, e As Renci.SshNet.Common.AuthenticationPromptEventArgs)
        For Each prompt As Renci.SshNet.Common.AuthenticationPrompt In e.Prompts
            If prompt.Request.StartsWith("Password") Then
                prompt.Response = TempHostInfoObj.SSHLoginPassword
            End If
        Next
    End Sub

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
                OutputConsoleResults.Add(execobj.Result.Replace(vbCrLf, vbNewLine).Replace(vbCr, vbNewLine).Replace(vbLf, vbNewLine))
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

    Public Sub ConnectionSessionError(ByVal sender As Object, ByVal e As Common.ExceptionEventArgs) Handles SSHClientSession.ErrorOccurred
        LastError = e.Exception
    End Sub
    Public Shared Function ConvertFingerprintToByteArray(ByVal fingerprint As String) As Byte()
        Return fingerprint.Split(":"c).[Select](Function(s) Convert.ToByte(s, 16)).ToArray()
    End Function

    Public Function GetReadableFingerprintFromByteArray(ByVal ByteArray As Byte()) As String
        Dim fingerPrint = New StringBuilder(ByteArray.Length * 2)

        For Each b In ByteArray
            fingerPrint.AppendFormat("{0:x2}", b)
        Next

        Return fingerPrint.ToString
    End Function

    Public Sub ConnectionSessionHostKeyReceived(ByVal sender As Object, ByVal e As Common.HostKeyEventArgs) Handles SSHClientSession.HostKeyReceived
        SSHHostFingerprint = GetReadableFingerprintFromByteArray(e.FingerPrint)
    End Sub
End Class
