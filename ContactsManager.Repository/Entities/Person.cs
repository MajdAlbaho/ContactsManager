namespace ContactsManager.Repository.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person.Person")]
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string FullEnName { get; set; }

        [Required]
        [StringLength(200)]
        public string FullArName { get; set; }

        public int JobTitleId { get; set; }

        public virtual JobTitle JobTitle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
