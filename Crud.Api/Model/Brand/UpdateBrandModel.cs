namespace Crud.Api.Model.Brand
{
    public class UpdateBrandModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string LogoPreview { get; set; }
        public ConfigurationModel Flags { get; set; }
        public ImageModel Images { get; set; }
        public VideoModel Videos { get; set; }
    }
    public class ConfigurationModel{
      public bool IsActive { get; set; }
      public string IsFeatured { get; set; }
    }
    public class ImageModel
    {
        public string Name { get; set; }
        public string Base64 { get; set; }

        public int DisplayOrder { get; set; }
        public string Description { get; set; }
    }
    public class VideoModel { }
 
}
