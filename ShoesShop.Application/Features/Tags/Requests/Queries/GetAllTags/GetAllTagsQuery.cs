using MediatR;
using ShoesShop.Application.DTOs.Tags;

namespace ShoesShop.Application.Features.Tags.Requests.Queries.GetAllTags
{
	public sealed record GetAllTagsQuery : IRequest<List<TagDto>>;
}
