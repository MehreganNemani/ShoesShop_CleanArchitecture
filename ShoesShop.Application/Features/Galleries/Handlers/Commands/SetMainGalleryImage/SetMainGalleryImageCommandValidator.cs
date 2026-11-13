using FluentValidation;
using ShoesShop.Application.Features.Galleries.Requests.Commands;

namespace ShoesShop.Application.Features.Galleries.Handlers.Commands.SetMainGalleryImage
{
	public sealed class SetMainGalleryImageCommandValidator : AbstractValidator<SetMainGalleryImageCommand>
	{
		public SetMainGalleryImageCommandValidator()
		{
			RuleFor(g => g.Id).GreaterThan(0).WithMessage("Id Is Not Valid...");
		}
	}
}
