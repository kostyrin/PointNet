using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace PointNet.Data.Conventions
{
    public class ForeignKeyCompositeIdentityConvention : ICompositeIdentityConvention
    {
        public void Apply(ICompositeIdentityInstance instance)
        {
            foreach (var key in instance.KeyManyToOnes)
            {
                key.ForeignKey(string.Format("{0}_{1}_fk", key.Name.ToLower(), instance.EntityType.Name.ToLower()));
            }

            //instance.ForeignKey(string.Format("{0}_{1}_fk", instance.Member.Name.ToLower(), instance.EntityType.Name.ToLower()));
        }
    }
}
