using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Galleries.Requests.Commands;

namespace ShoesShop.Application.Features.Galleries.Handlers.Commands.SetMainGalleryImage
{
	public sealed class SetMainGalleryImageCommandHandler : IRequestHandler<SetMainGalleryImageCommand, bool>
	{
		private readonly IGalleryRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		public SetMainGalleryImageCommandHandler(IGalleryRepository repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> Handle(SetMainGalleryImageCommand request, CancellationToken cancellationToken)
		{
			var selected = await _repository.GetByIdAsync(request.Id, cancellationToken);

			if (selected is null)
			{
				return false;
			}

			var images = await _repository.GetTrackByProductIdAsync(selected.ProductID, cancellationToken);

			if (images.All(g => g.ID != selected.ID))
			{
				return false;
			}

			if (selected.IsMain)
			{
				return true;
			}

			foreach (var image in images)
			{
				image.IsMain = false;
			}

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			selected.IsMain = true;

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
