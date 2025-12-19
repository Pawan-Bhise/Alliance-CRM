namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin_isactive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllianceBranches", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AllianceBranches", "IsActive");
        }
    }
}
