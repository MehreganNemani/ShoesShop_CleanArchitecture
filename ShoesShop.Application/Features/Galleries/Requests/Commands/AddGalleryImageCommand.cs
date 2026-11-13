using MediatR;
using Microsoft.AspNetCore.Http;

namespace ShoesShop.Application.Features.Galleries.Requests.Commands
{
	public sealed record AddGalleryImageCommand : IRequest<bool>
	{
		public int productId { get; set; }
		public List<IFormFile> Images { get; set; } = new();
	}
}
