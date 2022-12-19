<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProjectSettings
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PortData = New System.Windows.Forms.TextBox()
        Me.Error500Path = New System.Windows.Forms.TextBox()
        Me.Error404Path = New System.Windows.Forms.TextBox()
        Me.ExtensionsData = New System.Windows.Forms.TextBox()
        Me.SaveFiler = New System.Windows.Forms.SaveFileDialog()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Server Port:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(127, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "500 Error Page:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(127, 15)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "404 Error Page:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 114)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 15)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Extension:"
        '
        'PortData
        '
        Me.PortData.Location = New System.Drawing.Point(146, 11)
        Me.PortData.Name = "PortData"
        Me.PortData.Size = New System.Drawing.Size(100, 25)
        Me.PortData.TabIndex = 4
        Me.PortData.Text = "80"
        '
        'Error500Path
        '
        Me.Error500Path.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Error500Path.Location = New System.Drawing.Point(146, 44)
        Me.Error500Path.Name = "Error500Path"
        Me.Error500Path.Size = New System.Drawing.Size(319, 25)
        Me.Error500Path.TabIndex = 5
        Me.Error500Path.Text = "500.html"
        '
        'Error404Path
        '
        Me.Error404Path.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Error404Path.Location = New System.Drawing.Point(146, 75)
        Me.Error404Path.Name = "Error404Path"
        Me.Error404Path.Size = New System.Drawing.Size(319, 25)
        Me.Error404Path.TabIndex = 6
        Me.Error404Path.Text = "404.html"
        '
        'ExtensionsData
        '
        Me.ExtensionsData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExtensionsData.Location = New System.Drawing.Point(146, 108)
        Me.ExtensionsData.Name = "ExtensionsData"
        Me.ExtensionsData.Size = New System.Drawing.Size(319, 25)
        Me.ExtensionsData.TabIndex = 7
        '
        'SaveFiler
        '
        Me.SaveFiler.Filter = "NorthernLights Website Project Settings|*.nlproj|All Files|*.*"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(146, 149)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(327, 15)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Example: .txt=text/plain;.bat=text/plain"
        '
        'ProjectSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(490, 184)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ExtensionsData)
        Me.Controls.Add(Me.Error404Path)
        Me.Controls.Add(Me.Error500Path)
        Me.Controls.Add(Me.PortData)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.MinimumSize = New System.Drawing.Size(304, 198)
        Me.Name = "ProjectSettings"
        Me.Text = "Project Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents PortData As TextBox
    Friend WithEvents Error500Path As TextBox
    Friend WithEvents Error404Path As TextBox
    Friend WithEvents ExtensionsData As TextBox
    Friend WithEvents SaveFiler As SaveFileDialog
    Friend WithEvents Label5 As Label
End Class
