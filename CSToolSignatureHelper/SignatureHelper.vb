'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.IO
Imports System.Security.Cryptography.X509Certificates

Public Class SignatureHelper
    Public Function CheckSign(Path As String, Optional ByVal RevocationMode As X509RevocationMode = X509RevocationMode.Offline) As Boolean
        Try
            Dim filePath As String = Path

            If Not File.Exists(filePath) Then
                Return False
            End If

            Dim theCertificate As X509Certificate2

            Try
                Dim theSigner As X509Certificate = X509Certificate.CreateFromSignedFile(filePath)
                theCertificate = New X509Certificate2(theSigner)
            Catch ex As Exception
                Return False
            End Try

            Dim chainIsValid As Boolean = False

            Dim theCertificateChain = New X509Chain(True)

            theCertificateChain.ChainPolicy.RevocationFlag = X509RevocationFlag.ExcludeRoot
            theCertificateChain.ChainPolicy.RevocationMode = RevocationMode
            theCertificateChain.ChainPolicy.UrlRetrievalTimeout = New TimeSpan(0, 0, 10)
            theCertificateChain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag

            chainIsValid = theCertificateChain.Build(theCertificate)


            Dim doTrust As Boolean = (theCertificateChain.ChainElements(theCertificateChain.ChainElements.Count - 1).Certificate.Verify)
            If doTrust = False Then
                Return False
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
