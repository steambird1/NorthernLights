﻿Imports System.IO
Imports System.Threading
Imports NorthernLights

Public Class ProjectViewer

    Implements IDEChildInterface

    Public AcceptableStarter As HashSet(Of String) = New HashSet(Of String)({"html", "htm", "xml", "blue", "bp", "txt", "log", "nlproj", "js"})
    ' Also, bold for all directories.
    Public Highlighted As Dictionary(Of String, Color) = New Dictionary(Of String, Color)

    Private JustMove As Boolean = False
    Private Cutting As Boolean = False
    Private _CurrentFile As String
    Private Property CurrentFile As String
        Get
            Return _CurrentFile
        End Get
        Set(value As String)
            _CurrentFile = value
            Dim en As Boolean = Trim(_CurrentFile) <> ""
            PasteToolStripMenuItem.Enabled = en
            CancelToolStripMenuItem.Enabled = en
        End Set
    End Property

    Private Sub LoadConstants()
        Highlighted.Add("html", Color.Red)
        Highlighted.Add("htm", Color.Red)
        Highlighted.Add("xml", Color.Red)
        Highlighted.Add("blue", Color.Blue)
        Highlighted.Add("bp", Color.Blue)
        Highlighted.Add("js", Color.DarkOrange)
        Highlighted.Add("txt", Color.Green)
        Highlighted.Add("log", Color.Green)
        Highlighted.Add("nlproj", Color.Magenta)
    End Sub

    Private SuppressUpdater As Boolean = False
    Private HasUpdated As Boolean = False
    Private _ProjectDirectory As String
    Public Property ProjectDirectory As String
        Get
            Return _ProjectDirectory
        End Get
        Set(value As String)
            _ProjectDirectory = value
            FSWatcher.Path = value
            FSWatcher.EnableRaisingEvents = True
        End Set
    End Property

    ''' <summary>
    ''' Find or delete specififed tree node.
    ''' </summary>
    ''' <param name="Paths">Path of file (remove current project component)</param>
    ''' <param name="DeleteAfterFound">Symbol if the function will delete after found the node. (Always returns Nothing as it's true)</param>
    ''' <returns></returns>
    Private Function FindTreeNode(Paths As String, Optional DeleteAfterFound As Boolean = False) As TreeNode
        Dim Path As String = Paths.Substring(Paths.IndexOf(ProjectDirectory) + ProjectDirectory.Length)
        Dim spl As String() = Split(Path, "\")
        Dim sumi As String = ""
        Dim curoot As TreeNode = DocumentTree.Nodes(0)
        For i = 0 To spl.Count() - 1
            Dim cur As String = spl(i)
            If Trim(cur) = "" Then
                Continue For
            End If
            If Trim(sumi) = "" Then
                sumi = cur
            Else
                sumi &= "\" & cur
            End If
            Dim found As Boolean = False
            For Each j In curoot.Nodes
                Dim tn As TreeNode = j
                If tn.Text = cur Then
                    If DeleteAfterFound AndAlso (i = spl.Count() - 1) Then
                        curoot.Nodes.Remove(tn)
                        Return Nothing
                    End If
                    curoot = tn
                    found = True
                    GoTo AfterNodeYielder
                End If
            Next
            If Not found Then
                If DeleteAfterFound AndAlso (i = spl.Count() - 1) Then
                    Return Nothing
                End If
                curoot = curoot.Nodes.Add(cur)
                curoot.Tag = ProjectDirectory & "\" & sumi
                If My.Computer.FileSystem.DirectoryExists(sumi) Then
                    curoot.ImageIndex = 0
                Else
                    curoot.ImageIndex = 1
                    ' Query if here's its color
                    Dim ge As String = GetExtension(cur)
                    If Environments.Contains(cur) Then
                        curoot.ForeColor = Color.Purple
                    ElseIf Highlighted.ContainsKey(ge) Then
                        curoot.ForeColor = Highlighted(ge)
                    Else
                        curoot.ForeColor = Color.Black
                    End If
                End If
                curoot.Text = cur '??
            End If
AfterNodeYielder: 'Must be found!
        Next
        Return curoot
    End Function


    Public ReadOnly Property HaveOwnCreator As Boolean Implements IDEChildInterface.HaveOwnCreator
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property IsViewer As Boolean Implements IDEChildInterface.IsViewer
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Load all directories and files under a directory.
    ''' </summary>
    ''' <param name="Path">The directory.</param>
    ''' <param name="CurrentNode">The node provided for the directory.</param>
    Public Sub RecesuiveUpload(Path As String, CurrentNode As TreeNode)
        For Each i In My.Computer.FileSystem.GetDirectories(Path)
            ' Don't add if it already exists ...
            Dim exists As Boolean = False
            Dim c As TreeNode
            For Each j In CurrentNode.Nodes
                Dim tj As TreeNode = j
                If tj.Tag = i Then
                    c = tj
                    exists = True
                    GoTo AfterFoundA
                End If
            Next
            c = CurrentNode.Nodes.Add(My.Computer.FileSystem.GetName(i))
            c.ImageIndex = 0
            c.Tag = i
AfterFoundA: RecesuiveUpload(i, c)
        Next
        For Each i In My.Computer.FileSystem.GetFiles(Path)
            If i.IndexOf(SearchContent.Text) >= 0 Then
                Dim gn As String = My.Computer.FileSystem.GetName(i)
                Dim ge As String = GetExtension(gn)
                Dim c As TreeNode
                Dim exists As Boolean = False
                For Each j In CurrentNode.Nodes
                    Dim tj As TreeNode = j
                    If tj.Tag = i Then
                        c = tj
                        exists = True
                        GoTo AfterFoundB
                    End If
                Next
                c = CurrentNode.Nodes.Add(gn)
AfterFoundB:    c.ImageIndex = 1
                If Environments.Contains(gn) Then
                    c.ForeColor = Color.Purple
                ElseIf Highlighted.ContainsKey(ge) Then
                    c.ForeColor = Highlighted(ge)
                Else
                    c.ForeColor = Color.Black
                End If
                c.Tag = i
            End If
        Next
    End Sub

    Public Sub Upload()
        ' Yield the directory.
        DocumentTree.Nodes.Clear()
        Dim dt As TreeNode = DocumentTree.Nodes.Add("(Root)")
        dt.ImageIndex = 0
        dt.Tag = ProjectDirectory
        RecesuiveUpload(ProjectDirectory, dt)
    End Sub

    Public Sub Opening() Implements IDEChildInterface.Opening
        ' Must be implemented ...
    End Sub

    Private Function FindFor() As String
        Dim frees As Integer = 0
        Dim nd As TreeNode = DocumentTree.SelectedNode
        While My.Computer.FileSystem.DirectoryExists(nd.Tag & "\New " & frees) OrElse My.Computer.FileSystem.FileExists(nd.Tag & "\New " & frees)
            frees += 1
        End While
        Dim okp As String = nd.Tag & "\New " & frees
        Return okp
    End Function

    Public Sub Creating() Implements IDEChildInterface.Creating
        Try
            Dim okp As String = FindFor()
            SuppressUpdater = True
            My.Computer.FileSystem.OpenTextFileWriter(okp, True).Close()
            Dim ft As TreeNode = DocumentTree.SelectedNode.Nodes.Add(My.Computer.FileSystem.GetName(okp))
            ft.ImageIndex = 1
            ft.Tag = okp
            SuppressUpdater = False
            ft.BeginEdit()
        Catch ex As Exception
            MsgBox("Cannot create file here!", MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Sub Saving() Implements IDEChildInterface.Saving
        ' Nothing
    End Sub

    Public Sub SavingAs() Implements IDEChildInterface.SavingAs
        ' Nothing
    End Sub

    Public Sub OpenFile(Filename As String) Implements IDEChildInterface.OpenFile
        Dim ent = Filename
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

    Public Sub RenameFile() Implements IDEChildInterface.RenameFile
        Dim nd As TreeNode = DocumentTree.SelectedNode
        If Not nd.IsEditing Then
            nd.BeginEdit()
        End If
    End Sub

    Private Sub ProjectViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadConstants()
        Upload()
    End Sub

    Private Sub IDEChildInterface_Closing() Implements IDEChildInterface.Closing
        Me.Close()
    End Sub

    Private Sub SearchContent_TextChanged(sender As Object, e As EventArgs) Handles SearchContent.TextChanged
        Upload()
    End Sub

    Private Sub DocumentTree_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles DocumentTree.NodeMouseDoubleClick
        Dim ent As String = e.Node.Tag
        OpenFile(ent)
    End Sub


    Private Sub FSWatcher_Created(sender As Object, e As FileSystemEventArgs) Handles FSWatcher.Created
        Dim CurrentTreeNode = FindTreeNode(e.FullPath)
        If My.Computer.FileSystem.DirectoryExists(e.FullPath) Then
            CurrentTreeNode.ImageIndex = 0
            RecesuiveUpload(e.FullPath, CurrentTreeNode)
        End If
        HasUpdated = True ' For file and directory creater
    End Sub

    Private Sub FSWatcher_Deleted(sender As Object, e As FileSystemEventArgs) Handles FSWatcher.Deleted
        FindTreeNode(e.FullPath, True)
    End Sub

    Private Sub FSWatcher_Renamed(sender As Object, e As RenamedEventArgs) Handles FSWatcher.Renamed
        If Not SuppressUpdater Then
            Dim tag As TreeNode = FindTreeNode(e.OldFullPath)
            tag.Tag = e.FullPath
            tag.Text = My.Computer.FileSystem.GetName(e.FullPath)
            Dim ge As String = GetExtension(tag.Text)
            If Environments.Contains(tag.Text) Then
                tag.ForeColor = Color.Purple
            ElseIf Highlighted.ContainsKey(ge) Then
                tag.ForeColor = Highlighted(ge)
            Else
                tag.ForeColor = Color.Black
            End If
        End If
    End Sub

    Private Sub DocumentTree_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles DocumentTree.AfterSelect
        ' For better view
        DocumentTree.SelectedNode.SelectedImageIndex = DocumentTree.SelectedNode.ImageIndex
    End Sub

    Private Sub DocumentTree_AfterLabelEdit(sender As Object, e As NodeLabelEditEventArgs) Handles DocumentTree.AfterLabelEdit
        Dim tn As TreeNode = e.Node
        If tn.Text = "(Root)" OrElse e.Label = "(Root)" Then
            e.CancelEdit = True
            Exit Sub
        End If
        If tn.Text = e.Label Then
            Exit Sub
        End If
        Try
            'SuppressUpdater = True
            If My.Computer.FileSystem.DirectoryExists(tn.Tag) Then
                ' Directory
                My.Computer.FileSystem.RenameDirectory(tn.Tag, e.Label)
            Else
                ' File
                My.Computer.FileSystem.RenameFile(tn.Tag, e.Label)
            End If
            ' Will be used automaticly!
            'tn.Text = e.Label
        Catch ex As Exception
            e.CancelEdit = True
            MsgBox("Cannot use this value!", MsgBoxStyle.Critical, "Error")
        End Try
        Thread.Yield()
        ' Leave a tag for file system watcher
        e.CancelEdit = True
    End Sub

    ' By this way you still have chance to undo (from Recycle Bin).
    Public Sub Deleting() Implements IDEChildInterface.Deleting
        Dim cf As String = DocumentTree.SelectedNode.Tag
        If MsgBox("Are you sure to delete this file or directory?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
            If Environments.Contains(My.Computer.FileSystem.GetName(cf)) Then
                If MsgBox("This is a server executable file! Are you sure to delete it?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
            If My.Computer.FileSystem.DirectoryExists(cf) Then
                My.Computer.FileSystem.DeleteDirectory(cf, FileIO.DeleteDirectoryOption.DeleteAllContents)
            Else
                My.Computer.FileSystem.DeleteFile(cf)
            End If
        End If
        ' Will be automaticly updated
    End Sub

    Private Sub CreateDirectoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateDirectoryToolStripMenuItem.Click
        Try
            Dim okp As String = FindFor()
            SuppressUpdater = True
            My.Computer.FileSystem.CreateDirectory(okp)
            Dim ft As TreeNode = DocumentTree.SelectedNode.Nodes.Add(My.Computer.FileSystem.GetName(okp))
            ft.ImageIndex = 0
            ft.Tag = okp
            Thread.Yield()
            SuppressUpdater = False
            ft.BeginEdit()
        Catch ex As Exception
            MsgBox("Cannot create directory here!", MsgBoxStyle.Critical)
        End Try

    End Sub

    Public Function ConfirmMasterClose() As Boolean Implements IDEChildInterface.ConfirmMasterClose
        Return False
    End Function

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        Dim tn As TreeNode = DocumentTree.SelectedNode
        If Not IsNothing(tn) Then
            Cutting = True
            CurrentFile = tn.Tag
        End If
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        Dim tn As TreeNode = DocumentTree.SelectedNode
        If Not IsNothing(tn) Then
            Cutting = False
            CurrentFile = tn.Tag
        End If
    End Sub

    Private Sub CancelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelToolStripMenuItem.Click
        Cutting = False
        CurrentFile = ""
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        Try
            Dim tn As TreeNode = DocumentTree.SelectedNode
            If Not IsNothing(tn) Then
                With My.Computer.FileSystem
                    Dim gn As String = .GetName(CurrentFile)
                    Dim target As String = tn.Tag & "\" & gn
                    If Cutting Then
                        If .DirectoryExists(CurrentFile) Then
                            .MoveDirectory(CurrentFile, target)
                            CurrentFile = target
                        Else
                            .MoveFile(CurrentFile, tn.Tag & "\" & gn)
                            CurrentFile = target
                        End If
                        Cutting = False ' Always copy then
                    Else
                        If .DirectoryExists(CurrentFile) Then
                            .CopyDirectory(CurrentFile, target)
                            CurrentFile = target
                        Else
                            .CopyFile(CurrentFile, target)
                            CurrentFile = target
                        End If
                    End If
                End With
            End If
        Catch ex As Exception
            MsgBox("Error: cannot move or copy this file or directory!", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub CopyHere_Click(sender As Object, e As EventArgs) Handles CopyHere.Click
        Dim tn As TreeNode = DocumentTree.SelectedNode
        If Not IsNothing(tn) Then
            Dim MyFile As String = tn.Tag
            Try
                With My.Computer.FileSystem
                    Dim Finder As Integer = 1
                    Dim Prefixer As String = .GetParentPath(MyFile) & "\" & .GetName(MyFile) & "_Copy "
                    Dim Exts As String = GetExtension(MyFile)
                    While .FileExists(Prefixer & Finder & "." & Exts) OrElse .DirectoryExists(Prefixer & Finder & "." & Exts)
                        Finder += 1
                    End While
                    If .FileExists(MyFile) Then
                        .CopyFile(MyFile, Prefixer & Finder & "." & Exts)
                    ElseIf .DirectoryExists(myfile) Then
                        .CopyDirectory(MyFile, Prefixer & Finder & "." & Exts)
                    End If

                End With
            Catch ex As Exception
                MsgBox("Error: cannot copy this file or directory!", MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    Public Sub OpeningSpecified(Filename As String) Implements IDEChildInterface.OpeningSpecified

    End Sub
End Class