using MediatR;

namespace ShoesShop.Application.Features.ProductCategories.Requests.Commands.AddProductCategory
{
	public sealed class AddProductCategoryCommand : IRequest<bool>
	{
		public int ProductId { get; set; }
		public int ParentCategoryId { get; set; }
		public int ChildCategoryId { get; set; }
	}
}
