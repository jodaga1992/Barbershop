namespace Barbershop.ViewModels
{
    using Barbershop.Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    public class AppointmentsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRefreshing;
        private List<AppointmentResponse> list;
        private ObservableCollection<ScheduleItemViewModel> schedules;
        #endregion

        #region Properties

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public ObservableCollection<ScheduleItemViewModel> Schedules
        {
            get { return this.schedules; }
            set { SetValue(ref this.schedules, value); }
        }
        public BarberResponse Barber { get; set; }
        #endregion

    }
}
