using MediatR;
using ShoesShop.Application.Common.Models;
using ShoesShop.Application.DTOs.Tags;

namespace ShoesShop.Application.Features.Tags.Requests.Queries.GetTags
{
	public sealed record GetTagsQuery(
	string? Search,
	 int PageNumber = 1,
	 int PageSize = 10) : IRequest<PaginatedResult<TagDto>>;
}
