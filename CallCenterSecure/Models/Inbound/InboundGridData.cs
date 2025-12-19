using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterSecure.Models.Inbound
{
    public class InboundGridData
    {
        public int AllianceInboundId { get; set; }
        public string TicketID { get; set; }
        public string TicketType { get; set; }
        public string TicketTypeId { get; set; }
        public string TicketStatus { get; set; }
        public string TicketStatusId { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string CallObjective { get; set; }
        public string Branch { get; set; }
        public string ClientName { get; set; }
        public string PhoneNumber { get; set; }
        public string Product { get; set; }
        public string Response { get; set; }
        public DateTime? FollowUpCallBackSchedule { get; set; }
        public string Prev_TicketId { get; set; }

    }
}
