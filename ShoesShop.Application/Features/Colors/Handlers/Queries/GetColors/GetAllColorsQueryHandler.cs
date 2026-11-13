using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.DTOs.Colors;
using ShoesShop.Application.Features.Colors.Requests.Queries.GetColors;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.Colors.Handlers.Queries.GetColors
{
	public sealed class GetAllColorsQueryHandler : IRequestHandler<GetAllColorsQuery, List<ColorDto>>
	{
		private readonly IRepository<Color> _repository;

		public GetAllColorsQueryHandler(IRepository<Color> repository)
		{
			_repository = repository;
		}

		public async Task<List<ColorDto>> Handle(GetAllColorsQuery request, CancellationToken cancellationToken)
		{
			return await _repository.Query().AsNoTracking().Where(c => !c.IsDeleted).Select(c => new ColorDto
			{
				ID = c.ID,
				ColorName = c.ColorName
			}).ToListAsync(cancellationToken);
		}
	}
}
