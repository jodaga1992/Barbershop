namespace Barbershop.ViewModels
{
    using System.Collections.ObjectModel;
    using Models;

    public class MainViewModel : BaseViewModel
    {
        #region Attibrutes
        private UserLocal user;
        #endregion

        #region Properties

        public TokenResponse Token
        {
            get;
            set;
        }
        
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

        public UserLocal User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
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

        public MyProfileViewModel MyProfile
        {
            get;
            set;
        }

        public ChangePasswordViewModel ChangePassword
        {
            get;
            set;
        }

        public ScheduleViewModel Schedule
        {
            get;
            set;
        }

        public AppointmentsViewModel Appointments
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
                Icon = "ic_date_range",
                PageName = "AppointmentsPage",
                Title = "My Appointments"
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
