using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Dapper;
using Crud.Data.Entities;
using Crud.Data.Entities.Brand;
using Crud.Data.Entities.Category;
using Dapper;
using Newtonsoft.Json;

namespace Crud.Data.Repository
{
    public class BrandRepository : IBrandRepository
	{
	private readonly IDapperRepository _dapperRepository;
	public BrandRepository(IDapperRepository dapperRepository)
	{
		_dapperRepository = dapperRepository;

	}
	public BoolResponse SaveBrand(Brand brand)
	{
		DynamicParameters dbParams = new DynamicParameters();
		dbParams.AddDynamicParams(
			new
			{
				@Id = brand.Id,
				@name = brand.Name,
				@shortDescription = brand.ShortDescription,
				@description = brand.Description,
			}
		);
		var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertBrand_20240917", dbParams, "MasterDataConnectionStrings");
		return dbResponse;
	}
		public Brand GetBrandById(Guid id)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@Id = id,


				}
			);
			var dbResponse = _dapperRepository.Get<dynamic>("procGetBrandDetail_20241013", dbParams, "MasterDataConnectionStrings");
			if (dbResponse != null)
			{

				var brand = new Brand
				{
					Id = dbResponse.Id,
					Name = dbResponse.Name,
					ShortDescription = dbResponse.ShortDescription,
					Description = dbResponse.Description,
					LogoPreview = dbResponse.LogoPreview,
					// Deserialize the Flags JSON
					Flags = JsonConvert.DeserializeObject<Configuration>(dbResponse.Flags ?? ""),
					// Deserialize the Images JSON array
					Images = JsonConvert.DeserializeObject<List<Image>>(dbResponse.Images ?? "")
				};
				//var a  = new List<Product>();
				return brand;
			}
			return null;
		}

		public BoolResponse UpsertBrand(Brand brand)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@id = brand.Id,
					@name = brand.Name,
					@shortDescription = brand.ShortDescription,
					@description = brand.Description,
				});
			var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertBrand_20240917", dbParams, "MasterDataConnectionstrings");
			return dbResponse;
		}

		public List<Brand> GetBrandList(int currentPage, int pageSize)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@CurrentPage = currentPage,
					@PageSize = pageSize

				}
			);
			var dbResponse = _dapperRepository.GetAll<Brand>("procGetBrandList_20240918", dbParams, "MasterDataConnectionStrings");
			return dbResponse;
		}
		public BoolResponse DeleteBrand(Guid id)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(new { @Id = id });
			var dbResponse = _dapperRepository.Update<BoolResponse>("procDeleteBrand_18092024", dbParams, "MasterDataConnectionStrings");
			return dbResponse;
		}
	}
}
