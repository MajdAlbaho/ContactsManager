using ContactsManager.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContactsManager.WebClient.ViewModel.Contact
{
    public class ContactViewModel
    {

        [DisplayName("Person")]
        public List<SelectListItem> Persons { get; set; }

        [Required]
        public int PersonId { get; set; }

        [DisplayName("Branch")]
        public List<SelectListItem> Branches { get; set; }

        [Required]
        public int BranchId { get; set; }

        public string Email { get; set; }

        [Required]
        public string AnyDesk { get; set; }

        public List<string> Phones { get; set; }
    }
}
