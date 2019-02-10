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
            Filter = Filter.Replace("*", "")
            If str.Length >= Filter.Length Then
                If (str.Substring(0, Filter.Length).ToLower = Filter.ToLower) Then
                    Return False
                End If
            End If
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
                If Not Processes.ContainsKey(p.Id) And p.Id <> 0 Then
                    Dim cProc As New Proc
                    Dim new_item As New ListViewItem()
                    new_item.Text = p.ProcessName
                    new_item.Tag = p.Id
                    new_item.Name = p.Id
                    new_item.SubItems.Add(p.MainWindowTitle.Trim())
                    new_item.SubItems.Add(p.Id)
                    new_item.SubItems.Add(IIf(is32bit(p.Id), "x86", "x64"))
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
            Else
                Console.WriteLine("Removing " & p.Id)
                Processes.Remove(p.Id)
                ProcessIdList.Remove(p.Id)
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
            .InitialDirectory = My.Settings.FileToInject.Substring(0, My.Settings.FileToInject64.LastIndexOf("\"))
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
        My.Settings.FileToInject = DllPath.Text
        My.Settings.FileToInject64 = DllPath64.Text
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
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub ProcessList_DragEnter64(sender As Object, e As DragEventArgs) Handles DllPath64.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub DllBrowse64_Click(sender As Object, e As EventArgs) Handles DllBrowse64.Click
        Dim ld As OpenFileDialog = New System.Windows.Forms.OpenFileDialog
        With ld
            .DefaultExt = ".dll"
            .Filter = "Dll Files|*.dll"
            .InitialDirectory = My.Settings.FileToInject64.Substring(0, My.Settings.FileToInject64.LastIndexOf("\"))
        End With
        If ld.ShowDialog = Windows.Forms.DialogResult.OK Then
            DllPath64.Text = ld.FileName
            My.Settings.FileToInject64 = ld.FileName
        End If
    End Sub
End Class
