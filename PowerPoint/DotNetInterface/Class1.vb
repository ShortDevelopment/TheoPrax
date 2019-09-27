Imports System.Runtime.InteropServices
<ComVisible(True)>
Public Class LKTest
    <DllImport("user32.dll")>
    Public Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function
    <DllImport("user32.dll")>
    Public Shared Function SendMessage(hWnd As IntPtr, wMsg As Integer, wParam As IntPtr, lParam As IntPtr) As Integer
    End Function
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Shared Function RegisterWindowMessage(lpString As String) As UInteger
    End Function
    Public Sub New()

    End Sub
    Const WindowTitle As String = "ArduinoCommunication"
    Public Sub Show(Optional path As String = "")
        If Process.GetProcessesByName("livedisplay").Length = 0 Then
            Process.Start(path + "\livedisplay.exe")
        Else
            Dim id = RegisterWindowMessage("Show:ArduinoCommunication")
            Dim WindowToFind = FindWindow(Nothing, WindowTitle)
            Debug.Assert(WindowToFind)
            SendMessage(WindowToFind, id, IntPtr.Zero, IntPtr.Zero)
        End If
    End Sub
    Public Sub SendMessage(msg As String)
        Dim id = RegisterWindowMessage(msg)
        Dim WindowToFind = FindWindow(Nothing, WindowTitle)
        Debug.Assert(WindowToFind)
        SendMessage(WindowToFind, id, IntPtr.Zero, IntPtr.Zero)
    End Sub
End Class