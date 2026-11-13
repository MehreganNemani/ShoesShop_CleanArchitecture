using MediatR;

namespace ShoesShop.Application.Features.ProductTags.Requests.Commands
{
	public sealed class AddProductTagCommand : IRequest<bool>
	{
		public int ProductId { get; set; }

		public int TagId { get; set; }
	}
}
