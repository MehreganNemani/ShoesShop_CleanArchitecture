using FluentValidation;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Products.Requests.Commands;

namespace ShoesShop.Application.Features.Products.Handlers.Commands.CreateProduct
{
	public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
	{
		public CreateProductCommandValidator(IProductRepository productRepository)
		{
			RuleFor(p => p.ProductName).NotEmpty().WithMessage("Product Name Is Required...")
				  .MaximumLength(100).WithMessage("Product Name Must Be Less Than 100 Characters...")
					.MustAsync(async (name, cancellationToken) =>
						   !await productRepository.ExistsByNameAsync(name, null, cancellationToken)
				  ).WithMessage("Product Name Already Exists...");

			RuleFor(p => p.ProductCode).NotEmpty().WithMessage("Product Code Is Required...")
				  .MaximumLength(6).WithMessage("Product Code Must Be Less Than 6 Characters...")
					.MustAsync(async (code, cancellationToken) =>
						   !await productRepository.ExistsByCodeAsync(code, null, cancellationToken)
				  ).WithMessage("Product Code Already Exists...");

			RuleFor(p => p.ProductModel).NotEmpty().WithMessage("Product Model Is Required...")
				  .MaximumLength(8).WithMessage("Product Model Must Be Less Than 8 Characters...")
					.MustAsync(async (model, cancellationToken) =>
						   !await productRepository.ExistsByModelAsync(model, null, cancellationToken)
				  ).WithMessage("Product Model Already Exists...");
		}
	}
}
