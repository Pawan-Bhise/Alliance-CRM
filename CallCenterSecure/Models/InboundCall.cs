using Microsoft.SqlServer.Server;
using System;
using System.ComponentModel.DataAnnotations;

namespace CallCenterSecure.Models
{
    public class InboundCall
    {
        public int Id { get; set; }
        public string CallerName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CallDate { get; set; }
        public string CallObjective { get; set; }
        public string Region { get; set; }
        public string Branch { get; set; }
        public string ClientName { get; set; }
        public string Address { get; set; }
        public string Origin { get; set; }
        public string Product { get; set; }
        public string DetailConversation { get; set; }
        public string Response { get; set; }
        public string TicketType { get; set; }
        public DateTime? FollowUpCallBackSchedule { get; set; }
        public string TicketStatus { get; set; }
        public string Action { get; set; }

     
        
    }

    public class CallObjective
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class Origin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class TicketType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TicketStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Adding a new property
        public string Description { get; set; }
    }


    // Written by Waqar Ahmed, Classes for RegionBranch and WardVillage fields
        public class RegionBranch
        {
            public int Id { get; set; }
            public string Region { get; set; }
            public string BranchName { get; set; }
        }

        public class WardVillage
        {
            public int Id { get; set; }
            public string SRName { get; set; }
            public string District { get; set; }
            public string Township { get; set; }
            public string TractTown { get; set; }
            public string WardEnglishName { get; set; }
            public string WardMMRName { get; set; }
        }
        public class NaDisposition
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class CmpDisposition
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        
        
}
