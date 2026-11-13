using MediatR;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductCategories.Requests.Queries.GetParentCategories
{
	public sealed class GetParentCategoriesQuery : IRequest<List<Category>>
	{
	}
}
