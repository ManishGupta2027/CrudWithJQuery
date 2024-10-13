using Crud.Api.Model.CommanModals;

namespace Crud.Api.Model.Category
{
    public class ImageDetailModel: ImageModel
    {
        public string Url { get; set; }
        public string Base64 { get; set; }
    }
}
