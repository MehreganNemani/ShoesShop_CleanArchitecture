using MediatR;

namespace ShoesShop.Application.Features.Colors.Requests.Commands
{
    public sealed record UpdateColorCommand(int Id, string ColorName) : IRequest<bool>;
}
