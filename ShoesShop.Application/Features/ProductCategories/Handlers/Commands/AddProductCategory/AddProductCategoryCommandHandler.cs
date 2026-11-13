using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.ProductCategories.Requests.Commands.AddProductCategory;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductCategories.Handlers.Commands.AddProductCategory
{
	public sealed class AddProductCategoryCommandHandler : IRequestHandler<AddProductCategoryCommand, bool>
	{
		private readonly IRepository<Category> _categoryRepository;
		private readonly IRepository<Product> _productRepository;
		private readonly IRepository<ProductCategory> _productCategoryRepository;
		private readonly IUnitOfWork _unitOfWork;

		public AddProductCategoryCommandHandler(IRepository<Category> categoryRepository,
												IRepository<Product> productRepository,
												IRepository<ProductCategory> productCategoryRepository,
												IUnitOfWork unitOfWork)
		{
			_categoryRepository = categoryRepository;
			_productRepository = productRepository;
			_productCategoryRepository = productCategoryRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> Handle(AddProductCategoryCommand request, CancellationToken cancellationToken)
		{
			var product = await _productRepository.Query().AsNoTracking().FirstOrDefaultAsync(p => p.ID == request.ProductId && !p.IsDeleted, cancellationToken);

			if (product == null)
			{
				throw new Exception("Product Not Found...");
			}

			var parentCategory = await _categoryRepository.Query().AsNoTracking().FirstOrDefaultAsync(p => p.ID == request.ParentCategoryId && !p.IsDeleted, cancellationToken);

			if (parentCategory == null)
			{
				throw new Exception("Parent Id Not Found...");
			}

			var childCategory = await _categoryRepository.Query().AsNoTracking().FirstOrDefaultAsync(c => c.ID == request.ChildCategoryId && c.ParentID == request.ParentCategoryId && !c.IsDeleted, cancellationToken);

			if (childCategory == null)
			{
				throw new Exception("Child Id Is Not Belong To Parent Id...");
			}

			var parentExists = await _productCategoryRepository.Query().AnyAsync(pc => pc.ProductID == request.ProductId && pc.CategoryID == request.ParentCategoryId && !pc.IsDeleted, cancellationToken);

			var childExists = await _productCategoryRepository.Query().AnyAsync(pc => pc.ProductID == request.ProductId && pc.CategoryID == request.ChildCategoryId && !pc.IsDeleted, cancellationToken);

			if (parentExists || childExists)
			{
				throw new Exception("At Least One Of Categories Alreadey Attached For Product...");
			}

			var parentRelation = new ProductCategory
			{
				ProductID = request.ProductId,
				CategoryID = request.ParentCategoryId,
				CreateDate = DateTime.Now,
				IsDeleted = false
			};

			var childRelation = new ProductCategory
			{
				ProductID = request.ProductId,
				CategoryID = request.ChildCategoryId,
				CreateDate = DateTime.Now,
				IsDeleted = false
			};

			await _productCategoryRepository.AddAsync(parentRelation);
			await _productCategoryRepository.AddAsync(childRelation);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
