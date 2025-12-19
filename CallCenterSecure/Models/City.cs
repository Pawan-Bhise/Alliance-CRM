using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallCenter.Models
{
    public class City
    {
        [Key]
        public string CityCode { get; set; }
        public string CityName { get; set; }

        [ForeignKey("District")]
        public string DistrictCode { get; set; }
        public bool IsActive { get; set; }
        public virtual District District { get; set; }
    }
}
