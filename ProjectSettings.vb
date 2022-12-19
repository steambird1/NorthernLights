Imports System.ComponentModel
Imports System.IO

Public Class ProjectSettings
    Implements IDEChildInterface

    Public Property CurrentFileName As String
    Private _Modified As Boolean = False
    Public Property Modified As Boolean
        Get
            Return _Modified
        End Get
        Set(value As Boolean)
            If value Then
                Me.Text = "Project Settings *"
            Else
                Me.Text = "Project Settings"
            End If
            _Modified = value
        End Set
    End Property

    Public ReadOnly Property HaveOwnCreator As Boolean Implements IDEChildInterface.HaveOwnCreator
        Get
            Return False
        End Get
    End Property

    Private ReadOnly Property SelfParent As General
        Get
            Return Me.MdiParent
        End Get
    End Property

    Public ReadOnly Property IsViewer As Boolean Implements IDEChildInterface.IsViewer
        Get
            Return True
        End Get
    End Property

    Public Sub Opening() Implements IDEChildInterface.Opening

    End Sub

    Public Sub Creating() Implements IDEChildInterface.Creating

    End Sub

    Public Sub SaveFileTo(Path As String)
        Dim Writing As StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(Path, False)
        For Each i In SelfParent.CurrentProjectSettings
            Writing.Write(i.Key & "=" & i.Value)
            Writing.WriteLine()
        Next
        Writing.Close()
    End Sub

    Public Sub Saving() Implements IDEChildInterface.Saving
        SelfParent.CurrentProjectSettings("Port") = PortData.Text
        SelfParent.CurrentProjectSettings("Page500") = Error500Path.Text
        SelfParent.CurrentProjectSettings("Page404") = Error404Path.Text
        SelfParent.CurrentProjectSettings("Extension") = ExtensionsData.Text
        SaveFileTo(CurrentFileName)
        Modified = False
    End Sub

    ''' <summary>
    ''' Check whether window is able to close safely.
    ''' </summary>
    Public Function Checking() As Boolean
        If Modified Then
            Dim r = MsgBox("Current file " & CurrentFileName & " is not saved. Save?", MsgBoxStyle.YesNoCancel, "Prompt")
            Select Case r
                Case MsgBoxResult.Yes
                    Me.Saving()
                Case MsgBoxResult.Cancel
                    Return False
            End Select
            Return True
        Else
            Return True
        End If
    End Function

    Public Sub SavingAs() Implements IDEChildInterface.SavingAs
        If SaveFiler.ShowDialog() = DialogResult.OK Then
            SaveFileTo(SaveFiler.FileName)
        End If
    End Sub

    Public Sub OpenFile(Filename As String) Implements IDEChildInterface.OpenFile

    End Sub

    Public Sub RenameFile() Implements IDEChildInterface.RenameFile

    End Sub

    Public Sub Deleting() Implements IDEChildInterface.Deleting

    End Sub

    Private Sub ProjectSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CurrentFileName = SelfParent.CurrentProject & "\project.nlproj"
        PortData.Text = SelfParent.CurrentProjectSettings("Port")
        Error500Path.Text = SelfParent.CurrentProjectSettings("Page500")
        Error404Path.Text = SelfParent.CurrentProjectSettings("Page404")
        ExtensionsData.Text = SelfParent.CurrentProjectSettings("Extension")
    End Sub

    Private Sub IDEChildInterface_Closing() Implements IDEChildInterface.Closing
        Me.Close()
    End Sub

    Public Function ConfirmMasterClose() As Boolean Implements IDEChildInterface.ConfirmMasterClose
        Return Not Checking()
    End Function

    Private Sub ProjectSettings_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = Not Checking()
    End Sub

    Private Sub PortData_TextChanged(sender As Object, e As EventArgs) Handles PortData.TextChanged
        Modified = True
    End Sub

    Private Sub Error500Path_TextChanged(sender As Object, e As EventArgs) Handles Error500Path.TextChanged
        Modified = True
    End Sub

    Private Sub Error404Path_TextChanged(sender As Object, e As EventArgs) Handles Error404Path.TextChanged
        Modified = True
    End Sub

    Private Sub ExtensionsData_TextChanged(sender As Object, e As EventArgs) Handles ExtensionsData.TextChanged
        Modified = True
    End Sub
End Class