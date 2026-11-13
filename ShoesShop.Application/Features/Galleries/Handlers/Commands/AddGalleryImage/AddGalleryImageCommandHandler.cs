using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Galleries.Requests.Commands;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.Galleries.Handlers.Commands.AddGalleryImage
{
	public sealed class AddGalleryImageCommandHandler : IRequestHandler<AddGalleryImageCommand, bool>
	{
		private readonly IGalleryRepository _galleryRepository;
		private readonly IImageStorageService _imageStorageService;
		private readonly IUnitOfWork _unitOfWork;

		public AddGalleryImageCommandHandler(IGalleryRepository galleryRepository, IImageStorageService imageStorageService, IUnitOfWork unitOfWork)
		{
			_galleryRepository = galleryRepository;
			_imageStorageService = imageStorageService;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> Handle(AddGalleryImageCommand request, CancellationToken cancellationToken)
		{
			var productExists = await _galleryRepository.ProductExistsAsync(request.productId, cancellationToken);

			if (!productExists)
			{
				throw new KeyNotFoundException("Product Not Found...");
			}

			var savedNames = new List<string>();

			try
			{
				var existingImages = await _galleryRepository.GetByProductIdAsync(request.productId, cancellationToken);

				var makeFirstImageMain = existingImages.Count == 0;

				foreach (var file in request.Images)
				{
					await using var stream = file.OpenReadStream();

					var imageName = await _imageStorageService.SaveAsync(stream, file.FileName, cancellationToken);

					savedNames.Add(imageName);

					var gallery = new Gallery()
					{
						ProductID = request.productId,
						ImageName = imageName,
						IsMain = makeFirstImageMain,
						CreateDate = DateTime.Now,
						IsDeleted = false
					};

					await _galleryRepository.AddAsync(gallery, cancellationToken);

					makeFirstImageMain = false;
				}

				await _unitOfWork.SaveChangesAsync(cancellationToken);

				return true;
			}
			catch
			{
				foreach (var imageName in savedNames)
				{
					try
					{
						await _imageStorageService.DeleteAsync(imageName, cancellationToken);
					}
					catch
					{

					}
				}

				throw;
			}
		}
	}
}
