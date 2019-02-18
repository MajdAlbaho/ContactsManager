using ContactsManager.Repository.Entities;
using ContactsManager.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Api.Controllers
{
    public class AreaController : BaseApiController<IAreaRepository, Area, DTO.Area>
    {
        private readonly IAreaRepository _areaRepository;
        public AreaController(IAreaRepository repository)
            : base(repository) {
            _areaRepository = repository;
        }
    }
}
