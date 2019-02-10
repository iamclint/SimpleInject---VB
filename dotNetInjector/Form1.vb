'This code was written by clint.ban@gmail.com   
'You may use this code for learning purposes only.
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Xml.Serialization
Imports System.Xml
Public Class Form1
    Public Structure Proc
        Public process As String
        Public item As ListViewItem
    End Structure
    Public Processes As New Dictionary(Of Integer, Proc)
    <DllImport("libinj.dll", SetLastError:=True, CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Function is32bit(ByVal process_id As Integer) As Boolean
    End Function
    <DllImport("libinj.dll", SetLastError:=True, CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Function Inject(ByVal process_id As Integer, ByVal dll As String) As Boolean
    End Function

    Function filterProcesses(ByVal str As String) As Boolean
        Dim filters As String() = My.Settings.Filters.Split(";")
        For Each Filter As String In filters
            '    Dim x = InStr(Filter, "*")
            'If x > 0 Then
            Filter = Filter.Replace("*", "")
            If str.Length >= Filter.Length Then
                If (str.Substring(0, Filter.Length).ToLower = Filter.ToLower) Then
                    Return False
                End If
            End If
            'End If
            '   If Filter.ToLower = str.ToLower Then
            ' Return False
            ' End If
        Next
        Return True
    End Function

    Sub refreshProcessList()
        My.Settings.Filters = Filters.Text
        Dim ProcessIdList As List(Of Integer) = New List(Of Integer)

        'add new processes
        For Each p As Process In Process.GetProcesses()
            ProcessIdList.Add(p.Id)
            If Not filterProcesses(p.ProcessName) Or My.Settings.Filters = "" Then
                If Not Processes.ContainsKey(p.Id) Then
                    Dim cProc As New Proc
                    Dim new_item As New ListViewItem()
                    new_item.Text = p.ProcessName
                    new_item.Tag = p.Id
                    new_item.Name = p.Id
                    new_item.SubItems.Add(p.MainWindowTitle.Trim())
                    new_item.SubItems.Add(p.Id)
                    new_item.SubItems.Add(IIf(is32bit(p.Id), "x86", "x64"))
                    'new_item.SubItems.Add(p.MainModule.ModuleName.ToString)
                    'new_item.SubItems.Add("0x" & Hex(p.MainModule.BaseAddress.ToInt64).ToString)
                    ProcessList.Items.Add(new_item)
                    cProc.item = new_item
                    cProc.process = p.MainWindowTitle
                    Processes.Add(p.Id, cProc)
                Else 'Update the window
                    If (is32bit(p.Id)) Then
                        Dim window As String = p.MainWindowTitle.Trim()
                        Processes.Item(p.Id).item.SubItems(1).Text = window 'update the title
                    End If
                    End If
            End If
        Next

        Dim x As Integer
        For Each item As ListViewItem In ProcessList.Items
            Integer.TryParse(item.Name, x)
            If x <> 0 Then
                If Not ProcessIdList.Contains(x) Then
                    ProcessList.Items.Remove(item)
                End If
            End If
            x = 0
        Next
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DllPath.Text = My.Settings.FileToInject
        DllPath64.Text = My.Settings.FileToInject64
        Filters.Text = My.Settings.Filters
        If DllPath.Text = "" Then
            DllPath.Text = "Select a file to inject"
            NotifyMessage.ShowBalloonTip(3000, "Help", "Select the file you want to inject via the ... button", ToolTipIcon.Info)
        End If
        refreshProcessList()
    End Sub
   

    Private Sub DllBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DllBrowse.Click
        Dim ld As OpenFileDialog = New System.Windows.Forms.OpenFileDialog
        With ld
            .DefaultExt = ".dll"
            .Filter = "Dll Files|*.dll"
            .InitialDirectory = Application.StartupPath
        End With
        If ld.ShowDialog = Windows.Forms.DialogResult.OK Then
            DllPath.Text = ld.FileName
            My.Settings.FileToInject = ld.FileName
        End If
    End Sub

    Private Sub RefreshProcesses_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshProcesses.Tick
        refreshProcessList()
    End Sub

    Private Function isValidFile() As Boolean
        If ProcessList.SelectedItems.Count <= 0 Then
            Return False
        End If
        If Not File.Exists(DllPath.Text) Then
            Return False
        End If
        Return True
    End Function

    Private Sub ProcessList_DoubleClick(sender As Object, e As EventArgs) Handles ProcessList.DoubleClick
        'If Not isValidFile() Then
        '    NotifyMessage.ShowBalloonTip(3000, "Error", "Invalid file to inject", ToolTipIcon.Error)
        '    Exit Sub
        'End If

        My.Settings.FileToInject = DllPath.Text
        Dim pID As Integer = Integer.Parse(ProcessList.SelectedItems(0).SubItems(2).Text.ToString)
        Dim title As String
        If (ProcessList.SelectedItems(0).SubItems(3).Text.ToString = "x86") Then
            title = DllPath.Text
            Inject(pID, DllPath.Text)
        Else
            title = DllPath64.Text
            Inject(pID, DllPath64.Text)
        End If
        If Marshal.GetLastWin32Error() <> 0 Then
            NotifyMessage.ShowBalloonTip(3000, "Error", "Injection failed!" & vbCrLf & "Win32 Error #" & Marshal.GetLastWin32Error(), ToolTipIcon.Error)
        Else
            NotifyMessage.ShowBalloonTip(3000, "Injection Successful!", title.Substring(DllPath.Text.LastIndexOf("\") + 1, title.Length - title.LastIndexOf("\") - 1) & " -> " & Integer.Parse(ProcessList.SelectedItems(0).SubItems(0).Text.ToString), ToolTipIcon.Info)
        End If
        'Dim handle As Integer = Integer.Parse(ProcessList.SelectedItems(0).SubItems(2).Text.ToString)
        'Dim proc As String = ""
        'Try
        '    Dim pl As New cMem.ProcessList()
        '    For Each p As Process In pl.Processes.Values
        '        If handle = p.Id Then
        '            mem.Attach(p)
        '            proc = p.ProcessName
        '        End If
        '    Next

        '    Dim str = DllPath.Text
        '    str = str.Substring(0, str.LastIndexOf("\")) & "\awe\"
        '    mem.Inject(DllPath.Text)
        '    mem.Detach()
        '    If Marshal.GetLastWin32Error() <> 0 Then
        '        NotifyMessage.ShowBalloonTip(3000, "Error", "Injection failed!" & vbCrLf & "Win32 Error #" & Marshal.GetLastWin32Error(), ToolTipIcon.Error)
        '        'mem.Inject64(DllPath.Text)
        '        'If Marshal.GetLastWin32Error() <> 0 Then
        '        '    NotifyMessage.ShowBalloonTip(3000, "Error", "Injection failed!" & vbCrLf & "Win32 Error #" & Marshal.GetLastWin32Error(), ToolTipIcon.Error)
        '        'Else
        '        '    NotifyMessage.ShowBalloonTip(3000, "Injection Successful!", DllPath.Text.Substring(DllPath.Text.LastIndexOf("\") + 1, DllPath.Text.Length - DllPath.Text.LastIndexOf("\") - 1) & " -> " & proc, ToolTipIcon.Info)
        '        'End If
        '    Else
        '        NotifyMessage.ShowBalloonTip(3000, "Injection Successful!", DllPath.Text.Substring(DllPath.Text.LastIndexOf("\") + 1, DllPath.Text.Length - DllPath.Text.LastIndexOf("\") - 1) & " -> " & proc, ToolTipIcon.Info)
        '    End If

        'Catch ex As Exception
        '    Console.WriteLine(ex.StackTrace)
        '    Console.WriteLine(ex.Message)
        '    NotifyMessage.ShowBalloonTip(3000, "Application Error", ex.Message, ToolTipIcon.Error)
        'End Try
    End Sub

    Private Sub NotifyMessage_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyMessage.MouseDoubleClick
        Me.WindowState = FormWindowState.Normal
        Me.Show()
    End Sub

    Private Sub ProcessList_DragDrop(sender As Object, e As DragEventArgs) Handles ProcessList.DragDrop, DllPath.DragDrop
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            Dim Files() As String
            Files = e.Data.GetData(DataFormats.FileDrop)
            If Files.Length > 0 Then
                DllPath.Text = Files(0).ToString
            End If
        End If
    End Sub

    Private Sub ProcessList_DragEnter(sender As Object, e As DragEventArgs) Handles ProcessList.DragEnter, DllPath.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            ' Display the copy cursor.
            e.Effect = DragDropEffects.All
        Else
            ' Display the no-drop cursor.
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub ProcessList_KeyUp(sender As Object, e As KeyEventArgs) Handles ProcessList.KeyUp
        'If e.KeyCode = Keys.Delete Then
        '    If ProcessList.SelectedItems.Count > 0 Then
        '        Try
        '            Dim handle As Integer = Integer.Parse(ProcessList.SelectedItems(0).SubItems(2).Text.ToString)
        '            Dim proc As String = ""
        '            Dim pl As New cMem.ProcessList()
        '            For Each p As Process In pl.Processes.Values
        '                If handle = p.Id Then
        '                    mem.Attach(p)
        '                    proc = p.ProcessName
        '                End If
        '            Next
        '            NotifyMessage.ShowBalloonTip(3000, "Kill Process", "Attempting to kill -> " & proc, ToolTipIcon.Info)
        '            mem.Process_Obj.Kill()
        '        Catch ex As Exception
        '            NotifyMessage.ShowBalloonTip(3000, "Kill Process", ex.Message, ToolTipIcon.Error)
        '        End Try
        '    End If
        'ElseIf e.KeyCode = Keys.Space Then
        '    If ProcessList.SelectedItems.Count > 0 Then
        '        If Filters.Text.Substring(Filters.Text.Length - 1, 1) = ";" Then
        '            Filters.Text &= ProcessList.SelectedItems(0).SubItems(0).Text & ";"
        '        Else
        '            Filters.Text &= ";" & ProcessList.SelectedItems(0).SubItems(0).Text & ";"
        '        End If
        '    End If
        'ElseIf e.KeyCode = Keys.Enter Then
        '    ProcessList_DoubleClick(Nothing, Nothing)
        'End If
    End Sub

    Private Sub ProcessList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ProcessList.SelectedIndexChanged

    End Sub

    Private Sub DllBrowse64_Click(sender As Object, e As EventArgs) Handles DllBrowse64.Click
        Dim ld As OpenFileDialog = New System.Windows.Forms.OpenFileDialog
        With ld
            .DefaultExt = ".dll"
            .Filter = "Dll Files|*.dll"
            .InitialDirectory = Application.StartupPath
        End With
        If ld.ShowDialog = Windows.Forms.DialogResult.OK Then
            DllPath64.Text = ld.FileName
            My.Settings.FileToInject64 = ld.FileName
        End If
    End Sub
End Class
