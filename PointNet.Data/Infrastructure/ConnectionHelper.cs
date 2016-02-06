using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using PointNet.Data.Conventions;
using PointNet.Data.Mappings;

namespace PointNet.Data.Infrastructure
{
    static public class ConnectionHelper
    {
        public static ISessionFactory BuildSessionFactory(string connString)
        {
            return GetConfiguration(connString).BuildSessionFactory();
        }

        public static FluentConfiguration GetConfiguration(string connString)
        {
            return Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(c => c.FromConnectionStringWithKey(connString)))
                .ExposeConfiguration(c => c.SetProperty("command_timeout", (TimeSpan.FromMinutes(5).TotalSeconds).ToString()))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>()
                .Conventions.AddFromAssemblyOf<TableNameConvention>());
        }

        public static void CreateDatabase(string connString)
        {
            var configuration = GetConfiguration(connString).BuildConfiguration();
            var exporter = new SchemaExport(configuration);
            exporter.Execute(true, true, false);
            //configuration.BuildSessionFactory();
        }
    }
}
