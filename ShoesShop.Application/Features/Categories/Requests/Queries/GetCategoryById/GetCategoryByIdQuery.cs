using MediatR;
using ShoesShop.Application.DTOs.Categories;

namespace ShoesShop.Application.Features.Categories.Requests.Queries.GetCategoryById
{
	public sealed record GetCategoryByIdQuery(int id) : IRequest<CategoryDto?>;
}
