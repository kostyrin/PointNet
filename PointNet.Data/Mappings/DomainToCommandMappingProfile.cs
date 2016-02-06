using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PointNet.Model;
using PointNet.Model.Commands;

namespace PointNet.Data
{
    public class DomainToCommandMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToCommandMappingProfile"; }
        }

        protected override void Configure()
        {
            CreateMap<Category, CreateOrUpdateCategoryCommand>();
            CreateMap<Category, DeleteCategoryCommand>();
            CreateMap<Expense, CreateOrUpdateExpenseCommand>();
            CreateMap<Expense, DeleteExpenseCommand>();
            CreateMap<User, UserRegisterCommand>().ForMember(x => x.Password, o => o.Ignore());
        }
    }
}
