using ContactsManager.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.Db
{
    public partial class ContactsManagerContext : DbContext
    {
        public ContactsManagerContext()
            : base() {
        }

        public virtual DbSet<BranchType> BranchType { get; set; }
        public virtual DbSet<JobTitle> JobTitle { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeePhone> EmployeePhone { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        
    }
}
