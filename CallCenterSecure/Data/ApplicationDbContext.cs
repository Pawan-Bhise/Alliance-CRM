using CallCenter.DataAccess;
using CallCenter.Models;
using CallCenterSecure.Models;
using CallCenterSecure.Models.CustomerLoan;
using CallCenterSecure.Models.Inbound;
using CallCenterSecure.Models.Outbound;
using System.Data.Entity;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() : base("DefaultConnection")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasKey(u => u.UserId);
        modelBuilder.Entity<TicketManagement>().HasKey(t => t.TicketId);
        modelBuilder.Entity<SRManagement>().HasKey(s => s.SRId);

        modelBuilder.Entity<CategoryMaster>().HasKey(c => c.CategoryId);
        modelBuilder.Entity<SubCategoryMaster>().HasKey(sc => sc.SubCategoryId);
        modelBuilder.Entity<SubSubCategoryMaster>().HasKey(ssc => ssc.SubSubCategoryId);
        modelBuilder.Entity<KnowledgeCenter>().HasKey(kc => kc.KnowledgeId);

    }

    // ============================
    // Authentication tables
    // ============================
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<TicketManagement> TicketManagements { get; set; }
    public DbSet<SRManagement> SRManagements { get; set; }
    public DbSet<CategoryMaster> CategoryMasters { get; set; }
    public DbSet<SubCategoryMaster> SubCategoryMasters { get; set; }
    public DbSet<SubSubCategoryMaster> SubSubCategoryMasters { get; set; }
    public DbSet<KnowledgeCenter> KnowledgeCenters { get; set; }
    public DbSet<AllianceInbound> AllianceInbounds { get; set; }
    public DbSet<AllianceOutbound> AllianceOutbounds { get; set; }
    public DbSet<CustomerLoanInformation> CustomerLoanInformations { get; set; }
    public DbSet<Origin> Origins { get; set; }

    // ============================
    // ApplicationDbContext tables
    // ============================
    public DbSet<InboundCall> InboundCalls { get; set; }
    public DbSet<CallObjective> CallObjectives { get; set; }
    public DbSet<RegionBranch> RegionBranches { get; set; }
    public DbSet<WardVillage> WardVillages { get; set; }
    public DbSet<NaDisposition> NaDisposition { get; set; }
    public DbSet<CmpDisposition> CmpDisposition { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<TicketType> TicketTypes { get; set; }
    public DbSet<TicketStatus> TicketStatuses { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<VillageTract> VillageTracts { get; set; }
    public DbSet<Village> Villages { get; set; }
    public DbSet<Area> Areas { get; set; }

    public DbSet<MIMULocation> MIMULocations { get; set; }
    public DbSet<AllianceProduct> AllianceProducts { get; set; }
    public DbSet<AllianceBranch> AllianceBranches { get; set; }
    public DbSet<AllianceDesignation> AllianceDesignations { get; set; }
    public DbSet<Citizen> Citizen { get; set; }
    public DbSet<StateDivision> StateDivision { get; set; }
    public DbSet<Township> Township { get; set; }
    public DbSet<Designations> Designations { get; set; }
    public DbSet<CustomerLoan> CustomerLoan { get; set; }
    

}
