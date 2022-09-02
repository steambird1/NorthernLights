<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainIDE
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim UpdateTimer As System.Windows.Forms.Timer
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.QuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FindToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchClassToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddStaticFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RunToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RunCodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DebugCodedebugToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VersionInformationversionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CodeData = New System.Windows.Forms.RichTextBox()
        Me.Searcher = New System.Windows.Forms.GroupBox()
        Me.Closer = New System.Windows.Forms.Button()
        Me.Replacer = New System.Windows.Forms.Button()
        Me.SchPush = New System.Windows.Forms.Button()
        Me.ReplaceBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SearchBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ofd = New System.Windows.Forms.OpenFileDialog()
        Me.sfd = New System.Windows.Forms.SaveFileDialog()
        Me.LineLabel0 = New System.Windows.Forms.Label()
        Me.LineLabel = New System.Windows.Forms.RichTextBox()
        Me.Notify1 = New System.Windows.Forms.Label()
        UpdateTimer = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1.SuspendLayout()
        Me.Searcher.SuspendLayout()
        Me.SuspendLayout()
        '
        'UpdateTimer
        '
        UpdateTimer.Enabled = True
        UpdateTimer.Interval = 1000
        AddHandler UpdateTimer.Tick, AddressOf Me.UpdateTimer_Tick
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.RunToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(861, 28)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.QuitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(46, 24)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(194, 26)
        Me.NewToolStripMenuItem.Text = "New"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(194, 26)
        Me.OpenToolStripMenuItem.Text = "Open..."
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(194, 26)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(194, 26)
        Me.SaveAsToolStripMenuItem.Text = "Save as..."
        '
        'QuitToolStripMenuItem
        '
        Me.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem"
        Me.QuitToolStripMenuItem.Size = New System.Drawing.Size(194, 26)
        Me.QuitToolStripMenuItem.Text = "Quit"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FindToolStripMenuItem, Me.SearchClassToolStripMenuItem, Me.AddStaticFileToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(49, 24)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'FindToolStripMenuItem
        '
        Me.FindToolStripMenuItem.Name = "FindToolStripMenuItem"
        Me.FindToolStripMenuItem.Size = New System.Drawing.Size(278, 26)
        Me.FindToolStripMenuItem.Text = "Find and Replace..."
        '
        'SearchClassToolStripMenuItem
        '
        Me.SearchClassToolStripMenuItem.Name = "SearchClassToolStripMenuItem"
        Me.SearchClassToolStripMenuItem.Size = New System.Drawing.Size(278, 26)
        Me.SearchClassToolStripMenuItem.Text = "Search class and function..."
        '
        'AddStaticFileToolStripMenuItem
        '
        Me.AddStaticFileToolStripMenuItem.Name = "AddStaticFileToolStripMenuItem"
        Me.AddStaticFileToolStripMenuItem.Size = New System.Drawing.Size(278, 26)
        Me.AddStaticFileToolStripMenuItem.Text = "Add static file..."
        '
        'RunToolStripMenuItem
        '
        Me.RunToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RunCodeToolStripMenuItem, Me.DebugCodedebugToolStripMenuItem, Me.VersionInformationversionToolStripMenuItem})
        Me.RunToolStripMenuItem.Name = "RunToolStripMenuItem"
        Me.RunToolStripMenuItem.Size = New System.Drawing.Size(49, 24)
        Me.RunToolStripMenuItem.Text = "Run"
        '
        'RunCodeToolStripMenuItem
        '
        Me.RunCodeToolStripMenuItem.Name = "RunCodeToolStripMenuItem"
        Me.RunCodeToolStripMenuItem.Size = New System.Drawing.Size(307, 26)
        Me.RunCodeToolStripMenuItem.Text = "Run code"
        '
        'DebugCodedebugToolStripMenuItem
        '
        Me.DebugCodedebugToolStripMenuItem.Name = "DebugCodedebugToolStripMenuItem"
        Me.DebugCodedebugToolStripMenuItem.Size = New System.Drawing.Size(307, 26)
        Me.DebugCodedebugToolStripMenuItem.Text = "Debug code (--debug)"
        '
        'VersionInformationversionToolStripMenuItem
        '
        Me.VersionInformationversionToolStripMenuItem.Name = "VersionInformationversionToolStripMenuItem"
        Me.VersionInformationversionToolStripMenuItem.Size = New System.Drawing.Size(307, 26)
        Me.VersionInformationversionToolStripMenuItem.Text = "Version information (--version)"
        '
        'CodeData
        '
        Me.CodeData.AcceptsTab = True
        Me.CodeData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CodeData.Font = New System.Drawing.Font("Lucida Sans Typewriter", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CodeData.Location = New System.Drawing.Point(52, 31)
        Me.CodeData.Name = "CodeData"
        Me.CodeData.Size = New System.Drawing.Size(809, 420)
        Me.CodeData.TabIndex = 1
        Me.CodeData.Text = ""
        '
        'Searcher
        '
        Me.Searcher.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Searcher.Controls.Add(Me.Closer)
        Me.Searcher.Controls.Add(Me.Replacer)
        Me.Searcher.Controls.Add(Me.SchPush)
        Me.Searcher.Controls.Add(Me.ReplaceBox)
        Me.Searcher.Controls.Add(Me.Label2)
        Me.Searcher.Controls.Add(Me.SearchBox)
        Me.Searcher.Controls.Add(Me.Label1)
        Me.Searcher.Location = New System.Drawing.Point(0, 399)
        Me.Searcher.Name = "Searcher"
        Me.Searcher.Size = New System.Drawing.Size(861, 52)
        Me.Searcher.TabIndex = 2
        Me.Searcher.TabStop = False
        Me.Searcher.Text = "Search and replace"
        Me.Searcher.Visible = False
        '
        'Closer
        '
        Me.Closer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Closer.Location = New System.Drawing.Point(838, 0)
        Me.Closer.Name = "Closer"
        Me.Closer.Size = New System.Drawing.Size(23, 23)
        Me.Closer.TabIndex = 6
        Me.Closer.Text = "X"
        Me.Closer.UseVisualStyleBackColor = True
        '
        'Replacer
        '
        Me.Replacer.Location = New System.Drawing.Point(745, 23)
        Me.Replacer.Name = "Replacer"
        Me.Replacer.Size = New System.Drawing.Size(104, 23)
        Me.Replacer.TabIndex = 5
        Me.Replacer.Text = "Replace"
        Me.Replacer.UseVisualStyleBackColor = True
        '
        'SchPush
        '
        Me.SchPush.Location = New System.Drawing.Point(609, 24)
        Me.SchPush.Name = "SchPush"
        Me.SchPush.Size = New System.Drawing.Size(110, 23)
        Me.SchPush.TabIndex = 4
        Me.SchPush.Text = "Search"
        Me.SchPush.UseVisualStyleBackColor = True
        '
        'ReplaceBox
        '
        Me.ReplaceBox.Location = New System.Drawing.Point(377, 20)
        Me.ReplaceBox.Name = "ReplaceBox"
        Me.ReplaceBox.Size = New System.Drawing.Size(206, 25)
        Me.ReplaceBox.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(291, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Replace:"
        '
        'SearchBox
        '
        Me.SearchBox.Location = New System.Drawing.Point(76, 20)
        Me.SearchBox.Name = "SearchBox"
        Me.SearchBox.Size = New System.Drawing.Size(199, 25)
        Me.SearchBox.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Search:"
        '
        'ofd
        '
        Me.ofd.Filter = "BlueBetter file|*.blue|All files|*.*"
        '
        'sfd
        '
        Me.sfd.Filter = "BlueBetter file|*.blue|All files|*.*"
        '
        'LineLabel0
        '
        Me.LineLabel0.Font = New System.Drawing.Font("新宋体", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LineLabel0.Location = New System.Drawing.Point(343, 47)
        Me.LineLabel0.Margin = New System.Windows.Forms.Padding(3)
        Me.LineLabel0.Name = "LineLabel0"
        Me.LineLabel0.Size = New System.Drawing.Size(46, 420)
        Me.LineLabel0.TabIndex = 3
        '
        'LineLabel
        '
        Me.LineLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LineLabel.BackColor = System.Drawing.Color.LightGray
        Me.LineLabel.Font = New System.Drawing.Font("新宋体", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LineLabel.Location = New System.Drawing.Point(0, 31)
        Me.LineLabel.Name = "LineLabel"
        Me.LineLabel.ReadOnly = True
        Me.LineLabel.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.LineLabel.Size = New System.Drawing.Size(55, 420)
        Me.LineLabel.TabIndex = 4
        Me.LineLabel.Text = ""
        '
        'Notify1
        '
        Me.Notify1.AutoSize = True
        Me.Notify1.Location = New System.Drawing.Point(294, 230)
        Me.Notify1.Name = "Notify1"
        Me.Notify1.Size = New System.Drawing.Size(319, 15)
        Me.Notify1.TabIndex = 5
        Me.Notify1.Text = "Please wait until the file is shown ..."
        '
        'MainIDE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(861, 450)
        Me.Controls.Add(Me.Searcher)
        Me.Controls.Add(Me.CodeData)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.LineLabel0)
        Me.Controls.Add(Me.LineLabel)
        Me.Controls.Add(Me.Notify1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainIDE"
        Me.Text = "BlueBetter IDE"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Searcher.ResumeLayout(False)
        Me.Searcher.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents QuitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FindToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SearchClassToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RunToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RunCodeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DebugCodedebugToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CodeData As RichTextBox
    Friend WithEvents Searcher As GroupBox
    Friend WithEvents Replacer As Button
    Friend WithEvents SchPush As Button
    Friend WithEvents ReplaceBox As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents SearchBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Closer As Button
    Friend WithEvents ofd As OpenFileDialog
    Friend WithEvents sfd As SaveFileDialog
    Friend WithEvents AddStaticFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LineLabel0 As Label
    Friend WithEvents LineLabel As RichTextBox
    Friend WithEvents Notify1 As Label
    Friend WithEvents VersionInformationversionToolStripMenuItem As ToolStripMenuItem
End Class
