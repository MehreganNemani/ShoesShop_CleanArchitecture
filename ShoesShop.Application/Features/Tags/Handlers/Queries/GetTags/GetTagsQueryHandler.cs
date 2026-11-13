using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Common.Models;
using ShoesShop.Application.DTOs.Tags;
using ShoesShop.Application.Features.Tags.Requests.Queries.GetTags;

namespace ShoesShop.Application.Features.Tags.Handlers.Queries.GetTags
{
	public sealed class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, PaginatedResult<TagDto>>
	{
		private const int DefaultPageSize = 10;

		private const int MaxPageSize = 100;

		private readonly ITagRepository _repository;
		public GetTagsQueryHandler(ITagRepository repository)
		{
			_repository = repository;
		}

		public async Task<PaginatedResult<TagDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
		{
			var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;

			var pageSize = request.PageSize <= 0 ? DefaultPageSize : Math.Min(request.PageSize, MaxPageSize);

			var result = await _repository.GetPagedAsync(request.Search, pageNumber, pageSize, cancellationToken);

			return new PaginatedResult<TagDto>
			{
				PageNumber = result.PageNumber,
				PageSize = result.PageSize,
				TotalCount = result.TotalCount,
				Items = result.Items.Select(t => new TagDto
				{
					ID = t.ID,
					TagTitle = t.TagTitle,
					LastUpdateDate = t.LastUpdateDate,
					CreateDate = t.CreateDate
				}).ToList()
			};
		}
	}
}
