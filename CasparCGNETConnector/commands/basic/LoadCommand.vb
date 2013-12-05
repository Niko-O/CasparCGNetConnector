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

Public Class LoadCommand
    Inherits AbstractCommand

    Public Sub New()
        MyBase.New("LOAD", "Loads a media")
        InitParameter()
    End Sub

    Public Sub New(ByVal channel As Integer, Optional ByVal layer As Integer = -1, Optional ByVal media As AbstractCasparCGMedia = Nothing, Optional ByVal looping As Boolean = False, Optional ByVal seek As Long = 0, Optional ByVal length As Long = 0, Optional ByVal transition As CasparCGTransition = Nothing, Optional ByVal filter As String = "")
        MyBase.New("LOAD", "Loads a media")
        InitParameter()
        Init(channel, layer, media.getFullName, looping, seek, length, transition, filter)
    End Sub

    Public Sub New(ByVal channel As Integer, Optional ByVal layer As Integer = -1, Optional ByVal media As String = "", Optional ByVal looping As Boolean = False, Optional ByVal seek As Long = 0, Optional ByVal length As Long = 0, Optional ByVal transition As CasparCGTransition = Nothing, Optional ByVal filter As String = "")
        MyBase.New("LOAD", "Loads a media")
        InitParameter()
        Init(channel, layer, media, looping, seek, length, transition, filter)
    End Sub

    Private Sub Init(ByVal channel As Integer, ByVal layer As Integer, Optional ByVal media As String = "", Optional ByVal looping As Boolean = False, Optional ByVal seek As Long = 0, Optional ByVal length As Long = 0, Optional ByVal transition As CasparCGTransition = Nothing, Optional ByVal filter As String = "")
        If channel > 0 Then DirectCast(getCommandParameter("channel"), CommandParameter(Of Integer)).setValue(channel)
        If layer > -1 Then DirectCast(getCommandParameter("layer"), CommandParameter(Of Integer)).setValue(layer)

        If media.Length > 0 Then
            DirectCast(getCommandParameter("media"), CommandParameter(Of String)).setValue(media)
        End If
        If looping Then
            DirectCast(getCommandParameter("looping"), CommandParameter(Of Boolean)).setValue(looping)
        End If
        If Not IsNothing(transition) Then
            DirectCast(getCommandParameter("transition"), CommandParameter(Of CasparCGTransition)).setValue(transition)
        End If
        If seek > 0 Then
            DirectCast(getCommandParameter("seek"), CommandParameter(Of Integer)).setValue(seek)
        End If
        If length > 0 Then
            DirectCast(getCommandParameter("length"), CommandParameter(Of Integer)).setValue(length)
        End If
        If filter.Length > 0 Then
            DirectCast(getCommandParameter("filter"), CommandParameter(Of String)).setValue(filter)
        End If
    End Sub

    Private Sub InitParameter()
        '' Add all paramters here:
        addCommandParameter(New CommandParameter(Of Integer)("channel", "The channel", 1, False))
        addCommandParameter(New CommandParameter(Of Integer)("layer", "The layer", 0, True))
        addCommandParameter(New CommandParameter(Of String)("media", "The media to play", "", True))
        addCommandParameter(New CommandParameter(Of Boolean)("looping", "Loops the media", False, True))
        addCommandParameter(New CommandParameter(Of Integer)("seek", "The Number of frames to seek before playing", 0, True))
        addCommandParameter(New CommandParameter(Of Integer)("length", "The number of frames to play", 0, True))
        addCommandParameter(New CommandParameter(Of CasparCGTransition)("transition", "The transition to perform at start", Nothing, True))
        addCommandParameter(New CommandParameter(Of String)("filter", "The ffmpeg filter to apply", "", True))
    End Sub

    Public Overrides Function getCommandString() As String
        Dim cmd As String = "LOAD " & getDestination(getCommandParameter("channel"), getCommandParameter("layer"))

        If getCommandParameter("media").isSet Then
            cmd = cmd & " '" & DirectCast(getCommandParameter("media"), CommandParameter(Of String)).getValue() & "'"
        End If
        If getCommandParameter("looping").isSet AndAlso DirectCast(getCommandParameter("looping"), CommandParameter(Of Boolean)).getValue() Then
            cmd = cmd & " LOOP"
        End If
        If getCommandParameter("transition").isSet Then
            cmd = cmd & " " & DirectCast(getCommandParameter("transition"), CommandParameter(Of CasparCGTransition)).getValue().toString
        End If
        If getCommandParameter("seek").isSet Then
            cmd = cmd & " SEEK " & DirectCast(getCommandParameter("seek"), CommandParameter(Of Integer)).getValue()
        End If
        If getCommandParameter("length").isSet Then
            cmd = cmd & " LENGTH " & DirectCast(getCommandParameter("length"), CommandParameter(Of Integer)).getValue()
        End If
        If getCommandParameter("filter").isSet Then
            cmd = cmd & " FILTER '" & DirectCast(getCommandParameter("filter"), CommandParameter(Of String)).getValue() & "'"
        End If

        Return escape(cmd)
    End Function

    Public Overrides Function getRequiredVersion() As Integer()
        Return {1}
    End Function

    Public Overrides Function getMaxAllowedVersion() As Integer()
        Return {Integer.MaxValue}
    End Function
End Class
