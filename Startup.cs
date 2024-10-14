using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectSite.Startup))]
namespace ProjectSite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
