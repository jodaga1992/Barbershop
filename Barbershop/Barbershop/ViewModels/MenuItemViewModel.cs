
namespace Barbershop.ViewModels
{
    using Views;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MenuItemViewModel
    {
        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }

        }

        private void Navigate()
        {
            App.Master.IsPresented = false;
            if (this.PageName == "LoginPage")
            {
                Settings.IsRemembered = "false";
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = null;
                mainViewModel.User = null;
                mainViewModel.TokenType = string.Empty;
                Application.Current.MainPage = new LoginPage();
            }
        }
        #endregion
    }

}
