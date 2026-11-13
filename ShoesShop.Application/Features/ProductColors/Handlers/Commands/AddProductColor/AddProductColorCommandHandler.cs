using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.ProductColors.Requests.Commands.AddProductColor;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductColors.Handlers.Commands.AddProductColor
{
	public sealed class AddProductColorCommandHandler : IRequestHandler<AddProductColorCommand, bool>
	{
		private readonly IRepository<ProductColor> _productColor;
		private readonly IRepository<Product> _product;
		private readonly IRepository<Color> _color;
		private readonly IUnitOfWork _unitOfWork;
		public AddProductColorCommandHandler(IRepository<ProductColor> productColor, IRepository<Product> product, IRepository<Color> color, IUnitOfWork unitOfWork)
		{
			_productColor = productColor;
			_product = product;
			_color = color;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> Handle(AddProductColorCommand request, CancellationToken cancellationToken)
		{
			var productExists = await _product.Query().AnyAsync(p => p.ID == request.ProductId, cancellationToken);

			if (!productExists)
			{
				throw new InvalidOperationException("Product Does Not Exists...");
			}

			var colorExists = await _color.Query().AnyAsync(c => c.ID == request.ColorId, cancellationToken);

			if (!colorExists)
			{
				throw new InvalidOperationException("Color Does Not Exists...");
			}

			var alreadyExists = await _productColor.Query().AnyAsync(pc => pc.ProductID == request.ProductId && pc.ColorID == request.ColorId, cancellationToken);

			if (alreadyExists)
			{
				throw new InvalidOperationException("This Color Already Added To This Product...");
			}

			var productColor = new ProductColor
			{
				ProductID = request.ProductId,
				ColorID = request.ColorId,
				CreateDate = DateTime.Now,
				IsDeleted = false
			};

			await _productColor.AddAsync(productColor);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
