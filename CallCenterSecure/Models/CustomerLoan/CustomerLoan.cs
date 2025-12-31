using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterSecure.Models.CustomerLoan
{
    public class CustomerLoan
    {
        [Key]
        public int Id { get; set; }

        public int? GroupCode { get; set; }

        [MaxLength(30)]
        public string COCashAccount { get; set; }

        [MaxLength(50)]
        public string COStaffId { get; set; }

        [MaxLength(150)]
        public string COName { get; set; }

        [MaxLength(20)]
        public string ProductCode { get; set; }

        [MaxLength(150)]
        public string ProductName { get; set; }

        [MaxLength(100)]
        public string ProductCategory { get; set; }

        [MaxLength(30)]
        public string CustomerCode { get; set; }

        [MaxLength(30)]
        public string AccountNumber { get; set; }
        
        public int BranchCode { get; set; }

        [MaxLength(150)]
        public string BranchName { get; set; }

        [MaxLength(150)]
        public string ParentBranchName { get; set; }

        [MaxLength(150)]
        public string RegionalBranchName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfActOpening { get; set; }
        
        public int Salutation { get; set; }

        [MaxLength(150)]
        public string CustomerName { get; set; }

        [MaxLength(10)]
        public string Gender { get; set; }

        [MaxLength(150)]
        public string FatherName { get; set; }

        [MaxLength(50)]
        public string AreaType { get; set; }

        [MaxLength(150)]
        public string Area { get; set; }

        [MaxLength(150)]
        public string VillageWard { get; set; }

        [MaxLength(150)]
        public string VillageTractTown { get; set; }

        [MaxLength(150)]
        public string CityTownship { get; set; }

        [MaxLength(150)]
        public string District { get; set; }

        [MaxLength(150)]
        public string RegionState { get; set; }

        [MaxLength(50)]
        public string NRC { get; set; }

        [MaxLength(20)]
        public string MobileNo1 { get; set; }

        [MaxLength(20)]
        public string MobileNo2 { get; set; }

        [MaxLength(50)]
        public string CustomerStatus { get; set; }

        [MaxLength(50)]
        public string FreezeStatus { get; set; }

        [RegularExpression(@"^[0-9,\.]+$", ErrorMessage = "Only numbers, comma (,) and dot (.) are allowed")]
        public string DisbursedAmount { get; set; }

        [RegularExpression(@"^[0-9,\.]+$", ErrorMessage = "Only numbers, comma (,) and dot (.) are allowed")]
        public string LPFAmount { get; set; }

        public int? Installments { get; set; }

        [RegularExpression(@"^[0-9,\.]+$", ErrorMessage = "Only numbers, comma (,) and dot (.) are allowed")]
        public string InstallmentAmount { get; set; }

        [MaxLength(50)]
        public string PaymentFrequency { get; set; }

        [RegularExpression(@"^[0-9,\.]+$", ErrorMessage = "Only numbers, comma (,) and dot (.) are allowed")]
        public string PrincipleOutstanding { get; set; }

        [RegularExpression(@"^[0-9,\.]+$", ErrorMessage = "Only numbers, comma (,) and dot (.) are allowed")]
        public string InterestReceivable { get; set; }

        public string NonCreditCustomer { get; set; }
        public string VoluntaryDepositor { get; set; }

        public string PovertyScore { get; set; }

        [RegularExpression(@"^[0-9,\.]+$", ErrorMessage = "Only numbers, comma (,) and dot (.) are allowed")]
        public string HouseholdSurplusIncome { get; set; }

        [MaxLength(200)]
        public string Purpose { get; set; }

        [MaxLength(150)]
        public string BusinessCategory { get; set; }

        [MaxLength(200)]
        public string BusinessActivity { get; set; }

        [MaxLength(50)]
        public string AccountStatus { get; set; }

        public DateTime? MaturitydateLoan { get; set; }

        [MaxLength(50)]
        public string PARClient { get; set; }

        public int? DayOfOverDue { get; set; }

        [MaxLength(50)]
        public string AreaStatus { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }

}
