Public Class SelectRecents

    Public Property RecentList As RecentFilesList
    Private RecentReferrer As List(Of String) = New List(Of String)
    Private _Result As String = ""
    Public ReadOnly Property Result As String
        Get
            Return _Result
        End Get
    End Property

    Private Sub SelectRecents_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RecentFiles.Items.Clear()
        Dim Rel = RecentList.RecentQueue
        For i = Rel.Count - 1 To 0 Step -1
            RecentFiles.Items.Add(Rel(i))
            RecentReferrer.Add(Rel(i))
        Next
    End Sub

    Private Sub RecentFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RecentFiles.SelectedIndexChanged
        OpenNow.Enabled = RecentFiles.SelectedIndex >= 0
    End Sub

    Private Sub OpenNow_Click(sender As Object, e As EventArgs) Handles OpenNow.Click
        Dim Res = RecentReferrer(RecentFiles.SelectedIndex)
        _Result = Res
        Me.Close()
    End Sub
End Class