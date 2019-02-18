using ContactsManager.WebClient.Helper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.WebClient.ViewModel
{
    public class ContactsViewModel
    {
        public IPagedList<DTO.JobTitle> pagedList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
