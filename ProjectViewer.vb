Imports BlueBetter_IDE

Public Class ProjectViewer

    Implements IDEChildInterface

    Public AcceptableStarter As HashSet(Of String) = New HashSet(Of String)({"html", "htm", "xml", "blue", "bp", "txt", "log"})
    Public Property ProjectDirectory As String

    ' All directory included !
    Public Sub RecesuiveUpload(Path As String, CurrentNode As TreeNode)
        For Each i In My.Computer.FileSystem.GetDirectories(Path)
            Dim c As TreeNode = CurrentNode.Nodes.Add(My.Computer.FileSystem.GetName(i))
            c.Tag = ""  ' Not a file!
            RecesuiveUpload(i, c)
        Next
        For Each i In My.Computer.FileSystem.GetFiles(Path)
            If i.IndexOf(SearchContent.Text) >= 0 Then
                Dim c As TreeNode = CurrentNode.Nodes.Add(My.Computer.FileSystem.GetName(i))
                c.Tag = i
            End If
        Next
    End Sub

    Public Sub Upload()
        ' Yield the directory.
        DocumentTree.Nodes.Clear()
        RecesuiveUpload(ProjectDirectory, DocumentTree.Nodes.Add("(Root)"))
    End Sub

    Public Sub Opening() Implements IDEChildInterface.Opening
        ' Must be implemented ...
    End Sub

    Public Sub Creating() Implements IDEChildInterface.Creating
        ' Must be implemented ...
    End Sub

    Public Sub Saving() Implements IDEChildInterface.Saving
        ' Nothing
    End Sub

    Public Sub SavingAs() Implements IDEChildInterface.SavingAs
        ' Nothing
    End Sub

    Public Sub OpenFile(Filename As String) Implements IDEChildInterface.OpenFile
        ' Nothing
    End Sub

    Public Sub RenameFile() Implements IDEChildInterface.RenameFile
        ' Must be implemented ...
    End Sub

    Private Sub ProjectViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Upload()
    End Sub

    Private Sub IDEChildInterface_Closing() Implements IDEChildInterface.Closing

    End Sub

    Private Sub SearchContent_TextChanged(sender As Object, e As EventArgs) Handles SearchContent.TextChanged
        Upload()
    End Sub

    Private Sub DocumentTree_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles DocumentTree.AfterSelect
        Dim ent As String = e.Node.Tag
        If IsNothing(ent) Then
            Exit Sub
        End If
        If Not AcceptableStarter.Contains(ent.Substring(ent.LastIndexOf(".") + 1)) Then
            Exit Sub
        End If
        Try
            Dim g As General = Me.MdiParent
            g.OpenSpecifiedFile(ent)
        Catch ex As Exception
            ' Cannot open :(
        End Try
    End Sub
End Class