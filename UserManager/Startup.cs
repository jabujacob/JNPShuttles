using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JNPPortal.Startup))]
namespace JNPPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

