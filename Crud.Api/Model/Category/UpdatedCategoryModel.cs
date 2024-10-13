using Crud.Api.Model.CommanModals;

namespace Crud.Api.Model.Category
{
    public class UpdatedCategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string LogoPreview { get; set; } 
        public string LogoUrl { get; set; } 
        public List<ImageUpsertModel> Images { get; set; }
        public ConfigurationModel Flags { get; set; }
    }
}
