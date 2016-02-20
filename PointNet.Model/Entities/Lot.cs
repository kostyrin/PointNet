using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNet.Model.Entities
{
    public class Lot
    {
        public virtual int LotId { get; set; }
        public virtual decimal Price { get; set; }
        public virtual DateTime Stamp { get; set; }
        public virtual string Type { get; set; }
        public virtual EventLine EventLine { get; set; }
    }
}
