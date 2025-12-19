namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AllianceBranches",
                c => new
                    {
                        BranchCode = c.String(nullable: false, maxLength: 128),
                        BranchName = c.String(),
                        BranchType = c.String(),
                        ParentBranch = c.String(),
                        BranchEmailID = c.String(),
                        BranchAddress = c.String(),
                        HeadOffice = c.String(),
                        RegionalOffice = c.String(),
                    })
                .PrimaryKey(t => t.BranchCode);
            
            CreateTable(
                "dbo.AllianceDesignations",
                c => new
                    {
                        DesignationID = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        Designation = c.String(),
                        Branch = c.String(),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.DesignationID);
            
            CreateTable(
                "dbo.AllianceInbounds",
                c => new
                    {
                        AllianceInboundId = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        TicketID = c.String(),
                        CallObjective = c.String(nullable: false),
                        Region = c.String(nullable: false),
                        Branch = c.String(nullable: false),
                        ClientName = c.String(nullable: false),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        Origin = c.String(nullable: false),
                        Product = c.String(),
                        DetailConversation = c.String(nullable: false),
                        Response = c.String(nullable: false),
                        TicketType = c.String(),
                        FollowUpCallBackSchedule = c.DateTime(),
                        TicketStatus = c.String(nullable: false),
                        AgentName = c.String(),
                        AgentId = c.String(),
                        CallStartDateTime = c.DateTime(),
                        CallEndDateTime = c.DateTime(),
                        Duration = c.Int(),
                        Lead_CustomerName = c.String(),
                        Lead_Branch = c.String(),
                        Lead_StateRegion = c.String(),
                        Lead_District = c.String(),
                        Lead_CityTownship = c.String(),
                        Lead_VillageTractTown = c.String(),
                        Lead_VillageWard = c.String(),
                        Lead_Address = c.String(),
                        Lead_PrimaryMobileNumber = c.String(),
                        Lead_AlternateMobileNumber = c.String(),
                        Lead_ProductInterested = c.String(),
                        Lead_Latitude = c.Double(),
                        Lead_Longitude = c.Double(),
                        Lead_NRC = c.String(),
                        Lead_DateOfBirth = c.DateTime(),
                        Lead_Age = c.Int(),
                        Lead_Gender = c.String(),
                        Lead_MaritalStatus = c.String(),
                        Lead_SpouseName = c.String(),
                        Lead_Priority = c.String(),
                        Lead_ClientOfficerName = c.String(),
                        Lead_LeadStatus = c.String(),
                        Cmp_Disposition = c.String(),
                        Cmp_CustomerCode = c.String(),
                        Cmp_CustomerName = c.String(),
                        Cmp_PhoneNumber = c.String(),
                        Cmp_Region = c.String(),
                        Cmp_Branch = c.String(),
                        Cmp_StateRegion = c.String(),
                        Cmp_District = c.String(),
                        Cmp_CityTownship = c.String(),
                        Cmp_VillageTractTown = c.String(),
                        Cmp_VillageWard = c.String(),
                        Cmp_Address = c.String(),
                        Cmp_PrimaryMobileNumber = c.String(),
                        Cmp_AlternateMobileNumber = c.String(),
                        Cmp_ProductInterested = c.String(),
                        Cmp_Latitude = c.Double(),
                        Cmp_Longitude = c.Double(),
                        Cmp_NRC = c.String(),
                        Cmp_DateOfBirth = c.DateTime(),
                        Cmp_Age = c.Int(),
                        Cmp_Gender = c.String(),
                        Cmp_MaritalStatus = c.String(),
                        Cmp_SpouseName = c.String(),
                        Cmp_Priority = c.String(),
                        Cmp_ClientOfficerName = c.String(),
                        Cmp_LeadStatus = c.String(),
                        Cmp_Product = c.String(),
                        Cmp_Origin = c.String(),
                        Cmp_ComplainToDesignation = c.String(),
                        Cmp_ComplainTo = c.String(),
                        Cmp_ComplainCC = c.String(),
                        Cmp_NatureOfComplaint = c.String(),
                        Cmp_CaseDetail = c.String(),
                        Cmp_ComplainStatus = c.String(),
                        Na_Disposition = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        FileName = c.String(),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.AllianceInboundId);
            
            CreateTable(
                "dbo.AllianceOutbounds",
                c => new
                    {
                        AllianceOutboundId = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        TicketID = c.String(),
                        CustomerCode = c.String(),
                        CustomerNameEnglish = c.String(nullable: false),
                        Branch = c.String(),
                        StateRegion = c.String(),
                        District = c.String(),
                        CityTownship = c.String(),
                        VillageTractTown = c.String(),
                        VillageWard = c.String(),
                        Address = c.String(),
                        PrimaryMobileNumber = c.String(nullable: false),
                        AlternateMobileNumber = c.String(),
                        ProductInterested = c.String(),
                        Latitude = c.Double(),
                        Longitude = c.Double(),
                        NRC = c.String(),
                        DateOfBirth = c.DateTime(),
                        Age = c.Int(),
                        Gender = c.String(),
                        MaritalStatus = c.String(),
                        SpouseName = c.String(),
                        Priority = c.String(),
                        ClientOfficerName = c.String(),
                        CallStatus = c.String(nullable: false),
                        CallType = c.String(nullable: false),
                        AgentName = c.String(),
                        AgentId = c.String(),
                        CallStartDateTime = c.DateTime(),
                        CallEndDateTime = c.DateTime(),
                        Duration = c.Int(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AllianceOutboundId);
            
            CreateTable(
                "dbo.AllianceProducts",
                c => new
                    {
                        ProductCode = c.String(nullable: false, maxLength: 128),
                        ProductName = c.String(),
                        ProductGroup = c.String(),
                    })
                .PrimaryKey(t => t.ProductCode);
            
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        AreaCode = c.String(nullable: false, maxLength: 128),
                        AreaName = c.String(),
                        VillageCode = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AreaCode)
                .ForeignKey("dbo.Villages", t => t.VillageCode)
                .Index(t => t.VillageCode);
            
            CreateTable(
                "dbo.Villages",
                c => new
                    {
                        VillageCode = c.String(nullable: false, maxLength: 128),
                        VillageName = c.String(),
                        VillageTractCode = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.VillageCode)
                .ForeignKey("dbo.VillageTracts", t => t.VillageTractCode)
                .Index(t => t.VillageTractCode);
            
            CreateTable(
                "dbo.VillageTracts",
                c => new
                    {
                        VillageTractCode = c.String(nullable: false, maxLength: 128),
                        VillageTractName = c.String(),
                        CityCode = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.VillageTractCode)
                .ForeignKey("dbo.Cities", t => t.CityCode)
                .Index(t => t.CityCode);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityCode = c.String(nullable: false, maxLength: 128),
                        CityName = c.String(),
                        DistrictCode = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CityCode)
                .ForeignKey("dbo.Districts", t => t.DistrictCode)
                .Index(t => t.DistrictCode);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        DistrictCode = c.String(nullable: false, maxLength: 128),
                        DistrictName = c.String(),
                        StateCode = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DistrictCode)
                .ForeignKey("dbo.States", t => t.StateCode)
                .Index(t => t.StateCode);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateCode = c.String(nullable: false, maxLength: 128),
                        StateName = c.String(),
                        CountryCode = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.StateCode)
                .ForeignKey("dbo.Countries", t => t.CountryCode)
                .Index(t => t.CountryCode);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryCode = c.String(nullable: false, maxLength: 128),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.CountryCode);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CallObjectives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryMasters",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.CmpDispositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerLoanInformations",
                c => new
                    {
                        CustomerLoanId = c.Int(nullable: false, identity: true),
                        CustomerContactNumber = c.String(),
                        AccountType = c.String(),
                        StaffID = c.String(),
                        GroupMemberNo = c.String(),
                        GroupCode = c.String(),
                        PI_CustomerName = c.String(),
                        PI_Gender = c.String(),
                        PI_Salutation = c.String(),
                        PI_DateOfBirth = c.DateTime(nullable: false),
                        PI_ClientAge = c.Int(nullable: false),
                        PI_FatherName = c.String(),
                        PI_MotherName = c.String(),
                        PI_MaritalStatus = c.String(),
                        PI_SpouseName = c.String(),
                        PI_EducationalQualification = c.String(),
                        PI_NRC = c.String(),
                        PI_StateRegion = c.String(),
                        PI_District = c.String(),
                        PI_City = c.String(),
                        PI_VillageTractTown = c.String(),
                        PI_VillageWard = c.String(),
                        PI_Area = c.String(),
                        PI_Address = c.String(),
                        BI_IsBusinessAddressSameAsHome = c.Boolean(nullable: false),
                        BI_StateRegion = c.String(),
                        BI_District = c.String(),
                        BI_City = c.String(),
                        BI_VillageTractTown = c.String(),
                        BI_VillageWard = c.String(),
                        BI_Area = c.String(),
                        BI_Address = c.String(),
                        PrimaryMobileNumber = c.String(),
                        AlternateMobileNumber = c.String(),
                        Latitude = c.Double(),
                        Longitude = c.Double(),
                        Branch = c.String(),
                        ClientOfficerName = c.String(),
                        ProductInterested = c.String(),
                        CustomerStatus = c.String(),
                        memberCode = c.String(),
                        sfoCode = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerLoanId);
            
            CreateTable(
                "dbo.InboundCalls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CallerName = c.String(),
                        PhoneNumber = c.String(),
                        CallDate = c.DateTime(nullable: false),
                        CallObjective = c.String(),
                        Region = c.String(),
                        Branch = c.String(),
                        ClientName = c.String(),
                        Address = c.String(),
                        Origin = c.String(),
                        Product = c.String(),
                        DetailConversation = c.String(),
                        Response = c.String(),
                        TicketType = c.String(),
                        FollowUpCallBackSchedule = c.DateTime(),
                        TicketStatus = c.String(),
                        Action = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KnowledgeCenters",
                c => new
                    {
                        KnowledgeId = c.Int(nullable: false, identity: true),
                        CategoryId = c.String(),
                        SubCategoryId = c.String(),
                        SubSubCategoryId = c.String(),
                        Description = c.String(),
                        FileURL = c.String(),
                    })
                .PrimaryKey(t => t.KnowledgeId);
            
            CreateTable(
                "dbo.MIMULocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryCode = c.String(nullable: false),
                        CountryName = c.String(),
                        StateCode = c.String(nullable: false),
                        StateName = c.String(),
                        DistrictCode = c.String(nullable: false),
                        DistrictName = c.String(),
                        CityCode = c.String(nullable: false),
                        CityName = c.String(),
                        VillageTractCode = c.String(nullable: false),
                        VillageTractName = c.String(),
                        VillageCode = c.String(nullable: false),
                        VillageName = c.String(),
                        AreaCode = c.String(nullable: false),
                        AreaName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NaDispositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Origins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RegionBranches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Region = c.String(),
                        BranchName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ActivationCode = c.Guid(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.SRManagements",
                c => new
                    {
                        SRId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false),
                        PhoneNo = c.String(nullable: false),
                        Email = c.String(),
                        AgentName = c.String(nullable: false),
                        RequestComplaintDetails = c.String(),
                        Category = c.String(),
                        SubCategory = c.String(),
                        SubSubCategory = c.String(),
                        Remark = c.String(),
                        TicketOpenDate = c.DateTime(),
                        TicketCloseDate = c.DateTime(),
                        TicketOpenAgentName = c.String(),
                        TicketCloseAgentName = c.String(),
                        Status = c.String(),
                        TypeOfCaller = c.String(),
                        CustomerSegment = c.String(),
                        TypeOfCall = c.String(),
                        ResoluctionFeedback = c.String(),
                        ClientType = c.String(),
                    })
                .PrimaryKey(t => t.SRId);
            
            CreateTable(
                "dbo.SubCategoryMasters",
                c => new
                    {
                        SubCategoryId = c.Int(nullable: false, identity: true),
                        SubCategoryName = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubCategoryId);
            
            CreateTable(
                "dbo.SubSubCategoryMasters",
                c => new
                    {
                        SubSubCategoryId = c.Int(nullable: false, identity: true),
                        SubSubCategoryName = c.String(),
                        SubCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubSubCategoryId);
            
            CreateTable(
                "dbo.TicketManagements",
                c => new
                    {
                        TicketId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CallingNumber = c.String(),
                        TypeOfCaller = c.String(),
                        CustomerSegment = c.String(),
                        TypeOfCall = c.String(),
                        Category = c.String(),
                        SubCategory = c.String(),
                        SubSubCategory = c.String(),
                        Remark = c.String(),
                        TicketOpenDate = c.DateTime(),
                        TicketCloseDate = c.DateTime(),
                        TicketOpenAgentName = c.String(),
                        TicketCloseAgentName = c.String(),
                        Status = c.String(),
                        PolicyNumber = c.String(),
                        ClientType = c.String(),
                    })
                .PrimaryKey(t => t.TicketId);
            
            CreateTable(
                "dbo.TicketStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WardVillages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SRName = c.String(),
                        District = c.String(),
                        Township = c.String(),
                        TractTown = c.String(),
                        WardEnglishName = c.String(),
                        WardMMRName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        Role_RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Role_RoleId })
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Role_RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Areas", "VillageCode", "dbo.Villages");
            DropForeignKey("dbo.Villages", "VillageTractCode", "dbo.VillageTracts");
            DropForeignKey("dbo.VillageTracts", "CityCode", "dbo.Cities");
            DropForeignKey("dbo.Cities", "DistrictCode", "dbo.Districts");
            DropForeignKey("dbo.Districts", "StateCode", "dbo.States");
            DropForeignKey("dbo.States", "CountryCode", "dbo.Countries");
            DropIndex("dbo.UserRoles", new[] { "Role_RoleId" });
            DropIndex("dbo.UserRoles", new[] { "User_UserId" });
            DropIndex("dbo.States", new[] { "CountryCode" });
            DropIndex("dbo.Districts", new[] { "StateCode" });
            DropIndex("dbo.Cities", new[] { "DistrictCode" });
            DropIndex("dbo.VillageTracts", new[] { "CityCode" });
            DropIndex("dbo.Villages", new[] { "VillageTractCode" });
            DropIndex("dbo.Areas", new[] { "VillageCode" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.WardVillages");
            DropTable("dbo.TicketTypes");
            DropTable("dbo.TicketStatus");
            DropTable("dbo.TicketManagements");
            DropTable("dbo.SubSubCategoryMasters");
            DropTable("dbo.SubCategoryMasters");
            DropTable("dbo.SRManagements");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Regions");
            DropTable("dbo.RegionBranches");
            DropTable("dbo.Products");
            DropTable("dbo.Origins");
            DropTable("dbo.NaDispositions");
            DropTable("dbo.MIMULocations");
            DropTable("dbo.KnowledgeCenters");
            DropTable("dbo.InboundCalls");
            DropTable("dbo.CustomerLoanInformations");
            DropTable("dbo.CmpDispositions");
            DropTable("dbo.CategoryMasters");
            DropTable("dbo.CallObjectives");
            DropTable("dbo.Branches");
            DropTable("dbo.Countries");
            DropTable("dbo.States");
            DropTable("dbo.Districts");
            DropTable("dbo.Cities");
            DropTable("dbo.VillageTracts");
            DropTable("dbo.Villages");
            DropTable("dbo.Areas");
            DropTable("dbo.AllianceProducts");
            DropTable("dbo.AllianceOutbounds");
            DropTable("dbo.AllianceInbounds");
            DropTable("dbo.AllianceDesignations");
            DropTable("dbo.AllianceBranches");
        }
    }
}
