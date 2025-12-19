using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CallCenter.DataAccess
{
    public class SRManagement
    {
        public int SRId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string PhoneNo { get; set; }
       
        public string Email { get; set; }
        [Required]
        public string AgentName { get; set; }
        public string RequestComplaintDetails { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string SubSubCategory { get; set; }
        public string Remark { get; set; }
        public DateTime? TicketOpenDate { get; set; }
        public DateTime? TicketCloseDate { get; set; }
        public string TicketOpenAgentName { get; set; }
        public string TicketCloseAgentName { get; set; }
        public string Status { get; set; } //Open, Inprogress, Hold, Closed, Cancelled
        public string TypeOfCaller { get; set; }
        public string CustomerSegment { get; set; }
        public string TypeOfCall { get; set; }
        public string  ResoluctionFeedback { get; set; }
        [NotMapped]
        public string SlaBridge { get; set; }
        public string ClientType { get; set; }
    }
}