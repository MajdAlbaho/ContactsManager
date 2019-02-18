using ContactsManager.Repository.Entities;
using ContactsManager.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Api.Controllers
{
    public class BranchTypeController : BaseApiController<IBranchTypeRepository, BranchType, DTO.BranchType>
    {
        private readonly IBranchTypeRepository _branchTypeRepository;

        public BranchTypeController(IBranchTypeRepository repository)
            : base(repository) {
            _branchTypeRepository = repository;
        }
    }
}
