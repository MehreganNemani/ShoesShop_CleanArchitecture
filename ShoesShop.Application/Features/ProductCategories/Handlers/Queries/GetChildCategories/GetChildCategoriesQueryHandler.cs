using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.ProductCategories.Requests.Queries.GetChildCategories;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductCategories.Handlers.Queries.GetChildCategories
{
	public sealed class GetChildCategoriesQueryHandler : IRequestHandler<GetChildCategoriesQuery, List<Category>>
	{
		private readonly IRepository<Category> _repository;

		public GetChildCategoriesQueryHandler(IRepository<Category> repository)
		{
			_repository = repository;
		}

		public async Task<List<Category>> Handle(GetChildCategoriesQuery request, CancellationToken cancellationToken)
		{
			return await _repository.Query().AsNoTracking().Where(c => c.ParentID == request.ParentId && !c.IsDeleted).OrderBy(c => c.CategoryName).ToListAsync(cancellationToken);
		}
	}
}
