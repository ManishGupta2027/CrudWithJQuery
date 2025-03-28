using System.Web.Mvc;
using CrudOperation.Data;
using CrudOperation.Repository;
using CrudOperation.Repository.Tray;
using CrudOperation.Service;
using CrudOperation.Service.Content;
using CrudOperation.Service.Tray;
using Unity;
using Unity.Mvc5;

namespace CrudOperation
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

			// register all your components with the container here
			// it is NOT necessary to register your controllers

			container.RegisterType<IDataFactory, DataFactory>();
			container.RegisterType<IProductRepository, ProductRepository>();
			container.RegisterType<IProductService, ProductService>();
			container.RegisterType<ITrayRepository, TrayRepository>();
			container.RegisterType<ITrayService, TrayService>();
			container.RegisterType<IContentService, ContentService>();

			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}