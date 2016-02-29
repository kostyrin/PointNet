using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointNet.CommandProcessor.Command;

namespace PointNet.Model.Commands
{
    public class CreateOrUpdateOrderCommand : ICommand
    {
        public virtual int OrderId { get; set; }
        public virtual ItemCustomer ItemCustomer { get; set; }
    }
}
