using Crud.Api.Model.CommanModals;
using Crud.Data.Entities;

namespace Crud.Api.Model.Brand
{
    public class BrandDetailModel : BrandModel
    {
        public Guid Id { get; set; }
        public string LogoUrl { get; set; }
        public ConfigurationModel Flags { get; set; }
        public List<ImageDetailModel> Images { get; set; }
        public List<VideoDetailModel> Videos { get; set; }
    }
}
