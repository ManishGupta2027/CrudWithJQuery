using CloudinaryDotNet.Actions;
using Crud.Data.Entities.Asset;
using Crud.Service.Service.asset;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Crud.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetController : ControllerBase
    {
        private readonly CloudinaryService _cloudinaryService;

        public AssetController(CloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost("GetDirContents")]
        public async Task<IActionResult> GetDirectoryContents([FromBody] CloudinaryRequest request)
        {
            if (request.Command == "GetDirContents" && request.Arguments.PathInfo != null)
            {
                string folderPath = string.Join("/", request.Arguments.PathInfo); // Convert pathInfo to folder path
                var files = await _cloudinaryService.GetAllFilesInFolderAsync(folderPath);
                return Ok(files);
            }
            return BadRequest("Invalid request");
        }

        // Endpoint to get root folders and files
        [HttpGet("GetRootContents")]
        public async Task<IActionResult> GetRootContents()
        {
            var results = await _cloudinaryService.GetRootFoldersAndFilesAsync();
            return Ok(results);
        }
        [HttpGet("GetSubfolderContents/{folderId}")]
        public async Task<IActionResult> GetSubfolderContents(string folderId)
        {
            var results = await _cloudinaryService.GetSubFoldersAndFilesAsync(folderId);
            return Ok(results);
        }

        [HttpGet("folders")]
        public async Task<IActionResult> GetRootFolders()
        {
            var foldersResult = await _cloudinaryService.GetRootFoldersAsync();
            if (foldersResult == null)
            {
                return NotFound(new { message = "No root folders found." });
            }

            var response = new
            {
                folders = foldersResult.Folders.Select(f => new
                {
                    name = f.Name,
                    path = f.Path,
                   // external_id = f.ExternalId
                }).ToList(),
                next_cursor = foldersResult.NextCursor,
                total_count = foldersResult.Folders.Count
            };

            return Ok(response);
        }

        [HttpGet("folders/{folder}")]
        public async Task<IActionResult> GetSubFolders(string folder)
        {
            // Decode the folder path to handle URL-encoded characters like %2F
            string decodedFolder = HttpUtility.UrlDecode(folder);

            var subFoldersResult = await _cloudinaryService.GetSubFoldersAsync(decodedFolder);
            if (subFoldersResult == null)
            {
                return NotFound(new { message = $"No subfolders found for folder '{folder}'." });
            }

            var response = new
            {
                folders = subFoldersResult.Folders.Select(f => new
                {
                    name = f.Name,
                    path = f.Path,
                   // external_id = f.ExternalId
                }).ToList(),
                next_cursor = subFoldersResult.NextCursor,
                total_count = subFoldersResult.Folders.Count
            };

            return Ok(response);
        }
        [HttpPost("folders/{folder}")]
        public async Task<IActionResult> CreateFolder(string folder)
        {
            // Decode folder path to handle URL-encoded characters
            string decodedFolder = HttpUtility.UrlDecode(folder);

            var folderResult = await _cloudinaryService.CreateFolderAsync(decodedFolder);
            if (folderResult == null || !folderResult.Success)
            {
                return BadRequest(new { message = $"Failed to create folder '{folder}'." });
            }

            var response = new
            {
                success = folderResult.Success,
                path = folderResult.Path,
                name = folderResult.Name
            };

            return Ok(response);
        }
        [HttpDelete("folders/{folder}")]
        public async Task<IActionResult> DeleteFolder(string folder)
        {
            // Decode folder path to handle URL-encoded characters
            string decodedFolder = HttpUtility.UrlDecode(folder);

            var deleteResult = await _cloudinaryService.DeleteFolderAsync(decodedFolder);
            if (deleteResult == null || deleteResult.Deleted == null || deleteResult.Deleted.Count == 0)
            {
                return NotFound(new { message = $"Failed to delete folder '{folder}' or folder not found." });
            }

            var response = new
            {
                deleted = deleteResult.Deleted
            };

            return Ok(response);
        }
        //        {
        //    "base64Image": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==",
        //    "folder": "tsestayush/tsestayush"
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpPost("upload")]
        //public async Task<IActionResult> UploadImage([FromBody] UploadImageRequest request)
        //{
        //    if (string.IsNullOrWhiteSpace(request.Base64Image))
        //    {
        //        return BadRequest(new { message = "Image data is required." });
        //    }

        //    if (string.IsNullOrWhiteSpace(request.Folder))
        //    {
        //        return BadRequest(new { message = "Folder name is required." });
        //    }

        //    var uploadResult = await _cloudinaryService.UploadImageAsync(request.Base64Image, request.Folder);
        //    if (uploadResult == null)
        //    {
        //        return BadRequest(new { message = "Failed to upload image." });
        //    }

        //    var response = new
        //    {
        //        asset_id = uploadResult.AssetId,
        //        public_id = uploadResult.PublicId,
        //        version = uploadResult.Version,
        //        // version_id = uploadResult.VersionId,
        //        signature = uploadResult.Signature,
        //        width = uploadResult.Width,
        //        height = uploadResult.Height,
        //        format = uploadResult.Format,
        //        resource_type = uploadResult.ResourceType,
        //        created_at = uploadResult.CreatedAt,
        //        tags = uploadResult.Tags,
        //        bytes = uploadResult.Bytes,
        //        type = uploadResult.Type,
        //        etag = uploadResult.Etag,
        //        placeholder = uploadResult.Placeholder,
        //        url = uploadResult.Url.ToString(),
        //        secure_url = uploadResult.SecureUrl.ToString(),
        //        // folder = uploadResult.Folder,
        //        // existing = uploadResult.Existing,
        //        original_filename = uploadResult.OriginalFilename
        //    };

        //    return Ok(response);
        //}

        // POST: api/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromBody] UploadRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Base64) || string.IsNullOrEmpty(request.FileName) || string.IsNullOrEmpty(request.FolderName))
            {
                return BadRequest("Invalid upload request");
            }

            try
            {
                // Use the CloudinaryService to upload the image
                var uploadedUrl = await _cloudinaryService.UploadImageAsync(request.Base64, request.FileName, request.FolderName);

                // Return the URL of the uploaded image
                return Ok(new { Url = uploadedUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("resources")]
        public async Task<IActionResult> GetResourcesByPublicIds([FromQuery] string publicIds)
        {
            if (string.IsNullOrWhiteSpace(publicIds))
            {
                return BadRequest(new { message = "Public IDs are required." });
            }

            var publicIdsArray = publicIds.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var resources = await _cloudinaryService.GetResourcesByPublicIdsAsync(publicIdsArray);

            if (resources == null || !resources.Any())
            {
                return NotFound(new { message = "No resources found." });
            }

            return Ok(new { resources });
        }

        [HttpGet("all-resources")]
        public async Task<ListResourcesResult> GetAllResources()
        {
     

            // Call the service method to get resources by folder name
           return await _cloudinaryService.AllResources();

        }


    }

    public class CloudinaryRequest
    {
        public string Command { get; set; }
        public Arguments Arguments { get; set; }
    }

    public class Arguments
    {
        public List<string> PathInfo { get; set; }
    }

}
