using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using PointNet.Data;
using PointNet.Model;
using PointNet.CommandProcessor.Command;
using PointNet.CommandProcessor.Dispatcher;
using PointNet.Data.Infrastructure;
using PointNet.Data.Repositories;
using PointNet.Web.Core.Authentication;
using PointNet.Web.Core.Models;
using Microsoft.AspNet.Identity;
using NHibernate;
using AutoMapper;
using Microsoft.Owin.Security;

namespace PointNet.Web
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            //Infrastructure objects
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).AsImplementedInterfaces();
            builder.RegisterType<DefaultCommandBus>().As<ICommandBus>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterModule(new AutofacWebTypesModule());

            //Command Query Responsibility Separation objects
            var services = Assembly.Load("PointNet.Domain");
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerRequest();
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerRequest();

            //Repositories objects
            builder.RegisterAssemblyTypes(typeof(IRepository<Expense>).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IRepository<Category>).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IRepository<User>).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();

            //NHibernate objects
            builder.Register(c => PointNet.Data.Infrastructure.ConnectionHelper.BuildSessionFactory("PointNetContainer")).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerRequest();

            //Automapper objects
            //builder.Register(c => new ConfigurationStore(new TypeMapFactory(), AutoMapper.Mappers.MapperRegistry.Mappers)).AsImplementedInterfaces().SingleInstance();

            //builder.Register(c => new MapperConfiguration(cfg =>
            //{
            //    cfg.
            //})).AsImplementedInterfaces().SingleInstance();
            //builder.Register(c => Mapper.Engine).As<IMappingEngine>().SingleInstance();
            builder.RegisterType<MappingEngine>().As<IMappingEngine>();
            builder.RegisterType<TypeMapFactory>().As<ITypeMapFactory>().SingleInstance();

            //Microsoft Identity objects
            builder.RegisterType<PointNetUser>().InstancePerRequest();
            builder.RegisterType<DefaultUserRoleStore>().As<IUserRoleStore<PointNetUser, int>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager<PointNetUser, int>>().InstancePerRequest();
            builder.RegisterType<EmailService>().As<IIdentityMessageService>().InstancePerRequest();
            builder.RegisterType<SmsService>().As<IIdentityMessageService>().InstancePerRequest();

            builder.RegisterModelBinderProvider();
            builder.RegisterFilterProvider();

            IContainer container = builder.Build();
 
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }        
    }
}
