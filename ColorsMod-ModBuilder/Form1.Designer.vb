<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.VerticalProgressBar3 = New ColorsMod_ModBuilder.VerticalProgressBar()
        Me.VerticalProgressBar2 = New ColorsMod_ModBuilder.VerticalProgressBar()
        Me.VerticalProgressBar1 = New ColorsMod_ModBuilder.VerticalProgressBar()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.Location = New System.Drawing.Point(13, 67)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(379, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Start"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Location = New System.Drawing.Point(13, 39)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(54, 17)
        Me.CheckBox1.TabIndex = 4
        Me.CheckBox1.Text = ".smod"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(13, 13)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(310, 20)
        Me.TextBox1.TabIndex = 5
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.Control
        Me.Button2.Location = New System.Drawing.Point(329, 11)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(63, 23)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "Browse"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.SystemColors.Control
        Me.Button3.Location = New System.Drawing.Point(329, 39)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(63, 23)
        Me.Button3.TabIndex = 10
        Me.Button3.Text = "EditColors"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(73, 39)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(84, 17)
        Me.CheckBox2.TabIndex = 11
        Me.CheckBox2.Text = "delete folder"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'VerticalProgressBar3
        '
        Me.VerticalProgressBar3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VerticalProgressBar3.BackColor = System.Drawing.Color.AliceBlue
        Me.VerticalProgressBar3.ForeColor = System.Drawing.Color.Blue
        Me.VerticalProgressBar3.Location = New System.Drawing.Point(443, 9)
        Me.VerticalProgressBar3.Name = "VerticalProgressBar3"
        Me.VerticalProgressBar3.Size = New System.Drawing.Size(15, 81)
        Me.VerticalProgressBar3.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.VerticalProgressBar3.TabIndex = 8
        Me.VerticalProgressBar3.Value = 10
        '
        'VerticalProgressBar2
        '
        Me.VerticalProgressBar2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VerticalProgressBar2.BackColor = System.Drawing.Color.Ivory
        Me.VerticalProgressBar2.ForeColor = System.Drawing.Color.Gold
        Me.VerticalProgressBar2.Location = New System.Drawing.Point(422, 9)
        Me.VerticalProgressBar2.Name = "VerticalProgressBar2"
        Me.VerticalProgressBar2.Size = New System.Drawing.Size(15, 81)
        Me.VerticalProgressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.VerticalProgressBar2.TabIndex = 7
        Me.VerticalProgressBar2.Value = 10
        '
        'VerticalProgressBar1
        '
        Me.VerticalProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VerticalProgressBar1.BackColor = System.Drawing.Color.MistyRose
        Me.VerticalProgressBar1.ForeColor = System.Drawing.Color.Red
        Me.VerticalProgressBar1.Location = New System.Drawing.Point(401, 9)
        Me.VerticalProgressBar1.Name = "VerticalProgressBar1"
        Me.VerticalProgressBar1.Size = New System.Drawing.Size(15, 81)
        Me.VerticalProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.VerticalProgressBar1.TabIndex = 6
        Me.VerticalProgressBar1.Value = 10
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(470, 102)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.VerticalProgressBar3)
        Me.Controls.Add(Me.VerticalProgressBar2)
        Me.Controls.Add(Me.VerticalProgressBar1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "Colors Mod: Mod Builder"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents VerticalProgressBar1 As ColorsMod_ModBuilder.VerticalProgressBar
    Friend WithEvents VerticalProgressBar2 As ColorsMod_ModBuilder.VerticalProgressBar
    Friend WithEvents VerticalProgressBar3 As ColorsMod_ModBuilder.VerticalProgressBar
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog

End Class
