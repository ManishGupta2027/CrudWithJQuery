using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities.Product
{
    public class UpdateProduct: BaseEntity
    {

        public BasicInfo BasicInfo { get; set; }
        public Identifier Identifier { get; set; }
        public Media Media { get; set; }
        public Inventory Inventory { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public int Status { get; set; }
    }
    public class BasicInfo
    {
        public string Name { get; set; }
        public string StockCode { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Gender { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
    }

    // Identification details for the product
    public class Identifier
    {
        public string SKU { get; set; }
        //public string StockCode { get; set; }
        public string EAN { get; set; }  // European Article Number
        public string UPC { get; set; }  // Universal Product Code
    }

    // Media details, such as images and videos
    public class Media
    {
        public List<Image> Files { get; set; }
        // public List<string> VideoUrls { get; set; } = new List<string>();
    }

    // Inventory details, such as stock levels
    public class Inventory
    {
        public int StockQuantity { get; set; }
        public int MinOrderQuantity { get; set; }
        public int MaxOrderQuantity { get; set; }
        public decimal Weight { get; set; }
        public string Dimensions { get; set; }
    }
}
