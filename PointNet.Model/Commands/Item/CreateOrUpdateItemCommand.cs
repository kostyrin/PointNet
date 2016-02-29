using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointNet.CommandProcessor.Command;

namespace PointNet.Model.Commands
{
    public class CreateOrUpdateItemCommand : ICommand
    {
        public virtual int ItemId { get; set; }
        public virtual string Name { get; set; }
        public virtual int Kind { get; set; } //TODO
        public virtual Item Parent { get; set; }
        public virtual bool IsActive { get; set; }
    }
}
