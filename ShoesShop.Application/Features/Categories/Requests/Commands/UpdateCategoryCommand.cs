using MediatR;

namespace ShoesShop.Application.Features.Categories.Requests.Commands
{
	public sealed record UpdateCategoryCommand(int Id, string categoryName, int? ParentId, int code) : IRequest<Unit>;
}
