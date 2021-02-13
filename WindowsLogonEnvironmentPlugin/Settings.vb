'Copyright (C) 2019-2021 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
<Serializable> Public Class SettingsClass
    Public Property GetFullUsername As Boolean = True
    Public Property GetUsernameGivenName As Boolean = True
    Public Property GetUsernameName As Boolean = True
    Public Property GetUsernameMiddlename As Boolean = False
    Public Property GetUsernameSurname As Boolean = True
    Public Property GetUserMail As Boolean = False
    Public Property GetUsernameSID As Boolean = False
    Public Property GetUsernameEmployeeID As Boolean = False
    Public Property GetUsernameSAMAccountName As Boolean = False
End Class
