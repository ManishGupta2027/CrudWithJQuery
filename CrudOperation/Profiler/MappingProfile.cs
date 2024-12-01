using AutoMapper;
using CrudOperation.Entities.Trays;
using CrudOperation.Models.Tray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Profiler
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Define mappings here
            CreateMap<TrayAddModel, Tray>();
            CreateMap<TrayDetailModel, Tray>().ReverseMap();
            // Add other mappings as needed
        }
    }
}