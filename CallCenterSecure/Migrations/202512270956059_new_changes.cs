namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllianceInbounds", "NRC", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AllianceInbounds", "NRC");
        }
    }
}
