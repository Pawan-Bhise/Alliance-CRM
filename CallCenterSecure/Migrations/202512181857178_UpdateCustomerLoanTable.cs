namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerLoanTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllianceDesignations", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Areas", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Villages", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.VillageTracts", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cities", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Districts", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.States", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Countries", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Branches", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.MIMULocations", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Origins", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Regions", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Regions", "IsActive");
            DropColumn("dbo.Products", "IsActive");
            DropColumn("dbo.Origins", "IsActive");
            DropColumn("dbo.MIMULocations", "IsActive");
            DropColumn("dbo.Branches", "IsActive");
            DropColumn("dbo.Countries", "IsActive");
            DropColumn("dbo.States", "IsActive");
            DropColumn("dbo.Districts", "IsActive");
            DropColumn("dbo.Cities", "IsActive");
            DropColumn("dbo.VillageTracts", "IsActive");
            DropColumn("dbo.Villages", "IsActive");
            DropColumn("dbo.Areas", "IsActive");
            DropColumn("dbo.AllianceDesignations", "IsActive");
        }
    }
}
