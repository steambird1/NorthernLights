Public Class Intelli

    Public mytab As String = "    "
    Public Property ObjectInfo As List(Of MainIDE.BObject)
    Public Property CurrentParent As MainIDE

    Private Function GenerateAttributeDescription(ByRef obj As MainIDE.BObject)
        If IsNothing(obj.Attributes) Then
            Return ""
        End If
        Return String.Join(",", obj.Attributes)
    End Function

    Public Structure TreeTagData
        Public LineID As Integer
        Public Name, Description As String

        Public Sub New(CopyFrom As MainIDE.BObject)
            Me.Name = CopyFrom.ObjectName
            Me.LineID = CopyFrom.LinePosition
            Me.Description = CopyFrom.ObjectDescription
        End Sub
    End Structure

    Private Sub InheritanceUpdate(inheritw As MainIDE.BObject, ByRef root As TreeNode, Optional layer As Integer = 0)
        If layer > 16 Then
            root.Nodes.Add("<Too much inherits!>")
        End If
        For Each i In inheritw.ClassFunction
            If i.ObjectType = "Function" OrElse i.ObjectType.IndexOf("Property") = 0 Then
                Dim desc As String = i.ObjectType & mytab & i.ObjectName & mytab
                If i.ObjectType = "Function" OrElse i.ObjectType.IndexOf("Property") = 0 Then
                    'desc &= i.FunctionParameter
                    Dim spls As String() = Split(i.FunctionParameter)
                    For Each j In spls
                        desc &= j & ","
                    Next
                    If desc.Length > 0 AndAlso desc(desc.Length - 1) = ","c Then
                        desc = desc.Remove(desc.Length - 1)
                    End If
                End If
                root.Nodes.Add(desc & mytab & GenerateAttributeDescription(i))
                With root.Nodes(root.Nodes.Count - 1)
                    '.Tag = i.LinePosition
                    .Tag = New TreeTagData(i)
                End With
                If i.ObjectType.IndexOf("Property") = 0 Then
                    root.Nodes(root.Nodes.Count - 1).ForeColor = Color.DarkMagenta
                End If
            ElseIf i.ObjectType = "Variable" Then
                Dim desc As String = i.ObjectType & mytab & i.ObjectName & mytab
                root.Nodes.Add(desc & mytab & GenerateAttributeDescription(i))
                With root.Nodes(root.Nodes.Count - 1)
                    '.Tag = i.LinePosition
                    .Tag = New TreeTagData(i)
                    .ForeColor = Color.Blue
                End With
            ElseIf i.ObjectType = "Inheritance" Then
                root.Nodes.Add("Inheritance" & mytab & i.ObjectName)
                With root
                    .Nodes(.Nodes.Count - 1).ForeColor = Color.Red
                End With
                InheritanceUpdate(i, root.Nodes(root.Nodes.Count - 1))
            End If
        Next
    End Sub

    Private Sub TreeUpdate(sender As Object, e As EventArgs) Handles SearchMask.TextChanged
        ElemViewer.Nodes.Clear()
        For Each i In ObjectInfo
            If i.ObjectType = "Bad" Then
                Continue For
            End If
            If i.ObjectName.Contains(SearchMask.Text) Then
                Dim desc As String = i.ObjectType & mytab & i.ObjectName & mytab
                If i.ObjectType = "Function" OrElse i.ObjectType.IndexOf("Property") = 0 Then
                    'desc &= i.FunctionParameter
                    Dim spls As String() = Split(i.FunctionParameter)
                    For Each j In spls
                        desc &= j & ","
                    Next
                    If desc.Length > 0 AndAlso desc(desc.Length - 1) = ","c Then
                        desc = desc.Remove(desc.Length - 1)
                    End If
                End If
                ElemViewer.Nodes.Add(desc & mytab & GenerateAttributeDescription(i))
                With ElemViewer.Nodes(ElemViewer.Nodes.Count - 1)
                    '.Tag = i.LinePosition
                    .Tag = New TreeTagData(i)
                    .ForeColor = Color.Black        ' No special thing for functions
                    If i.ObjectType.IndexOf("Property") = 0 Then
                        .ForeColor = Color.DarkMagenta
                    ElseIf i.ObjectType = "Variable" Then
                        .ForeColor = Color.Blue
                    End If
                End With
                If i.ObjectType = "Class" Then
                    ' Add sub nodes...
                    ElemViewer.Nodes(ElemViewer.Nodes.Count - 1).ForeColor = Color.Green
                    For Each j In i.ClassFunction
                        If j.ObjectType = "Inheritance" Then
                            With ElemViewer.Nodes(ElemViewer.Nodes.Count - 1)
                                .Nodes.Add("Inheritance" & mytab & j.ObjectName)
                                .Nodes(.Nodes.Count - 1).ForeColor = Color.Red
                                InheritanceUpdate(j, .Nodes(.Nodes.Count - 1))
                            End With

                        Else
                            Dim mesc As String = "Member " & j.ObjectType & " " & mytab & j.ObjectName & mytab
                            Dim spls As String() = Split(j.FunctionParameter)
                            For Each k In spls
                                mesc &= k & ","
                            Next
                            If mesc.Length > 0 AndAlso mesc(mesc.Length - 1) = ","c Then
                                mesc = mesc.Remove(mesc.Length - 1)
                            End If
                            With ElemViewer.Nodes(ElemViewer.Nodes.Count - 1)
                                .Nodes.Add(mesc & mytab & GenerateAttributeDescription(j))
                                With .Nodes(.Nodes.Count - 1)
                                    .ForeColor = Color.Black
                                    If j.ObjectType = "Variable" Then
                                        .ForeColor = Color.Blue
                                    ElseIf j.ObjectType.IndexOf("Property") = 0 Then
                                        .ForeColor = Color.DarkMagenta
                                    End If
                                    '.Tag = j.LinePosition
                                    .Tag = New TreeTagData(j)
                                End With
                            End With
                        End If
                    Next
                End If
            End If
        Next
    End Sub

    Private Sub Intelli_Load(sender As Object, e As EventArgs) Handles Me.Load
        TreeUpdate(Nothing, New EventArgs)
    End Sub

    Private Sub ElemViewer_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles ElemViewer.AfterSelect

        Try
            DetName.Text = e.Node.Tag.Name
            DetDisplayer.Text = e.Node.Tag.Description
            If e.Node.Tag.LineID > 0 Then
                CurrentParent.CodeData.Select(CurrentParent.CodeData.GetFirstCharIndexFromLine(e.Node.Tag.LineID - 1), 0)
                CurrentParent.CodeData.Select()
            End If
        Catch ex As NullReferenceException
            DetName.Text = ""
            DetDisplayer.Text = ""
        Catch ex As Exception

        End Try
    End Sub
End Class