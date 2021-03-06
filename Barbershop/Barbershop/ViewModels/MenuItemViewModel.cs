﻿namespace Barbershop.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Views;
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

        private async void Navigate()
        {            
            App.Master.IsPresented = false;
            if (this.PageName == "LoginPage")
            {
                var answer = await Application.Current.MainPage.DisplayAlert(
                Languages.ConfirmMessage, Languages.LeaveMessage, 
                Languages.YesMessage, Languages.NoMessage);
                if (answer)
                {
                    Settings.IsRemembered = "false";
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.Token = null;
                    mainViewModel.User = null;
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                
            }
            else if (this.PageName == "MyProfilePage")
            {
                MainViewModel.GetInstance().MyProfile = new MyProfileViewModel();
                App.Navigator.PushAsync(new MyProfilePage());
            }
            else if (this.PageName == "AppointmentsPage")
            {
                MainViewModel.GetInstance().Appointments = new AppointmentsViewModel();
                App.Navigator.PushAsync(new AppointmentsPage());
            }
        }
        
        #endregion
    }

}
