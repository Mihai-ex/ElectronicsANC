using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ElectronicsANC.Startup))]
namespace ElectronicsANC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
