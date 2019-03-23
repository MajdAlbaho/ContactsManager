namespace ContactsManager.Db.Entities
{
    using ContactsManager.Repository.Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person.Employee")]
    public partial class Employee : IEntity<int>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            EmployeePhone = new HashSet<EmployeePhone>();
        }

        public int Id { get; set; }

        public int PersonId { get; set; }

        public int BranchId { get; set; }

        [StringLength(300)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string AnyDesk { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Person Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePhone> EmployeePhone { get; set; }
    }
}
