Imports System.Windows.Forms

Public Class General

    Private ActiveRequired, ProjectActiveRequired As List(Of ToolStripMenuItem)
    Private Environments As List(Of String) = New List(Of String)({"BlueBetter4.exe", "bmain.blue", "algo.blue", "WebHeader.blue", "BluePage.blue", "BluePage.exe", "VBWeb.exe"})

    Private _CurrentObject As String = ""
    Public Property CurrentProject As String
        Get
            Return _CurrentObject
        End Get
        Set(value As String)
            _CurrentObject = value
            If Trim(value) = "" Then
                Me.Text = "NorthernLights Web IDE"
            Else
                Me.Text = "NorthernLights Web IDE - " & value
            End If
        End Set
    End Property

    Public ReadOnly Property HaveChildren As Boolean
        Get
            Return Me.MdiChildren.Count() > 0
        End Get
    End Property

    Private Sub General_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Constants
        ActiveRequired = New List(Of ToolStripMenuItem)({ToolStripMenuItem2, ToolStripMenuItem3, ToolStripMenuItem7, ToolStripMenuItem4})
        ProjectActiveRequired = New List(Of ToolStripMenuItem)  ' Nothing is provided !
        ' End
        Dim alert As String = ""
        For Each i In Environments
            If Not My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & i) Then
                alert &= i & vbCrLf
            End If
        Next
        If Trim(alert) <> "" Then
            MsgBox("Warning: Following files are not found in the application directory:" & vbCrLf & alert & vbCrLf & vbCrLf & "Without these files, the editor can be runned, but you may not able to run BlueBetter program or website.", MsgBoxStyle.Exclamation, "Warning")
        End If
    End Sub

    Private Function CreatingOne() As MainIDE
        Dim s As New MainIDE
        s.MdiParent = Me
        s.Creating()
        s.Show()
        Return s
    End Function

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        CreatingOne()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click

        CreatingOne().Opening()

    End Sub

    Public Sub OpenSpecifiedFile(Filename As String)
        Try
            If Not CurrentWebProcess.HasExited Then
                ' Select...
                If ViewSpecified.Checked Then
                    ' Getting the resolve
                    Dim suffix As String = Filename.Substring(Filename.IndexOf(CurrentProject) + CurrentProject.Length)
                    System.Diagnostics.Process.Start("http://localhost" & suffix.Replace("\"c, "/"c))
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            ' The process is not running.
        End Try
        CreatingOne().OpenFile(Filename)
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        If Not IsNothing(Me.ActiveMdiChild) Then
            Dim s As IDEChildInterface = Me.ActiveMdiChild
            s.Saving()
        End If
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        If HaveChildren Then
            Dim s As IDEChildInterface = Me.ActiveMdiChild
            s.SavingAs()
        End If
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        If HaveChildren Then
            Dim s As IDEChildInterface = Me.ActiveMdiChild
            s.Closing()
        End If
    End Sub

    Private Sub NewProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewProjectToolStripMenuItem.Click
        If fbd.ShowDialog() = DialogResult.OK Then
            If My.Computer.FileSystem.DirectoryExists(fbd.SelectedPath) Then
                CurrentProject = fbd.SelectedPath
                ProjectViews(sender, e)
            End If
        End If
    End Sub

    Private Sub ProjectViews(Optional sender As Object = Nothing, Optional e As EventArgs = Nothing) Handles ToolStripMenuItem6.Click
        Dim p As New ProjectViewer
        p.ProjectDirectory = CurrentProject
        p.Upload()
        p.MdiParent = Me
        p.Show()
    End Sub

    Private Sub FileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileToolStripMenuItem.Click, FileToolStripMenuItem.MouseMove
        Dim oks As Boolean = HaveChildren
        For Each i In ActiveRequired
            Dim ob As ToolStripMenuItem = i
            ob.Enabled = oks
        Next
        Dim poks As Boolean = Trim(CurrentProject） <> ""
        For Each i In ProjectActiveRequired
            Dim ob As ToolStripMenuItem = i
            ob.Enabled = poks
        Next
    End Sub

    Public Property CurrentWebProcess As Process = New Process

    Private Sub RunWebsiteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RunWebsiteToolStripMenuItem.Click
        ' TODO: Add runner
        Try
            For Each i In Environments
                My.Computer.FileSystem.CopyFile(Application.StartupPath & "\" & i, CurrentProject & "\" & i, True)
            Next
            With CurrentWebProcess.StartInfo
                .FileName = CurrentProject & "\VBWeb.exe"
                .UseShellExecute = True
                .RedirectStandardError = False
                .RedirectStandardInput = False
                .RedirectStandardOutput = False
                .CreateNoWindow = False
                .WorkingDirectory = CurrentProject
                .WindowStyle = ProcessWindowStyle.Normal
            End With
            CurrentWebProcess.Start()
        Catch ex As OperationCanceledException
            ' Do nothing
        Catch ex As Exception
            MsgBox("Cannot run website: " & ex.Message, MsgBoxStyle.Critical, "Fatal error")
        End Try

    End Sub

    Private Sub ProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProjectToolStripMenuItem.Click, ProjectToolStripMenuItem.MouseMove
        Dim avail As Boolean = Trim(CurrentProject) <> ""
        ToolStripMenuItem6.Enabled = avail
        RunWebsiteToolStripMenuItem.Enabled = avail
    End Sub


    Private Sub ToolStripMenuItem7_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem7.Click
        If HaveChildren Then
            Dim s As IDEChildInterface = Me.ActiveMdiChild
            s.RenameFile()
        End If
    End Sub

End Class
