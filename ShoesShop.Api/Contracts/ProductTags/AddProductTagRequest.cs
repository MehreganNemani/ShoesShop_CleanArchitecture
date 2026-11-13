namespace ShoesShop.Api.Contracts.ProductTags
{
	public sealed class AddProductTagRequest
	{
		public int ProductID { get; set; }
		public int TagID { get; set; }
	}
}
