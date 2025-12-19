using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallCenter.Models
{
    public class State
    {
        [Key]
        public string StateCode { get; set; }
        public string StateName { get; set; }

        [ForeignKey("Country")]
        public string CountryCode { get; set; }
        public bool IsActive { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<District> Districts { get; set; }
    }
}
