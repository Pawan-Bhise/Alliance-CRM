using CallCenter.Models;
using CallCenterSecure.Models.Inbound;
using System;
using System.Collections.Generic;

namespace CallCenter.Models.ViewModels
{
    public class AllianceInboundIndexVM
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string PhoneNumber { get; set; }
        public string TicketType { get; set; }
        public string TicketStatus { get; set; }
        public string TicketId { get; set; }

        public string TicketTypeId { get; set; }
        public string TicketStatusId { get; set; }
        public List<InboundGridData> InboundList { get; set; }
        public string Prev_TicketId { get; set; }
    }

}