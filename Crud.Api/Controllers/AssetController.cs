using Crud.Service.Service.asset;
using Microsoft.AspNetCore.Mvc;

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
