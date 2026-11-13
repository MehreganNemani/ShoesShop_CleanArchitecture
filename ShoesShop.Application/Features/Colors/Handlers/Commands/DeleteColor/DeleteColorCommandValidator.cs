using FluentValidation;
using ShoesShop.Application.Features.Colors.Requests.Commands;

namespace ShoesShop.Application.Features.Colors.Handlers.Commands.DeleteColor
{
	public class DeleteColorCommandValidator : AbstractValidator<DeleteColorCommand>
	{
		public DeleteColorCommandValidator()
		{
			RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id Is Not Valid...");
		}
	}
}
