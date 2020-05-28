using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Users.UI.Startup))]
namespace Users.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
