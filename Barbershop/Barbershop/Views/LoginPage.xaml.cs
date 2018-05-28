namespace Barbershop.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using ViewModels;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        private async void Login(object sender, ItemTappedEventArgs e)
        {
            Syncfusion.SfRadialMenu.XForms.SfRadialMenuItem item = sender as Syncfusion.SfRadialMenu.XForms.SfRadialMenuItem;
            radialMenu.Close();

            if (item.Text == "Facebook")
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Login.LoginFacebook();
            }
            else if (item.Text == "Instagram")
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Login.LoginInstagram();
            }
            else if(item.Text == "Linkedin")
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Login.LoginLinkedin();
            }
        }
    }
}