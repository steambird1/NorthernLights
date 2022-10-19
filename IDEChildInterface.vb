Public Interface IDEChildInterface
    Sub Opening()
    Sub Creating()
    Sub Saving()
    Sub SavingAs()
    Sub Closing()
    Sub OpenFile(Filename As String)
    Sub RenameFile()
    Sub Deleting()
    Function ConfirmMasterClose() As Boolean            ' Return True to cancel
    ReadOnly Property HaveOwnCreator As Boolean
    'Property SuppressConfirmer As Boolean
End Interface
