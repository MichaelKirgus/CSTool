'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Security.Cryptography.X509Certificates
Imports CefSharp

Public Class CustomRequestHandler
    Implements IRequestHandler

    Private userName As String
    Private password As String

    Public Sub New(ByVal userName As String, ByVal password As String)
        Me.userName = userName
        Me.password = password
    End Sub

    Public Function OnBeforeBrowse(chromiumWebBrowser As IWebBrowser, browser As IBrowser, frame As IFrame, request As IRequest, userGesture As Boolean, isRedirect As Boolean) As Boolean Implements IRequestHandler.OnBeforeBrowse
        Return False
    End Function

    Private Function IRequestHandler_OnOpenUrlFromTab(chromiumWebBrowser As IWebBrowser, browser As IBrowser, frame As IFrame, targetUrl As String, targetDisposition As WindowOpenDisposition, userGesture As Boolean) As Boolean Implements IRequestHandler.OnOpenUrlFromTab
        Throw New NotImplementedException()
    End Function

    Public Function GetResourceRequestHandler(chromiumWebBrowser As IWebBrowser, browser As IBrowser, frame As IFrame, request As IRequest, isNavigation As Boolean, isDownload As Boolean, requestInitiator As String, ByRef disableDefaultHandling As Boolean) As IResourceRequestHandler Implements IRequestHandler.GetResourceRequestHandler
        Throw New NotImplementedException()
    End Function

    Public Function GetAuthCredentials(chromiumWebBrowser As IWebBrowser, browser As IBrowser, originUrl As String, isProxy As Boolean, host As String, port As Integer, realm As String, scheme As String, callback As IAuthCallback) As Boolean Implements IRequestHandler.GetAuthCredentials
        callback.[Continue](userName, password)
        Return True
    End Function

    Private Function IRequestHandler_OnQuotaRequest(chromiumWebBrowser As IWebBrowser, browser As IBrowser, originUrl As String, newSize As Long, callback As IRequestCallback) As Boolean Implements IRequestHandler.OnQuotaRequest
        Throw New NotImplementedException()
    End Function

    Private Function IRequestHandler_OnCertificateError(chromiumWebBrowser As IWebBrowser, browser As IBrowser, errorCode As CefErrorCode, requestUrl As String, sslInfo As ISslInfo, callback As IRequestCallback) As Boolean Implements IRequestHandler.OnCertificateError
        Return False
    End Function

    Private Function IRequestHandler_OnSelectClientCertificate(chromiumWebBrowser As IWebBrowser, browser As IBrowser, isProxy As Boolean, host As String, port As Integer, certificates As X509Certificate2Collection, callback As ISelectClientCertificateCallback) As Boolean Implements IRequestHandler.OnSelectClientCertificate
        Throw New NotImplementedException()
    End Function

    Private Sub IRequestHandler_OnPluginCrashed(chromiumWebBrowser As IWebBrowser, browser As IBrowser, pluginPath As String) Implements IRequestHandler.OnPluginCrashed
        Throw New NotImplementedException()
    End Sub

    Private Sub IRequestHandler_OnRenderViewReady(chromiumWebBrowser As IWebBrowser, browser As IBrowser) Implements IRequestHandler.OnRenderViewReady
        Throw New NotImplementedException()
    End Sub

    Private Sub IRequestHandler_OnRenderProcessTerminated(chromiumWebBrowser As IWebBrowser, browser As IBrowser, status As CefTerminationStatus) Implements IRequestHandler.OnRenderProcessTerminated
        Throw New NotImplementedException()
    End Sub

    Public Sub OnDocumentAvailableInMainFrame(chromiumWebBrowser As IWebBrowser, browser As IBrowser) Implements IRequestHandler.OnDocumentAvailableInMainFrame
        Throw New NotImplementedException()
    End Sub
End Class
