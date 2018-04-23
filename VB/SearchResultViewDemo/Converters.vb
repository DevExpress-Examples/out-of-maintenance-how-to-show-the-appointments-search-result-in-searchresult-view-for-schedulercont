Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Data
Imports DevExpress.Xpf.Scheduler
Imports System.Windows

Namespace SearchResultViewDemo
    Public Class FilterCriteriaConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return If(value Is Nothing, 1, 0)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
    Public Class LabelIdConverter
        Implements IMultiValueConverter

        Public Function Convert(ByVal values() As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            For Each value In values
                If value Is DependencyProperty.UnsetValue Then
                    Return Nothing
                End If
            Next value
            Dim scheduler As SchedulerControl = TryCast(values(1), SchedulerControl)
            Dim labelIndex As Integer = DirectCast(values(0), Integer)
            Return scheduler.Storage.AppointmentStorage.Labels(labelIndex).Brush
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes() As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class StatusIdConverter
        Implements IMultiValueConverter

        Public Function Convert(ByVal values() As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            For Each value In values
                If value Is DependencyProperty.UnsetValue Then
                    Return Nothing
                End If
            Next value
            Dim scheduler As SchedulerControl = TryCast(values(1), SchedulerControl)
            Dim statusIndex As Integer = DirectCast(values(0), Integer)
            Return scheduler.Storage.AppointmentStorage.Statuses(statusIndex).Brush
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes() As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
