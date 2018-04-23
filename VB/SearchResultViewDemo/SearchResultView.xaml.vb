Imports DevExpress.Data.Filtering
Imports DevExpress.XtraScheduler
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls

Namespace SearchResultViewDemo
    Partial Public Class SearchResultView
        Inherits UserControl

        Public Shared FilterCriteriaProperty As DependencyProperty = DependencyProperty.Register("FilterCriteria", GetType(CriteriaOperator), GetType(SearchResultView), New PropertyMetadata(Nothing))
        Public Shared ReadOnly OwnerSchedulerProperty As DependencyProperty = DependencyProperty.Register("OwnerScheduler", GetType(DevExpress.Xpf.Scheduler.SchedulerControl), GetType(SearchResultView), New UIPropertyMetadata(Nothing))
        Public Property FilterCriteria() As CriteriaOperator
            Get
                Return DirectCast(GetValue(FilterCriteriaProperty), CriteriaOperator)
            End Get
            Set(ByVal value As CriteriaOperator)
                SetValue(FilterCriteriaProperty, value)
            End Set
        End Property
        Public Property OwnerScheduler() As DevExpress.Xpf.Scheduler.SchedulerControl
            Get
                Return TryCast(GetValue(OwnerSchedulerProperty), DevExpress.Xpf.Scheduler.SchedulerControl)
            End Get
            Set(ByVal value As DevExpress.Xpf.Scheduler.SchedulerControl)
                SetValue(OwnerSchedulerProperty, value)
            End Set
        End Property
        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, AddressOf SearchResultView_Loaded
        End Sub

        Private Sub SearchResultView_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If OwnerScheduler IsNot Nothing Then
                AddHandler OwnerScheduler.Storage.AppointmentsChanged, AddressOf Storage_AppointmentsChanged
            End If
            RemoveHandler Loaded, AddressOf SearchResultView_Loaded
        End Sub

        Private Sub Storage_AppointmentsChanged(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
            searchResultGrid.RefreshData()
        End Sub

        Private Sub TableView_RowDoubleClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.RowDoubleClickEventArgs)
            Dim apt As Appointment = TryCast(e.Source.DataControl.CurrentItem, Appointment)
            If apt IsNot Nothing Then
                OwnerScheduler.ShowEditAppointmentForm(apt)
            End If
        End Sub

        Private Sub searchResultGrid_CustomGroupDisplayText(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs)
            Select Case e.Column.FieldName
                Case "ResourceId"
                    e.DisplayText = OwnerScheduler.Storage.ResourceStorage.GetResourceById(e.Value).Caption
                Case "LabelId"
                    e.DisplayText = OwnerScheduler.Storage.AppointmentStorage.Labels(CInt((e.Value))).DisplayName
                Case "StatusId"
                    e.DisplayText = OwnerScheduler.Storage.AppointmentStorage.Statuses(CInt((e.Value))).DisplayName
            End Select
        End Sub
    End Class
End Namespace