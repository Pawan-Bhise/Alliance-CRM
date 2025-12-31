using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CallCenterSecure.Models
{
    public class AllianceOutbound
    {
        public int AllianceOutboundId { get; set; } // Primary Key

        // === Edited by waqarahmedansari06@gmail.com ===
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd} hh:mm:ss")]
        public DateTime DateTime { get; set; } = DateTime.Now;

        //[Required]
        public string TicketID { get; set; }

        // === Edited by waqarahmedansari06@gmail.com ===
        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }
        [Required(ErrorMessage = "Customer Name is required")]
        [Display(Name = "Customer Name English")]
        public string CustomerNameEnglish { get; set; }
        public string Branch { get; set; }
        [Display(Name = "State Region")]
        public string StateRegion { get; set; }
        public string District { get; set; }
        [Display(Name = "City Township")]
        public string CityTownship { get; set; }
        [Display(Name = "Village Tract Town")]
        public string VillageTractTown { get; set; }
        [Display(Name = "Village Ward")]
        public string VillageWard { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Primary mobile number is required")]
        [Display(Name = "Primary Mobile Number")]
        public string PrimaryMobileNumber { get; set; }
        [Display(Name = "Alternate Mobile Number")]
        public string AlternateMobileNumber { get; set; }
        [Display(Name = "Product Interested")]
        public string ProductInterested { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string NRC { get; set; }
        [Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }
        [Display(Name = "Spouse Name")]
        public string SpouseName { get; set; }
        public string Priority { get; set; }
        [Display(Name = "Client Officer Name")]
        public string ClientOfficerName { get; set; }
        [Required(ErrorMessage = "Call status is required")]
        [Display(Name = "Call Status")]
        public string CallStatus { get; set; }
        [Required(ErrorMessage = "Call type is required")]
        [Display(Name = "Call Type")]
        public string CallType { get; set; }

        public string AgentName { get; set; }
        public string AgentId { get; set; }
        public DateTime? CallStartDateTime { get; set; }
        public DateTime? CallEndDateTime { get; set; }
        public int? Duration { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime ModifiedOn { get; set; } = DateTime.Now;

        [NotMapped]
        public List<AllianceOutbound> AllianceOutboundList { get; set; }
        [NotMapped]
        public string BranchName { get; set; }
        [NotMapped]
        public string StateRegionName { get; set; }
        [NotMapped]
        public string DistrictName { get; set; }

        [NotMapped]
        public string CityTownshipName { get; set; }
        [NotMapped]
        public string VillageTractTownName { get; set; }

        [NotMapped]
        public string ProductInterestedName { get; set; }
        [NotMapped]
        public string PrimaryMobileNumberSearch { get; set; }
        [Display(Name = "Previous TicketId")]
        public string Prev_TicketId { get; set; }
        [Display(Name = "Detail Conversation")]
        public string DetailConversation { get; set; }

        [NotMapped]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? FromDate { get; set; }

        [NotMapped]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? ToDate { get; set; }
    }
}