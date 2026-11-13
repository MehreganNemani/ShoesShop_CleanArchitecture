using FluentValidation;
using ShoesShop.Application.Features.ProductCategories.Requests.Commands.RemoveProductCategory;

namespace ShoesShop.Application.Features.ProductCategories.Handlers.Commands.RemoveProductCategory
{
	public sealed class RemoveProductCategoryCommandValidator : AbstractValidator<RemoveProductCategoryCommand>
	{
		public RemoveProductCategoryCommandValidator()
		{
			RuleFor(c => c.ProductId).GreaterThan(0).WithMessage("Product Id Is Not Valid...");

			RuleFor(c => c.CategoryId).GreaterThan(0).WithMessage("Category Id Is Not Valid...");
		}
	}
}
