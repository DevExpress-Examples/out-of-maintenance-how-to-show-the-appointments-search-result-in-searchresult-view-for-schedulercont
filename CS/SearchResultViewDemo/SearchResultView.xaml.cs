using DevExpress.Data.Filtering;
using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SearchResultViewDemo {
    public partial class SearchResultView : UserControl {
        public static DependencyProperty FilterCriteriaProperty = DependencyProperty.Register("FilterCriteria", typeof(CriteriaOperator), typeof(SearchResultView), new PropertyMetadata(null));
        public static readonly DependencyProperty OwnerSchedulerProperty = DependencyProperty.Register("OwnerScheduler", typeof(DevExpress.Xpf.Scheduler.SchedulerControl), typeof(SearchResultView), new UIPropertyMetadata(null));
        public CriteriaOperator FilterCriteria {
            get { return (CriteriaOperator)GetValue(FilterCriteriaProperty); }
            set { SetValue(FilterCriteriaProperty, value); }
        }
        public DevExpress.Xpf.Scheduler.SchedulerControl OwnerScheduler {
            get {
                return GetValue(OwnerSchedulerProperty) as DevExpress.Xpf.Scheduler.SchedulerControl;
            }
            set {
                SetValue(OwnerSchedulerProperty, value);
            }
        }
        public SearchResultView() {
            InitializeComponent();
            Loaded += SearchResultView_Loaded;
        }

        void SearchResultView_Loaded(object sender, RoutedEventArgs e) {
            if (OwnerScheduler != null)
                OwnerScheduler.Storage.AppointmentsChanged += Storage_AppointmentsChanged;
            Loaded -= SearchResultView_Loaded;
        }

        private void Storage_AppointmentsChanged(object sender, PersistentObjectsEventArgs e) {
            searchResultGrid.RefreshData();
        }

        private void TableView_RowDoubleClick(object sender, DevExpress.Xpf.Grid.RowDoubleClickEventArgs e) {
            Appointment apt = e.Source.DataControl.CurrentItem as Appointment;
            
            if (apt != null)
                OwnerScheduler.ShowEditAppointmentForm(apt);
        }

        private void searchResultGrid_CustomGroupDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e) {
            switch (e.Column.FieldName) {
                case "ResourceId":
                    e.DisplayText = OwnerScheduler.Storage.ResourceStorage.GetResourceById(e.Value).Caption;
                    break;
                case "LabelId":
                    e.DisplayText = OwnerScheduler.Storage.AppointmentStorage.Labels[(int)e.Value].DisplayName;
                    break;
                case "StatusId":
                    e.DisplayText = OwnerScheduler.Storage.AppointmentStorage.Statuses[(int)e.Value].DisplayName;
                    break;
            }
        }
    }
}