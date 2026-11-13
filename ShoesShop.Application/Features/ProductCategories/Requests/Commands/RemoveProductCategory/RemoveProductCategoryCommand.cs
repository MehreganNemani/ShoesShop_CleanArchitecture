using MediatR;

namespace ShoesShop.Application.Features.ProductCategories.Requests.Commands.RemoveProductCategory
{
	public sealed class RemoveProductCategoryCommand : IRequest<bool>
	{
		public int ProductId { get; set; }
		public int CategoryId { get; set; }
	}
}
