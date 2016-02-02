using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PointNet.Data.Infrastructure;
using PointNet.Model;
using NHibernate;

namespace PointNet.Data.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ISession session)
            : base(session)
        {
        }
    }

    public interface ICategoryRepository : IRepository<Category>
    {
    }
}
