using System.ComponentModel.DataAnnotations;

namespace CallCenter.Models
{
    public class AllianceBranch
    {
        [Key]
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchType { get; set; }
        public string ParentBranch { get; set; }
        public string BranchEmailID { get; set; }
        public string BranchAddress { get; set; }
        public string HeadOffice { get; set; }
        public string RegionalOffice { get; set; }

        public static readonly string[] HeadOffices = { "Head Office 1", "Head Office 2", "Head Office 3" };
        public static readonly string[] RegionalOffices = { "Regional Office 1", "Regional Office 2", "Regional Office 3" };
        public bool IsActive { get; set; }
    }
}
