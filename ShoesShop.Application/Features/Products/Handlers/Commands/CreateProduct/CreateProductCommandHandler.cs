using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Products.Requests.Commands;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.Products.Handlers.Commands.CreateProduct
{
	public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
	{
		private readonly IProductRepository _repository;
		private readonly IUnitOfWork _unitOfWork;

		public CreateProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			var productName = request.ProductName.Trim();
			var productModel = request.ProductModel.Trim();
			var productCode = request.ProductCode.Trim();

			var product = new Product
			{
				ProductName = productName,
				ProductCode = productCode,
				ProductModel = productModel,
				ProductDesc = request.ProductDesc,
				ProductPrice = request.ProductPrice,
				ShowInSlider = request.ShowInSlider,
				IsAvailable = request.IsAvailable,
				CreateDate = DateTime.Now,
				IsDeleted = false
			};

			await _repository.AddAsync(product, cancellationToken);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return product.ID;
		}
	}
}
