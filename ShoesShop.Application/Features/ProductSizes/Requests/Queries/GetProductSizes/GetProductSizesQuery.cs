using MediatR;
using ShoesShop.Application.DTOs.ProductSizes;

namespace ShoesShop.Application.Features.ProductSizes.Requests.Queries.GetProductSizes
{
	public sealed class GetProductSizesQuery : IRequest<List<ProductSizeDto>>
	{
		public int ProductID { get; set; }

		public GetProductSizesQuery(int productID) => ProductID = productID;
	}
}
