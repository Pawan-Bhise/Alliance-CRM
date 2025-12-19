namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customer_loan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerLoans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupCode = c.Int(),
                        COCashAccount = c.String(maxLength: 30),
                        COStaffId = c.String(maxLength: 50),
                        COName = c.String(maxLength: 150),
                        ProductCode = c.String(maxLength: 20),
                        ProductName = c.String(maxLength: 150),
                        ProductCategory = c.String(maxLength: 100),
                        CustomerCode = c.String(maxLength: 30),
                        AccountNumber = c.String(maxLength: 30),
                        BranchCode = c.Int(nullable: false),
                        BranchName = c.String(maxLength: 150),
                        ParentBranchName = c.String(maxLength: 150),
                        RegionalBranchName = c.String(maxLength: 150),
                        DateOfActOpening = c.DateTime(),
                        Salutation = c.Int(nullable: false),
                        CustomerName = c.String(maxLength: 150),
                        Gender = c.String(maxLength: 10),
                        FatherName = c.String(maxLength: 150),
                        AreaType = c.String(maxLength: 50),
                        Area = c.String(maxLength: 150),
                        VillageWard = c.String(maxLength: 150),
                        VillageTractTown = c.String(maxLength: 150),
                        CityTownship = c.String(maxLength: 150),
                        District = c.String(maxLength: 150),
                        RegionState = c.String(maxLength: 150),
                        NRC = c.String(maxLength: 50),
                        MobileNo1 = c.String(maxLength: 20),
                        MobileNo2 = c.String(maxLength: 20),
                        CustomerStatus = c.String(maxLength: 50),
                        FreezeStatus = c.String(maxLength: 50),
                        DisbursedAmount = c.String(),
                        LPFAmount = c.String(),
                        Installments = c.Int(),
                        InstallmentAmount = c.String(),
                        PaymentFrequency = c.String(maxLength: 50),
                        PrincipleOutstanding = c.String(),
                        InterestReceivable = c.String(),
                        NonCreditCustomer = c.String(),
                        VoluntaryDepositor = c.String(),
                        PovertyScore = c.String(),
                        HouseholdSurplusIncome = c.String(),
                        Purpose = c.String(maxLength: 200),
                        BusinessCategory = c.String(maxLength: 150),
                        BusinessActivity = c.String(maxLength: 200),
                        AccountStatus = c.String(maxLength: 50),
                        MaturitydateLoan = c.DateTime(),
                        PARClient = c.String(maxLength: 50),
                        DayOfOverDue = c.Int(),
                        AreaStatus = c.String(maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomerLoans");
        }
    }
}
