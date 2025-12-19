using System.ComponentModel.DataAnnotations;

namespace CallCenter.Models
{
    public class MIMULocation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        [Required]
        public string StateCode { get; set; }
        public string StateName { get; set; }

        [Required]
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }

        [Required]
        public string CityCode { get; set; }
        public string CityName { get; set; }

        [Required]
        public string VillageTractCode { get; set; }
        public string VillageTractName { get; set; }

        [Required]
        public string VillageCode { get; set; }
        public string VillageName { get; set; }

        [Required]
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public bool IsActive { get; set; }
    }
}
