namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class satbeer_changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllianceInbounds", "Cmp_ComplainCCDesignation", c => c.String());
            AddColumn("dbo.AllianceInbounds", "Cmp_Designation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AllianceInbounds", "Cmp_Designation");
            DropColumn("dbo.AllianceInbounds", "Cmp_ComplainCCDesignation");
        }
    }
}
