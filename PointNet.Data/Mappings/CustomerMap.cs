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
            Map(x => x.Type);
            References(x => x.Parent).NotFound.Ignore().Column("Parent_Id");
            HasMany(x => x.SubCustomers).Cascade.All().KeyColumn("Parent_Id");
            HasMany(x => x.Settings).LazyLoad().Inverse().Cascade.All();
            HasMany(x => x.Events).LazyLoad().Inverse().Cascade.All();
            HasMany(x => x.Lines).LazyLoad().Inverse().Cascade.All();
        }
    }
}
