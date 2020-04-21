'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Text
Imports CSToolLogLib.LogSettings

Public Class LogLib
    Public LogCollection As New List(Of LogEntry)
    Public LastLogAdvancedMessage As String = ""
    Public LastLogDebugMessage As String = ""
    Public LastLogEssentialMessage As String = ""
    Public LogFileWriterHandler As IO.StreamWriter = Nothing
    Public LogFileStreamHandler As IO.FileStream = Nothing
    Public LogEventLogHandler As EventLog = Nothing

    Public Property LogSettings As New LogSettings

    Public Sub WriteToFile(ByVal LogEntryObj As LogEntry)
        Try
            LogFileWriterHandler.WriteLine(LogEntryObj.DateAndTime.ToLocalTime.ToString() & vbTab &
                                                         LogEntryObj.Level & vbTab & LogEntryObj.Type & vbTab &
                                                         LogEntryObj.TargetHandler.ToString & vbTab &
                                                         LogEntryObj.Message)

        Catch ex As Exception
        End Try
    End Sub

    Public Sub PostLogEntry(ByVal LogEntryObj As LogEntry)
        Try
            If LogSettings.LogToFile Then
                Select Case LogSettings.LogToFileLevel
                    Case LogEntryLevelEnum.Essential
                        If LogEntryObj.Level = LogEntryLevelEnum.Essential Then
                            WriteToFile(LogEntryObj)
                        End If
                    Case LogEntryLevelEnum.Advanced
                        If LogEntryObj.Level = LogEntryLevelEnum.Essential Or LogEntryObj.Level = LogEntryLevelEnum.Advanced Then
                            WriteToFile(LogEntryObj)
                        End If
                    Case LogEntryLevelEnum.Debug
                        WriteToFile(LogEntryObj)
                End Select
            End If
            If LogSettings.LogToEventLog Then
                Select Case LogSettings.LogToEventLogLevel
                    Case LogEntryLevelEnum.Essential
                        If LogEntryObj.Level = LogEntryLevelEnum.Essential Then
                            LogEventLogHandler.WriteEntry(LogEntryObj.Message)
                        End If
                    Case LogEntryLevelEnum.Advanced
                        If LogEntryObj.Level = LogEntryLevelEnum.Essential Or LogEntryObj.Level = LogEntryLevelEnum.Advanced Then
                            LogEventLogHandler.WriteEntry(LogEntryObj.Message)
                        End If
                    Case LogEntryLevelEnum.Debug
                        LogEventLogHandler.WriteEntry(LogEntryObj.Message)
                End Select
            End If
            If LogSettings.LogToStandardOutputStream Then
                Console.WriteLine(LogEntryObj.Message)
            End If
            If LogSettings.LogToDebugConsole Then
                Debug.WriteLine(LogEntryObj.Message)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub InitLogSystem()
        Try
            If LogSettings.LogToFile Then
                If IsNothing(LogFileStreamHandler) Then
                    InitFileStream()
                Else
                    CloseStreams()
                    InitFileStream()
                End If
            End If
            If LogSettings.LogToEventLog Then
                If Not EventLog.SourceExists(LogSettings.EventLogSource) Then
                    EventLog.CreateEventSource(LogSettings.EventLogSource, LogSettings.EventLogName)
                    LogEventLogHandler = New EventLog(LogSettings.EventLogName)
                    LogEventLogHandler.Source = LogSettings.EventLogSource
                    LogEventLogHandler.Log = LogSettings.EventLogName
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ResetLogStore()
        LogCollection.Clear()
        ReInitLogSystem()
    End Sub

    Public Sub ReInitLogSystem()
        CloseStreams()
        InitLogSystem()
    End Sub


    Public Sub CloseStreams()
        Try
            If Not IsNothing(LogFileWriterHandler) And LogSettings.LogToFile Then
                Try
                    LogFileWriterHandler.Flush()
                    LogFileStreamHandler.Flush()
                Catch ex As Exception
                End Try
                LogFileWriterHandler.Close()
                LogFileStreamHandler.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub InitFileStream()
        Try
            LogFileStreamHandler = IO.File.Open(LogSettings.LogToFilePath, IO.FileMode.OpenOrCreate, IO.FileAccess.Write, IO.FileShare.Read)
            LogFileWriterHandler = New IO.StreamWriter(LogFileStreamHandler)
            LogFileWriterHandler.AutoFlush = True
        Catch ex As Exception
        End Try
    End Sub

    Public Function WriteLogEntry(ByVal Message As String, ByVal Handler As Type, Optional ByVal LogEntryType As LogEntryTypeEnum = LogEntryTypeEnum.Info, Optional ByVal LogEntryLevel As LogEntryLevelEnum = LogEntryLevelEnum.Essential, Optional ErrorObj As ErrObject = Nothing) As Boolean
        Try
            If LogSettings.GlobalLogLevel >= LogEntryLevel Then
                Dim qq As New LogEntry
                qq.DateAndTime = DateAndTime.Now
                qq.Type = LogEntryType
                qq.Level = LogEntryLevel
                qq.TargetHandler = Handler.ToString
                qq.Message = Message

                If Not IsNothing(ErrorObj) Then
                    qq.ErrorObj = ErrorObj
                    qq.Message += vbTab & ErrorObj.Description
                    Try
                        If Not IsNothing(ErrorObj.GetException.Data) Then
                            For index = 0 To ErrorObj.GetException.Data.Values.Count - 1
                                qq.Message += vbNewLine & ErrorObj.GetException.Data.Values(index)
                            Next
                        End If
                    Catch ex As Exception
                    End Try
                    Try
                        qq.Message += vbNewLine & "Stack trace:"
                        If Not IsNothing(ErrorObj.GetException.StackTrace) Then
                            qq.Message += vbNewLine & ErrorObj.GetException.StackTrace
                        End If
                    Catch ex As Exception
                    End Try
                End If
                If Not IsNothing(LogCollection) Then
                    LogCollection.Add(qq)
                End If
                If LogSettings.LogToFile Or LogSettings.LogToEventLog Then
                    PostLogEntry(qq)
                End If

                Select Case LogEntryType
                    Case LogEntryLevelEnum.Essential
                        LastLogEssentialMessage = ParseLogEntryToMessageString(LogCollection.Count - 1)
                    Case LogEntryLevelEnum.Advanced
                        LastLogAdvancedMessage = ParseLogEntryToMessageString(LogCollection.Count - 1)
                    Case LogEntryLevelEnum.Debug
                        LastLogDebugMessage = ParseLogEntryToMessageString(LogCollection.Count - 1)
                End Select

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ParseLogEntryToMessageString(ByVal LogIndex As Integer) As String
        Try
            Dim MessageStr As String = ""
            If Not IsNothing(LogCollection) Then
                If Not LogCollection.Count = 0 Then
                    If Not (LogCollection.Count - 1) < LogIndex Then
                        MessageStr = LogCollection(LogIndex).Message
                    End If
                End If
            End If

            Return MessageStr
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function ParseLogEntryToString(ByVal LogIndex As Integer) As String
        Try
            Dim MessageStr As String = ""
            If Not IsNothing(LogCollection) Then
                If Not LogCollection.Count = 0 Then
                    MessageStr = LogCollection(LogIndex).DateAndTime.ToLocalTime.ToString() & vbTab &
                                 LogCollection(LogIndex).Level & vbTab & LogCollection(LogIndex).Type & vbTab &
                                 LogCollection(LogIndex).TargetHandler.ToString & vbTab &
                                 LogCollection(LogIndex).Message
                End If
            End If

            Return MessageStr
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class
