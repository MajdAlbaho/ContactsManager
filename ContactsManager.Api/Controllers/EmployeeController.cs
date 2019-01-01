using ContactsManager.Repository.Data;
using ContactsManager.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ContactsManager.Api.Controllers
{
    [RoutePrefix("api")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository _employeeRepository;


        public EmployeeController(IEmployeeRepository employeeRepository) {
            _employeeRepository = employeeRepository;
        }

        [HttpGet, Route("Branch/{branchId}/Employees")]
        public IHttpActionResult Get(int branchId) {
            try {
                var items = Mapper.MapCollection<DTO.Employee>(_employeeRepository.FindAll(emp => emp.BranchId == branchId).ToList());

                if (items == null)
                    return NotFound();

                return Ok(items);
            }
            catch (Exception) {
                return InternalServerError();
            }
        }
    }
}
