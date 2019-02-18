using AutoMapper;
using CacheCow.Server.WebApi;
using ContactsManager.Api.Helper;
using ContactsManager.Repository.Entities;
using ContactsManager.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace ContactsManager.Api.Controllers
{
    [RoutePrefix("api")]
    public class EmployeeController : BaseApiController<IEmployeeRepository, Employee, DTO.Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository repository)
            : base(repository) {
            _employeeRepository = repository;
        }

        [HttpGet]
        [Route("api/Employee", Name = "EmployeeList"), HttpCache(DefaultExpirySeconds = 60)]
        public IHttpActionResult Get(string sort = "Id",
                    string Email = null, string AnyDesk = null,
                    int page = 1, int pageSize = 3,
                    string fields = null) {
            try {
                var listOfFields = fields?.ToLower().Split(',').ToList();

                var items = _employeeRepository.GetAll()?
                    .ApplySort(sort)
                    .Where(e => (Email == null || e.Email == Email))
                    .Where(e => (AnyDesk == null || e.AnyDesk == AnyDesk));

                int totalCount = items.Count();
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                if (pageSize > _maxPageSize)
                    pageSize = _maxPageSize;

                var urlHelper = new UrlHelper(Request);

                var prevLink = page > 1 ? urlHelper.Link("EmployeeList",
                    new {
                        page = page - 1,
                        pageSize,
                        fields,
                        Email,
                        AnyDesk,
                        sort
                    }) : "";
                var nextLink = page < totalPages ? urlHelper.Link("EmployeeList",
                    new {
                        page = page + 1,
                        pageSize,
                        fields,
                        Email,
                        AnyDesk,
                        sort
                    }) : "";


                var paginationHeader = new {
                    currentPage = page,
                    pageSize,
                    totalCount,
                    totalPages,
                    previousPageLink = prevLink,
                    nextPageLink = nextLink
                };

                HttpContext.Current.Response.Headers.Add("X-Pagination",
                    Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

                return Ok(items
                    .ApplySort(sort)
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize).ToList()
                    .Select(e => ExpandObject.CreateDataShapedObject(Mapper.Map<DTO.JobTitle>(e), listOfFields))
                    .ToList());
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }
    }
}
