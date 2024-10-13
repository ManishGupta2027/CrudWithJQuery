using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Dapper;
using Crud.Data.Entities;
using Crud.Data.Entities.Category;
using Crud.Data.Entities.ProductCustomField;
using Dapper;
using Newtonsoft.Json;

namespace Crud.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
	{ 
	private readonly IDapperRepository _dapperRepository;
	public CategoryRepository(IDapperRepository dapperRepository)
	{
		_dapperRepository = dapperRepository;

	}
	public BoolResponse SaveCategory(Category category)
	{
		DynamicParameters dbParams = new DynamicParameters();
		dbParams.AddDynamicParams(
			new
			{
				@Id = category.Id,
				@name = category.Name,
				@code = category.Code
							}
		);
		var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertCategory_20240919", dbParams, "MasterDataConnectionStrings");
		return dbResponse;
	}
	public Category GetCategoryListById(Guid id)
	{
		DynamicParameters dbParams = new DynamicParameters();
		dbParams.AddDynamicParams(
			new
			{
				@Id = id,


			}
		);
		var dbResponse = _dapperRepository.Get<dynamic>("procGetCategoryDetail_20241013", dbParams, "MasterDataConnectionStrings");
        if (dbResponse != null)
        {
            // Convert the dynamic response to a Category object
            var category = new Category
            {
                Id = dbResponse.Id,
                Name = dbResponse.Name,
                Code = dbResponse.Code,
                ShortDescription = dbResponse.ShortDescription,
                Description = dbResponse.Description,
                LogoUrl = dbResponse.LogoUrl,
                CreatedBy = dbResponse.CreatedBy,
                LastUpdatedBy = dbResponse.LastUpdatedBy,
                // Deserialize the Flags JSON
                Flags = JsonConvert.DeserializeObject<Configuration>(dbResponse.Flags.ToString()),
                // Deserialize the Images JSON array
                Images = JsonConvert.DeserializeObject<List<Image>>(dbResponse.Images.ToString())
            };

            return category;
        }

        return null;
        }

	public BoolResponse UpsertCategory(Category category)
	{
		DynamicParameters dbParams = new DynamicParameters();
		dbParams.AddDynamicParams(
			new
			{
				@id = category.Id,
				@name = category.Name,
				@code = category.Code,
                @description = category.Description,
                @shortDescription = category.ShortDescription,
                @logoUrl = category.LogoUrl,
                @isActive = category.Flags.IsActive,
                @isFeatured = category.Flags.IsFeatured
			
			});
		var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertCategory_20241013", dbParams, "MasterDataConnectionstrings");
		return dbResponse;
	}

	public List<Category> GetCategoryList(int currentPage, int pageSize, string name)
	{
		DynamicParameters dbParams = new DynamicParameters();
		dbParams.AddDynamicParams(
			new
			{
				@Name=  name,
				@CurrentPage = 1,
				@PageSize = 40


			}
		);
		var dbResponse = _dapperRepository.GetAll<Category>("procGetCategoryList_20240919", dbParams, "MasterDataConnectionStrings");
		return dbResponse;
	}
	public BoolResponse DeleteCategory(Guid id)
	{
		DynamicParameters dbParams = new DynamicParameters();
		dbParams.AddDynamicParams(new { @Id = id });
		var dbResponse = _dapperRepository.Update<BoolResponse>("procDeleteCategory_20240919", dbParams, "MasterDataConnectionStrings");
		return dbResponse;
	}

        public BoolResponse Media(Guid categoryId, Image model)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.AddDynamicParams(
            new
            {
                @id = categoryId,
                @MediaJson=  JsonConvert.SerializeObject(model)

        });
            var dbResponse = _dapperRepository.Update<BoolResponse>("procJsonUpsertCategoryMedia", dbParams, "MasterDataConnectionstrings");
            return dbResponse;
        }
    }
}
