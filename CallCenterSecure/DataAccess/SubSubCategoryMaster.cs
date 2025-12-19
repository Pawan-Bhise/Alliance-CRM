using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CallCenter.DataAccess
{
    public class SubSubCategoryMaster
    {
        public int SubSubCategoryId { get; set; }
        public string SubSubCategoryName { get; set; }
        public int SubCategoryId { get; set; }
        [NotMapped]
        public string SubCategoryName { get; set; }
    }
}