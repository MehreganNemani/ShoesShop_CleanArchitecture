using MediatR;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.Galleries.Requests.Queries.GetGalleryByProductId
{
	public sealed record GetGalleryByProductIdQuery(int productId) : IRequest<List<Gallery>>;
}
