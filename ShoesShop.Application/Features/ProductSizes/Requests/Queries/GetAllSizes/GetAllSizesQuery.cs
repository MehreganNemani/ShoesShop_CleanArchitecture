using MediatR;
using ShoesShop.Application.DTOs.Sizes;

namespace ShoesShop.Application.Features.ProductSizes.Requests.Queries.GetAllSizes
{
	public sealed record GetAllSizesQuery : IRequest<List<SizeDto>>;

}
