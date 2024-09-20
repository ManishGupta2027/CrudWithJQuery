using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Dapper;
using Crud.Data.Entities;
using Dapper;

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
				@code = category.Code,
				@createdBy = category.CreatedBy,
				@lastUpdatedBy = category.LastUpdatedBy,
			}
		);
		var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertCategory_20240919", dbParams, "MasterDataConnectionStrings");
		return dbResponse;
	}
	public Category GetCategoryListById(int id)
	{
		DynamicParameters dbParams = new DynamicParameters();
		dbParams.AddDynamicParams(
			new
			{
				@Id = id,


			}
		);
		var dbResponse = _dapperRepository.Get<Category>("procGetCategoryDetail_20240919", dbParams, "MasterDataConnectionStrings");
		//var a  = new List<Product>();
		return dbResponse;
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
			//	@created = category.Created,
				@createdBy = category.CreatedBy,
			//	@lastUpdated = category.LastUpdated,
				@lastUpdatedBy = category.LastUpdatedBy,
			});
		var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertCategory_20240919", dbParams, "MasterDataConnectionstrings");
		return dbResponse;
	}

	public List<Category> GetCategoryList(int currentPage, int pageSize)
	{
		DynamicParameters dbParams = new DynamicParameters();
		dbParams.AddDynamicParams(
			new
			{
				@CurrentPage = 1,
				@PageSize = 40

			}
		);
		var dbResponse = _dapperRepository.GetAll<Category>("procGetCategoryList_20240919", dbParams, "MasterDataConnectionStrings");
		return dbResponse;
	}
	public BoolResponse DeleteCategory(int id)
	{
		DynamicParameters dbParams = new DynamicParameters();
		dbParams.AddDynamicParams(new { @Id = id });
		var dbResponse = _dapperRepository.Update<BoolResponse>("procDeleteCategory_20240919", dbParams, "MasterDataConnectionStrings");
		return dbResponse;
	}

    }
}
