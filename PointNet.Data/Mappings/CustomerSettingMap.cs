using FluentNHibernate.Mapping;
using PointNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNet.Data.Mappings
{
    public class CustomerSettingMap : ClassMap<CustomerSetting>
    {
        public CustomerSettingMap()
        {
            Id(x => x.CustomerSettingId);
            Map(x => x.Name);
            Map(x => x.Type);
            Map(x => x.Key);
            Map(x => x.AppId);
            Map(x => x.Account);
            Map(x => x.Password);
            Map(x => x.Host);
            Map(x => x.HostAnother);
            References(x => x.Customer).NotFound.Ignore().Column("CustomerId");
        }
    }
}
