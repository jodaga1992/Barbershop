namespace Barbershop.ViewModels
{
    using Helpers;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;
    using System.Linq;

    public class ScheduleViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRefreshing;
        private List<AppointmentResponse> list;
        private ObservableCollection<AppointmentItemViewModel> appointments;
        #endregion

        #region Properties

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public ObservableCollection<AppointmentItemViewModel> Appointments
        {
            get { return this.appointments; }
            set { SetValue(ref this.appointments, value); }
        }
        public ScheduleResponse Schedule { get; set; }
        #endregion

        #region Constructors
        public ScheduleViewModel(ScheduleResponse schedule)
        {
            this.apiService = new ApiService();
            this.Schedule = schedule;
            this.LoadAppointments();
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadAppointments);
            }
        }
        #endregion

        #region Methods

        private async void LoadAppointments()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                connection.Message,
                Languages.Accept);
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var date = String.Format("{0:yyyy-MM-dd}", Schedule.Date);

            var response = await this.apiService.GetList<AppointmentResponse>(
               "http://barbershopgokuapi.azurewebsites.net",
                "/api",
                string.Format("/GetBarberAvailability?id={0}&date={1}", Schedule.BarberId, date));

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            this.list = (List<AppointmentResponse>)response.Result;
            this.Appointments = new ObservableCollection<AppointmentItemViewModel>(
                this.ToAppointmentItemViewModel());
            this.IsRefreshing = false;
        }

        private IEnumerable<AppointmentItemViewModel> ToAppointmentItemViewModel()
        {
            var mainViewModel = MainViewModel.GetInstance();
            return list.Select(l => new AppointmentItemViewModel
            {
                BarberId = l.BarberId,
                Date = l.Date,
                Hour = l.Hour,
                UserId = mainViewModel.User.UserId,
                StatusAppointmentId = 1
            });
        }

        #endregion
    }
}
