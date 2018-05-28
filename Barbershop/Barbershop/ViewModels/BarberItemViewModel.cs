namespace Barbershop.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Views;

    public class BarberItemViewModel: BarberResponse
    {
        #region Commands
        public ICommand SelectBerberCommand
        {
            get
            {
                return new RelayCommand(SelectBarber);
            }
        }
        #endregion

        #region Methods
        private async void SelectBarber()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Barber = new BarberViewModel(this);
            await App.Navigator.PushAsync(new BarberPage());
        }
        #endregion
    }
}
