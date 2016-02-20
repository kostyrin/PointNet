using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNet.Model
{
    public class Item
    {
        public virtual int ItemId { get; set; }
        public virtual string Name { get; set; }
        public virtual int Kind { get; set; } //TODO
        public virtual Item Parent { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual IList<Item> SubItems { get; set; }

        public virtual IList<ItemCustomer> ItemCustomers { get; set; }

    }
}
