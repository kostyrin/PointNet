using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using PointNet.Model;
using PointNet.Model.Entities;

namespace PointNet.Data.Mappings
{
    public class LotMap : ClassMap<Lot>
    {
        public LotMap()
        {
            Id(x => x.LotId);
            Map(x => x.Price);
            Map(x => x.Stamp);
            Map(x => x.Type);

            References(x => x.EventLine);
        }
    }
}
