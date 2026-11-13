using FluentValidation;
using ShoesShop.Application.Features.ProductTags.Requests.Commands;

namespace ShoesShop.Application.Features.ProductTags.Handlers.Commands.AddProductTag
{
	public sealed class AddProductTagCommandValidator : AbstractValidator<AddProductTagCommand>
	{
		public AddProductTagCommandValidator()
		{
			RuleFor(pt => pt.ProductId).GreaterThan(0).WithMessage("Product Id Is Not Valid...");

			RuleFor(pt => pt.TagId).GreaterThan(0).WithMessage("Tag Id Is Not Valid...");
		}
	}
}
