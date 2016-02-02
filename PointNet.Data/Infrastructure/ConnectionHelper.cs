using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using PointNet.Data.Mappings;

namespace PointNet.Data.Infrastructure
{
    static public class ConnectionHelper
    {
        public static ISessionFactory BuildSessionFactory(string ConnString)
        {
            return GetConfiguration(ConnString).BuildSessionFactory();
        }

        public static FluentConfiguration GetConfiguration(string ConnString)
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey(ConnString)))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>());
        }
    }
}
