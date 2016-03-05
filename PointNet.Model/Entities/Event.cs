using System;
using System.Collections.Generic;

namespace PointNet.Model
{
    public class Event
    {
        public virtual int EventId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime ActivateDate { get; set; }
        public virtual DateTime DeadlineDate { get; set; }

        public virtual IList<EventLine> Lines { get; set; }
        public virtual IList<EventItem> EventItems { get; set; }
    }
}
