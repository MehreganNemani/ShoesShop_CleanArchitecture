using MediatR;

namespace ShoesShop.Application.Features.Galleries.Requests.Commands
{
	public sealed record SetMainGalleryImageCommand : IRequest<bool>
	{
		public int Id { get; set; }
	}
}
