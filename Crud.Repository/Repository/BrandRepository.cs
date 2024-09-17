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
				//@Id = brand.Id,
				@Name = brand.Name,
				@ShortDescription = brand.ShortDescription,
				@Description = brand.Description,
			}
		);
		var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertBrand_20240917", dbParams, "MasterDataConnectionStrings");
		return dbResponse;
	}
		public Brand GetBrandListById(int id)
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
	}
}
