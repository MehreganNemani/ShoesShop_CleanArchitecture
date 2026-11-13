using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Tags.Requests.Commands;

namespace ShoesShop.Application.Features.Tags.Handlers.Commands.DeleteTag
{
	public sealed class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, bool>
	{
		private readonly ITagRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		public DeleteTagCommandHandler(ITagRepository repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
		{
			var tag = await _repository.GetByIdAsync(request.Id, cancellationToken);

			if (tag is null || tag.IsDeleted)
			{
				return false;
			}

			tag.IsDeleted = true;

			tag.LastUpdateDate = DateTime.Now;

			_repository.Update(tag);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
