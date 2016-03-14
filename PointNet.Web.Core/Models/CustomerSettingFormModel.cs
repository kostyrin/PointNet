using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNet.Web.Core.Models
{
    public class CustomerSettingFormModel
    {
        public int CustomerSettingId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; } //TODO
        public string Key { get; set; }
        public string AppId { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string HostAnother { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        //public CustomerFormModel Customer { get; set; }
    }
}
