
''' <summary>
''' This class holds util functions and Enumns for CasparCGNetConnector 
''' </summary>
Public Class CasparCGUtil
    Public Enum Directions
        LEFT = 1
        RIGHT = 0
    End Enum

    Public Enum Tweens
        linear = 0
        easenone
        easeinquad
        easeoutquad
        easeinoutquad
        easeoutinquad
        easeincubic
        easeoutcubic
        easeinoutcubic
        easeoutincubic
        easeinquart
        easeoutquart
        easeinoutquart
        easeoutinquart
        easeinquint
        easeoutquint
        easeinoutquint
        easeoutinquint
        easeinsine
        easeoutsine
        easeinoutsine
        easeoutinsine
        easeinexpo
        easeoutexpo
        easeinoutexpo
        easeoutinexpo
        easeincirc
        easeoutcirc
        easeinoutcirc
        easeoutincirc
        easeinelastic
        easeoutelastic
        easeinoutelastic
        easeoutinelastic
        easeinback
        easeoutback
        easeinoutback
        easeoutintback
        easeoutbounce
        easeinbounce
        easeinoutbounce
        easeoutinbounce
    End Enum

    Public Enum Transitions
        CUT = 0
        MIX = 1
        PUSH = 2
        WIPE = 3
        SLIDE = 4
    End Enum

    Public Shared Function repairBase64(ByRef base64String As String) As String
        'Roundtripping through the System.Convert.To/FromBase64String-Functions is enough to normalize an otherwise valid String.
        
        Return Convert.ToBase64String(Convert.FromBase64String(base64String))

        'Not sure what's going on down there though.

        ''Replace whitespaces
        'base64String = base64String.Replace(" ", "")
        'base64String = base64String.Replace(Microsoft.VisualBasic.Constants.vbTab, "")
        'base64String = base64String.Replace(Environment.NewLine, "")
        'base64String = base64String.Replace(Microsoft.VisualBasic.Constants.vbCr, "")
        'base64String = base64String.Replace(Microsoft.VisualBasic.Constants.vbLf, "")

        ''Fill or remove fillspaces if length mod 4 != 0
        'If base64String.Length Mod 4 <> 0 AndAlso base64String.Length > 3 Then
        '    If base64String.Contains("==") Then
        '        base64String = base64String.Remove(base64String.IndexOf("=="), 1)
        '    ElseIf base64String.Length Mod 4 = 3 Then
        '        base64String = base64String & "="
        '    ElseIf base64String.Length Mod 4 = 2 Then
        '        base64String = base64String.Substring(0, base64String.Length - 1) & "AA="
        '    ElseIf base64String.Length Mod 4 = 1 AndAlso base64String.Length > 6 Then
        '        ' This is only the last way to solve the problem. 
        '        ' We will lose some pixel of the image like that
        '        base64String = base64String.Substring(0, base64String.Length - 6) & "="
        '    Else
        '        logger.warn("CasparCGUtil.repairBase64: Unable to repair the base64 string.")
        '        Return ""
        '    End If
        'End If

        'Return base64String
    End Function



End Class
