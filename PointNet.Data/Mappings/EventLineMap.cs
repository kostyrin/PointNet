using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using PointNet.Model;

namespace PointNet.Data.Mappings
{
    public class EventLineMap : ClassMap<EventLine>
    {
        public EventLineMap()
        {
            Id(x => x.LineId);
            Map(x => x.Code);
            Map(x => x.Type);

            References(x => x.Customer);
            References(x => x.Event);
            HasMany(x => x.Lots).LazyLoad().Inverse().Cascade.All();
        }
    }
}
