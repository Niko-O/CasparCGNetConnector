﻿'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'' Author: Christopher Diekkamp
'' Email: christopher@development.diekkamp.de
'' GitHub: https://github.com/mcdikki
'' 
'' This software is licensed under the 
'' GNU General Public License Version 3 (GPLv3).
'' See http://www.gnu.org/licenses/gpl-3.0-standalone.html 
'' for a copy of the license.
''
'' You are free to copy, use and modify this software.
'' Please let me know of any changes and improvements you made to it.
''
'' Thank you!
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Class InfoCommand
    Inherits AbstractCommand

    Public Sub New()
        MyBase.New("INFO", "Requests informations about a channel or layer")
        InitParameter()
    End Sub

    Public Sub New(Optional ByVal channel As Integer = -1, Optional ByVal layer As Integer = -1, Optional ByVal onlyBackground As Boolean = False, Optional ByVal onlyForeground As Boolean = False, Optional ByVal delay As Boolean = False)
        MyBase.New("INFO", "Requests informations about a channel or layer")
        InitParameter()
        Init(channel, layer, onlyBackground, onlyForeground, delay)
    End Sub

    Private Sub Init(ByVal channel As Integer, ByVal layer As Integer, Optional ByVal onlyBackground As Boolean = False, Optional ByVal onlyForeground As Boolean = False, Optional ByVal delay As Boolean = False)
        If channel > 0 Then DirectCast(getCommandParameter("channel"), CommandParameter(Of Integer)).setValue(channel)
        If layer > -1 Then DirectCast(getCommandParameter("layer"), CommandParameter(Of Integer)).setValue(layer)

        If onlyBackground Then
            DirectCast(getCommandParameter("only background"), CommandParameter(Of Boolean)).setValue(onlyBackground)
        End If
        If onlyForeground Then
            DirectCast(getCommandParameter("only foreground"), CommandParameter(Of Boolean)).setValue(onlyForeground)
        End If
        If delay Then
            DirectCast(getCommandParameter("delay"), CommandParameter(Of Boolean)).setValue(delay)
        End If
    End Sub

    Private Sub InitParameter()
        addCommandParameter(New CommandParameter(Of Integer)("channel", "The channel", 1, True))
        addCommandParameter(New CommandParameter(Of Integer)("layer", "The layer", 0, True))
        addCommandParameter(New CommandParameter(Of Boolean)("only background", "Only show info of background", False, True))
        addCommandParameter(New CommandParameter(Of Boolean)("only foreground", "Only show info of foreground", False, True))
        addCommandParameter(New CommandParameter(Of Boolean)("delay", "shows the delay of a channel", False, True))
    End Sub

    Public Overrides Function getCommandString() As String
        Dim cmd As String = "INFO"
        If getCommandParameter("channel").isSet Then
            cmd = cmd & " " & getDestination(getCommandParameter("channel"), getCommandParameter("layer"))
            If getCommandParameter("layer").isSet Then
                If getCommandParameter("only background").isSet AndAlso DirectCast(getCommandParameter("only background"), CommandParameter(Of Boolean)).getValue Then
                    cmd = cmd & " B"
                ElseIf getCommandParameter("only foreground").isSet AndAlso DirectCast(getCommandParameter("only foreground"), CommandParameter(Of Boolean)).getValue Then
                    cmd = cmd & " F"
                End If
            ElseIf getCommandParameter("delay").isSet AndAlso DirectCast(getCommandParameter("delay"), CommandParameter(Of Boolean)).getValue Then
                cmd = cmd & " DELAY"
            End If
        End If
        Return cmd
    End Function

    Public Overrides Function getRequiredVersion() As Integer()
        Return {1}
    End Function

    Public Overrides Function getMaxAllowedVersion() As Integer()
        'Return {2, 0, 4}
        Return {Integer.MaxValue}
    End Function
End Class
