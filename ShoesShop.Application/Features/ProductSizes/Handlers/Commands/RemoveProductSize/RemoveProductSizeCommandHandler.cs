using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.ProductSizes.Requests.Commands;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductSizes.Handlers.Commands.RemoveProductSize
{
	public sealed class RemoveProductSizeCommandHandler : IRequestHandler<RemoveProductSizeCommand, bool>
	{
		private readonly IRepository<ProductSize> _repository;
		private readonly IUnitOfWork _unitOfWork;

		public RemoveProductSizeCommandHandler(IRepository<ProductSize> repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> Handle(RemoveProductSizeCommand request, CancellationToken cancellationToken)
		{
			var productSize = await _repository.Query().FirstOrDefaultAsync(ps => ps.ProductID == request.ProductID && ps.SizeID == request.SizeID, cancellationToken);

			if (productSize == null)
			{
				return false;
			}

			_repository.Delete(productSize);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
