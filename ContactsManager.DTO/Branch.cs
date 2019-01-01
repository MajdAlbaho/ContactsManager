using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.DTO
{
    public class Branch : MultiLangModelBase<int>
    {
        public int BranchTypeId { get; set; }
        public int CityId { get; set; }
        public int AreaId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
