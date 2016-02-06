using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace PointNet.Data.Conventions
{
    public class ForeignKeyConstraintNameConvention : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Key.ForeignKey(string.Format("{0}_{1}_fk", instance.Member.Name.ToLower(), instance.EntityType.Name.ToLower()));
        }
    }
}
