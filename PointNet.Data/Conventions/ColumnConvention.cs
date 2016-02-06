using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;

namespace PointNet.Data.Conventions
{
    public class ColumnConvention : IPropertyConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IPropertyInstance instance)
        {
            var regexString = @"([A-Z][\w^[A-Z]]*)([A-Z][\w^[A-Z]]*)*";

            var newName = Regex.Replace(instance.Name, regexString, (m => (m.Index != 0 ? "_" : "") + m.Value.ToLower()));

            instance.Column(newName);
        }
    }
}
