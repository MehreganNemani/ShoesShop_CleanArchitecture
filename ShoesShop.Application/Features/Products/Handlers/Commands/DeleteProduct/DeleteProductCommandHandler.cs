using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Products.Requests.Commands;

namespace ShoesShop.Application.Features.Products.Handlers.Commands.DeleteProduct
{
	public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
	{
		private readonly IProductRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		public DeleteProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
		{
			var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

			if (product is null || product.IsDeleted)
			{
				return false;
			}

			product.IsDeleted = true;

			product.LastUpdateDate = DateTime.Now;

			_repository.Update(product);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
