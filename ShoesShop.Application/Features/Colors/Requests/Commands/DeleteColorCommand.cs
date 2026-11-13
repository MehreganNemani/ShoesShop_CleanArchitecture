using MediatR;

namespace ShoesShop.Application.Features.Colors.Requests.Commands
{
    public sealed record DeleteColorCommand(int Id) : IRequest<bool>;
}
