using MediatR;
using ShoesShop.Application.DTOs.Tags;

namespace ShoesShop.Application.Features.Tags.Requests.Queries.GetTagById
{
	public sealed record GetTagByIdQuery(int Id) : IRequest<TagDto?>;
}
