'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Runtime.InteropServices

Public Class ProcessHelper

#Region "API Structures"
    <StructLayout(LayoutKind.Sequential)>
    Public Structure PROCESS_INFORMATION
        Dim hProcess As System.IntPtr
        Dim hThread As System.IntPtr
        Dim dwProcessId As Integer
        Dim dwThreadId As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure STARTUPINFO
        Dim cb As Integer
        Dim lpReserved As System.IntPtr
        Dim lpDesktop As System.IntPtr
        Dim lpTitle As System.IntPtr
        Dim dwX As Integer
        Dim dwY As Integer
        Dim dwXSize As Integer
        Dim dwYSize As Integer
        Dim dwXCountChars As Integer
        Dim dwYCountChars As Integer
        Dim dwFillAttribute As Integer
        Dim dwFlags As Integer
        Dim wShowWindow As Short
        Dim cbReserved2 As Short
        Dim lpReserved2 As System.IntPtr
        Dim hStdInput As System.IntPtr
        Dim hStdOutput As System.IntPtr
        Dim hStdError As System.IntPtr
    End Structure
#End Region

#Region "API Constants"
    Private Const LOGON_NETCREDENTIALS_ONLY As Integer = &H2
    Private Const NORMAL_PRIORITY_CLASS As Integer = &H20
    Private Const CREATE_DEFAULT_ERROR_MODE As Integer = &H4000000
    Private Const CREATE_NEW_CONSOLE As Integer = &H10
    Private Const CREATE_NEW_PROCESS_GROUP As Integer = &H200
    Private Const LOGON_WITH_PROFILE As Integer = &H1
#End Region

#Region "API Functions"
    Private Declare Unicode Function CreateProcessWithLogon Lib "Advapi32" Alias "CreateProcessWithLogonW" _
        (ByVal lpUsername As String,
         ByVal lpDomain As String,
         ByVal lpPassword As String,
         ByVal dwLogonFlags As Integer,
         ByVal lpApplicationName As String,
         ByVal lpCommandLine As String,
         ByVal dwCreationFlags As Integer,
         ByVal lpEnvironment As System.IntPtr,
         ByVal lpCurrentDirectory As System.IntPtr,
         ByRef lpStartupInfo As STARTUPINFO,
         ByRef lpProcessInfo As PROCESS_INFORMATION) As Integer

    Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As System.IntPtr) As Integer

#End Region

    Public Sub RunProgram(ByVal UserName As String, ByVal Password As String, ByVal Domain As String, ByVal Application As String, ByVal CommandLine As String)

        Dim siStartup As STARTUPINFO
        Dim piProcess As PROCESS_INFORMATION
        Dim intReturn As Integer

        If CommandLine Is Nothing OrElse CommandLine = "" Then CommandLine = String.Empty

        siStartup.cb = Marshal.SizeOf(siStartup)
        siStartup.dwFlags = 0

        intReturn = CreateProcessWithLogon(UserName, Domain, Password, LOGON_WITH_PROFILE, Application, CommandLine,
        NORMAL_PRIORITY_CLASS Or CREATE_DEFAULT_ERROR_MODE Or CREATE_NEW_CONSOLE Or CREATE_NEW_PROCESS_GROUP,
        IntPtr.Zero, IntPtr.Zero, siStartup, piProcess)

        If intReturn = 0 Then
            Throw New System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error())
        End If

        CloseHandle(piProcess.hProcess)
        CloseHandle(piProcess.hThread)
    End Sub
End Class
