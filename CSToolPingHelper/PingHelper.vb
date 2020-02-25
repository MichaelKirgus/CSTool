'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Net
Imports System.Net.Sockets

Public Class PingHelper
    Dim mIpAddrV4 As String
    Dim mIpAddrV6 As String
    Dim mResponseTime As Long
    Dim mError As String
    Dim mHostName As String
    Dim mSuccess As Boolean = False

    ''' <summary>
    ''' Liefert die Rückmeldezeit in Millisekunden zurück
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ResponseTime() As Integer
        Get
            ResponseTime = mResponseTime
        End Get
    End Property

    ''' <summary>
    ''' Liefert die Fehlermeldung zurück (ist leer bei erfolreichem Ping)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ErrorDesc() As String
        Get
            ErrorDesc = mError
        End Get
    End Property

    ''' <summary>
    ''' Liefert True zurück wenn Ping erfolgreich war (sonst False)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Success() As Boolean
        Get
            Success = mSuccess
        End Get
    End Property

    ''' <summary>
    ''' Liefert die IP-Adresse des Hosts zurück
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IpAddressV4() As String
        Get
            If mIpAddrV4 IsNot Nothing Then
                IpAddressV4 = mIpAddrV4.ToString()
            Else
                IpAddressV4 = ""
            End If
        End Get
    End Property
    Public ReadOnly Property IpAddressV6() As String
        Get
            If mIpAddrV6 IsNot Nothing Then
                IpAddressV6 = mIpAddrV6.ToString()
            Else
                IpAddressV6 = ""
            End If
        End Get
    End Property

    ''' <summary>
    ''' Liefert den Hostnamen zurück
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property HostName() As String
        Get
            HostName = mHostName
        End Get
    End Property

    ''' <summary>
    ''' Versucht den Hostnamen aufzulösen (Führt kein Ping durch.)
    ''' </summary>
    ''' <param name="HName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckByName(ByVal HName As String) As Boolean
        mError = ""
        Try
            Dim ipadresscol As IPAddress()
            ipadresscol = Dns.GetHostEntry(HName).AddressList
            mIpAddrV4 = ipadresscol(0).MapToIPv4.ToString()
            mIpAddrV6 = ipadresscol(0).MapToIPv6.ToString()
            mHostName = Dns.GetHostEntry(HName.ToString).HostName
            If mHostName.Length = 0 Then mHostName = HName
            Return True
        Catch ex As System.Net.Sockets.SocketException
            mError = ex.Message
            If mHostName.Length = 0 Then mHostName = HName
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Versucht die IP-Adresse aufzulösen (Führt kein Ping durch.)
    ''' </summary>
    ''' <param name="ipAddr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckByIpAddr(ByVal ipAddr As String) As Boolean
        mError = ""
        Try
            Dim ip As IPAddress
            ip = System.Net.IPAddress.Parse(ipAddr)

            If ip.AddressFamily = AddressFamily.InterNetworkV6 Then
                mIpAddrV6 = ip.ToString
                Try
                    mHostName = Dns.GetHostEntry(mIpAddrV6).HostName
                    If mHostName.Equals(mIpAddrV6) Then mHostName = ""
                    Return True
                Catch SEx As SocketException
                    mError = SEx.Message
                    Return False
                End Try
            Else
                mIpAddrV4 = ip.ToString
                Try
                    mHostName = Dns.GetHostEntry(mIpAddrV4).HostName
                    If mHostName.Equals(mIpAddrV4) Then mHostName = ""
                    Return True
                Catch SEx As SocketException
                    mError = SEx.Message
                    Return False
                End Try
            End If

        Catch Ex As Exception
            mError = Ex.Message
            Return False
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Diese Funktion prüft ob der übergebene String eine 
    ''' (gültige) IP-Adresse ist
    ''' </summary>
    ''' <param name="sHostNameOrAddress">IP oder Hostname</param>
    ''' <returns>Liefert False zurück wenn die IP ungültig ist 
    ''' bzw. ein Hostname übergeben wurde</returns>
    ''' <remarks></remarks>
    Public Function IsValidIP(ByVal sHostNameOrAddress As String) As Boolean
        Return System.Net.IPAddress.TryParse(sHostNameOrAddress,
          System.Net.IPAddress.Any)
    End Function

    ''' <summary>
    ''' Sendet ein Ping-Signal an die angegebene IP-Adresse oder den Hostnamen.
    ''' Über "Success" erhalten sie die Fehlermledung fals beim Pingen ein 
    ''' Fehler aufgetreten ist
    ''' </summary>
    ''' <param name="sHostNameOrAddress">String. Der URL, der Computername oder 
    ''' die IP-Adressse des Servers an den ein Ping-Signal gesendet werden soll.</param>
    ''' <param name="timeout">Zeitgrenzwert (in Millisekunden) für das Herstellen 
    ''' einer Verbindung mit dem Ziel</param>
    ''' <param name="ResolveIPAndHostname">Wenn True, wird die IP-Adresse zu dem 
    ''' übergebenen Hostnamen (und umgekehrt) ermittelt.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Ping(ByVal sHostNameOrAddress As String,
      Optional ByVal timeout As Integer = 5000,
      Optional ByVal ResolveIPAndHostname As Boolean = True) As Boolean

        Dim bIsIP As Boolean = System.Net.IPAddress.TryParse(
          sHostNameOrAddress, System.Net.IPAddress.Any)
        Dim oPing = New System.Net.NetworkInformation.Ping
        Dim PingReturn As System.Net.NetworkInformation.PingReply

        mError = ""
        mHostName = ""
        mIpAddrV4 = ""
        mResponseTime = 0

        Try
            PingReturn = oPing.Send(sHostNameOrAddress, timeout)
            ' Feststellen ob Ping erfolgreich war
            If PingReturn.Status = NetworkInformation.IPStatus.Success Then
                ' IP und Hostname ermitteln (jenachdem od übergebener String 
                ' eine IP oder Host war)
                If ResolveIPAndHostname = True Then
                    If bIsIP = False Then

                        mIpAddrV4 = PingReturn.Address().MapToIPv4.ToString
                        mIpAddrV6 = PingReturn.Address().MapToIPv6.ToString

                        mHostName = sHostNameOrAddress
                    Else
                        mIpAddrV4 = sHostNameOrAddress
                        mHostName = Dns.GetHostEntry(mIpAddrV4).HostName
                    End If
                End If

                mResponseTime = PingReturn.RoundtripTime
                mSuccess = True
                Return True
            Else
                ' Prüfen ob der String von sHostNameOrAddress eine IP oder 
                ' ein Hostname war
                If bIsIP Then
                    mIpAddrV4 = sHostNameOrAddress
                Else
                    mHostName = sHostNameOrAddress
                End If
                mError = PingReturn.Status.ToString
                mSuccess = False
                Return False
            End If

        Catch ex As System.Net.NetworkInformation.PingException
            If bIsIP Then
                mIpAddrV4 = sHostNameOrAddress
            Else
                mHostName = sHostNameOrAddress
            End If
            mError = ex.InnerException.Message
            mSuccess = False
            Return False
        End Try
    End Function
End Class
