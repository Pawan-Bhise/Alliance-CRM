namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Designation_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        DesignationId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        Designation = c.String(),
                        Branch = c.String(),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.DesignationId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Designations");
        }
    }
}
