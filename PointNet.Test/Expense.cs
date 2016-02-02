﻿using PointNet.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
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
    public class ExpenseTest
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

        public ExpenseTest()
        {
            var builder = new ContainerBuilder();

            builder.Register(c => PointNet.Data.Infrastructure.ConnectionHelper.BuildSessionFactory("PointNetContainer")).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerLifetimeScope();

            builder.RegisterType<DefaultCommandBus>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IRepository<Expense>).Assembly).Where(t => t.Name.EndsWith("ExpenseRepository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(IRepository<Category>).Assembly).Where(t => t.Name.EndsWith("CategoryRepository")).AsImplementedInterfaces().InstancePerLifetimeScope();

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
        public void ExpenseCreateTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                ICategoryRepository categoryRepository = lifetime.Resolve<ICategoryRepository>();
                IMappingEngine mapper = lifetime.Resolve<IMappingEngine>();

                Category category = categoryRepository.GetAll().FirstOrDefault();

                Expense expense = new Expense()
                {
                    Amount = 120,
                    Date = DateTime.Now,
                    Category = category,
                    TransactionDesc = "Test transaction."
                };

                CreateOrUpdateExpenseCommand command = mapper.Mapper.Map<CreateOrUpdateExpenseCommand>(expense);

                ICommandHandler<CreateOrUpdateExpenseCommand> commnadHandler = lifetime.Resolve<ICommandHandler<CreateOrUpdateExpenseCommand>>();
                ICommandResult result = commandBus.Submit(command, commnadHandler);
                Assert.IsNotNull(result, "Error: Tipo Via Was Not Created by CommandBus");
                Assert.IsTrue(result.Success, "Error: Tipo Via Was Not Created by CommandBus");
            }
        }

        [TestMethod()]
        public void ExpenseGetTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                IExpenseRepository expenseRepository = lifetime.Resolve<IExpenseRepository>();

                Expense expense = expenseRepository.Get(c => c.Amount == 120);
                Assert.IsNotNull(expense, "Error: Expense was not found");
            }
        }

        [TestMethod()]
        public void ExpenseUpdateTest()
        {
            Expense expense = null;
            using (var lifetime = container.BeginLifetimeScope())
            {
                IExpenseRepository expenseRepository = lifetime.Resolve<IExpenseRepository>();
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                IMappingEngine mapper = lifetime.Resolve<IMappingEngine>();

                expense = expenseRepository.Get(c => c.Amount == 120);
                Assert.IsNotNull(expense, "Error: Expense was not found");
                expense.Amount = 150;
            }

            using (var lifetime = container.BeginLifetimeScope())
            {
                IExpenseRepository expenseRepository = lifetime.Resolve<IExpenseRepository>();
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                IMappingEngine mapper = lifetime.Resolve<IMappingEngine>();

                CreateOrUpdateExpenseCommand command = mapper.Mapper.Map<CreateOrUpdateExpenseCommand>(expense);

                ICommandHandler<CreateOrUpdateExpenseCommand> commnadHandler = lifetime.Resolve<ICommandHandler<CreateOrUpdateExpenseCommand>>();
                ICommandResult result = commandBus.Submit(command, commnadHandler);
                Assert.IsNotNull(result, "Error: Expense was not updated");
                Assert.IsTrue(result.Success, "Error: Expense was not updated");
            }

        }

        [TestMethod()]
        public void ExpenseDeleteTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                IExpenseRepository expenseRepository = lifetime.Resolve<IExpenseRepository>();
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                IMappingEngine mapper = lifetime.Resolve<IMappingEngine>();

                Expense expense = expenseRepository.Get(c => c.Amount == 150);
                Assert.IsNotNull(expense, "Error: Expense was not found");

                DeleteExpenseCommand command = mapper.Mapper.Map<DeleteExpenseCommand>(expense);
                ICommandHandler<DeleteExpenseCommand> commnadHandler = lifetime.Resolve<ICommandHandler<DeleteExpenseCommand>>();
                ICommandResult result = commandBus.Submit(command, commnadHandler);
                Assert.IsNotNull(result, "Error: Expense was not deleted");
                Assert.IsTrue(result.Success, "Error: Expense was not deleted");
            }
        }
    }
}
