using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNet.Model
{
    public class Customer
    {
        public virtual int CustomerId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string Url { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual Customer Parent { get; set; }

        public virtual IList<Customer> SubCustomers { get; set; }
        public virtual IList<CustomerSetting> Settings { get; set; }
        public virtual IList<ItemCustomer> ItemCustomers { get; set; }
        public virtual IList<EventLine> Lines { get; set; }
    }
}
