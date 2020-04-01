Imports System.Text
Imports System.IO

Public Class Tokenizer
    Dim letter As String = "abcdefghijklmnopqrstuvwxyz"

    Public Function tokenize(ByVal source As String) As List(Of String)
        Dim l As New List(Of String)
        Dim line As Integer = 0
        Using Sr As New StringReader(source)
            While Sr.Peek >= 0
                Dim str As String = Sr.ReadLine
                Dim i As Integer = 0
                While i < str.Length
                    If Char.IsLetter(str(i)) Or str(i) = "_"c Then
                        'Identifier
                        Dim sb As New StringBuilder
                        sb.Append(str(i))
                        While i + 1 < str.Length AndAlso (Char.IsLetterOrDigit(str(i + 1)) Or str(i + 1) = "_"c)
                            i += 1
                            sb.Append(str(i))
                        End While
                        l.Add(sb.ToString)
                    ElseIf str(i) = "'"c Then
                        'Comment
                        Dim sb As New StringBuilder
                        While i < str.Length
                            sb.Append(str(i))
                            i += 1
                        End While
                        l.Add(sb.ToString)
                    ElseIf str(i) = """"c Then
                        'String Literal
                        Dim sb As New StringBuilder
                        sb.Append(str(i))
                        i += 1
                        While i < str.Length
                            sb.Append(str(i))
                            If str(i) = """"c Then
                                If i + 1 < str.Length AndAlso str(i + 1) <> """"c Then
                                    l.Add(sb.ToString)
                                    Exit While
                                Else
                                    'sb(sb.Length - 1) = "\"
                                    sb.Append(str(i))
                                    i += 1
                                End If
                            End If
                            i += 1
                        End While
                    ElseIf str(i) = "<"c Or str(i) = ">"c Or str(i) = "="c Or str(i) = "&"c Or str(i) = "+"c Or str(i) = "-"c Or str(i) = "*"c Or str(i) = "/"c Or str(i) = "^"c Then
                        'operators and Hex
                        Dim sb As New StringBuilder
                        sb.Append(str(i))
                        If i + 1 < str.Length Then
                            If str(i) = "<"c And (str(i + 1) = "<"c Or str(i + 1) = ">"c Or str(i + 1) = "="c) Then
                                i += 1
                                sb.Append(str(i))
                            ElseIf str(i) = ">"c And (str(i + 1) = ">"c Or str(i + 1) = "="c) Then
                                i += 1
                                sb.Append(str(i))
                            ElseIf (str(i) = "+"c Or str(i) = "-"c Or str(i) = "*"c Or str(i) = "*"c Or str(i) = "="c Or str(i) = "&"c) And str(i + 1) = "="c Then
                                i += 1
                                sb.Append(str(i))
                            ElseIf str(i) = "&"c And str(i + 1).ToString.ToUpper = "H" Then
                                'Hex Numbers
                                i += 1
                                sb.Append(str(i))
                                Dim hex As String = "0123456789ABCDEF."
                                While i + 1 < str.Length AndAlso hex.Contains(str(i + 1).ToString.ToUpper)
                                    i += 1
                                    sb.Append(str(i))
                                End While
                            ElseIf str(i) = "&"c And str(i + 1).ToString.ToUpper = "O" Then
                                'Oct Numbers
                                i += 1
                                sb.Append(str(i))
                                Dim hex As String = "01234567."
                                While i + 1 < str.Length AndAlso hex.Contains(str(i + 1).ToString.ToUpper)
                                    i += 1
                                    sb.Append(str(i))
                                End While
                            End If
                        End If
                        l.Add(sb.ToString.ToUpper)
                    ElseIf str(i) = "("c Or str(i) = ")"c Or str(i) = "["c Or str(i) = "]"c Or str(i) = "{"c Or str(i) = "}"c Then
                        'brackets
                        l.Add(str(i))
                    ElseIf str(i) = "," Or str(i) = ":" Then
                        'seperators
                        l.Add(str(i))
                    ElseIf str(i) = "."c Or Char.IsDigit(str(i)) Then
                        'number and dot operator 
                        Dim sb As New StringBuilder
                        sb.Append(str(i))
                        Dim suffix As String = "FDL"
                        Dim e As Integer = 0
                        Dim pt As Integer = 0
                        If str(i) = "."c Then
                            pt += 1
                        End If
                        If i + 1 < str.Length AndAlso (Char.IsDigit(str(i + 1)) Or str(i + 1) = "."c) Then
                            While i + 1 < str.Length AndAlso (suffix.Contains(str(i + 1).ToString.ToUpper) Or str(i + 1) = "."c Or str(i + 1).ToString.ToUpper = "E" Or Char.IsDigit(str(i + 1)))
                                Dim cc As String = str(i + 1)
                                If str(i + 1) = "."c Then
                                    pt += 1
                                    If pt > 1 Then
                                        Exit While
                                    End If
                                End If
                                If str(i + 1).ToString.ToUpper = "E" Then
                                    e += 1
                                    If e > 1 Then
                                        Exit While
                                    End If
                                    If i + 2 < str.Length AndAlso (str(i + 2) = "+" Or str(i + 2) = "-") Then
                                        cc &= str(i + 2)
                                        i += 1
                                    End If
                                End If
                                i += 1
                                sb.Append(cc)
                                If i < str.Length AndAlso suffix.Contains(str(i).ToString.ToUpper) Then
                                    Exit While
                                End If

                            End While
                        End If
                        l.Add(sb.ToString)
                    ElseIf str(i) = " "c Or str(i) = vbTab Then

                    Else
                        MsgBox("Invalid charater: " & str(i))
                    End If
                    i += 1
                End While
                line += 1
            End While
        End Using
        Return l
    End Function
End Class
