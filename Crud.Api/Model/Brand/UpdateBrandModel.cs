using Crud.Api.Model.CommanModals;

namespace Crud.Api.Model.Brand
{
    public class UpdateBrandModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string LogoName { get; set; }
        public string? LogoUrl { get; set; }
        public string? LogoBase64 { get; set; }
        public ConfigurationModel Flags { get; set; }
        public List<ImageUpsertModel> Images { get; set; }
      //  public List<VideoUpsertModel>? Videos { get; set; }
    }
}
