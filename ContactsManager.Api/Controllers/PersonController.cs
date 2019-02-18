using ContactsManager.Repository.Entities;
using ContactsManager.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ContactsManager.Api.Controllers
{
    public class PersonController : BaseApiController<IPersonRepository, Person, DTO.Person>
    {
        private readonly IPersonRepository _personRepository;
        public PersonController(IPersonRepository repository)
            : base(repository) {
            _personRepository = repository;
        }
    }
}
