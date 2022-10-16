Module Utility
    Public Environments As List(Of String) = New List(Of String)({"BlueBetter4.exe", "bmain.blue", "algo.blue", "WebHeader.blue", "BluePage.blue", "BluePage.exe", "VBWeb.exe"})
    Public Function GetExtension(Filename As String) As String
        Dim ext As String = Filename
        Try
            ext = ext.Substring(ext.LastIndexOf("."c) + 1)
        Catch ex As ArgumentOutOfRangeException
            ext = ""
        End Try
        Return ext
    End Function
End Module
