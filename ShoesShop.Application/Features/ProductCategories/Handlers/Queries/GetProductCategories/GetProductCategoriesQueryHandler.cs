using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.DTOs.ProductCategories;
using ShoesShop.Application.Features.ProductCategories.Requests.Queries.GetProductCategories;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductCategories.Handlers.Queries.GetProductCategories
{
	public sealed class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, List<ProductCategoryDto>>
	{
		private readonly IRepository<ProductCategory> _repository;

		public GetProductCategoriesQueryHandler(IRepository<ProductCategory> repository)
		{
			_repository = repository;
		}

		public async Task<List<ProductCategoryDto>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
		{
			return await _repository.Query().AsNoTracking()
				.Where(pc => pc.ProductID == request.ProductId && !pc.IsDeleted && !pc.Category.IsDeleted)
				.Select(pc => new ProductCategoryDto
				{
					ProductId = pc.ProductID,
					CategoryId = pc.CategoryID,
					CategoryName = pc.Category.CategoryName
				})
				.OrderBy(pc => pc.CategoryName)
				.ToListAsync(cancellationToken);
		}
	}
}
