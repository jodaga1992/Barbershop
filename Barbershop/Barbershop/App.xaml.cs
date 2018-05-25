namespace Barbershop
{
    using Xamarin.Forms;
    using Helpers;
    using ViewModels;
    using Views;
    using System;
    using Services;
    using Models;

    public partial class App : Application
    {
        #region Properties
        public static NavigationPage Navigator
        {
            get;
            internal set;
        }

        public static MasterPage Master
        {
            get;
            internal set;
        }
        #endregion

        #region Constructors
        public App()
        {
            InitializeComponent();
            if (Settings.IsRemembered != "true")
            {
                this.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                var dataService = new DataService();
                var token = dataService.First<TokenResponse>(false);
                if (token != null && token.Expires > DateTime.Now)
                {
                    var user = dataService.First<UserLocal>(false);
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.Token = token;
                    mainViewModel.User = user;
                    mainViewModel.Barbershops = new BarbershopsViewModel();
                    Application.Current.MainPage = new MasterPage();
                }
            }
        }

        
        #endregion

        #region Methods
        public static Action HideLoginView
        {
            get
            {
                return new Action(() => App.Current.MainPage = new NavigationPage(new LoginPage()));
            }
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion
    }
}
