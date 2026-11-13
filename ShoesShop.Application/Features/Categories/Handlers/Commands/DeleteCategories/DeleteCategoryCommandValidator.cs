using FluentValidation;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Categories.Requests.Commands;

namespace ShoesShop.Application.Features.Categories.Handlers.Commands.DeleteCategories
{
	public sealed class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
	{
		private readonly ICategoryRepository _categoryRepository;
		public DeleteCategoryCommandValidator(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;

			RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id Is Not Valid...");

			RuleFor(x => x.Id).MustAsync(async (id, cancellationToken) => await _categoryRepository.IsExistAsync(id, cancellationToken)).WithMessage("Category Id Is Not Exists...");

			RuleFor(x => x.Id).MustAsync(async (id, cancellationToken) => !await _categoryRepository.HasChilderAsync(id, cancellationToken)).WithMessage("This Category Is A Parent , You Can Not Delete It...");
		}
	}
}
