using ShoesShop.Api.Contracts.Categories;
using System.ComponentModel.DataAnnotations;

namespace ShoesShop.MVC.Models.Categories
{
	public sealed class CreateCategoryVM
	{
        public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string CategoryName { get; set; } = string.Empty;
		public int? ParentId { get; set; }

		[Required]
		public int Code { get; set; }
		public IReadOnlyList<CategoryOptionDto> Categories { get; set; } = new List<CategoryOptionDto>();
	}
}
