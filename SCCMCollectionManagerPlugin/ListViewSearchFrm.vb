'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports System.Windows.Forms

Public Class ListViewSearchFrm

    Public ListViewCtl As ListView

    Public Sub LoadGroupItems()
        Try
            If Not ListViewCtl.Groups.Count = 0 Then
                For index = 0 To ListViewCtl.Groups.Count - 1
                    ComboBox1.Items.Add(ListViewCtl.Groups(index).Header)
                Next
            End If

            ComboBox1.SelectedIndex = 0
        Catch ex As Exception
        End Try
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim result As Boolean = False
            Dim FirstFoundItemIndex As Integer = 0
            Dim resultcount As Integer = 0

            If ComboBox1.SelectedIndex = 0 Then
                If RadioButton1.Checked Then
                    If CheckBox1.Checked Then
                        For ind = 0 To ListViewCtl.Items.Count - 1
                            If ListViewCtl.Items(ind).Text.Contains(TextBox1.Text) Then
                                If CheckBox2.Checked Then
                                    ListViewCtl.Items(ind).Selected = True
                                End If
                                If result = False Then
                                    FirstFoundItemIndex = ind
                                End If
                                result = True
                                resultcount += 1
                            End If
                        Next
                    Else
                        For ind = 0 To ListViewCtl.Items.Count - 1
                            If ListViewCtl.Items(ind).Text.ToLower.Contains(TextBox1.Text.ToLower) Then
                                If CheckBox2.Checked Then
                                    ListViewCtl.Items(ind).Selected = True
                                End If
                                If result = False Then
                                    FirstFoundItemIndex = ind
                                End If
                                result = True
                                resultcount += 1
                            End If
                        Next
                    End If
                Else
                    If CheckBox1.Checked Then
                        For ind = 0 To ListViewCtl.Items.Count - 1
                            If ListViewCtl.Items(ind).Text = TextBox1.Text Then
                                If CheckBox2.Checked Then
                                    ListViewCtl.Items(ind).Selected = True
                                End If
                                If result = False Then
                                    FirstFoundItemIndex = ind
                                End If
                                result = True
                                resultcount += 1
                            End If
                        Next
                    Else
                        For ind = 0 To ListViewCtl.Items.Count - 1
                            If ListViewCtl.Items(ind).Text.ToLower = TextBox1.Text.ToLower Then
                                If CheckBox2.Checked Then
                                    ListViewCtl.Items(ind).Selected = True
                                End If
                                If result = False Then
                                    FirstFoundItemIndex = ind
                                End If
                                result = True
                                resultcount += 1
                            End If
                        Next
                    End If
                End If
            Else
                For ind = 0 To ListViewCtl.Items.Count - 1
                    If ListViewCtl.Items(ind).Group.Header = ListViewCtl.Groups(ComboBox1.SelectedIndex - 1).Header Then
                        If RadioButton1.Checked Then
                            If CheckBox1.Checked Then
                                If ListViewCtl.Items(ind).Text.Contains(TextBox1.Text) Then
                                    If CheckBox2.Checked Then
                                        ListViewCtl.Items(ind).Selected = True
                                    End If
                                    If result = False Then
                                        FirstFoundItemIndex = ind
                                    End If
                                    result = True
                                    resultcount += 1
                                End If
                            Else
                                If ListViewCtl.Items(ind).Text.ToLower.Contains(TextBox1.Text.ToLower) Then
                                    If CheckBox2.Checked Then
                                        ListViewCtl.Items(ind).Selected = True
                                    End If
                                    If result = False Then
                                        FirstFoundItemIndex = ind
                                    End If
                                    result = True
                                    resultcount += 1
                                End If
                            End If
                        Else
                            If CheckBox1.Checked Then
                                If ListViewCtl.Items(ind).Text = TextBox1.Text Then
                                    If CheckBox2.Checked Then
                                        ListViewCtl.Items(ind).Selected = True
                                    End If
                                    If result = False Then
                                        FirstFoundItemIndex = ind
                                    End If
                                    result = True
                                    resultcount += 1
                                End If
                            Else
                                If ListViewCtl.Items(ind).Text.ToLower = TextBox1.Text.ToLower Then
                                    If CheckBox2.Checked Then
                                        ListViewCtl.Items(ind).Selected = True
                                    End If
                                    If result = False Then
                                        FirstFoundItemIndex = ind
                                    End If
                                    result = True
                                    resultcount += 1
                                End If
                            End If
                        End If

                    End If
                Next
            End If

            If result = False Then
                ResultCountLbl.Text = "No results found."
                MsgBox("No results found!", MsgBoxStyle.Information)
            Else
                ResultCountLbl.Text = resultcount & " result(s) found."
                ListViewCtl.EnsureVisible(FirstFoundItemIndex)
            End If
        Catch ex As Exception
            MsgBox("Error searching.", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub ListViewSearchFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGroupItems()
    End Sub
End Class
