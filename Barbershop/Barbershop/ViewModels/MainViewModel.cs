
namespace Barbershop.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Models;

    public class MainViewModel
    {
        #region Properties

        public string Token { get; set; }

        public string TokenType { get; set; }

        public ObservableCollection<MenuItemViewModel> Menus
        {
            get;
            set;
        }

        public string SelectedModule
        {
            get;
            set;
        }
        #endregion

        #region ViewModels
        public LoginViewModel Login
        {
            get;
            set;
        }

        public BarbershopsViewModel Barbershops
        {
            get;
            set;
        }

        public RegisterViewModel Register
        {
            get;
            set;
        }

        public BarberViewModel Barber
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.LoadMenu();
        }


        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }

        #endregion

        #region Methods
        private void LoadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>();
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "setting",
                PageName = "MyProfilePage",
                Title = "Perfil" //Languages.
            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "grafig",
                PageName = "StatisticsPage",
                Title = "Statistics"
            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "exit",
                PageName = "LoginPage",
                Title = "LogOut"
            });

        }
        #endregion
    }
}
