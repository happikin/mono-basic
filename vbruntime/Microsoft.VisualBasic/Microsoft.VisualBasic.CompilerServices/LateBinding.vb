'
' LateBinding.vb
'
' Author:
'   Boris Kirzner (borisk@mainsoft.com)
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
Imports System.Reflection
Imports System.Globalization

Namespace Microsoft.VisualBasic.CompilerServices

    Public Class LateBinding

        Private Shared ReadOnly Property LBinder() As Microsoft.VisualBasic.CompilerServices.LateBinder
            Get
                Return New LateBinder
            End Get
        End Property

        Public Shared Sub LateCall(ByVal o As Object, ByVal objType As System.Type, ByVal name As String, ByVal args() As Object, ByVal paramnames() As String, ByVal CopyBack() As Boolean)
            LateGet(o, objType, name, args, paramnames, CopyBack)
        End Sub

        Public Shared Sub LateIndexSet(ByVal o As Object, ByVal args() As Object, ByVal paramnames() As String)
            Dim realType As System.Type = o.GetType()
            Dim flags As BindingFlags
            If realType.IsArray Then
                flags = BindingFlags.IgnoreCase Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.InvokeMethod
                realType.InvokeMember("Set", flags, Nothing, o, args, Nothing)
            Else
                Dim defaultMembers() As MemberInfo = realType.GetDefaultMembers()
                flags = BindingFlags.IgnoreCase Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.SetProperty
                realType.InvokeMember(defaultMembers(0).Name, flags, Nothing, o, args, Nothing)
            End If
        End Sub

        Public Shared Sub LateIndexSetComplex(ByVal o As Object, ByVal args() As Object, ByVal paramnames() As String, ByVal OptimisticSet As Boolean, ByVal RValueBase As Boolean)
            'FIXME
            LateIndexSet(o, args, paramnames)
        End Sub

        Public Shared Sub LateSet(ByVal o As Object, ByVal objType As System.Type, ByVal name As String, ByVal args() As Object, ByVal paramnames() As String)
            Dim realType As System.Type = objType
            If realType Is Nothing Then
                realType = o.GetType()
            End If

            Dim flags As BindingFlags
            Try 'first try property set
                flags = BindingFlags.FlattenHierarchy Or _
                                           BindingFlags.SetField Or _
                                           BindingFlags.SetProperty Or _
                                           BindingFlags.IgnoreCase Or _
                                           BindingFlags.Instance Or _
                                           BindingFlags.NonPublic Or _
                                           BindingFlags.OptionalParamBinding Or _
                                           BindingFlags.Public Or _
                                           BindingFlags.Static

                realType.InvokeMember(name, flags, LBinder, o, args, Nothing, Nothing, paramnames)
                Return
            Catch e As MissingMemberException
            End Try

            'if failed, try method call
            flags = BindingFlags.FlattenHierarchy Or _
                    BindingFlags.GetField Or _
                    BindingFlags.GetProperty Or _
                    BindingFlags.IgnoreCase Or _
                    BindingFlags.Instance Or _
                    BindingFlags.InvokeMethod Or _
                    BindingFlags.NonPublic Or _
                    BindingFlags.OptionalParamBinding Or _
                    BindingFlags.Public Or _
                    BindingFlags.Static

            realType.InvokeMember(name, flags, LBinder, o, args, Nothing, Nothing, paramnames)
        End Sub

        Public Shared Sub LateSetComplex(ByVal o As Object, ByVal objType As System.Type, ByVal name As String, ByVal args() As Object, ByVal paramnames() As String, ByVal OptimisticSet As Boolean, ByVal RValueBase As Boolean)
            'FIXME
            LateSet(o, objType, name, args, paramnames)
        End Sub

        Public Shared Function LateGet(ByVal o As Object, ByVal objType As System.Type, ByVal name As String, ByVal args() As Object, ByVal paramnames() As String, ByVal CopyBack() As Boolean) As Object          
            Dim realType As System.Type = objType
            If realType Is Nothing Then
                realType = o.GetType()
            End If
         
            Dim flags As BindingFlags = BindingFlags.FlattenHierarchy Or _
                                        BindingFlags.GetField Or _
                                        BindingFlags.GetProperty Or _
                                        BindingFlags.IgnoreCase Or _
                                        BindingFlags.Instance Or _
                                        BindingFlags.InvokeMethod Or _
                                        BindingFlags.NonPublic Or _
                                        BindingFlags.OptionalParamBinding Or _
                                        BindingFlags.Public Or _
                                        BindingFlags.Static

            Try
                Dim result As Object = realType.InvokeMember(name, flags, LBinder, o, args, Nothing, CultureInfo.CurrentCulture, paramnames)

                Return result
            Catch e As MissingMethodException
                'FIXME: should the InvokeMember always call a binder instead of throwing an exceptions
                Throw New MissingMemberException
            End Try
        End Function

        Public Shared Function LateIndexGet(ByVal o As Object, ByVal args() As Object, ByVal paramnames() As String) As Object
            Dim realType As System.Type = o.GetType()
            Dim flags As BindingFlags
            If realType.IsArray Then
                flags = BindingFlags.IgnoreCase Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.InvokeMethod
                Return realType.InvokeMember("Get", flags, Nothing, o, args, Nothing)
            Else
                Dim defaultMembers() As MemberInfo = realType.GetDefaultMembers()
                flags = BindingFlags.IgnoreCase Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.GetProperty
                Return realType.InvokeMember(defaultMembers(0).Name, flags, Nothing, o, args, Nothing)
            End If
        End Function

    End Class
End Namespace