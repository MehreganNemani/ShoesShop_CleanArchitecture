using MediatR;
using ShoesShop.Application.Common.Models;
using ShoesShop.Application.DTOs.Colors;

namespace ShoesShop.Application.Features.Colors.Requests.Queries.GetColors
{
    public sealed record GetColorsQuery(
    string? Search,
     int PageNumber = 1,
     int PageSize = 10) : IRequest<PaginatedResult<ColorDto>>;
}
