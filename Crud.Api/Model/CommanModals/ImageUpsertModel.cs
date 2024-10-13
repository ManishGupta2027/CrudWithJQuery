namespace Crud.Api.Model.CommanModals
{
    public class ImageUpsertModel:ImageModel
    {
        public string? Base64 { get; set; }
        public string? Url { get; set; }
    }
}
