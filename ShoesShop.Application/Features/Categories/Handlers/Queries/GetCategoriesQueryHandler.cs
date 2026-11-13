using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.DTOs.Categories;
using ShoesShop.Application.Features.Categories.Requests.Queries.GetAllCategories;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.Categories.Handlers.Queries
{
	public sealed class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
	{
		private readonly IRepository<Category> _repository;

		public GetCategoriesQueryHandler(IRepository<Category> repository)
		{
			_repository = repository;
		}

		public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
		{
			var query = _repository.Query().AsNoTracking().Where(c => !c.IsDeleted);

			if (!string.IsNullOrWhiteSpace(request.search))
			{
				var search = request.search.Trim();

				query = query.Where(c => c.CategoryName.Contains(search));
			}

			var result = await query.Include(c => c.Parent).Select(c => new CategoryDto
			{
				ID = c.ID,
				CategoryName = c.CategoryName,
				Code = c.Code,
				ParentID = c.ParentID,
				ParentName = c.Parent != null ? c.Parent.CategoryName : null,
				CreateDate = c.CreateDate,
				IsDeleted = c.IsDeleted,
				LastUpdateDate = c.LastUpdateDate
			}).ToListAsync(cancellationToken);

			return result;
		}
	}
}
