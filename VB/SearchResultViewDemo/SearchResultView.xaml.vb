Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.Grid
Imports DevExpress.XtraScheduler
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls

Namespace SearchResultViewDemo

    Public Partial Class SearchResultView
        Inherits UserControl

        Public Shared FilterCriteriaProperty As DependencyProperty = DependencyProperty.Register("FilterCriteria", GetType(CriteriaOperator), GetType(SearchResultView), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly OwnerSchedulerProperty As DependencyProperty = DependencyProperty.Register("OwnerScheduler", GetType(DevExpress.Xpf.Scheduler.SchedulerControl), GetType(SearchResultView), New UIPropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property FilterCriteria As CriteriaOperator
            Get
                Return CType(GetValue(FilterCriteriaProperty), CriteriaOperator)
            End Get

            Set(ByVal value As CriteriaOperator)
                SetValue(FilterCriteriaProperty, value)
            End Set
        End Property

        Public Property OwnerScheduler As DevExpress.Xpf.Scheduler.SchedulerControl
            Get
                Return TryCast(GetValue(OwnerSchedulerProperty), DevExpress.Xpf.Scheduler.SchedulerControl)
            End Get

            Set(ByVal value As DevExpress.Xpf.Scheduler.SchedulerControl)
                SetValue(OwnerSchedulerProperty, value)
            End Set
        End Property

        Public Sub New()
            Me.InitializeComponent()
            AddHandler Loaded, AddressOf SearchResultView_Loaded
        End Sub

        Private Sub SearchResultView_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If OwnerScheduler IsNot Nothing Then AddHandler OwnerScheduler.Storage.AppointmentsChanged, AddressOf Storage_AppointmentsChanged
            RemoveHandler Loaded, AddressOf SearchResultView_Loaded
        End Sub

        Private Sub Storage_AppointmentsChanged(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
            Me.searchResultGrid.RefreshData()
        End Sub

        Private Sub TableView_RowDoubleClick(ByVal sender As Object, ByVal e As RowDoubleClickEventArgs)
            Dim apt As Appointment = TryCast(e.Source.DataControl.CurrentItem, Appointment)
            If apt IsNot Nothing Then OwnerScheduler.ShowEditAppointmentForm(apt)
        End Sub

        Private Sub searchResultGrid_CustomGroupDisplayText(ByVal sender As Object, ByVal e As CustomColumnDisplayTextEventArgs)
            Select Case e.Column.FieldName
                Case "ResourceId"
                    e.DisplayText = OwnerScheduler.Storage.ResourceStorage.GetResourceById(e.Value).Caption
                Case "LabelId"
                    e.DisplayText = OwnerScheduler.Storage.AppointmentStorage.Labels(CInt(e.Value)).DisplayName
                Case "StatusId"
                    e.DisplayText = OwnerScheduler.Storage.AppointmentStorage.Statuses(CInt(e.Value)).DisplayName
            End Select
        End Sub
    End Class
End Namespace
