namespace Barbershop.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Services;
    using Views;
    using Xamarin.Forms;

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

        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }

        }

        #endregion

        #region Methods
        
        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password",
                    "Accept");
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
                    "Error",
                    connection.Message,
                    "Accept");
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
                    "Error",
                    "Something was wrong, please try later",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    token.ErrorDescription,
                    "Accept");
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

        private async void Register()
        {
            MainViewModel.GetInstance().Register = new RegisterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        public async void LoginFacebook()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginFacebookPage());
        }

        public async void LoginInstagram()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginInstagramPage());
        }

        public async void LoginLinkedin()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginLinkedinPage());
        }
        #endregion

    }
}
