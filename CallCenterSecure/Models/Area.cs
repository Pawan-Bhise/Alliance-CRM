using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallCenter.Models
{
    public class Area
    {
        [Key]
        public string AreaCode { get; set; }
        public string AreaName { get; set; }

        [ForeignKey("Village")]
        public string VillageCode { get; set; }
        public virtual Village Village { get; set; }
        public bool IsActive { get; set; }
    }
}
