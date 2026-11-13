using FluentValidation;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Colors.Requests.Commands;

namespace ShoesShop.Application.Features.Colors.Handlers.Commands.UpdateColor
{
	public sealed class UpdateColorCommandValidator : AbstractValidator<UpdateColorCommand>
	{
		public UpdateColorCommandValidator(IColorRepository colorRepository)
		{
			RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id Is Not Valid...");

			RuleFor(c => c.ColorName).NotEmpty().WithMessage("Color Name Is Required...")
				   .MaximumLength(50).WithMessage("Color Name Must Be Less Than 50 Characters...");

			RuleFor(c => c).CustomAsync(async (command, context, cancellationToken) =>
			{
				if (await colorRepository.ExistsByNameAsync(command.ColorName, command.Id, cancellationToken))
				{
					context.AddFailure(nameof(command.ColorName), "Color Name Already Exists...");
				}
			});
		}
	}
}
