using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.DTOs.Tags;
using ShoesShop.Application.Features.Tags.Requests.Queries.GetTagById;

namespace ShoesShop.Application.Features.Tags.Handlers.Queries.GetTagById
{
	public sealed class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, TagDto?>
	{
		private readonly ITagRepository _repository;
		public GetTagByIdQueryHandler(ITagRepository repository)
		{
			_repository = repository;
		}

		public async Task<TagDto?> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
		{
			var tag = await _repository.GetByIdAsync(request.Id);

			if (tag == null)
			{
				return null;
			}

			return new TagDto()
			{
				ID = tag.ID,
				TagTitle = tag.TagTitle,
				CreateDate = tag.CreateDate,
				LastUpdateDate = tag.LastUpdateDate
			};
		}
	}
}
