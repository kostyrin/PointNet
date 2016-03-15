using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointNet.Web.Core.Models
{
    public class CustomerFormModel
    {
        public int CustomerId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
    }
}