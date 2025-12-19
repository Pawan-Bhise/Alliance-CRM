namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Outbound_Detail_Conversation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllianceOutbounds", "DetailConversation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AllianceOutbounds", "DetailConversation");
        }
    }
}
