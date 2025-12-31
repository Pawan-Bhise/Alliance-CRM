using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterSecure.Models.Outbound
{
    public class AllianceOutboundExcelModel
    {

        public int AllianceOutboundId { get; set; }
        public DateTime DateTime { get; set; }
        public string TicketID { get; set; }

        public string CustomerCode { get; set; }
        public string CustomerNameEnglish { get; set; }

        public string Branch { get; set; }
        public string StateRegion { get; set; }
        public string District { get; set; }
        public string CityTownship { get; set; }
        public string VillageTractTown { get; set; }

        public string VillageWard { get; set; }
        public string PrimaryMobileNumber { get; set; }

        public string ProductInterested { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public string NRC { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }

        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string SpouseName { get; set; }

        public string Priority { get; set; }
        public string ClientOfficerName { get; set; }

        public string CallStatus { get; set; }
        public string CallType { get; set; }

        public string AgentName { get; set; }

        public string Prev_TicketId { get; set; }
        public string DetailConversation { get; set; }


    }
}
