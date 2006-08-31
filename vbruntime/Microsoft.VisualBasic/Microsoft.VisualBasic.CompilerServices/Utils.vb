'
' Utils.vb
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
Imports System.Runtime.InteropServices

Namespace Microsoft.VisualBasic.CompilerServices
    Public Class Utils

        Private Sub New()
            'Nobody should see constructor
        End Sub

        Public Shared Function CopyArray(ByVal arySrc As System.Array, ByVal aryDest As System.Array) As System.Array

            If arySrc Is Nothing Then
#If TRACE Then
                Console.WriteLine("TRACE:Utils.CopyArray:arySrc is Nothing")
#End If
                Return aryDest
            End If

            Dim CopyLength As Long
            CopyLength = arySrc.Length

            If CopyLength = 0 Then
#If TRACE Then
                Console.WriteLine("TRACE:Utils.CopyArray:arySrc.Length:" + arySrc.Length.ToString())
#End If
                Return aryDest
            End If

            If CopyLength > aryDest.Length Then
                CopyLength = aryDest.Length
            End If

            Array.Copy(arySrc, 0, aryDest, 0, CopyLength)

            Return aryDest
        End Function
        Public Shared Function MethodToString(ByVal Method As System.Reflection.MethodBase) As String
            Throw New NotImplementedException
        End Function
        Public Shared Function SetCultureInfo(ByVal Culture As System.Globalization.CultureInfo) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Sub ThrowException(ByVal hr As Integer)
            Throw New NotImplementedException
        End Sub
#If NET_2_0 Then
        Public Shared Function GetResourceString(ByVal ResourceKey As String, ByVal ParamArray Args As String()) As String
            Throw New NotImplementedException
        End Function
#End If
    End Class

End Namespace