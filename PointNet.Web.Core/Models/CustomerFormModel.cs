using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointNet.Web.Core.Models
{
    public class CustomerFormModel
    {
        public virtual int CustomerId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string Url { get; set; }
        public virtual bool IsActive { get; set; }
    }
}