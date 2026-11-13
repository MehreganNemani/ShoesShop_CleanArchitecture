using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.DTOs.Tags;
using ShoesShop.Application.Features.Tags.Requests.Queries.GetAllTags;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.Tags.Handlers.Queries.GetAllTags
{
	public sealed class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, List<TagDto>>
	{
		private readonly IRepository<Tag> _repository;

		public GetAllTagsQueryHandler(IRepository<Tag> repository)
		{
			_repository = repository;
		}

		public async Task<List<TagDto>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
		{
			return await _repository.Query().AsNoTracking().Where(t => !t.IsDeleted).Select(t => new TagDto
			{
				ID = t.ID,
				TagTitle = t.TagTitle
			}).ToListAsync(cancellationToken);
		}
	}
}
