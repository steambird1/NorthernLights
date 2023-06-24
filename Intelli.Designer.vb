<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Intelli
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
        Me.ElemViewer = New System.Windows.Forms.TreeView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SearchMask = New System.Windows.Forms.TextBox()
        Me.InfoBox = New System.Windows.Forms.GroupBox()
        Me.DetName = New System.Windows.Forms.Label()
        Me.DetDisplayer = New System.Windows.Forms.Label()
        Me.InfoBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'ElemViewer
        '
        Me.ElemViewer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ElemViewer.Location = New System.Drawing.Point(0, 40)
        Me.ElemViewer.Name = "ElemViewer"
        Me.ElemViewer.Size = New System.Drawing.Size(524, 365)
        Me.ElemViewer.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Search for:"
        '
        'SearchMask
        '
        Me.SearchMask.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SearchMask.Location = New System.Drawing.Point(114, 9)
        Me.SearchMask.Name = "SearchMask"
        Me.SearchMask.Size = New System.Drawing.Size(650, 25)
        Me.SearchMask.TabIndex = 2
        '
        'InfoBox
        '
        Me.InfoBox.Controls.Add(Me.DetDisplayer)
        Me.InfoBox.Controls.Add(Me.DetName)
        Me.InfoBox.Location = New System.Drawing.Point(531, 41)
        Me.InfoBox.Name = "InfoBox"
        Me.InfoBox.Size = New System.Drawing.Size(233, 364)
        Me.InfoBox.TabIndex = 3
        Me.InfoBox.TabStop = False
        Me.InfoBox.Text = "Details"
        '
        'DetName
        '
        Me.DetName.AutoSize = True
        Me.DetName.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.DetName.Location = New System.Drawing.Point(19, 37)
        Me.DetName.Name = "DetName"
        Me.DetName.Size = New System.Drawing.Size(0, 15)
        Me.DetName.TabIndex = 0
        '
        'DetDisplayer
        '
        Me.DetDisplayer.Location = New System.Drawing.Point(22, 72)
        Me.DetDisplayer.Name = "DetDisplayer"
        Me.DetDisplayer.Size = New System.Drawing.Size(188, 259)
        Me.DetDisplayer.TabIndex = 1
        Me.DetDisplayer.Text = "Select something from the left to show details."
        '
        'Intelli
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(776, 405)
        Me.Controls.Add(Me.InfoBox)
        Me.Controls.Add(Me.SearchMask)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ElemViewer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "Intelli"
        Me.Text = "Intelligent Analyzer"
        Me.InfoBox.ResumeLayout(False)
        Me.InfoBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ElemViewer As TreeView
    Friend WithEvents Label1 As Label
    Friend WithEvents SearchMask As TextBox
    Friend WithEvents InfoBox As GroupBox
    Friend WithEvents DetDisplayer As Label
    Friend WithEvents DetName As Label
End Class
