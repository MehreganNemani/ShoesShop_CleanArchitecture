using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Categories.Requests.Commands;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.Categories.Handlers.Commands.CreateCategories
{
	public sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
	{
		private readonly IRepository<Category> _categoryRepository;
		private readonly IUnitOfWork _unitOfWork;
		public CreateCategoryCommandHandler(IRepository<Category> categoryRepository, IUnitOfWork unitOfWork)
		{
			_categoryRepository = categoryRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
		{
			/*
			 
			if (request.ParentId.HasValue)
			{
				var parent = await _categoryRepository.GetByIdAsync(request.ParentId.Value, cancellationToken);

				if (parent == null || parent.IsDeleted)
				{
					throw new InvalidOperationException("Parent Id Is Not Valid...");
				}
			}
			
			*/

			var category = new Category
			{
				CategoryName = request.categoryName.Trim(),
				ParentID = request.ParentId,
				Code = request.code,
				CreateDate = DateTime.Now,
				IsDeleted = false
			};

			await _categoryRepository.AddAsync(category, cancellationToken);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return category.ID;
		}
	}
}
