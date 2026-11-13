using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.ProductTags.Requests.Commands;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductTags.Handlers.Commands.RemoveProductTag
{
	public sealed class RemoveProductTagCommandHandler : IRequestHandler<RemoveProductTagCommand, bool>
	{
		private readonly IRepository<ProductTag> _repository;
		private readonly IUnitOfWork _unitOfWork;

		public RemoveProductTagCommandHandler(IRepository<ProductTag> repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> Handle(RemoveProductTagCommand request, CancellationToken cancellationToken)
		{
			var productTag = await _repository.Query().FirstOrDefaultAsync(pt => pt.ProductID == request.ProductId && pt.TagID == request.TagId, cancellationToken);

			if (productTag == null)
			{
				return false;
			}

			_repository.Delete(productTag);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
