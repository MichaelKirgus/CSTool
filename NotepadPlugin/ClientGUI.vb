'Copyright (C) 2019-2020 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
'Additional copyright notices in project base directory or main executable directory.
Imports CSToolLogLib.LogSettings

Public Class ClientGUI
    Public _Settings As SettingsClass
    Public _ParentInstance As CSToolPluginLib.ICSToolInterface
    Public LastFilename As String = ""
    Private Sub SaveToFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToFileToolStripMenuItem.Click
        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFromFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFromFileToolStripMenuItem.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub ClearTextToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearTextToolStripMenuItem.Click
        RichTextBox1.Text = ""
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        _ParentInstance.CurrentLogInstance.WriteLogEntry("Save content to file " & SaveFileDialog1.FileName, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
        RichTextBox1.SaveFile(SaveFileDialog1.FileName)
        LastFilename = SaveFileDialog1.FileName
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        _ParentInstance.CurrentLogInstance.WriteLogEntry("Load content from file " & OpenFileDialog1.FileName, Me.GetType, LogEntryTypeEnum.Info, LogEntryLevelEnum.Debug)
        RichTextBox1.LoadFile(OpenFileDialog1.FileName)
        LastFilename = OpenFileDialog1.FileName
    End Sub

    Public Sub LoadFileAtStart()
        If Not _Settings.InitialFile = "" Then
            If IO.File.Exists(_Settings.InitialFile) Then
                RichTextBox1.LoadFile(_Settings.InitialFile)
            End If
        End If
    End Sub

    Public Sub RefreshGUI()
        If Not _Settings.WindowTitle = "" Then
            _ParentInstance.CurrentWindowTitle = _Settings.WindowTitle
            Me.ParentForm.Text = _Settings.WindowTitle
        End If
    End Sub

    Public Sub SaveFileAtExit()
        If SaveChangesBeforeExitToolStripMenuItem.Checked Then
            If Not LastFilename = "" Then
                RichTextBox1.SaveFile(LastFilename)
            End If
        End If
    End Sub

    Private Sub ClientGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadFileAtStart()
    End Sub
End Class
