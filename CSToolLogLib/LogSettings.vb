'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Serializable> Public Class LogSettings
    Public Property GlobalLogLevel As LogEntryLevelEnum = LogEntryLevelEnum.Debug
    Public Property LogToFile As Boolean = False
    Public Property LogToFilePath As String = "CSTool_Log.log"
    Public Property LogToFileBufferSize As Integer = 1024
    Public Property LogToFileLevel As LogEntryLevelEnum = LogEntryLevelEnum.Essential
    Public Property LogToEventLog As Boolean = False
    Public Property LogToEventLogLevel As LogEntryLevelEnum = LogEntryLevelEnum.Essential
    Public Property EventLogSource As String = "CSTool"
    Public Property EventLogName As String = "CSTool application log"
    Public Property LogToDebugConsole As Boolean = False
    Public Property LogToStandardOutputStream As Boolean = False

    Public Enum LogEntryTypeEnum
        Info = 0
        Warning = 1
        ErrorL = 2
    End Enum

    Public Enum LogEntryLevelEnum
        Essential = 0
        Advanced = 1
        Debug = 2
    End Enum
End Class
