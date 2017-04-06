using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrekSurfing.Web.Startup))]
namespace TrekSurfing.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
