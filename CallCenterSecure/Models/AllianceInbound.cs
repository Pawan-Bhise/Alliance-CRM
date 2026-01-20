using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace CallCenter.Models
{
    public class AllianceInbound
    {
        [Key]
        public int AllianceInboundId { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm:ss}")]
        [Display(Name = "Date Time")]
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string TicketID { get; set; }

        [Required(ErrorMessage = "Call objective is required")]
        [Display(Name = "Call Objective")]
        public string CallObjective { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Region is required")]
        [Display(Name = "Region")]
        public int? RegionId { get; set; }
        
        public string Region { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Branch is required")]
        [Display(Name = "Branch")]
        public int? BranchId { get; set; }        
        public string Branch { get; set; }

        [Display(Name = "Client Name")]
        [Required(ErrorMessage = "Client name is required")]

        public string ClientName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Origin is required")]
        public string Origin { get; set; }
        public string Product { get; set; }
        [Required(ErrorMessage = "Detail conversation is required")]
        [Display(Name = "Detail Conversation")]
        public string DetailConversation { get; set; }
        [Required(ErrorMessage = "Response is required")]
        public string Response { get; set; }
        [Required(ErrorMessage = "Region is required")]
        public string TicketType { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm:ss}")]
        [Display(Name = "FollowUp CallBack Schedule")]
        public DateTime? FollowUpCallBackSchedule { get; set; }
        [Required(ErrorMessage = "Ticket status is required")]
        [Display(Name = "Ticket Status")]
        public string TicketStatus { get; set; }
        public string AgentName { get; set; }
        public string AgentId { get; set; }
        public DateTime? CallStartDateTime { get; set; }
        public DateTime? CallEndDateTime { get; set; }
        public int? Duration { get; set; }

        // Lead Fields
        [Display(Name = "Lead Customer Name")]
        public string Lead_CustomerName { get; set; }
        [Display(Name = "Lead Branch")]
        public string Lead_Branch { get; set; }
        [Display(Name = "Lead State Region")]
        public string Lead_StateRegion { get; set; }
        [Display(Name = "Lead District")]
        public string Lead_District { get; set; }
        [Display(Name = "Lead City Township")]
        public string Lead_CityTownship { get; set; }
        [Display(Name = "Lead Village TractTown")]
        public string Lead_VillageTractTown { get; set; }
        [Display(Name = "Lead Village Ward")]
        public string Lead_VillageWard { get; set; }
        [Display(Name = "Lead Address")]
        public string Lead_Address { get; set; }
        [Display(Name = "Lead Primary Mobile Number")]
        public string Lead_PrimaryMobileNumber { get; set; }
        [Display(Name = "Lead Alternate Mobile Number")]
        public string Lead_AlternateMobileNumber { get; set; }
        [Display(Name = "Lead Product Interested")]
        public string Lead_ProductInterested { get; set; }
        [Display(Name = "Lead Latitude")]
        public double? Lead_Latitude { get; set; }
        [Display(Name = "Lead Longitude")]
        public double? Lead_Longitude { get; set; }
        [Display(Name = "Lead NRC")]
        public string Lead_NRC { get; set; }
        [Display(Name = "Lead Date Of Birth")]

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? Lead_DateOfBirth { get; set; }
        [Display(Name = "Lead Age")]
        public int? Lead_Age { get; set; }
        [Display(Name = "Lead Gender")]
        public string Lead_Gender { get; set; }
        [Display(Name = "Lead Marital Status")]
        public string Lead_MaritalStatus { get; set; }
        [Display(Name = "Lead SpouseName")]
        public string Lead_SpouseName { get; set; }
        [Display(Name = "Lead Priority")]
        public string Lead_Priority { get; set; }
        [Display(Name = "Client Officer Name")]
        public string Lead_ClientOfficerName { get; set; }
        [Display(Name = "Lead Status")]
        public string Lead_LeadStatus { get; set; }

        // Complain Fields
        [Display(Name = "Cmp Disposition")]
        public string Cmp_Disposition { get; set; }

        [Display(Name = "Cmp Customer Code")]
        public string Cmp_CustomerCode { get; set; }

        [Display(Name = "Cmp Customer Name")]
        public string Cmp_CustomerName { get; set; }

        [Display(Name = "Cmp Phone Number")]
        public string Cmp_PhoneNumber { get; set; }

        [Display(Name = "Cmp Region")]
        public string Cmp_Region { get; set; }

        [Display(Name = "Cmp Branch")]
        public string Cmp_Branch { get; set; }

        [Display(Name = "Cmp State Region")]
        public string Cmp_StateRegion { get; set; }

        [Display(Name = "Cmp District")]
        public string Cmp_District { get; set; }

        [Display(Name = "Cmp City Township")]
        public string Cmp_CityTownship { get; set; }

        [Display(Name = "Cmp Village Tract Town")]
        public string Cmp_VillageTractTown { get; set; }

        [Display(Name = "Cmp Village Ward")]
        public string Cmp_VillageWard { get; set; }

        [Display(Name = "Cmp Address")]
        public string Cmp_Address { get; set; }

        [Display(Name = "Cmp Primary Mobile Number")]
        public string Cmp_PrimaryMobileNumber { get; set; }

        [Display(Name = "Cmp Alternate Mobile Number")]
        public string Cmp_AlternateMobileNumber { get; set; }

        [Display(Name = "Cmp Product Interested")]
        public string Cmp_ProductInterested { get; set; }

        [Display(Name = "Cmp Latitude")]
        public double? Cmp_Latitude { get; set; }
        [Display(Name = "CmpLongitude")]
        public double? Cmp_Longitude { get; set; }
        [Display(Name = "Cmp NRC")]
        public string Cmp_NRC { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [Display(Name = "Cmp Date Of Birth")]
        public DateTime? Cmp_DateOfBirth { get; set; }
        public int? Cmp_Age { get; set; }
        public string Cmp_Gender { get; set; }
        public string Cmp_MaritalStatus { get; set; }
        public string Cmp_SpouseName { get; set; }
        public string Cmp_Priority { get; set; }
        public string Cmp_ClientOfficerName { get; set; }
        public string Cmp_LeadStatus { get; set; }
        public string Cmp_Product { get; set; }
        public string Cmp_Origin { get; set; }
        [Display(Name = "Cmp Complain To Designation")]
        public string Cmp_ComplainToDesignation { get; set; }
        [Display(Name = "Cmp Complain CC Designation")]
        public string Cmp_ComplainCCDesignation { get; set; }
        [Display(Name = "Cmp Complain To")]
        public string Cmp_ComplainTo { get; set; }
        [Display(Name = "Cmp Complain CC")]
        public string Cmp_ComplainCC { get; set; }
        [Display(Name = "Cmp Nature Of Complaint")]
        public string Cmp_NatureOfComplaint { get; set; }
        [Display(Name = "Cmp Case Detail")]
        public string Cmp_CaseDetail { get; set; }
        [Display(Name = "Cmp Complain Status")]
        public string Cmp_ComplainStatus { get; set; }
        [Display(Name = "Cmp Designation")]
        public string Cmp_Designation { get; set; }
        // NA Fields
        [Display(Name = "Na Disposition")]
        public string Na_Disposition { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
        public string FileName { get; set; }
        public string FilePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase File { get; set; }
        [NotMapped]
        public string BranchName { get; set; }
        [NotMapped]
        public string CallObjectiveName { get; set; }
        [NotMapped]
        public string RegionName { get; set; }
        [NotMapped]
        public string TicketTypeName { get; set; }
        [NotMapped]
        public string TicketStatusName { get; set; }
        [NotMapped]
        public List<AllianceInbound> AllianceInboundList { get; set; }

        [NotMapped]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? FromDate { get; set; }

        [NotMapped]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? ToDate { get; set; }

        [NotMapped]
        public string OriginName { get; set; }
        // Lead 
        [NotMapped]
        public string LeadBranchName { get; set; }
        [NotMapped]
        public string LeadStateRegionName { get; set; }
        [NotMapped]
        public string LeadDistrictName { get; set; }
        [NotMapped]
        public string LeadCityTownshipName { get; set; }
        [NotMapped]
        public string LeadVillageTractTownName { get; set; }
        [NotMapped]
        public string LeadVillageWardName { get; set; }
        [NotMapped]
        public string LeadProductInterestedName { get; set; }

        // Complain attributes name
        [NotMapped]
        public string CmpRegionName { get; set; }
        [NotMapped]
        public string CmpBranchName { get; set; }
        [NotMapped]
        public string CmpStateRegionName { get; set; }
        [NotMapped]
        public string CmpDistrictName { get; set; }
        [NotMapped]
        public string CmpCityTownshipName { get; set; }
        [NotMapped]
        public string CmpVillageTractTownName { get; set; }
        [NotMapped]
        public string CmpVillageWardName { get; set; }
        [NotMapped]
        public string ProductName { get; set; }

        [Display(Name = "Previous TicketId")]
        public string Prev_TicketId { get; set; }
        [Display(Name = "Complain Resolve")]
        public string ComplainResolve { get; set; }

        public string NRC { get; set; }
    }
}
