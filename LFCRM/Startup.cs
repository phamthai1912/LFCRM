using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LFCRM.Startup))]
namespace LFCRM
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
