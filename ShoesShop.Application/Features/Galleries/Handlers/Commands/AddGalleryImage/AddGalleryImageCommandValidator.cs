using FluentValidation;
using ShoesShop.Application.Features.Galleries.Requests.Commands;

namespace ShoesShop.Application.Features.Galleries.Handlers.Commands.AddGalleryImage
{
	public sealed class AddGalleryImageCommandValidator : AbstractValidator<AddGalleryImageCommand>
	{
		private static readonly string[] AllowedExtensions = { ".jpeg", ".jpg", ".png" };

		public AddGalleryImageCommandValidator()
		{
			RuleFor(g => g.productId).GreaterThan(0);

			RuleFor(g => g.Images).NotNull().Must(g => g.Count > 0).WithMessage("You Should Select a Photo...")
				.Must(g => g.Count <= 10).WithMessage("You Can Add 10 Photos Per Action...");

			RuleForEach(g => g.Images).Must(file => file is not null && file.Length > 0).WithMessage("File Is Empty...");

			RuleForEach(g => g.Images).Must(file => file is not null && file.Length <= 5 * 1024 * 1024).WithMessage("Photo Size Must Be Less Than 5 mb...");

			RuleForEach(g => g.Images).Must(file => file is not null && AllowedExtensions.Contains(Path.GetExtension(file.FileName), StringComparer.OrdinalIgnoreCase)).WithMessage("File Format Is Not Valid...");
		}
	}
}
