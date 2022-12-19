<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectRecents
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
        Me.RecentFiles = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.OpenNow = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'RecentFiles
        '
        Me.RecentFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RecentFiles.FormattingEnabled = True
        Me.RecentFiles.ItemHeight = 15
        Me.RecentFiles.Location = New System.Drawing.Point(1, 40)
        Me.RecentFiles.Name = "RecentFiles"
        Me.RecentFiles.Size = New System.Drawing.Size(802, 379)
        Me.RecentFiles.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(335, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Select recent file/project from the list:"
        '
        'OpenNow
        '
        Me.OpenNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OpenNow.Enabled = False
        Me.OpenNow.Location = New System.Drawing.Point(677, 425)
        Me.OpenNow.Name = "OpenNow"
        Me.OpenNow.Size = New System.Drawing.Size(115, 35)
        Me.OpenNow.TabIndex = 2
        Me.OpenNow.Text = "Open"
        Me.OpenNow.UseVisualStyleBackColor = True
        '
        'SelectRecents
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(804, 464)
        Me.Controls.Add(Me.OpenNow)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RecentFiles)
        Me.Name = "SelectRecents"
        Me.Text = "Select Recent File/Project"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RecentFiles As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents OpenNow As Button
End Class
