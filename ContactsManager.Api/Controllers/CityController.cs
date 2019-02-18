using ContactsManager.Repository.Entities;
using ContactsManager.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Api.Controllers
{
    public class CityController : BaseApiController<ICityRepository, City, DTO.City>
    {
        private readonly ICityRepository _cityRepository;
        public CityController(ICityRepository repository)
            : base(repository) {
            _cityRepository = repository;
        }
    }
}
