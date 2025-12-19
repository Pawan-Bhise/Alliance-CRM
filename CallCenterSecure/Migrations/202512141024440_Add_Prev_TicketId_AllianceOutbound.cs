namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Prev_TicketId_AllianceOutbound : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllianceOutbounds", "Prev_TicketId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AllianceOutbounds", "Prev_TicketId");
        }
    }
}
