using MediatR;

namespace ShoesShop.Application.Features.Tags.Requests.Commands
{
	public sealed record CreateTagCommand(string TagTitle) : IRequest<int>;

}
