using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Dapper;
using Crud.Data.Entities;
using Crud.Data.Entities.Category;
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
				@code = category.Code
			
			});
		var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertCategory_20240919", dbParams, "MasterDataConnectionstrings");
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

    }
}
