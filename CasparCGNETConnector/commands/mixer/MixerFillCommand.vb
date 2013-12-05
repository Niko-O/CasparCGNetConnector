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

Public Class MixerFillCommand
    Inherits AbstractCommand

    Public Sub New()
        MyBase.New("MIXER FILL", "Scales the video stream on the specified layer. The concept is quite simple; it comes from the ancient DVE machines like ADO. Imagine that the screen has a size of 1x1 (not in pixel, but in an abstract measure). Then the coordinates of a full size picture is 0 0 1 1, which means left edge is at coordinate 0, top edge at coordinate 0, width full size => 1, heigh full size => 1. If you want to crop the picture on the left side (for wipe left to right) You set the left edge to full right => 1 and the width to 0. So this give you the start-coordinates of 1 0 0 1. End coordinates of any wipe are allways the full picture 0 0 1 1. With the FILL command it can make sense to have values between 1 and 0, if you want to do a smaller window. If, for instance you want to have a window of half the size of your screen, you set with and height to 0.5. If you want to center it you set left and top edge to 0.25 so you will get the arguments 0.25 0.25 0.5 0.5 ")
        InitParameter()
    End Sub

    Public Sub New(ByVal channel As Integer, ByVal layer As Integer, ByVal x As Single, ByVal y As Single, ByVal xscale As Single, ByVal ysacle As Single, Optional ByVal duration As Integer = 0, Optional ByVal tween As CasparCGUtil.Tweens = CasparCGUtil.Tweens.linear)
        MyBase.New("MIXER FILL", "Scales the video stream on the specified layer. The concept is quite simple; it comes from the ancient DVE machines like ADO. Imagine that the screen has a size of 1x1 (not in pixel, but in an abstract measure). Then the coordinates of a full size picture is 0 0 1 1, which means left edge is at coordinate 0, top edge at coordinate 0, width full size => 1, heigh full size => 1. If you want to crop the picture on the left side (for wipe left to right) You set the left edge to full right => 1 and the width to 0. So this give you the start-coordinates of 1 0 0 1. End coordinates of any wipe are allways the full picture 0 0 1 1. With the FILL command it can make sense to have values between 1 and 0, if you want to do a smaller window. If, for instance you want to have a window of half the size of your screen, you set with and height to 0.5. If you want to center it you set left and top edge to 0.25 so you will get the arguments 0.25 0.25 0.5 0.5 ")
        InitParameter()
        DirectCast(getCommandParameter("channel"), CommandParameter(Of Integer)).setValue(channel)
        If layer > -1 Then DirectCast(getCommandParameter("layer"), CommandParameter(Of Integer)).setValue(layer)
        DirectCast(getCommandParameter("x"), CommandParameter(Of Single)).setValue(x)
        DirectCast(getCommandParameter("y"), CommandParameter(Of Single)).setValue(y)
        DirectCast(getCommandParameter("xscale"), CommandParameter(Of Single)).setValue(xscale)
        DirectCast(getCommandParameter("yscale"), CommandParameter(Of Single)).setValue(ysacle)
        DirectCast(getCommandParameter("duration"), CommandParameter(Of Integer)).setValue(duration)
        DirectCast(getCommandParameter("tween"), CommandParameter(Of CasparCGUtil.Tweens)).setValue(tween)
    End Sub

    Private Sub InitParameter()
        addCommandParameter(New CommandParameter(Of Integer)("channel", "The channel", 1, False))
        addCommandParameter(New CommandParameter(Of Integer)("layer", "The layer", 0, True))
        addCommandParameter(New CommandParameter(Of Single)("x", " The left edge of the new fillSize, 0 = left edge of monitor, 0.5 = middle of monitor, 1.0 = right edge of monitor. Higher and lower values allowed. ", 0, False))
        addCommandParameter(New CommandParameter(Of Single)("y", "The top edge of the new fillSize, 0 = top edge of monitor, 0.5 = middle of monitor, 1.0 = bottom edge of monitor. Higher and lower values allowed.", 0, False))
        addCommandParameter(New CommandParameter(Of Single)("xscale", "The size of the new fillSize, 1 = 1x the screen size, 0.5 = half the screen size. Higher and lower values allowed. ", 1, False))
        addCommandParameter(New CommandParameter(Of Single)("yscale", "The size of the new fillSize, 1 = 1x the screen size, 0.5 = half the screen size. Higher and lower values allowed. ", 1, False))
        addCommandParameter(New CommandParameter(Of Integer)("duration", "The the duration of the tween", 0, True))
        addCommandParameter(New CommandParameter(Of CasparCGUtil.Tweens)("tween", "The the tween to use", CasparCGUtil.Tweens.linear, True))
    End Sub

    Public Overrides Function getCommandString() As String
        Dim cmd As String = "MIXER " & getDestination(getCommandParameter("channel"), getCommandParameter("layer")) & " FILL"

        cmd = cmd & " " & DirectCast(getCommandParameter("x"), CommandParameter(Of Single)).getValue
        cmd = cmd & " " & DirectCast(getCommandParameter("y"), CommandParameter(Of Single)).getValue
        cmd = cmd & " " & DirectCast(getCommandParameter("xscale"), CommandParameter(Of Single)).getValue
        cmd = cmd & " " & DirectCast(getCommandParameter("yscale"), CommandParameter(Of Single)).getValue

        If getCommandParameter("duration").isSet AndAlso getCommandParameter("tween").isSet Then
            cmd = cmd & " " & DirectCast(getCommandParameter("duration"), CommandParameter(Of Integer)).getValue & " " & CasparCGUtil.Tweens.GetName(GetType(CasparCGUtil.Tweens), DirectCast(getCommandParameter("tween"), CommandParameter(Of CasparCGUtil.Tweens)).getValue)
        End If

        Return cmd
    End Function

    Public Overrides Function getRequiredVersion() As Integer()
        Return {1}
    End Function

    Public Overrides Function getMaxAllowedVersion() As Integer()
        Return {Integer.MaxValue}
    End Function
End Class
