using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PointNet.CommandProcessor.Command;
using PointNet.Data.Infrastructure;
using PointNet.Data.Repositories;
using PointNet.Model;
using PointNet.Model.Commands;

namespace PointNet.Domain.Handlers
{
    public class CreateOrUpdateCustomerSettingHandler : ICommandHandler<CreateOrUpdateCustomerSettingCommand>
    {
        private readonly IMappingEngine _mapper;
        private readonly ICustomerSettingRepository _customerSettingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrUpdateCustomerSettingHandler(IMappingEngine mapper, ICustomerSettingRepository customerSettingRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _customerSettingRepository = customerSettingRepository;
            _unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(CreateOrUpdateCustomerSettingCommand command)
        {
            var setting = _mapper.Mapper.Map<CustomerSetting>(command);

            if (setting.CustomerSettingId == 0)
                _customerSettingRepository.Add(setting);
            else
                _customerSettingRepository.Update(setting);

            _unitOfWork.Commit();
            return new CommandResult(true);
        }
    }
}
