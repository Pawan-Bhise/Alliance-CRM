using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterSecure.Models.Outbound
{
    public class Township
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TownshipCode { get; set; }
        public string TownshipName { get; set; }
        public int StateDivisionCode { get; set; }
    }
}
