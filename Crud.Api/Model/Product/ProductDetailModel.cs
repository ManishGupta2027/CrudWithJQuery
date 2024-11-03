namespace Crud.Api.Model.Product
{
    public class ProductDetailModel 
    {
        public Guid Id { get; set; }
		public BasicInfoModel BasicInfo { get; set; }
		public IdentifierModel Identifier { get; set; }
		public MediaModel Media { get; set; }
		public InventoryModel Inventory { get; set; }
		public bool IsActive { get; set; }

	}
}
