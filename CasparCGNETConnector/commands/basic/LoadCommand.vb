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

    'Public Sub New(ByVal channel As Integer, Optional ByVal layer As Integer = -1, Optional ByVal media As ICasparCGMedia = Nothing, Optional ByVal looping As Boolean = False, Optional ByVal seek As Long = 0, Optional ByVal length As Long = 0, Optional ByVal transition As CasparCGTransition = Nothing, Optional ByVal filter As String = "")
    '    MyBase.New("LOAD", "Loads a media")
    '    InitParameter()
    '    Init(channel, layer, media.FullName, looping, seek, length, transition, filter)
    'End Sub

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
        If transition IsNot Nothing Then
            DirectCast(getCommandParameter("transition"), CommandParameter(Of CasparCGTransition)).setValue(transition)
        End If
        If seek > 0 Then
            DirectCast(getCommandParameter("seek"), CommandParameter(Of Integer)).setValue(CInt(seek))
        End If
        If length > 0 Then
            DirectCast(getCommandParameter("length"), CommandParameter(Of Integer)).setValue(CInt(length))
        End If
        If filter.Length > 0 Then
            DirectCast(getCommandParameter("filter"), CommandParameter(Of String)).setValue(filter)
        End If
    End Sub

    Private Sub InitParameter()
        '' Add all paramters here:
        addCommandParameter(New ChannelParameter)
        addCommandParameter(New LayerParameter)
        addCommandParameter(New CommandParameter(Of String)("media", "The media to play", "", False))
        addCommandParameter(New CommandParameter(Of Boolean)("looping", "Loops the media", False, True))
        addCommandParameter(New CommandParameter(Of Integer)("seek", "The Number of frames to seek before playing", 0, True))
        addCommandParameter(New CommandParameter(Of Integer)("length", "The number of frames to play", 0, True))
        addCommandParameter(New CommandParameter(Of CasparCGTransition)("transition", "The transition to perform at start", Nothing, True))
        addCommandParameter(New CommandParameter(Of String)("filter", "The ffmpeg filter to apply", "", True))
    End Sub

    Public Overrides Function getCommandString() As String
        checkParameter()
        Dim cmd As String = "LOAD " & getDestination() & " '" & getMedia() & "'"

        If getCommandParameter("looping").isSet AndAlso getLooping() Then
            cmd = cmd & " LOOP"
        End If
        If getCommandParameter("transition").isSet Then
            cmd = cmd & " " & getTransition.toString
        End If
        If getCommandParameter("seek").isSet Then
            cmd = cmd & " SEEK " & getSeek()
        End If
        If getCommandParameter("length").isSet Then
            cmd = cmd & " LENGTH " & getLength()
        End If
        If getCommandParameter("filter").isSet Then
            cmd = cmd & " FILTER '" & getFilter() & "'"
        End If

        Return escape(cmd)
    End Function

    Public Sub setChannel(ByVal channel As Integer)
        If channel > 0 Then
            DirectCast(getCommandParameter("channel"), CommandParameter(Of Integer)).setValue(channel)
        Else
            Throw New ArgumentException("Illegal argument channel=" & channel & ". The parameter channel has to be greater than 0.")
        End If
    End Sub

    Public Function getChannel() As Integer
        Dim param As CommandParameter(Of Integer) = DirectCast(getCommandParameter("channel"), CommandParameter(Of Integer))
        If param IsNot Nothing And param.isSet Then
            Return param.getValue
        Else
            Return param.getDefault
        End If
    End Function

    Public Sub setLayer(ByVal layer As Integer)
        If layer < 0 Then
            Throw New ArgumentException("Illegal argument layer=" & layer & ". The parameter layer has to be greater or equal than 0.")
        Else
            DirectCast(getCommandParameter("layer"), CommandParameter(Of Integer)).setValue(layer)
        End If
    End Sub

    Public Function getLayer() As Integer
        Dim param As CommandParameter(Of Integer) = DirectCast(getCommandParameter("layer"), CommandParameter(Of Integer))
        If param IsNot Nothing And param.isSet Then
            Return param.getValue
        Else
            Return param.getDefault
        End If
    End Function

    Public Sub setMedia(ByVal media As String)
        If media IsNot Nothing Then
            DirectCast(getCommandParameter("media"), CommandParameter(Of String)).setValue(media)
        Else
            DirectCast(getCommandParameter("media"), CommandParameter(Of String)).setValue("")
        End If
    End Sub

    Public Sub setMedia(ByVal media As ICasparCGMedia)
        If media IsNot Nothing Then
            DirectCast(getCommandParameter("media"), CommandParameter(Of String)).setValue(media.FullName)
        Else
            DirectCast(getCommandParameter("media"), CommandParameter(Of String)).setValue("")
        End If
    End Sub

    Public Function getMedia() As String
        Dim param As CommandParameter(Of String) = DirectCast(getCommandParameter("media"), CommandParameter(Of String))
        If param IsNot Nothing And param.isSet Then
            Return param.getValue
        Else
            Return param.getDefault
        End If
    End Function

    Public Sub setLooping(ByVal looping As Boolean)
        DirectCast(getCommandParameter("looping"), CommandParameter(Of Boolean)).setValue(looping)
    End Sub

    Public Function getLooping() As Boolean
        Dim param As CommandParameter(Of Boolean) = DirectCast(getCommandParameter("looping"), CommandParameter(Of Boolean))
        If param IsNot Nothing And param.isSet Then
            Return param.getValue
        Else
            Return param.getDefault
        End If
    End Function

    Public Sub setTransition(ByRef transition As CasparCGTransition)
        DirectCast(getCommandParameter("transition"), CommandParameter(Of CasparCGTransition)).setValue(transition)
    End Sub

    Public Function getTransition() As CasparCGTransition
        Dim param As CommandParameter(Of CasparCGTransition) = DirectCast(getCommandParameter("transition"), CommandParameter(Of CasparCGTransition))
        If param IsNot Nothing And param.isSet Then
            Return param.getValue
        Else
            Return param.getDefault
        End If
    End Function

    Public Sub setSeek(ByVal seek As Integer)
        If seek >= 0 Then
            DirectCast(getCommandParameter("seek"), CommandParameter(Of Integer)).setValue(seek)
        Else
            Throw New ArgumentException("Illegal argument. Seek must be positiv.")
        End If
    End Sub

    Public Function getSeek() As Integer
        Dim param As CommandParameter(Of Integer) = DirectCast(getCommandParameter("seek"), CommandParameter(Of Integer))
        If param IsNot Nothing And param.isSet Then
            Return param.getValue
        Else
            Return param.getDefault
        End If
    End Function

    Public Sub setLength(ByVal length As Integer)
        If length >= 0 Then
            DirectCast(getCommandParameter("length"), CommandParameter(Of Integer)).setValue(length)
        Else
            Throw New ArgumentException("Illegal argument. Length must be positiv.")
        End If
    End Sub

    Public Function getLength() As Integer
        Dim param As CommandParameter(Of Integer) = DirectCast(getCommandParameter("length"), CommandParameter(Of Integer))
        If param IsNot Nothing And param.isSet Then
            Return param.getValue
        Else
            Return param.getDefault
        End If
    End Function

    Public Sub setFilter(ByVal filter As String)
        If filter IsNot Nothing Then
            DirectCast(getCommandParameter("filter"), CommandParameter(Of String)).setValue(filter)
        Else
            DirectCast(getCommandParameter("filter"), CommandParameter(Of String)).setValue("")
        End If
    End Sub

    Public Function getFilter() As String
        Dim param As CommandParameter(Of String) = DirectCast(getCommandParameter("filter"), CommandParameter(Of String))
        If param IsNot Nothing And param.isSet Then
            Return param.getValue
        Else
            Return param.getDefault
        End If
    End Function

    Public Overrides Function getRequiredVersion() As Integer()
        Return {1}
    End Function

    Public Overrides Function getMaxAllowedVersion() As Integer()
        Return {Integer.MaxValue}
    End Function
End Class
