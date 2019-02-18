using AutoMapper;
using ContactsManager.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ContactsManager.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start() {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            Mapper.Initialize(map => {
                map.CreateMap<DTO.Area, Area>();
                map.CreateMap<DTO.Branch, Branch>();
                map.CreateMap<DTO.BranchType, BranchType>();
                map.CreateMap<DTO.City, City>();
                map.CreateMap<DTO.Employee, Employee>();
                map.CreateMap<DTO.JobTitle, JobTitle>();
                map.CreateMap<DTO.Person, Person>();
            });
        }
    }
}
