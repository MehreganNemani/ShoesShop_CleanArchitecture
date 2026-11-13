using MediatR;

namespace ShoesShop.Application.Features.Colors.Requests.Commands
{
    public  sealed record CreateColorCommand(string ColorName) : IRequest<int>;
}
