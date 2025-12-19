using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using CallCenter.Models;
using CallCenterSecure.Models.Inbound;

namespace CallCenterSecure.Repositories
{
    public class CustomerRepository
    {
        private readonly string _conn;
        public CustomerRepository()
        {
            _conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IEnumerable<AllianceInbound> GetData(int id)
        {
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();

                string sql = @"
                select ai.AllianceInboundId,ai.DateTime,ai.TicketID,
                co.Name AS CallObjective, ai.Region,ai.Branch,ai.ClientName,ai.PhoneNumber,
                ai.Address,
                o.Name as Origin,
                p.Name AS [Product], ai.DetailConversation, ai.Response,
                tt.Name AS TicketType,ai.FollowUpCallBackSchedule,
                ts.Name as TicketStatus, ai.AgentName,

                ai.Cmp_CustomerCode,ai.Cmp_CustomerName,ai.Cmp_PhoneNumber,
                ai.Cmp_Region,ai.Cmp_Branch,ai.Cmp_ComplainToDesignation,dsc.Designation AS Cmp_ComplainTo,
                dscc.Designation AS Cmp_ComplainCC,ai.Cmp_NatureOfComplaint,ai.Cmp_CaseDetail,ai.Cmp_ComplainStatus,ai.FileName,
                
                ai.Lead_CustomerName,lb.Name AS Lead_Branch,lr.Name AS Lead_StateRegion,
                ds.DistrictName AS Lead_District,ai.Lead_CityTownship,
                ai.Lead_VillageTractTown,ai.Lead_VillageWard,ai.Lead_Address,ai.Lead_PrimaryMobileNumber,ai.Lead_AlternateMobileNumber,
                lp.Name AS Lead_ProductInterested,ai.Lead_Latitude,ai.Lead_Longitude,ai.Lead_NRC,ai.Lead_DateOfBirth,ai.Lead_Age,
                ai.Lead_Gender,ai.Lead_MaritalStatus,ai.Lead_SpouseName,ai.Lead_ClientOfficerName,ai.Lead_LeadStatus,
                ai.Prev_TicketId
                from AllianceInbounds ai
                LEFT JOIN CallObjectives co on co.Id=ai.CallObjective
                LEFT JOIN Products p on p.Id=ai.[Product]
                LEFT JOIN Origins o on o.Id=ai.Origin
                LEFT JOIN TicketTypes tt on tt.Id=ai.TicketType
                LEFT JOIN TicketStatus ts on ts.Id=ai.TicketStatus
                LEFT JOIN Designations dsc on dsc.DesignationId=ai.cmp_complainTo
                LEFT JOIN Designations dscc on dscc.DesignationId=ai.cmp_complainCC

                LEFT JOIN Branches lb on ai.Lead_Branch=lb.Id
                LEFT JOIN Regions lr on ai.Lead_StateRegion=lr.Id
                LEFT JOIN Products lp on ai.Lead_ProductInterested=lp.Id
                LEFT JOIN Districts ds on ai.Lead_District=ds.DistrictCode
                where ai.AllianceInboundId=@id;";

                return con.Query<AllianceInbound>(sql, new { Id = id });
            }
        }
        public IEnumerable<InboundGridData> GetDataAll()
        {
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();

                string sql = @"
                select ai.AllianceInboundId,
                ai.TicketID,
                tt.Name AS TicketType,
                ai.TicketType AS TicketTypeId,
                ts.Name as TicketStatus,
                ai.TicketStatus AS TicketStatusId,
                ai.DateTime,
                co.Name AS CallObjective,
                ai.Branch,
                ai.ClientName,
                ai.PhoneNumber,
                p.Name AS [Product],
                ai.Response,
                ai.FollowUpCallBackSchedule,
                ai.Prev_TicketId
                
                from AllianceInbounds ai
                LEFT JOIN CallObjectives co on co.Id=ai.CallObjective
                LEFT JOIN Products p on p.Id=ai.[Product]                
                LEFT JOIN TicketTypes tt on tt.Id=ai.TicketType
                LEFT JOIN TicketStatus ts on ts.Id=ai.TicketStatus
                ORDER BY ai.AllianceInboundId DESC;";

                return con.Query<InboundGridData>(sql);
            }
        }

        public bool DeletePreviousData()
        {
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();

                string sql = @"
                TRUNCATE TABLE CustomerLoanInformations;";

                return con.Execute(sql) > 0 ? true : false;
            }
        }
    }
}
