namespace ShoesShop.Api.Contracts.ProductSizes
{
	public sealed class AddProductSizeRequest
	{
		public int ProductID { get; set; }
		public int SizeID { get; set; }
	}
}
