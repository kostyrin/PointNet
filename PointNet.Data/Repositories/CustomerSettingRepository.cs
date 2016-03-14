using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using PointNet.Data.Infrastructure;
using PointNet.Model;

namespace PointNet.Data.Repositories
{
    public class CustomerSettingRepository : RepositoryBase<CustomerSetting>, ICustomerSettingRepository
    {
        public CustomerSettingRepository(ISession session)
            : base(session)
        {
        }
    }

    public interface ICustomerSettingRepository : IRepository<CustomerSetting>
    {
    }
}
