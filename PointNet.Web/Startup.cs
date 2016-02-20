using System.Web.Mvc;
using Autofac;
using Microsoft.Owin;
using Owin;
using PointNet.Common.Logging;
using PointNet.Data.Infrastructure;
using PointNet.Data.SchemaTool;

[assembly: OwinStartupAttribute(typeof(PointNet.Web.Startup))]
namespace PointNet.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var log = DependencyResolver.Current.GetService<ILogger>();
            log.Info("config");
            ConfigureAuth(app);
            log.Info("create schema");
            SchemaTool.CreatSchema(string.Empty, "PointNetContainer");
            log.Info("schema ready");
        }
    }
}
