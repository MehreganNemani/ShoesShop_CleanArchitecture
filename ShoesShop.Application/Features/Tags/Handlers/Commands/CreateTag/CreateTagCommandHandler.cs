using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Tags.Requests.Commands;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.Tags.Handlers.Commands.CreateTag
{
	public sealed class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
	{
		private readonly ITagRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		public CreateTagCommandHandler(ITagRepository repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}
		public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
		{
			var tagTitle = request.TagTitle.Trim();

			var tag = new Tag
			{
				TagTitle = request.TagTitle,
				CreateDate = DateTime.Now,
				IsDeleted = false
			};

			await _repository.AddAsync(tag, cancellationToken);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return tag.ID;
		}
	}
}
