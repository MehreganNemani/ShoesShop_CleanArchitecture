using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.DTOs.Categories;
using ShoesShop.Application.Features.Categories.Requests.Queries.GetCategoryOptions;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.Categories.Handlers.Queries
{
	public sealed class GetCategoryOptionsQueryHandler : IRequestHandler<GetCategoryOptionsQuery, List<CategoryOptionDto>>
	{
		private readonly IRepository<Category> _repository;
		public GetCategoryOptionsQueryHandler(IRepository<Category> repository)
		{
			_repository = repository;
		}

		public async Task<List<CategoryOptionDto>> Handle(GetCategoryOptionsQuery request, CancellationToken cancellationToken)
		{
			return await _repository.Query().AsNoTracking().Where(c => !c.IsDeleted)
				.OrderBy(c => c.CategoryName).Select(c => new CategoryOptionDto
				{
					Id = c.ID,
					CategoryName = c.CategoryName
				}).ToListAsync(cancellationToken);

			/*

			if (!request.ExcludeCategoryId.HasValue)
			{
				return categories;
			}

			var excludedIds = new HashSet<int>
			{
				request.ExcludeCategoryId.Value
			};

			bool changed = true;

			while (changed)
			{
				changed = false;

				foreach (var category in categories)
				{
					if (category.ParentID.HasValue && excludedIds.Contains(category.ParentID.Value))
					{
						if (excludedIds.Add(category.ID))
						{
							changed = true;
						}
					}
				}
			}

			

			return categories.Where(c => !excludedIds.Contains(c.ID))
				.ToList();

			*/
		}
	}
}
