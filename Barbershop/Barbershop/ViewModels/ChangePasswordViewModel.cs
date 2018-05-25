
namespace Barbershop.ViewModels
{
    using System;
    using System.Threading;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class ChangePasswordViewModel :BaseViewModel
    {
        #region Services
        private ApiService apiService;
        private DataService dataService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private string progress;
        private ImageSource imageSource;
        private MediaFile file;
        #endregion

        #region Properties
        public UserLocal User
        {
            get;
            set;
        }

        public string Progress
        {
            get { return this.progress; }
            set { SetValue(ref this.progress, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public string CurrentPassword
        {
            get;
            set;
        }

        public string NewPassword
        {
            get;
            set;
        }

        public string Confirm
        {
            get;
            set;
        }

        #endregion

        #region Constructor
        public ChangePasswordViewModel()
        {
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.IsEnabled = true;
            this.Progress = "0";
        }
        #endregion

        #region Commands
        public ICommand ChangePasswordCommand 
        {
            get
            {
                return new RelayCommand(ChangePassword);
            }
                
        }

        private async void ChangePassword()
        {
            this.Progress = "100";
            if (string.IsNullOrEmpty(this.CurrentPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "You must enter a password.",
                    Languages.Accept);
                return;
            }

            if (this.CurrentPassword.Length < 6)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "The password must have at least seix (6) characters.",
                    Languages.Accept);
                return;
            }
            
            if (this.CurrentPassword !=MainViewModel.GetInstance().User.Password)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "You must enter a password.",
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.NewPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "You must enter a password.",
                    Languages.Accept);
                return;
            }

            if (this.NewPassword.Length < 6)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "The password must have at least seix (6) characters.",
                    Languages.Accept);
                return;
            }
            
            if (string.IsNullOrEmpty(this.Confirm))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "You must enter a confirm.",
                    Languages.Accept);
                return;
            }

            if (this.NewPassword != this.Confirm)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "The password and confirm does not match",
                    Languages.Accept);
                return;
            }
            
            this.IsRunning = true;
            this.IsEnabled = false;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;
            }
            
            var request = new ChangePasswordRequest
            {
                CurrentPassword = this.CurrentPassword,
                Email = MainViewModel.GetInstance().User.Email,
                NewPassword = this.NewPassword,
            };

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.ChangePassword(
                apiSecurity,
                "/api",
                "/Users/ChangePassword",
                MainViewModel.GetInstance().Token.TokenType,
                MainViewModel.GetInstance().Token.AccessToken,
                request);
           
            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "Error Changing Password",
                    Languages.Accept);
                return;
            }
          
            MainViewModel.GetInstance().User.Password = this.NewPassword;
            this.dataService.Update(MainViewModel.GetInstance().User);
            this.IsRunning = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.DisplayAlert(
                "Confirm",
                "Chage Password Confirm",
                Languages.Accept);
            await App.Navigator.PopAsync();


        }
        #endregion
    }
}
