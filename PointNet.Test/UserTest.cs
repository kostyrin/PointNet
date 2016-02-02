using PointNet.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using Autofac;
using PointNet.CommandProcessor.Dispatcher;
using PointNet.Data.Repositories;
using PointNet.Data.Infrastructure;
using PointNet.Model.Commands;
using PointNet.Core.Common;
using PointNet.CommandProcessor.Command;
using System.Reflection;
using NHibernate;
using FluentNHibernate.Cfg;
using AutoMapper;

namespace PointNet.Test
{
    [TestClass()]
    public class UserTest
    {
        private TestContext testContextInstance;
        private IContainer container;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        public UserTest()
        {
            var builder = new ContainerBuilder();

            builder.Register(c => PointNet.Data.Infrastructure.ConnectionHelper.BuildSessionFactory("PointNetContainer")).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerLifetimeScope();

            builder.RegisterType<DefaultCommandBus>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IRepository<User>).Assembly).Where(t => t.Name.EndsWith("UserRepository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            var services = Assembly.Load("PointNet.Domain");
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerLifetimeScope();

            //builder.Register(c => new ConfigurationStore(new TypeMapFactory(), AutoMapper.Mappers.MapperRegistry.Mappers)).AsImplementedInterfaces().SingleInstance();
            builder.Register(c => Mapper.Engine).As<IMappingEngine>().SingleInstance();
            builder.RegisterType<TypeMapFactory>().As<ITypeMapFactory>().SingleInstance();

            container = builder.Build();

            AutoMapperConfiguration.Configure();
        }

        [TestMethod()]
        public void UserCreateTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                IMappingEngine mapper = lifetime.Resolve<IMappingEngine>();

                User user = new User()
                {
                    FirstName = "Test",
                    LastName = "User",
                    Email = "testuser@gmail.com",
                    DateCreated = DateTime.Now,
                    RoleId = 1,
                    Activated = true
                };

                UserRegisterCommand command = mapper.Mapper.Map<UserRegisterCommand>(user);
                command.Password = "TEST";

                IValidationHandler<UserRegisterCommand> validationHandler = lifetime.Resolve<IValidationHandler<UserRegisterCommand>>();
                IEnumerable<ValidationResult> validations = commandBus.Validate(command, validationHandler);
                foreach (var val in validations)
                {
                    Assert.IsNull(val, "Error: User creation did not validate " + val.Message);
                }
                ICommandHandler<UserRegisterCommand> commnadHandler = lifetime.Resolve<ICommandHandler<UserRegisterCommand>>();
                ICommandResult result = commandBus.Submit(command, commnadHandler);
                Assert.IsNotNull(result, "Error: User was not created by CommandBus");
                Assert.IsTrue(result.Success, "Error: User was not created by CommandBus");
            }
        }

        [TestMethod()]
        public void UserGetTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                IUserRepository userRepository = lifetime.Resolve<IUserRepository>();

                User user = userRepository.Get(c => c.Email == "testuser@gmail.com");
                Assert.IsNotNull(user, "Error: User was not found");
            }
        }

        [TestMethod()]
        public void UserChangePasswordTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                IUserRepository userRepository = lifetime.Resolve<IUserRepository>();
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();

                User user = userRepository.Get(c => c.Email == "testuser@gmail.com");
                Assert.IsNotNull(user, "Error: User was not found");

                ChangePasswordCommand command = new ChangePasswordCommand();
                command.UserId = user.UserId;
                command.OldPassword = "TEST";
                command.NewPassword = "TEST2";

                IValidationHandler<ChangePasswordCommand> validationHandler = lifetime.Resolve<IValidationHandler<ChangePasswordCommand>>();
                IEnumerable<ValidationResult> validations = commandBus.Validate(command, validationHandler);
                foreach (var val in validations)
                {
                    Assert.IsNull(val, "Error: User password change did not validate " + val.Message);
                }
                ICommandHandler<ChangePasswordCommand> commnadHandler = lifetime.Resolve<ICommandHandler<ChangePasswordCommand>>();
                ICommandResult result = commandBus.Submit(command, commnadHandler);
                Assert.IsNotNull(result, "Error: User password change did not work");
                Assert.IsTrue(result.Success, "Error: User password change did not work");
            }
        }
    }
}
