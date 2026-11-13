using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.ProductColors.Requests.Commands.RemoveProductColor;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductColors.Handlers.Commands.RemoveProductColor
{
	public sealed class RemoveProductColorCommandHandler : IRequestHandler<RemoveProductColorCommand, bool>
	{
		private readonly IRepository<ProductColor> _repository;
		private readonly IUnitOfWork _unitOfWork;

		public RemoveProductColorCommandHandler(IRepository<ProductColor> repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> Handle(RemoveProductColorCommand request, CancellationToken cancellationToken)
		{
			var productColor = await _repository.Query().FirstOrDefaultAsync(pc => pc.ProductID == request.ProductID && pc.ColorID == request.ColorID, cancellationToken);

			if (productColor == null)
			{
				return false;
			}

			_repository.Delete(productColor);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
