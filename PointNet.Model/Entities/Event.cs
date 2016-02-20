using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNet.Model.Entities
{
    public class Event
    {
        public virtual int EventId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Status { get; set; }
    }
}
