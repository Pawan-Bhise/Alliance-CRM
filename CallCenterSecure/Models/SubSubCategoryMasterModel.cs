using CallCenter.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CallCenter.Models
{
    public class SubSubCategoryMasterModel
    {
        public int SubSubCategoryId { get; set; }
        [Required(ErrorMessage ="Please enter sub sub category anme")]
        public string SubSubCategoryName { get; set; }
        public int SubCategoryId { get; set; }

        public string SubCategoryName{ get; set; }
        public List<SubSubCategoryMaster> SubSubCategoryList { get; set; }
        public List<SubCategoryMaster> SubCategoryList { get; set; }

    }
}