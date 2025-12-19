using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallCenter.Models
{
    public class VillageTract
    {
        [Key]
        public string VillageTractCode { get; set; }
        public string VillageTractName { get; set; }

        [ForeignKey("City")]
        public string CityCode { get; set; }
        public virtual City City { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Village> Villages { get; set; }
    }
}
