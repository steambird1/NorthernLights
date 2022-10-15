Imports System.Windows.Forms

Public Class General
    Private Sub General_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
        If IsNothing(Me.ActiveMdiChild) Then
            CreatingOne().Opening()
        Else
            Dim s As IDEChildInterface = Me.ActiveMdiChild
            s.Opening()
        End If

    End Sub
End Class
