using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.DTOs.ProductTags;
using ShoesShop.Application.Features.ProductTags.Requests.Queries.GetProductTags;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductTags.Handlers.Queries.GetProductTags
{
	public sealed class GetProductTagsQueryHandler : IRequestHandler<GetProductTagsQuery, List<ProductTagDto>>
	{
		private readonly IRepository<ProductTag> _repository;

		public GetProductTagsQueryHandler(IRepository<ProductTag> repository)
		{
			_repository = repository;
		}

		public async Task<List<ProductTagDto>> Handle(GetProductTagsQuery request, CancellationToken cancellationToken)
		{
			return await _repository.Query().AsNoTracking().Where(t => t.ProductID == request.ProductID && t.Tag != null && !t.Tag.IsDeleted).Select(pt => new ProductTagDto
			{
				ProductID = pt.ProductID,
				TagID = pt.TagID,
				TagTitle = pt.Tag!.TagTitle
			}).ToListAsync(cancellationToken);
		}
	}
}
