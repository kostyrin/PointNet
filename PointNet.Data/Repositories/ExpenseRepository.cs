using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PointNet.Data.Infrastructure;
using PointNet.Model;
using NHibernate;

namespace PointNet.Data.Repositories
{
    public class ExpenseRepository : RepositoryBase<Expense>, IExpenseRepository
    {
        public ExpenseRepository(ISession session)
            : base(session)
        {
        }
    }

    public interface IExpenseRepository : IRepository<Expense>
    {
    }
}
