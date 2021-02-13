'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Public Class WindowSearcher
    Public Declare Function EnumWindows Lib "user32.dll" (ByVal lpEnumFunc As EnumWindowsProc, ByVal lParam As Int32) As Int32
    Public Declare Function IsWindowVisible Lib "user32.dll" (ByVal hwnd As IntPtr) As Boolean
    Public Delegate Function EnumWindowsProc(ByVal hwnd As IntPtr, ByVal lParam As Int32) As Boolean
    Public Declare Function GetWindowText Lib "user32.dll" Alias "GetWindowTextA" (ByVal hwnd As IntPtr, ByVal lpString As String, ByVal cch As Int32) As Int32
    Public Declare Function GetWindowTextLength Lib "user32.dll" Alias "GetWindowTextLengthA" (ByVal hwnd As IntPtr) As Int32
    Public Declare Function GetWindowLong Lib "user32.dll" Alias "GetWindowLongA" (ByVal hwnd As IntPtr, ByVal nIndex As Int32) As Int32
    Public Declare Function GetParent Lib "user32.dll" (ByVal intptr As IntPtr) As IntPtr

    Public Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As Long) As Long
    Private Declare Function FindWindowEx Lib "user32.dll" Alias "FindWindowExA" (ByVal hWnd1 As IntPtr, ByVal hWnd2 As IntPtr, ByVal lpsz1 As String, ByVal lpsz2 As String) As IntPtr

    Public Const GWL_HWNDPARENT As Int32 = -8
    Private newwindowlist As List(Of String)

    Private Function EnumWinProc(ByVal hwnd As IntPtr, ByVal lParam As Int32) As Boolean
        If IsWindowVisible(hwnd) Then
            If GetParent(hwnd) = IntPtr.Zero Then
                If GetWindowLong(hwnd, GWL_HWNDPARENT) = 0 Then
                    Dim str As String = String.Empty.PadLeft(GetWindowTextLength(hwnd) + 1)
                    GetWindowText(hwnd, str, str.Length)
                    If Not String.IsNullOrEmpty(str.Substring(0, str.Length - 1)) Then newwindowlist.Add(str.Substring(0, str.Length - 1))
                End If
            End If
        End If
        EnumWinProc = True
    End Function

    Private Sub RefreshWindowList()
        newwindowlist = New List(Of String)
        EnumWindows(AddressOf EnumWinProc, CInt(True))
    End Sub

    Public Function GetWindowTitles() As List(Of String)
        Try
            RefreshWindowList()
            Return newwindowlist
        Catch ex As Exception
            Return New List(Of String)
        End Try
    End Function

    Public Function GetChildWindow(ByVal Parent As IntPtr) As IntPtr
        Try
            Return FindWindowEx(Parent, 0, "", "")
        Catch ex As Exception
        End Try
    End Function
End Class
