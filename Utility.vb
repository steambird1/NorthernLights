Module Utility
    Public Environments As List(Of String) = New List(Of String)({"BlueBetter4.exe", "bmain.blue", "algo.blue", "WebHeader.blue", "BluePage.blue", "BluePage.exe", "VBWeb.exe", "Postback.js"})
    Public ErrorHandlers As List(Of String) = New List(Of String)({"500.html", "404.html"})

    ''' <summary>
    ''' Get extension of filename, no '.' is provided.
    ''' </summary>
    ''' <param name="Filename">File name or path.</param>
    ''' <returns>Extension name of file.</returns>
    Public Function GetExtension(Filename As String) As String
        Dim ext As String = Filename
        Try
            ext = ext.Substring(ext.LastIndexOf("."c) + 1)
        Catch ex As ArgumentOutOfRangeException
            ext = ""
        End Try
        Return ext
    End Function
    Public Sub ShowVersion(Executable As String)
        Dim p As New Process
        p.StartInfo.FileName = Application.StartupPath & Executable
        p.StartInfo.Arguments = "--version"
        p.StartInfo.UseShellExecute = False
        p.StartInfo.RedirectStandardInput = True
        p.StartInfo.RedirectStandardOutput = True
        p.StartInfo.RedirectStandardError = True
        p.StartInfo.CreateNoWindow = True
        p.Start()
        p.WaitForExit()
        Dim res As String = p.StandardOutput.ReadToEnd()
        p.Close()
        MsgBox(res, MsgBoxStyle.Information, "About")
    End Sub
End Module
