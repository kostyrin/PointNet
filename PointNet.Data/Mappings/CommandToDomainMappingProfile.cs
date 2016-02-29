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
            CreateMap<CreateOrUpdateCustomerCommand, Customer>()
                .ForMember(c => c.Events, o => o.Ignore())
                .ForMember(c => c.Settings, o => o.Ignore())
                .ForMember(c => c.ItemCustomers, o => o.Ignore())
                .ForMember(c => c.Events, o => o.Ignore())
                .ForMember(c => c.Lines, o => o.Ignore())
                //.ForMember(c => c.SubCustomers, o => o.Ignore())
                ;
            CreateMap<CreateOrUpdateCustomerSettingCommand, CustomerSetting>();
            CreateMap<CreateOrUpdateItemCommand, Item>()
                //.ForMember(c => c.SubItems, o => o.Ignore())
                .ForMember(c => c.ItemCustomers, o => o.Ignore());
            CreateMap<CreateOrUpdateItemCustomerCommand, ItemCustomer>().ForMember(i => i.Orders, o => o.Ignore());
            CreateMap<CreateOrUpdateEventCommand, Event>().ForMember(e => e.Lines, o => o.Ignore());
            CreateMap<CreateOrUpdateEventLineCommand, EventLine>().ForMember(e => e.Lots, o => o.Ignore());
            CreateMap<CreateOrUpdateLotCommand, Lot>();
            CreateMap<CreateOrUpdateOrderCommand, Order>();
        }
    }
}
