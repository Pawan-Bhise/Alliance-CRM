using CallCenter.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CallCenter.Models
{
    public class SRManagementModel
    {
        public int SRId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        [Required]
        public string AgentName { get; set; }
        [Required]
        public string RequestComplaintDetails { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string SubSubCategory { get; set; }
        [DisplayName("Policy Number")]
        public string Remark { get; set; }
        public List<CategoryMaster> CategoryList { get; set; }
        public List<SubCategoryMaster> SubCategoryList { get; set; }
        public List<SubSubCategoryMaster> SubSubCategoryList { get; set; }
        public List<SRManagement> SRList { get; set; }

        public DateTime? TicketOpenDate { get; set; }
        public DateTime? TicketCloseDate { get; set; }
        public string TicketOpenAgentName { get; set; }
        public string TicketCloseAgentName { get; set; }
        public string Status { get; set; } //Open, Inprogress, Closed

        public string TypeOfCaller { get; set; }
        public string CustomerSegment { get; set; }
        public string TypeOfCall { get; set; }

        public string ResoluctionFeedback { get; set; }
        public string SlaBridge { get; set; }
        public string ClientType { get; set; }
    }
}