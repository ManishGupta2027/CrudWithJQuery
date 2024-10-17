using Crud.Api.Model.CommanModals;

namespace Crud.Api.Model.Category
{
    public class CategoryDetailModel : CategoryModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string LogoUrl { get; set; }
        public string? LogoBase64 { get; set; }
        public List<ImageDetailModel> Images { get; set; }
        public ConfigurationModel Flags { get; set; }
    }
}
