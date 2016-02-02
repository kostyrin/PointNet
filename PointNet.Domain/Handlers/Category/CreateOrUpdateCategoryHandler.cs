using PointNet.Data.Repositories;
using PointNet.Data.Infrastructure;
using PointNet.Model.Commands;
using PointNet.CommandProcessor.Command;
using PointNet.Model;
using AutoMapper;

namespace PointNet.Domain.Handlers
{
    public class CreateOrUpdateCategoryHandler : ICommandHandler<CreateOrUpdateCategoryCommand>
    {
        private readonly IMappingEngine mapper;
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateOrUpdateCategoryHandler(IMappingEngine mapper, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(CreateOrUpdateCategoryCommand command)
        {
            var category = this.mapper.Mapper.Map<Category>(command);

            if (category.CategoryId == 0)
                categoryRepository.Add(category);
            else
                categoryRepository.Update(category);

            unitOfWork.Commit();
            return new CommandResult(true);
        }
    }
}
