namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NRC_Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Citizens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Reference = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StateDivisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateDivisionCode = c.Int(nullable: false),
                        StateDivisionName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Townships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TownshipCode = c.String(),
                        TownshipName = c.String(),
                        StateDivisionCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Townships");
            DropTable("dbo.StateDivisions");
            DropTable("dbo.Citizens");
        }
    }
}
