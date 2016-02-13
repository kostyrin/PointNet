using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNet.Model.Entities
{
    public class Customer
    {
        public virtual int CustomerId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string Url { get; set; }
    }
}
