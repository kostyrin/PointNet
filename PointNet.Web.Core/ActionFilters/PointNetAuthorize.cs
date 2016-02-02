using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace PointNet.Web.Core.ActionFilters
{
    public class PointNetAuthorize : AuthorizeAttribute
    {
        public PointNetAuthorize(params string[] roles)
        {
            this.Roles = String.Join(", ", roles);
        }
    }
}
