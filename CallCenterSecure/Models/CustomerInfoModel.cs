using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterSecure.Models
{
    public class CustomerInfoModel
    {

        public string AccountType { get; set; }
        public string StaffID { get; set; }
        public string MemberCode { get; set; }
        public string GroupCode { get; set; }
        public string CustomerName { get; set; }
        public string Gender { get; set; }
        public string Salutation { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? ClientAge { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string MaritalStatus { get; set; }
        public string SpouseName { get; set; }
        public string FileName { get; set; }
        public string EducationalQualification { get; set; }
        public string NRC { get; set; }
        public string Branch { get; set; }
        public string SfoCode { get; set; }
        public string ClientOfficerName { get; set; }
        public string ProductInterested { get; set; }
        public string CustomerStatus { get; set; }
        public string State { get; set; }
        public string LeadDistrictCode { get; set; }
        public string Township { get; set; }
        public string Village { get; set; }
        public string Area { get; set; }
        public string Address1 { get; set; }
        public bool BussinessLocationYN { get; set; }
        public string BState { get; set; }
        public string BDistinct { get; set; }
        public string BCity { get; set; }
        public string BVillageTract { get; set; }
        public string BVillageWard { get; set; }
        public string BArea { get; set; }
        public string BAddress { get; set; }
        public string PrimaryMobileNumber { get; set; }
        public string AltMobileNumber { get; set; }
        public double? Lat { get; set; }
        public double? Long { get; set; }

    }
   
    
}