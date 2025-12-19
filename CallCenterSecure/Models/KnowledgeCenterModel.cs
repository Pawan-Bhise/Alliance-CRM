using CallCenter.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CallCenter.Models
{
    public class KnowledgeCenterModel
    {
        public int KnowledgeId { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public string SubCategoryId { get; set; }
        [Required]
        public string SubSubCategoryId { get; set; }
        [Required]
        public string Description { get; set; }
        public string FileURL { get; set; }

        public List<CategoryMaster> CategoryList { get; set; }
        public List<SubCategoryMaster> SubCategoryList { get; set; }
        public List<SubSubCategoryMaster> SubSubCategoryList { get; set; }

        public HttpPostedFileBase File { get; set; }

        public List<KnowledgeCenter> KnowledgeList { get; set; }
    }
}