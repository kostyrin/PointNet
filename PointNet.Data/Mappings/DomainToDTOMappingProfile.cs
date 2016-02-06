using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PointNet.Model;

namespace PointNet.Data.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        private readonly IConfiguration _configuration;
        public DomainToDTOMappingProfile(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public override string ProfileName
        {
            get { return "DomainToDTOMappingProfile"; }
        }

        protected override void Configure()
        {
            //_configuration.CreateMap<Category, DTOCategory>("Category");
            //Mapper.CreateMap<Expense, DTOExpense>();
            //Mapper.CreateMap<User, DTOUser>();
        }
    }
}
