using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterSecure.Models
{
    public class ChartSRTicketModel
    {
        public int TotalSR { get; set; }
        public int OpenSR { get; set; }
        public int CloseSR { get; set; }
        public int InProgressSR { get; set; }
        public int TotalTicket { get; set; }
        public int OpenTicket { get; set; }
        public int CloseTicket { get; set; }
        public int InProgressTicket { get; set; }
        public string Top3Ticket { get; set; }
        public string Top3SR { get; set; }
    }
}