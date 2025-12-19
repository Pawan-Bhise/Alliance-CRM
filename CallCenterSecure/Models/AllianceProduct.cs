using System.ComponentModel.DataAnnotations;

namespace CallCenter.Models
{
    public class AllianceProduct
    {
        [Key]
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductGroup { get; set; }
    }
}
