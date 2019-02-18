using ContactsManager.Repository.Entities;
using ContactsManager.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Api.Controllers
{
    public class BranchController : BaseApiController<IBranchRepository, Branch, DTO.Branch>
    {
        private readonly IBranchRepository _branchRepository;
        public BranchController(IBranchRepository repository) 
            : base(repository) {
            _branchRepository = repository;
        }


    }
}
