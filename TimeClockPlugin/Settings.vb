'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Windows.Forms

<Serializable> Public Class Settings
    Public Property WindowTitle As String = ""
    Public Property ShowBreakfast As Boolean = False
    Public Property ShowCustomBreakfast As Boolean = True
    Public Property ShowCustomLunch As Boolean = True
    Public Property ShowCustomEndTime As Boolean = True
    Public Property CalculateCustomEndTimeWithOffsets As Boolean = False
    Public Property UseCustomEndTimeAsNormalWorkimeEnd As Boolean = False
    Public Property CustomEndTime As Date = New Date(2020, 5, 1, 17, 0, 0)
    Public Property CustomBreakfast As Date = New Date(2020, 5, 1, 9, 30, 0)
    Public Property CustomLunch As Date = New Date(2020, 5, 1, 12, 30, 0)
    Public Property UseSystemBootupTime As Boolean = True
    Public Property UseLocalFileTimestampIfOlder As Boolean = True
    Public Property NormalWorktimeSpan As Integer = 450
    Public Property NormalWorktimeMaxSpan As Integer = 360
    Public Property NormalWorktimeLunchSpan As Integer = 30
    Public Property NormalWorktimeBreakfastSpan As Integer = 15
    Public Property StartWorktimeOffset As Integer = 0
    Public Property EndWorktimeOffset As Integer = 0
    Public Property MaxWorktimeSpan As Integer = 600
    Public Property MaxWorktimeAdditionalBreakSpan As Integer = 15
    Public Property RefreshInterval As Integer = 1000
    Public Property WorktimeTag As String = "Worktime1"
    Public Property AlwaysShowTrayIcon As Boolean = False
    Public Property CustomNotifications As New List(Of NotificationEntry)
End Class

<Serializable> Public Class NotificationEntry
    Public WasFired As Boolean = False
    Public Property Enable As Boolean = True
    Public Property FireAtElapsedMinute As Integer = 0
    Public Property NotificationType As ToolTipIcon = ToolTipIcon.Info
    Public Property NotificationTitle As String = ""
    Public Property NotificationText As String = ""
End Class


