using ContactsManager.Data;
using ContactsManager.DTO;
using ContactsManager.WebClient.Helper;
using ContactsManager.WebClient.ViewModel.Contact;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ContactsManager.WebClient.Controllers
{
    public class ContactsController : BaseController
    {
        public ActionResult SetCulture(string culture) {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return RedirectToAction("Create");
        }

        public async Task<ActionResult> ContactsView(int page = 1) {

            var response = await Client.GetApiClient().GetAsync("api/Employee?page=" + page);
            ContactsViewModel contactsViewModel = new ContactsViewModel();

            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IList<Employee>>(content);

                var pagingInfo = HeaderParser.FindAndParsePagingInfo(response.Headers);

                var items = result.Select(e => new EmployeeViewModel() {
                    Id = e.Id,
                    AnyDesk = e.AnyDesk,
                    Branch = e.Branch.EnName,
                    PersonFullName = e.Person.FullArName,
                    JobTitle = e.Person.JobTitle.EnName,
                    Phones = e.EmployeePhone
                }).ToList();
                
                contactsViewModel = new ContactsViewModel() {
                    ContactsPagedList = new StaticPagedList<EmployeeViewModel>(items, pagingInfo.CurrentPage,
                    pagingInfo.PageSize, pagingInfo.TotalCount),
                    PagingInfo = pagingInfo
                };
            }

            return View(contactsViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Create() {
            IEnumerable<Person> persons = new List<Person>();
            IEnumerable<Branch> branches = new List<Branch>();

            var personResponse = await Client.GetApiClient().GetAsync("api/Person");
            if (personResponse.IsSuccessStatusCode) {
                persons = JsonConvert.DeserializeObject<IEnumerable<Person>>(await personResponse.Content.ReadAsStringAsync());
            }

            var branchResponse = await Client.GetApiClient().GetAsync("api/Branch");
            if (branchResponse.IsSuccessStatusCode) {
                branches = JsonConvert.DeserializeObject<IEnumerable<Branch>>(await branchResponse.Content.ReadAsStringAsync());
            }

            var contactViewModel = new ContactViewModel() {
                Persons = new List<SelectListItem>(persons.Select(p => new SelectListItem() {
                    Text = p.FullEnName,
                    Value = p.Id.ToString()
                })),
                Branches = new List<SelectListItem>(branches.Select(b => new SelectListItem() {
                    Text = b.EnName,
                    Value = b.Id.ToString()
                }))
            };

            return View(contactViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ContactViewModel contactViewModel) {
            if (!ModelState.IsValid)
                return View();

            var serializedItem = JsonConvert.SerializeObject(new Employee() {
                AnyDesk = contactViewModel.AnyDesk,
                BranchId = contactViewModel.BranchId,
                PersonId = contactViewModel.PersonId,
                Email = contactViewModel.Email
            });

            var response = await Client.GetApiClient().PostAsync("api/Employee/Post",
                                    new StringContent(serializedItem,
                                        Encoding.Unicode, "application/json"));
            if (response.IsSuccessStatusCode) {
                return RedirectToAction("ContactsView");
            }
            else
                return View("Error");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int Id) {
            IEnumerable<Person> persons = new List<Person>();
            IEnumerable<Branch> branches = new List<Branch>();
            Employee employee = new Employee();

            var personResponse = await Client.GetApiClient().GetAsync("api/Person");
            if (personResponse.IsSuccessStatusCode) {
                persons = JsonConvert.DeserializeObject<IEnumerable<Person>>(await personResponse.Content.ReadAsStringAsync());
            }

            var branchResponse = await Client.GetApiClient().GetAsync("api/Branch");
            if (branchResponse.IsSuccessStatusCode) {
                branches = JsonConvert.DeserializeObject<IEnumerable<Branch>>(await branchResponse.Content.ReadAsStringAsync());
            }

            var employeeResponse = await Client.GetApiClient().GetAsync("api/Employee/" + Id);
            if (employeeResponse.IsSuccessStatusCode) {
                string res = await employeeResponse.Content.ReadAsStringAsync();
                employee = JsonConvert.DeserializeObject<Employee>(res);
            }

            var contactViewModel = new ContactViewModel() {
                Persons = new List<SelectListItem>(persons.Select(p => new SelectListItem() {
                    Text = p.FullEnName,
                    Value = p.Id.ToString()
                })),
                Branches = new List<SelectListItem>(branches.Select(b => new SelectListItem() {
                    Text = b.EnName,
                    Value = b.Id.ToString()
                })),
                AnyDesk = employee.AnyDesk,
                BranchId = employee.BranchId,
                Email = employee.Email,
                PersonId = employee.PersonId
            };

            return View(contactViewModel);
        }

        public async Task<ActionResult> Edit(int Id, ContactViewModel contactViewModel) {
            if (!ModelState.IsValid)
                return View();

            var serializedItem = JsonConvert.SerializeObject(new Employee() {
                PersonId = contactViewModel.PersonId,
                BranchId = contactViewModel.BranchId,
                AnyDesk = contactViewModel.AnyDesk,
                Email = contactViewModel.Email,
                EmployeePhone = contactViewModel.Phones
            });

            var response = await Client.GetApiClient().PutAsync("api/Employee/" + Id,
                                        new StringContent(serializedItem,
                                        Encoding.Unicode, "application/json"));
            if (response.IsSuccessStatusCode) {
                return RedirectToAction("ContactsView");
            }
            else
                return View("Error");
        }


        [HttpGet]
        public ActionResult Delete() {
            return View();
        }

        public async Task<ActionResult> Delete(int Id) {
            var response = await Client.GetApiClient().DeleteAsync("api/Employee/" + Id);
            if (response.IsSuccessStatusCode) {
                return RedirectToAction("ContactsView");
            }
            else
                return View("Error");
        }

        public async Task<ActionResult> Details(int Id) {
            var employeeResponse = await Client.GetApiClient().GetAsync("api/Employee/" + Id);

            var result = JsonConvert.DeserializeObject<Employee>(await employeeResponse.Content.ReadAsStringAsync());

            if (employeeResponse.IsSuccessStatusCode) {
                var contactDetail = new ContactsDetails() {
                    Id = result.Id,
                    AnyDesk = result.AnyDesk,
                    AreaName = result.Branch.Area.EnName,
                    BranchName = result.Branch.EnName,
                    BranchType = result.Branch.BranchType.EnName,
                    CityName = result.Branch.City.EnName,
                    Email = result.Email,
                    FullName = result.Person.FullEnName,
                    JobTitleName = result.Person.JobTitle.EnName,
                    PhonesList = result.EmployeePhone
                };

                return View(contactDetail);
            }

            return View("Error");
        }
    }
}