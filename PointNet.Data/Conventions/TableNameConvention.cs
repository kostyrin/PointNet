using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace PointNet.Data.Conventions
{
    public class TableNameConvention : IClassConvention
    {
        public void Apply(IClassInstance instance)
        {
            string typeName = instance.EntityType.Name;

            instance.Table(Inflector.Inflector.Pluralize(typeName).ToLower());
        }

        //private string ToSnakeCase(string name)
        //{
        //    var result = new StringBuilder(name.Length);
        //    for (int i = 0; i < name.Length; i++)
        //    {
        //        if (i > 0 && char.IsUpper(name[i]))
        //            result.Append('_').Append(char.ToLower(name[i]));
        //        else
        //            result.Append(char.ToLower(name[i]));
        //    }
        //    return result.ToString();
        //}
    }
}
