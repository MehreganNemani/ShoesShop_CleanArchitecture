using MediatR;

namespace ShoesShop.Application.Features.Categories.Requests.Commands
{
	public sealed record CreateCategoryCommand(string categoryName, int code, int? ParentId) : IRequest<int>;

}
