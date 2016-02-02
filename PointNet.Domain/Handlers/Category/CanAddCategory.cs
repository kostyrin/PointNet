using System.Collections.Generic;
using PointNet.CommandProcessor.Command;
using PointNet.Model.Commands;
using PointNet.Core.Common;
using PointNet.Data.Repositories;
using PointNet.Data.Infrastructure;
using PointNet.Model;

namespace PointNet.Domain.Handlers
{
    public class CanAddCategory : IValidationHandler<CreateOrUpdateCategoryCommand>
    {
        private readonly ICategoryRepository categoryRepository;

        public CanAddCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<ValidationResult> Validate(CreateOrUpdateCategoryCommand command)
        {
            Category isCategoryExists = null;
            if (command.CategoryId == 0)
                isCategoryExists = categoryRepository.Get(c => c.Name == command.Name);
            else
                isCategoryExists = categoryRepository.Get(c => c.Name == command.Name && c.CategoryId != command.CategoryId);

            if (isCategoryExists != null)
            {
                yield return new ValidationResult("Name", Resources.CategoryExists);
            }
        }
    }
}
