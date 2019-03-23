namespace ContactsManager.Db.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Location.Branch")]
    public partial class Branch : IEntity<int>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Branch()
        {
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string EnName { get; set; }

        [Required]
        [StringLength(200)]
        public string ArName { get; set; }

        public int BranchTypeId { get; set; }

        public int CityId { get; set; }

        public int AreaId { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        public virtual BranchType BranchType { get; set; }

        public virtual Area Area { get; set; }

        public virtual City City { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
