namespace Barbershop.ViewModels
{
    using Helpers;
    using GalaSoft.MvvmLight.Command;
    using Views;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Services;
    using System;

    public class LoginViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        private DataService dataService;
        #endregion

        #region Attributes
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRemembered
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.IsRemembered = true;
            this.IsEnabled = true;

            // this.Email = "mj@g.com";
            //this.Password = "1234";
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }

        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "You must enter an email",
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.YouMustPassword,
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
            //barbershopgokuapi.azurewebsites.net
            var token = await this.apiService.GetToken(
                "http://barbershopgokuapi.azurewebsites.net",
                this.Email,
                this.Password);

            if (token == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "Something was wrong, please try later",
                     Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    token.ErrorDescription,
                    Languages.Accept);
                this.Password = string.Empty;
                return;
            }

            var user = await this.apiService.GetUserByEmail(
               "http://barbershopgokuapi.azurewebsites.net",
               "/api",
               "/Users/GetUserByEmail",
               token.TokenType,
               token.AccessToken,
               this.Email);

            var userLocal = Converter.ToUserLocal(user);
            userLocal.Password = this.Password;

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.User = userLocal;

            if (this.IsRemembered)
            {
                Settings.IsRemembered = "true";
            }
            else
            {
                Settings.IsRemembered = "false";
            }

            this.dataService.DeleteAllAndInsert(userLocal);
            this.dataService.DeleteAllAndInsert(token);
            
            this.IsRunning = false;
            this.IsEnabled = true;
            this.Email = string.Empty;
            this.Password = string.Empty;

            mainViewModel.Barbershops = new BarbershopsViewModel();
            Application.Current.MainPage = new MasterPage();

        }

        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }

        }

        private async void Register()
        {
           MainViewModel.GetInstance().Register = new RegisterViewModel();
           await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        #endregion


    }
}
