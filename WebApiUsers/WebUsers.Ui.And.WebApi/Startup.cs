using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebUsers.Ui.And.WebApi.Startup))]
namespace WebUsers.Ui.And.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
