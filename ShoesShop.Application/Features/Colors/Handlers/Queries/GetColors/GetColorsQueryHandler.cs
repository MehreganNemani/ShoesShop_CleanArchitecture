using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Common.Models;
using ShoesShop.Application.DTOs.Colors;
using ShoesShop.Application.Features.Colors.Requests.Queries.GetColors;

namespace ShoesShop.Application.Features.Colors.Handlers.Queries.GetColors
{
	public sealed class GetColorsQueryHandler : IRequestHandler<GetColorsQuery, PaginatedResult<ColorDto>>
	{
		private const int DefaultPageSize = 10;

		private const int MaxPageSize = 100;

		private readonly IColorRepository _repository;
		public GetColorsQueryHandler(IColorRepository repository)
		{
			_repository = repository;
		}

		public async Task<PaginatedResult<ColorDto>> Handle(GetColorsQuery request, CancellationToken cancellationToken)
		{
			var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;

			var pageSize = request.PageSize <= 0 ? DefaultPageSize : Math.Min(request.PageSize, MaxPageSize);

			var result = await _repository.GetPagedAsync(request.Search, pageNumber, pageSize, cancellationToken);

			return new PaginatedResult<ColorDto>
			{
				PageNumber = result.PageNumber,
				PageSize = result.PageSize,
				TotalCount = result.TotalCount,
				Items = result.Items.Select(c => new ColorDto
				{
					ID = c.ID,
					ColorName = c.ColorName,
					LastUpdateDate = c.LastUpdateDate,
					CreateDate = c.CreateDate
				}).ToList()
			};
		}
	}
}