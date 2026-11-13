using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.DTOs.ProductSizes;
using ShoesShop.Application.Features.ProductSizes.Requests.Queries.GetProductSizes;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductSizes.Handlers.Queries.GetProductSizes
{
	public sealed class GetProductSizesQueryHandler : IRequestHandler<GetProductSizesQuery, List<ProductSizeDto>>
	{
		private readonly IRepository<ProductSize> _repository;

		public GetProductSizesQueryHandler(IRepository<ProductSize> repository)
		{
			_repository = repository;
		}

		public async Task<List<ProductSizeDto>> Handle(GetProductSizesQuery request, CancellationToken cancellationToken)
		{
			return await _repository.Query().AsNoTracking().Where(s => s.ProductID == request.ProductID && s.Size != null && !s.Size.IsDeleted).Select(ps => new ProductSizeDto
			{
				ProductID = ps.ProductID,
				SizeID = ps.SizeID,
				SizeNumber = ps.Size!.SizeNumber
			}).ToListAsync(cancellationToken);
		}
	}
}
