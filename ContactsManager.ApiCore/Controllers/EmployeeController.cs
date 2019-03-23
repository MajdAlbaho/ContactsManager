using AutoMapper;
using ContactsManager.ApiCore.Helpers;
using ContactsManager.ApiCore.Models;
using ContactsManager.DTO;
using ContactsManager.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace ContactsManager.ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseApiController<IEmployeeRepository, Employee, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IUrlHelper urlHelper) {
            _employeeRepository = employeeRepository;
        }


        // GET api/values
        [HttpGet(Name = nameof(GetAll))]
        [Authorize]
        public ActionResult<IEnumerable<string>> GetAll(QueryParameters queryParameters) {
            var items = _employeeRepository.GetAll();

            var result = items.OrderBy(queryParameters.OrderBy,
              queryParameters.IsDescending());

            if (queryParameters.HasQuery()) {
                items = items
                    .Where(x => x.AnyDesk.ToString().Contains(queryParameters.Query.ToLowerInvariant())
                    || x.Person.FullEnName.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            List<Employee> employees = Mapper.Map<List<Employee>>(items
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount).ToList());

            var allItemCount = employees.Count();

            var paginationMetadata = new {
                totalCount = allItemCount,
                pageSize = queryParameters.PageCount,
                currentPage = queryParameters.Page,
                totalPages = queryParameters.GetTotalPages(allItemCount)
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            var links = CreateLinksForCollection(queryParameters, allItemCount);

            var toReturn = employees.Select(x => ExpandSingleItem(x));

            return Ok(new {
                value = toReturn,
                links
            });
        }
    }
}
