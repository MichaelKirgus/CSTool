'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Windows.Forms

<Serializable> Public Class Settings
    Public Property InitialTitle As String = ""
    Public Property RaiseActions As Boolean = True
    Public Property ConnectAtLoad As Boolean = False
    Public Property SQLConnectionString As String = "Data Source=TESTSQL;Initial Catalog=TESTDB;Integrated Security=True"
    Public Property TestForSQLServer As Boolean = True
    Public Property TestForSQLServerHostname As String = "TESTSQL"
    Public Property TestForSQLServerPort As Integer = 1433
    Public Property TestForSQLServerTimeout As Integer = 500
    Public Property UseCustomSQLCredentials As Boolean = False
    Public Property CustomSQLCredentialsUsername As String = ""
    Public Property CustomSQLCredentialsPassword As String = ""
    Public Property SQLSelectString As String = "SELECT * FROM MYTABLE"
    Public Property SQLCommandTimeout As Integer = 3000
    Public Property AutoCreatingColumns As Boolean = True
    Public Property CustomColumnCollection As New List(Of CustomColumn)
    Public Property AutoResizeColumns As Boolean = True
    Public Property SelectFirstRow As Boolean = True
    Public Property ShowErrorIfNoRow As Boolean = True
    Public Property NullValue As String = "(null)"
    Public Property FirstLoadDelay As Integer = 0
    Public Property SQLQueryDelay As Integer = 0
    Public Property ResultStyle As ResultStyleEnum = ResultStyleEnum.ShowOnlyColumns
    Public Property SplitViewStyle As Orientation = Orientation.Horizontal
    Public Property GroupTextCollection As New List(Of CustomGroupEntry)
End Class

<Serializable> Public Enum ResultStyleEnum As Integer
    ShowOnlyColumns = 0
    ShowOnlyGroups = 1
    ShowGroupsAndColumns = 2
End Enum

<Serializable> Public Class CustomColumn
    Public Property Name As String = ""
    Public Property ColumnHeaderText As String = ""
    Public Property ColumnWidth As Integer = 200
    Public Property TextAlign As HorizontalAlignment = HorizontalAlignment.Left
    Public Property DisplayIndex As Integer = -1
End Class

<Serializable> Public Class CustomGroupEntry
    Public Property Name As String = ""
    Public Property GroupText As String = ""
    Public Property ValueIndex As Integer = 0
    Public Property GroupWidth As Integer = 200
End Class
