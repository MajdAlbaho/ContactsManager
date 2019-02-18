using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.DTO
{
    public class Employee : ModelBase<int>
    {
        public int PersonId { get; set; }
        public int BranchId { get; set; }
        public string Email { get; set; }
        public string AnyDesk { get; set; }

        public Person Person { get; set; }
        public Branch Branch { get; set; }

    }
}
