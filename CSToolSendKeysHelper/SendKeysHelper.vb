'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Public Class SendKeysHelper
    <DllImport("user32.dll", CallingConvention:=CallingConvention.StdCall,
               CharSet:=CharSet.Unicode, EntryPoint:="keybd_event",
               ExactSpelling:=True, SetLastError:=True)>
    Public Shared Function keybd_event(ByVal bVk As Byte, ByVal _
      bScan As Byte, ByVal dwFlags As UInteger, ByVal dwExtraInfo As UIntPtr) _
      As Boolean
    End Function

    Public Const VK_CAPITAL As Byte = &H14
    Public Const KEYEVENTF_KEYUP As Byte = &H2

    Public Const VK_LBUTTON = &H1
    Public Const VK_RBUTTON = &H2
    Public Const VK_MBUTTON = &H4
    Public Const vbKeyCapital = 20

    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Long) As Integer
    Public Declare Function GetKeyboardState Lib "user32" (pbKeyState As Byte) As Long
    Public Declare Function SetKeyboardState Lib "user32" (lppbKeyState As Byte) As Long
    Public Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As Long) As Long

    Public Function SimulateText(ByVal FirstStr As String, ByVal SecoundStr As String, ByVal AutoFillCharTimeout As Integer, ByVal AutoFillCtlTimeout As Integer, ByVal AutoFillSwitchCommand As String, ByVal AutoFillSubmitCommand As String) As Boolean
        'Diese Funktion simuliert Tastatureingaben.
        Try
            'Capslock deaktivieren, falls aktiv
            Dim wascapsaktive As Boolean = My.Computer.Keyboard.CapsLock

            If wascapsaktive = True Then
                Try
                    keybd_event(VK_CAPITAL, 1, 0, 0)
                    keybd_event(VK_CAPITAL, 1, KEYEVENTF_KEYUP, 0)
                Catch ex As Exception
                End Try
            End If

            'Eingabe des Benutzernamens
            If Not FirstStr = "" Then
                Threading.Thread.Sleep(10)
                Dim str As String
                str = ReplaceSpecialKeys(FirstStr)
                For Each jk As String In str
                    SendKeys.Send(jk)
                    Threading.Thread.Sleep(AutoFillCharTimeout)
                Next

                Threading.Thread.Sleep(AutoFillCtlTimeout)
                SendKeys.Send(AutoFillSwitchCommand)
                Threading.Thread.Sleep(AutoFillCtlTimeout)
            End If

            'Eingabe des Kennworts über die Tastatursimulation
            Dim str1 As String
            str1 = ReplaceSpecialKeys(SecoundStr)
            Dim compatspecial As String = ""
            Dim specialkey As Boolean = False
            Dim commitkeys As Boolean = False
            For Each jk As String In str1
                If Not jk = "{" Then
                    If specialkey = False Then
                        SendKeys.Send(jk)
                        Threading.Thread.Sleep(AutoFillCharTimeout)
                    End If
                Else
                    specialkey = True
                End If
                If specialkey = True Then
                    If jk = "}" Then
                        specialkey = False
                        compatspecial += jk
                        commitkeys = True
                    Else
                        compatspecial += jk
                    End If
                End If
                If commitkeys = True Then
                    commitkeys = False
                    SendKeys.Send(compatspecial)
                    compatspecial = ""
                    Threading.Thread.Sleep(AutoFillCharTimeout)
                End If
            Next

            Threading.Thread.Sleep(AutoFillCtlTimeout)
            SendKeys.Send(AutoFillSubmitCommand)

            'Capslock wieder aktivieren, wenn es vor dem Aufruf aktiviert war
            If wascapsaktive = True Then
                Try
                    keybd_event(VK_CAPITAL, 1, 0, 0)
                    keybd_event(VK_CAPITAL, 1, KEYEVENTF_KEYUP, 0)
                Catch ex As Exception
                End Try
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ReplaceSpecialKeys(ByVal KeyStr As String) As String
        Try
            KeyStr = KeyStr.Replace("+", "{+}")
            KeyStr = KeyStr.Replace("^", "{^}")
            KeyStr = KeyStr.Replace("%", "{%}")
            KeyStr = KeyStr.Replace("~", "{~}")
            KeyStr = KeyStr.Replace("(", "{(}")
            KeyStr = KeyStr.Replace(")", "{)}")

            Return KeyStr
        Catch ex As Exception
            Return KeyStr
        End Try
    End Function
End Class
