using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNet.Model
{
    public class EventLine
    {
        public virtual int LineId { get; set; }
        public virtual string Code { get; set; }
        public virtual string Type { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Event Event { get; set; }
    }
}
