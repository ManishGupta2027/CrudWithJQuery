using Crud.Data.Entities.Brand;

namespace Crud.Api.Model.Brand
{
    public class BrandDetailModel : BrandModel
    {
        public Guid Id { get; set; }
        public string LogoPreview { get; set; }
        public Configuration Flags { get; set; }
        public List<ImageDetailModel> Images { get; set; }
        public List<VideoDetailModel> Videos { get; set; }
    }
}
