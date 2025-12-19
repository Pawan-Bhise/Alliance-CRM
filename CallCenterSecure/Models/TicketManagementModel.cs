using CallCenter.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CallCenter.Models
{
    public class TicketManagementModel
    {
        public int TicketId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CallingNumber { get; set; }
        public string TypeOfCaller { get; set; }
        public string CustomerSegment { get; set; }
        public string TypeOfCall { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string SubSubCategory { get; set; }
        public string Remark { get; set; }
        //public List<SelectListItem> CategoryList { get; set; }

        public List<CategoryMaster> CategoryList { get; set; }
        public List<SubCategoryMaster> SubCategoryList { get; set; }
        public List<SubSubCategoryMaster> SubSubCategoryList { get; set; }

        public List<TicketManagement> TicketList { get; set; }

        public DateTime? TicketOpenDate { get; set; }
        public DateTime? TicketCloseDate { get; set; }
        public string TicketOpenAgentName { get; set; }
        public string TicketCloseAgentName { get; set; }
        public string Status { get; set; } //Open, Inprogress, Closed
        [Required]
        public string PolicyNumber { get; set; }
        public string ClientType { get; set; }


    }
}