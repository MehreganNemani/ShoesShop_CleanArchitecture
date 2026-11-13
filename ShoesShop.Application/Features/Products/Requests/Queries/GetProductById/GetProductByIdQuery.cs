using MediatR;
using ShoesShop.Application.DTOs.Products;

namespace ShoesShop.Application.Features.Products.Requests.Queries.GetProductById
{
	public sealed record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;
}
