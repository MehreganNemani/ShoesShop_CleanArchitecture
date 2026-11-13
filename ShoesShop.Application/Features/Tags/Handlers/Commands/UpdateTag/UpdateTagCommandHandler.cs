using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Tags.Requests.Commands;

namespace ShoesShop.Application.Features.Tags.Handlers.Commands.UpdateTag
{
	public sealed class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, bool>
	{
		private readonly ITagRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		public UpdateTagCommandHandler(ITagRepository repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
		{
			var tag = await _repository.GetByIdAsync(request.Id, cancellationToken);

			if (tag == null || tag.IsDeleted)
			{
				return false;
			}

			tag.TagTitle = request.TagTitle.Trim();

			tag.LastUpdateDate = DateTime.Now;

			_repository.Update(tag);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
