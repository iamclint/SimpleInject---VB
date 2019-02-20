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
    Public Shared Function Inject(ByVal process_id As Integer, ByVal dll As String, ByVal method As Integer, Optional ByVal handle As IntPtr = Nothing) As Boolean
    End Function
    <DllImport("libinj.dll", SetLastError:=True, CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Function getLastError() As String
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
            If Not filterProcesses(p.ProcessName) And p.Id > 0 Then
                ProcessIdList.Add(p.Id) 'keep a list of all process id's so they can be removed when no longer valid
                If Not Processes.ContainsKey(p.Id) Then
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
                Else
                    Dim window As String = p.MainWindowTitle.Trim()
                    Processes.Item(p.Id).item.SubItems(1).Text = window 'update the title
                End If
            End If
        Next

        Dim removeList As List(Of Integer) = New List(Of Integer)
        For Each process_id As Integer In Processes.Keys
            If Not ProcessIdList.Contains(process_id) Then 'if the process is still in our local processes dictionary but its no longer a valid process we need to remove it
                removeList.Add(process_id)
            End If
        Next

        For Each process_id As Integer In removeList
            Console.WriteLine("Removing from dictionary: " & Processes(process_id).process)
            Processes.Remove(process_id)
        Next


        Dim x As Integer
        For Each item As ListViewItem In ProcessList.Items
            Integer.TryParse(item.Name, x)
            If x <> 0 Then
                If Not Processes.ContainsKey(x) Then
                    Console.WriteLine("Removing from listview: " & item.SubItems(0).Text)
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
            If (My.Settings.FileToInject.LastIndexOf("\") > 0) Then
                .InitialDirectory = My.Settings.FileToInject.Substring(0, My.Settings.FileToInject.LastIndexOf("\"))
            End If
        End With
        If ld.ShowDialog = Windows.Forms.DialogResult.OK Then
            DllPath.Text = ld.FileName
            My.Settings.FileToInject = ld.FileName
        End If
    End Sub

    Private Sub RefreshProcesses_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshProcesses.Tick
        refreshProcessList()
    End Sub

    Private Function isValidFile(ByVal file_path As String) As Boolean
        If Not File.Exists(file_path) Then
            Return False
        End If
        Return True
    End Function

    Private Sub ProcessList_DoubleClick(sender As Object, e As EventArgs) Handles ProcessList.DoubleClick
        My.Settings.FileToInject = DllPath.Text
        My.Settings.FileToInject64 = DllPath64.Text
        Dim pID As Integer = Integer.Parse(ProcessList.SelectedItems(0).SubItems(2).Text.ToString)
        Dim title As String
        Dim balloonText As String
        If (ProcessList.SelectedItems(0).SubItems(3).Text.ToString = "x86") Then
            title = DllPath.Text
            If (Not isValidFile(DllPath.Text)) Then
                NotifyMessage.ShowBalloonTip(3000, "Error", "Invalid file selected: " & DllPath.Text, ToolTipIcon.Error)
                Exit Sub
            End If

            If Not Inject(pID, DllPath.Text, 1) Then
                NotifyMessage.ShowBalloonTip(3000, "Error", getLastError(), ToolTipIcon.Error)
            Else
                balloonText = title.Substring(title.LastIndexOf("\") + 1, title.Length - title.LastIndexOf("\") - 1) & " -> " & ProcessList.SelectedItems(0).SubItems(0).Text.ToString
                NotifyMessage.ShowBalloonTip(3000, "Injection Successful!", balloonText, ToolTipIcon.Info)
            End If
        Else
            title = DllPath64.Text
            If (Not isValidFile(DllPath64.Text)) Then
                NotifyMessage.ShowBalloonTip(3000, "Error", "Invalid file selected: " & DllPath64.Text, ToolTipIcon.Error)
                Exit Sub
            End If
            If Not Inject(pID, DllPath64.Text, 1) Then
                NotifyMessage.ShowBalloonTip(3000, "Error", getLastError(), ToolTipIcon.Error)
            Else
                balloonText = title.Substring(title.LastIndexOf("\") + 1, title.Length - title.LastIndexOf("\") - 1) & " -> " & ProcessList.SelectedItems(0).SubItems(0).Text.ToString
                NotifyMessage.ShowBalloonTip(3000, "Injection Successful!", balloonText, ToolTipIcon.Info)
            End If
        End If
    End Sub

    Private Sub NotifyMessage_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyMessage.MouseDoubleClick
        Me.WindowState = FormWindowState.Normal
        Me.Show()
    End Sub

    Private Sub DllBrowse64_Click(sender As Object, e As EventArgs) Handles DllBrowse64.Click
        Dim ld As OpenFileDialog = New System.Windows.Forms.OpenFileDialog
        With ld
            .DefaultExt = ".dll"
            .Filter = "Dll Files|*.dll"
            If (My.Settings.FileToInject64.LastIndexOf("\") > 0) Then
                .InitialDirectory = My.Settings.FileToInject64.Substring(0, My.Settings.FileToInject64.LastIndexOf("\"))
            End If
        End With
        If ld.ShowDialog = Windows.Forms.DialogResult.OK Then
            DllPath64.Text = ld.FileName
            My.Settings.FileToInject64 = ld.FileName
        End If
    End Sub
End Class
