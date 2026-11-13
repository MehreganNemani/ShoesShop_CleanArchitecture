using MediatR;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductCategories.Requests.Queries.GetChildCategories
{
	public sealed class GetChildCategoriesQuery : IRequest<List<Category>>
	{
		public int ParentId { get; set; }
	}
}
