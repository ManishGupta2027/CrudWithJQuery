namespace Crud.Api.Model.Brand
{
    public class UpdateBrandModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
    }
}
