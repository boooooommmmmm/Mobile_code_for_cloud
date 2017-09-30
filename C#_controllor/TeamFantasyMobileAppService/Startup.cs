using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TeamFantasyMobileAppService.Startup))]

namespace TeamFantasyMobileAppService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}