<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class General
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenRecentFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.NewProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenRecentWebsite = New System.Windows.Forms.ToolStripMenuItem()
        Me.QuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProjectOptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.RunWebsiteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DebugMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewSpecified = New System.Windows.Forms.ToolStripMenuItem()
        Me.WindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CascadeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TiltToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TiltVerticalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ArrangeIconsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutBlueBetterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutBluePageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutMinserverTour = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutIDEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.fbd = New System.Windows.Forms.FolderBrowserDialog()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ProjectToolStripMenuItem, Me.WindowToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.MdiWindowListItem = Me.WindowToolStripMenuItem
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(843, 28)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.ToolStripMenuItem1, Me.OpenRecentFiles, Me.ToolStripMenuItem2, Me.ToolStripMenuItem3, Me.ToolStripMenuItem7, Me.ToolStripMenuItem5, Me.ToolStripMenuItem4, Me.ToolStripSeparator1, Me.NewProjectToolStripMenuItem, Me.OpenRecentWebsite, Me.QuitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(46, 24)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(356, 26)
        Me.NewToolStripMenuItem.Text = "New..."
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(356, 26)
        Me.ToolStripMenuItem1.Text = "Open file..."
        '
        'OpenRecentFiles
        '
        Me.OpenRecentFiles.Name = "OpenRecentFiles"
        Me.OpenRecentFiles.Size = New System.Drawing.Size(356, 26)
        Me.OpenRecentFiles.Text = "Open Recent Files..."
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(356, 26)
        Me.ToolStripMenuItem2.Text = "Save file"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(356, 26)
        Me.ToolStripMenuItem3.Text = "Save file as ..."
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(356, 26)
        Me.ToolStripMenuItem7.Text = "Rename..."
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.ShortcutKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.Delete), System.Windows.Forms.Keys)
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(356, 26)
        Me.ToolStripMenuItem5.Text = "Delete file"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(356, 26)
        Me.ToolStripMenuItem4.Text = "Close file"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(353, 6)
        '
        'NewProjectToolStripMenuItem
        '
        Me.NewProjectToolStripMenuItem.Name = "NewProjectToolStripMenuItem"
        Me.NewProjectToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.NewProjectToolStripMenuItem.Size = New System.Drawing.Size(356, 26)
        Me.NewProjectToolStripMenuItem.Text = "Open/Create Website..."
        '
        'OpenRecentWebsite
        '
        Me.OpenRecentWebsite.Name = "OpenRecentWebsite"
        Me.OpenRecentWebsite.Size = New System.Drawing.Size(356, 26)
        Me.OpenRecentWebsite.Text = "Open Recent Website..."
        '
        'QuitToolStripMenuItem
        '
        Me.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem"
        Me.QuitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.QuitToolStripMenuItem.Size = New System.Drawing.Size(356, 26)
        Me.QuitToolStripMenuItem.Text = "Quit"
        '
        'ProjectToolStripMenuItem
        '
        Me.ProjectToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem6, Me.ProjectOptions, Me.ToolStripSeparator2, Me.RunWebsiteToolStripMenuItem, Me.DebugMenu, Me.ViewSpecified})
        Me.ProjectToolStripMenuItem.Name = "ProjectToolStripMenuItem"
        Me.ProjectToolStripMenuItem.Size = New System.Drawing.Size(81, 24)
        Me.ProjectToolStripMenuItem.Text = "Website"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(359, 26)
        Me.ToolStripMenuItem6.Text = "Project viewer..."
        '
        'ProjectOptions
        '
        Me.ProjectOptions.Name = "ProjectOptions"
        Me.ProjectOptions.Size = New System.Drawing.Size(359, 26)
        Me.ProjectOptions.Text = "Project options..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(356, 6)
        '
        'RunWebsiteToolStripMenuItem
        '
        Me.RunWebsiteToolStripMenuItem.Name = "RunWebsiteToolStripMenuItem"
        Me.RunWebsiteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10
        Me.RunWebsiteToolStripMenuItem.Size = New System.Drawing.Size(359, 26)
        Me.RunWebsiteToolStripMenuItem.Text = "Run website..."
        '
        'DebugMenu
        '
        Me.DebugMenu.Name = "DebugMenu"
        Me.DebugMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F10), System.Windows.Forms.Keys)
        Me.DebugMenu.Size = New System.Drawing.Size(359, 26)
        Me.DebugMenu.Text = "Debug website... (--debug)"
        '
        'ViewSpecified
        '
        Me.ViewSpecified.Checked = True
        Me.ViewSpecified.CheckOnClick = True
        Me.ViewSpecified.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ViewSpecified.Name = "ViewSpecified"
        Me.ViewSpecified.Size = New System.Drawing.Size(359, 26)
        Me.ViewSpecified.Text = "View specified file when running"
        '
        'WindowToolStripMenuItem
        '
        Me.WindowToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CascadeToolStripMenuItem, Me.TiltToolStripMenuItem, Me.TiltVerticalToolStripMenuItem, Me.ArrangeIconsToolStripMenuItem, Me.ToolStripSeparator3})
        Me.WindowToolStripMenuItem.Name = "WindowToolStripMenuItem"
        Me.WindowToolStripMenuItem.Size = New System.Drawing.Size(81, 24)
        Me.WindowToolStripMenuItem.Text = "Window"
        '
        'CascadeToolStripMenuItem
        '
        Me.CascadeToolStripMenuItem.Name = "CascadeToolStripMenuItem"
        Me.CascadeToolStripMenuItem.Size = New System.Drawing.Size(187, 26)
        Me.CascadeToolStripMenuItem.Text = "Cascade"
        '
        'TiltToolStripMenuItem
        '
        Me.TiltToolStripMenuItem.Name = "TiltToolStripMenuItem"
        Me.TiltToolStripMenuItem.Size = New System.Drawing.Size(187, 26)
        Me.TiltToolStripMenuItem.Text = "Tilt Horizontal"
        '
        'TiltVerticalToolStripMenuItem
        '
        Me.TiltVerticalToolStripMenuItem.Name = "TiltVerticalToolStripMenuItem"
        Me.TiltVerticalToolStripMenuItem.Size = New System.Drawing.Size(187, 26)
        Me.TiltVerticalToolStripMenuItem.Text = "Tilt Vertical"
        '
        'ArrangeIconsToolStripMenuItem
        '
        Me.ArrangeIconsToolStripMenuItem.Name = "ArrangeIconsToolStripMenuItem"
        Me.ArrangeIconsToolStripMenuItem.Size = New System.Drawing.Size(187, 26)
        Me.ArrangeIconsToolStripMenuItem.Text = "Arrange Icons"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(184, 6)
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutBlueBetterToolStripMenuItem, Me.AboutBluePageToolStripMenuItem, Me.AboutMinserverTour, Me.AboutIDEToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(64, 24)
        Me.HelpToolStripMenuItem.Text = "Utility"
        '
        'AboutBlueBetterToolStripMenuItem
        '
        Me.AboutBlueBetterToolStripMenuItem.Name = "AboutBlueBetterToolStripMenuItem"
        Me.AboutBlueBetterToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.AboutBlueBetterToolStripMenuItem.Text = "About BlueBetter"
        '
        'AboutBluePageToolStripMenuItem
        '
        Me.AboutBluePageToolStripMenuItem.Name = "AboutBluePageToolStripMenuItem"
        Me.AboutBluePageToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.AboutBluePageToolStripMenuItem.Text = "About BluePage"
        '
        'AboutMinserverTour
        '
        Me.AboutMinserverTour.Name = "AboutMinserverTour"
        Me.AboutMinserverTour.Size = New System.Drawing.Size(216, 26)
        Me.AboutMinserverTour.Text = "About MinServer"
        '
        'AboutIDEToolStripMenuItem
        '
        Me.AboutIDEToolStripMenuItem.Name = "AboutIDEToolStripMenuItem"
        Me.AboutIDEToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.AboutIDEToolStripMenuItem.Text = "About IDE"
        '
        'General
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(843, 523)
        Me.Controls.Add(Me.MenuStrip1)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "General"
        Me.Text = "NorthernLights Web IDE"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewProjectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents QuitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProjectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RunWebsiteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents fbd As FolderBrowserDialog
    Friend WithEvents ToolStripMenuItem6 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem7 As ToolStripMenuItem
    Friend WithEvents ViewSpecified As ToolStripMenuItem
    Friend WithEvents WindowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As ToolStripMenuItem
    Friend WithEvents CascadeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TiltToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TiltVerticalToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ArrangeIconsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutBlueBetterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutBluePageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutIDEToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DebugMenu As ToolStripMenuItem
    Friend WithEvents AboutMinserverTour As ToolStripMenuItem
    Friend WithEvents ProjectOptions As ToolStripMenuItem
    Friend WithEvents OpenRecentFiles As ToolStripMenuItem
    Friend WithEvents OpenRecentWebsite As ToolStripMenuItem
End Class
