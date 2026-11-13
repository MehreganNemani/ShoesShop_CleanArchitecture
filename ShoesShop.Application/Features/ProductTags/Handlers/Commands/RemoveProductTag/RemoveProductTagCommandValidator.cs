using FluentValidation;
using ShoesShop.Application.Features.ProductTags.Requests.Commands;

namespace ShoesShop.Application.Features.ProductTags.Handlers.Commands.RemoveProductTag
{
	public sealed class RemoveProductTagCommandValidator : AbstractValidator<RemoveProductTagCommand>
	{
		public RemoveProductTagCommandValidator()
		{
			RuleFor(pt => pt.ProductId).GreaterThan(0).WithMessage("Product Id Is Not Valid...");

			RuleFor(pt => pt.TagId).GreaterThan(0).WithMessage("Tag Id Is Not Valid...");
		}
	}
}
