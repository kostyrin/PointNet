using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PointNet.Data.Infrastructure;
using PointNet.Model;
using NHibernate;

namespace PointNet.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ISession session)
            : base(session)
        {
        }
    }

    public interface IUserRepository : IRepository<User>
    {
    }
}
