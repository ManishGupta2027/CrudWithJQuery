using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Crud.Data.Entities.Asset;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Service.Service.asset
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }

        //// Simulate directory structure for Cloudinary (since Cloudinary doesn't have real directories)
        //public async Task<List<CloudinaryFileResponse>> GetAllFilesAsync()
        //{
        //    var results = new List<CloudinaryFileResponse>();

        //    var resources = await _cloudinary.ListResourcesAsync(new ListResourcesParams());

        //    if (resources?.Resources != null && resources.Resources.Any())
        //    {
        //        foreach (var resource in resources.Resources)
        //        {
        //            results.Add(new CloudinaryFileResponse
        //            {
        //                Key = resource.PublicId,
        //                Name = resource.DisplayName,
        //                IsDirectory = false, // Cloudinary doesn't have directories, so this is always false
        //                DateModified = resource.CreatedAt?.ToString(),
        //                Size = resource.Bytes,
        //                HasSubDirectories = false,
        //                Url = resource.Url.ToString(),
        //            });
        //        }
        //    }

        //    //// Example of simulating some directories
        //    //results.Add(new CloudinaryFileResponse
        //    //{
        //    //    Key = Guid.NewGuid().ToString(),
        //    //    Name = "backendattachments",
        //    //    IsDirectory = true,
        //    //    DateModified = DateTime.UtcNow.ToString("o"),
        //    //    Size = 0,
        //    //    HasSubDirectories = true,
        //    //    Url = null
        //    //});

        //    //results.Add(new CloudinaryFileResponse
        //    //{
        //    //    Key = Guid.NewGuid().ToString(),
        //    //    Name = "barcodes",
        //    //    IsDirectory = true,
        //    //    DateModified = DateTime.UtcNow.ToString("o"),
        //    //    Size = 0,
        //    //    HasSubDirectories = false,
        //    //    Url = null
        //    //});

        //    return results;
        //}
        // Method to get all files and directories from a specific folder
        // Method to get all files and directories from a specific folder
        public async Task<List<CloudinaryFileResponse>> GetAllFilesInFolderAsync(string folderPath = "")
       {
            var results = new List<CloudinaryFileResponse>();

            // List all resources starting with the given folderPath (simulate folders with PublicId)
            var resources = await _cloudinary.ListResourcesAsync(new ListResourcesParams
            {
                Type = "upload",
                MaxResults = 500 // Adjust as necessary (pagination can be handled for more results)
            });

            // Filter resources by the folderPath prefix
            var filteredResources = resources.Resources.Where(r => r.PublicId.StartsWith(folderPath)).ToList();

            // Process files
            foreach (var resource in filteredResources)
            {
                results.Add(new CloudinaryFileResponse
                {
                    Key = resource.PublicId,
                    Name = resource.DisplayName,
                    IsDirectory = false,
                    DateModified = resource.CreatedAt?.ToString(),
                    Size = resource.Bytes,
                    HasSubDirectories = false,
                    Url = resource.Url.ToString()
                });
            }

            // Simulate directories by looking for prefixes (folders in the resource paths)
            var folderPaths = filteredResources
                .Select(r => r.PublicId.Substring(0, r.PublicId.LastIndexOf('/') + 1)) // Get the folder part
                .Distinct() // Remove duplicates
                .Where(f => f.StartsWith(folderPath) && f != folderPath) // Only subfolders
                .ToList();

            foreach (var folder in folderPaths)
            {
                results.Add(new CloudinaryFileResponse
                {
                    Key = folder,
                    Name = folder.TrimEnd('/').Split('/').Last(), // Take the last part of the folder path as the name
                    IsDirectory = true,
                    DateModified = DateTime.UtcNow.ToString("o"),
                    Size = 0,
                    HasSubDirectories = true,
                    Url = null
                });
            }

            return results;
        }
    }
}


