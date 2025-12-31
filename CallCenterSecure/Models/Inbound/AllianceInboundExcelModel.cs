using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterSecure.Models.Inbound
{
    public class AllianceInboundExcelModel
    {
        public int AllianceInboundId { get; set; }
        public DateTime DateTime { get; set; }
        public string TicketID { get; set; }

        public string CallObjective { get; set; }
        public string Region { get; set; }
        public string Branch { get; set; }
        public string ClientName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public string Origin { get; set; }
        public string Product { get; set; }
        public string DetailConversation { get; set; }
        public string Response { get; set; }

        public int TicketTypeId { get; set; }
        public string TicketType { get; set; }
        public DateTime? FollowUpCallBackSchedule { get; set; }

        public int TicketStatusId { get; set; }
        public string TicketStatus { get; set; }
        public string AgentName { get; set; }

        // Complain Fields
        public string Cmp_CustomerCode { get; set; }
        public string Cmp_CustomerName { get; set; }
        public string Cmp_PhoneNumber { get; set; }
        public string Cmp_Region { get; set; }
        public string Cmp_Branch { get; set; }
        public string Cmp_ComplainToDesignation { get; set; }
        public string Cmp_ComplainTo { get; set; }
        public string Cmp_ComplainCCDesignation { get; set; }
        public string Cmp_Designation { get; set; }
        public string ComplainResolve { get; set; }
        public string Cmp_ComplainCC { get; set; }
        public string Cmp_NatureOfComplaint { get; set; }
        public string Cmp_CaseDetail { get; set; }
        public string Cmp_ComplainStatus { get; set; }
        public string FileName { get; set; }

        // Lead Fields
        public string Lead_CustomerName { get; set; }
        public string Lead_Branch { get; set; }
        public string Lead_StateRegion { get; set; }
        public string Lead_District { get; set; }
        public string Lead_CityTownship { get; set; }
        public string Lead_VillageTractTown { get; set; }
        public string Lead_VillageWard { get; set; }
        public string Lead_Address { get; set; }
        public string Lead_PrimaryMobileNumber { get; set; }
        public string Lead_AlternateMobileNumber { get; set; }
        public string Lead_ProductInterested { get; set; }
        public double? Lead_Latitude { get; set; }
        public double? Lead_Longitude { get; set; }
        public string Lead_NRC { get; set; }
        public DateTime? Lead_DateOfBirth { get; set; }
        public int? Lead_Age { get; set; }
        public string Lead_Gender { get; set; }
        public string Lead_MaritalStatus { get; set; }
        public string Lead_SpouseName { get; set; }
        public string Lead_ClientOfficerName { get; set; }
        public string Lead_LeadStatus { get; set; }

        public string Prev_TicketId { get; set; }

        public string Na_Disposition { get; set; }

    }
}
