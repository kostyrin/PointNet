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
    public class CommandToDomainMappingProfile : Profile
    {

        public override string ProfileName
        {
            get { return "CommandToDomainMappingProfile"; }
        }

        protected override void Configure()
        {
            CreateMap<CreateOrUpdateCategoryCommand, Category>().ForMember(x => x.Expenses, o => o.Ignore());
            CreateMap<DeleteCategoryCommand, Category>().ForMember(x => x.Description, o => o.Ignore())
                                                               .ForMember(x => x.Expenses, o => o.Ignore())
                                                               .ForMember(x => x.Name, o => o.Ignore());
            CreateMap<CreateOrUpdateExpenseCommand, Expense>();
            CreateMap<DeleteExpenseCommand, Expense>().ForMember(x => x.Amount, o => o.Ignore())
                                                             .ForMember(x => x.Category, o => o.Ignore())
                                                             .ForMember(x => x.Date, o => o.Ignore())
                                                             .ForMember(x => x.TransactionDesc, o => o.Ignore());
            CreateMap<UserRegisterCommand, User>().ForMember(x => x.UserId, o => o.Ignore())
                                                         .ForMember(x => x.PasswordHash, o => o.Ignore())
                                                         .ForMember(x => x.DateCreated, o => o.Ignore())
                                                         //.ForMember(x => x.LastLoginTime, o => o.Ignore())
                                                         ;
        }
    }
}
