using ContactsManager.WebClient.Helper;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.WebClient.ViewModel.Contact
{
    public class ContactsViewModel
    {
        public IPagedList<EmployeeViewModel> ContactsPagedList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }

    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        public string PersonFullName { get; set; }

        [DisplayName("Job title")]
        public string JobTitle { get; set; }

        [DisplayName("Branch")]
        public string Branch { get; set; }

        [DisplayName("Any desk")]
        public string AnyDesk { get; set; }

        [DisplayName("Phones")]
        public List<string> Phones { get; set; }
    }
}
