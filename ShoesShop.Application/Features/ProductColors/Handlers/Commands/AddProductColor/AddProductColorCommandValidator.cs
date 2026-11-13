using FluentValidation;
using ShoesShop.Application.Features.ProductColors.Requests.Commands.AddProductColor;

namespace ShoesShop.Application.Features.ProductColors.Handlers.Commands.AddProductColor
{
	public sealed class AddProductColorCommandValidator : AbstractValidator<AddProductColorCommand>
	{
		public AddProductColorCommandValidator()
		{
			RuleFor(pc => pc.ProductId).GreaterThan(0).WithMessage("Product Id Is Not Valid...");

			RuleFor(pc => pc.ColorId).GreaterThan(0).WithMessage("Color Id Is Not Valid...");
		}
	}
}
