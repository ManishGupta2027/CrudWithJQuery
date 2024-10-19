using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Dapper;
using Crud.Data.Entities;
using Crud.Data.Entities.Brand;
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
			var dbResponse = _dapperRepository.Get<Brand>("procGetBrandDetail_20240917", dbParams, "MasterDataConnectionStrings");
			//var a  = new List<Product>();
			return dbResponse;
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
					@logoName = brand.LogoName,
					@logoUrl = brand.LogoUrl,
					@isActive = brand.IsActive,
					@isFeatured = brand.IsFeatured,
				
				});
			var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertBrand_20241014", dbParams, "MasterDataConnectionstrings");
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
		public BoolResponse BrandMedia(Guid brandId, List<Image> model)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
			new
			{
				@BrandId = brandId,
				@BrandMediaJson = JsonConvert.SerializeObject(model)

			});
			var dbResponse = _dapperRepository.Update<BoolResponse>("procJsonUpsertBrandMedia", dbParams, "MasterDataConnectionstrings");
			return dbResponse;
		}
	}
}
