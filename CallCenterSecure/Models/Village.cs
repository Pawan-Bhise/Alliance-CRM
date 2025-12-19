using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallCenter.Models
{
    public class Village
    {
        [Key]
        public string VillageCode { get; set; }
        public string VillageName { get; set; }

        [ForeignKey("VillageTract")]
        public string VillageTractCode { get; set; }
        public virtual VillageTract VillageTract { get; set; }
        public bool IsActive { get; set; }
    }
}
