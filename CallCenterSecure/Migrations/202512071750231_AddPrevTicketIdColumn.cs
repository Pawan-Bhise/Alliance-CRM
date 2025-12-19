namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrevTicketIdColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllianceInbounds", "Prev_TicketId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AllianceInbounds", "Prev_TicketId");
        }
    }
}
