




namespace Barbershop.Droid
{
    using Android.App;

    using Android.OS;

    [Activity(
        Theme = "@style/Theme.Splash",//indicative the themes
        MainLauncher = true, //set it as boot activity
        NoHistory = true)] // 

    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            System.Threading.Thread.Sleep(1800);
            this.StartActivity(typeof(MainActivity));
        }
    }
}