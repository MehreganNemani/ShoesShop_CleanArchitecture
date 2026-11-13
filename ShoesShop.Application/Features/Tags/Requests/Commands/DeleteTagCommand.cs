using MediatR;

namespace ShoesShop.Application.Features.Tags.Requests.Commands
{
	public sealed record DeleteTagCommand(int Id) : IRequest<bool>;
}
