using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.ProductCategories.Requests.Queries.GetParentCategories;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductCategories.Handlers.Queries.GetParentCategories
{
	public sealed class GetParentCategoriesQueryHandler : IRequestHandler<GetParentCategoriesQuery, List<Category>>
	{
		private readonly IRepository<Category> _repository;

		public GetParentCategoriesQueryHandler(IRepository<Category> repository)
		{
			_repository = repository;
		}

		public async Task<List<Category>> Handle(GetParentCategoriesQuery request, CancellationToken cancellationToken)
		{
			return await _repository.Query().AsNoTracking().Where(c => c.ParentID == null && !c.IsDeleted).OrderBy(c => c.CategoryName).ToListAsync(cancellationToken);
		}
	}
}
