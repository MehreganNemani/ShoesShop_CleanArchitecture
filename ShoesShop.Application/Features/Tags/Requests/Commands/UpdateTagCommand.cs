using MediatR;

namespace ShoesShop.Application.Features.Tags.Requests.Commands
{
	public sealed record UpdateTagCommand(int Id, string TagTitle) : IRequest<bool>;
}
