namespace Barbershop.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class BarbershopsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<BarberItemViewModel> barbers;
        private List<BarberResponse> list;
        private bool isRefreshing;
        private string filter;
        #endregion

        #region Properties
        public ObservableCollection<BarberItemViewModel> Barbers
        {
            get { return this.barbers; }
            set { SetValue(ref this.barbers, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                //this.Search();
            }
        }
        #endregion

        #region Constructors
        public BarbershopsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadBarbers();
        }
        #endregion

        #region Methods
        private async void LoadBarbers()
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

            var response = await this.apiService.GetList<BarberResponse>(
                "http://barbershopgokuapi.azurewebsites.net",
                "/api",
                "/barbers");

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

            this.list = (List<BarberResponse>)response.Result;
            this.Barbers = new ObservableCollection<BarberItemViewModel>(
                this.ToBarberItemViewModel());
            this.IsRefreshing = false;
        }

        private IEnumerable<BarberItemViewModel> ToBarberItemViewModel()
        {
            return list.Select(l => new BarberItemViewModel
            {
                BarberId = l.BarberId,
                FirstName = l.FirstName,
                LastName = l.LastName,
                Email = l.Email,
                Telephone = l.Telephone,
                ImagePath = l.ImagePath,
                Password = l.Password,
                ImageFullPath = l.ImageFullPath,
                FullName = l.FullName
            });
        }

        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadBarbers);
            }
        }
        #endregion
    }
}
