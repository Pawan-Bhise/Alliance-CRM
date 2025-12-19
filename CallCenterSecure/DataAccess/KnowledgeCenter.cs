using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenter.DataAccess
{
    public class KnowledgeCenter
    {
        public int KnowledgeId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string SubSubCategoryId { get; set; }
        public string Description { get; set; }
        public string FileURL { get; set; }
    }
}