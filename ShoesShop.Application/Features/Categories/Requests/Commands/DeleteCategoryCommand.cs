using MediatR;

namespace ShoesShop.Application.Features.Categories.Requests.Commands
{
	public sealed record DeleteCategoryCommand(int Id) : IRequest<Unit>;
}
