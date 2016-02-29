using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointNet.CommandProcessor.Command;

namespace PointNet.Model.Commands
{
    public class CreateOrUpdateEventCommand : ICommand
    {
        public virtual int EventId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime ActivateDate { get; set; }
        public virtual DateTime DeadlineDate { get; set; }
        public virtual Item ItemFirst { get; set; }
        public virtual Item ItemSecond { get; set; }
    }
}
