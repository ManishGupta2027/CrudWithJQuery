using AutoMapper;
using CrudOperation.Profiler;
using Newtonsoft.Json.Serialization;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json;
using System.Web.Http;

namespace CrudOperation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Register AutoMapper
            RegisterAutoMapper();
            // Configure JSON serialization
            // ConfigureJsonSerialization();
            // Enable global camelCase serialization
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };
        }

        //private void ConfigureJsonSerialization()
        //{
        //    var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
        //    jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        //    jsonFormatter.SerializerSettings.Formatting = Formatting.Indented;

        //    // Optionally, remove the XML formatter to only use JSON
        //    GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        //}

            private void RegisterAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); // Add the mapping profile
            });

            // Store the Mapper instance for use across the application
            var mapper = config.CreateMapper();
            Application["Mapper"] = mapper;
        }
    }
}
