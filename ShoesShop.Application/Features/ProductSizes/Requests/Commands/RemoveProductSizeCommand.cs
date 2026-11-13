using MediatR;

namespace ShoesShop.Application.Features.ProductSizes.Requests.Commands
{
	public sealed class RemoveProductSizeCommand : IRequest<bool>
	{
		public int ProductID { get; set; }
		public int SizeID { get; set; }
	}
}
