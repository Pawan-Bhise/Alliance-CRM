using CallCenter.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CallCenter.Models
{
    public class SubCategoryMasterModel
    {
        public int SubCategoryId { get; set; }
        [Required(ErrorMessage ="Please enter sub category name")]
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<SubCategoryMaster> SubCategoryList { get; set; }
        public List<CategoryMaster> CategoryList { get; set; }
        
    }
}