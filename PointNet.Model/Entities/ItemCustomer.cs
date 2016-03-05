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
        public virtual IList<Order> Orders { get; set; }
        public virtual IList<EventLine> Lines { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ItemCustomer);
        }

        private bool Equals(ItemCustomer other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, this)) return true;

            return Customer.CustomerId == other.Customer.CustomerId &&
                Item.ItemId == other.Item.ItemId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = GetType().GetHashCode();
                hash = (hash * 31) ^ Customer.CustomerId.GetHashCode();
                hash = (hash * 31) ^ Item.ItemId.GetHashCode();

                return hash;
            }
        }
    }
}
