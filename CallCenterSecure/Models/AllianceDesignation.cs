using System.ComponentModel.DataAnnotations;

namespace CallCenter.Models
{
    public class AllianceDesignation
    {
        [Key]
        public int DesignationID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Branch { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
    }
}
