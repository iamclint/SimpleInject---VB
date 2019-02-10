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
        Me.RefreshProcesses = New System.Windows.Forms.Timer(Me.components)
        Me.AutoInjTmr = New System.Windows.Forms.Timer(Me.components)
        Me.Filters = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NotifyMessage = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DllBrowse64 = New System.Windows.Forms.Button()
        Me.DllPath64 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'DllPath
        '
        Me.DllPath.AllowDrop = True
        Me.DllPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DllPath.Location = New System.Drawing.Point(42, 11)
        Me.DllPath.Name = "DllPath"
        Me.DllPath.Size = New System.Drawing.Size(747, 20)
        Me.DllPath.TabIndex = 0
        '
        'DllBrowse
        '
        Me.DllBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DllBrowse.Location = New System.Drawing.Point(795, 9)
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
        Me.ProcessList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.col1, Me.ColumnHeader2, Me.ColumnHeader1, Me.ColumnHeader3})
        Me.ProcessList.FullRowSelect = True
        Me.ProcessList.HideSelection = False
        Me.ProcessList.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ProcessList.Location = New System.Drawing.Point(12, 64)
        Me.ProcessList.MultiSelect = False
        Me.ProcessList.Name = "ProcessList"
        Me.ProcessList.Size = New System.Drawing.Size(810, 349)
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
        Me.ColumnHeader2.Width = 415
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "ID"
        Me.ColumnHeader1.Width = 61
        '
        'RefreshProcesses
        '
        Me.RefreshProcesses.Enabled = True
        Me.RefreshProcesses.Interval = 2000
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
        Me.Filters.Location = New System.Drawing.Point(52, 420)
        Me.Filters.Name = "Filters"
        Me.Filters.Size = New System.Drawing.Size(770, 20)
        Me.Filters.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 423)
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
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "x86/x64"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(24, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "x86"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "x64"
        '
        'DllBrowse64
        '
        Me.DllBrowse64.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DllBrowse64.Location = New System.Drawing.Point(795, 36)
        Me.DllBrowse64.Name = "DllBrowse64"
        Me.DllBrowse64.Size = New System.Drawing.Size(24, 23)
        Me.DllBrowse64.TabIndex = 11
        Me.DllBrowse64.Text = "..."
        Me.DllBrowse64.UseVisualStyleBackColor = True
        '
        'DllPath64
        '
        Me.DllPath64.AllowDrop = True
        Me.DllPath64.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DllPath64.Location = New System.Drawing.Point(42, 38)
        Me.DllPath64.Name = "DllPath64"
        Me.DllPath64.Size = New System.Drawing.Size(747, 20)
        Me.DllPath64.TabIndex = 10
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(831, 447)
        Me.Controls.Add(Me.DllBrowse64)
        Me.Controls.Add(Me.DllPath64)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
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
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents DllBrowse64 As Button
    Friend WithEvents DllPath64 As TextBox
End Class
