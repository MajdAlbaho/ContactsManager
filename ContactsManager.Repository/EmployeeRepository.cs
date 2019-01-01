using ContactsManager.Repository.Entities;
using ContactsManager.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
    }
}
