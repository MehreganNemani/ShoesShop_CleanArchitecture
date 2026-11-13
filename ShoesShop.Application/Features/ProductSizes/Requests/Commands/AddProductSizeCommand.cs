using MediatR;

namespace ShoesShop.Application.Features.ProductSizes.Requests.Commands
{
	public sealed class AddProductSizeCommand : IRequest<bool>
	{
		public int ProductId { get; set; }

		public int SizeId { get; set; }
	}
}
