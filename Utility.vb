Imports System.IO

Public Module Utility
    Public Environments As List(Of String) = New List(Of String)({"BlueBetter4.exe", "bmain.blue", "algo.blue", "WebHeader.blue", "document.blue", "BluePage.blue", "BluePage.exe", "VBWeb.exe", "Postback.js", "math.blue"})
    Public ErrorHandlers As List(Of String) = New List(Of String)({"500.html", "404.html", "403.html"})

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


    ''' <summary>
    ''' A class that manages 'Recent files'.
    ''' </summary>
    Public Class RecentFilesList

        Private BindingRecorder As String = ""
        Public Shared ReadOnly Property CountOfRecents As Integer = 10
        Private _RecentQueue As List(Of String) = New List(Of String)
        Public Property RecentQueue As List(Of String)
            Get
                Return _RecentQueue
            End Get
            Private Set(value As List(Of String))
                _RecentQueue = value
            End Set
        End Property

        Public Sub New()

        End Sub

        Public Sub New(RecordPath As String)
            BindingRecorder = RecordPath
            Try
                If Not My.Computer.FileSystem.FileExists(RecordPath) Then
                    My.Computer.FileSystem.OpenTextFileWriter(RecordPath, False).Close()    ' Create this file simply.
                Else
                    Dim ReaderStream As StreamReader = My.Computer.FileSystem.OpenTextFileReader(RecordPath)
                    Do Until ReaderStream.EndOfStream
                        _RecentQueue.Add(ReaderStream.ReadLine())
                    Loop
                    ReaderStream.Close()
                End If
            Catch ex As Exception
                ' Do nothing special ?
            End Try
        End Sub

        Private Sub Flush()
            If Trim(BindingRecorder) = "" Then
                Exit Sub
            End If
            Dim Writer As StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(BindingRecorder, False)
            For i = _RecentQueue.Count - 1 To 0 Step -1
                Writer.Write(_RecentQueue(i))
                Writer.WriteLine()
            Next
            Writer.Close()
        End Sub

        Public Sub AddRecentFile(File As String)
            If _RecentQueue.Contains(File) Then
                Exit Sub
            End If
            While _RecentQueue.Count > CountOfRecents AndAlso RecentQueue.Count > 0
                _RecentQueue.RemoveAt(0)
            End While
            _RecentQueue.Add(File)
            Flush()
        End Sub

    End Class

    Public Function FontExists(Name As String) As Boolean
        Dim Tester As Font = New Font(Name, 11)
        Return Tester.OriginalFontName = Tester.Name
    End Function
End Module
