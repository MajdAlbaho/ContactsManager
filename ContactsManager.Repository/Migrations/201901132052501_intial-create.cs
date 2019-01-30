namespace ContactsManager.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Location.Area",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnName = c.String(nullable: false, maxLength: 200, unicode: false),
                        ArName = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Location.Branch",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnName = c.String(nullable: false, maxLength: 200, unicode: false),
                        ArName = c.String(nullable: false, maxLength: 200),
                        BranchTypeId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        AreaId = c.Int(nullable: false),
                        PhoneNumber = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Common.BranchType", t => t.BranchTypeId)
                .ForeignKey("Location.City", t => t.CityId)
                .ForeignKey("Location.Area", t => t.AreaId)
                .Index(t => t.BranchTypeId)
                .Index(t => t.CityId)
                .Index(t => t.AreaId);
            
            CreateTable(
                "Common.BranchType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnName = c.String(nullable: false, maxLength: 200, unicode: false),
                        ArName = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Location.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnName = c.String(nullable: false, maxLength: 200, unicode: false),
                        ArName = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Person.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        Email = c.String(maxLength: 300),
                        AnyDesk = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Person.Person", t => t.PersonId)
                .ForeignKey("Location.Branch", t => t.BranchId)
                .Index(t => t.PersonId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "Person.EmployeePhone",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        PhoneNumber = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.PhoneNumber })
                .ForeignKey("Person.Employee", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "Person.Person",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullEnName = c.String(nullable: false, maxLength: 200, unicode: false),
                        FullArName = c.String(nullable: false, maxLength: 200),
                        JobTitleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Common.JobTitle", t => t.JobTitleId)
                .Index(t => t.JobTitleId);
            
            CreateTable(
                "Common.JobTitle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnName = c.String(nullable: false, maxLength: 200, unicode: false),
                        ArName = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Location.Branch", "AreaId", "Location.Area");
            DropForeignKey("Person.Employee", "BranchId", "Location.Branch");
            DropForeignKey("Person.Person", "JobTitleId", "Common.JobTitle");
            DropForeignKey("Person.Employee", "PersonId", "Person.Person");
            DropForeignKey("Person.EmployeePhone", "EmployeeId", "Person.Employee");
            DropForeignKey("Location.Branch", "CityId", "Location.City");
            DropForeignKey("Location.Branch", "BranchTypeId", "Common.BranchType");
            DropIndex("Person.Person", new[] { "JobTitleId" });
            DropIndex("Person.EmployeePhone", new[] { "EmployeeId" });
            DropIndex("Person.Employee", new[] { "BranchId" });
            DropIndex("Person.Employee", new[] { "PersonId" });
            DropIndex("Location.Branch", new[] { "AreaId" });
            DropIndex("Location.Branch", new[] { "CityId" });
            DropIndex("Location.Branch", new[] { "BranchTypeId" });
            DropTable("Common.JobTitle");
            DropTable("Person.Person");
            DropTable("Person.EmployeePhone");
            DropTable("Person.Employee");
            DropTable("Location.City");
            DropTable("Common.BranchType");
            DropTable("Location.Branch");
            DropTable("Location.Area");
        }
    }
}
