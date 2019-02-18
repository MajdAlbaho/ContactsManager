using ContactsManager.DTO;
using ContactsManager.WebClient.Helper;
using ContactsManager.WebClient.ViewModel;
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
    public class HomeController : Controller
    {
        public async Task<ActionResult> Contacts(int page = 1) {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:54427/");

            var response = await client.GetAsync("api/JobTitle?page=" + page);
            ContactsViewModel contactsViewModel = new ContactsViewModel();

            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IList<JobTitle>>(content);

                var pagingInfo = HeaderParser.FindAndParsePagingInfo(response.Headers);

                contactsViewModel = new ContactsViewModel() {
                    pagedList = new StaticPagedList<JobTitle>(result, pagingInfo.CurrentPage,
                    pagingInfo.PageSize, pagingInfo.TotalCount),
                    PagingInfo = pagingInfo
                };
            }

            return View(contactsViewModel);
        }

        [HttpGet]
        public ActionResult CreateContact() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateContact(JobTitle jobTitle) {
            if (!ModelState.IsValid)
                return View();

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:54427/");


            var serializedItem = JsonConvert.SerializeObject(jobTitle);

            var response = await client.PostAsync("api/JobTitle",
                                new StringContent(serializedItem,
                                    Encoding.Unicode, "application/json"));
            if (response.IsSuccessStatusCode) {
                return RedirectToAction("Contacts");
            }
            else
                return View();
        }

        [HttpGet]
        public ActionResult EditContact() {
            return View();
        }

        [HttpPut]
        public ActionResult EditContact(int Id, JobTitle jobTitle) {
            return null;
        }


        [HttpGet]
        public ActionResult DeleteContact() {
            return View();
        }

        [HttpDelete]
        public ActionResult DeleteContact(int Id) {
            return null;
        }
    }
}