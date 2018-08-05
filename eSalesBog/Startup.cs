using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eSalesBog.Startup))]
namespace eSalesBog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
