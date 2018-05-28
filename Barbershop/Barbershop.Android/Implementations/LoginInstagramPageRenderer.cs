[assembly: Xamarin.Forms.ExportRenderer(
    typeof(Barbershop.Views.LoginInstagramPage),
    typeof(Barbershop.Droid.Implementations.LoginInstagramPageRenderer))]

namespace Barbershop.Droid.Implementations
{
    using Android.App;
    using Barbershop.Services;
    using Xamarin.Forms.Platform.Android;
    using Xamarin.Auth;
    using System;

    class LoginInstagramPageRenderer : PageRenderer
    {
        public LoginInstagramPageRenderer()
        {
            var activity = this.Context as Activity;
            var auth = new OAuth2Authenticator(
                clientId: "7507da3a643c4ce4bf8aeacc152118c3",
                scope: "basic",
                authorizeUrl: new Uri("https://api.instagram.com/oauth/authorize/"),
                redirectUrl: new Uri("http://zulu-software.com/"));
            auth.RequestParameters.Add("response_type", "code");
            auth.Completed += async (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    var token = eventArgs.Account.Properties["access_token"].ToString();
                    var apiService = new ApiService();
                    var profile = await apiService.GetInstagram(token);
                    App.NavigateToInstagramProfile(profile);
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