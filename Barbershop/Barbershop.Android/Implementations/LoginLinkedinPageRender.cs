[assembly: Xamarin.Forms.ExportRenderer(
    typeof(Barbershop.Views.LoginLinkedinPage),
    typeof(Barbershop.Droid.Implementations.LoginLinkedinPageRender))]
namespace Barbershop.Droid.Implementations
{
    using System;
    using System.Threading.Tasks;
    using Android.App;
    using Barbershop.Models;
    using Barbershop.Services;
    using Xamarin.Auth;
    using Xamarin.Forms.Platform.Android;

    class LoginLinkedinPageRender : PageRenderer
    {
        public LoginLinkedinPageRender()
        {
            var activity = this.Context as Activity;

            var auth = new OAuth2Authenticator(
                clientId: "78jj3nquoj0vwe",
                clientSecret: "Yf7vsH7jDua9vTxP",
                scope: "",
                authorizeUrl: new Uri("https://www.linkedin.com/oauth/v2/authorization"),
                redirectUrl: new Uri("http://zulu-software.com/"),
                accessTokenUrl: new Uri("https://www.linkedin.com/oauth/v2/accessToken"));

            auth.Completed += async (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    var accesToken = eventArgs.Account.Properties["access_token"].ToString();
                    var api = new ApiService();
                    var userLinkedin = await api.GetLinkedin(accesToken);
                    App.NavigateToLinkedinProfile(userLinkedin);
                }
                else
                {
                    App.HideLoginView();
                }
            };
            activity.StartActivity(auth.GetUI(activity));
        }
    }
}