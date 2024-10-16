namespace Crud.Api.Model.Brand
{
    public class UpdateBrandModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string LogoName { get; set; }
        public string LogoURL { get; set; }
        public string LogoBase64 { get; set; }
        public Configuration Flags { get; set; }
        public List<ImageUpsertModel> Images { get; set; }
        public List<VideoUpsertModel> Videos { get; set; }
    }
    public class Configuration{
      public bool IsActive { get; set; }
      public string IsFeatured { get; set; }
    }

  
 
}
