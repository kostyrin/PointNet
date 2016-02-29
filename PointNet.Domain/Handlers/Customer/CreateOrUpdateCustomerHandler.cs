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
    public class CreateOrUpdateCustomerHandler : ICommandHandler<CreateOrUpdateCustomerCommand>
    {
        private readonly IMappingEngine _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrUpdateCustomerHandler(IMappingEngine mapper, ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(CreateOrUpdateCustomerCommand command)
        {
            var customer = _mapper.Mapper.Map<Customer>(command);

            if (customer.CustomerId == 0)
                _customerRepository.Add(customer);
            else
                _customerRepository.Update(customer);

            _unitOfWork.Commit();
            return new CommandResult(true);
        }
    }
}
