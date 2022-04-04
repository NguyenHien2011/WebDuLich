using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DOANWEBDULICH.Startup))]
namespace DOANWEBDULICH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
