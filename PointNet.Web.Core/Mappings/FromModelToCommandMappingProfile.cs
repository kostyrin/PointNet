﻿using System;
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
            CreateMap<CustomerFormModel, CreateOrUpdateCustomerCommand>().ForMember(c => c.Parent, o => o.Ignore());
        }
    }
}