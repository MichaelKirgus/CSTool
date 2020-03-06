'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSCustomActionHelper
Imports CSToolLogLib

Public Class CustomButtonCtl
    Public _parent As ClientGUI

    Private Sub CustomButton_Click(sender As Object, e As EventArgs) Handles CustomButton.Click
        Try
            Dim actionobj As CustomButtonEntry
            actionobj = Me.Tag

            If Not actionobj.RaisingActions.Count = 0 Then
                For index = 0 To actionobj.RaisingActions.Count - 1
                    Dim actionsitm As New CustomActionHelper
                    actionsitm._LogManager = _parent._ParentInstance.CurrentLogInstance
                    actionsitm._EnvironmentRuntimeVariables = _parent._ParentInstance.EnvironmentRuntimeVariables
                    actionsitm.RaiseCustomItem(actionobj.RaisingActions(index))
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
