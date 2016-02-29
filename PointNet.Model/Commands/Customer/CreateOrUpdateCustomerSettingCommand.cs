using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointNet.CommandProcessor.Command;

namespace PointNet.Model.Commands
{
    public class CreateOrUpdateCustomerSettingCommand : ICommand
    {
        public virtual int CustomerSettingId { get; set; }
        public virtual string Name { get; set; }
        public virtual int Type { get; set; } //TODO
        public virtual string Key { get; set; }
        public virtual string AppId { get; set; }
        public virtual string Account { get; set; }
        public virtual string Password { get; set; }
        public virtual string Host { get; set; }
        public virtual string HostAnother { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
