'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Serializable> Public Class Settings
    Public Property InitialTitle As String = ""
    Public Property ServerName As String = ""
    Public Property RDPDomain As String = ""
    Public Property RDPUsername As String = ""
    Public Property RDPPassword As String = ""
    Public Property EnableCredentialSSPSupport As Boolean = True
    Public Property AllowCredentialSaving As Boolean = False
    Public Property PromptForCredentials As Boolean = False
    Public Property PromptForCredentialsAtClient As Boolean = False
    Public Property ShowServerNameInWindowTitle As Boolean = True
    Public Property ShowToolbar As Boolean = True
    Public Property HideToolbarAfterConnect As Boolean = True
    Public Property AutoConnect As Boolean = True
    Public Property EnablePrinterRedirection As Boolean = False
    Public Property EnableBidirectionalClipboard As Boolean = True
    Public Property EnableSoundRedirection As Boolean = False
    Public Property ResizeRemoteDesktopToLocalSize As Boolean = False
    Public Property EnableSmartResizing As Boolean = False
    Public Property GrabFocusOnConnect As Boolean = False
End Class
