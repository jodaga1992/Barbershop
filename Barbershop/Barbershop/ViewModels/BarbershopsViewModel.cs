
namespace Barbershop.ViewModels
{
    //using Models;
    using Helpers;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Domain2;
    using Xamarin.Forms;
    using Services;
    using Models;
    using System;
    using System.Linq;

    public class BarbershopsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<BarberResponse> barbers;
        private bool isRefreshing;
        private string filter;
        #endregion

        #region Properties
        public ObservableCollection<BarberResponse> Barbers
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

            var list = (List<BarberResponse>)response.Result;
            this.Barbers = new ObservableCollection<BarberResponse>(list);
            this.IsRefreshing = false;
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
