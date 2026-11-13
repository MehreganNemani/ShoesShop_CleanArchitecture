using FluentValidation;
using ShoesShop.Application.Features.ProductCategories.Requests.Commands.AddProductCategory;

namespace ShoesShop.Application.Features.ProductCategories.Handlers.Commands.AddProductCategory
{
	public sealed class AddProductCategoryCommandValidator : AbstractValidator<AddProductCategoryCommand>
	{
		public AddProductCategoryCommandValidator()
		{
			RuleFor(c => c.ProductId).GreaterThan(0).WithMessage("Product Id Is Not Valid...");

			RuleFor(c => c.ParentCategoryId).GreaterThan(0).WithMessage("Parent Id Is Not Valid...");

			RuleFor(c => c.ChildCategoryId).GreaterThan(0).WithMessage("Child Id Is Not Valid...");

			RuleFor(c => c.ChildCategoryId).NotEqual(c => c.ParentCategoryId).WithMessage("Parent Id And Child Id Can Not Be Equal...");
		}
	}
}
