using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Common.Models;
using ShoesShop.Application.DTOs.Products;
using ShoesShop.Application.Features.Products.Requests.Queries.GetProducts;

namespace ShoesShop.Application.Features.Products.Handlers.Queries.GetProducts
{
	public sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PaginatedResult<ProductDto>>
	{
		private const int DefaultPageSize = 10;

		private const int MaxPageSize = 100;

		private readonly IProductRepository _repository;
		public GetProductsQueryHandler(IProductRepository repository)
		{
			_repository = repository;
		}
		public async Task<PaginatedResult<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
		{
			var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;

			var pageSize = request.PageSize <= 0 ? DefaultPageSize : Math.Min(request.PageSize, MaxPageSize);

			var result = await _repository.GetPagedAsync(request.Search, pageNumber, pageSize, cancellationToken);

			return new PaginatedResult<ProductDto>
			{
				PageNumber = result.PageNumber,
				PageSize = result.PageSize,
				TotalCount = result.TotalCount,
				Items = result.Items.Select(p => new ProductDto
				{
					ID = p.ID,
					ProductName = p.ProductName,
					ProductModel = p.ProductModel,
					ProductCode = p.ProductCode,
					ProductPrice = p.ProductPrice,
					CreateDate = p.CreateDate,
					IsAvailable = p.IsAvailable
				}).ToList()
			};
		}
	}
}
