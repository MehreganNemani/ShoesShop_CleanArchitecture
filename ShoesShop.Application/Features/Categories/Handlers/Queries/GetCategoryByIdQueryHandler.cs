using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.DTOs.Categories;
using ShoesShop.Application.Features.Categories.Requests.Queries.GetCategoryById;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.Categories.Handlers.Queries
{
	public sealed class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto?>
	{
		private readonly IRepository<Category> _repository;
		public GetCategoryByIdQueryHandler(IRepository<Category> repository)
		{
			_repository = repository;
		}

		public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
		{
			return await _repository.Query().AsNoTracking().Where(x => x.ID == request.id && !x.IsDeleted).Select(x => new CategoryDto
			{
				ID = x.ID,
				CategoryName = x.CategoryName,
				Code = x.Code,
				IsDeleted = x.IsDeleted,
				CreateDate = x.CreateDate,
				ParentID = x.ParentID,
				ParentName = x.Parent != null ? x.Parent.CategoryName : null
			}).FirstOrDefaultAsync(cancellationToken);
		}
	}
}
