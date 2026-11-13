using FluentValidation;
using ShoesShop.Application.Features.Galleries.Requests.Commands;

namespace ShoesShop.Application.Features.Galleries.Handlers.Commands.DeleteGalleryImage
{
	public sealed class DeleteGalleryImageCommandValidator : AbstractValidator<DeleteGalleryImageCommand>
	{
		public DeleteGalleryImageCommandValidator()
		{
			RuleFor(g => g.Id).GreaterThan(0).WithMessage("Id Is Not Valid...");
		}
	}
}
