using MediatR;

namespace ShoesShop.Application.Features.Galleries.Requests.Commands
{
	public sealed record DeleteGalleryImageCommand : IRequest<bool>
	{
		public int Id { get; set; }
	}
}
