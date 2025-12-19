using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallCenter.Models
{
    public class District
    {
        [Key]
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }

        [ForeignKey("State")]
        public string StateCode { get; set; }
        public bool IsActive { get; set; }
        public virtual State State { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
