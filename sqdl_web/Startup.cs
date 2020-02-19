using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sqdl_web.Startup))]
namespace sqdl_web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
