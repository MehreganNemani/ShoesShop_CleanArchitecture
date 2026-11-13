using MediatR;
using ShoesShop.Application.Common.Models;
using ShoesShop.Application.DTOs.Categories;

namespace ShoesShop.Application.Features.Categories.Requests.Queries.GetAllCategories
{
	public sealed record GetCategoriesQuery(string? search) : IRequest<List<CategoryDto>>;
}
