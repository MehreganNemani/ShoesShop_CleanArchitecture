using FluentValidation;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Categories.Requests.Commands;

namespace ShoesShop.Application.Features.Categories.Handlers.Commands.CreateCategories
{
	public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
	{
		private readonly ICategoryRepository _categoryRepository;
		public CreateCategoryCommandValidator(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;

			RuleFor(c => c.categoryName).NotEmpty().WithMessage("Category Name Is Required...")
					  .MaximumLength(50).WithMessage("Category Name Must Be Less Than 50 Characters...")
			.MustAsync(async (name, cancellationToken) =>
					   !await _categoryRepository.IsNameExistAsync(name, null, cancellationToken)
			  ).WithMessage("Category Name Already Exists...");

			RuleFor(c => c.code).NotEmpty().WithMessage("Category Code Is Required...")
					  .MustAsync(async (code, cancellationToken) =>
							   !await _categoryRepository.IsCodeExistAsync(code, null, cancellationToken)
					  ).WithMessage("Category Code Already Exists...");

			RuleFor(c => c.ParentId).MustAsync(async (parentId, cancellationToken) =>
			{
				if (!parentId.HasValue)
				{
					return true;
				}

				return await _categoryRepository.ParentExistsAsync(parentId.Value, cancellationToken);
			}).WithMessage("ParentId Is Not Valid...");
		}
	}
}
