using MediatR;
using ShoesShop.Application.DTOs.Colors;

namespace ShoesShop.Application.Features.Colors.Requests.Queries.GetColors
{
	public sealed record GetAllColorsQuery : IRequest<List<ColorDto>>;
}
