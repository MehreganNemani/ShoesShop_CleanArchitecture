using FluentValidation;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Tags.Requests.Commands;

namespace ShoesShop.Application.Features.Tags.Handlers.Commands.UpdateTag
{
	public sealed class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
	{
		public UpdateTagCommandValidator(ITagRepository tagRepository)
		{
			RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id Is Not Valid...");

			RuleFor(t => t.TagTitle).NotEmpty().WithMessage("Tag Title Is Required...")
				   .MaximumLength(50).WithMessage("Tag Title Must Be Less Than 50 Characters...");

			RuleFor(t => t).CustomAsync(async (command, context, cancellationToken) =>
			{
				if (await tagRepository.ExistsByNameAsync(command.TagTitle, command.Id, cancellationToken))
				{
					context.AddFailure(nameof(command.TagTitle), "Tag Title Already Exists...");
				}
			});
		}
	}
}
