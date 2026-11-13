using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Galleries.Requests.Queries.GetGalleryByProductId;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.Galleries.Handlers.Queries.GetGalleryByProductId
{
	public sealed class GetGalleryByProductIdQueryHandler : IRequestHandler<GetGalleryByProductIdQuery, List<Gallery>>
	{
		private readonly IGalleryRepository _galleryRepository;

		public GetGalleryByProductIdQueryHandler(IGalleryRepository galleryRepository)
		{
			_galleryRepository = galleryRepository;
		}

		public async Task<List<Gallery>> Handle(GetGalleryByProductIdQuery request, CancellationToken cancellationToken)
		{
			if (request.productId <= 0)
			{
				return new List<Gallery>();
			}

			return await _galleryRepository.GetByProductIdAsync(request.productId, cancellationToken);
		}
	}
}
