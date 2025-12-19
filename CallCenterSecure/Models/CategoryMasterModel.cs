using CallCenter.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CallCenter.Models
{
    public class CategoryMasterModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Please enter category name")]
        public string CategoryName { get; set; }
        public List<CategoryMaster> CategoryList { get; set; }
    }
}