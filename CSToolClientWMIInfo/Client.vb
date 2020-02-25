Imports CSToolPluginLib

Namespace My
    Public Class Client
        Implements ICSToolInterface

        Public ReadOnly Property PluginName As String Implements ICSToolInterface.PluginName
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public ReadOnly Property PluginVersion As String Implements ICSToolInterface.PluginVersion
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public ReadOnly Property PluginPublisher As String Implements ICSToolInterface.PluginPublisher
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public ReadOnly Property GUILoaded As Boolean Implements ICSToolInterface.GUILoaded
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Function SavePluginSettings(Optional Filename As String = "") As Boolean Implements ICSToolInterface.SavePluginSettings
            Throw New NotImplementedException()
        End Function

        Public Function LoadPluginSettings(Optional Filename As String = "") As Boolean Implements ICSToolInterface.LoadPluginSettings
            Throw New NotImplementedException()
        End Function

        Public Function RefreshGUI() As Boolean Implements ICSToolInterface.RefreshGUI
            Throw New NotImplementedException()
        End Function

        Public Function LockGUI() As Boolean Implements ICSToolInterface.LockGUI
            Throw New NotImplementedException()
        End Function

        Public Function UnlockGUI() As Boolean Implements ICSToolInterface.UnlockGUI
            Throw New NotImplementedException()
        End Function

        Public Function RaiseActions(HostnameOrIP As String) As Boolean Implements ICSToolInterface.RaiseActions
            Throw New NotImplementedException()
        End Function

        Public Function LoadPlugin() As Boolean Implements ICSToolInterface.LoadPlugin
            Throw New NotImplementedException()
        End Function

        Public Function UnloadPlugin() As Boolean Implements ICSToolInterface.UnloadPlugin
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace


