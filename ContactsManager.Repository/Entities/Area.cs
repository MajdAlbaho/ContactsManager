namespace ContactsManager.Repository.Entities
{
    using ContactsManager.Repository.Interface;
    using ContactsManager.Repository.Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Location.Area")]
    public partial class Area : IEntity<int>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Area()
        {
            Branch = new HashSet<Branch>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string EnName { get; set; }

        [Required]
        [StringLength(200)]
        public string ArName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Branch> Branch { get; set; }
    }
}
