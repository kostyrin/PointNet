using FluentNHibernate.Mapping;
using PointNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNet.Data.Mappings
{
    public class ItemCustomerMap : ClassMap<ItemCustomer>
    {
        public ItemCustomerMap()
        {
            Id(x => x.ItemCustomerId);
            Map(x => x.NameCustomer);
            Map(x => x.Code);
            Map(x => x.IsActive);
            References(x => x.Customer).NotFound.Ignore();
            References(x => x.Item).NotFound.Ignore();
        }
    }
}
