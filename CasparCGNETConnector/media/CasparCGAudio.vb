﻿<Serializable()> _
Public Class CasparCGAudio
    Inherits CasparCGMedia

    Public Sub New(ByVal name As String)
        MyBase.New(name)
    End Sub

    Public Sub New(ByVal name As String, ByVal xml As String)
        MyBase.New(name, xml)
    End Sub

    Public Overrides Function clone() As CasparCGMedia
        Dim media As New CasparCGAudio(getFullName)
        For Each info As String In getInfos.Keys
            media.addInfo(info, getInfo(info))
        Next
        Return media
    End Function

    Public Overrides Function getMediaType() As CasparCGMedia.MediaType
        Return MediaType.AUDIO
    End Function

End Class