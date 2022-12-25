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

    Private Sub InheritanceUpdate(inheritw As MainIDE.BObject, ByRef root As TreeNode, Optional layer As Integer = 0)
        If layer > 16 Then
            root.Nodes.Add("<Too much inherits!>")
        End If
        For Each i In inheritw.ClassFunction
            If i.ObjectType = "Function" Then
                Dim desc As String = i.ObjectType & mytab & i.ObjectName & mytab
                If i.ObjectType = "Function" Then
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
                root.Nodes(root.Nodes.Count - 1).Tag = i.LinePosition
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
                If i.ObjectType = "Function" Then
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
                    .Tag = i.LinePosition
                    .ForeColor = Color.Black        ' No special thing for functions
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
                                    End If
                                    .Tag = j.LinePosition
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
        If e.Node.Tag > 0 Then
            Try
                CurrentParent.CodeData.Select(CurrentParent.CodeData.GetFirstCharIndexFromLine(e.Node.Tag - 1), 0)
                CurrentParent.CodeData.Select()
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class