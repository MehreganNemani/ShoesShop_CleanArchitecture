using FluentValidation;
using ShoesShop.Application.Features.ProductColors.Requests.Commands.RemoveProductColor;

namespace ShoesShop.Application.Features.ProductColors.Handlers.Commands.RemoveProductColor
{
	public sealed class RemoveProductColorCommandValidator : AbstractValidator<RemoveProductColorCommand>
	{
		public RemoveProductColorCommandValidator()
		{
			RuleFor(pc => pc.ProductID).GreaterThan(0).WithMessage("Product Id Is Not Valid...");

			RuleFor(pc => pc.ColorID).GreaterThan(0).WithMessage("Color Id Is Not Valid...");
		}
	}
}
