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

Public Class CgUpdateCommand
    Inherits AbstractCommand

    Public Sub New()
        MyBase.New("CG Update", "Sends new data to the template on specified layer")
        InitParameter()
    End Sub

    Public Sub New(ByVal channel As Integer, ByVal layer As Integer, ByVal flashlayer As Integer, ByVal data As String)
        MyBase.New("CG Update", "Sends new data to the template on specified layer")
        InitParameter()
        setChannel(channel)
        If layer > -1 Then setLayer(layer)
        setFlashlayer(flashlayer)
        setData(data)
    End Sub

    Public Sub New(ByVal channel As Integer, ByVal layer As Integer, ByVal flashlayer As Integer, ByVal data As CasparCGTemplateData)
        MyBase.New("CG Update", "Sends new data to the template on specified layer")
        InitParameter()
        setChannel(channel)
        If layer > -1 Then setLayer(layer)
        setFlashlayer(flashlayer)
        setData(data.getDataString)
    End Sub

    Private Sub InitParameter()
        addCommandParameter(New ChannelParameter)
        addCommandParameter(New LayerParameter)
        addCommandParameter(New CommandParameter(Of Integer)("flashlayer", "The flashlayer", 0, False))
        addCommandParameter(New CommandParameter(Of String)("data", "The xml data string", "", False))
    End Sub

    Public Overrides Function getCommandString() As String
        checkParameter()
        Return escape("CG " & getDestination() & " UPDATE " & getFlashlayer() & " '" & getData() & "'")
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

    Public Sub setFlashlayer(ByVal flashlayer As Integer)
        If flashlayer < 0 Then
            Throw New ArgumentException("Illegal argument flashlayer=" & flashlayer & ". The parameter flashlayer has to be greater or equal than 0.")
        Else
            DirectCast(getCommandParameter("flashlayer"), CommandParameter(Of Integer)).setValue(flashlayer)
        End If
    End Sub

    Public Function getFlashlayer() As Integer
        Dim param As CommandParameter(Of Integer) = DirectCast(getCommandParameter("flashlayer"), CommandParameter(Of Integer))
        If param IsNot Nothing And param.isSet Then
            Return param.getValue
        Else
            Return param.getDefault
        End If
    End Function

    Public Sub setData(ByVal data As String)
        If data Is Nothing Then
            DirectCast(getCommandParameter("data"), CommandParameter(Of String)).setValue("")
        Else
            DirectCast(getCommandParameter("data"), CommandParameter(Of String)).setValue(data)
        End If
    End Sub

    Public Sub setData(ByVal data As CasparCGTemplateData)
        If data Is Nothing Then
            DirectCast(getCommandParameter("data"), CommandParameter(Of String)).setValue("")
        Else
            DirectCast(getCommandParameter("data"), CommandParameter(Of String)).setValue(data.getDataString)
        End If
    End Sub

    Public Function getData() As String
        Dim param As CommandParameter(Of String) = DirectCast(getCommandParameter("data"), CommandParameter(Of String))
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
