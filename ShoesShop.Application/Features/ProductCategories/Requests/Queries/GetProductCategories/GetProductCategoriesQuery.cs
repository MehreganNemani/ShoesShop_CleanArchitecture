using MediatR;
using ShoesShop.Application.DTOs.ProductCategories;

namespace ShoesShop.Application.Features.ProductCategories.Requests.Queries.GetProductCategories
{
	public sealed class GetProductCategoriesQuery : IRequest<List<ProductCategoryDto>>
	{
		public int ProductId { get; set; }
	}
}
