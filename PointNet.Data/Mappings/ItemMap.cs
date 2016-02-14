using FluentNHibernate.Mapping;
using PointNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNet.Data.Mappings
{
    public class ItemMap : ClassMap<Item>
    {
        public ItemMap()
        {
            Id(x => x.ItemId);
            Map(x => x.Name);
            Map(x => x.Kind);
            Map(x => x.IsActive);
            References(x => x.Parent).NotFound.Ignore().Column("ParentId");
            HasMany(x => x.SubItems).Cascade.All().KeyColumn("ParentId");
        }
    }
}
