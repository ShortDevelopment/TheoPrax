Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Threading

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel1.Location = New Point(Me.Width / 2 - Panel1.Width / 2, Me.Height / 2 - Panel1.Height / 2)
    End Sub
#Region "Logoff"
    <DllImport("wtsapi32.dll", SetLastError:=True)>
    Public Shared Function WTSDisconnectSession(hServer As IntPtr, sessionId As Integer, bWait As Boolean) As Boolean : End Function
    Const WTS_CURRENT_SESSION As Integer = -1
    Shared ReadOnly WTS_CURRENT_SERVER_HANDLE As IntPtr = IntPtr.Zero
    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function ExitWindowsEx(uFlags As UInteger, dwReason As UInteger) As Boolean : End Function
#End Region
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label4.Text = "Bitte folgen Sie den folgenden Anweisungen." + vbNewLine + "Zum Starten einfach auf ""Jetzt beginnen"" klicken!"
        Button1.Text = "Jetzt beginnen"
        If Not WTSDisconnectSession(WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION, False) Then
            If Not ExitWindowsEx(0 Or &H4, 0) Then
                MsgBox("Der aktuelle Benutzer konnte nicht abgemeldet werden!", MsgBoxStyle.Critical)
            End If
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            For Each p As Process In Process.GetProcessesByName("tskmgr")
                p.Kill()
            Next
            For Each p As Process In Process.GetProcessesByName("Taskmgr")
                p.Kill()
            Next
        Catch : End Try
    End Sub
    Private Sub Button1_Click(sender As Button, e As EventArgs) Handles Button1.Click
        If sender.Text = "Jetzt beginnen" Then
            For Each p As Process In Process.GetProcessesByName("livedisplay")
                p.Kill()
            Next
            For Each p As Process In Process.GetProcessesByName("POWERPNT")
                p.Kill()
            Next
            With My.Computer.FileSystem
                Dim mi = SoundHelper.GetMixerControls()
                'SoundHelper.AdjustVolume(mi, 100)
                'SoundHelper.SetMute(mi, True)
                'Speak("Hallo. Ich führe dich durch das Setup!")
                Speak("Hello. I will lead you threw the setup!")
                If .FileExists(Application.StartupPath + "\livedisplay.exe") AndAlso .FileExists(Application.StartupPath + "\ppt.ppsm") Then
                    Button1.Text = "PPT starten"
                    'Speak("Bitte wählen Sie jetzt den Port aus, an dem der Arduino hängt." + vbNewLine + "Klicken Sie anschließen auf das PowerPoint Symbol um die Präsentation zu starten")
                    Speak("Please select the Port connected to the Arduino." + vbNewLine + "Then click on Start Application." + vbNewLine + "After that, click on Start Ppt")
                    Process.Start(Application.StartupPath + "\livedisplay.exe")
                Else
                    MsgBox("Achtung!" + vbNewLine + "Das Setup ist unvollständig!" + vbNewLine + "Das Programm versucht das Setup wiederherzustellen...", MsgBoxStyle.Critical)
                    MsgBox("Das Programm konnte das Setup nicht wiederherzustellen. Kontaktieren Sie bitte Lukas Kurz (vb.net@kurzweb.de)", MsgBoxStyle.Critical)
                End If
            End With
        ElseIf Button1.Text = "PPT starten" Then
            Process.Start(Application.StartupPath + "\ppt.ppsm")
        End If
    End Sub
    Dim v As Object
    Sub Speak(text As String)
        Try
            If v Is Nothing Then
                v = CreateObject("Sapi.Spvoice")
            End If
        Catch : End Try
        Dim t As New Thread(Sub()
                                v.speak(text)
                            End Sub)
        t.IsBackground = True
        t.Priority = ThreadPriority.Highest
        t.Start()
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
    End Sub
End Class
Friend Class SoundHelper
    Const MAXPNAMELEN As Integer = 32
    Const MIXER_SHORT_NAME_CHARS As Integer = 16
    Const MIXER_LONG_NAME_CHARS As Integer = 64

    <Flags>
    Friend Enum MIXERLINE_LINEF
        ACTIVE = &H1
        DISCONNECTED = &H8000
        SOURCE = &H80000000
    End Enum

    <Flags>
    Friend Enum MIXER
        GETLINEINFOF_DESTINATION = &H0
        GETLINEINFOF_SOURCE = &H1
        GETLINEINFOF_LINEID = &H2
        GETLINEINFOF_COMPONENTTYPE = &H3
        GETLINEINFOF_TARGETTYPE = &H4
        GETLINEINFOF_QUERYMASK = &HF
        GETLINECONTROLSF_ALL = &H0
        GETLINECONTROLSF_ONEBYID = &H1
        GETLINECONTROLSF_ONEBYTYPE = &H2
        GETLINECONTROLSF_QUERYMASK = &HF
        GETCONTROLDETAILSF_VALUE = &H0
        GETCONTROLDETAILSF_LISTTEXT = &H1
        GETCONTROLDETAILSF_QUERYMASK = &HF
        OBJECTF_MIXER = &H0
        OBJECTF_WAVEOUT = &H10000000
        OBJECTF_WAVEIN = &H20000000
        OBJECTF_MIDIOUT = &H30000000
        OBJECTF_MIDIIN = &H40000000
        OBJECTF_AUX = &H50000000
        OBJECTF_HANDLE = &H80000000
        OBJECTF_HMIXER = OBJECTF_HANDLE Or OBJECTF_MIXER
        OBJECTF_HWAVEOUT = OBJECTF_HANDLE Or OBJECTF_WAVEOUT
        OBJECTF_HWAVEIN = OBJECTF_HANDLE Or OBJECTF_WAVEIN
        OBJECTF_HMIDIOUT = OBJECTF_HANDLE Or OBJECTF_MIDIOUT
        OBJECTF_HMIDIIN = OBJECTF_HANDLE Or OBJECTF_MIDIIN
    End Enum

    <Flags>
    Friend Enum MIXERCONTROL_CT
        CLASS_MASK = &HF0000000
        CLASS_CUSTOM = &H0
        CLASS_METER = &H10000000
        CLASS_SWITCH = &H20000000
        CLASS_NUMBER = &H30000000
        CLASS_SLIDER = &H40000000
        CLASS_FADER = &H50000000
        CLASS_TIME = &H60000000
        CLASS_LIST = &H70000000
        SUBCLASS_MASK = &HF000000
        SC_SWITCH_BOOLEAN = &H0
        SC_SWITCH_BUTTON = &H1000000
        SC_METER_POLLED = &H0
        SC_TIME_MICROSECS = &H0
        SC_TIME_MILLISECS = &H1000000
        SC_LIST_SINGLE = &H0
        SC_LIST_MULTIPLE = &H1000000
        UNITS_MASK = &HFF0000
        UNITS_CUSTOM = &H0
        UNITS_BOOLEAN = &H10000
        UNITS_SIGNED = &H20000
        UNITS_UNSIGNED = &H30000
        UNITS_DECIBELS = &H40000
        UNITS_PERCENT = &H50000
    End Enum

    <Flags>
    Friend Enum MIXERCONTROL_CONTROLTYPE As UInteger
        CUSTOM = MIXERCONTROL_CT.CLASS_CUSTOM Or MIXERCONTROL_CT.UNITS_CUSTOM
        BOOLEANMETER = MIXERCONTROL_CT.CLASS_METER Or MIXERCONTROL_CT.SC_METER_POLLED Or MIXERCONTROL_CT.UNITS_BOOLEAN
        SIGNEDMETER = MIXERCONTROL_CT.CLASS_METER Or MIXERCONTROL_CT.SC_METER_POLLED Or MIXERCONTROL_CT.UNITS_SIGNED
        PEAKMETER = SIGNEDMETER + 1
        UNSIGNEDMETER = MIXERCONTROL_CT.CLASS_METER Or MIXERCONTROL_CT.SC_METER_POLLED Or MIXERCONTROL_CT.UNITS_UNSIGNED
        [Boolean] = MIXERCONTROL_CT.CLASS_SWITCH Or MIXERCONTROL_CT.SC_SWITCH_BOOLEAN Or MIXERCONTROL_CT.UNITS_BOOLEAN
        ONOFF = [Boolean] + 1
        MUTE = [Boolean] + 2
        MONO = [Boolean] + 3
        LOUDNESS = [Boolean] + 4
        STEREOENH = [Boolean] + 5
        BASS_BOOST = [Boolean] + &H2277
        BUTTON = MIXERCONTROL_CT.CLASS_SWITCH Or MIXERCONTROL_CT.SC_SWITCH_BUTTON Or MIXERCONTROL_CT.UNITS_BOOLEAN
        DECIBELS = MIXERCONTROL_CT.CLASS_NUMBER Or MIXERCONTROL_CT.UNITS_DECIBELS
        SIGNED = MIXERCONTROL_CT.CLASS_NUMBER Or MIXERCONTROL_CT.UNITS_SIGNED
        UNSIGNED = MIXERCONTROL_CT.CLASS_NUMBER Or MIXERCONTROL_CT.UNITS_UNSIGNED
        PERCENT = MIXERCONTROL_CT.CLASS_NUMBER Or MIXERCONTROL_CT.UNITS_PERCENT
        SLIDER = MIXERCONTROL_CT.CLASS_SLIDER Or MIXERCONTROL_CT.UNITS_SIGNED
        PAN = SLIDER + 1
        QSOUNDPAN = SLIDER + 2
        FADER = MIXERCONTROL_CT.CLASS_FADER Or MIXERCONTROL_CT.UNITS_UNSIGNED
        VOLUME = FADER + 1
        BASS = FADER + 2
        TREBLE = FADER + 3
        EQUALIZER = FADER + 4
        SINGLESELECT = MIXERCONTROL_CT.CLASS_LIST Or MIXERCONTROL_CT.SC_LIST_SINGLE Or MIXERCONTROL_CT.UNITS_BOOLEAN
        MUX = SINGLESELECT + 1
        MULTIPLESELECT = MIXERCONTROL_CT.CLASS_LIST Or MIXERCONTROL_CT.SC_LIST_MULTIPLE Or MIXERCONTROL_CT.UNITS_BOOLEAN
        MIXER = MULTIPLESELECT + 1
        MICROTIME = MIXERCONTROL_CT.CLASS_TIME Or MIXERCONTROL_CT.SC_TIME_MICROSECS Or MIXERCONTROL_CT.UNITS_UNSIGNED
        MILLITIME = MIXERCONTROL_CT.CLASS_TIME Or MIXERCONTROL_CT.SC_TIME_MILLISECS Or MIXERCONTROL_CT.UNITS_UNSIGNED
    End Enum

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Friend Structure MIXERLINE
        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
        Public Structure TargetInfo
            Public dwType As UInteger
            Public dwDeviceID As UInteger
            Public wMid As UShort
            Public wPid As UShort
            Public vDriverVersion As UInteger
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAXPNAMELEN)>
            Public szPname As String
        End Structure

        Public cbStruct As UInteger
        Public dwDestination As UInteger
        Public dwSource As UInteger
        Public dwLineID As UInteger
        Public fdwLine As MIXERLINE_LINEF
        Public dwUser As UInteger
        Public dwComponentType As UInteger
        Public cChannels As UInteger
        Public cConnection As UInteger
        Public cControls As UInteger
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MIXER_SHORT_NAME_CHARS)>
        Public szShortName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MIXER_LONG_NAME_CHARS)>
        Public szName As String
        Public Target As TargetInfo
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Friend Structure MIXERCONTROL
        <StructLayout(LayoutKind.Explicit)>
        Public Structure BoundsInfo
            <FieldOffset(0)>
            Public lMinimum As Integer
            <FieldOffset(4)>
            Public lMaximum As Integer
            <FieldOffset(0)>
            Public dwMinimum As UInteger
            <FieldOffset(4)>
            Public dwMaximum As UInteger
            <FieldOffset(8), MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)>
            Public dwReserved As UInteger()
        End Structure

        <StructLayout(LayoutKind.Explicit)>
        Public Structure MetricsInfo
            <FieldOffset(0)>
            Public cSteps As UInteger
            <FieldOffset(0)>
            Public cbCustomData As UInteger
            <FieldOffset(4), MarshalAs(UnmanagedType.ByValArray, SizeConst:=5)>
            Public dwReserved As UInteger()
        End Structure

        Public cbStruct As UInteger
        Public dwControlID As UInteger
        Public dwControlType As MIXERCONTROL_CONTROLTYPE
        Public fdwControl As UInteger
        Public cMultipleItems As UInteger
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MIXER_SHORT_NAME_CHARS)>
        Public szShortName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MIXER_LONG_NAME_CHARS)>
        Public szName As String
        Public Bounds As BoundsInfo
        Public Metrics As MetricsInfo
    End Structure

    <StructLayout(LayoutKind.Explicit)>
    Friend Structure MIXERLINECONTROLS
        <FieldOffset(0)>
        Public cbStruct As UInteger
        <FieldOffset(4)>
        Public dwLineID As UInteger
        <FieldOffset(8)>
        Public dwControlID As UInteger
        <FieldOffset(8)>
        Public dwControlType As UInteger
        <FieldOffset(12)>
        Public cControls As UInteger
        <FieldOffset(16)>
        Public cbmxctrl As UInteger
        <FieldOffset(20)>
        Public pamxctrl As IntPtr
    End Structure

    <StructLayout(LayoutKind.Explicit)>
    Friend Structure MIXERCONTROLDETAILS
        <FieldOffset(0)>
        Public cbStruct As UInteger
        <FieldOffset(4)>
        Public dwControlID As UInteger
        <FieldOffset(8)>
        Public cChannels As UInteger
        <FieldOffset(12)>
        Public hwndOwner As IntPtr
        <FieldOffset(12)>
        Public cMultipleItems As UInteger
        <FieldOffset(16)>
        Public cbDetails As UInteger
        <FieldOffset(20)>
        Public paDetails As IntPtr
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Friend Structure VOLUME
        Public left As Integer
        Public right As Integer
    End Structure

    Friend Structure MixerInfo
        Public volumeCtl As UInteger
        Public muteCtl As UInteger
        Public minVolume As Integer
        Public maxVolume As Integer
    End Structure

    <DllImport("WinMM.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function mixerGetLineInfo(ByVal hmxobj As IntPtr, ByRef pmxl As MIXERLINE, ByVal flags As MIXER) As UInteger

    End Function
    <DllImport("WinMM.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function mixerGetLineControls(ByVal hmxobj As IntPtr, ByRef pmxlc As MIXERLINECONTROLS, ByVal flags As MIXER) As UInteger

    End Function
    <DllImport("WinMM.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function mixerGetControlDetails(ByVal hmxobj As IntPtr, ByRef pmxcd As MIXERCONTROLDETAILS, ByVal flags As MIXER) As UInteger

    End Function
    <DllImport("WinMM.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function mixerSetControlDetails(ByVal hmxobj As IntPtr, ByRef pmxcd As MIXERCONTROLDETAILS, ByVal flags As MIXER) As UInteger

    End Function

    Public Shared Function GetMixerControls() As MixerInfo
        Dim mxl As MIXERLINE = New MIXERLINE()
        Dim mlc As MIXERLINECONTROLS = New MIXERLINECONTROLS()
        mxl.cbStruct = CUInt(Marshal.SizeOf(GetType(MIXERLINE)))
        mlc.cbStruct = CUInt(Marshal.SizeOf(GetType(MIXERLINECONTROLS)))
        mixerGetLineInfo(IntPtr.Zero, mxl, MIXER.OBJECTF_MIXER Or MIXER.GETLINEINFOF_DESTINATION)
        mlc.dwLineID = mxl.dwLineID
        mlc.cControls = mxl.cControls
        mlc.cbmxctrl = CUInt(Marshal.SizeOf(GetType(MIXERCONTROL)))
        mlc.pamxctrl = Marshal.AllocHGlobal(CInt((mlc.cbmxctrl * mlc.cControls)))
        mixerGetLineControls(IntPtr.Zero, mlc, MIXER.OBJECTF_MIXER Or MIXER.GETLINECONTROLSF_ALL)
        Dim rtn As MixerInfo = New MixerInfo()

        For i As Integer = 0 To mlc.cControls - 1
            Dim mxc As MIXERCONTROL = CType(Marshal.PtrToStructure(CType((CInt(mlc.pamxctrl) + CInt(mlc.cbmxctrl) * i), IntPtr), GetType(MIXERCONTROL)), MIXERCONTROL)

            Select Case mxc.dwControlType
                Case MIXERCONTROL_CONTROLTYPE.VOLUME
                    rtn.volumeCtl = mxc.dwControlID
                    rtn.minVolume = mxc.Bounds.lMinimum
                    rtn.maxVolume = mxc.Bounds.lMaximum
                Case MIXERCONTROL_CONTROLTYPE.MUTE
                    rtn.muteCtl = mxc.dwControlID
            End Select
        Next

        Marshal.FreeHGlobal(mlc.pamxctrl)
        'MsgBox(New Win32Exception(Marshal.GetLastWin32Error()).Message)
        Return rtn
    End Function

    Private Shared Function GetVolume(ByVal mi As MixerInfo) As VOLUME
        Dim mcd As MIXERCONTROLDETAILS = New MIXERCONTROLDETAILS()
        mcd.cbStruct = CUInt(Marshal.SizeOf(GetType(MIXERCONTROLDETAILS)))
        mcd.dwControlID = mi.volumeCtl
        mcd.cMultipleItems = 0
        mcd.cChannels = 2
        mcd.cbDetails = CUInt(Marshal.SizeOf(GetType(Integer)))
        mcd.paDetails = Marshal.AllocHGlobal(CInt(mcd.cbDetails))
        mixerGetControlDetails(IntPtr.Zero, mcd, MIXER.GETCONTROLDETAILSF_VALUE Or MIXER.OBJECTF_MIXER)
        Dim rtn As VOLUME = CType(Marshal.PtrToStructure(mcd.paDetails, GetType(VOLUME)), VOLUME)
        Marshal.FreeHGlobal(mcd.paDetails)
        Return rtn
    End Function

    Private Shared Function IsMuted(ByVal mi As MixerInfo) As Boolean
        Dim mcd As MIXERCONTROLDETAILS = New MIXERCONTROLDETAILS()
        mcd.cbStruct = CUInt(Marshal.SizeOf(GetType(MIXERCONTROLDETAILS)))
        mcd.dwControlID = mi.muteCtl
        mcd.cMultipleItems = 0
        mcd.cChannels = 1
        mcd.cbDetails = 4
        mcd.paDetails = Marshal.AllocHGlobal(CInt(mcd.cbDetails))
        mixerGetControlDetails(IntPtr.Zero, mcd, MIXER.GETCONTROLDETAILSF_VALUE Or MIXER.OBJECTF_MIXER)
        Dim rtn As Integer = Marshal.ReadInt32(mcd.paDetails)
        Marshal.FreeHGlobal(mcd.paDetails)
        Return rtn <> 0
    End Function

    Public Shared Sub AdjustVolume(ByVal mi As MixerInfo, ByVal delta As Integer)
        Dim volume As VOLUME = GetVolume(mi)
        If delta > 0 Then
            volume.left = Math.Min(mi.maxVolume, volume.left + delta)
            volume.right = Math.Min(mi.maxVolume, volume.right + delta)
        Else
            volume.left = Math.Max(mi.minVolume, volume.left + delta)
            volume.right = Math.Max(mi.minVolume, volume.right + delta)
        End If

        SetVolume(mi, volume)
        'MsgBox(New Win32Exception(Marshal.GetLastWin32Error()).Message)
    End Sub

    Public Shared Sub SetVolume(ByVal mi As MixerInfo, ByVal volume As VOLUME)
        Dim mcd As MIXERCONTROLDETAILS = New MIXERCONTROLDETAILS()
        mcd.cbStruct = CUInt(Marshal.SizeOf(GetType(MIXERCONTROLDETAILS)))
        mcd.dwControlID = mi.volumeCtl
        mcd.cMultipleItems = 0
        mcd.cChannels = 2
        mcd.cbDetails = CUInt(Marshal.SizeOf(GetType(Integer)))
        mcd.paDetails = Marshal.AllocHGlobal(CInt(mcd.cbDetails))
        Marshal.StructureToPtr(volume, mcd.paDetails, False)
        mixerSetControlDetails(IntPtr.Zero, mcd, MIXER.GETCONTROLDETAILSF_VALUE Or MIXER.OBJECTF_MIXER)
        Marshal.FreeHGlobal(mcd.paDetails)
        'MsgBox(New Win32Exception(Marshal.GetLastWin32Error()).Message)
    End Sub

    Public Shared Sub SetMute(ByVal mi As MixerInfo, ByVal mute As Boolean)
        Dim mcd As MIXERCONTROLDETAILS = New MIXERCONTROLDETAILS()
        mcd.cbStruct = CUInt(Marshal.SizeOf(GetType(MIXERCONTROLDETAILS)))
        mcd.dwControlID = mi.muteCtl
        mcd.cMultipleItems = 0
        mcd.cChannels = 1
        mcd.cbDetails = 4
        mcd.paDetails = Marshal.AllocHGlobal(CInt(mcd.cbDetails))
        Marshal.WriteInt32(mcd.paDetails, If(mute, 1, 0))
        mixerSetControlDetails(IntPtr.Zero, mcd, MIXER.GETCONTROLDETAILSF_VALUE Or MIXER.OBJECTF_MIXER)
        Marshal.FreeHGlobal(mcd.paDetails)
    End Sub
End Class