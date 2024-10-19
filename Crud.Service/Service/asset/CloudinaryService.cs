using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Crud.Data.Entities.Asset;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

        // Method to get root folders and files
        public async Task<List<CloudinaryFileResponse>> GetRootFoldersAndFilesAsync()
        {
            var results = new List<CloudinaryFileResponse>();

            // Fetch root folders using Cloudinary's RootFolders method
            var rootFolders = await _cloudinary.RootFoldersAsync();

            foreach (var folder in rootFolders.Folders)
            {
                results.Add(new CloudinaryFileResponse
                {
                    Key = folder.Path,
                    Name = folder.Name,
                    IsDirectory = true,
                    DateModified = DateTime.UtcNow.ToString("o"),
                    Size = 0,
                    HasSubDirectories = true,
                    Url = null
                });
            }

            // Fetch root-level resources (files)
            var resources = await _cloudinary.ListResourcesAsync(new ListResourcesParams
            {
                Type = "upload",
                MaxResults = 500 // Adjust as necessary
            });

            foreach (var resource in resources.Resources)
            {
                if (!resource.PublicId.Contains("/")) // Root-level files only
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
            }

            return results;
        }

        public async Task<List<CloudinaryFileResponse>> GetSubFoldersAndFilesAsync(string folderId)
        {
            var results = new List<CloudinaryFileResponse>();

            // Use Search API to find resources within the specified folder
            var searchResult = await _cloudinary.Search()
                .Expression($"folder:{folderId}")  // Correct expression to search within the folder
                .MaxResults(500)
                .ExecuteAsync();

            // Process files and folders from the search results
            foreach (var resource in searchResult.Resources)
            {
                // Check if it's a folder (you can simulate folder detection based on PublicId)
                if (resource.Type == "upload") // Only considering upload type resources
                {
                    results.Add(new CloudinaryFileResponse
                    {
                        Key = resource.PublicId,
                        Name = resource.DisplayName,
                        IsDirectory = resource.PublicId.EndsWith("/"),  // Simulating directory check
                        DateModified = resource.CreatedAt?.ToString(),
                        Size = resource.Bytes,
                        HasSubDirectories = false, // You can decide this based on additional logic
                        Url = resource.Url
                    });
                }
            }

            // If you need to handle subfolders specifically, process folder paths manually
            // (this depends on how Cloudinary stores subfolders and how you want to treat folder paths)

            return results;
        }

        public async Task<RootFoldersResult> GetRootFoldersAsync()
        {
            var foldersResult = await _cloudinary.RootFoldersAsync();

            if (foldersResult == null)
            {
                return null;
            }

            var rootFoldersResult = new RootFoldersResult
            {
                Folders = foldersResult.Folders.Select(f => new Data.Entities.Asset.Folder
                {
                    Name = f.Name,
                    Path = f.Path,
                 //   ExternalId = f.ExternalId
                }).ToList(),
                NextCursor = foldersResult.NextCursor,
                TotalCount = foldersResult.Folders.Count
            };

            return rootFoldersResult;
        }
        public async Task<RootFoldersResult> GetSubFoldersAsync(string parentFolder)
        {
            var subFoldersResult = await _cloudinary.SubFoldersAsync(parentFolder);

            if (subFoldersResult == null)
            {
                return null;
            }

            var subFolders = new RootFoldersResult
            {
                Folders = subFoldersResult.Folders.Select(f => new Data.Entities.Asset.Folder
                {
                    Name = f.Name,
                    Path = f.Path,
                   // ExternalId = f.ExternalId
                }).ToList(),
                NextCursor = subFoldersResult.NextCursor,
                TotalCount = subFoldersResult.Folders.Count
            };

            return subFolders;
        }
        public async Task<FolderResult> CreateFolderAsync(string folderPath)
        {
            var createFolderResult = await _cloudinary.CreateFolderAsync(folderPath);

            if (createFolderResult == null)
            {
                return null;
            }

            var folderResult = new FolderResult
            {
                Name = createFolderResult.Name,
                Path = createFolderResult.Path,
                Success = createFolderResult.Success
            };

            return folderResult;
        }
        public async Task<Data.Entities.Asset.DeleteFolderResult> DeleteFolderAsync(string folderPath)
        {
            var deleteFolderResult = await _cloudinary.DeleteFolderAsync(folderPath);

            if (deleteFolderResult == null)
            {
                return null;
            }

            var result = new Data.Entities.Asset.DeleteFolderResult
            {
                Deleted = deleteFolderResult.Deleted
            };

            return result;
        }

        public async Task<ImageUploadResult> UploadImageAsync(string base64Image, string folder)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(base64Image),
                Folder = folder // Set the folder here
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult;
        }
        // Upload base64 image
        public async Task<string> UploadImageAsync(string base64, string fileName, string folderName)
        {
            try
            {
                // Ensure the base64 string does not contain any prefix like 'data:image/jpeg;base64,'
                var cleanBase64 = base64.Contains(",") ? base64.Split(',')[1] : base64;

                // Set the upload parameters for the image
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription($"data:image/jpeg;base64,{cleanBase64}"),
                    PublicId = fileName,
                    //PublicId = $"{folderName}/{fileName}",
                    Folder = folderName
                };

                // Upload the image to Cloudinary
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                // Return the secure URL of the uploaded image
                return uploadResult.SecureUrl.ToString();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to upload image to Cloudinary", ex);
            }
        }
		/// <summary>
		/// base64 ,fileName = prefixfolderName/filename,folderName=container Name
		/// </summary>
		/// <param name="base64"></param>
		/// <param name="fileName"></param>
		/// <param name="folderName"></param>
		/// <returns></returns>
		/// <exception cref="ApplicationException"></exception>
		public string UploadImage(string base64, string fileName, string folderName)
        {
            try
            {
                // Ensure the base64 string does not contain any prefix like 'data:image/jpeg;base64,'
                var cleanBase64 = base64.Contains(",") ? base64.Split(',')[1] : base64;

                // Set the upload parameters for the image
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription($"data:image/jpeg;base64,{cleanBase64}"),
                    PublicId = fileName,
                    //PublicId = $"{folderName}/{fileName}",
                    Folder = folderName
                };

                // Upload the image to Cloudinary
                var uploadResult =  _cloudinary.Upload(uploadParams);

                // Return the secure URL of the uploaded image
                return uploadResult.SecureUri.ToString();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to upload image to Cloudinary", ex);
            }
        }
        public async Task<IList<CloudinaryDotNet.Actions.Resource>> GetResourcesByPublicIdsAsync(string[] publicIds)
        {
           // var a = _cloudinary.ListResourceTypes();
            var result = await _cloudinary.ListResourcesByPublicIdsAsync(publicIds);
            return result.Resources.ToList();
        }
        public async Task<ListResourcesResult> AllResources()
        {


            // List all resources starting with the given folderPath (simulate folders with PublicId)
            var resources = await _cloudinary.ListResourcesAsync(new ListResourcesParams
            {
                Type = "upload",
                MaxResults = 1000 // Adjust as necessary (pagination can be handled for more results)
            });
            return resources;
            // var assetFolderParams = new ListResourcesByAssetFolderParams()
            // {
            //     AssetFolder = "technology",
            //     Tags = true,
            //     Metadata = true
            // };
            // var assetFolderResult = _cloudinary.ListResourcesByAssetFolder(folder);
            //var result = _cloudinary.GetResource("sample");

            // var f = _cloudinary.ListResourcesByAssetFolder("avatars");
            //var aa= _cloudinary.GetResource()

            // var a = new GetResourceParamsBase()
            // {
            //     Prefix="Sa"
            // };
            //var b= _cloudinary.GetResourceAsync(a);

            // Call Cloudinary API to list resources by the specified folder
            // var result = await _cloudinary.ListResourceByAssetFolderAsync(folder, false, false, false);

            // Return the list of resources
            // return result.Resources.ToList();
        }

    }

}


