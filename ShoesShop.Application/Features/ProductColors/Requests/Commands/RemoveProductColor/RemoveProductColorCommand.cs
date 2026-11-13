using MediatR;

namespace ShoesShop.Application.Features.ProductColors.Requests.Commands.RemoveProductColor
{
	public sealed class RemoveProductColorCommand : IRequest<bool>
	{
		public int ProductID { get; set; }
		public int ColorID { get; set; }
	}
}
