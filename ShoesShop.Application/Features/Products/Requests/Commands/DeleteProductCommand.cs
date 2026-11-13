using MediatR;

namespace ShoesShop.Application.Features.Products.Requests.Commands
{
	public sealed record DeleteProductCommand(int Id) : IRequest<bool>;
}
