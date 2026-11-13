using FluentValidation;
using ShoesShop.Application.Features.ProductSizes.Requests.Commands;

namespace ShoesShop.Application.Features.ProductSizes.Handlers.Commands.RemoveProductSize
{
	public sealed class RemoveProductSizeCommandValidator : AbstractValidator<RemoveProductSizeCommand>
	{
		public RemoveProductSizeCommandValidator()
		{
			RuleFor(ps => ps.ProductID).GreaterThan(0).WithMessage("Product Id Is Not Valid...");

			RuleFor(ps => ps.SizeID).GreaterThan(0).WithMessage("Size Id Is Not Valid...");
		}
	}
}
