namespace ContactsManager.Repository.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ContactsManagerContext : DbContext
    {
        public ContactsManagerContext()
            : base("name=ContactsManagerContext")
        {
        }

        public virtual DbSet<BranchType> BranchType { get; set; }
        public virtual DbSet<JobTitle> JobTitle { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeePhone> EmployeePhone { get; set; }
        public virtual DbSet<Person> Person { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BranchType>()
                .Property(e => e.EnName)
                .IsUnicode(false);

            modelBuilder.Entity<BranchType>()
                .HasMany(e => e.Branch)
                .WithRequired(e => e.BranchType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<JobTitle>()
                .Property(e => e.EnName)
                .IsUnicode(false);

            modelBuilder.Entity<JobTitle>()
                .HasMany(e => e.Person)
                .WithRequired(e => e.JobTitle)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Area>()
                .Property(e => e.EnName)
                .IsUnicode(false);

            modelBuilder.Entity<Area>()
                .HasMany(e => e.Branch)
                .WithRequired(e => e.Area)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Branch>()
                .Property(e => e.EnName)
                .IsUnicode(false);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Employee)
                .WithRequired(e => e.Branch)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .Property(e => e.EnName)
                .IsUnicode(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Branch)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeePhone)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeePhone>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.FullEnName)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Employee)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);
        }
    }
}
