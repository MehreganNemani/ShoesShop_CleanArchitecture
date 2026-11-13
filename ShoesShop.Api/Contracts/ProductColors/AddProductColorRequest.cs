namespace ShoesShop.Api.Contracts.ProductColors
{
	public sealed class AddProductColorRequest
	{
		public int ProductID { get; set; }
        public int ColorID { get; set; }
    }
}
