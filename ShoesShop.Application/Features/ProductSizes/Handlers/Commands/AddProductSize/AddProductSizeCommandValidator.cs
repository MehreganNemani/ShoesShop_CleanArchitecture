using FluentValidation;
using ShoesShop.Application.Features.ProductSizes.Requests.Commands;

namespace ShoesShop.Application.Features.ProductSizes.Handlers.Commands.AddProductSize
{
	public sealed class AddProductSizeCommandValidator : AbstractValidator<AddProductSizeCommand>
	{
		public AddProductSizeCommandValidator()
		{
			RuleFor(ps => ps.ProductId).GreaterThan(0).WithMessage("Product Id Is Not Valid...");

			RuleFor(ps => ps.SizeId).GreaterThan(0).WithMessage("Size Id Is Not Valid...");
		}
	}
}
