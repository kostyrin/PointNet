using Microsoft.Owin;
using Owin;
using PointNet.Data.Infrastructure;
using PointNet.Data.SchemaTool;

[assembly: OwinStartupAttribute(typeof(PointNet.Web.Startup))]
namespace PointNet.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            SchemaTool.CreatSchema(string.Empty, "PointNetContainer");
        }
    }
}
