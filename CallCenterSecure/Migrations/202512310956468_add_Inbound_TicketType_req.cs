namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_Inbound_TicketType_req : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AllianceInbounds", "TicketType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AllianceInbounds", "TicketType", c => c.String());
        }
    }
}
