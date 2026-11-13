using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Categories.Requests.Commands;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.Categories.Handlers.Commands.UpdateCategories
{
	public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
	{
		private readonly IRepository<Category> _categoryRepository;
		private readonly IUnitOfWork _unitOfWork;

		public UpdateCategoryCommandHandler(IRepository<Category> categoryRepository, IUnitOfWork unitOfWork)
		{
			_categoryRepository = categoryRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
		{
			var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

			if (category == null || category.IsDeleted)
			{
				throw new KeyNotFoundException("Your Category Not Found...");
			}

			category.CategoryName = request.categoryName.Trim();
			category.Code = request.code;
			category.ParentID = request.ParentId;
			category.LastUpdateDate = DateTime.Now;
			category.IsDeleted = false;

			_categoryRepository.Update(category);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
