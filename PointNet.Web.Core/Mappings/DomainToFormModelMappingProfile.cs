using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PointNet.Model;
using PointNet.Model.Commands;
using PointNet.Web.Core.Models;

namespace PointNet.Web.Core.Mappings
{
    public class DomainToFormModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToFormModelMappingProfile"; }
        }

        protected override void Configure()
        {
            CreateMap<Customer, CustomerFormModel>();
        }
    }
}
