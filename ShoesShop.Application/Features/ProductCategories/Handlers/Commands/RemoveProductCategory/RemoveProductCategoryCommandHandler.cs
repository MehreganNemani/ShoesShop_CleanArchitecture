using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.ProductCategories.Requests.Commands.RemoveProductCategory;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductCategories.Handlers.Commands.RemoveProductCategory
{
	public sealed class RemoveProductCategoryCommandHandler : IRequestHandler<RemoveProductCategoryCommand, bool>
	{
		private readonly IRepository<ProductCategory> _repository;
		private readonly IUnitOfWork _unitOfWork;

		public RemoveProductCategoryCommandHandler(IRepository<ProductCategory> repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}
		public async Task<bool> Handle(RemoveProductCategoryCommand request, CancellationToken cancellationToken)
		{
			var relation = await _repository.Query().FirstOrDefaultAsync(pc => pc.ProductID == request.ProductId && pc.CategoryID == request.CategoryId && !pc.IsDeleted, cancellationToken);

			if (relation == null)
			{
				return false;
			}

			_repository.Delete(relation);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
