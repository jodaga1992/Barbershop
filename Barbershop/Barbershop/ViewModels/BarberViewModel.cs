namespace Barbershop.ViewModels
{
    using Models;
    using Services;
    using Helpers;
    using Xamarin.Forms;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public class BarberViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRefreshing;
        private List<ScheduleResponse> list;
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

        #region Constructors
        public BarberViewModel(BarberResponse barber)
        {
            this.apiService = new ApiService();
            this.Barber = barber;
            this.LoadSchedules();
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadSchedules);
            }
        }
        #endregion

        #region Methods

        private async void LoadSchedules()
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

            var response = await this.apiService.GetList<ScheduleResponse>(
               "http://barbershopgokuapi.azurewebsites.net",
                "/api",
                string.Format("/GetBarberSchedules?id={0}", Barber.BarberId));

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

            this.list = (List<ScheduleResponse>)response.Result;
            this.Schedules = new ObservableCollection<ScheduleItemViewModel>(
                this.ToScheduleItemViewModel());
            this.IsRefreshing = false;
        }

        private IEnumerable<ScheduleItemViewModel> ToScheduleItemViewModel()
        {
            return list.Select(l => new ScheduleItemViewModel
            {
                ScheduleId = l.ScheduleId,
                Barber = l.Barber,
                BarberId = l.BarberId,
                Date = l.Date,
                HourStart = l.HourStart,
                HourEnd = l.HourEnd
            });
        }

        #endregion
    }
}
