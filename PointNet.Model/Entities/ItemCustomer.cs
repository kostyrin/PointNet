using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNet.Model
{
    public class ItemCustomer
    {
        public virtual int ItemCustomerId { get; set; }
        public virtual string NameCustomer { get; set; }
        public virtual string Code { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Item Item { get; set; }
    }
}
