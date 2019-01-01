using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.DTO
{
    public class Person : ModelBase<int>
    {
        private string FullEnName { get; set; }
        private string FullArName { get; set; }
        public int JobTitleId { get; set; }
    }
}
