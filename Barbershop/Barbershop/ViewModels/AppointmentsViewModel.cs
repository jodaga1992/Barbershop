namespace Barbershop.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;

    public class AppointmentsViewModel : BaseViewModel
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
        #endregion

        #region Constructors
        public AppointmentsViewModel()
        {
            this.apiService = new ApiService();
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

            var mainViewModel = MainViewModel.GetInstance();

            var response = await this.apiService.GetList<AppointmentResponse>(
               "http://barbershopgokuapi.azurewebsites.net",
                "/api",
                string.Format("/GetAppointmentUser?id={0}", mainViewModel.User.UserId));

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
                StatusAppointmentId = l.StatusAppointmentId,
                AppointmentId = l.AppointmentId
            });
        }

        #endregion
    }
}
