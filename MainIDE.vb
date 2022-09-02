Imports System.ComponentModel
Imports Microsoft.Win32.Forms
Public Class MainIDE

    ' Load class information and library information
    ' Data can be used for all systems


    '[DllImport("user32.dll", EntryPoint = "SendMessage")]
    'Private Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As Integer
    Dim upd As Boolean = False
    Dim sync As Boolean = False
    Dim saved As Boolean = True
    'Dim intsync As Boolean = False
    Dim current As String = ""
    'Private Shared Function SendMessage(ByVal hwnd As HWND, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As Integer

    'End Function


    Public Structure BObject
        Public ObjectType As String
        ' "Class" or "Function" or "Bad"

        Public Function Vaild() As Boolean
            Return Not (Me.ObjectType = "Bad")
        End Function

        Public Sub Clear()
            Me.ObjectType = "Bad"
            Me.ClassFunction = New List(Of BObject)
            Me.FunctionParameter = ""
            Me.ObjectName = ""
            Me.LinePosition = -1
            Me.Attributes = New List(Of String)
        End Sub

        ' I don't know why. x is reserved.
        Public Sub New(Optional x As Object = Nothing)
            Me.Clear()
        End Sub

        Public ObjectName As String
        ' For function,
        Public FunctionParameter As String
        ' For class,
        Public ClassFunction As List(Of BObject)
        Public LinePosition As Integer
        Public Attributes As List(Of String)

    End Structure

    Public StaticInfo As List(Of BObject) = New List(Of BObject)
    Public ObjectInfo As List(Of BObject) = New List(Of BObject)

    Public ReadOnly keywords As List(Of String) = New List(Of String)({"class ", "function ", "if ", "elif ", "else:", "while ", "for ", "set ", "serial ", "object ", "ishave ", "init:", "print ", "file ", "break", "continue", "run ", "new ", "this.", "dump", "debugger", "import ", "inherits ", "return ", "global ", "call ", "shared ", "shared class", "must_inherit", "no_inherit", "raise ", "error_handler:", "this:"})
    Public static_func As List(Of String) = New List(Of String) ' To match as mag.
    Public ReadOnly acceptable_near As SortedSet(Of Char) = New SortedSet(Of Char)({"~"c, "+"c, "-"c, "*"c, "/"c, "%"c, ":"c, "#"c, "("c, ")"c, " "c, ","c, vbLf, vbCr})

    Public Sub LoadObjectInfo(Data As String, Optional ToStatic As Boolean = False, Optional NoLine As Boolean = False)
        Dim sp As String() = Split(Data, vbLf)
        For i = 0 To sp.Count - 1
            Dim id As Integer = sp(i).IndexOf(vbCr)
            If id >= 0 Then
                sp(i) = sp(i).Remove(id)
            End If

        Next
        'Dim reading_class As String = ""
        Dim r_class As BObject = New BObject
        r_class.Clear()
        Dim line_id As Integer = 0
        For Each i In sp
            line_id += 1
            Dim arg As String() = Split(i, " ", 2)
            If arg.Count() <= 0 OrElse arg(0).Length <= 0 Then
                Continue For
            End If
            Dim count As Integer = 0
            While arg(0).Length > 0 AndAlso arg(0)(0) = vbTab
                arg(0) = arg(0).Remove(0, 1)
                count += 1
            End While
            If count = 0 Then
                If r_class.Vaild() Then
                    If ToStatic Then
                        StaticInfo.Add(r_class)
                        For Each rc In r_class.ClassFunction
                            If rc.ObjectType = "Function" Then
                                Dim to_add As String = "." + rc.ObjectName
                                If static_func.IndexOf(to_add) < 0 Then
                                    static_func.Add(to_add)
                                End If
                            End If
                        Next
                    Else
                        ObjectInfo.Add(r_class)
                    End If
                    r_class.Clear()
                End If
            End If
            If arg(0) = "class" Then
                ' Read class data from then on
                Try
                    arg(1) = arg(1).Remove(arg(1).Length - 1)
                    r_class.ObjectType = "Class"
                    r_class.ObjectName = arg(1)
                    r_class.Attributes = New List(Of String)

                    If (Not ToStatic) And (Not NoLine) Then
                        r_class.LinePosition = line_id
                    End If
                Catch ex As Exception

                End Try

                'reading_class = ""
            ElseIf arg(0) = "shared" Then
                If arg(1) = "class" Then
                    r_class.Attributes.Add("Shared")
                Else
                    Dim flag As Boolean = False
                    For j = 0 To r_class.ClassFunction.Count - 1
                        If r_class.ClassFunction(j).ObjectName = arg(1) Then
                            r_class.ClassFunction(j).Attributes.Add("Shared")
                            flag = True
                        End If
                    Next
                    If Not flag Then
                        Dim m As BObject = New BObject
                        m.ObjectType = "Variable"
                        m.ObjectName = arg(1)
                        m.Attributes = New List(Of String)({"Shared"})
                        r_class.ClassFunction.Add(m)
                    End If
                End If
            ElseIf arg(0) = "function" Then
                Dim argz As String = ""
                Dim fn As String = ""
                Try
                    'arg(1).Remove(0, 1)
                    Dim argw As String() = Split(arg(1), " ", 2)
                    fn = argw(0)
                    argz = argw(1)
                Catch ex As Exception

                End Try
                Dim b As BObject = New BObject
                b.Attributes = New List(Of String)
                b.ObjectType = "Function"
                b.FunctionParameter = argz
                b.ObjectName = fn
                If fn.Length > 0 AndAlso fn(fn.Length - 1) = ":"c Then
                    fn = fn.Remove(fn.Length - 1)
                End If
                If count = 0 Then
                    If ToStatic Then
                        StaticInfo.Add(b)
                        If static_func.IndexOf(fn) < 0 Then
                            static_func.Add(fn)
                        End If
                    Else
                        If Not NoLine Then
                            b.LinePosition = line_id
                        End If
                        ObjectInfo.Add(b)
                    End If
                Else
                    ' Must be class
                    If Not NoLine Then
                        b.LinePosition = line_id
                    End If
                    r_class.ClassFunction.Add(b)
                End If
            ElseIf arg(0) = "init:" Then
                If r_class.Vaild() Then
                    Dim b As BObject = New BObject
                    b.ObjectType = "Function"
                    b.FunctionParameter = ""
                    b.ObjectName = "(Initalizer)"
                    b.LinePosition = line_id
                    r_class.ClassFunction.Add(b)
                End If
            ElseIf arg(0) = "error_handler:" Then
                Dim b As BObject = New BObject
                b.Clear()
                b.Attributes = New List(Of String)
                b.ClassFunction = New List(Of BObject)
                b.FunctionParameter = ""
                b.LinePosition = line_id
                b.ObjectName = "(Error Handler)"
                b.ObjectType = "Function"
                If ToStatic Then

                    StaticInfo.Add(b)
                Else
                    ObjectInfo.Add(b)
                End If
            ElseIf arg(0) = "import" Then
                Try
                    Dim f As String
                    Dim fd As IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath & "\" & arg(1))
                    f = fd.ReadToEnd()
                    fd.Close()
                    LoadObjectInfo(f, False, True)
                Catch ex As Exception

                End Try
            ElseIf arg(0) = "inherits" Then
                If r_class.Vaild() Then
                    Dim gotClass As BObject = New BObject
                    gotClass.Clear()
                    For Each a In ObjectInfo
                        If a.ObjectType = "Class" And a.ObjectName = arg(1) Then
                            gotClass = a
                        End If
                    Next
                    If Not gotClass.Vaild() Then
                        For Each a In StaticInfo
                            If a.ObjectType = "Class" And a.ObjectName = arg(1) Then
                                gotClass = a
                            End If
                        Next
                    End If
                    Dim inh_flag As BObject = New BObject
                    inh_flag.Clear()
                    inh_flag.ObjectType = "Inheritance"
                    inh_flag.ObjectName = gotClass.ObjectName
                    inh_flag.LinePosition = gotClass.LinePosition
                    For Each a In gotClass.ClassFunction
                        inh_flag.ClassFunction.Add(a)
                    Next
                    r_class.ClassFunction.Add(inh_flag)
                End If
            ElseIf arg(0) = "no_inherit" Then
                r_class.Attributes.Add("No Inheriting")
            ElseIf arg(0) = "must_inherit" Then
                r_class.Attributes.Add("Must Inherit")
            End If
        Next
        If r_class.Vaild() Then
            If ToStatic Then
                StaticInfo.Add(r_class)
            Else
                ObjectInfo.Add(r_class)
            End If
            r_class.Clear()
        End If
    End Sub

    Public Sub LoadIntelli()
        ObjectInfo.Clear()
        LoadObjectInfo(CodeData.Text)
        'ObjectInfo.Union(StaticInfo)
        For Each i In StaticInfo
            ObjectInfo.Add(i)
        Next
    End Sub

    Private suspendScroller As Boolean = False

    Private Sub CodeHUpdate(Optional sender As Object = Nothing, Optional e As EventArgs = Nothing)
        'If Not CodeHighlighter.IsBusy Then
        'CodeHighlighter.RunWorkerAsync()
        'End If
        CodeHUpdateInner(sender, e)
    End Sub

    Private Sub CodeHUpdateInner(sender As Object, e As EventArgs)
        ' Color them
        ' 0. Clear
        'CodeData.SelectAll()
        'CodeData.SelectionColor = Color.Black
        'Me.SuspendLayout()
        'Dim bw As BackgroundWorker = sender
        Exit Sub 'testing

    End Sub

    Private Sub MainIDE_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim required As String() = {"\BlueBetter4.exe", "\bmain.blue"}
        For Each i In required
            If Not My.Computer.FileSystem.FileExists(Application.StartupPath & i) Then
                MsgBox("BlueBetter environment file (" & i & ") not found!", MsgBoxStyle.Critical)
                End
            End If
        Next
        CodeData_VScroll(New Object, New EventArgs)
        Dim f As String
        Dim fd As IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath & "\bmain.blue")
        f = fd.ReadToEnd()
        fd.Close()
        LoadObjectInfo(f, True)
    End Sub

    Private Sub SearchClassToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchClassToolStripMenuItem.Click
        LoadIntelli()
        ' Load forms for it
        Dim s As New Intelli
        s.Show()
    End Sub

    Private Sub Closer_Click(sender As Object, e As EventArgs) Handles Closer.Click
        Searcher.Visible = False
    End Sub

    Private Sub FindToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindToolStripMenuItem.Click
        Searcher.Visible = True
    End Sub

    Dim starter As Integer = 0

    Private Sub SchPush_Click(sender As Object, e As EventArgs) Handles SchPush.Click
        starter = CodeData.Find(SearchBox.Text, starter, RichTextBoxFinds.None)
        If starter < 0 Then
            starter = 0
            Dim sel = MsgBox("Already reaches the end of text! Restart searching?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Search")
            If sel = vbNo Then
                Exit Sub
            End If
        End If
        CodeData.Select(starter, SearchBox.Text.Length)
        CodeData.Select()
        starter += SearchBox.Text.Length + 1
        If starter > CodeData.TextLength Then
            starter = -1
        End If
    End Sub

    Private Sub Replacer_Click(sender As Object, e As EventArgs) Handles Replacer.Click
        Dim cursor As Integer = 0
        Do ' Until cursor < 0
            cursor = CodeData.Find(SearchBox.Text, cursor, RichTextBoxFinds.None)
            If cursor < 0 Then
                Exit Do
            End If
            'CodeData.Select()
            CodeData.Select(cursor, SearchBox.Text.Length)
            CodeData.SelectedText = ReplaceBox.Text
            cursor += ReplaceBox.Text.Length + 1
            If cursor > CodeData.TextLength Then
                Exit Do
            End If
        Loop
        MsgBox("Complete!", MsgBoxStyle.Information, "Replace")
    End Sub

    Private Sub CreateClassToolStripMenuItem_Click(sender As Object, e As EventArgs)
        ' Test
        CodeData.Select(2, 5)
    End Sub

    Private lineJustEdit As Integer = 0

    Private Sub LinearUpdate(Optional alwaysRun As Boolean = False)

        Dim currentline As Integer = CodeData.GetLineFromCharIndex(CodeData.SelectionStart)
        Dim usercur As Integer = CodeData.SelectionStart
        If (currentline <> lineJustEdit) Or alwaysRun Then
            suspendScroller = True
            ' Update 'LineJustEdit'
            'Dim allline As String
            Dim currentbegin As Integer = CodeData.GetFirstCharIndexFromLine(lineJustEdit)
            Dim currentend As Integer = CodeData.GetFirstCharIndexFromLine(lineJustEdit + 1) - 1
            If currentbegin < 0 Or currentend < 0 Then GoTo vsc
            CodeData.Select(currentbegin, currentend - currentbegin)
            Dim allline As String = CodeData.SelectedText
            CodeData.SelectionColor = Color.Black
            Dim totrim As String = Trim(allline)
            While totrim.Length > 0 AndAlso totrim(0) = vbTab
                totrim = totrim.Remove(0, 1)
            End While
            If totrim.Length > 0 AndAlso totrim(0) = "#" Then
                CodeData.SelectionColor = Color.DarkGreen
                GoTo FinishA
            End If

            ' End of currentbegin-currentend selection !
            ' 1. Keywords
            For Each i In keywords
                Dim sp As Integer = 0
                'Dim _first As Boolean = False
                Dim previous As Integer = -2
                Do
                    'sp = CodeData.Find(i, sp, RichTextBoxFinds.None)
                    sp = allline.IndexOf(i, sp)
                    If previous >= sp Or sp < 0 Then
                        Exit Do
                    End If
                    previous = sp
                    CodeData.SelectionStart = sp + currentbegin
                    CodeData.SelectionLength = i.Length
                    CodeData.SelectionColor = Color.Blue
                    sp += i.Length + 1
                    If sp > allline.Length Then
                        Exit Do
                    End If
                Loop
            Next
            For Each i In static_func
                Dim sp As Integer = 0
                'Dim _first As Boolean = False
                Dim previous As Integer = -2
                Do
                    sp = allline.IndexOf(i, sp)
                    If previous >= sp Or sp < 0 Then
                        Exit Do
                    End If
                    previous = sp
                    If (sp > 0 AndAlso (Not acceptable_near.Contains(allline(sp - 1)))) OrElse (sp < allline.Length - i.Length AndAlso (Not acceptable_near.Contains(allline(sp + i.Length)))) Then
                        sp += i.Length + 1
                        If sp > allline.Length Then
                            Exit Do
                        End If
                        Continue Do ' Not filtered!
                    End If
                    CodeData.SelectionStart = sp + currentbegin
                    CodeData.SelectionLength = i.Length
                    CodeData.SelectionColor = Color.Magenta
                    sp += i.Length + 1
                    If sp > allline.Length Then
                        Exit Do
                    End If
                Loop
            Next
            For Each iobj In StaticInfo
                If iobj.ObjectType <> "Class" Then
                    Continue For
                End If
                Dim i As String = "new " & iobj.ObjectName   ' Must be EOL
                ' Only detects end of line
                ' 1. Add if here's tab
                Dim wline As String = allline
                ' Only 1 LF acceptable.

                Dim ai As Integer = wline.IndexOf(vbLf)
                If ai >= 0 Then
                    wline = wline.Substring(0, ai)
                End If

                While wline.Length > 0 AndAlso (wline(wline.Length - 1) = vbLf Or wline(wline.Length - 1) = vbTab)
                    wline = wline.Remove(wline.Length - 1)
                End While

                If (i.Length < wline.Length) AndAlso (wline.Substring(wline.Length - i.Length) = i) Then
                    CodeData.SelectionStart = wline.Length - i.Length + currentbegin
                    CodeData.SelectionLength = i.Length
                    CodeData.SelectionColor = Color.DarkCyan
                End If
            Next
            ' After all override our quotes
            Dim turned As Boolean = False
            Dim instring As Boolean = False
            Dim justturn As Boolean = False
            For i = 0 To allline.Length - 1
                justturn = False
                If allline(i) = """"c Or instring Then
                    CodeData.SelectionStart = i + currentbegin
                    CodeData.SelectionLength = 1
                    CodeData.SelectionColor = Color.DarkGray
                    If allline(i) = """"c Then
                        If Not turned Then
                            instring = Not instring
                        End If
                    End If
                End If
                If allline(i) = "\"c Then
                        If Not turned Then
                            turned = True
                            justturn = True
                        End If
                    End If
                    If turned And (Not justturn) Then
                    turned = False
                End If
            Next
FinishA:    CodeData.Select(usercur, 0)
            CodeData.SelectionColor = Color.Black

            suspendScroller = False
        End If
vsc:    lineJustEdit = currentline
        If suspendScroller Then
            suspendScroller = False
        End If
    End Sub

    Private Sub CodeData_TextChanged(sender As Object, e As EventArgs) Handles CodeData.TextChanged
        upd = True
        sync = False

        ' Update my stuff here.

        saved = False
        CodeData_VScroll(sender, e)
    End Sub

    Private ReadOnly skipper As List(Of Char) = New List(Of Char)({vbTab, vbCr, vbLf})
    Private isRevDiv As Boolean = False
    Private isString As Boolean = False

    Private Sub CodeData_KeyUp(sender As Object, e As KeyEventArgs) Handles CodeData.KeyUp
        Const Quotes As Integer = 222
        Const RevDiv As Integer = 220

        Dim justDiv As Boolean = False

        Select Case e.KeyCode
            Case Keys.Enter
                Dim pos As Integer = CodeData.SelectionStart - 1
                Dim tabs As Integer = 0
                While pos >= 0 AndAlso skipper.Contains(CodeData.Text(pos))
                    pos -= 1
                End While
                If pos < 0 Then
                    Exit Sub
                End If
                Dim wpos As Integer
                wpos = CodeData.GetFirstCharIndexFromLine(CodeData.GetLineFromCharIndex(pos))
                While wpos < CodeData.TextLength AndAlso CodeData.Text(wpos) = vbTab
                    wpos += 1
                    tabs += 1
                End While
                If CodeData.Text(pos) = ":"c Then
                    'CodeData.SelectedText += vbCrLf
                    For i = 0 To tabs
                        CodeData.SelectedText += vbTab
                    Next
                Else
                    For i = 0 To tabs - 1
                        CodeData.SelectedText += vbTab
                    Next
                End If
            Case Else

        End Select

        If isRevDiv And (Not justDiv) Then
            isRevDiv = False
        End If
    End Sub

    Public Sub SaveThisTo(filename As String, Optional noSaved As Boolean = False)
        Dim s As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(filename, False, System.Text.Encoding.Default)
        s.Write(CodeData.Text)
        s.Close()
        If Not noSaved Then
            saved = True
        End If

    End Sub

    Public Sub SelectSave()
        If sfd.ShowDialog() = DialogResult.OK Then
            SaveThisTo(sfd.FileName)
            current = sfd.FileName
        End If
    End Sub

    Public Sub LoadSave()
        If current.Length > 0 Then
            SaveThisTo(current)
        Else
            SelectSave()
        End If
    End Sub

    Public Sub ClearCheck()
        If Not saved Then
            Dim r = MsgBox("Current file not saved. Save?", MsgBoxStyle.YesNo, "Prompt")
            If r = MsgBoxResult.Yes Then
                LoadSave()
            End If
        End If
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        ClearCheck()
        CodeData.Text = ""
        current = ""
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        ClearCheck()
        If ofd.ShowDialog() = DialogResult.OK Then
            Dim d As IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(ofd.FileName)
            'CodeData.Visible = False
            CodeData.Text = d.ReadToEnd()
            current = ofd.FileName
            d.Close()
            suspendScroller = True
            CodeData.Enabled = False
            For i = 0 To CodeData.GetLineFromCharIndex(CodeData.TextLength - 1)
                lineJustEdit = i
                LinearUpdate(True)
            Next
            CodeData.Enabled = True
            suspendScroller = False
            'CodeHUpdate()
            'CodeData.Visible = True
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        LoadSave()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        SelectSave()
    End Sub

    Private Sub QuitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem.Click
        ClearCheck()
        Me.Close()
    End Sub

    Private Sub MainIDE_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ClearCheck()
    End Sub

    Private PreparePath As String = Application.StartupPath & "\tcode.blue"

    Private Sub PrepareCurrentProgram()
        SaveThisTo(PreparePath, True)
    End Sub

    Private Sub RunCurrentProgram(Optional parameter As String = "")

        Shell(Application.StartupPath & "\BlueBetter4.exe """ & PreparePath & " " & parameter, AppWinStyle.NormalFocus)
    End Sub

    Private Sub RunCodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RunCodeToolStripMenuItem.Click
        'ClearCheck()
        'If saved Then
        '    FileCopy()
        'CodeData.SaveFile(Environ("temp") & "\tcode.blue", RichTextBoxStreamType.PlainText)
        RunCurrentProgram()
        'End If
    End Sub

    Private Sub DebugCodedebugToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DebugCodedebugToolStripMenuItem.Click
        'ClearCheck()
        'If saved Then
        RunCurrentProgram("--debug")
        'End If
    End Sub

    Private Sub AddStaticFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddStaticFileToolStripMenuItem.Click
        If ofd.ShowDialog() = DialogResult.OK Then
            Dim s As String
            Dim f As IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(ofd.FileName, System.Text.Encoding.Default)
            s = f.ReadToEnd()
            f.Close()
            LoadObjectInfo(s, True, True)
            CodeHUpdate()
        End If
    End Sub

    Private Sub CodeData_VScroll(sender As Object, e As EventArgs) Handles CodeData.VScroll
        If suspendScroller Then
            Exit Sub
        End If
        Dim showbegin = CodeData.GetCharIndexFromPosition(New Point(0, 0))
        Dim showend = CodeData.GetCharIndexFromPosition(New Point(CodeData.ClientSize.Width, CodeData.ClientSize.Height))
        Dim beginline = CodeData.GetLineFromCharIndex(showbegin)
        Dim endline = CodeData.GetLineFromCharIndex(showend)
        LineLabel.Font = CodeData.Font
        LineLabel.Text = ""
        For i = beginline To endline
            LineLabel.Text = LineLabel.Text & Format(i + 1, "000") & vbCrLf
        Next
    End Sub

    Private Sub UpdateTimer_Tick(sender As Object, e As EventArgs) 
        CodeHUpdate()
    End Sub

    Private Sub LineLabel_GotFocus(sender As Object, e As EventArgs) Handles LineLabel.GotFocus
        CodeData.Select()
    End Sub

    Private Sub CodeData_SelectionChanged(sender As Object, e As EventArgs) Handles CodeData.SelectionChanged
        If Not suspendScroller Then
            LinearUpdate()
        End If

    End Sub

    Private Sub VersionInformationversionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VersionInformationversionToolStripMenuItem.Click
        Dim p As New Process
        p.StartInfo.FileName = Application.StartupPath & "\BlueBetter4.exe"
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
        MsgBox(res, MsgBoxStyle.Information, "Version")
    End Sub

End Class
