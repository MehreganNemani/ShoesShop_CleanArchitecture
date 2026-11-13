using MediatR;
using ShoesShop.Application.DTOs.Colors;

namespace ShoesShop.Application.Features.Colors.Requests.Queries.GetColorById
{
	public sealed record GetColorByIdQuery(int Id) : IRequest<ColorDto?>;

}
