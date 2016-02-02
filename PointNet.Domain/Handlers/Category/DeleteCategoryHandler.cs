﻿using PointNet.CommandProcessor.Command;
using PointNet.Model.Commands;
using PointNet.Data.Repositories;
using PointNet.Data.Infrastructure;

namespace PointNet.Domain.Handlers
{
    public class DeleteCategoryHandler : ICommandHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(DeleteCategoryCommand command)
        {
            var category = categoryRepository.GetById(command.CategoryId);
            categoryRepository.Delete(category);
            unitOfWork.Commit();
            return new CommandResult(true);
        }
    }
}
