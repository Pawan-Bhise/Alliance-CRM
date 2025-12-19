namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_Inbound_ComplainResolve : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllianceInbounds", "ComplainResolve", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AllianceInbounds", "ComplainResolve");
        }
    }
}
