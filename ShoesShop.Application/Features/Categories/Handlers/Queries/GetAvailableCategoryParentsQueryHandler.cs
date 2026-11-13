using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.DTOs.Categories;
using ShoesShop.Application.Features.Categories.Requests.Queries.GetAvailableCategoryParents;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.Categories.Handlers.Queries
{
	public sealed class GetAvailableCategoryParentsQueryHandler : IRequestHandler<GetAvailableCategoryParentsQuery, IReadOnlyList<CategoryOptionDto>>
	{
		private readonly IRepository<Category> _repository;
		private readonly ICategoryRepository _categoryRepository;
		public GetAvailableCategoryParentsQueryHandler(IRepository<Category> repository, ICategoryRepository categoryRepository)
		{
			_repository = repository;
			_categoryRepository = categoryRepository;
		}

		public async Task<IReadOnlyList<CategoryOptionDto>> Handle(GetAvailableCategoryParentsQuery request, CancellationToken cancellationToken)
		{
			var descendantIds = await _categoryRepository.GetDescendantIdsAsync(request.categoryId, cancellationToken);

			var excludedIds = descendantIds.Append(request.categoryId).ToList();

			return await _repository.Query().AsNoTracking()
				.Where(c => !c.IsDeleted && !excludedIds.Contains(c.ID))
				.OrderBy(c => c.CategoryName)
				.Select(c => new CategoryOptionDto
				{
					Id = c.ID,
					CategoryName = c.CategoryName
				}).ToListAsync(cancellationToken);
		}
	}
}
