using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace PointNet.Data.Conventions
{
    //public class CurrentForeignKeyConvention : ForeignKeyConvention
    //{
    //    protected override string GetKeyName(Member member, Type type)
    //    {
    //        if (member == null)
    //            return "FK" + type.Name;  // many-to-many, one-to-many, join

    //        return "FK" + member.Name; // many-to-one
    //    }
    //}
}
