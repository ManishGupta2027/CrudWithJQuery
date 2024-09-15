using AutoMapper;
using Crud.Api.Model;
using Crud.Data.Entities;

namespace Crud.Api.Profiler
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<ProductModel, Product>();
				
			//CreateMap<Product, ProductModel>()
			//	.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
			//	.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductPrice));
		}
	}
}
