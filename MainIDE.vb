Imports System.ComponentModel
Imports NorthernLights
Imports System.Text
Imports System.IO

''' <!--
''' We'll consider supporting pre-function things like
''' # [Description ...]
''' # [Description ...]
''' And show it in the searcher
''' -->

Public Class MainIDE
    Implements IDEChildInterface

    ' Load class information and library information
    ' Data can be used for all systems
    Public Property GeneralFont As String = "TESTTEST" '"ËÎÌו"
    Public HTMLKinds As HashSet(Of String) = New HashSet(Of String)({"htm", "html", "xml"})
    Public StandardLibrary As List(Of String) = New List(Of String)({"bmain.blue", "WebHeader.blue", "algo.blue", "math.blue", "document.blue"})
    Private IsPlainHTML As Boolean = False
    Private IsCreating As Boolean = False
    ' For plain file only
    Private NoExecution As Boolean = False
    Private OverridenExtension As String = ""
    Private ReadOnly Property CurrentExtension As String
        Get
            If OverridenExtension <> "" Then
                Return "." & OverridenExtension
            ElseIf current = "" Then
                Return ""
            Else
                Return "." & GetExtension(current)
            End If
        End Get
    End Property
    Private ReadOnly Property CurrentFilterIndex As Integer
        Get
            ' Filter is:
            ' BlueBetter file|*.blue|BluePage file|*.bp|HTML File|*.html|HTM File|*.htm|XML File|*.xml|Text file|*.txt|CSV Table file|*.csv|All files|*.*
            Select Case CurrentExtension
                Case ".blue"
                    Return 1
                Case ".bp"
                    Return 2
                Case ".html"
                    Return 3
                Case ".htm"
                    Return 4
                Case ".xml"
                    Return 5
                Case ".txt"
                    Return 6
                Case ".csv"
                    Return 7
                Case Else
                    Return 8
            End Select
        End Get
    End Property
    Private ReadOnly Property DefaultEncoder As Encoding
        Get
            If UseANSIAsDefaultEncodinginsteadOfUTF8ToolStripMenuItem.Checked Then
                Return Encoding.Default
            Else
                Return Encoding.UTF8
            End If
        End Get
    End Property

    '[DllImport("user32.dll", EntryPoint = "SendMessage")]
    'Private Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As Integer
    Dim upd As Boolean = False
    Dim sync As Boolean = False
    'Dim saved As Boolean = True
    Private _saved As Boolean = True
    Private Property saved As Boolean
        Get
            Return _saved
        End Get
        Set(value As Boolean)
            If Not value Then
                Me.Text = "BlueBetter - " & current & " *"
            Else
                Me.Text = "BlueBetter - " & current
            End If
            _saved = value
        End Set
    End Property
    'Dim intsync As Boolean = False
    Private _current As String
    Private Property current As String
        Get
            Return _current
        End Get
        Set(value As String)
            _current = value
            If Trim(value) = "" Then
                Me.Text = "BlueBetter"
            Else
                Me.Text = "BlueBetter - " & _current
            End If
        End Set
    End Property
    'Private Shared Function SendMessage(ByVal hwnd As HWND, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As Integer

    'End Function

    Private Sub ShowFileSelector()
        OpeningPrompt.Text = ""
        RegardAsANSI.Visible = False
        RegardAsANSI.Checked = False
        FileKindSelector.Visible = True
    End Sub

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
            Me.ObjectDescription = ""
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
        Public ObjectDescription As String

    End Structure

    Public StaticInfo As List(Of BObject) = New List(Of BObject)
    Public ObjectInfo As List(Of BObject) = New List(Of BObject)

    Public ReadOnly keywords As List(Of String) = New List(Of String)({"undefined", "null", "true", "false", "serial ", "object ", "ishave ", "new ", "this.", "this:", "referof", "copyof", "isref", "const ", "preserve "})
    ' Only able to exist after removing vbTab
    Public commanding_keywords As List(Of String) = New List(Of String)({"class ", "function ", "if ", "elif ", "else:", "while ", "for ", "set ", "setstr ", "init:", "print ", "file ", "break", "continue", "run ", "dump", "debugger", "import ", "inherits ", "return", "global ", "call ", "shared ", "shared class", "must_inherit", "no_inherit", "raise ", "error_handler:", "hidden", "declare", "property get", "property set", "property noget", "property noset", "thread test", "thread new", "thread join", "thread detach", "mutex test", "mutex make", "mutex wait", "mutex release", "preset", "prerun"})
    Public postbacking_keywords As List(Of String) = New List(Of String)({"listen", "postback", "before_send", "after_send", "on_load", "progressive"})
    Public static_func As List(Of String) = New List(Of String) ' To match as mag.
    Public ReadOnly acceptable_near As SortedSet(Of Char) = New SortedSet(Of Char)({"~"c, "+"c, "-"c, "*"c, "/"c, "%"c, ":"c, "#"c, "("c, ")"c, " "c, ","c, vbLf, vbCr, vbTab, "$"c, "="c, "^"c, "|"c, "&"c, ">"c, "<"c})

    ' For JavaScript: (Reserved keywords are not here.)
    Public js_keywords As List(Of String) = New List(Of String)({"break", "case", "catch", "continue", "default", "delete", "do", "else", "finally", "for", "function", "if", "in", "instanceof", "new", "return", "switch", "this", "throw", "try", "typeof", "var", "void", "while", "with", "let"})
    Public js_operators As List(Of Char) = New List(Of Char)({"~"c, "+"c, "-"c, "*"c, "/"c, "%"c, ":"c, "#"c, "("c, ")"c, " "c, ","c, vbLf, vbCr, vbTab, "$"c, "="c, "^"c, "|"c, "&"c, ">"c, "<"c, "["c, "]"c, "{"c, "}"c, "!"c, "?"c, "."c})   ' Only for expression, not for Intelligent analyzer
    Public js_string_liked As List(Of String) = New List(Of String)({""""c, "/"c})
    'Public js_classes As List(Of String) = New List(Of String)({"document", "window", "XMLHttpRequest"})    ' These are what we usually use

    Private Function ShrinkDot(Str As String) As String
        If Str.Length >= 1 AndAlso Str(Str.Length - 1) = ":"c Then
            Return Str.Substring(0, Str.Length - 1)
        Else
            Return Str
        End If
    End Function

    Public Sub LoadObjectInfo(Data As String, Optional ToStatic As Boolean = False, Optional NoLine As Boolean = False)
        Try
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
            Dim is_blue As Boolean = False
            Dim PreparedComment As StringBuilder = New StringBuilder
            Dim CurrentRemarkPosition As Integer = -1
            For Each i In sp
                line_id += 1
                ' Skip non-BlueBetter lines
                If i.LastIndexOf("<?blue") > i.LastIndexOf("?>") Then
                    is_blue = True
                    Continue For
                ElseIf i.LastIndexOf("?>") > i.LastIndexOf("<?blue") Then
                    is_blue = False
                    Continue For
                End If
                If Not (isBluebetter OrElse is_blue OrElse ToStatic) Then
                    Continue For
                End If
                Dim arg As String() = Split(i, " ", 2)
                If arg.Count() <= 0 Then
                    Continue For
                End If
                Dim count As Integer = 0
                While arg(0).Length > 0 AndAlso arg(0)(0) = vbTab
                    arg(0) = arg(0).Remove(0, 1)
                    count += 1
                End While
                If arg(0).Length <= 0 Then
                    Continue For
                End If
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
                If arg(0)(0) = "#"c Then
                    If CurrentRemarkPosition <> count Then
                        PreparedComment.Clear()
                        CurrentRemarkPosition = count
                    End If
                    If i.Length > 1 Then
                        PreparedComment.Append(i.Substring(count + 1))
                    End If
                    PreparedComment.AppendLine()
                ElseIf arg(0) = "class" Then
                    ' Read class data from then on
                    Try
                        arg(1) = arg(1).Remove(arg(1).Length - 1)
                        r_class.ObjectType = "Class"
                        r_class.ObjectName = ShrinkDot(arg(1))
                        r_class.Attributes = New List(Of String)
                        If CurrentRemarkPosition = count Then
                            r_class.ObjectDescription = PreparedComment.ToString()
                        End If
                        PreparedComment.Clear()

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
                ElseIf arg(0) = "preset" Then
                    Dim m As BObject = New BObject
                    m.ObjectType = "Variable"
                    Dim EqIndex = arg(1).IndexOf("="c)
                    If EqIndex < 0 Then
                        EqIndex = arg(1).Length
                    End If
                    m.ObjectName = arg(1).Substring(0, EqIndex)
                    m.Attributes = New List(Of String)({"Shared", "Pre-Initalize"})
                    If CurrentRemarkPosition = count Then
                        m.ObjectDescription = PreparedComment.ToString()
                    End If
                    PreparedComment.Clear()
                    If Not NoLine Then
                        m.LinePosition = line_id
                    End If
                    If count = 0 Then
                        If ToStatic Then
                            StaticInfo.Add(m)
                        Else
                            ObjectInfo.Add(m)
                        End If
                    Else
                        r_class.ClassFunction.Add(m)
                    End If

                ElseIf arg(0) = "function" OrElse arg(0) = "property" Then
                    ' If it's a property ...
                    Dim ActualObjectType As String = "Function"
                    If arg(0) = "property" Then
                        Dim rs() As String = Split(arg(1), " ", 2)
                        ActualObjectType = "Property " & StrConv(rs(0), VbStrConv.ProperCase)
                        arg(1) = rs(1)          ' Name
                    End If
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
                    b.ObjectType = ActualObjectType
                    b.FunctionParameter = argz
                    b.ObjectName = ShrinkDot(fn)
                    If CurrentRemarkPosition = count Then
                        b.ObjectDescription = PreparedComment.ToString()
                    End If
                    PreparedComment.Clear()
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
                        If CurrentRemarkPosition = count Then
                            b.ObjectDescription = PreparedComment.ToString()
                        End If
                        PreparedComment.Clear()
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
                    If CurrentRemarkPosition = count Then
                        b.ObjectDescription = PreparedComment.ToString()
                    End If
                    PreparedComment.Clear()
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
                Else
                    PreparedComment.Clear()
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
        Catch ex As Exception ' To make everything broken
            ' Because everything can happen during the accidental processor
        End Try
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
                MsgBox("BlueBetter environmental file (" & i & ") not found!", MsgBoxStyle.Critical)
                End
            End If
        Next
        ' Load general font
        If FontExists("ËÎÌו") Then
            GeneralFont = "ËÎÌו"
        Else
            GeneralFont = "Microsoft Sans Serif"
        End If
        CodeData.Font = New Font(GeneralFont, 11)
        LineLabel.Font = New Font(GeneralFont, 11)
        CodeData_VScroll(New Object, New EventArgs)
        'CodeData.LanguageOption = RichTextBoxLanguageOptions.UIFonts    'For some system it works strangely.
        Dim failure As String = ""
        For Each i In StandardLibrary
            Try
                Dim f As String
                Dim fd As IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath & "\" & i)
                f = fd.ReadToEnd()
                fd.Close()
                LoadObjectInfo(f, True)
            Catch ex As Exception
                failure &= i & vbCrLf
            End Try
        Next
        If Trim(failure) <> "" Then
            MsgBox("Warning: Failed to load standard library: " & vbCrLf & vbCrLf & failure, MsgBoxStyle.Exclamation, "Warning")
        End If
    End Sub

    Private Sub SearchClassToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchClassToolStripMenuItem.Click
        LoadIntelli()
        ' Load forms for it
        Dim s As New Intelli
        s.ObjectInfo = Me.ObjectInfo
        s.CurrentParent = Me
        s.Show()
    End Sub

    Private Sub Closer_Click(sender As Object, e As EventArgs) Handles Closer.Click
        Searcher.Visible = False
    End Sub

    Private Sub FindToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindToolStripMenuItem.Click
        Searcher.Visible = True
    End Sub

    Dim starter As Integer = 0

    Private ReadOnly Property FindParameters As RichTextBoxFinds
        Get
            Dim options As Integer = RichTextBoxFinds.None
            If WholeWords.Checked Then
                options += RichTextBoxFinds.WholeWord
            End If
            If CaseSens.Checked Then
                options += RichTextBoxFinds.MatchCase
            End If
            Return CType(options, RichTextBoxFinds)
        End Get
    End Property

    Private Sub SchPush_Click(sender As Object, e As EventArgs) Handles SchPush.Click

        starter = CodeData.Find(SearchBox.Text, starter, FindParameters)
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
            cursor = CodeData.Find(SearchBox.Text, cursor, FindParameters)
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

    End Sub

    Private lineJustEdit As Integer = 0
    ' For bluepage it's false
    ' For debug propose also use False.
    Private isBluebetter As Boolean = False
    Private isJS As Boolean = False

    Private Sub LinearUpdate(Optional alwaysRun As Boolean = False)
        If NoExecution Then
            Exit Sub
            ' Plain file !
        End If
        Dim currentline As Integer = CodeData.GetLineFromCharIndex(CodeData.SelectionStart)
        Dim usercur As Integer = CodeData.SelectionStart
        If (currentline <> lineJustEdit) Or alwaysRun Then
            suspendScroller = True
            ' Update 'LineJustEdit'
            'Dim allline As String

            ' Common for all kinds
            Dim currentbegin As Integer = CodeData.GetFirstCharIndexFromLine(lineJustEdit)
            Dim currentend As Integer = CodeData.GetFirstCharIndexFromLine(lineJustEdit + 1) - 1
            If currentbegin < 0 Or currentend < 0 Then GoTo vsc
            CodeData.Select(currentbegin, currentend - currentbegin)
            Dim allline As String = CodeData.SelectedText
            CodeData.SelectionColor = Color.Black
            CodeData.SelectionFont = New Font(GeneralFont, 11) ' Always use this!
            Dim totrim As String = Trim(allline)
            While totrim.Length > 0 AndAlso totrim(0) = vbTab
                totrim = totrim.Remove(0, 1)
            End While

            Dim finder As Integer = lineJustEdit - 1    ' For <?blue test, NOT from the current line
            Dim is_blue As Boolean = isBluebetter
            Dim is_js As Boolean = isJS             ' May support JS opening in the future
            Dim is_postback As Boolean = False
            Dim is_comment As Boolean = False
            Dim comment_check As Boolean = True

            If (Not isBluebetter) AndAlso (Not IsPlainHTML) Then
                While finder >= 0
                    Dim fcurrentbegin As Integer = CodeData.GetFirstCharIndexFromLine(finder)
                    Dim fcurrentend As Integer = CodeData.GetFirstCharIndexFromLine(finder + 1) - 1
                    CodeData.Select(fcurrentbegin, fcurrentend - fcurrentbegin)
                    Dim fallline As String = CodeData.SelectedText
                    Dim fbeg As Integer = fallline.LastIndexOf("<?blue")
                    Dim fxbeg As Integer = fallline.LastIndexOf("<?blue postback")
                    Dim fend As Integer = fallline.LastIndexOf("?>")
                    If fbeg > fend Then
                        If fxbeg > fend Then
                            is_postback = True
                        End If
                        is_blue = True
                        Exit While
                    ElseIf fbeg < fend Then
                        is_blue = False
                        Exit While
                    End If
                    finder -= 1
                End While
            End If

            finder = lineJustEdit   ' This requires the first line match

            ' HTML Commons
            If (Not isBluebetter) AndAlso (Not is_blue) Then
                While finder >= 0
                    Dim fcurrentbegin As Integer = CodeData.GetFirstCharIndexFromLine(finder)
                    Dim fcurrentend As Integer = CodeData.GetFirstCharIndexFromLine(finder + 1) - 1
                    CodeData.Select(fcurrentbegin, fcurrentend - fcurrentbegin)
                    Dim fallline As String = CodeData.SelectedText
                    Dim cend As Integer = fallline.LastIndexOf("-->")
                    Dim cbegin As Integer = fallline.LastIndexOf("<!--")
                    If cbegin > cend Then
                        is_comment = True
                        Exit While
                    ElseIf cbegin < cend Then
                        is_comment = False
                        Exit While
                    End If
                    finder -= 1
                End While
                If Not is_comment Then
                    finder = lineJustEdit - 1   ' Ibld...
                    While finder >= 0
                        Dim fcurrentbegin As Integer = CodeData.GetFirstCharIndexFromLine(finder)
                        Dim fcurrentend As Integer = CodeData.GetFirstCharIndexFromLine(finder + 1) - 1
                        CodeData.Select(fcurrentbegin, fcurrentend - fcurrentbegin)
                        Dim fallline As String = CodeData.SelectedText
                        Dim cend As Integer = fallline.LastIndexOf("</script>")
                        Dim cbegin As Integer = fallline.LastIndexOf("<script") ' May have additional options ...
                        If cbegin > cend Then
                            is_js = True
                            Exit While
                        ElseIf cbegin < cend Then
                            is_js = False
                            Exit While
                        End If
                        finder -= 1
                    End While
                End If
            End If

            ' Reselect...
            CodeData.Select(currentbegin, currentend - currentbegin)

            If is_comment Then
                CodeData.SelectionColor = Color.DarkGreen
            ElseIf is_postback Then
                For Each i In postbacking_keywords
                    ' Requires at the beginning
                    If totrim.IndexOf(i) = 0 Then
                        Dim sp As Integer = allline.IndexOf(i)
                        CodeData.SelectionStart = currentbegin + sp
                        CodeData.SelectionLength = i.Length
                        CodeData.SelectionColor = Color.Blue
                        Exit For
                    End If
                Next
            ElseIf is_js Then
                If totrim.Length >= 2 AndAlso totrim.Substring(0, 2) = "//" Then
                    CodeData.SelectionColor = Color.DarkGreen
                    GoTo FinishA
                End If
                For Each i In js_keywords
                    Dim sp As Integer = 0
                    'Dim _first As Boolean = False
                    Dim previous As Integer = -2
                    Do
                        'sp = CodeData.Find(i, sp, RichTextBoxFinds.None)
                        sp = allline.IndexOf(i, sp)
                        If previous >= sp Or sp < 0 Then
                            Exit Do
                        End If

                        ' Require acceptable splitor, at least BEFORE it


                        previous = sp
                        If sp = 0 OrElse js_operators.Contains(allline(sp - 1)) Then
                            CodeData.SelectionStart = sp + currentbegin
                            CodeData.SelectionLength = i.Length
                            CodeData.SelectionColor = Color.Blue
                        End If
                        sp += i.Length + 1
                        If sp > allline.Length Then
                            Exit Do
                        End If
                    Loop
                    For Each str_mask In js_string_liked
                        Dim turned As Boolean = False
                        Dim instring As Boolean = False
                        Dim justturn As Boolean = False
                        For iter = 0 To allline.Length - 1
                            justturn = False
                            If allline(iter) = str_mask Or instring Then
                                CodeData.SelectionStart = iter + currentbegin
                                CodeData.SelectionLength = 1
                                CodeData.SelectionColor = Color.DarkGray
                                If allline(iter) = str_mask Then
                                    If Not turned Then
                                        instring = Not instring
                                    End If
                                End If
                            End If
                            If allline(iter) = "\"c Then
                                If Not turned Then
                                    turned = True
                                    justturn = True
                                End If
                            End If
                            If turned And (Not justturn) Then
                                turned = False
                            End If
                        Next
                    Next

                Next
            ElseIf is_blue Then
                ' End of currentbegin-currentend selection !
                ' 1. Keywords
                If totrim.Length > 0 AndAlso totrim(0) = "#" Then
                    CodeData.SelectionColor = Color.DarkGreen
                    GoTo FinishA
                End If
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
                        CodeData.SelectionColor = Color.OrangeRed
                        sp += i.Length + 1
                        If sp > allline.Length Then
                            Exit Do
                        End If
                    Loop
                Next
                For Each i In commanding_keywords
                    ' Requires at the beginning
                    If totrim.IndexOf(i) = 0 Then
                        Dim sp As Integer = allline.IndexOf(i)
                        CodeData.SelectionStart = currentbegin + sp
                        CodeData.SelectionLength = i.Length
                        CodeData.SelectionColor = Color.Blue
                        Exit For
                    End If
                Next
                For Each it In ObjectInfo ' static_func
                    Dim i As String = it.ObjectName
                    Dim selc As Color
                    If it.ObjectType = "Function" Then
                        selc = Color.Magenta
                    ElseIf it.ObjectType.IndexOf("Property") >= 0 Then
                        selc = Color.DarkMagenta
                    ElseIf it.ObjectType = "Variable" Then
                        selc = Color.DarkOrange
                    ElseIf it.ObjectType = "Class" Then
                        ' Originally another
                        Dim iobj As BObject = it
                        If iobj.ObjectType <> "Class" Then
                            Continue For
                        End If
                        Dim ifinder As String = "new " & iobj.ObjectName ' Everywhere, like operator
                        Dim sposition As Integer = 0
                        'Dim _first As Boolean = False
                        Dim prevpos As Integer = -2
                        Do
                            sposition = allline.IndexOf(ifinder, sposition)
                            If prevpos >= sposition Or sposition < 0 Then
                                Exit Do
                            End If
                            prevpos = sposition
                            If (sposition > 0 AndAlso (Not acceptable_near.Contains(allline(sposition - 1)))) OrElse (sposition < allline.Length - ifinder.Length AndAlso (Not acceptable_near.Contains(allline(sposition + ifinder.Length)))) Then
                                sposition += ifinder.Length + 1
                                If sposition > allline.Length Then
                                    Exit Do
                                End If
                                Continue Do ' Not filtered!
                            End If
                            CodeData.SelectionStart = sposition + currentbegin
                            CodeData.SelectionLength = ifinder.Length
                            CodeData.SelectionColor = Color.DarkCyan
                            sposition += ifinder.Length + 1
                            If sposition > allline.Length Then
                                Exit Do
                            End If
                        Loop
                        Continue For
                    Else
                        Continue For
                    End If
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
                        CodeData.SelectionColor = selc
                        sp += i.Length + 1
                        If sp > allline.Length Then
                            Exit Do
                        End If
                    Loop
                Next
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
            Else
                ' HTML mode
                Dim inside_tag As Boolean = False
                Dim previous_tag As Integer = -1
                For i = 0 To allline.Length - 1
                    If allline(i) = "<"c Then
                        If Not inside_tag Then
                            previous_tag = i
                            inside_tag = True
                        End If
                    ElseIf allline(i) = ">"c AndAlso ((i = 0) OrElse (Not allline(i - 1) = "?"c)) Then
                        inside_tag = False
                        Dim cs As Integer = currentbegin + previous_tag
                        CodeData.SelectionStart = cs
                        ' i+1: Current '>' character
                        CodeData.SelectionLength = i + 1 - previous_tag
                        Dim tagtext As String = CodeData.SelectedText
                        Dim hinstring As Boolean = False
                        Dim start_html As Boolean = False
                        CodeData.SelectionColor = Color.Blue
                        For j = 0 To tagtext.Length - 1
                            If tagtext(j) = """"c Or hinstring Then
                                If Not start_html Then
                                    Continue For
                                End If
                                CodeData.SelectionStart = cs + j
                                CodeData.SelectionLength = 1
                                CodeData.SelectionColor = Color.DarkGray
                                If tagtext(j) = """"c Then
                                    hinstring = Not hinstring
                                End If
                            ElseIf tagtext(j) = " "c AndAlso (Not start_html) Then
                                start_html = True
                            ElseIf (tagtext(j) <> "="c) AndAlso (tagtext(j) <> ">"c) AndAlso (tagtext(j) <> "/"c) Then
                                If Not start_html Then
                                    Continue For
                                End If
                                CodeData.SelectionStart = cs + j
                                CodeData.SelectionLength = 1
                                CodeData.SelectionColor = Color.Red         ' Properties
                            ElseIf (tagtext(j) = ">"c) OrElse (tagtext(j) = "/"c) Then
                                CodeData.SelectionStart = cs + j
                                CodeData.SelectionLength = 1
                                CodeData.SelectionColor = Color.Blue
                            ElseIf tagtext(j) = "="c Then
                                CodeData.SelectionStart = cs + j
                                CodeData.SelectionLength = 1
                                CodeData.SelectionColor = Color.Black
                            End If
                        Next
                    End If
                Next
            End If
            ' After all override our quotes (It's common for all kinds.)

FinishA:    CodeData.Select(usercur, 0)
            CodeData.SelectionColor = Color.Black

            suspendScroller = False
        End If
vsc:    lineJustEdit = currentline
        If suspendScroller Then
            suspendScroller = False
        End If
    End Sub

    Private DuringUpdating As Boolean = False

    Private Sub CodeData_TextChanged(sender As Object, e As EventArgs) Handles CodeData.TextChanged
        upd = True
        sync = False
        CodeData.Font = New Font(GeneralFont, 11)
        ' Update my stuff here.
        If Not DuringUpdating Then
            saved = False
        End If
        'DuringUpdating = True
        CodeData_VScroll(sender, e)
        'DuringUpdating = False
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

    Public Sub SaveThisTo(filename As String, Optional noSaved As Boolean = False, Optional ByVal encoder As System.Text.Encoding = Nothing)
        If IsNothing(encoder) Then
            encoder = DefaultEncoder
        End If
        Dim s As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(filename, False, encoder)
        s.Write(CodeData.Text)
        s.Close()
        If Not noSaved Then
            saved = True
        End If

    End Sub

    Public Sub SelectSave(Optional ByVal updateCurrent As Boolean = True, Optional ByVal encoder As System.Text.Encoding = Nothing)
        sfd.FilterIndex = CurrentFilterIndex
        If sfd.ShowDialog() = DialogResult.OK Then
            SaveThisTo(sfd.FileName, False, encoder)
            If updateCurrent Then
                current = sfd.FileName
            End If
        End If
    End Sub

    Public Sub LoadSave()
        If current.Length > 0 Then
            SaveThisTo(current)
        Else
            SelectSave()
        End If
    End Sub

    ''' <summary>
    ''' Ask the user whether or not the file should be saved.
    ''' </summary>
    ''' <returns>True - Go on execution, False - Stop execution</returns>
    Public Function ClearCheck() As Boolean
        If Not saved Then
            Dim r = MsgBox("Current file " & current & " is not saved. Save?", MsgBoxStyle.YesNoCancel, "Prompt")
            Select Case r
                Case MsgBoxResult.Yes
                    LoadSave()
                Case MsgBoxResult.Cancel
                    Return False
            End Select
            Return True
        Else
            Return True         ' Always return true as here's no work
        End If
    End Function

    Public Sub Creating() Implements IDEChildInterface.Creating
        IsCreating = True
        FileKindSelector.Visible = True
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        Creating()
    End Sub


    Public WriteOnly Property CodeFieldVisible As Boolean
        Set(value As Boolean)
            LineLabel.Visible = value
            CodeData.Visible = value
            SaveToolStripMenuItem.Enabled = value
            SaveAsToolStripMenuItem.Enabled = value
            EditToolStripMenuItem.Enabled = value
            RunCodeToolStripMenuItem.Enabled = value
            DebugCodedebugToolStripMenuItem.Enabled = value
        End Set
    End Property

    Public ReadOnly Property HaveOwnCreator As Boolean Implements IDEChildInterface.HaveOwnCreator
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property IsViewer As Boolean Implements IDEChildInterface.IsViewer
        Get
            Return False
        End Get
    End Property

    Public Sub OpenHTML()
        IsPlainHTML = True
        SelectAKind(False)
    End Sub

    Public Sub ForceRefresh()
        suspendScroller = True
        CodeData.Enabled = False
        DuringUpdating = True
        For i = 0 To CodeData.GetLineFromCharIndex(CodeData.TextLength - 1)
            lineJustEdit = i
            LinearUpdate(True)
        Next
        DuringUpdating = False
        CodeData.Enabled = True
        suspendScroller = False
    End Sub

    Private _TmpFilename As String
    Private Sub FurtherOpener()
        Dim Filename = _TmpFilename
        Dim CurrentEncoder = Me.DefaultEncoder
        If RegardAsANSI.Checked Then
            CurrentEncoder = System.Text.Encoding.Default
            UseANSIAsDefaultEncodinginsteadOfUTF8ToolStripMenuItem.Checked = True
        Else
            ' Will judge that kid of file it is
            ' Read first character of it.
            Dim tryReader As IO.BinaryReader = New BinaryReader(File.Open(Filename, FileMode.Open))
            Dim firsts() As Byte = tryReader.ReadBytes(3)
            If firsts(0) >= 239 Then
                ' EF Header of file.
                ' Do nothing!
            ElseIf firsts(0) = 254 AndAlso firsts(1) = 255 Then
                CurrentEncoder = System.Text.Encoding.BigEndianUnicode
            ElseIf firsts(0) = 255 AndAlso firsts(1) = 254 Then
                CurrentEncoder = System.Text.Encoding.Unicode
            Else
                ' ANSI Here!
                CurrentEncoder = System.Text.Encoding.Default
                UseANSIAsDefaultEncodinginsteadOfUTF8ToolStripMenuItem.Checked = True
            End If
            tryReader.Close()
        End If
        Dim d As IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(Filename, CurrentEncoder)
        current = Filename
        CodeData.Text = d.ReadToEnd()
        d.Close()
        LoadObjectInfo(CodeData.Text)
        ForceRefresh()
        saved = True                    ' Initial time
    End Sub

    Private Sub OpenFileDialoger(Filename As String)
        IsCreating = False
        ShowFileSelector()
        RegardAsANSI.Visible = True
        OpeningPrompt.Text = "You are opening file " & Filename
    End Sub

    Public Sub OpenFile(Filename As String) Implements IDEChildInterface.OpenFile
        If Not My.Computer.FileSystem.FileExists(Filename) Then
            MsgBox("Specified file " & Filename & " does not exist!", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End If
        If ClearCheck() Then
            _TmpFilename = Filename
            FileKindSelector.Visible = False
            CodeFieldVisible = True
            IsPlainHTML = False
            Dim ext As String = GetExtension(Filename)
            Dim parenter As General = Me.MdiParent
            If IsNothing(parenter) OrElse (Not parenter.AlwaysAskFileType.Checked) Then
                Select Case ext
                    Case "blue"
                        SelectAKind(True)
                    Case "bp"
                        SelectAKind(False)
                    Case "js"
                        SelectAKind(False, True)
                    Case Else
                        If HTMLKinds.Contains(ext) Then
                            ' Process as HTML
                            OpenHTML()
                        Else
                            OpenFileDialoger(Filename)
                            Exit Sub
                        End If

                End Select
                FurtherOpener()
            Else
                Select Case ext
                    Case "blue"
                        BlueFile.Checked = True
                    Case "bp"
                        PageFile.Checked = True
                    Case "js"
                        JSFile.Checked = True
                    Case "html", "html", "xml"
                        HTMLFile.Checked = True
                    Case Else
                        PlainFile.Checked = True
                End Select
                OpenFileDialoger(Filename)
            End If

        End If
    End Sub

    Public Sub Opening() Implements IDEChildInterface.Opening
        ofd.FilterIndex = CurrentFilterIndex
        If ClearCheck() Then
            If ofd.ShowDialog() = DialogResult.OK Then
                OpenFile(ofd.FileName)
                Dim Gen As General = Me.MdiParent
                Gen.RecentFile.AddRecentFile(ofd.FileName)
                EditorStateUpdate()
                RunnerStateUpdate()
                'CodeHUpdate()
                'CodeData.Visible = True
            End If
        End If
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Opening()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        LoadSave()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        SelectSave()
    End Sub

    Private Sub QuitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem.Click
        If ClearCheck() Then
            Me.Close()
        End If
    End Sub

    Private Sub MainIDE_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = Not ClearCheck()
    End Sub

    Private PreparePath As String = Application.StartupPath & "\tcode.blue"

    Private Sub PrepareCurrentProgram()
        SaveThisTo(PreparePath, True, System.Text.Encoding.Default)
    End Sub

    Private Sub RunCurrentProgram(Optional parameter As String = "")
        PrepareCurrentProgram()
        Shell(Application.StartupPath & "\BlueBetter4.exe """ & PreparePath & """ " & parameter, AppWinStyle.NormalFocus)
    End Sub

    Private Sub RunCodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RunCodeToolStripMenuItem.Click
        RunCurrentProgram()
        'End If
    End Sub

    Private Sub DebugCodedebugToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DebugCodedebugToolStripMenuItem.Click
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
        'LineLabel.Font = New Font(GeneralFont, 11)
        LineLabel.Text = ""
        For i = beginline To endline
            LineLabel.Text = LineLabel.Text & Format(i + 1, "000") & vbCrLf
        Next
    End Sub

    Private Sub UpdateTimer_Tick(sender As Object, e As EventArgs)
        'CodeHUpdate()          ' This code is useless, but I don't know why it has lasted here for such a long time
        LoadIntelli()
    End Sub

    Private Sub LineLabel_GotFocus(sender As Object, e As EventArgs) Handles LineLabel.GotFocus
        CodeData.Select()
    End Sub

    Private Sub CodeData_SelectionChanged(sender As Object, e As EventArgs) Handles CodeData.SelectionChanged
        If Not suspendScroller Then
            DuringUpdating = True
            LinearUpdate()
            DuringUpdating = False
        End If

    End Sub

    ' Used as 'New' (or 'Open').
    Public Sub SelectAKind(IsBlueBetter As Boolean, Optional IsJS As Boolean = False)
        Me.isBluebetter = IsBlueBetter
        Me.isJS = IsJS
        If IsBlueBetter Then
            commanding_keywords.Remove("echo")
        Else
            commanding_keywords.Add("echo")
        End If
        FileKindSelector.Visible = False
        ClearCheck()
        CodeData.Text = ""
        current = ""
        'NoExecution = True '' CANNOT USE THIS !!!!!!!!!!
        CodeFieldVisible = True
        RunCodeToolStripMenuItem.Enabled = IsBlueBetter
        DebugCodedebugToolStripMenuItem.Enabled = IsBlueBetter
        If Not IsCreating Then
            FurtherOpener()
        End If
    End Sub

    Private Sub ConfirmOpening_Click(sender As Object, e As EventArgs) Handles ConfirmOpening.Click
        If ClearCheck() Then
            If BlueFile.Checked Then
                OverridenExtension = "blue"
                SelectAKind(True)
            ElseIf PageFile.Checked Then
                OverridenExtension = "bp"
                SelectAKind(False)
            ElseIf JSFile.Checked Then
                OverridenExtension = "js"
                SelectAKind(False, True)
            ElseIf HTMLFile.Checked Then
                OverridenExtension = "html"
                OpenHTML()
            ElseIf PlainFile.Checked Then
                OverridenExtension = "txt"
                NoExecution = True
                SelectAKind(False)
            Else
                MsgBox("Please select a file type!")
                Exit Sub
            End If
        End If
    End Sub

    Private Sub CancelOpening_Click(sender As Object, e As EventArgs) Handles CancelOpening.Click
        FileKindSelector.Visible = False
    End Sub

    Public Sub Saving() Implements IDEChildInterface.Saving
        LoadSave()
    End Sub

    Public Sub SavingAs() Implements IDEChildInterface.SavingAs
        SelectSave()
    End Sub

    Private Sub IDEChildInterface_Closing() Implements IDEChildInterface.Closing
        Me.Close()
    End Sub

    Public Sub RenameFile() Implements IDEChildInterface.RenameFile

    End Sub

    Private Sub EditorStateUpdate(Optional sender As Object = Nothing, Optional e As EventArgs = Nothing) Handles EditToolStripMenuItem.Click, EditToolStripMenuItem.MouseMove
        ' ...
        Dim ac As Boolean = Not (IsPlainHTML OrElse NoExecution OrElse isJS)
        SearchClassToolStripMenuItem.Enabled = ac
        AddStaticFileToolStripMenuItem.Enabled = ac
    End Sub

    Private Sub RunnerStateUpdate(Optional sender As Object = Nothing, Optional e As EventArgs = Nothing) Handles RunToolStripMenuItem.Click, RunToolStripMenuItem.MouseMove
        Dim ac As Boolean = (Not (IsPlainHTML OrElse NoExecution OrElse isJS)) AndAlso Me.isBluebetter
        RunToolStripMenuItem.Enabled = ac
        DebugCodedebugToolStripMenuItem.Enabled = ac
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Public Sub Deleting() Implements IDEChildInterface.Deleting

    End Sub

    Public Function ConfirmMasterClose() As Boolean Implements IDEChildInterface.ConfirmMasterClose
        Return Not ClearCheck()
    End Function

    Public Sub OpeningSpecified(Filename As String) Implements IDEChildInterface.OpeningSpecified
        OpenFile(Filename)
    End Sub

    Private Sub UseANSIAsDefaultEncodinginsteadOfUTF8ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UseANSIAsDefaultEncodinginsteadOfUTF8ToolStripMenuItem.Click
        ' The state must be changed!
        saved = False
    End Sub

    Private Sub ANSIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ANSIToolStripMenuItem.Click
        SelectSave(False, Encoding.Default)
    End Sub

    Private Sub UTF8ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UTF8ToolStripMenuItem.Click
        SelectSave(False, Encoding.UTF8)
    End Sub

    Private Sub RefreshHighlightToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshHighlightToolStripMenuItem.Click
        ForceRefresh()
    End Sub
End Class
