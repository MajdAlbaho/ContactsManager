using ContactsManager.Repository;
using ContactsManager.Repository.Interface;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ContactsManager.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents() {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IAreaRepository, AreaRepository>();
            container.RegisterType<IBranchRepository, BranchRepository>();
            container.RegisterType<IBranchTypeRepository, BranchTypeRepository>();
            container.RegisterType<ICityRepository, CityRepository>();
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            container.RegisterType<IJobTitleRepository, JobTitleRepository>();
            container.RegisterType<IPersonRepository, PersonRepository>();



            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}