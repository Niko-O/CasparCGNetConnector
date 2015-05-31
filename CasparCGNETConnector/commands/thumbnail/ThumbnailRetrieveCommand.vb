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

Public Class ThumbnailRetrieveCommand
    Inherits AbstractCommand

    Public Sub New()
        MyBase.New("THUMBNAIL RETRIEVE", "Requests the base64 encoded thumbnail for a specific media file")
        InitParameter()
    End Sub

    Public Sub New(ByVal media As String)
        MyBase.New("THUMBNAIL RETRIEVE", "Requests the base64 encoded thumbnail for a specific media file")
        InitParameter()
        If media IsNot Nothing AndAlso media.Length > 0 Then setMedia(media)
    End Sub

    Public Sub New(ByVal media As ICasparCGMedia)
        MyBase.New("THUMBNAIL RETRIEVE", "Requests the base64 encoded thumbnail for a specific media file")
        InitParameter()
        If media IsNot Nothing Then setMedia(media)
    End Sub

    Private Sub InitParameter()
        addCommandParameter(New CommandParameter(Of String)("media", "The media file", "", False))
    End Sub

    Public Overrides Function getCommandString() As String
        checkParameter()
        Return escape("THUMBNAIL RETRIEVE '" & getMedia() & "'")
    End Function

    Public Sub setMedia(ByVal media As String)
        If media Is Nothing Then
            DirectCast(getCommandParameter("media"), CommandParameter(Of String)).setValue("")
        Else
            DirectCast(getCommandParameter("media"), CommandParameter(Of String)).setValue(media)
        End If
    End Sub

    Public Sub setMedia(ByVal media As ICasparCGMedia)
        If media Is Nothing Then
            DirectCast(getCommandParameter("media"), CommandParameter(Of String)).setValue("")
        Else
            DirectCast(getCommandParameter("media"), CommandParameter(Of String)).setValue(media.FullName)
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

    Public Overrides Function getRequiredVersion() As Integer()
        Return {2, 0, 4}
    End Function

    Public Overrides Function getMaxAllowedVersion() As Integer()
        Return {Integer.MaxValue}
    End Function
End Class
