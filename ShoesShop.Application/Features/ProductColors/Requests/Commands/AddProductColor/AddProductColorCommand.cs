using MediatR;

namespace ShoesShop.Application.Features.ProductColors.Requests.Commands.AddProductColor
{
	public sealed class AddProductColorCommand : IRequest<bool>
	{
		public int ProductId { get; set; }

		public int ColorId { get; set; }
	}
}
