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
            Map(x => x.IsActive).Column("is_active");
            References(x => x.Customer).NotFound.Ignore().Column("Customer_Id");
            References(x => x.Item).NotFound.Ignore().Column("Item_Id");
            //CompositeId().Mapped().KeyReference(x => x.Customer, "customer_id").KeyReference(x => x.Item, "item_id"); TODO
            HasMany(x => x.Lines).LazyLoad().Inverse().Cascade.All();

        }
    }
}
