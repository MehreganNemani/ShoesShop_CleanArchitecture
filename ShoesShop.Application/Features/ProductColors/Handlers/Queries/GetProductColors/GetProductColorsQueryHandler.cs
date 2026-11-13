using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.DTOs.ProductColors;
using ShoesShop.Application.Features.ProductColors.Requests.Queries.GetProductColors;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductColors.Handlers.Queries.GetProductColors
{
	public sealed class GetProductColorsQueryHandler : IRequestHandler<GetProductColorsQuery, List<ProductColorDto>>
	{
		private readonly IRepository<ProductColor> _repository;

		public GetProductColorsQueryHandler(IRepository<ProductColor> repository)
		{
			_repository = repository;
		}

		public async Task<List<ProductColorDto>> Handle(GetProductColorsQuery request, CancellationToken cancellationToken)
		{
			return await _repository.Query().AsNoTracking().Where(c => c.ProductID == request.ProductID && c.Color != null && !c.Color.IsDeleted).Select(pc => new ProductColorDto
			{
				ProductID = pc.ProductID,
				ColorID = pc.ColorID,
				ColorName = pc.Color!.ColorName
			}).ToListAsync(cancellationToken);
		}
	}
}
