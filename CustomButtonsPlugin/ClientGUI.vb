'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Drawing
Imports CSToolEnvironmentManager

Public Class ClientGUI
    Public _Settings As Settings
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public EnvManager As New EnvironmentManager

    Public Sub RefreshGUI()
        Try
            If Not _Settings.InitialTitle = "" Then
                Me.ParentForm.Text = _Settings.InitialTitle
                _ParentInstance.CurrentWindowTitle = _Settings.InitialTitle
            End If

            CustomFlowPanel.Controls.Clear()

            If Not _Settings.CustomButtonsCollection.Count = 0 Then
                For index = 0 To _Settings.CustomButtonsCollection.Count - 1
                    Dim newctl As New CustomButtonCtl
                    Dim newsize As New Size(_Settings.CustomButtonsCollection(index).ButtonWidth, _Settings.CustomButtonsCollection(index).ButtonHeight)
                    CustomFlowPanel.Controls.Add(newctl)
                    newctl.Size = newsize
                    newctl.CustomButton.Text = EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomButtonsCollection(index).ButtonText)
                    newctl._parent = Me
                    If Not _Settings.CustomButtonsCollection(index).ButtonIconFile = "" Then
                        Dim iconobj As Image
                        iconobj = Image.FromFile(EnvManager.ResolveEnvironmentVariables(_ParentInstance.EnvironmentRuntimeVariables, _Settings.CustomButtonsCollection(index).ButtonIconFile))
                        newctl.CustomButton.Image = iconobj
                        newctl.CustomButton.ImageAlign = _Settings.CustomButtonsCollection(index).ButtonIconFileAlignment
                    End If
                    newctl.CustomButton.TextAlign = _Settings.CustomButtonsCollection(index).ButtonTextAlignment
                    newctl.CustomButton.TextImageRelation = _Settings.CustomButtonsCollection(index).ButtonImageTextRelation
                    newctl.CustomButton.FlatStyle = _Settings.CustomButtonsCollection(index).ButtonFlatStyle
                    newctl.Tag = _Settings.CustomButtonsCollection(index)
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshGUI()
    End Sub
End Class
