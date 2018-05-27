namespace Barbershop.ViewModels
{
    using Barbershop.Domain2;
    using Barbershop.Helpers;
    using Barbershop.Services;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class AppointmentItemViewModel : AppointmentResponse
    {
        #region Services
        private ApiService apiService;
        #endregion


        #region Commands
        public ICommand AddAppointmentCommand
        {
            get
            {
                return new RelayCommand(AddAppointment);
            }
        }

        public ICommand CancelAppointmentCommand
        {
            get
            {
                return new RelayCommand(CancelAppointment);
            }
        }
        #endregion

        #region Methods
        private async void AddAppointment()
        {            
            var answer = await Application.Current.MainPage.DisplayAlert(
                "Confirmacion", "¿Seguro que desea apartar esta cita", "Si", "No");
            if (answer)
            {
                this.apiService = new ApiService();
                var checkConnetion = await this.apiService.CheckConnection();
                if (!checkConnetion.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        checkConnetion.Message,
                        Languages.Accept);
                    return;
                }
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Schedule.IsRefreshing = true;
                var appointment = new Appointment
                {
                    UserId = mainViewModel.User.UserId,
                    BarberId = this.BarberId,
                    Date = this.Date.Date,
                    Hour = this.Hour,
                    StatusAppointmentId = 1
                };

                var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
                var response = await this.apiService.Post(
                    apiSecurity,
                    "/api",
                    "/Appointments",
                    appointment);

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        Languages.Accept);
                    return;
                }
                mainViewModel.Schedule.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                "Confirm",
                "La cita fue apartada",
                Languages.Accept);
                await App.Navigator.PopAsync();
            }
        }

        private async void CancelAppointment()
        {
            var answer = await Application.Current.MainPage.DisplayAlert(
                "Confirmacion", "¿Seguro que desea cancelar esta cita", "Si", "No");
            if (answer)
            {
                this.apiService = new ApiService();
                var checkConnetion = await this.apiService.CheckConnection();
                if (!checkConnetion.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        checkConnetion.Message,
                        Languages.Accept);
                    return;
                }
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Appointments.IsRefreshing = true;
                var appointment = new AppointmentResponse
                {
                    UserId = mainViewModel.User.UserId,
                    BarberId = this.BarberId,
                    Date = this.Date.Date,
                    Hour = this.Hour,
                    StatusAppointmentId = 3,
                    AppointmentId = this.AppointmentId
                };

                var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
                var response = await this.apiService.Put(
                    apiSecurity,
                    "/api",
                    "/Appointments",
                    mainViewModel.Token.TokenType,
                    mainViewModel.Token.AccessToken,
                    appointment);

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        Languages.Accept);
                    return;
                }
                mainViewModel.Appointments.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                "Confirm",
                "La cita fue Cancelada",
                Languages.Accept);
                await App.Navigator.PopAsync();
            }
        }

        #endregion
    }
}
