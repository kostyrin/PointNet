using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PointNet.Web.Startup))]
namespace PointNet.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
