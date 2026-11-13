using FluentValidation;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Tags.Requests.Commands;

namespace ShoesShop.Application.Features.Tags.Handlers.Commands.CreateTag
{
	public sealed class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
	{
		public CreateTagCommandValidator(ITagRepository tagRepository)
		{
			RuleFor(t => t.TagTitle).NotEmpty().WithMessage("Tag Title Is Required...")
					  .MaximumLength(50).WithMessage("Tag Title Must Be Less Than 50 Characters...")
					  .MustAsync(async (title, cancellationToken) =>
							   !await tagRepository.ExistsByNameAsync(title, null, cancellationToken)
					  ).WithMessage("Tag Title Already Exists...");
		}
	}
}
