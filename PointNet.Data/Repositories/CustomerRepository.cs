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
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(ISession session) : base(session)
        {
        }
    }

    public interface ICustomerRepository : IRepository<Customer>
    {
    }
}
