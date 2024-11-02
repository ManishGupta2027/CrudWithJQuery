using Crud.Api.Model.CommanModals;

namespace Crud.Api.Model.Product
{
    public class UpdateProductModel
    {

        public BasicInfoModel BasicInfo { get; set; }
        public IdentifierModel Identifier { get; set; }
        public MediaModel Media { get; set; }
        public InventoryModel Inventory { get; set; }
        public bool IsActive { get; set; }
    }
    // Basic Information about the product
   
}
