using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.DTO
{
    public class Person : ModelBase<int>
    {
        public string FullEnName { get; set; }
        public string FullArName { get; set; }
        public int JobTitleId { get; set; }
    }
}
