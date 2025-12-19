using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CallCenter.Models
{
    public class Country
    {
        [Key]
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public bool IsActive { get; set; }

        // Navigation property for related States
        public virtual ICollection<State> States { get; set; }
    }
}
