using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Barbershop.Backend.Startup))]
namespace Barbershop.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
