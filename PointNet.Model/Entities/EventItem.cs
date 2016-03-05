using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNet.Model
{
    public class EventItem
    {
        public virtual int EventItemId { get; set; }
        public virtual Item Item { get; set; }
        public virtual Event Event { get; set; }
        public virtual int Position { get; set; }
    }
}
