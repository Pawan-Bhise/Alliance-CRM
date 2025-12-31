namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cmp_des_updates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ComplaintDesignations", "Description", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.NatureOfComplaints", "ComplaintsDescrption", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NatureOfComplaints", "ComplaintsDescrption", c => c.String());
            AlterColumn("dbo.ComplaintDesignations", "Description", c => c.String());
        }
    }
}
