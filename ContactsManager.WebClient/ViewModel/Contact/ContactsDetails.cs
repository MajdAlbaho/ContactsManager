using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.WebClient.ViewModel.Contact
{
    public class ContactsDetails
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string JobTitleName { get; set; }
        public string BranchName { get; set; }
        public string BranchType { get; set; }
        public List<string> PhonesList { get; set; }
        public string CityName { get; set; }
        public string AreaName { get; set; }
        public string Email { get; set; }
        public string AnyDesk { get; set; }
    }
}
