using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MySch.Startup))]
namespace MySch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
