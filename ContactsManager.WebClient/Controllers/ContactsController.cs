using ContactsManager.Data;
using ContactsManager.DTO;
using ContactsManager.WebClient.Helper;
using ContactsManager.WebClient.ViewModel.Contact;
using Newtonsoft.Json;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContactsManager.WebClient.Controllers
{
    public class ContactsController : Controller
    {
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
                    Phones = new List<string>()
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
        
        public async Task<ActionResult> Edit(int Id,ContactViewModel contactViewModel) {
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
    }
}