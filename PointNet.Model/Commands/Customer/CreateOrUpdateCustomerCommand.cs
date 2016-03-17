using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointNet.CommandProcessor.Command;

namespace PointNet.Model.Commands
{
    public class CreateOrUpdateCustomerCommand : ICommand
    {
        public virtual int CustomerId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string Url { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual Customer Parent { get; set; }
        public virtual int Type { get; set; }
    }
}
