using FluentValidation;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Colors.Requests.Commands;

namespace ShoesShop.Application.Features.Colors.Handlers.Commands.CreateColor
{
	public sealed class CreateColorCommandValidator : AbstractValidator<CreateColorCommand>
	{
		public CreateColorCommandValidator(IColorRepository colorRepository)
		{
			RuleFor(c => c.ColorName).NotEmpty().WithMessage("Color Name Is Required...")
					  .MaximumLength(50).WithMessage("Color Name Must Be Less Than 50 Characters...")
					  .MustAsync(async (name, cancellationToken) =>
							   !await colorRepository.ExistsByNameAsync(name, null, cancellationToken)
					  ).WithMessage("Color Name Already Exists...");
		}
	}
}
