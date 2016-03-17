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
    public class FromModelToCommandMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "FromModelToCommandMappingProfile"; }
        }

        protected override void Configure()
        {
            CreateMap<CustomerFormModel, CreateOrUpdateCustomerCommand>()
                .ForMember(c => c.Parent, o => o.Ignore())
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type));
            //CreateMap<CustomerFormModel, Customer>().ForMember(c => c.Parent, o => o.Ignore());
            CreateMap<CustomerSettingFormModel, CreateOrUpdateCustomerSettingCommand>()
                .ForMember(c => c.Customer, o => o.Ignore())
                .ForMember(d => d.Type, o => o.MapFrom(s => s.SettingsType));

        }
    }
}
