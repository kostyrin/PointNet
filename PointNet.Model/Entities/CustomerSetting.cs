using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNet.Model
{
    public class CustomerSetting
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
