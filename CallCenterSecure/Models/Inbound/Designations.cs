using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterSecure.Models.Inbound
{
    public class Designations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DesignationId { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Branch { get; set; }
        public string EmailAddress { get; set; }
    }
}
