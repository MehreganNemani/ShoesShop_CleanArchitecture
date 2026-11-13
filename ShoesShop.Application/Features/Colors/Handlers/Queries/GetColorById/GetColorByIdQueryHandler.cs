using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.DTOs.Colors;
using ShoesShop.Application.Features.Colors.Requests.Queries.GetColorById;

namespace ShoesShop.Application.Features.Colors.Handlers.Queries.GetColorById
{
	public sealed class GetColorByIdQueryHandler : IRequestHandler<GetColorByIdQuery, ColorDto?>
	{

		private readonly IColorRepository _repository;
		public GetColorByIdQueryHandler(IColorRepository repository)
		{
			_repository = repository;
		}

		public async Task<ColorDto?> Handle(GetColorByIdQuery request, CancellationToken cancellationToken)
		{
			var color = await _repository.GetByIdAsync(request.Id , cancellationToken);

			if (color == null)
			{
				return null;
			}

			return new ColorDto()
			{
				ID = color.ID,
				ColorName = color.ColorName,
				CreateDate = color.CreateDate,
				LastUpdateDate = color.LastUpdateDate
			};
		}
	}
}
