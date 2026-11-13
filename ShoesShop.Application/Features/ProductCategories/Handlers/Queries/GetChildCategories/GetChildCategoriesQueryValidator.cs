using FluentValidation;
using ShoesShop.Application.Features.ProductCategories.Requests.Queries.GetChildCategories;

namespace ShoesShop.Application.Features.ProductCategories.Handlers.Queries.GetChildCategories
{
	public sealed class GetChildCategoriesQueryValidator : AbstractValidator<GetChildCategoriesQuery>
	{
		public GetChildCategoriesQueryValidator()
		{
			RuleFor(c => c.ParentId).GreaterThan(0).WithMessage("Parent Id Is Not Valid...");
		}
	}
}
