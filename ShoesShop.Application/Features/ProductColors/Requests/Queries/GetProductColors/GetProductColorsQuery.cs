using MediatR;
using ShoesShop.Application.DTOs.ProductColors;

namespace ShoesShop.Application.Features.ProductColors.Requests.Queries.GetProductColors
{
	public sealed class GetProductColorsQuery : IRequest<List<ProductColorDto>>
	{
		public int ProductID { get; set; }

		public GetProductColorsQuery(int productID) => ProductID = productID;
	}
}
