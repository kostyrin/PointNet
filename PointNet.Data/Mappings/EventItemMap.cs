using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using PointNet.Model;

namespace PointNet.Data.Mappings
{
    public class EventItemMap : ClassMap<EventItem>
    {
        public EventItemMap()
        {
            Id(x => x.EventItemId);
            Map(x => x.Position);
            References(x => x.Item);
            References(x => x.Event);
        }
    }
}
