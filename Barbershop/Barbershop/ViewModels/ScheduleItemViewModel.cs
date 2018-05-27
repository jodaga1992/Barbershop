namespace Barbershop.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Barbershop.Views;
    using GalaSoft.MvvmLight.Command;
    using Models;

    public class ScheduleItemViewModel : ScheduleResponse
    {
        #region Commands
        public ICommand SelectScheduleCommand
        {
            get
            {
                return new RelayCommand(SelectSchedule);
            }
        }
        #endregion

        #region Methods
        private async void SelectSchedule()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Schedule = new ScheduleViewModel(this);
            await App.Navigator.PushAsync(new SchedulePage());
        }
        #endregion
    }
}
