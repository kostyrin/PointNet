using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using PointNet.Model;

namespace PointNet.Data.Mappings
{
    public class EventMap : ClassMap<Event>
    {
        public EventMap()
        {
            Id(x => x.EventId);
            Map(x => x.Name);
            Map(x => x.Status);
            Map(x => x.CreateDate);
            Map(x => x.ActivateDate);
            Map(x => x.DeadlineDate);
            References(x => x.ItemFirst);
            References(x => x.ItemSecond);
        }
    }
}
