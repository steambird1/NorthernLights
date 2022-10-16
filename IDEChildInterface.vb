Public Interface IDEChildInterface
    Sub Opening()
    Sub Creating()
    Sub Saving()
    Sub SavingAs()
    Sub Closing()
    Sub OpenFile(Filename As String)
    Sub RenameFile()
    Sub Deleting()
    ReadOnly Property HaveOwnCreator As Boolean
End Interface
