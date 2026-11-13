using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Galleries.Requests.Commands;

namespace ShoesShop.Application.Features.Galleries.Handlers.Commands.DeleteGalleryImage
{
	public sealed class DeleteGalleryImageCommandHandler : IRequestHandler<DeleteGalleryImageCommand, bool>
	{
		private readonly IGalleryRepository _galleryRepository;
		private readonly IImageStorageService _imageStorageService;
		private readonly IUnitOfWork _unitOfWork;

		public DeleteGalleryImageCommandHandler(IGalleryRepository galleryRepository,
												IImageStorageService imageStorageService,
												IUnitOfWork unitOfWork)
		{
			_galleryRepository = galleryRepository;
			_imageStorageService = imageStorageService;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> Handle(DeleteGalleryImageCommand request, CancellationToken cancellationToken)
		{
			var gallery = await _galleryRepository.GetByIdAsync(request.Id, cancellationToken);

			if (gallery is null)
			{
				return false;
			}

			var imageName = gallery.ImageName;
			var productId = gallery.ProductID;
			var wasMain = gallery.IsMain;

			await _galleryRepository.ExecuteInTransactionAsync(async ct => {

				if (wasMain)
				{
					gallery.IsMain = false;

					await _unitOfWork.SaveChangesAsync(ct);
				}

				_galleryRepository.Remove(gallery);

				if (wasMain)
				{
					var remainingImages = await _galleryRepository.GetTrackByProductIdAsync(productId, ct);

					var replacement = remainingImages.Where(g => g.ID != gallery.ID).OrderBy(g => g.ID).FirstOrDefault();

					if (replacement is not null)
					{
						replacement.IsMain = true;
					}
				}

				await _unitOfWork.SaveChangesAsync(ct);
			},cancellationToken);

			await _imageStorageService.DeleteAsync(imageName, cancellationToken);

			return true;
		}
	}
}
