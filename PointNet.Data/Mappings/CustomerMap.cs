using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using PointNet.Model;

namespace PointNet.Data.Mappings
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(x => x.CustomerId);
            Map(x => x.Name);
            Map(x => x.Code);
            Map(x => x.Url);
            Map(x => x.IsActive);
            References(x => x.Parent).NotFound.Ignore().Column("ParentId");
            HasMany(x => x.SubCustomers).Cascade.All().KeyColumn("ParentId");

        }
    }
}
