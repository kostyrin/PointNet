using PointNet.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using Autofac;
using PointNet.CommandProcessor.Dispatcher;
using PointNet.Data.Repositories;
using PointNet.Data.Infrastructure;
using PointNet.Data.Mappings;
using PointNet.Model.Commands;
using PointNet.Core.Common;
using PointNet.CommandProcessor.Command;
using System.Reflection;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using PointNet.Data.SchemaTool;
//using HibernatingRhinos; //User NH Profiler to debug your NHibernate's queries

namespace PointNet.Test
{
    [TestClass]
    public class DDLSchemaTools
    {
        private TestContext testContextInstance;

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

        public DDLSchemaTools()
        {
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize(); //NH Profiler
        }

        [TestMethod]
        public void CreateSchema()
        {
            PointNet.Data.SchemaTool.SchemaTool.CreatSchema(string.Empty, "PointNetContainer");
        }

        [TestMethod]
        public void UpdateSchema()
        {
            PointNet.Data.SchemaTool.SchemaTool.UpdateSchema("PointNetContainer");
        }

        [TestMethod]
        public void ValidateSchema()
        {
            PointNet.Data.SchemaTool.SchemaTool.ValidateSchema("PointNetContainer");
        }
    }
}