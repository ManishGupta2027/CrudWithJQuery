using AutoMapper;
using Crud.Api.Model.Brand;
using Crud.Api.Model.Category;
using Crud.Api.Model.List;
using Crud.Api.Model.Product;
using Crud.Api.Model.ProductCustomField;
using Crud.Data.Entities.Brand;
using Crud.Data.Entities.Category;
using Crud.Data.Entities.Product;
using Crud.Data.Entities.ProductCustomField;

namespace Crud.Api.Profiler
{
    public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<ProductCreateModel, Product>();
			CreateMap<UpdateProductModel, Product>();
			CreateMap<Product,ProductDetailModel>();
			CreateMap<BrandModel, Brand>();
			CreateMap<Brand, BrandDetailModel>();
			CreateMap<UpdateBrandModel, Brand>();
			CreateMap<CategoryModel, Category>();
			CreateMap<Category, CategoryDetailModel>();
			CreateMap<Category, ListModel>();
			CreateMap<UpdatedCategoryModel, Category>();
			CreateMap<Product, ProductListModel>();
			CreateMap<Brand, BrandListModel>();
			CreateMap<Category, CategoryListModel>();
			CreateMap<ProductCustomFieldModel, ProductCustomField>();
			CreateMap<UpdateProductCustomFieldModel, ProductCustomField>();
			CreateMap<ProductCustomField , ProductCustomFieldListModel>();
			CreateMap<ProductCustomField, ProductCustomFieldDetailModel>();
			//	.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
			//	.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductPrice));
		}
	}
}
