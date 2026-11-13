using MediatR;
using ShoesShop.Application.Common.Models;
using ShoesShop.Application.DTOs.Products;

namespace ShoesShop.Application.Features.Products.Requests.Queries.GetProducts
{
	public sealed record GetProductsQuery(string? Search,
	 int PageNumber = 1,
	 int PageSize = 10) : IRequest<PaginatedResult<ProductDto>>;
}
