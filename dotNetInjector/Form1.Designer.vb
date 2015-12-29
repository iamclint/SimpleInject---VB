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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.DllPath = New System.Windows.Forms.TextBox()
        Me.DllBrowse = New System.Windows.Forms.Button()
        Me.ProcessList = New System.Windows.Forms.ListView()
        Me.col1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Column3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.RefreshProcesses = New System.Windows.Forms.Timer(Me.components)
        Me.AutoInjTmr = New System.Windows.Forms.Timer(Me.components)
        Me.Filters = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NotifyMessage = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.SuspendLayout()
        '
        'DllPath
        '
        Me.DllPath.AllowDrop = True
        Me.DllPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DllPath.Location = New System.Drawing.Point(12, 11)
        Me.DllPath.Name = "DllPath"
        Me.DllPath.Size = New System.Drawing.Size(679, 20)
        Me.DllPath.TabIndex = 0
        '
        'DllBrowse
        '
        Me.DllBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DllBrowse.Location = New System.Drawing.Point(697, 9)
        Me.DllBrowse.Name = "DllBrowse"
        Me.DllBrowse.Size = New System.Drawing.Size(24, 23)
        Me.DllBrowse.TabIndex = 1
        Me.DllBrowse.Text = "..."
        Me.DllBrowse.UseVisualStyleBackColor = True
        '
        'ProcessList
        '
        Me.ProcessList.AllowDrop = True
        Me.ProcessList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProcessList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.col1, Me.ColumnHeader2, Me.ColumnHeader1, Me.Column3, Me.ColumnHeader3})
        Me.ProcessList.FullRowSelect = True
        Me.ProcessList.HideSelection = False
        Me.ProcessList.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ProcessList.Location = New System.Drawing.Point(12, 38)
        Me.ProcessList.MultiSelect = False
        Me.ProcessList.Name = "ProcessList"
        Me.ProcessList.Size = New System.Drawing.Size(712, 284)
        Me.ProcessList.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ProcessList.TabIndex = 3
        Me.ProcessList.UseCompatibleStateImageBehavior = False
        Me.ProcessList.View = System.Windows.Forms.View.Details
        '
        'col1
        '
        Me.col1.Text = "Name"
        Me.col1.Width = 173
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Title"
        Me.ColumnHeader2.Width = 438
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "ID"
        Me.ColumnHeader1.Width = 61
        '
        'Column3
        '
        Me.Column3.Text = "Main Module"
        Me.Column3.Width = 182
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Main Module Base Address"
        Me.ColumnHeader3.Width = 142
        '
        'RefreshProcesses
        '
        Me.RefreshProcesses.Enabled = True
        Me.RefreshProcesses.Interval = 500
        '
        'AutoInjTmr
        '
        Me.AutoInjTmr.Enabled = True
        Me.AutoInjTmr.Interval = 500
        '
        'Filters
        '
        Me.Filters.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Filters.Location = New System.Drawing.Point(52, 328)
        Me.Filters.Name = "Filters"
        Me.Filters.Size = New System.Drawing.Size(672, 20)
        Me.Filters.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 331)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Filters"
        '
        'NotifyMessage
        '
        Me.NotifyMessage.Icon = CType(resources.GetObject("NotifyMessage.Icon"), System.Drawing.Icon)
        Me.NotifyMessage.Visible = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(733, 355)
        Me.Controls.Add(Me.ProcessList)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Filters)
        Me.Controls.Add(Me.DllBrowse)
        Me.Controls.Add(Me.DllPath)
        Me.Name = "Form1"
        Me.ShowIcon = False
        Me.Text = "Simple Inject"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DllPath As System.Windows.Forms.TextBox
    Friend WithEvents DllBrowse As System.Windows.Forms.Button
    Friend WithEvents ProcessList As System.Windows.Forms.ListView
    Friend WithEvents col1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents RefreshProcesses As System.Windows.Forms.Timer
    Friend WithEvents AutoInjTmr As System.Windows.Forms.Timer
    Friend WithEvents Filters As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NotifyMessage As System.Windows.Forms.NotifyIcon
    Friend WithEvents Column3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader

End Class
