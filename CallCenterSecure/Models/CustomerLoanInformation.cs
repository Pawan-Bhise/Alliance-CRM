using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace CallCenter.Models
{
    public class CustomerLoanInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerLoanId { get; set; }
        public string CustomerContactNumber { get; set; }

        public string AccountType { get; set; }
        public string StaffID { get; set; }
        public string GroupMemberNo { get; set; }
        public string GroupCode { get; set; }

        // Personal Information
        public string PI_CustomerName { get; set; }
        public string PI_Gender { get; set; }
        public string PI_Salutation { get; set; }
        public DateTime PI_DateOfBirth { get; set; }
        public int PI_ClientAge { get; set; }
        public string PI_FatherName { get; set; }
        public string PI_MotherName { get; set; }
        public string PI_MaritalStatus { get; set; }
        public string PI_SpouseName { get; set; }
        //  public byte[] PI_CustomerPhoto { get; set; }
        public string PI_EducationalQualification { get; set; }
        public string PI_NRC { get; set; }
        public string PI_StateRegion { get; set; }
        public string PI_District { get; set; }
        public string PI_City { get; set; }
        public string PI_VillageTractTown { get; set; }
        public string PI_VillageWard { get; set; }
        public string PI_Area { get; set; }
        public string PI_Address { get; set; }

        // Business Information
        public bool BI_IsBusinessAddressSameAsHome { get; set; }
        public string BI_StateRegion { get; set; }
        public string BI_District { get; set; }
        public string BI_City { get; set; }
        public string BI_VillageTractTown { get; set; }
        public string BI_VillageWard { get; set; }
        public string BI_Area { get; set; }
        public string BI_Address { get; set; }

        public string PrimaryMobileNumber { get; set; }
        public string AlternateMobileNumber { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Branch { get; set; }
        public string ClientOfficerName { get; set; }
        public string ProductInterested { get; set; }
        public string CustomerStatus { get; set; }

        public string memberCode { get; set; }
        public string sfoCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
