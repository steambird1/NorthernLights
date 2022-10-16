<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProjectViewer
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DocumentTree = New System.Windows.Forms.TreeView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SearchContent = New System.Windows.Forms.TextBox()
        Me.FSWatcher = New System.IO.FileSystemWatcher()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.TreeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateDirectoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.FSWatcher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DocumentTree
        '
        Me.DocumentTree.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DocumentTree.LabelEdit = True
        Me.DocumentTree.Location = New System.Drawing.Point(0, 71)
        Me.DocumentTree.Name = "DocumentTree"
        Me.DocumentTree.Size = New System.Drawing.Size(531, 337)
        Me.DocumentTree.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Search:"
        '
        'SearchContent
        '
        Me.SearchContent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SearchContent.Location = New System.Drawing.Point(82, 37)
        Me.SearchContent.Name = "SearchContent"
        Me.SearchContent.Size = New System.Drawing.Size(436, 25)
        Me.SearchContent.TabIndex = 2
        '
        'FSWatcher
        '
        Me.FSWatcher.EnableRaisingEvents = True
        Me.FSWatcher.IncludeSubdirectories = True
        Me.FSWatcher.SynchronizingObject = Me
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TreeToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(530, 28)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'TreeToolStripMenuItem
        '
        Me.TreeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateDirectoryToolStripMenuItem})
        Me.TreeToolStripMenuItem.Name = "TreeToolStripMenuItem"
        Me.TreeToolStripMenuItem.Size = New System.Drawing.Size(54, 24)
        Me.TreeToolStripMenuItem.Text = "Tree"
        '
        'CreateDirectoryToolStripMenuItem
        '
        Me.CreateDirectoryToolStripMenuItem.Name = "CreateDirectoryToolStripMenuItem"
        Me.CreateDirectoryToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.CreateDirectoryToolStripMenuItem.Text = "Create directory"
        '
        'ProjectViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(530, 408)
        Me.Controls.Add(Me.SearchContent)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DocumentTree)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "ProjectViewer"
        Me.Text = "Project Viewer"
        CType(Me.FSWatcher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DocumentTree As TreeView
    Friend WithEvents Label1 As Label
    Friend WithEvents SearchContent As TextBox
    Friend WithEvents FSWatcher As IO.FileSystemWatcher
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents TreeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CreateDirectoryToolStripMenuItem As ToolStripMenuItem
End Class
