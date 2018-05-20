
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
            if (this.PageName == "LoginPage")
            {
                Settings.Token = string.Empty;
                Settings.TokenType = string.Empty;
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.TokenType = string.Empty;
                Application.Current.MainPage = new LoginPage();
            }
        }
        #endregion
    }

}
