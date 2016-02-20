using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using PointNet.Model;

namespace PointNet.Data.Mappings
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Id(o => o.OrderId);
            //References(o => o.ItemCustomer).NotFound.Ignore().Column("item_customer_id");
        }
    }
}
