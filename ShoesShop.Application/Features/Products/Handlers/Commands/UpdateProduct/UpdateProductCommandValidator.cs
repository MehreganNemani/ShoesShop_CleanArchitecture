using FluentValidation;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Products.Requests.Commands;

namespace ShoesShop.Application.Features.Products.Handlers.Commands.UpdateProduct
{
	public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
	{
		public UpdateProductCommandValidator(IProductRepository productRepository)
		{
			RuleFor(p => p.Id).GreaterThan(0).WithMessage("Id Is Not Valid...");

			RuleFor(p => p.ProductName).NotEmpty().WithMessage("Product Name Is Required...")
				   .MaximumLength(100).WithMessage("Product Name Must Be Less Than 100 Characters...");

			RuleFor(p => p.ProductCode).NotEmpty().WithMessage("Product Code Is Required...")
				   .MaximumLength(6).WithMessage("Product Code Must Be Less Than 6 Characters...");

			RuleFor(p => p.ProductModel).NotEmpty().WithMessage("Product Model Is Required...")
				   .MaximumLength(8).WithMessage("Product Model Must Be Less Than 8 Characters...");

			RuleFor(p => p).CustomAsync(async (command, context, cancellationToken) =>
			{
				if (await productRepository.ExistsByNameAsync(command.ProductName, command.Id, cancellationToken))
				{
					context.AddFailure(nameof(command.ProductName), "Product Name Already Exists...");
				}

				if (await productRepository.ExistsByCodeAsync(command.ProductCode, command.Id, cancellationToken))
				{
					context.AddFailure(nameof(command.ProductCode), "Product Code Already Exists...");
				}

				if (await productRepository.ExistsByModelAsync(command.ProductModel, command.Id, cancellationToken))
				{
					context.AddFailure(nameof(command.ProductModel), "Product Model Already Exists...");
				}
			});
		}
	}
}
