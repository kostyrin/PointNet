using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using PointNet.Model;
using PointNet.CommandProcessor.Command;
using PointNet.CommandProcessor.Dispatcher;
using PointNet.Data.Infrastructure;
using PointNet.Data.Repositories;
using PointNet.Web.Core.Authentication;
using System.Web.Http;
using NHibernate;
using AutoMapper;
using Microsoft.Owin.Security;
using PointNet.Web.Core.Models;
using Microsoft.AspNet.Identity;

namespace PointNet.Web.API
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacWebAPIServices();
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacWebAPIServices()
        {
            var builder = new ContainerBuilder();

            //NHibernate objects
            builder.Register(c => PointNet.Data.Infrastructure.ConnectionHelper.BuildSessionFactory("PointNetContainer")).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerRequest();

            //Infrastructure objects
            builder.RegisterType<DefaultCommandBus>().As<ICommandBus>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(IRepository<Expense>).Assembly).Where(t => t.Name.EndsWith("ExpenseRepository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IRepository<Category>).Assembly).Where(t => t.Name.EndsWith("CategoryRepository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IRepository<User>).Assembly).Where(t => t.Name.EndsWith("UserRepository")).AsImplementedInterfaces().InstancePerRequest();

            //Command Query Responsibility Separation objects
            var services = Assembly.Load("PointNet.Domain");
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerRequest();
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerRequest();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Automapper objects
            //builder.Register(c => new ConfigurationStore(new TypeMapFactory(), AutoMapper.Mappers.MapperRegistry.Mappers)).AsImplementedInterfaces().SingleInstance();
            builder.Register(c => Mapper.Engine).As<IMappingEngine>().SingleInstance();
            builder.RegisterType<TypeMapFactory>().As<ITypeMapFactory>().SingleInstance();

            //Microsoft Identity objects
            builder.RegisterType<PointNetUser>().InstancePerRequest();
            builder.RegisterType<DefaultUserRoleStore>().As<IUserRoleStore<PointNetUser, int>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager<PointNetUser, int>>().As<UserManager<PointNetUser, int>>().InstancePerRequest();
            builder.RegisterType<EmailService>().As<IIdentityMessageService>().InstancePerRequest();
            builder.RegisterType<SmsService>().As<IIdentityMessageService>().InstancePerRequest();

            IContainer container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;            
        }
    }
}
