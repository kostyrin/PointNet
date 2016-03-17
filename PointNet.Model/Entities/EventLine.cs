using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointNet.Model;

namespace PointNet.Model
{
    public class EventLine
    {
        public virtual int LineId { get; set; }
        public virtual string Code { get; set; }
        public virtual int Type { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Event Event { get; set; }
        public virtual ItemCustomer ItemCustomer { get; set; }

        public virtual IList<Lot> Lots { get; set; }

    }
}
