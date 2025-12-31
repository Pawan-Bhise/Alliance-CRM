using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallCenter.Models
{
    public class NatureOfComplaints
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComplaintId { get; set; }
        [Required(ErrorMessage = "Complaint description is required")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Only spaces are not allowed")]
        [StringLength(200, ErrorMessage = "Maximum 200 characters allowed")]
        public string ComplaintsDescrption { get; set; }
        public bool IsActive { get; set; }
    }
}
