using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Exigo_DockerManager.Startup))]
namespace Exigo_DockerManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
