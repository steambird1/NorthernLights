Public Interface IDEChildInterface
    Sub Opening()
    Sub Creating()
    Sub Saving()
    Sub SavingAs()
    Sub Closing()
    Sub OpenFile(Filename As String)
    Sub RenameFile()
End Interface
