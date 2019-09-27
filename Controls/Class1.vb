Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Windows.Forms
Public Class Diagram
    Inherits Control

    Public Structure Achse
        Public Property Caption As String
        Public Property Steps As Double
        Public Property Resolution As Double
        Public Sub New(Caption As String, Steps As Double, Resolution As Double)
            Me.Caption = Caption
            Me.Steps = Steps
            Me.Resolution = Resolution
        End Sub
    End Structure

    <Category("Main")>
    Property MainLineColor As Color = Color.Black
    <Category("Main")>
    Property Offset As Point = New Point(15, 15)
    <Category("Main")>
    Property XAchse As New Achse("", 1, 10)
    <Category("Main")>
    Property YAchse As New Achse("", 1, 10)
    Dim r_timeless As Boolean = True
    <Category("Main")>
    Property Timeless As Boolean
        Get
            Return r_timeless
        End Get
        Set(value As Boolean)
            r_timeless = value
            If value Then
                Timer.Enabled = False
                Timer.Stop()
            Else
                Timer.Interval = 10
                Timer.Enabled = True
            End If
        End Set
    End Property


    WithEvents Timer As New Timer()
    '<Category("X-Achse")>
    'Property CaptionXAchse As String = "Zeit"
    '<Category("X-Achse")>
    'Property XAchseSteps As Double = 1
    '<Category("x-Achse")>
    'Property XAchseResolution As Double = 10

    '<Category("Y-Achse")>
    'Property CaptionYAchse As String = "mBar"
    '<Category("Y-Achse")>
    'Property YAchseSteps As Double = 1
    '<Category("Y-Achse")>
    'Property YAchseResolution As Double = 10

    <Category("Border")>
    Property ShowBorder As Boolean = True
    <Category("Border")>
    Property BorderColor As Color = Color.LightGray

    <Category("Grid")>
    Property GridDisplayMode As DisplayMode = DisplayMode.Horizontal
    <Category("Grid")>
    Property GridColor As Color = Color.LightGray

    <Category("Graph")>
    Property GraphColor As Color = Color.Red
    Dim r_points As New List(Of Point)
    <Category("Graph")>
    ReadOnly Property Points As New List(Of Point)
    Dim TimeStep As Double

    Public Enum DisplayMode
        None
        Vertical
        Horizontal
        Both = Vertical Or Horizontal
    End Enum
    Public Sub New()
        MyBase.DoubleBuffered = True
        AddPoint(New Point(5, 3))
        AddPoint(New Point(6, 4))
        If Not Timeless Then
            Timer.Interval = 10
            Timer.Enabled = True
        End If
    End Sub
    Protected Overrides Sub InitLayout()
        MyBase.InitLayout()
        If Not Timeless Then
            Timer.Interval = 10
            Timer.Enabled = True
        End If
    End Sub
    Public Shadows Sub Update()
        Me.Refresh()
    End Sub
    Public Sub AddPoint(p As Point)
        Points.Add(p)
    End Sub
    Public Structure Point
        Public Property X As Double
        Public Property Y As Double
        Public Sub New(x As Double, y As Double)
            Me.X = x
            Me.Y = y
        End Sub
        Shared Operator -(p1 As Point, p2 As Point)
            Return New Point(p1.X - p2.X, p1.Y - p2.Y)
        End Operator
        Shared Narrowing Operator CType(x As Diagram.Point) As Drawing.Point
            Return New Drawing.Point(x.X, x.Y)
        End Operator
    End Structure
    Public Sub AddTimePoint(y As Integer)
        Debug.Print(CalcRightPos())
        Points.Add(New Point(CalcRightPos(), y))
    End Sub
    Function CalcRightPos() As Integer
        Return (Me.Width - Offset.X) / XAchse.Steps / XAchse.Resolution + TimeStep
    End Function
    Function ClalcPointPos(p As Point) As Point
        Dim rp1 = New Point(p.X * XAchse.Steps * XAchse.Resolution, Me.Height - Offset.Y - p.Y * YAchse.Steps * YAchse.Resolution)
        Dim rp2 = rp1 - New Point(TimeStep * XAchse.Steps * XAchse.Resolution, 0)
        Return rp2
    End Function
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit
        e.Graphics.InterpolationMode = InterpolationMode.High
        'Grid
        If GridDisplayMode.HasFlag(DisplayMode.Horizontal) Then
            For i As Integer = Me.Height - Offset.Y To 0 Step -1 * YAchse.Steps * YAchse.Resolution
                e.Graphics.DrawLine(New Pen(GridColor, 1), New Drawing.Point(Offset.X, i), New Drawing.Point(Me.Width, i))
            Next
        End If
        If GridDisplayMode.HasFlag(DisplayMode.Vertical) Then
            For i As Integer = Offset.X To Me.Width Step XAchse.Steps * XAchse.Resolution
                e.Graphics.DrawLine(New Pen(GridColor, 1), New Drawing.Point(i, Me.Height - Offset.Y), New Drawing.Point(i, 0))
            Next
        End If
        'Graph
        Try
            For i As Integer = 1 To Points.Count - 1
                Dim p = ClalcPointPos(Points(i))
                If Not p.X < 0 Then
                    e.Graphics.DrawLine(New Pen(GraphColor, 1), ClalcPointPos(Points(i - 1)), p)
                End If
            Next
        Catch : End Try
        'X Achse
        e.Graphics.DrawLine(New Pen(MainLineColor, 2), New Drawing.Point(0, Me.Height - Offset.Y), New Drawing.Point(Me.Width, Me.Height - Offset.Y))
        Dim format As New StringFormat
        format.Alignment = StringAlignment.Far
        e.Graphics.DrawString(XAchse.Caption, Font, New SolidBrush(ForeColor), New Rectangle(New Drawing.Point(Offset.X + 2, Me.Height - Offset.Y + 2), New Size(Me.Width - Offset.X - 2, 30)), format)
        'Y Achse
        e.Graphics.DrawLine(New Pen(MainLineColor, 2), New Drawing.Point(Offset.X, 0), New Drawing.Point(Offset.X, Me.Height))
        format = New StringFormat
        format.Alignment = StringAlignment.Near
        format.FormatFlags = StringFormatFlags.DirectionVertical
        e.Graphics.DrawString(YAchse.Caption, Font, New SolidBrush(ForeColor), New Rectangle(New Drawing.Point(0, 0), New Size(Offset.X - 2, Me.Height - Offset.Y - 2)), format)
        'Border
        If ShowBorder Then
            e.Graphics.DrawLine(New Pen(BorderColor, 1), New Drawing.Point(0, 0), New Drawing.Point(0, Me.Height))
            e.Graphics.DrawLine(New Pen(BorderColor, 1), New Drawing.Point(Me.Width - 1, 0), New Drawing.Point(Me.Width - 1, Me.Height))
            e.Graphics.DrawLine(New Pen(BorderColor, 1), New Drawing.Point(0, 0), New Drawing.Point(Me.Width, 0))
            e.Graphics.DrawLine(New Pen(BorderColor, 1), New Drawing.Point(0, Me.Height - 1), New Drawing.Point(Me.Width, Me.Height - 1))
        End If

    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        'MsgBox("Hallo")
        TimeStep += 0.09

        Me.Refresh()
        'System.Diagnostics.Debug.Print(TimeStep)
    End Sub
End Class