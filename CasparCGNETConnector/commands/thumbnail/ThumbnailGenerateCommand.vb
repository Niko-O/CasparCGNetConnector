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

Public Class ThumbnailGenerateCommand
    Inherits AbstractCommand

    Public Sub New()
        MyBase.New("THUMBNAIL GENERATE", "Requests the server to (re-)generate the thumbnails for a specific media file")
    End Sub

    Public Sub New(ByVal media As String)
        MyBase.New("THUMBNAIL GENERATE", "Requests the server to (re-)generate the thumbnails for a specific media file")
        InitParameter()
        DirectCast(getCommandParameter("media"), CommandParameter(Of String)).setValue(media)
    End Sub

    Public Sub New(ByVal media As AbstractCasparCGMedia)
        MyBase.New("THUMBNAIL GENERATE", "Requests the server to (re-)generate the thumbnails for a specific media file")
        InitParameter()
        DirectCast(getCommandParameter("media"), CommandParameter(Of String)).setValue(media.getFullName)
    End Sub

    Private Sub InitParameter()
        addCommandParameter(New CommandParameter(Of String)("media", "The media file", "", False))
    End Sub

    Public Overrides Function getCommandString() As String
        Dim cmd = "THUMBNAIL GENERATE '" & DirectCast(getCommandParameter("media"), CommandParameter(Of String)).getValue & "'"
        Return escape(cmd)
    End Function

    Public Overrides Function getRequiredVersion() As Integer()
        Return {2, 0, 4}
    End Function

    Public Overrides Function getMaxAllowedVersion() As Integer()
        Return {Integer.MaxValue}
    End Function
End Class
