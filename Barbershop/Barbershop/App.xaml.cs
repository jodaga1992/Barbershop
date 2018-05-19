

namespace Barbershop
{
    using Xamarin.Forms;
    using Helpers;
    using ViewModels;
    using Views;
    using System;

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
            if (string.IsNullOrEmpty(Settings.Token))
            {
                this.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = Settings.Token;
                mainViewModel.TokenType = Settings.TokenType;
                mainViewModel.Barbershops = new BarbershopsViewModel();
                this.MainPage = new MasterPage();

            }

            // this.MainPage = new NavigationPage(new LoginPage());
            //this.MainPage = new MasterPage();
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
