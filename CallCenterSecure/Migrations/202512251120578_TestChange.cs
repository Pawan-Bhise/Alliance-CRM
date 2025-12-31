namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class TestChange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComplaintDesignations",
                c => new
                {
                    ComplaintDesignationId = c.Int(nullable: false, identity: true),
                    Description = c.String(),
                    IsActive = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ComplaintDesignationId);

            CreateTable(
                "dbo.NatureOfComplaints",
                c => new
                {
                    ComplaintId = c.Int(nullable: false, identity: true),
                    ComplaintsDescrption = c.String(),
                    IsActive = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ComplaintId);
        }

        public override void Down()
        {
            DropTable("dbo.NatureOfComplaints");
            DropTable("dbo.ComplaintDesignations");
        }
    }

}
