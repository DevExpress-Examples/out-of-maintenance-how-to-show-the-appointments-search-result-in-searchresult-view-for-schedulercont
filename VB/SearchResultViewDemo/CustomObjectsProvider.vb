Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.WindowsUI
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Namespace SearchResultViewDemo

    Public Class CustomObjects
        Inherits ViewModelBase

        Private _PreviewKeyDownCommand As ICommand(Of System.Windows.Input.KeyEventArgs), _ResetSearchTextCommand As ICommand, _SearchControlLoadedCommand As ICommand

        Private RandomInstance As Random = New Random()

        Private _resourcesList As BindingList(Of CustomResource) = New BindingList(Of CustomResource)()

        Private _appointmentsList As BindingList(Of CustomAppointment) = New BindingList(Of CustomAppointment)()

        Public Property PreviewKeyDownCommand As ICommand(Of KeyEventArgs)
            Get
                Return _PreviewKeyDownCommand
            End Get

            Private Set(ByVal value As ICommand(Of KeyEventArgs))
                _PreviewKeyDownCommand = value
            End Set
        End Property

        Public Property ResetSearchTextCommand As ICommand
            Get
                Return _ResetSearchTextCommand
            End Get

            Private Set(ByVal value As ICommand)
                _ResetSearchTextCommand = value
            End Set
        End Property

        Public Property SearchControlLoadedCommand As ICommand
            Get
                Return _SearchControlLoadedCommand
            End Get

            Private Set(ByVal value As ICommand)
                _SearchControlLoadedCommand = value
            End Set
        End Property

        Public Property SearchControl As SearchControl

        Public Property Resources As BindingList(Of CustomResource)
            Get
                Return _resourcesList
            End Get

            Set(ByVal value As BindingList(Of CustomResource))
                If _resourcesList Is Nothing Then Return
                _resourcesList = value
                RaisePropertyChanged(Function() Resources)
            End Set
        End Property

        Public Property Appointments As BindingList(Of CustomAppointment)
            Get
                Return _appointmentsList
            End Get

            Set(ByVal value As BindingList(Of CustomAppointment))
                If _appointmentsList Is Nothing Then Return
                _appointmentsList = value
                RaisePropertyChanged(Function() Appointments)
            End Set
        End Property

        Public Sub New()
            InitResources()
            InitAppointments()
            SearchControl = New SearchControl()
            PreviewKeyDownCommand = New DelegateCommand(Of KeyEventArgs)(AddressOf PreviewKeyDown)
            SearchControlLoadedCommand = New DelegateCommand(Of RoutedEventArgs)(AddressOf SearchControlLoaded)
            ResetSearchTextCommand = New DevExpress.Mvvm.DelegateCommand(Of SelectionChangedEventArgs)(AddressOf ResetSearchText)
        End Sub

        Private Sub InitResources()
            _resourcesList.Add(CreateCustomResource(1, "Max Fowler", System.Drawing.Color.AliceBlue))
            _resourcesList.Add(CreateCustomResource(2, "Nancy Drewmore", System.Drawing.Color.Lavender))
            _resourcesList.Add(CreateCustomResource(3, "Pak Jang", System.Drawing.Color.PeachPuff))
        End Sub

        Private Function CreateCustomResource(ByVal res_id As Integer, ByVal caption As String, ByVal resColor As System.Drawing.Color) As CustomResource
            Dim cr As CustomResource = New CustomResource()
            cr.ResID = res_id
            cr.Name = caption
            cr.Color = resColor
            Return cr
        End Function

        Private Sub InitAppointments()
            Dim count As Integer = _resourcesList.Count
            For i As Integer = 0 To count - 1
                Dim resource As CustomResource = _resourcesList(i)
                Dim subjPrefix As String = resource.Name & "'s "
                _appointmentsList.Add(CreateEvent(subjPrefix & "meeting", "meeting", resource.ResID, 2, 5, 0, "office"))
                _appointmentsList.Add(CreateEvent(subjPrefix & "travel", "travel", resource.ResID, 3, 6, 0, "out of the city"))
                _appointmentsList.Add(CreateEvent(subjPrefix & "phone call", "phone call", resource.ResID, 0, 10, 0, "office"))
                _appointmentsList.Add(CreateEvent(subjPrefix & "business trip", "business trip", resource.ResID, 3, 6, 3, "San-Francisco"))
                _appointmentsList.Add(CreateEvent(subjPrefix & "double personal day", "double personal day", resource.ResID, 0, 10, 2, "out of the city"))
                _appointmentsList.Add(CreateEvent(subjPrefix & "personal day", "personal day", resource.ResID, 0, 10, 1, "out of the city"))
            Next
        End Sub

        Private Function CreateEvent(ByVal description As String, ByVal subject As String, ByVal resourceId As Object, ByVal status As Integer, ByVal label As Integer, ByVal days As Integer, ByVal location As String) As CustomAppointment
            Dim apt As CustomAppointment = New CustomAppointment()
            apt.Subject = subject
            apt.Description = description
            apt.OwnerId = resourceId
            Dim rnd As Random = RandomInstance
            Dim rangeInMinutes As Integer = 60 * 24
            If days = 2 Then
                apt.StartTime = Date.Today
                apt.EndTime = Date.Today.AddDays(2)
            ElseIf days = 1 Then
                apt.StartTime = Date.Today
                apt.EndTime = Date.Today.AddDays(1)
            Else
                apt.StartTime = Date.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes))
                apt.EndTime = apt.StartTime.AddDays(days) + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes \ 4))
            End If

            apt.Location = location
            apt.Status = status
            apt.Label = label
            Return apt
        End Function

        Private Sub PreviewKeyDown(ByVal args As KeyEventArgs)
            If args.Key = Key.E AndAlso Keyboard.Modifiers = ModifierKeys.Control Then
                SearchControl.Focus()
                args.Handled = True
            End If
        End Sub

        Private Sub ResetSearchText(ByVal args As SelectionChangedEventArgs)
            Dim view As FlipView = TryCast(args.Source, FlipView)
            If view.SelectedIndex = 1 Then SearchControl.SearchText = String.Empty
        End Sub

        Private Sub SearchControlLoaded(ByVal args As RoutedEventArgs)
            SearchControl = TryCast(args.Source, SearchControl)
        End Sub
    End Class

'#Region "#customappointment"
    Public Class CustomAppointment

        Private m_Start As Date

        Private m_End As Date

        Private m_Subject As String

        Private m_Status As Integer

        Private m_Description As String

        Private m_Label As Integer

        Private m_Location As String

        Private m_Allday As Boolean

        Private m_EventType As Integer

        Private m_RecurrenceInfo As String

        Private m_ReminderInfo As String

        Private m_OwnerId As Object

        Public Property StartTime As Date
            Get
                Return m_Start
            End Get

            Set(ByVal value As Date)
                m_Start = value
            End Set
        End Property

        Public Property EndTime As Date
            Get
                Return m_End
            End Get

            Set(ByVal value As Date)
                m_End = value
            End Set
        End Property

        Public Property Subject As String
            Get
                Return m_Subject
            End Get

            Set(ByVal value As String)
                m_Subject = value
            End Set
        End Property

        Public Property Status As Integer
            Get
                Return m_Status
            End Get

            Set(ByVal value As Integer)
                m_Status = value
            End Set
        End Property

        Public Property Description As String
            Get
                Return m_Description
            End Get

            Set(ByVal value As String)
                m_Description = value
            End Set
        End Property

        Public Property Label As Integer
            Get
                Return m_Label
            End Get

            Set(ByVal value As Integer)
                m_Label = value
            End Set
        End Property

        Public Property Location As String
            Get
                Return m_Location
            End Get

            Set(ByVal value As String)
                m_Location = value
            End Set
        End Property

        Public Property AllDay As Boolean
            Get
                Return m_Allday
            End Get

            Set(ByVal value As Boolean)
                m_Allday = value
            End Set
        End Property

        Public Property EventType As Integer
            Get
                Return m_EventType
            End Get

            Set(ByVal value As Integer)
                m_EventType = value
            End Set
        End Property

        Public Property RecurrenceInfo As String
            Get
                Return m_RecurrenceInfo
            End Get

            Set(ByVal value As String)
                m_RecurrenceInfo = value
            End Set
        End Property

        Public Property ReminderInfo As String
            Get
                Return m_ReminderInfo
            End Get

            Set(ByVal value As String)
                m_ReminderInfo = value
            End Set
        End Property

        Public Property OwnerId As Object
            Get
                Return m_OwnerId
            End Get

            Set(ByVal value As Object)
                m_OwnerId = value
            End Set
        End Property

        Public Sub New()
        End Sub
    End Class

'#End Region  ' #customappointment
'#Region "#customresource"
    Public Class CustomResource

        Private m_name As String

        Private m_res_id As Integer

        Private m_color As Color

        Public Property Name As String
            Get
                Return m_name
            End Get

            Set(ByVal value As String)
                m_name = value
            End Set
        End Property

        Public Property ResID As Integer
            Get
                Return m_res_id
            End Get

            Set(ByVal value As Integer)
                m_res_id = value
            End Set
        End Property

        Public Property Color As Color
            Get
                Return m_color
            End Get

            Set(ByVal value As Color)
                m_color = value
            End Set
        End Property

        Public Sub New()
        End Sub
    End Class
'#End Region  ' #customresource
End Namespace
