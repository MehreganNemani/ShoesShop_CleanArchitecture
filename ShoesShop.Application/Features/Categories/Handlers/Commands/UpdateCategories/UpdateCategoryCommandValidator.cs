using FluentValidation;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Categories.Requests.Commands;

namespace ShoesShop.Application.Features.Categories.Handlers.Commands.UpdateCategories
{
	public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
	{
		private readonly ICategoryRepository _categoryRepository;

		public UpdateCategoryCommandValidator(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;

			RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id Is Not Valid...");

			RuleFor(c => c.categoryName).NotEmpty().WithMessage("Category Name Is Required...")
					  .MaximumLength(50).WithMessage("Category Name Must Be Less Than 50 Characters...");

			RuleFor(c => c).MustAsync(async (command, cancellationToken) => !await _categoryRepository.IsNameExistAsync(command.categoryName, command.Id, cancellationToken)).WithMessage("Category Name Already Exists...");

			RuleFor(c => c.code).NotEmpty().WithMessage("Category Code Is Required...");

			RuleFor(c => c).MustAsync(async (command, cancellationToken) => !await _categoryRepository.IsCodeExistAsync(command.code, command.Id, cancellationToken)).WithMessage("Category Code Already Exists...");

			RuleFor(c => c.ParentId).MustAsync(async (parentId, cancellationToken) =>
			{

				if (!parentId.HasValue)
				{
					return true;
				}

				return await _categoryRepository.ParentExistsAsync(parentId.Value, cancellationToken);
			}).WithMessage("ParentId Is Not Valid...");

			RuleFor(c => c).MustAsync(async (command, cancellationToken) =>
			{
				if (!command.ParentId.HasValue)
				{
					return true;
				}

				if (command.Id == command.ParentId.Value)
				{
					return false;
				}

				return !await _categoryRepository.IsChildOfAsync(command.Id, command.ParentId.Value, cancellationToken);
			}).WithMessage("You Can Not Select A Category That Is SelfParent...");
		}
	}
}
