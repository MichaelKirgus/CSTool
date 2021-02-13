'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Class EncryptDecryptHelper
    Public Function EncryptTextToBase64(ByVal KeyStr As String, ByVal DataStr As String) As String
        Try
            Dim rd As New RijndaelManaged

            Dim md5 As New MD5CryptoServiceProvider
            Dim key() As Byte = md5.ComputeHash(Encoding.UTF8.GetBytes(KeyStr))

            md5.Clear()
            rd.Key = key
            rd.GenerateIV()

            Dim iv() As Byte = rd.IV
            Dim ms As New MemoryStream

            ms.Write(iv, 0, iv.Length)

            Dim cs As New CryptoStream(ms, rd.CreateEncryptor, CryptoStreamMode.Write)
            Dim data() As Byte = System.Text.Encoding.UTF8.GetBytes(DataStr)

            cs.Write(data, 0, data.Length)
            cs.FlushFinalBlock()

            Dim encdata() As Byte = ms.ToArray()
            Dim enctxt As String
            enctxt = Convert.ToBase64String(encdata)
            cs.Close()
            rd.Clear()

            Return enctxt
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function DecryptTextFromBase64(ByVal KeyStr As String, ByVal DataStr As String) As String
        Try
            Dim rd As New RijndaelManaged
            Dim rijndaelIvLength As Integer = 16
            Dim md5 As New MD5CryptoServiceProvider
            Dim key() As Byte = md5.ComputeHash(Encoding.UTF8.GetBytes(KeyStr))

            md5.Clear()

            Dim encdata() As Byte = Convert.FromBase64String(DataStr)
            Dim ms As New MemoryStream(encdata)
            Dim iv(15) As Byte

            ms.Read(iv, 0, rijndaelIvLength)
            rd.IV = iv
            rd.Key = key

            Dim cs As New CryptoStream(ms, rd.CreateDecryptor, CryptoStreamMode.Read)

            Dim data(ms.Length - rijndaelIvLength) As Byte
            Dim i As Integer = cs.Read(data, 0, data.Length)

            Dim decstr As String
            decstr = System.Text.Encoding.UTF8.GetString(data, 0, i)
            cs.Close()
            rd.Clear()

            Return decstr
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class
