using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.ProductSizes.Requests.Commands;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductSizes.Handlers.Commands.AddProductSize
{
	public sealed class AddProductSizeCommandHandler : IRequestHandler<AddProductSizeCommand, bool>
	{
		private readonly IRepository<ProductSize> _productSize;
		private readonly IRepository<Product> _product;
		private readonly IRepository<Size> _size;
		private readonly IUnitOfWork _unitOfWork;

		public AddProductSizeCommandHandler(IRepository<ProductSize> productSize, IRepository<Product> product, IRepository<Size> size, IUnitOfWork unitOfWork)
		{
			_productSize = productSize;
			_product = product;
			_size = size;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> Handle(AddProductSizeCommand request, CancellationToken cancellationToken)
		{
			var productExists = await _product.Query().AnyAsync(p => p.ID == request.ProductId, cancellationToken);

			if (!productExists)
			{
				throw new InvalidOperationException("Product Does Not Exists...");
			}

			var sizeExists = await _size.Query().AnyAsync(s => s.ID == request.SizeId, cancellationToken);

			if (!sizeExists)
			{
				throw new InvalidOperationException("Size Does Not Exists...");
			}

			var alreadyExists = await _productSize.Query().AnyAsync(ps => ps.ProductID == request.ProductId && ps.SizeID == request.SizeId, cancellationToken);

			if (alreadyExists)
			{
				throw new InvalidOperationException("This Size Already Added To This Product...");
			}

			var productSize = new ProductSize
			{
				ProductID = request.ProductId,
				SizeID = request.SizeId,
				CreateDate = DateTime.Now,
				IsDeleted = false
			};

			await _productSize.AddAsync(productSize);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
