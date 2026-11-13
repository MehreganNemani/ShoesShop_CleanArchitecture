using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.DTOs.Sizes;
using ShoesShop.Application.Features.ProductSizes.Requests.Queries.GetAllSizes;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductSizes.Handlers.Queries.GetAllSizes
{
	public sealed class GetAllSizesQueryHandler : IRequestHandler<GetAllSizesQuery, List<SizeDto>>
	{
		private readonly IRepository<Size> _repository;

		public GetAllSizesQueryHandler(IRepository<Size> repository)
		{
			_repository = repository;
		}

		public async Task<List<SizeDto>> Handle(GetAllSizesQuery request, CancellationToken cancellationToken)
		{
			return await _repository.Query().AsNoTracking().Where(s => !s.IsDeleted).Select(s => new SizeDto
			{
				Id = s.ID,
				SizeNumber = s.SizeNumber
			}).ToListAsync(cancellationToken);
		}
	}
}
