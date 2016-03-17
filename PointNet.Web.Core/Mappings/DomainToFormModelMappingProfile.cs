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
            CreateMap<Customer, CustomerFormModel>()
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type));
            CreateMap<CustomerSetting, CustomerSettingFormModel>()
                .ForMember(d => d.CustomerId, o => o.MapFrom(s => s.Customer.CustomerId))
                .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.Customer.Name))
                .ForMember(d => d.SettingsType, o => o.MapFrom(s => s.Type));
        }
    }
}
