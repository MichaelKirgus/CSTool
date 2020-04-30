Imports CSToolApplicationSettingsLib
Imports CSToolApplicationSettingsManager
Imports CSToolEnvironmentManager
Imports CSToolPingHelper
Imports CSToolWindowManager

Public Class StandaloneClass
    Public WindowManagerHandler As New WindowManager
    Public ApplicationSettingManager As New ApplicationSettingsManager
    Public ApplicationSettings As New ApplicationSettings
    Public EnvironmentManager As New EnvironmentManager
    Public PingCheckManager As New PingHelper
End Class
