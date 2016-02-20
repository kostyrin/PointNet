using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNet.Model
{
    public class Order
    {
        public virtual int OrderId { get; set; }
        public virtual ItemCustomer ItemCustomer { get; set; }
    }
}
