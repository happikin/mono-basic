'
' CharArrayType.vb
'
' Author:
'   Mizrahi Rafael (rafim@mainsoft.com)
'

'
' Copyright (C) 2002-2006 Mainsoft Corporation.
' Copyright (C) 2004-2006 Novell, Inc (http://www.novell.com)
'
' Permission is hereby granted, free of charge, to any person obtaining
' a copy of this software and associated documentation files (the
' "Software"), to deal in the Software without restriction, including
' without limitation the rights to use, copy, modify, merge, publish,
' distribute, sublicense, and/or sell copies of the Software, and to
' permit persons to whom the Software is furnished to do so, subject to
' the following conditions:
' 
' The above copyright notice and this permission notice shall be
' included in all copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
' EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
' MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
' NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
' LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
' OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
' WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Imports System
Imports Microsoft.VisualBasic
Namespace Microsoft.VisualBasic.CompilerServices
    Public Class CharArrayType
        Private Sub New()
            'Nobody should see constructor
        End Sub
        Public Shared Function FromObject(ByVal Value As Object) As Char()
            Select Case (Type.GetTypeCode(Value.GetType()))
                Case TypeCode.String
                    Return CharArrayType.FromString(Value.ToString())
                Case Else
                    'TODO: add tests and check this case.
                    Throw New NotImplementedException
            End Select
        End Function
        Public Shared Function FromString(ByVal Value As String) As Char()
            Return Value.ToCharArray()
        End Function
    End Class
End Namespace
