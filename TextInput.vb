Public Class TextInput

    Private _CompletedInput As Boolean = False

    ''' <summary>
    ''' Return whether the input is completed.
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public ReadOnly Property CompletedInput As Boolean
        Get
            Return _CompletedInput
        End Get
    End Property

    Public WriteOnly Property NoCancel As Boolean
        Set(value As Boolean)
            CancelButton.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Return what user has inputted.
    ''' </summary>
    ''' <returns>String</returns>
    Public Property Data As String
        Get
            Return TextData.Text
        End Get
        Set(value As String)
            TextData.Text = value
        End Set
    End Property

    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
        Me.Close()
    End Sub

    Private Sub ConfirmButton_Click(sender As Object, e As EventArgs) Handles ConfirmButton.Click
        _CompletedInput = True
        Me.Close()
    End Sub

End Class