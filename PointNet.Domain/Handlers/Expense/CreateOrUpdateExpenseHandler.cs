using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointNet.CommandProcessor.Command;
using PointNet.Model.Commands;
using PointNet.Data.Repositories;
using PointNet.Data.Infrastructure;
using PointNet.Model;
using AutoMapper;

namespace PointNet.Domain.Handlers
{
    public class CreateOrUpdateExpenseHandler : ICommandHandler<CreateOrUpdateExpenseCommand>
    {
        private readonly IMappingEngine mapper;
        private readonly IExpenseRepository expenseRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateOrUpdateExpenseHandler(IMappingEngine mapper, IExpenseRepository expenseRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.expenseRepository = expenseRepository;
            this.unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(CreateOrUpdateExpenseCommand command)
        {
            var expense = this.mapper.Mapper.Map<Expense>(command);

            if (expense.ExpenseId == 0)
                expenseRepository.Add(expense);
            else
                expenseRepository.Update(expense);

            unitOfWork.Commit();
            return new CommandResult(true);
        }
    }
}
