'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.IO
Imports System.Security.Cryptography

Public Class MD5HashHelper
    Function GetMD5FromFile(ByVal file_name As String) As String
        Try
            Dim hash = MD5.Create()
            Dim hashValue() As Byte
            Dim fileStream As FileStream = File.OpenRead(file_name)
            fileStream.Position = 0
            hashValue = hash.ComputeHash(fileStream)
            Dim hash_hex = PrintByteArray(hashValue)
            fileStream.Close()
            Return hash_hex
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function PrintByteArray(ByVal array() As Byte) As String
        Try
            Dim hex_value As String = ""
            Dim i As Integer
            For i = 0 To array.Length - 1
                hex_value += array(i).ToString("X2")
            Next i
            Return hex_value.ToLower
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class
