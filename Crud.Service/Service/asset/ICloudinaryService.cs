using CloudinaryDotNet.Actions;
using Crud.Data.Entities.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Service.Service.asset
{
    public interface ICloudinaryService
    {
        Task<RootFoldersResult> GetRootFoldersAsync();
        Task<RootFoldersResult> GetSubFoldersAsync(string parentFolder);
        Task<FolderResult> CreateFolderAsync(string folderPath);
        Task<Data.Entities.Asset.DeleteFolderResult> DeleteFolderAsync(string folderPath);  // New method for deleting a folder
        Task<ImageUploadResult> UploadImageAsync(string base64Image);  // New method for uploading images
      
        Task<string> UploadImageAsync(string base64, string fileName, string folderName);
       
    }

}
