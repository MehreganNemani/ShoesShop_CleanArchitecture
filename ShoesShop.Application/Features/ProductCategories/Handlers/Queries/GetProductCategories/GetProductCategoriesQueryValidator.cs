using FluentValidation;
using ShoesShop.Application.Features.ProductCategories.Requests.Queries.GetProductCategories;

namespace ShoesShop.Application.Features.ProductCategories.Handlers.Queries.GetProductCategories
{
	public sealed class GetProductCategoriesQueryValidator : AbstractValidator<GetProductCategoriesQuery>
	{
		public GetProductCategoriesQueryValidator()
		{
			RuleFor(pc => pc.ProductId).GreaterThan(0).WithMessage("Product Id Is Not Valid...");
		}
	}
}
