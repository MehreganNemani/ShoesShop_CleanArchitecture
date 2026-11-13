using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Categories.Requests.Commands;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.Categories.Handlers.Commands.DeleteCategories
{
	public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
	{
		private readonly IRepository<Category> _categoryRepository;
		private readonly IUnitOfWork _unitOfWork;
		public DeleteCategoryCommandHandler(IRepository<Category> categoryRepository, IUnitOfWork unitOfWork)
		{
			_categoryRepository = categoryRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
		{
			var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

			if (category == null || category.IsDeleted)
			{
				throw new KeyNotFoundException("Your Category Not Found...");
			}

			category.IsDeleted = true;
			category.LastUpdateDate = DateTime.Now;

			_categoryRepository.Update(category);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
