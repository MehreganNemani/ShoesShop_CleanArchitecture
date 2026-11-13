using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.DTOs.Products;
using ShoesShop.Application.Features.Products.Requests.Queries.GetProductById;

namespace ShoesShop.Application.Features.Products.Handlers.Queries.GetProductById
{
	public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
	{
		private readonly IProductRepository _repository;
		public GetProductByIdQueryHandler(IProductRepository repository)
		{
			_repository = repository;
		}

		public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
		{
			var product = await _repository.GetByIdAsync(request.Id);

			if (product == null)
			{
				return null;
			}

			return new ProductDto()
			{
				ID = product.ID,
				ProductName = product.ProductName,
				ProductCode = product.ProductCode,
				ProductModel = product.ProductModel,
				ProductDesc = product.ProductDesc,
				ProductPrice = product.ProductPrice,
				ShowInSlider = product.ShowInSlider,
				IsAvailable = product.IsAvailable
			};
		}
	}
}
