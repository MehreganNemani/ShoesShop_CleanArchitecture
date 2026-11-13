using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Products.Requests.Commands;

namespace ShoesShop.Application.Features.Products.Handlers.Commands.UpdateProduct
{
	public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
	{
		private readonly IProductRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		public UpdateProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

			if (product == null || product.IsDeleted)
			{
				return false;
			}

			product.ProductName = request.ProductName.Trim();
			product.ProductModel = request.ProductModel.Trim();
			product.ProductCode = request.ProductCode.Trim();
			product.ProductDesc = request.ProductDesc;
			product.ProductPrice = request.ProductPrice;
			product.IsAvailable = request.IsAvailable;
			product.ShowInSlider = request.ShowInSlider;
			product.LastUpdateDate = DateTime.Now;

			_repository.Update(product);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
