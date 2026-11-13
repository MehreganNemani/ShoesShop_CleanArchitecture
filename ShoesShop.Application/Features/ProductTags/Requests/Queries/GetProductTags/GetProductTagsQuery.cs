using MediatR;
using ShoesShop.Application.DTOs.ProductTags;

namespace ShoesShop.Application.Features.ProductTags.Requests.Queries.GetProductTags
{
	public sealed class GetProductTagsQuery : IRequest<List<ProductTagDto>>
	{
		public int ProductID { get; set; }

		public GetProductTagsQuery(int productID) => ProductID = productID;
	}
}
